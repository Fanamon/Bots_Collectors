using UnityEngine;
using UnityEngine.Events;

public class Resource : MonoBehaviour
{
    public event UnityAction Delivered;

    public void BeDelivered()
    {
        Delivered?.Invoke();
        Destroy(gameObject);
    }
}