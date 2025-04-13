using Code;
using Code.CameraLogic;
using Code.Infrastructure;
using Code.Infrastructure.Services;
using Code.Services.Input;
using UnityEngine;
using Zenject;

public class HeroMove : MonoBehaviour
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

    private void Awake()
    {
        // _inputService = AllServices.Container.Single<IInputService>();
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
}