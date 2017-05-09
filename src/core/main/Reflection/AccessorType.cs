namespace Axle.Reflection
{
#if !netstandard
    [System.Serializable]
#endif
    //[Maturity(CodeMaturity.ProofOfConcept)]
    public enum AccessorType
    {
        Get, 
        Set, 
        Add, 
        Remove
    }
}