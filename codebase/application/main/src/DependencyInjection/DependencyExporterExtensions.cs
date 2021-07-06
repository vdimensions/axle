using Axle.Verification;

namespace Axle.DependencyInjection
{
    /// <summary>
    /// A static class containing common extension methods for <see cref="IDependencyExporter"/> instances.
    /// </summary>
    public static class DependencyExporterExtensions
    {
        private sealed class DependencyExporterProxy : IDependencyExporter
        {
            private readonly IDependencyExporter _impl;

            public DependencyExporterProxy(IDependencyExporter impl)
            {
                _impl = impl;
            }

            IDependencyExporter IDependencyExporter.Export(object instance, string name) => _impl.Export(instance, name);
        }
        
        public static IDependencyExporter Export<T>(this IDependencyExporter exporter, T instance)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(exporter, nameof(exporter)));
            Verifier.IsNotNull(Verifier.VerifyArgument(instance, nameof(instance)));
            return exporter.Export(instance, string.Empty);
        }

        public static IDependencyExporter AsDependencyExporter<T>(this T exporter) where T: IDependencyExporter
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(exporter, nameof(exporter)));
            return new DependencyExporterProxy(exporter);
        }
    }
}