using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class Base : MonoBehaviour
{
    private int _resourcesCount = 0;

    public event UnityAction<int> ResourceCountChanged;

    private void Start()
    {
        ResourceCountChanged?.Invoke(_resourcesCount);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Resource resource))
        {
            _resourcesCount++;

            resource.BeDelivered();
            Destroy(resource);

            ResourceCountChanged?.Invoke(_resourcesCount);
        }
    }
}
