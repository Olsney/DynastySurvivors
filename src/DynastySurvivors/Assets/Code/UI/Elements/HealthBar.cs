using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Elements
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private Image _currentHealth;

        public void SetValue(float current, float max) =>
            _currentHealth.fillAmount = current / max;
    }
}