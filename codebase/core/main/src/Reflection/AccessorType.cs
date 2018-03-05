namespace Axle.Reflection
{
    #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
    [System.Serializable]
    #endif
    public enum AccessorType : byte
    {
        Get, 
        Set, 
        Add, 
        Remove
    }
}