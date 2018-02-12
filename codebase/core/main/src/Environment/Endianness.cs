namespace Axle.Environment
{
    /// <summary>
    /// Indicates the byte order ("endianess") in which data is stored in a computer architecture. 
    /// </summary>
    /// <seealso cref="System.BitConverter.IsLittleEndian" />
    #if !NETSTANDARD
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
