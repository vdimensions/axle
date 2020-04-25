using System;

namespace Axle.Text.Documents.Binding
{
    /// <summary>
    /// A binding converter is an object which is used by a <see cref="IBinder"/>
    /// to convert a raw <see cref="string"/> value to an instance of a specified type.
    /// </summary>
    public interface IBindingConverter
    {
        /// <summary>
        /// Attempts to convert a given <paramref name="rawValue"/> string parameter to
        /// the specified <paramref name="type"/>.
        /// </summary>
        /// <param name="rawValue">
        /// The <see cref="string"/> value that will be converted.
        /// </param>
        /// <param name="type">
        /// The target type of the conversion.
        /// </param>
        /// <param name="boundValue">
        /// An output parameter representing the converted value.
        /// This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        /// <c><see langword="true"/></c> if the conversion was successful and the <paramref name="boundValue"/> was set;
        /// <c><see langword="false"/></c> otherwise.
        /// </returns>
        bool TryConvertMemberValue(string rawValue, Type type, out object boundValue);
    }
}
