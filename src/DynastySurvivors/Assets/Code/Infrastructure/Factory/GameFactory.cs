using Code.Infrastructure.AssetManagement;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IInstantiator _container;

        public GameFactory(IAssetProvider assets, IInstantiator container)
        {
            _assets = assets;
            _container = container;
        }
        
        public GameObject CreateHero(GameObject at)
        {
            // _assets.Instantiate(AssetPath.HeroPath, at: at.transform.position);
            GameObject prefab = _assets.Load(AssetPath.HeroPath);

            var instance = _container.InstantiatePrefab(prefab, at.transform.position, Quaternion.identity, null);

            return instance;
        }

        public void CreateHud()
        {
            _assets.Instantiate(AssetPath.HudPath);
        }
        
        public GameObject CreateCurtain()
        {
            GameObject prefab = _assets.Load(AssetPath.LoadCurtainPath);
            
            return _container.InstantiatePrefab(prefab);
        }
    }
}