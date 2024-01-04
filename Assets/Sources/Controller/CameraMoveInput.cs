using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMoveInput : MonoBehaviour
{
    private CameraControlActions _cameraActions;
    private InputAction _movement;

    private void Awake()
    {
        _cameraActions = new CameraControlActions();
    }

    private void OnEnable()
    {
        _movement = _cameraActions.Camera.Move;
        _cameraActions.Camera.Enable();
    }

    private void OnDisable()
    {
        _cameraActions.Camera.Disable();
    }

    public Vector3 GetKeyboardInput(Vector3 cameraRight, Vector3 cameraForward)
    {
        return (_movement.ReadValue<Vector2>().x * cameraRight +
            _movement.ReadValue<Vector2>().y * cameraForward).normalized;
    }
}