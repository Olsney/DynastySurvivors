using System;
using Code.Data;
using Code.Services.PersistentProgress;
using UnityEngine;

namespace Code.Hero
{
    public class HeroHealth : MonoBehaviour, ISavedProgress
    {
        [SerializeField] 
        private HeroAnimator _animator;
        
        [SerializeField]
        private GameObject _takeDamageEffectPrefab;
        private State _state;

        public event Action HealthChanged;

        public float Current
        {
            get => _state.CurrentHp;
            set
            {
                if (_state.CurrentHp != value)
                {
                    _state.CurrentHp = value;

                    HealthChanged?.Invoke();
                }
            }
        }

        public float Max
        {
            get => _state.MaxHp;
            set => _state.MaxHp = value;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _state = progress.HeroState;
            
            HealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HeroState.CurrentHp = Current;
            progress.HeroState.MaxHp = Max;
        }

        public void TakeDamage(float damage)
        {
            float effectVisualDiration = 3f;
            
            if (Current <= 0)
                return;

            Current -= damage;
            _animator.PlayHit();
            
            GameObject damageEffect = Instantiate(_takeDamageEffectPrefab, transform.position, Quaternion.identity);
            Destroy(damageEffect, effectVisualDiration);
        }
    }
}