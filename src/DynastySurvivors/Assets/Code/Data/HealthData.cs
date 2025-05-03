using System;

namespace Code.Data
{
    [Serializable]
    public class HealthData
    {
        public float CurrentHealth;
        public float MaxHealth;
        public bool IsInitialized;

        public void ResetHp() => CurrentHealth = MaxHealth;
    }
}