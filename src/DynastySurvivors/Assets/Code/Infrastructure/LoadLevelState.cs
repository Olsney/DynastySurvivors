using Code.CameraLogic;
using Code.Logic;
using UnityEngine;

namespace Code.Infrastructure
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string HeroPath = "Hero/Chr_Hero_Female_01";
        private const string HudPath = "Hud/Hud";
        private const string InitialPoint = "InitialPoint";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() => 
            _curtain.Hide();

        private void OnLoaded()
        {

            GameObject initialPoint = GameObject.FindWithTag(InitialPoint);
            GameObject hero = Instantiate(HeroPath, at: initialPoint.transform.position);
            Instantiate(HudPath);
            
            CameraFollow(hero);
            
            _stateMachine.Enter<GameLoopState>();
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
        
        private static GameObject Instantiate(string path, Vector3 at)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }
    }
}