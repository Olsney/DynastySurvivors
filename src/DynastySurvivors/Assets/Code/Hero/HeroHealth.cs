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

        public void Initialize(float current, float max)
        {
            _max = max;
            _current = current;
            
            Changed?.Invoke();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.HeroHealth.IsInitialized == false)
                return;
            
            _healthData = progress.HeroHealth;
            
            _current = _healthData.CurrentHealth;
            _max = _healthData.MaxHealth;
            
            Changed?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HeroHealth.IsInitialized = true;
            
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