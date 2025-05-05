using System;
using Code.Logic;
using UnityEngine;

namespace Code.Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth, IDamageable
    {
        private const float EffectDestroyDelay = 3f;
        public event Action Changed;

        [SerializeField]
        private EnemyAnimator _enemyAnimator;
        private float _current;
        private float _max;
        
        [SerializeField]
        private GameObject _takeDamageEffectPrefab;
        [SerializeField] 
        private Transform _takeDamageEffectSpawnPoint;


        public float Current => _current;
        public float Max => _max;
        
        public void Initialize(float current, float max)
        {
            _max = max;
            _current = current;
            Changed?.Invoke();
        }

        public void TakeDamage(float damage)
        {
            if (damage < 0)
                damage = 0;
            
            _current -= damage;
            _enemyAnimator.PlayHit();
            
            Changed?.Invoke();
            
            PlayDamageEffect();
        }

        private void PlayDamageEffect()
        {
            GameObject damageEffect = Instantiate(_takeDamageEffectPrefab, _takeDamageEffectSpawnPoint.position, Quaternion.identity);
            
            Destroy(damageEffect, EffectDestroyDelay);
        }
    }
}