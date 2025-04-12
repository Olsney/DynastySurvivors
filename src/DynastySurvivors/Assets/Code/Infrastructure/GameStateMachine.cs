using System;
using System.Collections.Generic;

namespace Code.Infrastructure
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type,IState> _states;
        private IState _activeState;

        public GameStateMachine(SceneLoader sceneLoad)
        {
            _states = new Dictionary<Type, IState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoad),
            };
        }
        
        public void Enter<TState>() where TState : IState
        {
            _activeState?.Exit();
            
            IState state = _states[typeof(TState)];
            _activeState = state;
            state.Enter();
        }
    }
}