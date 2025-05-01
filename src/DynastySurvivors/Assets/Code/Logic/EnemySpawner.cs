using Code.Data;
using Code.Infrastructure.Services.Identifiers;
using Code.Services.PersistentProgress;
using Code.StaticData;
using UnityEngine;
using Zenject;

namespace Code.Logic
{
    public class EnemySpawner : MonoBehaviour, ISavedProgressReader
    {
        [SerializeField] private EnemyTypeId _enemyTypeId;
        
        private IIdentifierService _identifier;
        
        [field: SerializeField] public int Id { get; private set; }

        [Inject]
        private void Construct(IIdentifierService identifier)
        {
            _identifier = identifier;
        }

        private void Awake()
        {
            Id = _identifier.Next();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            Spawn();
        }

        private void Spawn()
        {
        }
    }
}