using Code.CameraLogic;
using Code.Hero;
using Code.Infrastructure.Factory;
using Code.Logic;
using Code.Logic.Curtain;
using Code.Services.PersistentProgress;
using Code.UI;
using UnityEngine;

namespace Code.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPoint = "InitialPoint";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtainProvider _loadingCurtain;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(
            GameStateMachine stateMachine,
            SceneLoader sceneLoader,
            IGameFactory gameFactory,
            LoadingCurtainProvider loadingCurtain,
            IPersistentProgressService persistentProgressService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            // _curtain = curtain;
            _gameFactory = gameFactory;
            _loadingCurtain = loadingCurtain;
            _persistentProgressService = persistentProgressService;
        }

        public void Enter(string sceneName)
        {
            // _curtain.Show();
            _loadingCurtain.Instance.Show();
            _gameFactory.Cleanup();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            // _curtain.Hide();
            _loadingCurtain.Instance.Hide();
        }

        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReaders();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InitGameWorld()
        {
            _gameFactory.CreateSaveTriggerContainer();
            
            GameObject hero = _gameFactory.CreateHero(at: GameObject.FindWithTag(InitialPoint));
            
            InitHud(hero);
            CameraFollow(hero);
        }

        private void InitHud(GameObject hero)
        {
            GameObject hud = _gameFactory.CreateHud();
            
            hud.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<IHealth>());
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders) 
                progressReader.LoadProgress(_persistentProgressService.Progress);
        }

        private void CameraFollow(GameObject hero) => 
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
    }
}