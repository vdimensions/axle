using System;
using System.Security.Cryptography;

namespace Axle.Security.Cryptography.Algorithms.Sdk
{
    #if !NETSTANDARD
    [Serializable]
    #endif
    public class CryptoTransformProxy : /*Proxy<ICryptoTransform>, */ICryptoTransform
    {
        protected readonly ICryptoTransform Target;

        public CryptoTransformProxy(ICryptoTransform target)// : base(target)
        {
            Target = target;
        }

        public virtual void Dispose() => Target.Dispose();
        void IDisposable.Dispose() => Dispose();

        public virtual int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
        {
            return Target.TransformBlock(inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset);
        }

        public virtual byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
        {
            return Target.TransformFinalBlock(inputBuffer, inputOffset, inputCount);
        }

        public virtual int InputBlockSize => Target.InputBlockSize;
        public virtual int OutputBlockSize => Target.OutputBlockSize;
        public virtual bool CanReuseTransform => Target.CanReuseTransform;
        public virtual bool CanTransformMultipleBlocks => Target.CanTransformMultipleBlocks;
    }
}
