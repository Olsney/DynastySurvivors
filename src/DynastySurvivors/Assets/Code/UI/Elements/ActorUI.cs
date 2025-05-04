using Code.Logic;
using UnityEngine;

namespace Code.UI.Elements
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HealthBar _healthBar;

        private IHealth _heroHealth;

        public void Construct(IHealth health)
        {
            _heroHealth = health;

            _heroHealth.Changed += OnHealthChanged;
        }
        
        private void Start()
        {
            IHealth health = GetComponent<IHealth>();

            if (health != null)
                Construct(health);
        }

        private void OnHealthChanged() => 
            _healthBar.SetValue(_heroHealth.Current, _heroHealth.Max);

        private void OnDestroy()
        {
            _heroHealth.Changed -= OnHealthChanged;
        }
    }
}