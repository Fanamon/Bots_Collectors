using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class UnitMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;

    private Transform _transform;

    private Coroutine _move = null;

    public event UnityAction MoveCompleted;

    private void Awake()
    {
        _transform = transform;
        GetComponent<Rigidbody>().freezeRotation = true;
    }

    public void MoveTo(Vector3 position)
    {
        if (_move != null)
        {
            StopCoroutine(_move);
        }

        _move = StartCoroutine(Move(position));
    }

    public void StopMove()
    {
        StopCoroutine(_move);
    }

    private IEnumerator Move(Vector3 targetPosition)
    {
        Vector3 direction;

        while (_transform.position != targetPosition)
        {
            direction = Vector3.ProjectOnPlane(targetPosition - _transform.position, Vector3.up).normalized;

            _transform.forward = Vector3.Slerp(_transform.forward, direction, _rotationSpeed * Time.deltaTime);
            _transform.position = Vector3.MoveTowards(_transform.position, targetPosition, _speed * Time.deltaTime);

            yield return null;
        }

        MoveCompleted?.Invoke();
        _move = null;
    }
}
