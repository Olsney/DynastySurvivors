using System;
using System.Collections.Generic;
using Code.Enemy;
using Code.Hero;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Services.Identifiers;
using Code.Logic;
using Code.Logic.EnemySpawners;
using Code.Services.PersistentProgress;
using Code.Services.Random;
using Code.Services.StaticData;
using Code.Services.StaticData.Enemy;
using Code.Services.StaticData.Hero;
using Code.UI;
using Code.UI.Elements;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Object = UnityEngine.Object;

namespace Code.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IInstantiator _container;
        private readonly IStaticDataService _staticData;
        private readonly IRandomService _randomService;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IIdentifierService _identifierService;

        private GameObject _heroGameObject;

        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();

        public GameObject HeroGameObject => _heroGameObject;

        public GameFactory(
            IAssetProvider assets, 
            IInstantiator container, 
            IStaticDataService staticData, 
            IRandomService randomService,
            IPersistentProgressService persistentProgressService, 
            IIdentifierService identifierService
            )
        {
            _assets = assets;
            _container = container;
            _staticData = staticData;
            _randomService = randomService;
            _persistentProgressService = persistentProgressService;
            _identifierService = identifierService;
        }


        public GameObject CreateSaveTriggerContainer() => 
            InstantiateRegistered(AssetPath.SaveTriggerContainerPath);

        public GameObject CreateHero(HeroTypeId heroTypeId, Vector3 at)
        {
            HeroStaticData heroData = _staticData.GetHero(heroTypeId);
            
            _heroGameObject = InstantiateRegistered(heroData.Prefab, at);

            IHealth health = _heroGameObject.GetComponent<IHealth>();
            health.Initialize(
                heroData.CurrentHealth, 
                heroData.MaxHealth
                );

            HeroMove heroMove = _heroGameObject.GetComponent<HeroMove>();
            heroMove.Initialize(heroData.MovementSpeed);

            HeroAttack heroAttack = _heroGameObject.GetComponent<HeroAttack>();
            heroAttack.Initialize(
                heroData.Damage,
                heroData.DamageRadius,
                heroData.AttackCooldown
                );

            return _heroGameObject;
        }

        public GameObject CreateHud()
        {
            GameObject hud = InstantiateRegistered(AssetPath.HudPath);

            hud.GetComponentInChildren<LootCounter>()
                .Construct(_persistentProgressService.Progress.WorldData);
            
            return hud;
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        public GameObject CreateCurtain()
        {
            GameObject prefab = _assets.Load(AssetPath.LoadCurtainPath);
            
            return _container.InstantiatePrefab(prefab);
        }

        public GameObject CreateEnemy(EnemyTypeId enemyTypeId, Transform container)
        {
            EnemyStaticData enemyData = _staticData.GetEnemy(enemyTypeId);

            GameObject enemy = Object.Instantiate(enemyData.Prefab, container.position, Quaternion.identity, container);

            IHealth health = enemy.GetComponent<IHealth>();
            health.Initialize(enemyData.Health, enemyData.Health);
            
            enemy.GetComponent<ActorUI>().Construct(health);
            enemy.GetComponent<EnemyMoveToHero>().Construct(HeroGameObject.transform, _randomService);
            enemy.GetComponent<NavMeshAgent>().speed = enemyData.MoveSpeed;

            LootSpawner lootSpawner = enemy.GetComponentInChildren<LootSpawner>();
            lootSpawner.SetLoot(enemyData.MinLootValue, enemyData.MaxLootValue);
            lootSpawner.Construct(this, _randomService);

            switch (enemyTypeId)
            {
                case EnemyTypeId.Skeleton:
                    CreateSkeleton(enemy, enemyData);
                    break;
                case EnemyTypeId.Giant:
                    CreateGiant(enemy, enemyData);
                    break;
            }
            
            enemy.GetComponent<EnemyRotateToHero>()?.Construct(HeroGameObject.transform);
            
            return enemy;
        }

        private void CreateGiant(GameObject enemy, EnemyStaticData enemyData)
        {
            EnemyMeleeAttack enemyMeleeAttack = enemy.GetComponent<EnemyMeleeAttack>();
            enemyMeleeAttack.Construct(HeroGameObject.transform);
            enemyMeleeAttack.Initialize(
                enemyData.Damage, 
                enemyData.AttackCooldown, 
                enemyData.AttackOffsetY, 
                enemyData.AttackOffsetForward, 
                enemyData.AttackCleavage
                );
        }

        private static void CreateSkeleton(GameObject enemy, EnemyStaticData enemyData)
        {
            EnemyAreaPassiveAttack enemyAreaPassiveAttack = enemy.GetComponent<EnemyAreaPassiveAttack>();
            enemyAreaPassiveAttack.Initialize(
                enemyData.Damage, 
                enemyData.AttackCooldown
                );
        }

        public LootPiece CreateLoot()
        {
            LootPiece lootPiece = InstantiateRegistered(AssetPath.LootPath)
                .GetComponent<LootPiece>();
            lootPiece.Construct(_persistentProgressService.Progress.WorldData);
            return lootPiece;
        }

        public void CreateEnemySpawner(Vector3 at, string spawnerId, EnemyTypeId enemyTypeId, float minSpawnInterval, float maxSpawnInterval)
        {
            EnemySpawner enemySpawner = InstantiateRegistered(AssetPath.EnemySpawnerPath, at)
                .GetComponent<EnemySpawner>();

            // enemySpawner.Construct(identifier: _identifierService, factory: this);
            enemySpawner.Initialize(enemyTypeId, minSpawnInterval, maxSpawnInterval);
        }

        private GameObject InstantiateRegistered(GameObject prefab, Vector3 at)
        {
            GameObject instance = _container.InstantiatePrefab(prefab, at, Quaternion.identity, null);
      
            RegisterProgressWatchers(instance);
            return instance;
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject prefab = _assets.Load(path: prefabPath);
            GameObject instance = _container.InstantiatePrefab(prefab, at, Quaternion.identity, null);
            RegisterProgressWatchers(instance);

            return instance;
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject prefab = _assets.Load(path: prefabPath);
            GameObject instance = _container.InstantiatePrefab(prefab);
            RegisterProgressWatchers(instance);

            return instance;
        }

        public void Register(ISavedProgressReader progressReader)
        {
            if(progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);
            
            ProgressReaders.Add(progressReader);
        }

        private void RegisterProgressWatchers(GameObject hero)
        {
            foreach (ISavedProgressReader progressReader in hero.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }
    }
}