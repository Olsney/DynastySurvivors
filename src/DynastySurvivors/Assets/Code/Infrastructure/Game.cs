using Code.Services.Input;
using UnityEngine.Device;

namespace Code.Infrastructure
{
    public class Game
    {
        public static IInputService InputService;

        public Game()
        {
            RegisterInputService();
        }

        private static void RegisterInputService()
        {
            if (Application.isEditor)
                InputService = new StandaloneInputService();
            else
            {
                InputService = new MobileInputService();
            }
        }
    }
}