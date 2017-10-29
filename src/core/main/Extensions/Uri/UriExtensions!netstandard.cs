using System.Reflection;

using Axle.Environment;
using Axle.Extensions.String;
using Axle.Verification;


namespace Axle.Extensions.Uri
{
    static partial class UriExtensions
    {
        internal static readonly string UriSchemeHttp = System.Uri.UriSchemeHttp;
        internal static readonly string UriSchemeHttps = System.Uri.UriSchemeHttps;
        internal static readonly string UriSchemeFile = System.Uri.UriSchemeFile;
        internal static readonly string UriSchemeFtp = System.Uri.UriSchemeFtp;


        public static Assembly GetAssembly(this System.Uri uri)
        {
            uri.VerifyArgument(nameof(uri))
                .IsNotNull()
                .IsTrue(x => x.IsAbsoluteUri, "Provided uri is not an absolute uri.")
                .IsTrue(x => x.IsEmbeddedResource(), $"Provided uri does not have a valid schema that suggests it is an assebly uri. Assembly uri have one of the following shcemas: {UriSchemeAssembly} or {UriSchemeResource}");
            var assenblyName = uri.IsResource() ? uri.Host.TakeBeforeLast('.') : uri.Host;
            return Platform.Runtime.LoadAssembly(assenblyName);
        }

        public static bool TryGetAssembly(this System.Uri uri, out Assembly assembly)
        {
            uri.VerifyArgument(nameof(uri)).IsNotNull();

            if (uri.IsAbsoluteUri && uri.IsEmbeddedResource())
            {
                assembly = Platform.Runtime.LoadAssembly(uri.Host);
                return true;
            }
            assembly = null;
            return false;
        }
    }
}