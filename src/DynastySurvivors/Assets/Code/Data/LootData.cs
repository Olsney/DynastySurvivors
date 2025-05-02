using System;
using UnityEngine.Serialization;

namespace Code.Data
{
    [Serializable]
    public class LootData
    {
        public int LootValue;
        public Action Changed;

        public void Collect(Loot loot)
        {
            LootValue += loot.Value;
            Changed?.Invoke();
        }
    }
}