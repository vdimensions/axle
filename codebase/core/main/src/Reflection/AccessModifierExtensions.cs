namespace Axle.Reflection
{
    internal static class AccessModifierExtensions
    {
        #if NETSTANDARD
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        internal static AccessModifier GetAccessModifier(bool isPublic, bool isAssembly, bool isFamily, bool isPrivate)
        {
            if (isPublic)
            {
                return AccessModifier.Public;
            }
            if (isPrivate)
            {
                return AccessModifier.Private;
            }
            if (isFamily && !isAssembly)
            {
                return AccessModifier.Protected;
            }
            if (isAssembly && !isFamily)
            {
                return AccessModifier.Internal;
            }
            return AccessModifier.ProtectedInternal;
        }
    }
}