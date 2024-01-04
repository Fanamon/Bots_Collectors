using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using LayerMaskExtensions;

public class PlayerClickInput : MonoBehaviour
{
    private const float MaxRayDistance = 100f;

    private CameraControlActions _cameraControlActions;

    private Camera _camera;

    private Vector3 _lastFloorProjectPosition = Vector3.zero;

    public event UnityAction BaseSelected;
    public event UnityAction BuildPlaceSelected;
    public event UnityAction BuildingDeclined;

    private void Awake()
    {
        _cameraControlActions = new CameraControlActions();
        _camera = GetComponentInChildren<Camera>();

        if (_camera == null)
        {
            throw new System.NullReferenceException("Camera not found!");
        }
    }

    private void OnEnable()
    {
        _cameraControlActions.Camera.Click.performed += OnBaseSelected;
        _cameraControlActions.Camera.Enable();
    }

    private void OnDisable()
    {
        _cameraControlActions.Camera.Click.performed -= OnBaseSelected;
        _cameraControlActions.Camera.Click.performed -= OnPlaceSelected;
        _cameraControlActions.Camera.Disable();
    }

    public Vector3 GetPositionOnFloorPlane()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = _camera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, MaxRayDistance, LayerMasks.Floor))
        {
            _lastFloorProjectPosition = hitInfo.point;
            _lastFloorProjectPosition.y = GetSurfaceHeight(hitInfo.transform);
        }

        return _lastFloorProjectPosition;
    }

    private void OnBaseSelected(InputAction.CallbackContext context)
    {
        Ray ray = GetMouseRay();

        if (Physics.Raycast(ray, out RaycastHit hitInfo, MaxRayDistance, LayerMasks.Base))
        {
            BaseSelected?.Invoke();
            _cameraControlActions.Camera.Click.performed -= OnBaseSelected;
            _cameraControlActions.Camera.Click.performed += OnPlaceSelected;
        }
    }

    private void OnPlaceSelected(InputAction.CallbackContext context)
    {
        Ray ray = GetMouseRay();

        if (Physics.Raycast(ray, out RaycastHit hitInfo, MaxRayDistance, LayerMasks.Floor))
        {
            BuildPlaceSelected?.Invoke();
        }
        else
        {
            BuildingDeclined?.Invoke();
        }

        _cameraControlActions.Camera.Click.performed -= OnPlaceSelected;
        _cameraControlActions.Camera.Click.performed += OnBaseSelected;
    }

    private Ray GetMouseRay()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();

        return _camera.ScreenPointToRay(mousePosition);
    }

    private float GetSurfaceHeight(Transform surfaceObject)
    {
        return surfaceObject.position.y + surfaceObject.localScale.y / 2f;
    }
}