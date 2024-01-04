using UnityEngine;

public class BuildingFlagMovement : MonoBehaviour
{
    private Transform _transform;
    private Collider _collider;

    public float CenterHeight => GetObjectCenterHeight();

    private void Awake()
    {
        _transform = transform;
        _collider = GetComponent<Collider>();
    }

    public void MoveOnPlaneTo(Vector3 targetPosition)
    {
        targetPosition.y += CenterHeight;
        _transform.position = targetPosition;
    }

    private float GetObjectCenterHeight()
    {
        float objectDownBounds = ColliderCenterToDownPointDistanceCounter.
            GetDistance(_collider);

        return objectDownBounds;
    }
}
