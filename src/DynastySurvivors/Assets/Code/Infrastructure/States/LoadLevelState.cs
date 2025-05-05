using Code.CameraLogic;
using Code.Infrastructure.Factory;
using Code.Logic;
using Code.Logic.Curtain;
using Code.Services.PersistentProgress;
using Code.Services.StaticData;
using Code.StaticData.Hero;
using Code.StaticData.Level;
using Code.UI.Elements;
using Code.UI.Services.Factory;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtainProvider _loadingCurtainProvider;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IStaticDataService _staticDataService;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;

        public LoadLevelState(
            GameStateMachine stateMachine,
            SceneLoader sceneLoader,
            IGameFactory gameFactory,
            LoadingCurtainProvider loadingCurtainProvider,
            IPersistentProgressService persistentProgressService,
            IStaticDataService staticDataService, IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _loadingCurtainProvider = loadingCurtainProvider;
            _persistentProgressService = persistentProgressService;
            _staticDataService = staticDataService;
            _uiFactory = uiFactory;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtainProvider.Instance.Show();
            _gameFactory.Cleanup();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadingCurtainProvider.Instance.Hide();
        }

        private void OnLoaded()
        {
            InitUIRoot();
            InitGameWorld();
            InformProgressReaders();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InitGameWorld()
        {
            InitSpawners();
            
            _gameFactory.CreateSaveTriggerContainer();
            
            GameObject hero = _gameFactory.CreateHero(HeroTypeId.Woman, at: GetHeroSpawnPosition());
            
            InitHud(hero);
            CameraFollow(hero);
        }

        private void InitUIRoot() => 
            _uiFactory.CreateUIRoot();

        private Vector3 GetHeroSpawnPosition() => 
            GameObject.FindWithTag(InitialPointTag).transform.position;

        private void InitSpawners()
        {
            string sceneKey = SceneManager.GetActiveScene().name;
            LevelStaticData levelData = _staticDataService.GetLevel(sceneKey);
            
            foreach (EnemySpawnerData spawnerData in levelData.EnemySpawners)
            {
                _gameFactory.CreateEnemySpawner(
                    spawnerData.Position, 
                    spawnerData.Id, 
                    spawnerData.EnemyTypeId, 
                    levelData.MinSpawnInterval, 
                    levelData.MaxSpawnInterval);
            }
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