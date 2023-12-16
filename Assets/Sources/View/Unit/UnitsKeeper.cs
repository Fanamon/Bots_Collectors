using UnityEngine;

public class UnitsKeeper : MonoBehaviour
{
    [SerializeField] private Base _base;

    private void Awake()
    {
        foreach (var unit in GetComponentsInChildren<UnitView>())
        {
            unit.InitializeBase(_base);
        }
    }
}