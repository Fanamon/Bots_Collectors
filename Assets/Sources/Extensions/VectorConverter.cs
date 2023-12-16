namespace TransformParametersConverters
{
    public static class VectorConverter
    {
        public static UnityEngine.Vector3 ToUnity(System.Numerics.Vector3 vector)
        {
            return new UnityEngine.Vector3(vector.X, vector.Y, vector.Z);
        }

        public static System.Numerics.Vector3 ToNumerics(UnityEngine.Vector3 vector)
        {
            return new System.Numerics.Vector3(vector.x, vector.y, vector.z);
        }
    }

    public static class QuaternionConverter
    {
        public static UnityEngine.Quaternion ToUnity(System.Numerics.Quaternion quaternion)
        {
            return new UnityEngine.Quaternion(quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);
        }

        public static System.Numerics.Quaternion ToNumerics(UnityEngine.Quaternion quaternion)
        {
            return new System.Numerics.Quaternion(quaternion.x, quaternion.y, quaternion.z, quaternion.w);
        }
    }
}