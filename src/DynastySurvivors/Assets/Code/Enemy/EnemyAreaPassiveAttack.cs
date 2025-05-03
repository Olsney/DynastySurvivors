using Code.Logic;
using Code.Services.Times;
using UnityEngine;

namespace Code.Enemy
{
    public class EnemyAreaPassiveAttack : EnemyAttack
    {
        [SerializeField] private TriggerObserver _attackZone;
        private float _attackDamage;
        private float _attackCooldown;
        
        private ICooldownService _cooldown;

        private IDamageable _target;
        private bool _isAttackEnabled;

        public void Initialize(float attackDamage, float attackCooldown)
        {
            _attackDamage = attackDamage;
            _attackCooldown = attackCooldown;

            _cooldown = new CooldownService(_attackCooldown);
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
            UpdateCooldown();
            
            if (CanAttack()) 
                Attack();
        }

        private void UpdateCooldown() => 
            _cooldown.Tick(Time.deltaTime);

        private void Attack()
        {
            _target.TakeDamage(_attackDamage);
            _cooldown.PutOnCooldown();
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