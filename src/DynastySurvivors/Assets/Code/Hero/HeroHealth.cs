using Code.Data;
using Code.Services.PersistentProgress;
using UnityEngine;

namespace Code.Hero
{
    public class HeroHealth : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private HeroAnimator _animator;
        private State _state;

        public float Current
        {
            get => _state.CurrentHp;
            set => _state.CurrentHp = value;
        }

        public float Max
        {
            get => _state.MaxHp;
            set => _state.MaxHp = value;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _state = progress.HeroState;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HeroState.CurrentHp = Current;
            progress.HeroState.MaxHp = Max;
        }

        public void TakeDamage(float damage)
        {
            if (Current <= 0)
                return;

            Current -= damage;
            _animator.PlayHit();
        }
    }
}