﻿using System.Security.Cryptography;

using Axle.Security.Cryptography.Algorithms.Symmetric.Sdk;


namespace Axle.Security.Cryptography.Algorithms.Symmetric
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    [System.Obsolete("Prefer AES instead. The RC2 cipher is considered broken.")]
    public sealed class RC2HashAlgorithm : AbstractSymmetricHashAlgorithm
    {
        public RC2HashAlgorithm() : base(new RC2CryptoServiceProvider()) { }
    }
}