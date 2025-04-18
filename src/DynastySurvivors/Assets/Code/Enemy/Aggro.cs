using System.Collections;
using Code.Logic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Enemy
{
    public class Aggro : MonoBehaviour
    {
        [SerializeField]
        private TriggerObserver _triggerObserver;
        [SerializeField]
        private Follower _follower;
        [SerializeField]
        private float _delayBeforeStopAggro;

        private Coroutine _stopAggro;
        private bool _hasAggroTarget;

        private void Start()
        {
            SetFollowHeroDisabled();

            _triggerObserver.TriggerEntered += TriggerEntered;
            _triggerObserver.TriggerExited += TriggerExited;
        }

        private void TriggerEntered(Collider obj)
        {
            if (_hasAggroTarget)
                return;

            _hasAggroTarget = true;

            StopAggroCoroutine();
            SetFollowHeroEnabled();
        }

        private void TriggerExited(Collider obj)
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