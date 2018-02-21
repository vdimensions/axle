using System.Security.Cryptography;

using Axle.Security.Cryptography.Algorithms.Hash.Sdk;


namespace Axle.Security.Cryptography.Algorithms.Hash
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    public sealed class MD5NativeHashAlgorithm : AbstractHashAlgorithm
    {
        public MD5NativeHashAlgorithm() : base(new MD5CryptoServiceProvider()) { }
    }
}