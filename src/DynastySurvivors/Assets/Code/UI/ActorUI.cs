using System;
using Code.Hero;
using UnityEngine;

namespace Code.UI
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HealthBar _healthBar;

        private HeroHealth _heroHealth;

        public void Construct(HeroHealth health)
        {
            _heroHealth = health;

            _heroHealth.Changed += OnHealthChanged;
        }

        private void OnDestroy() => 
            _heroHealth.Changed -= OnHealthChanged;

        private void OnHealthChanged()
        {
            _healthBar.SetValue(_heroHealth.Current, _heroHealth.Max);
        }
    }
}