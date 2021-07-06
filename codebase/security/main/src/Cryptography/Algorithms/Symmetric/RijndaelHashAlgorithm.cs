using System.Security.Cryptography;

namespace Axle.Security.Cryptography.Algorithms.Symmetric
{
    public sealed class RijndaelHashAlgorithm : AbstractSymmetricHashAlgorithm<RijndaelManaged>
    {
        public RijndaelHashAlgorithm() : base(new RijndaelManaged()) { }
    }
}