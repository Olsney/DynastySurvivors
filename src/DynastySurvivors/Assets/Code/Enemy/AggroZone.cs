using System.Collections;
using Code.Logic;
using UnityEngine;

namespace Code.Enemy
{
    public class AggroZone : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private Follower _follower;
        [SerializeField] private float _delayBeforeStopAggro;

        private Coroutine _stopAggro;
        private bool _hasAggroTarget;

        private void Start()
        {
            SetFollowHeroDisabled();

            _triggerObserver.Entered += Entered;
            _triggerObserver.Exited += Exited;
        }

        private void Entered(Collider obj)
        {
            if (_hasAggroTarget)
                return;

            _hasAggroTarget = true;

            StopAggroCoroutine();
            SetFollowHeroEnabled();
        }

        private void Exited(Collider obj)
        {
            if (!_hasAggroTarget)
                return;

            _hasAggroTarget = false;

            _stopAggro = StartCoroutine(StopAggroAfterDelay());
        }

        private IEnumerator StopAggroAfterDelay()
        {
            yield return new WaitForSeconds(_delayBeforeStopAggro);

            SetFollowHeroDisabled();
        }

        private void SetFollowHeroEnabled() =>
            _follower.enabled = true;

        private void SetFollowHeroDisabled() =>
            _follower.enabled = false;

        private void StopAggroCoroutine()
        {
            if (_stopAggro == null)
                return;

            StopCoroutine(_stopAggro);
            _stopAggro = null;
        }
    }
}