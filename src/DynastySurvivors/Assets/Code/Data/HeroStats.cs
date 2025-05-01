using System;

namespace Code.Data
{
    [Serializable]
    public class HeroStats
    {
        public float Damage;
        public float DamageRadius;
        public float AttackCooldown;
        public bool IsInitialized;
    }
}