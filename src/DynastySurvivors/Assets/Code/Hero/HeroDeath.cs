using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Hero
{
    public class HeroDeath : MonoBehaviour
    {
        [SerializeField]
        private HeroHealth _health;
        [SerializeField]
        private HeroMove _move;
        [SerializeField]
        private HeroAttack _attack;
        [SerializeField]
        private HeroAnimator _animator;
        [SerializeField]
        private GameObject _deathEffectPrefab;
        [SerializeField]
        private Transform _deathEffectSpawnPoint;

        private bool _isDead;

        private void Start() => 
            _health.HealthChanged += OnHealthChanged;

        private void OnDestroy() => 
            _health.HealthChanged -= OnHealthChanged;

        private void OnHealthChanged()
        {
            if (!_isDead && _health.Current <= 0)
                Die();
        }

        private void Die()
        {
            float effectVisualDiration = 3f;
            
            _move.enabled = false;
            _attack.enabled = false;
            _animator.PlayDeath();
            GameObject deathEffect = Instantiate(_deathEffectPrefab, _deathEffectSpawnPoint.position, Quaternion.identity);
            
            _isDead = true;
            
            Destroy(deathEffect, effectVisualDiration);
        }
    }
}