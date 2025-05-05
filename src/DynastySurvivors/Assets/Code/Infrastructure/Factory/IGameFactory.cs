using System.Collections.Generic;
using Code.Enemy;
using Code.Infrastructure.Services;
using Code.Services.PersistentProgress;
using Code.StaticData.Enemy;
using Code.StaticData.Hero;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject HeroGameObject { get; }
        GameObject CreateHud();
        GameObject CreateCurtain();
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        void Cleanup();
        GameObject CreateSaveTriggerContainer();
        GameObject CreateEnemy(EnemyTypeId enemyTypeId, Transform container);
        GameObject CreateHero(HeroTypeId heroTypeId, Vector3 at);
        LootPiece CreateLoot();
        void CreateEnemySpawner(Vector3 at, string spawnerId, EnemyTypeId enemyTypeId, float minSpawnInterval, float maxSpawnInterval);
    }
}