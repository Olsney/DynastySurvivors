using Code.Data;
using Code.Logic;
using Code.Services.Input;
using Code.Services.PersistentProgress;
using Code.Services.Times;
using UnityEngine;
using Zenject;

namespace Code.Hero
{
    [RequireComponent(typeof(HeroAnimator), typeof(CharacterController))]
    public class HeroAttack : MonoBehaviour, ISavedProgressReader
    {
        private const string HittableLayerMask = "Hittable";

        [SerializeField] private HeroAnimator _heroAnimator;
        [SerializeField] private CharacterController _characterController;
        private ICooldownService _cooldown;

        private static int _hittableLayerMask;

        private Collider[] _hitsBuffer = new Collider[8];
        private IInputService _inputService;
        private HeroStats _stats;

        private float _damage;
        private float _damageRadius;
        private float _attackCooldown;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void Initialize(float damage, float damageRadius, float attackCooldown)
        {
            _damage = damage;
            _damageRadius = damageRadius;
            _attackCooldown = attackCooldown;

            _cooldown = new CooldownService(_attackCooldown);
        }

        private void Awake()
        {
            _hittableLayerMask = 1 << LayerMask.NameToLayer(HittableLayerMask);
        }

        private void Update()
        {
            UpdateCooldown();

            if (_inputService.IsAttackButtonUp() && !_heroAnimator.IsAttacking && _cooldown.IsReady)
            {
                _heroAnimator.PlayAttack();

                _cooldown.PutOnCooldown();
            }
        }

        private void UpdateCooldown() => 
            _cooldown.Tick(Time.deltaTime);

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.HeroStats.IsInitialized == false)
                return;

            _stats = progress.HeroStats;

            _damage = _stats.Damage;
            _damageRadius = _stats.DamageRadius;
            _attackCooldown = _stats.AttackCooldown;
        }

        public void OnAttack()
        {
            int hitCount = GetHitCount();

            for (int i = 0; i < hitCount; i++)
            {
                Collider hit = _hitsBuffer[i];

                if (hit == null)
                    continue;

                Vector3 toTarget = (hit.transform.position - transform.position).normalized;

                float dot = Vector3.Dot(transform.forward, toTarget);

                if (dot >= 0.5f)
                {
                    IDamageable damageable = hit.GetComponentInParent<IDamageable>();

                    if (damageable != null) 
                        damageable.TakeDamage(_damage);
                }
            }

            PhysicsDebugHelpers.DrawRaysFromPoint(GetStartPoint(), _damageRadius, Color.red, 1f);
        }

        private int GetHitCount() =>
            Physics.OverlapSphereNonAlloc(GetStartPoint(), _damageRadius, _hitsBuffer, _hittableLayerMask);

        private Vector3 GetStartPoint()
        {
            Vector3 center = _characterController.bounds.center;
            return center + transform.forward * 0.5f;
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying || _stats == null)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(GetStartPoint() + transform.forward, _damageRadius);
        }
    }
}