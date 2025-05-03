using Code.Data;
using Code.Infrastructure.Factory;
using Code.Services.Random;
using UnityEngine;

namespace Code.Enemy
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyDeath _enemyDeath;
        
        private IGameFactory _factory;
        private int _minLootValue;
        private int _maxLootValue;
        private IRandomService _random;

        public void Construct(IGameFactory factory, IRandomService random)
        {
            _factory = factory;
            _random = random;
        }

        private void Start()
        {
            _enemyDeath.Died += SpawnLoot;
        }

        private void SpawnLoot()
        {
            LootPiece loot = _factory.CreateLoot();
            loot.transform.position = transform.position;

            Loot lootItem = GenerateLoot();

            loot.Initialize(lootItem);
        }

        private Loot GenerateLoot()
        {
            return new Loot()
            {
                Value = _random.Next(_minLootValue, _maxLootValue)
            };
        }

        public void SetLoot(int minLootValue, int maxLootValue)
        {
            _minLootValue = minLootValue;
            _maxLootValue = maxLootValue;
        }
    }
}