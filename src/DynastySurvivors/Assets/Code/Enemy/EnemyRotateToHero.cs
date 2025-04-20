using System;
using Code.Infrastructure.Factory;
using UnityEngine;
using Zenject;

namespace Code.Enemy
{
    public class EnemyRotateToHero : Follower
    {
        [SerializeField]
        private float _rotationSpeed;
        
        private Transform _heroTransform;
        private IGameFactory _gameFactory;
        private Vector3 _positionToLook;

        [Inject]
        private void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }
        
        private void Start()
        {
            if (_gameFactory.HeroGameObject != null)
                IsHeroInitialized();
            else
                _gameFactory.HeroCreated += InitializeHero;
        }
        
        private void Update()
        {
            if (IsHeroInitialized())
                RotateTowardsHero();
        }
        
        private void InitializeHero() =>
            _heroTransform = _gameFactory.HeroGameObject.transform;

        private bool IsHeroInitialized() =>
            _heroTransform != null;

        private void RotateTowardsHero() =>
            transform.rotation = GetSmoothedRotation(transform.rotation, GetPositionToLookAt());

        private Quaternion GetSmoothedRotation(Quaternion rotation, Vector3 positionToLookAt) =>
            Quaternion.Lerp(rotation, GetTargetRotation(positionToLookAt), SpeedFactor());

        private Vector3 GetPositionToLookAt()
        {
            Vector3 positionDiff = _heroTransform.position - transform.position;
            
            return new Vector3(positionDiff.x , transform.position.y, positionDiff.z);
        }

        private Quaternion GetTargetRotation(Vector3 position) =>
            Quaternion.LookRotation(position);

        private float SpeedFactor() =>
            _rotationSpeed * Time.deltaTime;
    }
}