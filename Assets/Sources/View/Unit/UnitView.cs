using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(UnitMovement))]
[RequireComponent(typeof(ResourceTaker))]
public class UnitView : MonoBehaviour
{
    private Transform _base;

    private UnitMovement _unitMovement;
    private ResourceTaker _unitGather;

    private Resource _resourceToGather;

    public bool IsFreeToGather { get; private set; }

    public event UnityAction<UnitView> Freed;

    private void Awake()
    {
        IsFreeToGather = true;
        _unitMovement = GetComponent<UnitMovement>();
        _unitGather = GetComponent<ResourceTaker>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Resource resource) && resource == _resourceToGather)
        {
            _unitGather.TakeResource(_resourceToGather);
            _resourceToGather.Delivered += OnDelivered;
            _unitMovement.MoveTo(_base.localPosition);
        }
    }

    public void InitializeBase(Base mainBase)
    {
        _base = mainBase.transform;
    }

    public void Gather(Resource resource)
    {
        IsFreeToGather = false;
        _resourceToGather = resource;

        _unitMovement.MoveTo(resource.transform.position);
    }

    private void OnDelivered()
    {
        _resourceToGather.Delivered -= OnDelivered;
        _unitMovement.StopMove();
        IsFreeToGather = true;
        Freed?.Invoke(this);
    }
}