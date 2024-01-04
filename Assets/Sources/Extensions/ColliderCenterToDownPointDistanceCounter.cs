using UnityEngine;

public static class ColliderCenterToDownPointDistanceCounter
{
    private const float HalfMultipliyer = 0.5f;

    public static float GetDistance(Collider collider)
    {
        float colliderScaleY = collider.transform.localScale.y;

        if (collider.TryGetComponent(out BoxCollider boxCollider))
        {
            return boxCollider.size.y * HalfMultipliyer * colliderScaleY;
        }
        else if (collider.TryGetComponent(out SphereCollider sphereCollider))
        {
            return sphereCollider.radius * colliderScaleY;
        }
        else if (collider.TryGetComponent(out CapsuleCollider capsuleCollider))
        {
            return capsuleCollider.height * HalfMultipliyer * colliderScaleY;
        }
        else
        {
            throw new System.NullReferenceException("Invalid collider type, add this one.");
        }
    }
}
