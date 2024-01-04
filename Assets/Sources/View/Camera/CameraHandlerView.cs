using UnityEngine;

[RequireComponent(typeof(CameraMoveInput))]
[RequireComponent(typeof(CameraHorizontalMotion))]
[RequireComponent(typeof(CameraScreenEdgeMotion))]
public class CameraHandlerView : MonoBehaviour
{
    private const float MoveTolerance = 0.1f;

    [SerializeField] private bool _isEdgeMotionUsed = true;

    private CameraMoveInput _cameraControllerInput;
    private CameraHorizontalMotion _horizontalMotion;
    private CameraScreenEdgeMotion _screenEdgeMotion;

    private Vector3 _keyboardInput = Vector3.zero;

    private Transform _camera;

    private void Awake()
    {
        _cameraControllerInput = GetComponent<CameraMoveInput>();
        _horizontalMotion = GetComponent<CameraHorizontalMotion>();
        _screenEdgeMotion = GetComponent<CameraScreenEdgeMotion>();
        _camera = GetComponentInChildren<Camera>().transform;

        if (_camera == null)
        {
            throw new System.NullReferenceException("Camera not found!");
        }
    }

    private void Update()
    {
        TryMoveCameraHandler();

        _horizontalMotion.UpdateVelocity();
        _horizontalMotion.UpdateHandlerPosition(MoveTolerance);
    }

    private void TryMoveCameraHandler()
    {
        _keyboardInput = _cameraControllerInput.GetKeyboardInput(GetCameraRight(),
            GetCameraForward());

        if (_keyboardInput.sqrMagnitude > MoveTolerance)
        {
            _horizontalMotion.DirectTargetMotionPosition(_keyboardInput);
        }
        else if (_isEdgeMotionUsed)
        {
            TryMoveByMouse();
        }
    }

    private void TryMoveByMouse()
    {
        if (_screenEdgeMotion.TryGetMoveDirectionByMousePositionAtScreenEdge(GetCameraRight(),
            GetCameraForward(), out Vector3 moveDirection))
        {
            _horizontalMotion.DirectTargetMotionPosition(moveDirection);
        }
    }

    private Vector3 GetCameraRight()
    {
        return Vector3.Scale(_camera.right, Vector3.right);
    }

    private Vector3 GetCameraForward()
    {
        return Vector3.Scale(_camera.forward, Vector3.forward);
    }
}