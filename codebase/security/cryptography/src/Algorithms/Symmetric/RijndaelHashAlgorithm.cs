using System.Security.Cryptography;

using Axle.Security.Cryptography.Algorithms.Symmetric.Sdk;


namespace Axle.Security.Cryptography.Algorithms.Symmetric
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    public sealed class RijndaelHashAlgorithm : AbstractSymmetricHashAlgorithm
    {
        public RijndaelHashAlgorithm() : base(new RijndaelManaged()) { }
    }
}