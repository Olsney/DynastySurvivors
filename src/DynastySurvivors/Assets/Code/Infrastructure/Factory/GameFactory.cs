using System;
using System.Collections.Generic;
using Code.Enemy;
using Code.Infrastructure.AssetManagement;
using Code.Logic;
using Code.Services.PersistentProgress;
using Code.Services.StaticData;
using Code.UI;
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
        
        private GameObject _heroGameObject;

        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();

        public GameObject HeroGameObject => _heroGameObject;

        public GameFactory(IAssetProvider assets, IInstantiator container, IStaticDataService staticData)
        {
            _assets = assets;
            _container = container;
            _staticData = staticData;
        }


        public GameObject CreateSaveTriggerContainer() => 
            InstantiateRegistered(AssetPath.SaveTriggerContainerPath);

        public GameObject CreateHero(GameObject at)
        {
            _heroGameObject = InstantiateRegistered(AssetPath.HeroPath, at.transform.position);
            
            return _heroGameObject;
        }

        public GameObject CreateHud() => 
            InstantiateRegistered(AssetPath.HudPath);

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

        public GameObject CreateEnemy(EnemyTypeId enemyTypeId, Transform parent)
        {
            EnemyStaticData enemyData = _staticData.ForEnemy(enemyTypeId);

            GameObject enemy = Object.Instantiate(enemyData.Prefab, parent.position, Quaternion.identity, parent);

            IHealth health = enemy.GetComponent<IHealth>();
            health.Initialize(enemyData.Health, enemyData.Health);
            
            enemy.GetComponent<ActorUI>().Construct(health);
            enemy.GetComponent<EnemyMoveToHero>().Construct(HeroGameObject.transform);
            enemy.GetComponent<NavMeshAgent>().speed = enemyData.MoveSpeed;
            
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
                enemyData.AttackCleavage);
        }

        private static void CreateSkeleton(GameObject enemy, EnemyStaticData enemyData)
        {
            EnemyAreaPassiveAttack enemyAreaPassiveAttack = enemy.GetComponent<EnemyAreaPassiveAttack>();
            enemyAreaPassiveAttack.Initialize(enemyData.Damage, enemyData.AttackCooldown);
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject prefab = _assets.Load(path: prefabPath);
            GameObject instance = _container.InstantiatePrefab(prefab, at, Quaternion.identity, null);
            RegisterProgressWatchers(instance);

            return instance;
        }

        public void Register(ISavedProgressReader progressReader)
        {
            if(progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);
            
            ProgressReaders.Add(progressReader);
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject prefab = _assets.Load(path: prefabPath);
            GameObject instance = _container.InstantiatePrefab(prefab);
            RegisterProgressWatchers(instance);

            return instance;
        }

        private void RegisterProgressWatchers(GameObject hero)
        {
            foreach (ISavedProgressReader progressReader in hero.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }
    }
}