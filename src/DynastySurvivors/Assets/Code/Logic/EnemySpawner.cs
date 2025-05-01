using Code.Infrastructure.Services.Identifiers;
using Code.StaticData;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Code.Logic
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyTypeId _enemyTypeId;
        [SerializeField] private bool _slain;
        
        private int _id;

        [Inject]
        private void Construct(IIdentifierService identifier)
        {
            _id = identifier.Next();
        }
    }
}