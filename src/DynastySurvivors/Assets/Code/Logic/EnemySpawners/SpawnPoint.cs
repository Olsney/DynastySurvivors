using Code.Services.StaticData.Enemy;
using UnityEngine;

namespace Code.Logic.EnemySpawners
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private EnemyTypeId _enemyTypeId;
        
        public EnemyTypeId EnemyTypeId => _enemyTypeId;
    }
}