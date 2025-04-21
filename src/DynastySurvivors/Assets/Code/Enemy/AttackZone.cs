using Code.Logic;
using UnityEngine;

namespace Code.Enemy
{
    [RequireComponent(typeof(EnemyAttack))]
    public class AttackZone : MonoBehaviour
    {
        [SerializeField] private EnemyAttack _attack;
        [SerializeField] private TriggerObserver _triggerObserver;

        private void Start()
        {
            _triggerObserver.Entered += OnEntered;
            _triggerObserver.Exited += OnExited;

            _attack.DisableAttack();
        }

        private void OnEntered(Collider obj) => 
            _attack.EnableAttack();

        private void OnExited(Collider obj) => 
            _attack.DisableAttack();
    }
}