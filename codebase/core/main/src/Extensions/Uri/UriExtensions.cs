using System;
using System.Collections.Generic;
using System.Linq;

using Axle.Verification;


namespace Axle.Extensions.Uri
{
    /// <summary>
    /// A static class containing common extension methods to <see cref="Uri"/> instances.
    /// </summary>
    public static partial class UriExtensions
    {
        public const string UriSchemeAssembly = "assembly";
        public const string UriSchemeResource = "res";

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
        /// Determines if a given <see cref="System.Uri">URI</see> references a file on the filesystem. 
        /// The check is done against the <see cref="System.Uri">URI</see>'s scheme, expexting it to be <c>file://</c>
        /// </summary>
        /// <param name="uri">The <see cref="System.Uri">URI</see> to check.</param>
        /// <returns>
        /// <c>true</c> if the given <see cref="System.Uri">URI</see> references a file on the filesystem; 
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
        /// The check is done against the <see cref="System.Uri">URI</see>'s scheme, expexting it to be <c>ftp://</c>
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
        /// The check is done against the <see cref="System.Uri">URI</see>'s scheme, expexting it to be <c>http://</c>
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
        /// The check is done against the <see cref="System.Uri">URI</see>'s scheme, expexting it to be <c>https://</c>
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
        /// The check is done against the <see cref="System.Uri">URI</see>'s scheme, expexting it to be <c>res://</c>
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
        /// The check is done against the <see cref="System.Uri">URI</see>'s scheme, expexting it to be <c>assembly://</c>
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
        /// The check is done against the <see cref="System.Uri">URI</see>'s scheme, expexting it to be 
        /// <c>assembly://</c> or <c>res://</c>
        /// </summary>
        /// <param name="uri">The <see cref="System.Uri">URI</see> to check.</param>
        /// <returns>
        /// <c>true</c> if the given <see cref="System.Uri">URI</see> references an embedded resource; 
        /// <c>false</c> otherwise.
        /// </returns>
        /// <seealso cref="IsResource"/>
        /// <seealso cref="IsAssembly"/>
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

        public static System.Uri ChangeHost(this System.Uri uri, string hostname) 
        {
            uri.VerifyArgument(nameof(uri)).IsNotNull().IsTrue(x => x.IsAbsoluteUri, "Specified URI must be absolute!");
            hostname.VerifyArgument(nameof(hostname)).IsNotNullOrEmpty();

            return new System.Uri(string.Format(
                "{0}://{1}{2}{3}", 
                uri.Scheme, 
                hostname, 
                uri.Port == 0 ? string.Empty : ":" + uri.Port, 
                uri.PathAndQuery));
        }

        /// <summary>
        /// Creates a new <see cref="System.Uri">URI</see> using the address of an existing <see cref="System.Uri">URI</see>, 
        /// but with a different <see cref="System.Uri.Scheme">scheme</see>.
        /// </summary>
        /// <param name="uri">The <see cref="System.Uri">URI</see> form which the address will be taken. </param>
        /// <param name="scheme">The schema to be used for the new <see cref="System.Uri">URI</see>.</param>
        /// <returns>
        /// A new <see cref="System.Uri">URI</see> using the address of an existing <see cref="Uri">URI</see>, 
        /// that uses the <see cref="System.Uri.Scheme">scheme</see> provided by the <paramref name="scheme"/> parameter.
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

        /// <summary>
        /// Resolves an absolute <see cref="System.Uri">URI</see> instance based on the combination of a base 
        /// <see cref="System.Uri">URI</see> and a relative <see cref="System.Uri">URI</see>.
        /// </summary>
        /// <param name="uri">The base <see cref="System.Uri">URI</see></param>
        /// <param name="other">The relative <see cref="System.Uri">URI</see> to be combined with <paramref name="uri"/></param>
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
        /// The URI formed by combining <paramref name="uri" /> and <paramref name="other" /> is empty or contains only spaces.
        /// -or- 
        /// The scheme specified in the URI formed by combining <paramref name="uri" /> and <paramref name="other" /> is not valid.
        /// -or- 
        /// The URI formed by combining <paramref name="uri" /> and <paramref name="other" /> contains too many slashes.
        /// -or- 
        /// The password specified in the URI formed by combining <paramref name="uri" /> and <paramref name="other" /> is not valid.
        /// -or- 
        /// The host name specified in the URI formed by combining <paramref name="uri" /> and <paramref name="other" /> is not valid.
        /// -or- 
        /// The file name specified in the URI formed by combining <paramref name="uri" /> and <paramref name="other" /> is not valid.
        /// -or- 
        /// The user name specified in the URI formed by combining <paramref name="uri" /> and <paramref name="other" /> is not valid.
        /// -or- 
        /// The host or authority name specified in the URI formed by combining <paramref name="uri" /> and <paramref name="other" /> cannot be terminated by backslashes.
        /// -or- 
        /// The port number specified in the URI formed by combining <paramref name="uri" /> and <paramref name="other" /> is not valid or cannot be parsed.
        /// -or- 
        /// The length of the URI formed by combining <paramref name="uri" /> and <paramref name="other" /> exceeds 65519 characters.
        /// -or- 
        /// The length of the scheme specified in the URI formed by combining <paramref name="uri" /> and <paramref name="other" /> exceeds 1023 characters.
        /// -or- 
        /// There is an invalid character sequence in the URI formed by combining <paramref name="uri" /> and <paramref name="other" />.
        /// -or- 
        /// The MS-DOS path specified in <paramref name="uri" /> must start with <c>c:\</c>.
        /// </exception>
        public static System.Uri Resolve(this System.Uri uri, System.Uri other)
        {
            uri.VerifyArgument(nameof(uri)).IsNotNull();
            uri.VerifyArgument(nameof(other)).IsNotNull();

            if (other.IsAbsoluteUri)
            {
                return other;
            }
            if (uri.IsAbsoluteUri)
            {
                return new System.Uri(uri, other);
            }

            var val = uri.ToString();
            var oth = other.ToString();
            if (val.Length > 0 && val[val.Length - 1] == '/')
            {
                if (oth.Length >= 1 && oth[0] == '/')
                {
                    oth = oth.Substring(1);
                }
            } 
            else if (oth[0] != '/')
            {
                oth = '/' + oth;
            }
            return new System.Uri(val + oth, UriKind.Relative);
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
            return Resolve(uri, new System.Uri(other.VerifyArgument(nameof(other)).IsNotNull().IsNotEmpty().Value, UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        /// Gets a dictionary of key-value pairs representing the query parameters of the given <paramref name="uri"/>.
        /// </summary>
        /// <param name="uri">
        /// The <see cref="Uri"/> instance to get the query parameters from. 
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
            
			return uri.Query.TrimStart('?')
				.Split(new[] { '&', ';' }, StringSplitOptions.RemoveEmptyEntries)
				.Select(parameter => parameter.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries))
				.GroupBy(parts => parts[0], parts => parts.Length > 2 ? string.Join("=", parts, 1, parts.Length - 1) : (parts.Length > 1 ? parts[1] : ""), comparer)
                #if NET40_OR_NEWER
                .ToDictionary(grouping => grouping.Key, grouping => string.Join(",", grouping), comparer);
                #else
                .ToDictionary(grouping => grouping.Key, grouping => string.Join(",", grouping.ToArray()), comparer);
                #endif
        }
    }
}