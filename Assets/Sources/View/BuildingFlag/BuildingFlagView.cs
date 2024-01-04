using UnityEngine;
using LayerMaskExtensions;

public class BuildingFlagView : MonoBehaviour
{
    private const float HalfMultipliyer = 0.5f;

    [SerializeField] private float _buildingZoneScale = 5f;

    [SerializeField] private Transform _buildingZone;

    private Vector3 _buildingZoneHalfExtents;

    private FloorChecker[] _floorCheckers;

    private void Awake()
    {
        _floorCheckers = GetComponentsInChildren<FloorChecker>();
        _buildingZoneHalfExtents = Vector3.Scale(_buildingZone.GetComponent<BoxCollider>().size, 
            _buildingZone.localScale) * HalfMultipliyer;
    }

    private void OnValidate()
    {
        _buildingZone.localScale = new Vector3(_buildingZoneScale, _buildingZoneScale, _buildingZoneScale);
    }

    public void DisableBuildingZone()
    {
        _buildingZone.gameObject.SetActive(false);
    }

    public void EnableBuildingZone()
    {
        _buildingZone.gameObject.SetActive(true);
    } 

    public bool TrySelectPlaceToBuild(float maxDistance)
    {
        bool isSelectedPlaceCorrect = true;

        for (int i = 0; i < _floorCheckers.Length; i++)
        {
            if (Physics.Raycast(_floorCheckers[i].transform.position, Vector3.down, maxDistance, 
                LayerMasks.Floor) == false)
            {
                isSelectedPlaceCorrect = false; 
                break;
            }
        }

        return isSelectedPlaceCorrect && TryUncrossOtherBases();
    }

    private bool TryUncrossOtherBases()
    {
        bool isOtherBaseUncrossed = true;

        if (Physics.OverlapBox(_buildingZone.position, _buildingZoneHalfExtents, 
            Quaternion.identity, LayerMasks.Base).Length > 0)
        {
            isOtherBaseUncrossed = false;
        }

        return isOtherBaseUncrossed;
    }
}
