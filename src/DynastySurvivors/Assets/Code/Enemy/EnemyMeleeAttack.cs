using System.Linq;
using Code.Hero;
using Code.Infrastructure.Factory;
using Code.Logic;
using UnityEngine;
using Zenject;

namespace Code.Enemy
{
    public class EnemyMeleeAttack : EnemyAttack
    {
        private const string HeroLayerMask = "Hero";

        [SerializeField] private Cooldown _cooldown;
        [SerializeField] private EnemyAnimator _enemyAnimator;
        [SerializeField] private float _attackDamage = 50f;
        [SerializeField] private float _attackCooldown = 3f;
        [SerializeField] private float _attackOffsetY = 0.5f;
        [SerializeField] private float _attackOffsetForward = 0.5f;
        [SerializeField] private float _attackCleavage = 0.5f;

        private IGameFactory _gameFactory;
        private Transform _heroTransform;
        private float _attackCooldownTimer;
        private bool _isAttacking;
        private int _heroLayerMask;
        private Collider[] _hitsBuffer = new Collider[1];
        private bool _isAttackEnabled;

        [Inject]
        private void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        private void Awake()
        {
            _heroLayerMask = 1 << LayerMask.NameToLayer(HeroLayerMask);

            GameObject heroGameObject = _gameFactory.HeroGameObject;

            if (heroGameObject != null)
                _heroTransform = heroGameObject.transform;
            else
                _gameFactory.HeroCreated += OnHeroCreated;
        }

        private void Update()
        {
            UpdateCooldown();

            if (CanAttack())
                StartAttack();
        }

        public override void EnableAttack() => 
            _isAttackEnabled = true;

        public override void DisableAttack() => 
            _isAttackEnabled = false;

        private void UpdateCooldown()
        {
            if (IsAttackOnCooldown())
                _attackCooldownTimer -= Time.deltaTime;
        }

        private bool CanAttack() =>
            _isAttackEnabled && !_isAttacking && !_cooldown.IsOnCooldown();

        private void StartAttack()
        {
            transform.LookAt(_heroTransform);
            _enemyAnimator.PlayAttack();

            _isAttacking = true;
        }

        private void OnAttackEnded()
        {
            _cooldown.SetCooldown(_attackCooldown);
            
            _isAttacking = false;
        }

        private void OnAttack()
        {
            if (IsHitted(out Collider hit))
            {
                PhysicsDebugHelpers.DrawRaysFromPoint(GetAttackStartPosition(), _attackCleavage, Color.red, 1f);
                HeroHealth heroHealth = hit.transform.GetComponent<HeroHealth>();

                if (hit == null)
                    return;
                
                if (heroHealth == null)
                    return;
                
                heroHealth.TakeDamage(_attackDamage);
            }
        }

        private bool IsAttackOnCooldown() =>
            _attackCooldownTimer > 0f;

        private void OnHeroCreated() =>
            _heroTransform = _gameFactory.HeroGameObject.transform;

        private bool IsHitted(out Collider hit)
        {
            int hitsCount = Physics.OverlapSphereNonAlloc(GetAttackStartPosition(), _attackCleavage, _hitsBuffer, _heroLayerMask);
            hit = null;

            // hit = _hitsBuffer.FirstOrDefault();
            //
            // return hitsCount > 0;
            
            if (hitsCount > 0)
            {
                for (int i = 0; i < hitsCount; i++)
                {
                    if (_hitsBuffer[i] != null)
                    {
                        hit = _hitsBuffer[i];
                        
                        return true;
                    }
                }
            }

            return false;
        }

        private Vector3 GetAttackStartPosition()
        {
            // return new Vector3(transform.position.x, transform.position.y + _attackOffsetY, transform.position.z) +
            //        _attackOffsetForward * transform.forward;
            
            // Vector3 directionToHero = (_heroTransform.position - transform.position).normalized;
            // Vector3 attackPoint = transform.position + directionToHero * _attackOffsetForward;
            // attackPoint.y += _attackOffsetY;
            //
            // return attackPoint;
            
            Vector3 directionToHero = (_heroTransform.position - transform.position).normalized;
            float distanceToHero = Vector3.Distance(transform.position, _heroTransform.position);

            // Выбираем минимальное: либо заданное смещение, либо реальное расстояние до героя минус небольшая дельта
            float forwardOffset = Mathf.Min(_attackOffsetForward, distanceToHero - 0.1f); // 0.1f — чтобы не попасть точно в точку врага

            // Если слишком близко (меньше 0.1f) — оставляем без смещения
            forwardOffset = Mathf.Max(forwardOffset, 0f);

            Vector3 attackPoint = transform.position + directionToHero * forwardOffset;
            attackPoint.y += _attackOffsetY;
    
            return attackPoint;
        }
    }
}