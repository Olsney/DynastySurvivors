using UnityEngine;

namespace Code.Enemy
{
    public class EnemyRotateToHero : Follower
    {
        [SerializeField]
        private float _rotationSpeed;
        
        private Transform _heroTransform;
        private Vector3 _positionToLook;
        
        public void Construct(Transform heroTransform)
        {
            _heroTransform = heroTransform;
        }
        
        private void Update()
        {
            if (IsHeroInitialized())
                RotateTowardsHero();
        }

        private bool IsHeroInitialized() =>
            _heroTransform != null;

        private void RotateTowardsHero() =>
            transform.rotation = GetSmoothedRotation(transform.rotation, GetPositionToLookAt());

        private Quaternion GetSmoothedRotation(Quaternion rotation, Vector3 positionToLookAt) =>
            Quaternion.Lerp(rotation, GetTargetRotation(positionToLookAt), SpeedFactor());

        private Vector3 GetPositionToLookAt()
        {
            Vector3 positionDiff = _heroTransform.position - transform.position;
            
            return new Vector3(positionDiff.x , transform.position.y, positionDiff.z);
        }

        private Quaternion GetTargetRotation(Vector3 position) =>
            Quaternion.LookRotation(position);

        private float SpeedFactor() =>
            _rotationSpeed * Time.deltaTime;
    }
}