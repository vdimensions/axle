using System;
using System.Diagnostics;

//using Axle.References;


namespace Axle.Verification
{
    /// <summary>
    /// A struct that represents a reference to an argument for a method or constructor. 
    /// The argument reference is usually represented by its name (as defined in the respective method/constructor) and the value passed to it. 
    /// </summary>
    public partial struct ArgumentReference<T>// : IReference<T>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string name;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly T value;

        [DebuggerStepThrough]
        internal ArgumentReference(string name, T value)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            this.name = name;
            this.value = value;
        }

        internal string Name { get { return name; } }
        /// <summary>
        /// Gets the value passed in the argument the current <see cref="ArgumentReference{T}"/> instance represents.
        /// </summary>
        public T Value { get { return value; } }
        //T IReference<T>.Value { get { return value; } }
        //object IReference.Value { get { return value; } }

        /// <param name="reference">
        /// The argument reference instance to be unwrapped by this operator. 
        /// </param>
        public static implicit operator T(ArgumentReference<T> reference) { return reference.value; }
    }
}