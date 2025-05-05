using System;
using Code.UI.Services.Windows;
using Code.UI.Windows;

namespace Code.StaticData.Windows
{
    [Serializable]
    public class WindowConfig 
    {
        public WindowId WindowId;
        public WindowBase Prefab;
    }
}