using System;
using System.Collections;
using UnityEngine;

namespace Code.Enemy
{
    [RequireComponent(typeof(EnemyHealth), typeof(EnemyAnimator), typeof(EnemyMoveToHero))]
    public class EnemyDeath : MonoBehaviour
    {
        public event Action Died;
        
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private GameObject _deathFx;
        [SerializeField] private EnemyMoveToHero _enemyMove;
        [SerializeField] private EnemyAttack _enemyAttack;
        
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
            _enemyMove.enabled = false;
            _enemyAttack.enabled = false;
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
            WaitForSeconds wait = new WaitForSeconds(delayBeforeDestroy);
            
            yield return wait;
            
            Destroy(gameObject);
        }
    }
}