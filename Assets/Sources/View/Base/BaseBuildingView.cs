using UnityEngine;
using UnityEngine.Events;

public class BaseBuildingView : MonoBehaviour
{
    [SerializeField] private PlayerClickInput _playerClickInput;
    [SerializeField] private BuildingFlagMovement _buildingFlagPrefab;
    [SerializeField] private Transform _floor;

    private bool _isBaseSelected = false;

    private BuildingFlagView _buildingFlagView;
    private BuildingFlagMovement _buildingFlagMovement;

    public Transform Floor => _floor;
    public bool IsBaseFlagConstructed { get; private set; }

    public event UnityAction<Vector3> BuildingFlagConstructed;
    public event UnityAction BuildingFlagRemoved;

    private void Awake()
    {
        _buildingFlagMovement = Instantiate(_buildingFlagPrefab);
        _buildingFlagView = _buildingFlagMovement.GetComponent<BuildingFlagView>();
        _buildingFlagMovement.gameObject.SetActive(false);
        IsBaseFlagConstructed = false;

        _buildingFlagMovement.GetComponent<BuildingFlag>().Initialize(this);
    }

    private void OnEnable()
    {
        _playerClickInput.BaseSelected += OnBaseClicked;
        _playerClickInput.BuildPlaceSelected += OnBuildPlaceSelected;
        _playerClickInput.BuildingDeclined += OnBuildingDeclined;
    }

    private void OnDisable()
    {
        _playerClickInput.BaseSelected -= OnBaseClicked;
        _playerClickInput.BuildPlaceSelected -= OnBuildPlaceSelected;
        _playerClickInput.BuildingDeclined -= OnBuildingDeclined;
    }

    private void Update()
    {
        if (_isBaseSelected)
        {
            _buildingFlagMovement.MoveOnPlaneTo(_playerClickInput.GetPositionOnFloorPlane());
        }
    }

    public void FinishBuilding()
    {
        _buildingFlagView.EnableBuildingZone();
        OnBuildingDeclined();
    }

    public void InitializeConstructionUnit(UnitView unitView)
    {
        _buildingFlagMovement.GetComponent<BuildingFlag>().InitializeConstructionUnit(unitView);
    }

    private void OnBaseClicked()
    {
        BuildingFlagRemoved?.Invoke();
        _isBaseSelected = true;
        IsBaseFlagConstructed = false;
        _buildingFlagView.EnableBuildingZone();
        _buildingFlagMovement.gameObject.SetActive(true);
        _buildingFlagMovement.MoveOnPlaneTo(_playerClickInput.GetPositionOnFloorPlane());
    }

    private void OnBuildPlaceSelected()
    {
        if (_buildingFlagView.TrySelectPlaceToBuild(_buildingFlagMovement.CenterHeight))
        {
            _isBaseSelected = false;
            IsBaseFlagConstructed = true;
            _buildingFlagView.DisableBuildingZone();
            BuildingFlagConstructed?.Invoke(_buildingFlagMovement.transform.position);
        }
        else
        {
            OnBuildingDeclined();
        }
    }

    private void OnBuildingDeclined()
    {
        _isBaseSelected = false;
        IsBaseFlagConstructed = false;
        _buildingFlagMovement.gameObject.SetActive(false);
        _buildingFlagMovement.transform.position = Vector3.zero;
        BuildingFlagRemoved?.Invoke();
    }
}