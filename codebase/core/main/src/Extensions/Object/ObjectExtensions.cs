using System.Linq;


namespace Axle.Extensions.Object
{
    public static class ObjectExtensions
    {
        public static int CalculateHashCode<T>(this T @this, params object[] args)
        {
            return 31 * args.Aggregate(0, (x, y) => x ^ (y?.GetHashCode() ?? 0));
        }
    }
}
