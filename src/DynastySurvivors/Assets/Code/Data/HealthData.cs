using System;
using UnityEngine.Serialization;

namespace Code.Data
{
    [Serializable]
    public class HealthData
    {
        public float CurrentHealth;
        public float MaxHealth;

        public void ResetHp() => CurrentHealth = MaxHealth;
    }
}