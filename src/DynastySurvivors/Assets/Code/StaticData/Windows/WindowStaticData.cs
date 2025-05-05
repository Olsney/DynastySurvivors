using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Code.StaticData.Windows
{
    [CreateAssetMenu(fileName = "WindowData", menuName = "StaticData/Window Data")]
    public class WindowStaticData : ScriptableObject
    {
        [PropertyOrder(-1)]
        [BoxGroup("✦ Configuration", centerLabel: true)]
        [Title("Window Configuration", "List of all windows and their associated prefabs", TitleAlignments.Centered)]
        [TableList(ShowIndexLabels = true, AlwaysExpanded = true, DrawScrollView = true)]
        [GUIColor(1f, 1f, 0.9f)]
        [Tooltip("Each window entry defines the ID and prefab used in the UI system.")]
        public List<WindowConfig> Configs = new();
    }
}