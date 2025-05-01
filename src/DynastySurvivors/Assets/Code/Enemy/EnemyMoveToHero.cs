using Code.Data;
using Code.Infrastructure.Factory;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Random = UnityEngine.Random;

namespace Code.Enemy
{
    public class EnemyMoveToHero : Follower
    {
        private const float MinDistanceToHero = 0.5f;
        
        [SerializeField]
        private NavMeshAgent _agent;
        
        private Transform _heroTransform;
        private IGameFactory _gameFactory;

        public void Construct(Transform heroTransform) => 
            _heroTransform = heroTransform;

        private void Start()
        {
            _agent.avoidancePriority = Random.Range(30, 60);
        }

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