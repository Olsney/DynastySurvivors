using Code.Logic;
using Code.Services.Times;
using UnityEngine;

namespace Code.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyMeleeAttack : EnemyAttack
    {
        private const string HeroLayerMask = "Hero";

        [SerializeField] private EnemyAnimator _enemyAnimator;
        
        private float _attackDamage;
        private float _attackCooldown;
        private float _attackOffsetY;
        private float _attackOffsetForward;
        private float _attackCleavage;

        private Transform _heroTransform;
        private bool _isAttacking;
        private int _heroLayerMask;
        private Collider[] _hitsBuffer = new Collider[1];
        private bool _isAttackEnabled;
        private ICooldownService _cooldown;
        
        public void Construct(Transform heroTransform)
        {
            _heroTransform = heroTransform;
        }

        public void Initialize(float attackDamage, float attackCooldown, float attackOffsetY, float attackOffsetForward, float attackCleavage)
        {
            _attackDamage = attackDamage;
            _attackCooldown = attackCooldown;
            _attackOffsetY = attackOffsetY;
            _attackOffsetForward = attackOffsetForward;
            _attackCleavage = attackCleavage;

            _cooldown = new CooldownService(_attackCooldown);
        }

        private void Awake()
        {
            _heroLayerMask = 1 << LayerMask.NameToLayer(HeroLayerMask);
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

        private void UpdateCooldown() => 
            _cooldown.Tick(Time.deltaTime);

        private bool CanAttack() =>
            _isAttackEnabled && !_isAttacking && _cooldown.IsReady;

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
            if (IsHitted(out Collider hit) == false)
                return;

            IDamageable heroHealth = hit.transform.GetComponent<IDamageable>();

            if (hit == null)
                return;

            if (heroHealth == null)
                return;

            heroHealth.TakeDamage(_attackDamage);
        }

        private bool IsHitted(out Collider hit)
        {
            int hitsCount = Physics.OverlapSphereNonAlloc(GetAttackStartPosition(), _attackCleavage, _hitsBuffer,
                _heroLayerMask);
            hit = null;

            if (hitsCount <= 0)
                return false;
            
            for (int i = 0; i < hitsCount; i++)
            {
                if (_hitsBuffer[i] != null)
                {
                    hit = _hitsBuffer[i];

                    return true;
                }
            }

            return false;
        }

        private Vector3 GetAttackStartPosition()
        {
            Vector3 directionToHero = (_heroTransform.position - transform.position).normalized;
            float distanceToHero = Vector3.Distance(transform.position, _heroTransform.position);

            // Выбираем минимальное: либо заданное смещение, либо реальное расстояние до героя минус небольшая дельта
            float forwardOffset =
                Mathf.Min(_attackOffsetForward, distanceToHero - 0.1f); // 0.1f — чтобы не попасть точно в точку врага

            // Если слишком близко (меньше 0.1f) — оставляем без смещения
            forwardOffset = Mathf.Max(forwardOffset, 0f);

            Vector3 attackPoint = transform.position + directionToHero * forwardOffset;
            attackPoint.y += _attackOffsetY;

            return attackPoint;
        }
    }
}