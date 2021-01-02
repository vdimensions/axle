using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Axle.Verification;

namespace Axle.Text
{
    /// <summary>
    /// An abstract class representing a character sequence. A character sequence has the same properties
    /// as a <see cref="string"/>, but is not necessarily a string wrapper, it may also represent a collection
    /// of characters, such as a <see cref="char">character</see> array.
    /// </summary>
    /// <seealso cref="StringCharSequence"/>
    /// <seealso cref="EnumerableCharSequence"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
    [System.Serializable]
    #endif
    [StructLayout((LayoutKind.Sequential))]
    public abstract class CharSequence : IEnumerable<char>
    {
        /// <summary>
        /// Creates a <see cref="StringCharSequence"/> instance from the provided <see cref="string"/>
        /// <paramref name="value"/>.
        /// </summary>
        /// <param name="value">
        /// The <see cref="string"/> value to use as a source for the newly created <see cref="CharSequence"/>.
        /// </param>
        /// <returns>
        /// A <see cref="StringCharSequence"/> instance from the provided <see cref="string"/>
        /// <paramref name="value"/>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="value"/> is <c>null</c>.
        /// </exception>
        public static CharSequence Create(string value)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(value, nameof(value)));
            return new StringCharSequence(value);
        }
        /// <summary>
        /// Creates an <see cref="CharSequence"/> instance from the provided <see cref="char">character</see> array
        /// <paramref name="value"/>.
        /// </summary>
        /// <param name="value">
        /// The <see cref="char">character</see> array value to use as a source for the newly created
        /// <see cref="CharSequence"/>.
        /// </param>
        /// <returns>
        /// An <see cref="CharSequence"/> instance from the provided <see cref="char">character</see> array
        /// <paramref name="value"/>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="value"/> is <c>null</c>.
        /// </exception>
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static CharSequence Create(IEnumerable<char> value)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(value, nameof(value)));
            #if NETSTANDARD1_0
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (((object) value) is string str)
            #else
            if (value is string str)
            #endif
            {
                return new StringCharSequence(str);
            }
            return new EnumerableCharSequence(value);
        }
        
        /// <summary>
        /// Gets a <see cref="CharSequence"/> instance that represents an empty value.
        /// This is analogous to the <see cref="string.Empty"/> value of the <see cref="string"/> type.
        /// </summary>
        public static readonly CharSequence Empty = new StringCharSequence(string.Empty);

        /// <summary>
        /// Converts the provided <see cref="CharSequence"/> instance to a <see cref="string"/> value;
        /// </summary>
        /// <param name="charSequence">
        /// The <see cref="CharSequence"/> instance to convert to a <see cref="string"/>.
        /// </param>
        /// <returns>
        /// A <see cref="string"/> value produced from the provided <paramref name="charSequence"/>. 
        /// </returns>
        public static explicit operator string(CharSequence charSequence) => charSequence?.ToString();
        /// <summary>
        /// Converts the provided <see cref="string"/> <paramref name="value"/> to a <see cref="CharSequence"/>
        /// instance.
        /// </summary>
        /// <param name="value">
        /// The <see cref="string"/> value to convert.
        /// </param>
        /// <returns>
        /// A <see cref="StringCharSequence"/> produced from the provided <see cref="string"/> <paramref name="value"/>.
        /// </returns>
        public static implicit operator CharSequence(string value) 
            => value != null ? new StringCharSequence(value) : null;
        /// <summary>
        /// Converts the provided <see cref="char">character</see> array <paramref name="value"/> to a
        /// <see cref="CharSequence"/> instance.
        /// </summary>
        /// <param name="value">
        /// The <see cref="char">character</see> array value to convert.
        /// </param>
        /// <returns>
        /// An <see cref="EnumerableCharSequence"/> produced from the provided <see cref="char">character</see> array
        /// <paramref name="value"/>.
        /// </returns>
        public static implicit operator CharSequence(char[] value) 
            => value != null ? new EnumerableCharSequence(value) : null;
        
        internal CharSequence() { }

        /// <inheritdoc />
        public abstract IEnumerator<char> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        /// <summary>
        /// Gets the number of characters in the current <see cref="CharSequence"/> object.
        /// </summary>
        public abstract int Length { get; }
        
        /// <summary>
        /// Gets the <see cref="char">character</see> at a specified position in the current
        /// <see cref="CharSequence"/> instance.
        /// </summary>
        /// <param name="index">
        /// </param>
        /// <exception cref="System.IndexOutOfRangeException">
        /// <paramref name="index"/> is greater than or equal to the
        /// <see cref="Length">length</see> of this object, or less than zero.
        /// </exception>
        public abstract char this[int index] { get; }
    }
}