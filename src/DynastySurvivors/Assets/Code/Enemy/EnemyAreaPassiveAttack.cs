using Code.Infrastructure.Factory;
using Code.Logic;
using UnityEngine;
using Zenject;

namespace Code.Enemy
{
    [RequireComponent(typeof(Cooldown))]
    public class EnemyAreaPassiveAttack : EnemyAttack
    {
        [SerializeField] private TriggerObserver _attackZone;
        [SerializeField] private float _attackDamage = 1f;
        [SerializeField] private float _attackCooldown = 0.5f;
        [SerializeField] private Cooldown _cooldown;

        private IDamageable _target;
        private bool _isAttackEnabled;

        public void Initialize(float attackDamage, float attackCooldown)
        {
            _attackDamage = attackDamage;
            _attackCooldown = attackCooldown;
        }

        private void Awake()
        {
            _attackZone.Entered += OnZoneEntered;
            _attackZone.Exited += OnZoneExited;
        }

        private void OnDestroy()
        {
            _attackZone.Entered -= OnZoneEntered;
            _attackZone.Exited -= OnZoneExited;
        }

        public override void EnableAttack() =>
            _isAttackEnabled = true;

        public override void DisableAttack()
        {
            _isAttackEnabled = false;
            _target = null;
        }

        private void Update()
        {
            if (CanAttack()) 
                Attack();
        }

        private void Attack()
        {
            _target.TakeDamage(_attackDamage);
            _cooldown.SetCooldown(_attackCooldown);
        }

        private void OnZoneEntered(Collider other)
        {
            if (_cooldown.IsOnCooldown())
                return;
            
            if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
                _target = damageable;
        }

        private void OnZoneExited(Collider other)
        {
            _isAttackEnabled = false;

            if (other.TryGetComponent<IDamageable>(out IDamageable damageable) && damageable == _target)
                _target = null;
        }

        private bool CanAttack() => 
            _isAttackEnabled && _target != null && !_cooldown.IsOnCooldown();
    }
}