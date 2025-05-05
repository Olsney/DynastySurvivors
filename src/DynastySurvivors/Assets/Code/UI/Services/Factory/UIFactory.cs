using Code.Infrastructure.AssetManagement;
using Code.Services.PersistentProgress;
using Code.Services.StaticData;
using Code.StaticData.Windows;
using Code.UI.Services.Windows;
using Code.UI.Windows;
using UnityEngine;
using Zenject;

namespace Code.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private readonly IInstantiator _container;
        private readonly IPersistentProgressService _progressService;

        private Transform _uiRoot;

        public UIFactory(
            IAssetProvider assets, 
            IStaticDataService staticData, 
            IInstantiator container, 
            IPersistentProgressService progressService
            )
        {
            _assets = assets;
            _staticData = staticData;
            _container = container;
            _progressService = progressService;
        }

        public void CreateUIRoot()
        {
            GameObject prefab = _assets.Load(Constants.UIRootPath);
            _uiRoot = _container.InstantiatePrefab(prefab).transform;
        }

        public void CreateShop()
        {
            WindowConfig config = _staticData.GetWindow(WindowId.Shop);
            WindowBase window = Object.Instantiate(config.Prefab, _uiRoot);
            
            window.Construct(_progressService);
        }
    }
}