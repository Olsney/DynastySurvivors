using System;
using System.Collections.Generic;
using Code.Data;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using UnityEngine;
using Zenject;

namespace Code.Logic
{
    [RequireComponent(typeof(BoxCollider))]
    public class SaveTrigger : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private BoxCollider _collider;
        [SerializeField] private int _id;

        private ISaveLoadService _saveLoadService;
        private bool _isTriggered;

        [Inject]
        private void Construct(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"isTriggered in OnTriggerEnter - {_isTriggered}");

            if (_isTriggered)
                return;

            _isTriggered = true;

            _saveLoadService.SaveProgress();

            Debug.Log("Progress Saved");

            gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            if (!_collider)
                return;

            Gizmos.color = new Color32(30, 200, 30, 130);

            Gizmos.DrawCube(transform.position + _collider.center, _collider.size);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            Debug.Log("Мы загрузили прогресс");
            Debug.Log($"isTriggered in LoadProgress - {_isTriggered}");

            if (progress.WorldData.VisitedTriggerIds.Contains(_id))
            {
                Debug.Log("ID совпал!");
                _isTriggered = true;
                Debug.Log($"isTriggered После совпадения ID - {_isTriggered}. ВЫРУБАЮСЬ!!!");
                Debug.Log($"{_id}");
                // _collider.enabled = false;
                gameObject.SetActive(false);
            }
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (!_isTriggered)
                return;
            
            List<int> visitedTriggerIds = progress.WorldData.VisitedTriggerIds;

            
            if (!visitedTriggerIds.Contains(_id))
                progress.WorldData.VisitedTriggerIds.Add(_id);
        }
    }
}