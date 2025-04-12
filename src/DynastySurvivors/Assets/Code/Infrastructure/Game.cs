using Code.Services.Input;

namespace Code.Infrastructure
{
    public class Game
    {
        public static IInputService InputService;
        
        public readonly GameStateMachine StateMachine;

        public Game()
        {
            StateMachine = new GameStateMachine();
        }
    }
}