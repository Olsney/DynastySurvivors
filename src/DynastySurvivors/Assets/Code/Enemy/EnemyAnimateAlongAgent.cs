using UnityEngine;
using UnityEngine.AI;

namespace Code.Enemy
{
    public class EnemyAnimateAlongAgent : MonoBehaviour
    {
        private const float MinAnimatedVelocity = 0.1f;

        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private EnemyAnimator _enemyAnimator;

        public void Update()
        {
            float agentVelocity = _agent.velocity.magnitude;

            if (IsMoving(agentVelocity))
                _enemyAnimator.Move(agentVelocity);
            else
                _enemyAnimator.StopMoving();
        }

        private static bool IsMoving(float agentVelocity) =>
            agentVelocity > MinAnimatedVelocity;
    }
}