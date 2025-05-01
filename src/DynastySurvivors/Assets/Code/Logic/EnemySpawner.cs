using Code.StaticData;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Logic
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyTypeId _enemyTypeId;
        [SerializeField] private bool _slain;
        
        private string _id;
    }
}