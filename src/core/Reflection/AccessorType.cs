namespace Axle.Reflection
{
    using System;

    [Serializable]
    //[Maturity(CodeMaturity.ProofOfConcept)]
    public enum AccessorType
    {
        Get, 
        Set, 
        Add, 
        Remove
    }
}