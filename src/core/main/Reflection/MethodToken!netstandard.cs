using System;
using System.Reflection;


namespace Axle.Reflection
{
    [Serializable]
    public partial class MethodToken
    {
        /// <summary>
        /// Creates a new <see cref="MethodToken"/> instance using the provided <paramref name="info"/>.
        /// </summary>
        /// <param name="info">
        /// A <see cref="MethodInfo"/> object containing the reflected information for the represented method.
        /// </param>
        public MethodToken(MethodInfo info) : base(info)
        {
            memberType = info.ReturnType;
        }
    }
}