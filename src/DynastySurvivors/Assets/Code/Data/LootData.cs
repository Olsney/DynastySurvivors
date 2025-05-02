using System;

namespace Code.Data
{
    [Serializable]
    public class LootData
    {
        public int Collected;

        public void Collect(Loot loot)
        {
            Collected += loot.Value;
        }
    }
}