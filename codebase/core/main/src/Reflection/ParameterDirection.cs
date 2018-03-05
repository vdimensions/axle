using System;


namespace Axle.Reflection
{
    /// <summary>
    /// An enumeration representing the possible parameter directions.
    /// </summary>
    [Flags]
    #if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
    [Serializable]
    #endif
    public enum ParameterDirection
    {
        /// <summary>
        /// The parameter direction could not be determined. 
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// A flag used to mark an input parameter. 
        /// </summary>
        /// <seealso cref="System.Reflection.ParameterInfo.IsIn"/>
        Input = 1,

        /// <summary>
        /// A flag used to mark an output parameter. 
        /// </summary>
        /// <seealso cref="System.Reflection.ParameterInfo.IsOut"/>
        Output = 2,

        /// <summary>
        /// A flag used to mark a return value. 
        /// </summary>
        /// <seealso cref="System.Reflection.ParameterInfo.IsRetval"/>
        ReturnValue = 4,

        /// <summary>
        /// A flag used to represent a parameter which is both an input and an output one. 
        /// </summary>
        InputOutput = Input|Output
    }
}