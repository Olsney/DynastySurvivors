using Code.Infrastructure.AssetManagement;
using Code.Services.StaticData;
using Code.StaticData.Windows;
using Code.UI.Services.Windows;
using UnityEngine;
using Zenject;

namespace Code.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private const string UIRootPath = "UI/UIRoot";
        
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private readonly IInstantiator _container;

        private Transform _uiRoot;

        public UIFactory(IAssetProvider assets, IStaticDataService staticData, IInstantiator container)
        {
            _assets = assets;
            _staticData = staticData;
            _container = container;
        }

        public void CreateShop()
        {
            WindowConfig config = _staticData.GetWindow(WindowId.Shop);
            Object.Instantiate(config.Prefab, _uiRoot);
        }
        
        public void CreateUIRoot()
        {
            GameObject prefab = _assets.Load(UIRootPath);
            _uiRoot = _container.InstantiatePrefab(prefab).transform;
        }
    }
}