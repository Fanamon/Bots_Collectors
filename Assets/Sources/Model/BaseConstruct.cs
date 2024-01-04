using System.Numerics;

public class BaseConstruct
{
    public bool CheckCurrentUnitCloserToBaseThanPrevious(Vector3 currentUnitPosition,
        Vector3 previousUnitPosition, Vector3 basePosition)
    {
        return (basePosition - currentUnitPosition).Length() < (basePosition - previousUnitPosition).Length();
    }
}
