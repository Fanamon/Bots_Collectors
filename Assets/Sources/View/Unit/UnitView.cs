using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(UnitMovement))]
[RequireComponent(typeof(ResourceTaker))]
public class UnitView : MonoBehaviour
{
    private bool IsTookResource = false;

    private Base _base;

    private UnitMovement _unitMovement;
    private ResourceTaker _unitGather;

    private Resource _resourceToGather;

    public bool IsFree { get; private set; }

    public event UnityAction<UnitView> Freed;
    public event UnityAction ConstructionFinished;

    private void Awake()
    {
        IsFree = true;
        _unitMovement = GetComponent<UnitMovement>();
        _unitGather = GetComponent<ResourceTaker>();
    }

    private void OnEnable()
    {
        _unitMovement.MoveCompleted += OnMoveCompleted;
    }

    private void OnDisable()
    {
        _unitMovement.MoveCompleted -= OnMoveCompleted;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource resource) && resource == _resourceToGather)
        {
            IsTookResource = true;
            _unitGather.TakeResource(_resourceToGather);
            _resourceToGather.Gathered -= OnResourceGathered;
            _resourceToGather.Delivered += OnDelivered;
            _unitMovement.MoveTo(_base.transform.localPosition);
        }
    }

    public void InitializeBase(Base mainBase)
    {
        _base = mainBase;
    }

    public void Gather(Resource resource)
    {
        IsFree = false;
        IsTookResource = false;
        _resourceToGather = resource;
        _resourceToGather.Gathered += OnResourceGathered;

        _unitMovement.MoveTo(resource.transform.position);
    }

    public void Construct(Vector3 basePosition)
    {
        IsFree = false;

        _unitMovement.MoveTo(basePosition);
    }

    public void DeclineConstruction()
    {
        _unitMovement.StopMove();
        IsFree = true;
        Freed?.Invoke(this);
    }

    public void FinishConstruction()
    {
        _base.RemoveUnit(this);
        DeclineConstruction();
        ConstructionFinished?.Invoke();
    }

    private void OnResourceGathered()
    {
        if (IsTookResource == false)
        {
            _resourceToGather.Gathered -= OnResourceGathered;
            OnMoveCompleted();
        }
    }

    private void OnMoveCompleted()
    {
        IsFree = true;
        Freed?.Invoke(this);
    }

    private void OnDelivered()
    {
        _resourceToGather.Delivered -= OnDelivered;
        _unitMovement.StopMove();
        IsFree = true;
        Freed?.Invoke(this);
    }
}