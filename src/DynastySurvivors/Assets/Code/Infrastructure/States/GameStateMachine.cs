using System;
using System.Collections.Generic;
using Code.Infrastructure.Factory;
using Code.Infrastructure.Services;
using Code.Logic;
using Code.Logic.Curtain;

namespace Code.Infrastructure.States
{
    public class GameStateMachine
    {
        // private readonly Dictionary<Type, IExitableState> _states;
        private readonly IStateFactory _stateFactory;
        private IExitableState _activeState;

        // public GameStateMachine(SceneLoader sceneLoad, LoadingCurtain curtain, AllServices services)
        // {
        //     _states = new Dictionary<Type, IExitableState>()
        //     {
        //         [typeof(BootstrapState)] = new BootstrapState(this, sceneLoad, services),
        //         [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoad, curtain, services.Single<IGameFactory>()),
        //         [typeof(GameLoopState)] = new GameLoopState(this),
        //     };
        // }

        public GameStateMachine(IStateFactory stateFactory)
        {
            _stateFactory = stateFactory;
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();

            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        // private TState GetState<TState>() where TState : class, IExitableState => 
        //     _states[typeof(TState)] as TState;

        private TState GetState<TState>() where TState : class, IExitableState =>
            _stateFactory.GetState<TState>();
    }
}