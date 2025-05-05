using System;
using Code.Data;
using Code.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        private IPersistentProgressService _progressService;

        [SerializeField] private Button _closeButton;
        protected PlayerProgress Progress => _progressService.Progress;

        public void Construct(IPersistentProgressService progressService)
        {
            _progressService = progressService;
        }
        
        private void Awake() => 
            OnAwake();

        private void Start()
        {
            Initialize();
            SubscribeUpdates();
        }

        private void OnDestroy() => 
            Cleanup();

        protected virtual void OnAwake() => 
            _closeButton.onClick.AddListener(() => OnCloseButtonClicked());

        private void OnCloseButtonClicked() => 
            Destroy(gameObject);

        protected virtual void Initialize() { }

        protected virtual void SubscribeUpdates() { }

        protected virtual void Cleanup() { }
    }
}