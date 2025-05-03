using System;

namespace Code.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public HealthData HeroHealth;
        public HeroStats HeroStats;
        public WorldData WorldData;

        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
            HeroHealth = new HealthData();
            HeroStats = new HeroStats();
        }
    }
}