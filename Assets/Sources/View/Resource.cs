using UnityEngine;
using UnityEngine.Events;

public class Resource : MonoBehaviour
{
    public event UnityAction Delivered;
    public event UnityAction Gathered;

    public void BeDelivered()
    {
        Delivered?.Invoke();
        Destroy(gameObject);
    }

    public void BeGathered()
    {
        Gathered?.Invoke();
    }
}