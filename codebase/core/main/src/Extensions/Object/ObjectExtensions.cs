using System.Linq;

using Axle.Verification;


namespace Axle.Extensions.Object
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Calculates the hash code for a given object and its members. The members are passed in as arguments.
        /// </summary>
        /// <remarks>
        /// The usual context of calculating the hash code includes a type's members. However, this method does not
        /// constrain you in any way to pass external and potentially un-related objects that will be used to obtain the resuling hash.
        /// Be advised to carefully choose the arguments when performing calls to this method, as it serves more as a convenient
        /// shortcut to manualy doing the hash calculation.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the object to calculate a hash code for.
        /// </typeparam>
        /// <param name="obj">
        /// The source object to calculate the hash code for. 
        /// Its own <see cref="object.GetHashCode()"/> method is not invoked inside this method, only the hashes of the arguments are being used.
        /// </param>
        /// <param name="magic">
        /// An integer "magic" number, used to augment the combined hash code of the passed in arguments.
        /// </param>
        /// <param name="args">
        /// An array of objects, usually the key members of the target <paramref name="obj"/> that will participate in calculating the final hash code.
        /// </param>
        /// <returns>
        /// An integer representing the hash code as a combined value of the hashes from the objects passed in trough the <paramref name="args"/> parameter.
        /// </returns>
        public static int CalculateHashCode<T>(this T obj, int magic, params object[] args)
        {
            obj.VerifyArgument(nameof(obj)).IsNotNull();
            return (magic == 0 ? 1 : magic) * args.Aggregate(0, (x, y) => x ^ (y?.GetHashCode() ?? 0));
        }

        /// <summary>
        /// Calculates the hash code for a given object and its members. The members are passed in as arguments.
        /// </summary>
        /// <remarks>
        /// The usual context of calculating the hash code includes a type's members. However, this method does not
        /// constrain you in any way to pass external and potentially un-related objects that will be used to obtain the resuling hash.
        /// Be advised to carefully choose the arguments when performing calls to this method, as it serves more as a convenient
        /// shortcut to manualy doing the hash calculation.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the object to calculate a hash code for.
        /// </typeparam>
        /// <param name="obj">
        /// The source object to calculate the hash code for. 
        /// Its own <see cref="object.GetHashCode()"/> method is not invoked inside this method, only the hashes of the arguments are being used.
        /// </param>
        /// <param name="args">
        /// An array of objects, usually the key members of the target <paramref name="obj"/> that will participate in calculating the final hash code.
        /// </param>
        /// <returns>
        /// An integer representing the hash code as a combined value of the hashes from the objects passed in trough the <paramref name="args"/> parameter.
        /// </returns>
        public static int CalculateHashCode<T>(this T obj, params object[] args) => CalculateHashCode(obj, 31, args);
    }
}
