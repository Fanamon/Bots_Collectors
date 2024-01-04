using System.Collections;
using UnityEngine;
using TransformParametersConverters;

public class ResourcesSpawningZoneView : SpawningZoneView
{
    [SerializeField] private float _spawnDelay = 5;

    [SerializeField] private Transform _floor;
    [SerializeField] private Resource _resourcePrefab;

    private void Start()
    {
        GameObject resourceToSpawn = _resourcePrefab.gameObject;
        Floor = _floor;

        SpawningZone = new SpawningZone(VectorConverter.ToNumerics(Helper.position),
            VectorConverter.ToNumerics(ExceptionZoneHelper.position), GetSpawnHeight(resourceToSpawn));

        StartCoroutine(Spawning(resourceToSpawn));
    }

    private IEnumerator Spawning(GameObject resourceToSpawn)
    {
        while (Application.isPlaying)
        {
            Spawn(resourceToSpawn);

            yield return new WaitForSeconds(_spawnDelay);
        }
    }
}
