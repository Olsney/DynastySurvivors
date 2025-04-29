using System;
using Code.Logic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth, IDamageable
    {
        public event Action HealthChanged;

        [SerializeField]
        private EnemyAnimator _enemyAnimator;
        [SerializeField]
        private float _current;
        [SerializeField]
        private float _max;

        public float Current => _current;
        public float Max => _max;


        public void TakeDamage(float damage)
        {
            if (damage < 0)
                damage = 0;
            
            _current -= damage;
            _enemyAnimator.PlayHit();
            
            HealthChanged?.Invoke();
        }
    }
}