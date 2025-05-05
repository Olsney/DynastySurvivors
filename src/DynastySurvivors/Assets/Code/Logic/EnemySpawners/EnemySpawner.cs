using Code.Data;
using Code.Infrastructure.Factory;
using Code.Infrastructure.Services.Identifiers;
using Code.Services.PersistentProgress;
using Code.Services.Random;
using Code.Services.Times;
using Code.StaticData.Enemy;
using UnityEngine;
using Zenject;

namespace Code.Logic.EnemySpawners
{
    public class EnemySpawner : MonoBehaviour, ISavedProgressReader
    {
        private EnemyTypeId _enemyTypeId;
        
        private IIdentifierService _identifier;
        private IGameFactory _factory;
        private IRandomService _random;
        private ICooldownService _cooldown;
        private float _minSpawnInterval;
        private float _maxSpawnInterval;

        [field: SerializeField] public int Id { get; private set; }

        [Inject]
        private void Construct(IIdentifierService identifier, IGameFactory factory, IRandomService randomService)
        {
            _identifier = identifier;
            _factory = factory;
            _random = randomService;
            
            Id = _identifier.Next();
        }

        public void Initialize(EnemyTypeId enemyTypeId, float minSpawnInterval, float maxSpawnInterval)
        {
            _enemyTypeId = enemyTypeId;
            _minSpawnInterval = minSpawnInterval;
            _maxSpawnInterval = maxSpawnInterval;

            _cooldown = new CooldownService(GetNewSpawnDelay());
        }

        private void Update()
        {
            if (_cooldown == null)
                return;

            _cooldown.Tick(Time.deltaTime);

            if (_cooldown.IsReady)
            {
                Spawn();
                _cooldown.SetCooldown(GetNewSpawnDelay());
            }
        }

        public void LoadProgress(PlayerProgress progress)
        {
            Spawn();
            
            _cooldown.SetCooldown(GetNewSpawnDelay());
        }

        private void Spawn()
        { 
            _factory.CreateEnemy(_enemyTypeId, transform);
        }

        private float GetNewSpawnDelay() => 
            _random.Next(_minSpawnInterval, _maxSpawnInterval);
    }
}