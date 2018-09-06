#if NETSTANDARD || NET35_OR_NEWER
using System;

namespace Axle.Accelerators
{
    internal sealed class Partial<TState, T1, T2, T>
    {
        public Partial(TState state, T1 arg, Func<T1, T2, T> operation)
        {
            State = state;
            Arg = arg;
            Operation = operation;
        }

        public T Invoke(T2 arg) => Operation.Invoke(Arg, arg);
        // TODO: decide what accelerator to use
        public Accelerator<T2, T> Apply(T2 arg) => new FAccelerator<T2, T>(arg, Invoke);

        public Func<T1, T2, T> Operation { get; }
        public T1 Arg { get; }
        public TState State { get; }
    }
    internal sealed class Partial<TState, T1, T2, T3, T>
    {
        public Partial(TState state, T1 arg, Func<T1, T2, T3, T> operation)
        {
            State = state;
            Arg = arg;
            Operation = operation;
        }

        public T Invoke(T2 arg1, T3 arg2) => Operation.Invoke(Arg, arg1, arg2);
        public Partial<TState, T2, T3, T> Apply(T2 arg) => new Partial<TState, T2, T3, T>(State, arg, Invoke);

        public Func<T1, T2, T3, T> Operation { get; }
        public T1 Arg { get; }
        public TState State { get; }
    }
    internal sealed class Partial<TState, T1, T2, T3, T4, T>
    {
        public Partial(TState state, T1 arg, Func<T1, T2, T3, T4, T> operation)
        {
            State = state;
            Arg = arg;
            Operation = operation;
        }

        public T Invoke(T2 arg1, T3 arg2, T4 arg3) => Operation.Invoke(Arg, arg1, arg2, arg3);
        public Partial<TState, T2, T3, T4, T> Apply(T2 arg) => new Partial<TState, T2, T3, T4, T>(State, arg, Invoke);

        public Func<T1, T2, T3, T4, T> Operation { get; }
        public T1 Arg { get; }
        public TState State { get; }
    }
    internal sealed class Partial<TState, T1, T2, T3, T4, T5, T>
    {
        public Partial(TState state, T1 arg, Func<T1, T2, T3, T4, T5, T> operation)
        {
            State = state;
            Arg = arg;
            Operation = operation;
        }

        public T Invoke(T2 arg1, T3 arg2, T4 arg3, T5 arg4) => Operation.Invoke(Arg, arg1, arg2, arg3, arg4);
        public Partial<TState, T2, T3, T4, T5, T> Apply(T2 arg) => new Partial<TState, T2, T3, T4, T5, T>(State, arg, Invoke);

        public Func<T1, T2, T3, T4, T5, T> Operation { get; }
        public T1 Arg { get; }
        public TState State { get; }
    }
    internal sealed class Partial<TState, T1, T2, T3, T4, T5, T6, T>
    {
        public Partial(TState state, T1 arg, Func<T1, T2, T3, T4, T5, T6, T> operation)
        {
            State = state;
            Arg = arg;
            Operation = operation;
        }

        public T Invoke(T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5) => Operation.Invoke(Arg, arg1, arg2, arg3, arg4, arg5);
        public Partial<TState, T2, T3, T4, T5, T6, T> Apply(T2 arg) => new Partial<TState, T2, T3, T4, T5, T6, T>(State, arg, Invoke);

        public Func<T1, T2, T3, T4, T5, T6, T> Operation { get; }
        public T1 Arg { get; }
        public TState State { get; }
    }
    internal sealed class Partial<TState, T1, T2, T3, T4, T5, T6, T7, T>
    {
        public Partial(TState state, T1 arg, Func<T1, T2, T3, T4, T5, T6, T7, T> operation)
        {
            State = state;
            Arg = arg;
            Operation = operation;
        }

        public T Invoke(T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, T7 arg6) => Operation.Invoke(Arg, arg1, arg2, arg3, arg4, arg5, arg6);
        public Partial<TState, T2, T3, T4, T5, T6, T7, T> Apply(T2 arg) => new Partial<TState, T2, T3, T4, T5, T6, T7, T>(State, arg, Invoke);

        public Func<T1, T2, T3, T4, T5, T6, T7, T> Operation { get; }
        public T1 Arg { get; }
        public TState State { get; }
    }
    internal sealed class Partial<TState, T1, T2, T3, T4, T5, T6, T7, T8, T>
    {
        public Partial(TState state, T1 arg, Func<T1, T2, T3, T4, T5, T6, T7, T8, T> operation)
        {
            State = state;
            Arg = arg;
            Operation = operation;
        }

        public T Invoke(T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, T7 arg6, T8 arg7) => Operation.Invoke(Arg, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        public Partial<TState, T2, T3, T4, T5, T6, T7, T8, T> Apply(T2 arg) => new Partial<TState, T2, T3, T4, T5, T6, T7, T8, T>(State, arg, Invoke);

        public Func<T1, T2, T3, T4, T5, T6, T7, T8, T> Operation { get; }
        public T1 Arg { get; }
        public TState State { get; }
    }
}
#endif