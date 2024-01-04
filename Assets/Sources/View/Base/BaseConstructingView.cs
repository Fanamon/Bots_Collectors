using TransformParametersConverters;
using UnityEngine;

[RequireComponent(typeof(Base))]
public class BaseConstructingView : MonoBehaviour
{
    private const int BaseCost = 5;

    [SerializeField] private UnitsKeeper _unitsKeeper;

    private Base _base;
    private BaseConstruct _baseConstruct;

    private UnitView _unitToConstruct = null;

    public bool IsUnitToBuildRequires { get; private set; }

    private void Awake()
    {
        _base = GetComponent<Base>();
        _baseConstruct = new BaseConstruct();
        IsUnitToBuildRequires = false;
    }

    private void OnEnable()
    {
        _base.ResourceCountChanged += OnResourceCountChanged;
        _base.BuildingFlagRemoved += OnBuildingFlagRemoved;
    }

    private void OnDisable()
    {
        _base.ResourceCountChanged -= OnResourceCountChanged;
        _base.BuildingFlagRemoved -= OnBuildingFlagRemoved;
    }

    private void OnResourceCountChanged(int resourceCount)
    {
        if (resourceCount >= BaseCost && _base.IsBuildingFlagConstructed && _unitToConstruct == null)
        {
            if (TrySendFreeUnitToConstructBase(_base.BuildingFlagPosition))
            {
                _unitToConstruct.Freed += OnUnitToConstructFreed;
                _unitToConstruct.ConstructionFinished += OnConstructionFinished;
                _base.InitializeConstructionUnit(_unitToConstruct);
                IsUnitToBuildRequires = false;
            }
        }
    }

    private void OnBuildingFlagRemoved()
    {
        if (_unitToConstruct != null)
        {
            _unitToConstruct.DeclineConstruction();
        }
    }

    private void OnUnitToConstructFreed(UnitView unitView)
    {
        _unitToConstruct.Freed -= OnUnitToConstructFreed;
        _unitToConstruct.ConstructionFinished -= OnConstructionFinished;
        _unitToConstruct = null;
    }

    private void OnConstructionFinished()
    {
        _base.Purchase(BaseCost);
    }

    private bool TrySendFreeUnitToConstructBase(Vector3 basePosition)
    {
        IsUnitToBuildRequires = true;

        _unitsKeeper.TryGetFreeUnits().ForEach(unit =>
        {
            if (_unitToConstruct == null ||
            _baseConstruct.CheckCurrentUnitCloserToBaseThanPrevious(VectorConverter.ToNumerics(unit.transform.position),
            VectorConverter.ToNumerics(_unitToConstruct.transform.position), VectorConverter.ToNumerics(basePosition)))
            {
                _unitToConstruct = unit;
            }
        });

        _unitToConstruct?.Construct(basePosition);

        return _unitToConstruct != null;
    }
}