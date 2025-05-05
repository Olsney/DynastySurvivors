using Code.Infrastructure.AssetManagement;
using Code.Services.StaticData;
using Code.StaticData.Windows;
using Code.UI.Services.Windows;
using Code.UI.Windows;
using UnityEngine;

namespace Code.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private const string UIRootPath = "UI/UIRoot";
        
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        
        private Transform _uiRoot;

        public UIFactory(IAssetProvider assets, IStaticDataService staticData)
        {
            _assets = assets;
            _staticData = staticData;
        }

        public void CreateShop()
        {
            WindowConfig config = _staticData.GetWindow(WindowId.Shop);
            WindowBase windowBase = Object.Instantiate(config.Prefab, _uiRoot);
            
        }

        public void CreateUIRoot() => 
            _uiRoot = _assets.Load(UIRootPath).transform;
    }
}