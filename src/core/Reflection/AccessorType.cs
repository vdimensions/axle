using System;


namespace Axle.Reflection
{
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