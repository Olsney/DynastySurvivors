using System;
using System.Collections.Generic;
using Code.Enemy;
using Code.Infrastructure.Services;
using Code.Services.PersistentProgress;
using Code.Services.StaticData;
using Code.Services.StaticData.Enemy;
using Code.Services.StaticData.Hero;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        // GameObject CreateHero(GameObject at);
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
        void CreateSpawner(Vector3 at, string spawnerId, EnemyTypeId enemyTypeId);
    }
}