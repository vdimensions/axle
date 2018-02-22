using Axle.References;
using Axle.Verification;


namespace Axle.Extensions.References.WeakReference
{
    /// <summary>
    /// A static class containing common extensions to <see cref="bool"/> values.
    /// </summary>
    public static class WeakReferenceExtensions
    {
        /// <summary>
        /// Tries to retrieve the target object that is referenced by the <paramref name="current"/> <see cref="System.WeakReference"/> object.
        /// </summary>
        /// <param name="current">
        /// The <see cref="System.WeakReference"/> instance this extension method is called on.
        /// </param>
        /// <param name="target">
        /// When this method returns, contains the target object, if it is available. This parameter is treated as uninitialized.
        /// </param>
        /// <returns>
        /// <c>true</c> if the target was retrieved; <c>false</c> otherwise.
        /// </returns>
        public static bool TryGetTarget(this System.WeakReference current, out object target)
        {
            target = current.VerifyArgument(nameof(current)).IsNotNull().Value.Target;
            return current.IsAlive;
        }
    }
}
