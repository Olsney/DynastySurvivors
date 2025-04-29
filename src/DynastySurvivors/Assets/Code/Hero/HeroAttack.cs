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

        [SerializeField]
        private HeroAnimator _heroAnimator;
        [SerializeField]
        private CharacterController _characterController;

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
            if(_inputService.IsAttackButtonUp() && !_heroAnimator.IsAttacking)
                _heroAnimator.PlayAttack();
        }

        public void OnAttack()
        {
            for (int i = 0; i < Hit(); i++)
            {
                _hitsBuffer[i].transform.parent.GetComponent<IDamageable>().TakeDamage(_stats.Damage);
            }
        }

        private int Hit() => 
            Physics.OverlapSphereNonAlloc(GetStartPoint() + transform.forward, _stats.DamageRadius, _hitsBuffer, _hittableLayerMask);

        private Vector3 GetStartPoint() =>
            new Vector3(transform.position.x, _characterController.center.y / 2f, transform.position.z) + transform.forward;

        public void LoadProgress(PlayerProgress progress) => 
            _stats = progress.HeroStats;
    }
}