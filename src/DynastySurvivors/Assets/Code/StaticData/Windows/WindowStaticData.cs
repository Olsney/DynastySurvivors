using System.Collections.Generic;
using UnityEngine;

namespace Code.StaticData.Windows
{
    [CreateAssetMenu(fileName = "WindowData", menuName = "StaticData/WindowData")]
    public class WindowStaticData : ScriptableObject
    {
        public List<WindowConfig> Configs;
    }
}