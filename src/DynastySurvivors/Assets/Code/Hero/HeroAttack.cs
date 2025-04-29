using System;
using Code.Data;
using Code.Logic;
using Code.Services.Input;
using Code.Services.PersistentProgress;
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

        private static int _hittableLayerMask;

        private float _attackRadius;
        private float _attackDamage;
        private Collider[] _hitsBuffer = new Collider[8];

        private IInputService _inputService;
        private HeroStats _stats;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Awake()
        {
            _hittableLayerMask = 1 << LayerMask.NameToLayer(HittableLayerMask);
        }

        private void Update()
        {
            if (_inputService.IsAttackButtonUp() && !_heroAnimator.IsAttacking)
                _heroAnimator.PlayAttack();
        }

        public void LoadProgress(PlayerProgress progress) =>
            _stats = progress.HeroStats;

        // public void OnAttack()
        // {
        //     Debug.Log($"Radius: {_stats.DamageRadius}");
        //     Debug.Log("Попал в ОнАттак");
        //
        //     int hitCount = Hit();
        //     Debug.Log($"HitCount: {hitCount}");
        //
        //     PhysicsDebugHelpers.DrawRaysFromPoint(GetStartPoint(), _attackRadius, Color.yellow, 1f);
        //
        //     for (int i = 0; i < Hit(); i++)
        //     {
        //         Debug.Log($"{_stats.Damage} - урон нанесся по врагу");
        //         PhysicsDebugHelpers.DrawRaysFromPoint(GetStartPoint(), _attackRadius, Color.red, 1f);
        //         // _hitsBuffer[i].transform.parent.GetComponent<IDamageable>().TakeDamage(_stats.Damage);
        //         _hitsBuffer[i].GetComponentInParent<IDamageable>()?.TakeDamage(_stats.Damage);
        //     }
        // }
        
        public void OnAttack()
        {
            Debug.Log($"Radius: {_stats.DamageRadius}");
            Debug.Log("Попал в ОнАттак");

            Vector3 attackCenter = GetStartPoint();
            int hitCount = Physics.OverlapSphereNonAlloc(
                attackCenter,
                _stats.DamageRadius,
                _hitsBuffer,
                _hittableLayerMask);

            Debug.Log($"HitCount (до фильтрации по углу): {hitCount}");

            for (int i = 0; i < hitCount; i++)
            {
                Collider hit = _hitsBuffer[i];

                if (hit == null)
                    continue;

                // Определяем направление на цель
                Vector3 toTarget = (hit.transform.position - transform.position).normalized;

                // Сравниваем с направлением взгляда героя
                float dot = Vector3.Dot(transform.forward, toTarget);

                // Фильтр: только враги в секторе ~120 градусов (dot >= 0.5)
                if (dot >= 0.5f)
                {
                    IDamageable damageable = hit.GetComponentInParent<IDamageable>();

                    if (damageable != null)
                    {
                        damageable.TakeDamage(_stats.Damage);
                        Debug.Log($"{_stats.Damage} - урон нанесён по {hit.name}");
                    }
                }
                else
                {
                    Debug.Log($"{hit.name} — за пределами фронтального сектора (dot = {dot})");
                }
            }

            PhysicsDebugHelpers.DrawRaysFromPoint(attackCenter, _stats.DamageRadius, Color.red, 1f);
        }

        private int Hit() =>
            Physics.OverlapSphereNonAlloc(GetStartPoint() + transform.forward, _stats.DamageRadius, _hitsBuffer,
                _hittableLayerMask);

        private Vector3 GetStartPoint()
        {
            // return new Vector3(transform.position.x, _characterController.center.y / 2f, transform.position.z) +
            //        transform.forward;
            
            Vector3 center = _characterController.bounds.center;
            return center + transform.forward * 0.5f; // половина метра вперёд от центра героя
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying || _stats == null)
                return;
        
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(GetStartPoint() + transform.forward, _stats.DamageRadius);
        }
    }
}