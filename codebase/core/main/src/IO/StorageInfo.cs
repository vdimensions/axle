#if NETFRAMEWORK || NETSTANDARD1_1_OR_NEWER
using System;
using System.IO;
using System.Linq;
using Axle.Extensions.Uri;
using Axle.Verification;

namespace Axle.IO
{
    /// <summary>
    /// A class to act as a wrapper of a filesystem storage. 
    /// </summary>
    public sealed class StorageInfo
    {
        /// <summary>
        /// Creates a new instance of the <see cref="StorageInfo"/> class, representing a drive or directory location.
        /// </summary>
        /// <param name="path">
        /// The path representing a directory location.
        /// </param>
        /// <returns>
        /// A <see cref="StorageInfo"/> instance, representing a drive or directory location.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="path"/> does not represent a directory location.
        /// </exception>
        public static StorageInfo FromPath(string path)
        {
            Verifier.IsTrue(
                StringVerifier.IsNotNullOrEmpty(Verifier.VerifyArgument(path, nameof(path))), 
                p =>
                {
                    try
                    {
                        return (File.GetAttributes(p) & FileAttributes.Directory) == FileAttributes.Directory;
                    }
                    catch (DirectoryNotFoundException)
                    {
                        //
                        // No exception is being raised if a non-existing directory is passed.
                        //
                        return true;
                    }
                },
                "The provided path is not a valid directory location. ");
            return FromDirectory(new DirectoryInfo(path));
        }

        /// <summary>
        /// Creates a new instance of the <see cref="StorageInfo"/> class, representing a drive or directory location.
        /// </summary>
        /// <param name="directory">
        /// A <see cref="DirectoryInfo"/> object to act as a storage.
        /// </param>
        /// <returns>
        /// A <see cref="StorageInfo"/> instance, representing a drive or directory location.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="directory"/> is <c>null</c>.
        /// </exception>
        public static StorageInfo FromDirectory(DirectoryInfo directory)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(directory, nameof(directory)));
            if (!directory.Exists)
            {
                directory.Create();
            }
            return new StorageInfo(directory);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="StorageInfo"/> class, representing a drive or directory location.
        /// </summary>
        /// <param name="uri">
        /// A <see cref="Uri"/> reference to a drive or directory to act as a storage space.
        /// </param>
        /// <returns>
        /// A <see cref="StorageInfo"/> instance, representing a drive or directory location.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="uri"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="uri"/> does not represent a directory location.
        /// </exception>
        public static StorageInfo FromUri(Uri uri)
        {
            UriVerifier.IsFile(Verifier.IsNotNull(Verifier.VerifyArgument(uri, nameof(uri))));
            return FromDirectory(new DirectoryInfo(uri.LocalPath));
        }

        private StorageInfo(DirectoryInfo root) => Directory = root;

        /// <summary>
        /// Creates a <see cref="StorageInfo"/> objects from the provided <paramref name="path"/>, relative to
        /// the current <see cref="StorageInfo"/> location.
        /// </summary>
        /// <param name="path">
        /// A path, relative to the current <see cref="StorageInfo"/> location.
        /// </param>
        /// <returns>
        /// A <see cref="StorageInfo"/> instance, representing the provided location.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="path"/> does not represent a directory location.
        /// </exception>
        public StorageInfo Resolve(string path)
        {
            return FromUri(UriExtensions.Resolve(Uri, path));
        }

        /// <summary>
        /// Gets a <see cref="StorageInfo"/> list based on the subdirectories
        /// found within the current <see cref="StorageInfo"/> location.
        /// </summary>
        /// <returns>
        /// A <see cref="StorageInfo"/> instances array, representing the subdirectories
        /// of the location represented by the current <see cref="StorageInfo"/>.
        /// </returns>
        public StorageInfo[] List() => Enumerable.ToArray(Enumerable.Select(Directory.GetDirectories(), d => new StorageInfo(d)));

        /// <summary>
        /// Opens a <see cref="FileStream"/> on the specified <paramref name="path"/>,
        /// with <see cref="FileMode.OpenOrCreate"/> mode and the specified access.
        /// </summary>
        /// <param name="path">
        /// A path to a file, relative to the location represented by the current <see cref="StorageInfo"/> object.
        /// </param>
        /// <param name="access">
        /// A <see cref="FileAccess"/> value that specifies the operations that can be performed on the file.
        /// </param>
        /// <returns>
        /// An unshared <see cref="FileStream"/> that provides access to the specified file with the specified <paramref name="access"/> options.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters
        /// as defined by <see cref="Path.GetInvalidPathChars"/>.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred while opening the file.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// The specified location points to a file that is read-only and access is not Read.
        /// -or-
        /// the specified location is a directory.
        /// -or-
        /// The caller does not have the required permission.
        /// </exception>
        public FileStream OpenFile(string path, FileAccess access)
        {
            var uri = UriExtensions.Resolve(Uri, path);
            var filePath = uri.LocalPath;
            return File.Open(filePath, FileMode.OpenOrCreate, access);
        }
        /// <summary>
        /// Opens a <see cref="FileStream"/> on the specified <paramref name="path"/>,
        /// with <see cref="FileMode.OpenOrCreate"/> mode and <see cref="FileAccess.ReadWrite"/> access.
        /// </summary>
        /// <param name="path">
        /// A path to a file, relative to the location represented by the current <see cref="StorageInfo"/> object.
        /// </param>
        /// <returns>
        /// An unshared <see cref="FileStream"/> that provides access to the specified file.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters
        /// as defined by <see cref="Path.GetInvalidPathChars"/>.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred while opening the file.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// The specified location points to a file that is read-only and access is not Read.
        /// -or-
        /// the specified location is a directory.
        /// -or-
        /// The caller does not have the required permission.
        /// </exception>
        public FileStream OpenFile(string path)
        {
            var uri = UriExtensions.Resolve(Uri, path);
            var filePath = uri.LocalPath;
            return File.Open(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }

        /// <summary>
        /// A <see cref="DirectoryInfo"/> reference to the represented by the current <see cref="StorageInfo"/> drive or directory.
        /// </summary>
        public DirectoryInfo Directory { get; }

        /// <summary>
        /// Gets a <see cref="bool"/> value determining whether the current directory exists.
        /// </summary>
        public bool Exists => Directory.Exists;

        /// <summary>
        /// Gets the <see cref="Uri"/> of the location represented by the current <see cref="StorageInfo"/> instance. 
        /// </summary>
        public Uri Uri => new Uri(Directory.FullName, UriKind.Absolute);
    }
}
#endif