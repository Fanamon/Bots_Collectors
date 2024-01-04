using UnityEngine;

[RequireComponent(typeof(BuildingFlagView))]
public class BuildingFlag : MonoBehaviour
{
    [SerializeField] private Base _basePrefab;

    private Transform _transform;
    private BaseBuildingView _baseBuildingView;

    private UnitView _contructionUnit;

    private void Awake()
    {
        _transform = transform;
    }

    public void Initialize(BaseBuildingView baseBuildingView)
    {
        _baseBuildingView = baseBuildingView;
    }

    public void InitializeConstructionUnit(UnitView unit)
    {
        _contructionUnit = unit;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out UnitView unit) && unit == _contructionUnit && 
            _baseBuildingView.IsBaseFlagConstructed)
        {
            Vector3 newBasePosition = new Vector3(_transform.position.x, _basePrefab.transform.position.y, 
                _transform.position.z);
            Base newBase = Instantiate(_basePrefab, newBasePosition, Quaternion.identity);

            newBase.Initialize(_baseBuildingView);
            unit.FinishConstruction();
            newBase.AddUnit(unit);
            unit.InitializeBase(newBase);

            _baseBuildingView.FinishBuilding();
        }
    }
}
