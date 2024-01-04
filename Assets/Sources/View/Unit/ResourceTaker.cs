using UnityEngine;

public class ResourceTaker : MonoBehaviour
{
    [SerializeField] private Transform _resourceKeeper;

    public void TakeResource(Resource resource)
    {
        Rigidbody resourceRigidbody = resource.GetComponent<Rigidbody>();
        resource.BeGathered();

        resource.transform.parent = _resourceKeeper;
        resource.transform.localPosition = Vector3.zero;
        resourceRigidbody.useGravity = false;
        resourceRigidbody.constraints = RigidbodyConstraints.FreezePosition;
    }
}
