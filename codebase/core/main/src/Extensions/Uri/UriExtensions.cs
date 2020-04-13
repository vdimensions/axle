#if NETSTANDARD || NET20_OR_NEWER
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

using Axle.Verification;


namespace Axle.Extensions.Uri
{
    using StringComparison = System.StringComparison;

    /// <summary>
    /// A <see langword="static"/> class containing common extension 
    /// methods to <see cref="System.Uri"/> instances.
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// The uri scheme for accessing resources embedded into an assembly. 
        /// Similar to the <see cref="UriSchemeResource"/> but requires the assembly <em>name</em> 
        /// to be provided as a host of the <see cref="System.Uri"/>.
        /// </summary>
        public const string UriSchemeAssembly = "assembly";
        /// <summary>
        /// The uri scheme for accessing resources embedded into an assembly. 
        /// Similar to the <see cref="UriSchemeAssembly"/> but requires the assembly <em>file name</em>
        /// to be provided as a host of the <see cref="System.Uri"/>.
        /// </summary>
        public const string UriSchemeResource = "res";

        /// <summary>
        /// The uri scheme for the http protocol.
        /// </summary>
        #if NETSTANDARD
        public const string UriSchemeHttp = "http";
        #else
        public static readonly string UriSchemeHttp = System.Uri.UriSchemeHttp;
        #endif

        /// <summary>
        /// The uri scheme for the https (secure http) protocol.
        /// </summary>
        #if NETSTANDARD
        public const string UriSchemeHttps = "https";
        #else
        public static readonly string UriSchemeHttps = System.Uri.UriSchemeHttps;
        #endif

        /// <summary>
        /// The uri scheme for a file system location.
        /// </summary>
        #if NETSTANDARD
        public const string UriSchemeFile = "file";
        #else
        public static readonly string UriSchemeFile = System.Uri.UriSchemeFile;
        #endif

        /// <summary>
        /// The uri scheme for ftp location.
        /// </summary>
        #if NETSTANDARD
        public const string UriSchemeFtp = "ftp";
        #else
        public static readonly string UriSchemeFtp = System.Uri.UriSchemeFtp;
        #endif

        #if NETSTANDARD || NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        private static bool SchemeEquals(System.Uri uri, string scheme)
        {
            return uri.IsAbsoluteUri && SchemeEqualsAssumeAbsolute(uri, scheme);
        }
        #if NETSTANDARD || NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        private static bool SchemeEqualsAssumeAbsolute(System.Uri uri, string scheme)
        {
            return uri.Scheme.Equals(scheme, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Determines if a given <see cref="System.Uri">URI</see> references a file on the file system.
        /// The check is done against the <see cref="System.Uri">URI</see>'s scheme, expecting it to be <c>file://</c>
        /// </summary>
        /// <param name="uri">The <see cref="System.Uri">URI</see> to check.</param>
        /// <returns>
        /// <c>true</c> if the given <see cref="System.Uri">URI</see> references a file on the file system;
        /// <c>false</c> otherwise.
        /// </returns>
        public static bool IsFile(this System.Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }
            return uri.IsFile || SchemeEquals(uri, UriSchemeFile);
        }

        /// <summary>
        /// Determines if a given <see cref="System.Uri">URI</see> references a resource over FTP connection.
        /// The check is done against the <see cref="System.Uri">URI</see>'s scheme, expecting it to be <c>ftp://</c>
        /// </summary>
        /// <param name="uri">The <see cref="System.Uri">URI</see> to check.</param>
        /// <returns>
        /// <c>true</c> if the given <see cref="System.Uri">URI</see> references a resource over FTP connection;
        /// <c>false</c> otherwise.
        /// </returns>
        public static bool IsFtp(this System.Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }
            return SchemeEquals(uri, UriSchemeFtp);
        }

        /// <summary>
        /// Determines if a given <see cref="System.Uri">URI</see> references a resource over HTTP connection.
        /// The check is done against the <see cref="System.Uri">URI</see>'s scheme, expecting it to be <c>http://</c>
        /// </summary>
        /// <param name="uri">The <see cref="System.Uri">URI</see> to check.</param>
        /// <returns>
        /// <c>true</c> if the given <see cref="System.Uri">URI</see> references a resource over HTTP connection;
        /// <c>false</c> otherwise.
        /// </returns>
        public static bool IsHttp(this System.Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }
            return SchemeEquals(uri, UriSchemeHttp);
        }

        /// <summary>
        /// Determines if a given <see cref="System.Uri">URI</see> references a resource over HTTPS connection.
        /// The check is done against the <see cref="System.Uri">URI</see>'s scheme, expecting it to be <c>https://</c>
        /// </summary>
        /// <param name="uri">The <see cref="System.Uri">URI</see> to check.</param>
        /// <returns>
        /// <c>true</c> if the given <see cref="System.Uri">URI</see> references a resource over HTTPS connection;
        /// <c>false</c> otherwise.
        /// </returns>
        public static bool IsSecureHttp(this System.Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }
            return SchemeEquals(uri, UriSchemeHttps);
        }

        /// <summary>
        /// Determines if a given <see cref="System.Uri">URI</see> references an embedded resource.
        /// The check is done against the <see cref="System.Uri">URI</see>'s scheme, expecting it to be <c>res://</c>
        /// </summary>
        /// <param name="uri">The <see cref="System.Uri">URI</see> to check.</param>
        /// <returns>
        /// <c>true</c> if the given <see cref="System.Uri">URI</see> references an embedded resource;
        /// <c>false</c> otherwise.
        /// </returns>
        public static bool IsResource(this System.Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }
            return SchemeEquals(uri, UriSchemeResource);
        }

        /// <summary>
        /// Determines if a given <see cref="System.Uri">URI</see> references an embedded resource.
        /// The check is done against the <see cref="System.Uri">URI</see>'s scheme, expecting it to be <c>assembly://</c>
        /// </summary>
        /// <param name="uri">The <see cref="System.Uri">URI</see> to check.</param>
        /// <returns>
        /// <c>true</c> if the given <see cref="System.Uri">URI</see> references an embedded resource;
        /// <c>false</c> otherwise.
        /// </returns>
        public static bool IsAssembly(this System.Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }
            return SchemeEquals(uri, UriSchemeAssembly);
        }

        /// <summary>
        /// Determines if a given <see cref="System.Uri">URI</see> references an embedded resource.
        /// The check is done against the <see cref="System.Uri">URI</see>'s scheme, expecting it to be
        /// <c>assembly://</c> or <c>res://</c>
        /// </summary>
        /// <param name="uri">The <see cref="System.Uri">URI</see> to check.</param>
        /// <returns>
        /// <c>true</c> if the given <see cref="System.Uri">URI</see> references an embedded resource;
        /// <c>false</c> otherwise.
        /// </returns>
        /// <seealso cref="IsResource"/>
        /// <seealso cref="IsAssembly"/>
        /// <seealso cref="UriSchemeResource"/>
        /// <seealso cref="UriSchemeAssembly"/>
        public static bool IsEmbeddedResource(this System.Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }
            return SchemeEquals(uri, UriSchemeAssembly) || SchemeEquals(uri, UriSchemeResource);
        }

        public static System.Uri ChangeBase(this System.Uri uri, System.Uri baseUri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }
            return new System.Uri(baseUri, uri);
        }

        /// <summary>
        /// Creates a new <see cref="System.Uri">URI</see> using the address of an existing
        /// <see cref="System.Uri">URI</see>, but with a different <see cref="Uri.Host">host</see>.
        /// </summary>
        /// <param name="uri">The <see cref="System.Uri">URI</see> form which the address will be taken. </param>
        /// <param name="hostname">The hostname to be used for the new <see cref="System.Uri">URI</see>.</param>
        /// <returns>
        /// A new <see cref="System.Uri">URI</see> using the address of an existing <see cref="System.Uri">URI</see>,
        /// that uses the <see cref="System.Uri.Scheme">scheme</see> provided by the <paramref name="hostname"/>
        /// parameter.
        /// </returns>
        public static System.Uri ChangeHost(this System.Uri uri, string hostname) 
        {
            Verifier.IsTrue(Verifier.IsNotNull(Verifier.VerifyArgument(uri, nameof(uri))), x => x.IsAbsoluteUri, "Specified URI must be absolute!");
            StringVerifier.IsNotNullOrEmpty(Verifier.VerifyArgument(hostname, nameof(hostname)));
            return new System.Uri($"{uri.Scheme}://{hostname}{(uri.Port == 0 ? string.Empty : ":" + uri.Port)}{uri.PathAndQuery}");
        }

        /// <summary>
        /// Creates a new <see cref="System.Uri">URI</see> using the address of an existing
        /// <see cref="System.Uri">URI</see>, but with a different <see cref="System.Uri.Scheme">scheme</see>.
        /// </summary>
        /// <param name="uri">The <see cref="System.Uri">URI</see> form which the address will be taken. </param>
        /// <param name="scheme">The schema to be used for the new <see cref="System.Uri">URI</see>.</param>
        /// <returns>
        /// A new <see cref="System.Uri">URI</see> using the address of an existing <see cref="System.Uri">URI</see>,
        /// that uses the <see cref="System.Uri.Scheme">scheme</see> provided by the <paramref name="scheme"/>
        /// parameter.
        /// </returns>
        public static System.Uri ChangeScheme(this System.Uri uri, string scheme)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }
            if (!System.Uri.CheckSchemeName(scheme))
            {
                throw new ArgumentException($"The provided scheme `{scheme}` is not recognized as a valid URI scheme.", nameof(scheme));
            }
            if (!uri.IsAbsoluteUri)
            {
                throw new InvalidOperationException("Cannot change the scheme of a relative uri.");
            }
            return SchemeEqualsAssumeAbsolute(uri, scheme) ? uri : new System.Uri(scheme + uri.ToString().Substring(uri.Scheme.Length), UriKind.Absolute);
        }

        #if NETSTANDARD1_0
        /// <summary>
        /// Resolves an absolute <see cref="System.Uri">URI</see> instance based on the combination of a base
        /// <see cref="System.Uri">URI</see> and a relative <see cref="System.Uri">URI</see>.
        /// </summary>
        /// <param name="uri">
        /// The base <see cref="System.Uri">URI</see>
        /// </param>
        /// <param name="other">
        /// The relative <see cref="System.Uri">URI</see> to be combined with <paramref name="uri"/>
        /// </param>
        /// <returns>
        /// An absolute <see cref="System.Uri">URI</see> instance based on the combination of a base
        /// <see cref="System.Uri">URI</see> and a relative <see cref="System.Uri">URI</see>.
        /// </returns>
        /// <remarks>
        /// This method will immediately return the <paramref name="other"/> in case it represents an
        /// <see cref="System.Uri.IsAbsoluteUri">absolute</see> URI
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="uri" /> is not an absolute <see cref="T:System.Uri" /> instance.
        /// </exception>
        #else
        /// <summary>
        /// Resolves an absolute <see cref="System.Uri">URI</see> instance based on the combination of a base
        /// <see cref="System.Uri">URI</see> and a relative <see cref="System.Uri">URI</see>.
        /// </summary>
        /// <param name="uri">
        /// The base <see cref="System.Uri">URI</see>
        /// </param>
        /// <param name="other">
        /// The relative <see cref="System.Uri">URI</see> to be combined with <paramref name="uri"/>
        /// </param>
        /// <returns>
        /// An absolute <see cref="System.Uri">URI</see> instance based on the combination of a base
        /// <see cref="System.Uri">URI</see> and a relative <see cref="System.Uri">URI</see>.
        /// </returns>
        /// <remarks>
        /// This method will immediately return the <paramref name="other"/> in case it represents an
        /// <see cref="System.Uri.IsAbsoluteUri">absolute</see> URI
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="uri" /> is not an absolute <see cref="T:System.Uri" /> instance.
        /// </exception>
        /// <exception cref="UriFormatException">
        /// The URI formed by combining <paramref name="uri" /> and <paramref name="other" /> is empty or contains only
        /// spaces.
        /// -or-
        /// The scheme specified in the URI formed by combining <paramref name="uri" /> and <paramref name="other" /> 
        /// is not valid.
        /// -or-
        /// The URI formed by combining <paramref name="uri" /> and <paramref name="other" /> contains too many 
        /// slashes.
        /// -or-
        /// The password specified in the URI formed by combining <paramref name="uri" /> and <paramref name="other" />
        /// is not valid.
        /// -or-
        /// The host name specified in the URI formed by combining <paramref name="uri" /> and 
        /// <paramref name="other" /> is not valid.
        /// -or-
        /// The file name specified in the URI formed by combining <paramref name="uri" /> and 
        /// <paramref name="other" /> is not valid.
        /// -or-
        /// The user name specified in the URI formed by combining <paramref name="uri" /> and 
        /// <paramref name="other" /> is not valid.
        /// -or-
        /// The host or authority name specified in the URI formed by combining <paramref name="uri" /> and 
        /// <paramref name="other" /> cannot be terminated by backslashes.
        /// -or-
        /// The port number specified in the URI formed by combining <paramref name="uri" /> and 
        /// <paramref name="other" /> is not valid or cannot be parsed.
        /// -or-
        /// The length of the URI formed by combining <paramref name="uri" /> and 
        /// <paramref name="other" /> exceeds 65519 characters.
        /// -or-
        /// The length of the scheme specified in the URI formed by combining <paramref name="uri" /> and 
        /// <paramref name="other" /> exceeds 1023 characters.
        /// -or-
        /// There is an invalid character sequence in the URI formed by combining <paramref name="uri" /> and 
        /// <paramref name="other" />.
        /// -or-
        /// The MS-DOS path specified in <paramref name="uri" /> must start with <c>c:\</c>.
        /// </exception>
        #endif
        public static System.Uri Resolve(this System.Uri uri, System.Uri other)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(uri, nameof(uri)));
            Verifier.IsNotNull(Verifier.VerifyArgument(uri, nameof(other)));

            if (other.IsAbsoluteUri)
            {
                return other;
            }

            var kind = uri.IsAbsoluteUri ? UriKind.Absolute : UriKind.Relative;
            var val = new StringBuilder(uri.ToString());
            var oth = new StringBuilder(other.ToString());
            if (val.Length > 0 && val[val.Length - 1] == '/')
            {
                if (oth.Length >= 1 && oth[0] == '/')
                {
                    oth = oth.Remove(0, 1);
                }
            }
            else if (oth[0] != '/')
            {
                oth = oth.Insert(0, '/');
            }
            return new System.Uri(val.Append(oth).ToString(), kind);
        }

        /// <summary>
        /// Resolves an absolute <see cref="System.Uri">URI</see> instance based on the combination of a base
        /// <see cref="System.Uri">URI</see> and a relative <see cref="System.Uri">URI</see>.
        /// </summary>
        /// <param name="uri">The base <see cref="System.Uri">URI</see></param>
        /// <param name="other">A string representation of the relative <see cref="System.Uri">URI</see>
        /// to be combined with <paramref name="uri"/></param>
        /// <returns>
        /// An absolute <see cref="System.Uri">URI</see> instance based on the combination of
        /// a base <see cref="System.Uri">URI</see> and a relative <see cref="System.Uri">URI</see>.
        /// </returns>
        /// <remarks>
        /// This method will immediately return the <paramref name="other"/> in case it represents an
        /// <see cref="System.Uri.IsAbsoluteUri">absolute</see> URI.
        /// </remarks>
        public static System.Uri Resolve(this System.Uri uri, string other)
        {
            return Resolve(
                uri,
                new System.Uri(
                    StringVerifier.IsNotEmpty(Verifier.IsNotNull(Verifier.VerifyArgument(other, nameof(other)))).Value,
                    UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        /// Gets a dictionary of key-value pairs representing the query parameters of the given <paramref name="uri"/>.
        /// </summary>
        /// <param name="uri">
        /// The <see cref="System.Uri"/> instance to get the query parameters from.
        /// </param>
        /// <returns>
        /// A dictionary of key-value pairs representing the query parameters of the given <paramref name="uri"/>.
        /// </returns>
        public static IDictionary<string, string> GetQueryParameters(this System.Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            var comparer = StringComparer.OrdinalIgnoreCase;
            if (uri.Query.Length == 0)
            {
                return new Dictionary<string, string>(comparer);
            }

            return Enumerable.ToDictionary(
                Enumerable.GroupBy(
                    Enumerable.Select(
                        uri.Query.TrimStart('?').Split(new[] { '&', ';' }, StringSplitOptions.RemoveEmptyEntries), 
                        parameter => parameter.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries)), 
                    parts => parts[0], 
                    parts => parts.Length > 2 
                        ? string.Join("=", parts, 1, parts.Length - 1) 
                        : (parts.Length > 1 ? parts[1] : ""), 
                    comparer),
                grouping => grouping.Key,
                #if NET40_OR_NEWER
                grouping => string.Join(",", grouping),
                #else
                grouping => string.Join(",", Enumerable.ToArray(grouping)),
                #endif
                comparer);
        }

        /// <summary>
        /// Gets an array containing the path segments that make up the specified <paramref name="uri"/>.
        /// <remarks>
        /// This method does the same thing as the <see cref="System.Uri.Segments"/> property, but can be used
        /// on relative URIs as well. 
        /// </remarks>
        /// </summary>
        /// <param name="uri">
        /// The uri to get the segments for.
        /// </param>
        /// <returns>
        /// An array containing the path segments that make up the specified <paramref name="uri"/>.
        /// </returns>
        public static string[] GetSegments(this System.Uri uri)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(uri, nameof(uri)));
            if (uri.IsAbsoluteUri)
            {
                return uri.Segments;
            }
            else
            {
                const char slash = '/';
                var uriString = uri.ToString();
                var originalStringEndsInSlash = uriString[uriString.Length - 1] == slash;
                var parts = uriString.Split(slash);
                return parts
                    .Select(
                        (x, i) =>
                        {
                            var addSlash = i < parts.Length - 1 || originalStringEndsInSlash;
                            return addSlash ? $"{x}/" : x;
                        })
                    .ToArray();
            }
        }

        /// <summary>
        /// Attempts to get the relative path to a resource provided by the <paramref name="targetUri"/> parameter
        /// in respect to a given <paramref name="baseUri"></paramref>.
        /// </summary>
        /// <param name="baseUri">
        /// The base <see cref="System.Uri"/> to evaluate a relative path from.
        /// </param>
        /// <param name="targetUri">
        /// The <see cref="System.Uri"/> to get a relative path to.
        /// </param>
        /// <param name="result">
        /// An output parameter representing the produced relative uri. 
        /// </param>
        /// <returns>
        /// <c>true</c> if the resulting relative uri was successfully evaluated; <c>false</c> otherwise.
        /// This method also returns <c>false</c> in case either the <paramref name="baseUri"/> or
        /// <paramref name="targetUri"/> represent absolute uris themselves, but differ in their
        /// <see cref="System.Uri.Scheme"/> or <see cref="System.Uri.Host"/>. 
        /// </returns>
        public static bool TryGetRelativePathFrom(this System.Uri baseUri, System.Uri targetUri, out System.Uri result)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(baseUri, nameof(baseUri)));
            Verifier.IsNotNull(Verifier.VerifyArgument(targetUri, nameof(targetUri)));
            
            var comparer = StringComparer.OrdinalIgnoreCase;

            if (baseUri.IsAbsoluteUri && targetUri.IsAbsoluteUri 
                && (!comparer.Equals(baseUri.Scheme, targetUri.Scheme) || !comparer.Equals(baseUri.Host, targetUri.Host)))
            {
                result = null;
                return false;
            }

            var pathBuilder = new StringBuilder();
            var baseSegments = GetSegments(baseUri);
            var targetSegments = GetSegments(targetUri);
            int i = 0, length = Math.Min(baseSegments.Length, targetSegments.Length);
            if (length > 0 && !baseUri.IsAbsoluteUri && !comparer.Equals(baseSegments[0], targetSegments[0]))
            {
                result = null;
                return false;
            }
            for (i = 0; i < length; ++i)
            {
                if (!comparer.Equals(baseSegments[i], targetSegments[i]))
                {
                    break;
                }
            }
            for (var j = i; j < baseSegments.Length; ++j)
            {
                pathBuilder.Append("../");
            }
            for (var k = i; k < targetSegments.Length; ++k)
            {
                pathBuilder.Append(targetSegments[k]);
            }
            result = new System.Uri(pathBuilder.ToString(), UriKind.Relative);
            return true;
        }
    }
}
#endif