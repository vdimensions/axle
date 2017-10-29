namespace Axle.Extensions.Uri
{
    /// <summary>
    /// A static class containing common extension methods to <see cref="Uri"/> instances.
    /// </summary>
    public static partial class UriExtensions
    {
        internal static string UriSchemeFile => System.Uri.UriSchemeFile;
        internal static string UriSchemeHttp => System.Uri.UriSchemeHttp;
        internal static string UriSchemeHttps => System.Uri.UriSchemeHttps;
        internal static string UriSchemeFtp => System.Uri.UriSchemeFtp;
    }
}