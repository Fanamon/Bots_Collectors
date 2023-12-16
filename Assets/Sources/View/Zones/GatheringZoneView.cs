using System.Collections.Generic;
using System.Linq;
using TransformParametersConverters;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class GatheringZoneView : MonoBehaviour
{
    [SerializeField] private Transform _helper;
    [SerializeField] private Transform _unitsKeeper;

    private GatheringZone _gatheringZone;

    private List<UnitView> _units = new List<UnitView>();
    private Queue<Resource> _untouchedResources = new Queue<Resource>();

    private void Awake()
    {
        _gatheringZone = new GatheringZone(VectorConverter.ToNumerics(_helper.position));
        _units = _unitsKeeper.GetComponentsInChildren<UnitView>().ToList();
    }

    private void OnEnable()
    {
        foreach (var unit in _units)
        {
            unit.Freed += OnUnitFreed;
        }
    }

    private void OnDisable()
    {
        foreach (var unit in _units)
        {
            unit.Freed -= OnUnitFreed;
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

    private bool TryOrderUnitToGather(Resource resource)
    {
        UnitView unitToGather = null;

        _units.Where(unit => unit.IsFreeToGather).ToList().ForEach(unit =>
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

    private void OnUnitFreed(UnitView unit)
    {
        if (_untouchedResources.Count > 0)
        {
            unit.Gather(_untouchedResources.Dequeue());
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 zoneDiagonal = _helper.localPosition * 2f;

        GetComponent<BoxCollider>().size = Vector3.ProjectOnPlane(zoneDiagonal, Vector3.up);
    }
}