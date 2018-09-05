#if NETSTANDARD || NET20_OR_NEWER
using System;


namespace Axle.Reflection
{
    public interface ICombineAccessor : IAccessor
    {
        /// <summary>
        /// Combines the target delegate with the one provided by the <paramref name="handler"/>.
        /// </summary>
        /// <param name="target">
        /// 
        /// </param>
        /// <param name="handler">
        /// The delegate to combine. 
        /// </param>
        void AddDelegate(object target, Delegate handler);
    }
}
#endif