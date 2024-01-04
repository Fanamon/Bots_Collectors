using TransformParametersConverters;
using LayerMaskExtensions;
using UnityEngine;

public abstract class SpawningZoneView : ZoneView
{
    private const float MaxRayDistance = 5f;

    [SerializeField] protected Transform ExceptionZoneHelper;

    protected Transform Floor;
    protected SpawningZone SpawningZone;

    public void SetFloor(Transform floor)
    {
        Floor = floor;
    }

    protected GameObject Spawn(GameObject objectToSpawn)
    {
        Vector3 randomPosition = VectorConverter.ToUnity(SpawningZone.GetRandomPointInZone());

        return Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
    }

    protected float GetSpawnHeight(GameObject objectToSpawn)
    {
        float surfaceHeight = Floor.position.y + Floor.localScale.y / 2f;
        float objectDownBounds = ColliderCenterToDownPointDistanceCounter.
            GetDistance(objectToSpawn.GetComponent<Collider>());

        return surfaceHeight + objectDownBounds;
    }
}
