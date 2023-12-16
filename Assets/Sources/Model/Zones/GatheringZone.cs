using System.Numerics;

public class GatheringZone : Zone
{
    public GatheringZone(Vector3 vertexPosition) : base(vertexPosition) { }

    public bool CheckCurrentUnitCloserToResourceThanPrevious(Vector3 currentUnitPosition,
        Vector3 previousUnitPosition, Vector3 resourcePosition)
    {
        return (resourcePosition - currentUnitPosition).Length() < (resourcePosition - previousUnitPosition).Length();
    }
}
