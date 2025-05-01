using System;
using System.Collections.Generic;
using Code.Infrastructure.Services;
using Code.Services.PersistentProgress;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(GameObject at);
        GameObject HeroGameObject { get; }
        event Action HeroCreated;
        GameObject CreateHud();
        GameObject CreateCurtain();
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        void Cleanup();
        GameObject CreateSaveTriggerContainer();
        void Register(ISavedProgressReader progressReader);

    }
}