namespace LayerMaskExtensions
{
    public static class LayerMasks
    {
        private const string FloorMaskName = "Floor";
        private const string BaseMaskName = "Base";

        public static UnityEngine.LayerMask Floor => UnityEngine.LayerMask.GetMask(FloorMaskName);
        public static UnityEngine.LayerMask Base => UnityEngine.LayerMask.GetMask(BaseMaskName);
    }
}