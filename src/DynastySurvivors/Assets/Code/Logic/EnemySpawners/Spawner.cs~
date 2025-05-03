using Code.Data;
using Code.Infrastructure.Factory;
using Code.Infrastructure.Services.Identifiers;
using Code.Services.PersistentProgress;
using Code.Services.StaticData.Enemy;
using UnityEngine;

namespace Code.Logic.EnemySpawners
{
    public class Spawner : MonoBehaviour, ISavedProgressReader
    {
        [SerializeField] private EnemyTypeId _enemyTypeId;
        
        private IIdentifierService _identifier;
        private IGameFactory _factory;

        [field: SerializeField] public int Id { get; private set; }

        public void Construct(IIdentifierService identifier, IGameFactory factory)
        {
            _identifier = identifier;
            _factory = factory;
            
            Id = _identifier.Next();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            Spawn();
        }

        private void Spawn()
        { 
            _factory.CreateEnemy(_enemyTypeId, transform);
        }
    }
}