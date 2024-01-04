using UnityEngine;

[RequireComponent(typeof(CameraMoveInput))]
public class CameraHorizontalMotion : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _acceleration = 10f;
    [SerializeField] private float _slowing = 15f;

    private float _currentSpeed;

    private Vector3 _lastPosition;
    private Vector3 _targetMotionPosition;
    private Vector3 _horizontalVelocity;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void OnEnable()
    {
        _lastPosition = _transform.position;
    }

    public void DirectTargetMotionPosition(Vector3 direction)
    {
        _targetMotionPosition += direction;
    }

    public void UpdateVelocity()
    {
        _horizontalVelocity = (_transform.position - _lastPosition) / Time.deltaTime;
        _horizontalVelocity.y = 0;
        _lastPosition = _transform.position;
    }

    public void UpdateHandlerPosition(float moveTolerance)
    {
        if (_targetMotionPosition.sqrMagnitude > moveTolerance)
        {
            _currentSpeed = Mathf.Lerp(_currentSpeed, _moveSpeed, Time.deltaTime * _acceleration);
            _transform.position += _targetMotionPosition * _currentSpeed * Time.deltaTime;
        }
        else
        {
            _horizontalVelocity = Vector3.Lerp(_horizontalVelocity, Vector3.zero, Time.deltaTime * _slowing);
            _transform.position += _horizontalVelocity * Time.deltaTime;
        }

        _targetMotionPosition = Vector3.zero;
    }
}