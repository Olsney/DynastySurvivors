using TMPro;
using UnityEngine;

namespace Code.UI.Windows
{
    public class ShopWindow : WindowBase
    {
        [SerializeField] private TextMeshProUGUI _lootValue;
        
        protected override void Initialize() => 
            RefreshLootValue();

        protected override void SubscribeUpdates() => 
            Progress.WorldData.LootData.Changed += RefreshLootValue;

        protected override void Cleanup()
        {
            base.Cleanup();
            Progress.WorldData.LootData.Changed -= RefreshLootValue;
        }

        private void RefreshLootValue() => 
            _lootValue.text = Progress.WorldData.LootData.LootValue.ToString();
    }
}