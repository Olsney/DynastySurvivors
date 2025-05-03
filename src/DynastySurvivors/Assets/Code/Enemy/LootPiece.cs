using System.Collections;
using Code.Data;
using Code.Logic;
using TMPro;
using UnityEngine;

namespace Code.Enemy
{
    public class LootPiece : MonoBehaviour
    {
        private const float DestroyingDelay = 1.5f;
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
            _triggerObserver.Entered += OnEntered;
        }

        private void OnDestroy()
        {
            _triggerObserver.Entered -= OnEntered;
        }

        private void OnEntered(Collider other) => 
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
            
            StartCoroutine(DestroyLootAfterDelay(DestroyingDelay));
        }

        private void UpdateLootData() => 
            _worldData.LootData.Collect(_loot);

        private void HideLootVisual() => 
            _lootVisual.SetActive(false);

        private void SpawnPickUpFx()
        {
            GameObject pickUpFx = Instantiate(_lootPickUpFx, transform.position + Vector3.up, Quaternion.identity);
            
            Destroy(pickUpFx, DestroyingDelay);
        }

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