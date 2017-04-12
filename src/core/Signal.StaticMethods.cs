using System;

namespace Axle
{
    public partial class Signal
    {
        public static Signal Create(Action action)
        {
            return new Signal(() => action, x => action = x);
        }
        public static Signal Create(Func<Action> trigger, Func<Action, Action> updateFn)
        {
            return new Signal(trigger, updateFn);
        }

        public static Signal<T> Create<T>(Action<T> action)
        {
            return new Signal<T>(() => action, x => action = x);
        }
        public static Signal<T> Create<T>(Func<Action<T>> trigger, Func<Action<T>, Action<T>> updateFn)
        {
            return new Signal<T>(trigger, updateFn);
        }

        public static Signal<T1, T2> Create<T1, T2>(Action<T1, T2> action)
        {
            return new Signal<T1, T2>(() => action, x => action = x);
        }
        public static Signal<T1, T2> Create<T1, T2>(Func<Action<T1, T2>> trigger, Func<Action<T1, T2>, Action<T1, T2>> updateFn)
        {
            return new Signal<T1, T2>(trigger, updateFn);
        }
    }
}

