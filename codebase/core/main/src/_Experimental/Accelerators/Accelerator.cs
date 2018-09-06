#if NETSTANDARD || NET35_OR_NEWER
using System;
using System.Threading.Tasks;


namespace Axle.Accelerators
{
    internal abstract class Accelerator<T, TResult>
    {
        protected Accelerator(T arg, Func<T, TResult> operation)
        {
            Arg = arg;
            Operation = operation;
        }

        protected virtual TResult Invoke()
        {
            return Operation.Invoke(Arg);
        }

        public T Arg { get; }
        public Func<T, TResult> Operation { get; }
    }
    internal sealed class FAccelerator<T, TResult> : Accelerator<T, TResult>
    {
        public FAccelerator(T arg, Func<T, TResult> operation) : base(arg, operation)
        {
        }
    }
    //internal sealed class PAccelerator<T, TResult> : Accelerator<T, TResult>
    //{
    //    private Task<TResult> _task;
    //
    //    public PAccelerator(T arg, Func<T, TResult> operation) : base(arg, operation)
    //    {
    //    }
    //
    //    protected override TResult Invoke()
    //    {
    //        _task = Task<TResult>.Factory.StartNew(base.Invoke);
    //    }
    //}
    public static class Accelerator
    {
        //internal Accelerator<T2, TResult> Reduce<T1, T2, TResult>(Accelerator<T1, T2, TResult> acc)
        //{
        //    switch (acc)
        //    {
        //        case F
        //    }
        //}
    }
}
#endif