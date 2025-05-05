using Code.Services.StaticData;
using Code.Services.StaticData.Enemy;

namespace Code.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IStaticDataService _staticData;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, IStaticDataService staticData)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _staticData = staticData;
        }

        public void Enter()
        {
            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
            _staticData.LoadAllEnemies();
            _staticData.LoadAllHeroes();
            _staticData.LoadAllLevels();
            _staticData.LoadAllWindows();
        }

        private void EnterLoadLevel() => 
            _stateMachine.Enter<LoadProgressState>();

        public void Exit()
        {
        }
    }
}