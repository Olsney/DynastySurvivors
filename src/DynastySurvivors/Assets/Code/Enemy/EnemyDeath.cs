using System;
using System.Collections;
using UnityEngine;

namespace Code.Enemy
{
    [RequireComponent(typeof(EnemyHealth), typeof(EnemyAnimator))]
    public class EnemyDeath : MonoBehaviour
    {
        public event Action Died;
        
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private GameObject _deathFx;
        
        private void Start() => 
            _health.Changed += OnChanged;

        private void OnDestroy() => 
            _health.Changed -= OnChanged;

        private void OnChanged()
        {
            if (_health.Current <= 0) 
                Die();
        }

        private void Die()
        {
            _health.Changed -= OnChanged;

            _animator.PlayDeath();

            SpawnDeathFx();
            StartCoroutine(DestroyAfterDelay());

            Died?.Invoke();
        }

        private void SpawnDeathFx() => 
            Instantiate(_deathFx, transform.position, Quaternion.identity);

        private IEnumerator DestroyAfterDelay()
        {
            float delayBeforeDestroy = 3f;
            
            yield return new WaitForSeconds(delayBeforeDestroy);
            
            Destroy(gameObject);
        }
    }
}