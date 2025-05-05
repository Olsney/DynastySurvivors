using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.StaticData.Level
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/LevelData")]
    public class LevelStaticData : ScriptableObject
    {
        [FoldoutGroup("⚙️ General Settings", expanded: true)]
        [LabelText("Level Key")]
        [GUIColor(0.4f, 0.7f, 1f)]
        [Required, PropertyOrder(-10)]
        public string LevelKey;

        [FoldoutGroup("🧟 Enemy Spawners", expanded: true)]
        [LabelText("Enemy Spawner Points")]
        [TableList(AlwaysExpanded = true)]
        [GUIColor(1f, 0.9f, 0.6f)]
        public List<EnemySpawnerData> EnemySpawners = new();

        [FoldoutGroup("⏱ Spawn Timing", expanded: true)]
        [HorizontalGroup("⏱ Spawn Timing/Range")]
        [LabelText("Min Spawn Interval")]
        [SuffixLabel("seconds", true)]
        [MinValue(0.1f), MaxValue(300f)]
        [GUIColor(0.6f, 1f, 0.6f)]
        public float MinSpawnInterval = 10f;

        [HorizontalGroup("⏱ Spawn Timing/Range")]
        [LabelText("Max Spawn Interval")]
        [SuffixLabel("seconds", true)]
        [MinValue(0.1f), MaxValue(300f)]
        [GUIColor(1f, 0.6f, 0.6f)]
        public float MaxSpawnInterval = 60f;

        [FoldoutGroup("⏱ Spawn Timing")]
        [Button(ButtonSizes.Large)]
        [GUIColor(0.2f, 0.6f, 1f)]
        private void ResetToDefaults()
        {
            MinSpawnInterval = 10f;
            MaxSpawnInterval = 60f;
        }
    }
}