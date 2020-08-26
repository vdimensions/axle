namespace Axle.Security.Cryptography.Algorithms.Hash
{
    public interface IHashAlgorithm : IEncryptionAlgorithm
    {
        IHashCryptoTransform CreateEncryptor();
        IHashCryptoTransform CreateDecryptor();
    }
}