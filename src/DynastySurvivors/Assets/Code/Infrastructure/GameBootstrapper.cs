using Code.Infrastructure.Factory;
using Code.Infrastructure.States;
using Code.Logic.Curtain;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private GameStateMachine _gameStateMachine;
        private IGameFactory _gameFactory;
        private LoadingCurtainProvider _loadingCurtainProvider;
        
        [Inject]
        private void Construct(GameStateMachine gameStateMachine, IGameFactory gameFactory, LoadingCurtainProvider loadingCurtainProvider)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
            _loadingCurtainProvider = loadingCurtainProvider;
        }
        
        private void Start()
        {
            CreateCurtain();
            _gameStateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }

        private void CreateCurtain()
        {
            GameObject curtain = _gameFactory.CreateCurtain();
            
            _loadingCurtainProvider.Instance = curtain.GetComponent<LoadingCurtain>();
        }
    }
}