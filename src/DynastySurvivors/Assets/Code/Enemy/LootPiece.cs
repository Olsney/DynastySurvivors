using System;
using System.Collections;
using Code.Data;
using Code.Logic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Enemy
{
    public class LootPiece : MonoBehaviour
    {
        private const float LootDestroyingDelay = 1.5f;
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private GameObject _lootPickUpFx;
        [SerializeField] private GameObject _lootVisual;
        [SerializeField] private TextMeshPro _lootPopupText;
        [SerializeField] private GameObject _lootPopup;
        
        private Loot _loot;
        private bool _pickedUp;
        private WorldData _worldData;

        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
        }

        public void Initialize(Loot loot)
        {
            _loot = loot;
        }
        
        private void Start()
        {
            _triggerObserver.Entered += OnTriggerEnter;
        }

        private void OnDestroy()
        {
            _triggerObserver.Entered -= OnTriggerEnter;
        }

        private void OnTriggerEnter(Collider other) => 
            PickUp();

        private void PickUp()
        {
            if (_pickedUp)
                return;
            
            _pickedUp = true;

            UpdateLootData();
            HideLootVisual();
            SpawnPickUpFx();
            ShowLootPopupText();
            
            StartCoroutine(DestroyLootAfterDelay(LootDestroyingDelay));
        }

        private void UpdateLootData() => 
            _worldData.LootData.Collect(_loot);

        private void HideLootVisual() => 
            _lootVisual.SetActive(false);

        private void SpawnPickUpFx() => 
            Instantiate(_lootPickUpFx, transform.position, Quaternion.identity);

        private void ShowLootPopupText()
        {
            _lootPopupText.text = $"{_loot.Value}";
            _lootPopup.SetActive(true);
        }

        private IEnumerator DestroyLootAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            
            Destroy(gameObject);
        }
    }
}