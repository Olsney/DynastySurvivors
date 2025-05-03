using Code.Data;
using Code.Infrastructure.Factory;
using Code.Services.Random;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Enemy
{
    public class EnemyMoveToHero : Follower
    {
        private const float MinDistanceToHero = 0.5f;
        private const int MinAvoidancePriority = 30;
        private const int MaxAvoidancePriority = 60;
        
        [SerializeField]
        private NavMeshAgent _agent;

        private Transform _heroTransform;
        private IGameFactory _gameFactory;
        private IRandomService _random;

        public void Construct(Transform heroTransform, IRandomService random)
        {
            _heroTransform = heroTransform;
            _random = random;
        }

        private void Start() => 
            _agent.avoidancePriority = _random.Next(MinAvoidancePriority, MaxAvoidancePriority);

        private void Update()
        {
            if (IsInitialized() && IsEnemyFarFromHero())
                _agent.destination = _heroTransform.position;
        }
        
        private bool IsInitialized() => 
            _heroTransform != null;
        
        private bool IsEnemyFarFromHero() =>
           _agent.transform.position.SqrDistance(_heroTransform.position) > MinDistanceToHero;
    }
}