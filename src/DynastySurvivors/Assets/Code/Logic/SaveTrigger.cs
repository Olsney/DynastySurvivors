using System;
using Code.Services.SaveLoad;
using UnityEngine;
using Zenject;

namespace Code.Logic
{
    [RequireComponent(typeof(BoxCollider))]
    public class SaveTrigger : MonoBehaviour
    {
        private ISaveLoadService _saveLoadService;

        [SerializeField] private BoxCollider _collider;
        
        [Inject]
        private void Construct(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        private void OnTriggerEnter(Collider other)
        {
            _saveLoadService.SaveProgress();
            
            Debug.Log("Progress Saved");
            
            gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            if(!_collider) 
                return;
            
            Gizmos.color = new Color32(30, 200, 30, 130);
            
            Gizmos.DrawCube(transform.position + _collider.center, _collider.size);
        }
    }
}