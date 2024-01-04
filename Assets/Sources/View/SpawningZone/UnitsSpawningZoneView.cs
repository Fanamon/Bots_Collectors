using TransformParametersConverters;
using UnityEngine;

public class UnitsSpawningZoneView : SpawningZoneView
{
    private const int UnitSpawnCost = 3;

    [SerializeField] private UnitsKeeper _unitsKeeper;
    [SerializeField] private UnitView _unitPrefab;

    private GameObject _unitToSpawn;
    private Base _base;

    private void Awake()
    {
        _unitToSpawn = _unitPrefab.gameObject;

        _base = GetComponentInParent<Base>();

        if (_base == null)
        {
            throw new System.NullReferenceException("Put UnitsSpawningZoneView object inside Base(component) object!");
        }

        SpawningZone = new SpawningZone(VectorConverter.ToNumerics(Helper.position),
            VectorConverter.ToNumerics(ExceptionZoneHelper.position), GetSpawnHeight(_unitToSpawn));
    }

    private void OnEnable()
    {
        _base.ResourceCountChanged += OnResourceCountChanged;
    }

    private void OnDisable()
    {
        _base.ResourceCountChanged -= OnResourceCountChanged;
    }

    private void OnResourceCountChanged(int resourceCount)
    {
        if (resourceCount >= UnitSpawnCost && _base.IsBuildingFlagConstructed == false)
        {
            _base.Purchase(UnitSpawnCost);

            _unitsKeeper.AddNewUnit(SpawnUnit());
        }
    }

    private UnitView SpawnUnit()
    {
        return Spawn(_unitToSpawn).GetComponent<UnitView>();
    }
}