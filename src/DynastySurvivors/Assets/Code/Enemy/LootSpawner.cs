using System;
using Code.Infrastructure.Factory;
using UnityEngine;
using Zenject;

namespace Code.Enemy
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyDeath _enemyDeath;
        private IGameFactory _factory;
        
        public void Construct(IGameFactory factory)
        {
            _factory = factory;
        }

        private void Start()
        {
            _enemyDeath.Died += SpawnLoot;
        }

        private void SpawnLoot()
        {
            GameObject loot = _factory.CreateLoot();
            loot.transform.position = transform.position;
        }
    }
}