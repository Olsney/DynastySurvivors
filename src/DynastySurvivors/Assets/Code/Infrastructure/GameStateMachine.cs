using System;
using System.Collections.Generic;

namespace Code.Infrastructure
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoad)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoad),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoad),
            };
        }
        
        public void Enter<TState>() where TState : class, IState
        {
            _activeState?.Exit();
            
            IState state = GetState<TState>();
            _activeState = state;
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            _activeState?.Exit();
            
            IPayloadedState<TPayload> state = GetState<TState>();
            _activeState = state;
            state.Enter(payload);
        }

        private TState GetState<TState>() where TState : class, IExitableState => 
            _states[typeof(TState)] as TState;
    }
}