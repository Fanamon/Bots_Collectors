using System.Collections.Generic;
using TransformParametersConverters;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class GatheringZoneView : ZoneView
{
    [SerializeField] private UnitsKeeper _unitsKeeper;

    private GatheringZone _gatheringZone;
    private BaseConstructingView _baseConstructingView;

    private Queue<Resource> _untouchedResources = new Queue<Resource>();

    private void Awake()
    {
        _gatheringZone = new GatheringZone(VectorConverter.ToNumerics(Helper.position));
        _baseConstructingView = GetComponentInParent<BaseConstructingView>();

        if (_baseConstructingView == null)
        {
            throw new System.NullReferenceException("Put UnitsSpawningZoneView object inside Base(component) object!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Resource resource))
        {
            if (TryOrderUnitToGather(resource) == false)
            {
                _untouchedResources.Enqueue(resource);
            }
        }
    }

    private void Update()
    {
        if (_untouchedResources.Count > 0 && _baseConstructingView.IsUnitToBuildRequires == false)
        {
            if (TryOrderUnitToGather(_untouchedResources.Peek()))
            {
                _untouchedResources.Dequeue();
            }
        }
    }

    private bool TryOrderUnitToGather(Resource resource)
    {
        UnitView unitToGather = null;

        _unitsKeeper.TryGetFreeUnits().ForEach(unit =>
        {
            if (unitToGather == null ||
            _gatheringZone.CheckCurrentUnitCloserToResourceThanPrevious(VectorConverter.ToNumerics(unit.transform.position),
            VectorConverter.ToNumerics(unitToGather.transform.position), VectorConverter.ToNumerics(resource.transform.position)))
            {
                unitToGather = unit;
            }
        });

        unitToGather?.Gather(resource);

        return unitToGather != null;
    }

    protected override void OnDrawGizmos()
    {
        Vector3 zoneDiagonal = Helper.localPosition * 2f;

        GetComponent<BoxCollider>().size = Vector3.ProjectOnPlane(zoneDiagonal, Vector3.up);
    }
}