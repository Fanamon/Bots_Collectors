using System.Collections;
using UnityEngine;
using TransformParametersConverters;

public class SpawningZoneView : MonoBehaviour
{
    [SerializeField] private Transform _helper;
    [SerializeField] private Transform _exceptionZoneHelper;

    [SerializeField] private float _spawnDelay = 5;

    [SerializeField] private GameObject _spawnableObjectPrefab;

    private SpawningZone _spawningZone;

    private void Start()
    {
        _spawningZone = new SpawningZone(VectorConverter.ToNumerics(_helper.position),
            VectorConverter.ToNumerics(_exceptionZoneHelper.position));

        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        Vector3 randomPosition;

        while (Application.isPlaying)
        {
            randomPosition = VectorConverter.ToUnity(_spawningZone.GetRandomPointInZone());

            Instantiate(_spawnableObjectPrefab, randomPosition, Quaternion.identity);

            yield return new WaitForSeconds(_spawnDelay);
        }
    }
}
