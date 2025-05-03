using Code.Enemy;
using Code.Infrastructure.Factory;
using Code.Services.StaticData.Enemy;
using UnityEngine;

namespace Code.Logic.EnemySpawners
{
    public class Spawner : MonoBehaviour
    {
        private IGameFactory _factory;
        private EnemyDeath _enemyDeath;
        private string _id;
        private EnemyTypeId _enemyTypeId;

    }
}