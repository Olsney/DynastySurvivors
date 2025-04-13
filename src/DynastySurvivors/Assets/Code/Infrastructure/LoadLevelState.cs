using Code.CameraLogic;
using UnityEngine;

namespace Code.Infrastructure
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string HeroPath = "Hero/Chr_Hero_Female_01";
        private const string Hud = "Hud/Hud";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string sceneName) => 
            _sceneLoader.Load(sceneName, OnLoaded);

        public void Exit()
        {
        }

        private void OnLoaded()
        {
            GameObject hero = Instantiate(HeroPath);
            Instantiate(Hud);
            
            CameraFollow(hero);
        }

        private void CameraFollow(GameObject hero) => 
            Camera.main
                .GetComponent<CameraFollow>()
                .Follow(hero);

        private static GameObject Instantiate(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }
    }
}