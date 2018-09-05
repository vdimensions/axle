#if NETSTANDARD || NET20_OR_NEWER
namespace Axle.Environment
{
    /// <summary>
    /// Indicates the byte order ("endianness") in which data is stored in a computer architecture. 
    /// </summary>
    /// <seealso cref="System.BitConverter.IsLittleEndian" />
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    //[Maturity(CodeMaturity.Frozen)]
    public enum Endianness : byte
    {
        /// <summary>
        /// Represents the little endian computer architecture
        /// </summary>
        LittleEndian = 0,

        /// <summary>
        /// Represents the big endian computer architecture
        /// </summary>
        BigEndian = 1
    }
}
#endif