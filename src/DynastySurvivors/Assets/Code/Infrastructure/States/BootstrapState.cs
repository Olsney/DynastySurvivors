using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Factory;
using Code.Infrastructure.Services;
using Code.Services.Input;
using UnityEngine;

namespace Code.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
        }

        private void EnterLoadLevel() => 
            _stateMachine.Enter<LoadProgressState>();

        public void Exit()
        {
        }
    }
}