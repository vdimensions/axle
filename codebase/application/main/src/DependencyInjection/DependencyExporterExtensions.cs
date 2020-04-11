using Axle.Verification;

namespace Axle.DependencyInjection
{
    /// <summary>
    /// A static class containing common extension methods for <see cref="IDependencyExporter"/> instances.
    /// </summary>
    public static class DependencyExporterExtensions
    {
        public static IDependencyExporter Export<T>(this IDependencyExporter exporter, T instance)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(exporter, nameof(exporter)));
            Verifier.IsNotNull(Verifier.VerifyArgument(instance, nameof(instance)));
            return exporter.Export(instance, string.Empty);
        }
    }
}