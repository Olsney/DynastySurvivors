using UnityEngine;

namespace Code.Enemy
{
    public abstract class EnemyAttack : MonoBehaviour
    {
        public abstract void EnableAttack();
        public abstract void DisableAttack();

        protected Transform HeroTransform { get; private set; }
    }
}