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
        [SerializeField] private float _attackCooldown = 1f;
        [SerializeField] private Cooldown _cooldown;

        private IGameFactory _gameFactory;
        private Transform _heroTransform;
        private IDamageable _target;
        private bool _isAttackEnabled;

        [Inject]
        private void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        private void Awake()
        {
            if (_gameFactory.HeroGameObject != null)
                _heroTransform = _gameFactory.HeroGameObject.transform;
            else
                _gameFactory.HeroCreated += OnHeroCreated;

            _attackZone.Entered += OnZoneEntered;
            _attackZone.Exited += OnZoneExited;
        }

        private void OnDestroy()
        {
            _gameFactory.HeroCreated -= OnHeroCreated;

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
            {
                Attack();
            }
        }

        private void Attack()
        {
            _target.TakeDamage(_attackDamage);
            _cooldown.SetCooldown(_attackCooldown);
        }

        private void OnZoneEntered(Collider other)
        {
            Debug.Log("OnZoneEntered in EnemyAreaPassiveAttack");

            if (_cooldown.IsOnCooldown())
                return;
            
            if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
                _target = damageable;
        }

        private void OnZoneExited(Collider other)
        {
            Debug.Log("Hero Exited in EnemyAreaPassiveAttack");

            _isAttackEnabled = false;

            if (other.TryGetComponent<IDamageable>(out IDamageable damageable) && damageable == _target)
                _target = null;
        }

        private bool CanAttack()
        {
            Debug.Log($"{_isAttackEnabled && _target != null && !_cooldown.IsOnCooldown()}");
            return _isAttackEnabled && _target != null && !_cooldown.IsOnCooldown();
        }

        private void OnHeroCreated()
        {
            _heroTransform = _gameFactory.HeroGameObject.transform;
        }
    }
}