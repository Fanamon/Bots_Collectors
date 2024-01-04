using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class UnitsKeeper : MonoBehaviour
{
    [SerializeField] private Base _base;

    private Transform _transform;

    private List<UnitView> _units = new List<UnitView>();

    public event UnityAction<UnitView> UnitFreed;

    private void Awake()
    {
        foreach (var unit in GetComponentsInChildren<UnitView>())
        {
            unit.InitializeBase(_base);
        }

        _transform = transform;
        _units = GetComponentsInChildren<UnitView>().ToList();
    }

    private void OnEnable()
    {
        _base.UnitAdded += AddNewUnit;
        _base.UnitRemoved += OnUnitRemoved;

        foreach (var unit in _units)
        {
            unit.Freed += OnUnitFreed;
        }
    }

    private void OnDisable()
    {
        _base.UnitAdded -= AddNewUnit;
        _base.UnitRemoved -= OnUnitRemoved;

        foreach (var unit in _units)
        {
            unit.Freed -= OnUnitFreed;
        }
    }

    public void AddNewUnit(UnitView newUnit)
    {
        newUnit.transform.SetParent(_transform);
        newUnit.InitializeBase(_base);
        _units.Add(newUnit);
    }

    public void OnUnitRemoved(UnitView unit)
    {
        _units.Remove(unit);
    }

    public List<UnitView> TryGetFreeUnits()
    {
        return _units.Where(unit => unit.IsFree).ToList();
    }

    private void OnUnitFreed(UnitView unit)
    {
        UnitFreed?.Invoke(unit);
    }
}