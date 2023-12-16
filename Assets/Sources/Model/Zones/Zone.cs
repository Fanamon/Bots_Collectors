using System.Numerics;

public abstract class Zone
{
    protected Vector3 VertexPosition;

    public Zone(Vector3 vertexPosition)
    {
        VertexPosition = vertexPosition;
    }
}
