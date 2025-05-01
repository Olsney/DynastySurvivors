using System;
using System.Collections.Generic;
using Code.Infrastructure.AssetManagement;
using Code.Services.PersistentProgress;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        public event Action HeroCreated;
        
        private readonly IAssetProvider _assets;
        private readonly IInstantiator _container;
        private GameObject _heroGameObject;

        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();

        public GameObject HeroGameObject => _heroGameObject;

        public GameFactory(IAssetProvider assets, IInstantiator container)
        {
            _assets = assets;
            _container = container;
        }


        public GameObject CreateSaveTriggerContainer() => 
            InstantiateRegistered(AssetPath.SaveTriggerContainerPath);

        public GameObject CreateHero(GameObject at)
        {
            _heroGameObject = InstantiateRegistered(AssetPath.HeroPath, at.transform.position);

            HeroCreated?.Invoke();

            return _heroGameObject;
        }

        public GameObject CreateHud() => 
            InstantiateRegistered(AssetPath.HudPath);

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        public GameObject CreateCurtain()
        {
            GameObject prefab = _assets.Load(AssetPath.LoadCurtainPath);
            
            return _container.InstantiatePrefab(prefab);
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject prefab = _assets.Load(path: prefabPath);
            GameObject instance = _container.InstantiatePrefab(prefab, at, Quaternion.identity, null);
            RegisterProgressWatchers(instance);

            return instance;
        }

        public void Register(ISavedProgressReader progressReader)
        {
            if(progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);
            
            ProgressReaders.Add(progressReader);
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject prefab = _assets.Load(path: prefabPath);
            GameObject instance = _container.InstantiatePrefab(prefab);
            RegisterProgressWatchers(instance);

            return instance;
        }

        private void RegisterProgressWatchers(GameObject hero)
        {
            foreach (ISavedProgressReader progressReader in hero.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }
    }
}