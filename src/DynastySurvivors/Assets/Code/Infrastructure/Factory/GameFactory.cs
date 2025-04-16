using System.Collections.Generic;
using Code.Infrastructure.AssetManagement;
using Code.Services.PersistentProgress;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IInstantiator _container;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public GameFactory(IAssetProvider assets, IInstantiator container)
        {
            _assets = assets;
            _container = container;
        }
        
        public GameObject CreateSaveTriggerContainer() => 
            InstantiateRegistered(AssetPath.SaveTriggerContainerPath);
        
        public GameObject CreateHero(GameObject at) => 
            InstantiateRegistered(AssetPath.HeroPath, at.transform.position);

        public void CreateHud() => 
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

        private void Register(ISavedProgressReader progressReader)
        {
            if(progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);
            
            ProgressReaders.Add(progressReader);
        }
    }
}