using Code.StaticData.Enemy;
using UnityEngine;

namespace Code.Logic.EnemySpawners
{
    public class EnemySpawnPoint : MonoBehaviour
    {
        [SerializeField] private EnemyTypeId _enemyTypeId;
        
        public EnemyTypeId EnemyTypeId => _enemyTypeId;
    }
}