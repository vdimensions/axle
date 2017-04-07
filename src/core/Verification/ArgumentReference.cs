using System;
using System.Diagnostics;

//using Axle.References;


namespace Axle.Verification
{
    public struct ArgumentReference<T>// : IReference<T>
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

        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        private ArgumentReference<T> IsOfTypeUnchecked(Type expectedType)
        {
            var actualType = Value.GetType();
            if (!expectedType.IsAssignableFrom(actualType))
            {
                throw new ArgumentTypeMismatchException(expectedType, actualType, this.Name);
            }
            return this;
        }

        [DebuggerStepThrough]
        public ArgumentReference<T> IsOfType(Type expectedType)
        {
            return IsOfTypeUnchecked(expectedType.VerifyArgument("expectedType").IsNotNull());
        }
        [DebuggerStepThrough]
        public ArgumentReference<TExpected> IsOfType<TExpected>()
        {
            var arg = IsOfTypeUnchecked(typeof(TExpected));
            object val = arg.Value;
            return new ArgumentReference<TExpected>(arg.Name, (TExpected) val);
        }

        internal string Name { get { return name; } }
        public T Value { get { return value; } }
        //T IReference<T>.Value { get { return value; } }
        //object IReference.Value { get { return value; } }

        public static implicit operator T(ArgumentReference<T> reference) { return reference.value; }
    }
}