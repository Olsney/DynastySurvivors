using Code.Data;
using Code.Services.Input;
using Code.Services.PersistentProgress;
using UnityEngine;
using Zenject;

namespace Code.Hero
{
    [RequireComponent(typeof(CharacterController))]
    public class HeroMove : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _movementSpeed;

        private IInputService _inputService;
        private Camera _camera;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }
        
        private void Start() => 
            _camera = Camera.main;

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;

            _characterController.Move(movementVector * (_movementSpeed * Time.deltaTime));
        }

        public void LoadProgress(PlayerProgress progress)
        {
            progress.WorldData.Position = transform.position.AsVectorData();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
        }
    }
}