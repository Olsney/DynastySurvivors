using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Enemy
{
    [RequireComponent(typeof(EnemyHealth), typeof(EnemyAnimator), typeof(EnemyMoveToHero))]
    public class EnemyDeath : MonoBehaviour
    {
        private const float DelayBeforeDestroy = 3f;
        public event Action Died;
        
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private GameObject _deathFx;
        [SerializeField] private EnemyMoveToHero _enemyMove;
        [SerializeField] private EnemyAttack _enemyAttack;
        [SerializeField] private NavMeshAgent _agent;
        
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
            _agent.enabled = false;
            _animator.PlayDeath();

            PlayDeathFx();
            StartCoroutine(DestroyAfterDelay(gameObject));

            Died?.Invoke();
        }

        private void PlayDeathFx()
        {
            GameObject deathEffect = Instantiate(_deathFx, transform.position, Quaternion.identity);
            
            Destroy(deathEffect, DelayBeforeDestroy);
        }

        private IEnumerator DestroyAfterDelay(GameObject obj)
        {
            WaitForSeconds wait = new WaitForSeconds(DelayBeforeDestroy);
            
            yield return wait;
            
            Destroy(obj);
        }
    }
}