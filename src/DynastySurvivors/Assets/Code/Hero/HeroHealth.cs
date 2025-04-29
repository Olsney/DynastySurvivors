using System;
using Code.Data;
using Code.Logic;
using Code.Services.PersistentProgress;
using UnityEngine;

namespace Code.Hero
{
    public class HeroHealth : MonoBehaviour, ISavedProgress, IHealth, IDamageable
    {
        [SerializeField] 
        private HeroAnimator _animator;
        
        [SerializeField]
        private GameObject _takeDamageEffectPrefab;
        private HealthData _healthData;
        
        private float _current;
        private float _max;

        public float Current => _current;
        public float Max => _max;

        public event Action Changed;
        
        

        // public float Current
        // {
        //     get => _healthData.CurrentHealth;
        //     set
        //     {
        //         if (_healthData.CurrentHealth != value)
        //         {
        //             _healthData.CurrentHealth = value;
        //
        //             HealthChanged?.Invoke();
        //         }
        //     }
        // }
        //
        // public float Max
        // {
        //     get => _healthData.MaxHealth;
        //     set => _healthData.MaxHealth = value;
        // }

        public void LoadProgress(PlayerProgress progress)
        {
            _healthData = progress.HeroHealth;
            
            Changed?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            // progress.HeroHealth.CurrentHealth = Current;
            // progress.HeroHealth.MaxHealth = Max;
            progress.HeroHealth.CurrentHealth = _current;
            progress.HeroHealth.MaxHealth = _max;
        }

        public void TakeDamage(float damage)
        {
            float effectVisualDiration = 3f;
            
            if (_current <= 0)
                return;

            _current -= damage;
            _animator.PlayHit();
            Changed?.Invoke();
            
            GameObject damageEffect = Instantiate(_takeDamageEffectPrefab, transform.position, Quaternion.identity);
            Destroy(damageEffect, effectVisualDiration);
        }
    }
}