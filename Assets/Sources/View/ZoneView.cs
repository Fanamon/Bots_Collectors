using UnityEngine;

public abstract class ZoneView : MonoBehaviour
{
    [SerializeField] protected Transform Helper;

    protected virtual void OnDrawGizmos()
    {
        if (Helper == null)
        {
            return;
        }

        Vector3 zoneDiagonal = Helper.localPosition * 2f;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position,
            Vector3.ProjectOnPlane(zoneDiagonal, Vector3.up));
    }
}