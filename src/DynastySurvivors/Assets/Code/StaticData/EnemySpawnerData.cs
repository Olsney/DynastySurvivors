using System;
using Code.Services.StaticData.Enemy;
using UnityEngine;

namespace Code.StaticData
{
    [Serializable]
    public class EnemySpawnerData
    {
        public string Id;
        public EnemyTypeId EnemyTypeId;
        public Vector3 Position;
    }
}