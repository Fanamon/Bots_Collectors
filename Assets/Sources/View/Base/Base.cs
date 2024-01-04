using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class Base : MonoBehaviour
{
    [SerializeField] private BaseBuildingView _baseBuildingView;
    [SerializeField] private SpawningZoneView _spawningZoneView;

    private int _resourcesCount = 0;

    public bool IsBuildingFlagConstructed { get; private set; }
    public Vector3 BuildingFlagPosition { get; private set; }

    public event UnityAction BuildingFlagRemoved;
    public event UnityAction<UnitView> UnitAdded;
    public event UnityAction<UnitView> UnitRemoved;
    public event UnityAction<int> ResourceCountChanged;

    private void OnEnable()
    {
        _baseBuildingView.BuildingFlagConstructed += OnBuildingFlagConstructed;
        _baseBuildingView.BuildingFlagRemoved += OnBuildingFlagRemoved;

        _spawningZoneView.SetFloor(_baseBuildingView.Floor);
    }

    private void OnDisable()
    {
        _baseBuildingView.BuildingFlagConstructed -= OnBuildingFlagConstructed;
        _baseBuildingView.BuildingFlagRemoved -= OnBuildingFlagRemoved;
    }

    private void Start()
    {
        ResourceCountChanged?.Invoke(_resourcesCount);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource resource))
        {
            _resourcesCount++;

            resource.BeDelivered();

            ResourceCountChanged?.Invoke(_resourcesCount);
        }
    }

    public void Initialize(BaseBuildingView baseBuildingView)
    {
        _baseBuildingView = baseBuildingView;
        gameObject.SetActive(true);
    }

    public void InitializeConstructionUnit(UnitView unitView)
    {
        _baseBuildingView.InitializeConstructionUnit(unitView);
    }

    public void AddUnit(UnitView unit)
    {
        UnitAdded?.Invoke(unit);
    }

    public void RemoveUnit(UnitView unit)
    {
        UnitRemoved?.Invoke(unit);
    }

    public void Purchase(int purchasingCost)
    {
        _resourcesCount -= purchasingCost;
        ResourceCountChanged?.Invoke(_resourcesCount);
    }

    private void OnBuildingFlagConstructed(Vector3 buildingFlagPosition)
    {
        IsBuildingFlagConstructed = true;
        BuildingFlagPosition = buildingFlagPosition;
    }

    private void OnBuildingFlagRemoved()
    {
        IsBuildingFlagConstructed = false;
        BuildingFlagRemoved?.Invoke();
    }
}