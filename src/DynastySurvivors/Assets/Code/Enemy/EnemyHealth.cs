using System;
using Code.Logic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth, IDamageable
    {
        public event Action Changed;

        [SerializeField]
        private EnemyAnimator _enemyAnimator;
        [SerializeField]
        private float _current;
        [SerializeField]
        private float _max;
        
        [SerializeField]
        private GameObject _takeDamageEffectPrefab;

        public float Current => _current;
        public float Max => _max;


        public void TakeDamage(float damage)
        {
            float effectVisualDiration = 3f;
            
            if (damage < 0)
                damage = 0;
            
            _current -= damage;
            _enemyAnimator.PlayHit();
            
            Changed?.Invoke();
            
            GameObject damageEffect = Instantiate(_takeDamageEffectPrefab, transform.position, Quaternion.identity);
            Destroy(damageEffect, effectVisualDiration);
        }
    }
}