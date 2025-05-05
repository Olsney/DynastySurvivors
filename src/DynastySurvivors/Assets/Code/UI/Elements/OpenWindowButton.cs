using Code.UI.Services.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Elements
{
    public class OpenWindowButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private WindowId _windowID;

        private IWindowService _windowService;

        public void Construct(IWindowService windowService) => 
            _windowService = windowService;

        private void Awake() => 
            _button.onClick.AddListener(Open);

        private void Open() =>
            _windowService.Open(_windowID);
    }
}