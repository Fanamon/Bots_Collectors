using System.Numerics;
using RandomTypesExtensions;

public class SpawningZone : Zone
{
    private readonly Vector3 _right = new Vector3(1, 0, 0);
    private readonly Vector3 _forward = new Vector3(0, 0, 1);

    private Vector3 ExceptionZoneVertexPosition;

    private float _spawnHeight;

    private readonly RandomFloat _randomFloat;

    public SpawningZone(Vector3 vertexPosition, Vector3 exceptionZoneVertexPosition = new Vector3(), 
        float spawnHeight = 0) : base(vertexPosition)
    {
        ExceptionZoneVertexPosition = exceptionZoneVertexPosition;
        _spawnHeight = spawnHeight;
        _randomFloat = new RandomFloat();
    }

    public Vector3 GetRandomPointInZone()
    {
        Vector3 point = Vector3.Zero;

        point += _right * GetRandomValueExceptInterval(-VertexPosition.X, VertexPosition.X, 
            -ExceptionZoneVertexPosition.X, ExceptionZoneVertexPosition.X);
        point += _forward * GetRandomValueExceptInterval(-VertexPosition.Z, VertexPosition.Z,
            -ExceptionZoneVertexPosition.Z, ExceptionZoneVertexPosition.Z);
        point.Y += _spawnHeight;

        return point;
    }

    private float GetRandomValueExceptInterval(float minValue, float maxValue, float minExceptionValue, float maxExceptionValue)
    {
        float value;

        do
        {
            value = _randomFloat.NextFloat(minValue, maxValue + 1);
        }
        while (value > minExceptionValue && value < maxExceptionValue);

        return value;
    }
}
