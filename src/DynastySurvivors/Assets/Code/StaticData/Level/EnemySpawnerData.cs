using System;
using Code.StaticData.Enemy;
using UnityEngine;

namespace Code.StaticData.Level
{
    [Serializable]
    public class EnemySpawnerData
    {
        public string Id;
        public EnemyTypeId EnemyTypeId;
        public Vector3 Position;

        public EnemySpawnerData(string id, EnemyTypeId enemyTypeId, Vector3 position)
        {
            Id = id;
            EnemyTypeId = enemyTypeId;
            Position = position;
        }
    }
}