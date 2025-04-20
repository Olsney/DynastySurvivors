using UnityEngine;

namespace Code.Logic
{
    public class Cooldown : MonoBehaviour
    {
        private float _timer;

        private void Update()
        {
            UpdateCooldown();
        }

        public void SetCooldown(float time)
        {
            _timer = time;
        }

        public bool IsOnCooldown() =>
            _timer > 0f;

        private void UpdateCooldown()
        {
            if (IsOnCooldown())
                _timer -= Time.deltaTime;
        }
    }
}