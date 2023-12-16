using UnityEngine;

public class GatheringZoneHelperView : MonoBehaviour
{
    private void OnValidate()
    {
        Vector3 zoneDiagonal = transform.localPosition * 2f;

        GetComponentInParent<BoxCollider>().size = Vector3.ProjectOnPlane(zoneDiagonal, Vector3.up);
    }
}
