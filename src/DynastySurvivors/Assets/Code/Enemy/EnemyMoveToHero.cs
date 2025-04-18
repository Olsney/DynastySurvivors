using Code.Infrastructure.Factory;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Code.Enemy
{
    public class EnemyMoveToHero : MonoBehaviour
    {
        private const float MinDistanceToHero = 0.5f;
        
        [SerializeField]
        private NavMeshAgent _agent;
        
        private Transform _heroTransform;
        private IGameFactory _gameFactory;

        [Inject]
        private void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;

            if (_gameFactory.HeroGameObject != null)
                InitializeHero();
            else
                _gameFactory.OnHeroCreated += InitializeHero;
        }
        
        private void Update()
        {
            if (IsHeroInitialized() && IsEnemyFarFromHero())
                _agent.destination = _heroTransform.position;
        }
        
        private bool IsEnemyFarFromHero() =>
            Vector3.Distance(_heroTransform.position, _agent.transform.position) > MinDistanceToHero;

        private bool IsHeroInitialized() =>
            _heroTransform != null;

        private void InitializeHero() =>
            _heroTransform = _gameFactory.HeroGameObject.transform;
    }
    
}