﻿using Axle.Security.Cryptography.Algorithms;
using Axle.Security.Cryptography.Algorithms.Hash;
using NUnit.Framework;

namespace Axle.Security.Cryptography.Tests.Algorithms.Hash
{
    [TestFixture]
    public sealed class Sha1NativeIntegrityTest : AbstractSha1IntegrityTest
    {
        protected override IEncryptionAlgorithm CreateAlgorithmInstance() => new Sha1NativeHashAlgorithm();
    }
}