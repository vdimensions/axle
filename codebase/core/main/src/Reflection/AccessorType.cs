namespace Axle.Reflection
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    //[Maturity(CodeMaturity.ProofOfConcept)]
    public enum AccessorType : byte
    {
        Get, 
        Set, 
        Add, 
        Remove
    }
}