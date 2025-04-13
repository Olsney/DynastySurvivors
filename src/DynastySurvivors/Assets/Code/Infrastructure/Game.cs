using Code.Logic;
using Code.Services.Input;

namespace Code.Infrastructure
{
    public class Game
    {
        public static IInputService InputService;
        
        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain);
        }
    }
}