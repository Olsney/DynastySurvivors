using System;
using Code.Data;
using UnityEngine;

namespace Code.Enemy
{
    public class LootPiece : MonoBehaviour
    {
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

        private void OnTriggerEnter(Collider other) => 
            PickUp();

        private void PickUp()
        {
            if (_pickedUp == true)
                return;
            
            _pickedUp = true;

            _worldData.LootData.Collect(_loot);
        }
    }
}