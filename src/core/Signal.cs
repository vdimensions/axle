using System;

using Axle.Verification;

namespace Axle
{
    /// <summary>
    /// A class that represents a signal object; that is, a wrapper arround a delegate (usually of type <see cref="Action"/>), 
    /// which enables special event subscription options around that delegate.
    /// </summary>
    public partial class Signal : IDisposable
    {
        public static Signal operator +(Signal signal, Action action)
        {
            signal.Subscribe(action);
            return signal;
        }
        public static Signal operator -(Signal signal, Action action)
        {
            signal.Unsubscribe(action);
            return signal;
        }
        public static Signal operator ^(Signal signal, Action action)
        {
            signal.SubscribeOnce(action);
            return signal;
        }

        private readonly Func<Action> dereference;
        private readonly Func<Action, Action> update;

        private Signal(Func<Action> trigger, Func<Action, Action> updateFn)
        {
            this.dereference = trigger;
            this.update = updateFn;
        }

        public void Subscribe(Action action)
        {
            if (action != null)
            {
                update(dereference() + action);
            }
        }
        public void SubscribeOnce(Action action)
        {
            if (action != null)
            {
                var realAction = new Action[] { null };
                realAction[0] = () =>
                {
                    try
                    {
                        action();
                    }
                    finally
                    {
                        update(dereference() - realAction[0]);
                    }
                };
                update(dereference() + realAction[0]);
            }
        }
        public void SubscribeWhile(Action action, Func<bool> predicate)
        {
            predicate.VerifyArgument("predicate").IsNotNull();
            if (action != null)
            {
                var realAction = new Action[] { null };
                realAction[0] = () =>
                {
                    try
                    {
                        action();
                    }
                    finally
                    {
                        if (!predicate())
                        {
                            update(dereference() - realAction[0]);
                        }
                    }
                };
                update(dereference() + realAction[0]);
            }
        }
        public void Unsubscribe(Action action)
        {
            if (action != null)
            {
                update(dereference() - action);
            }
        }

        public void Dispose(bool disposing)
        {
            update(null);
        }
        void IDisposable.Dispose() { Dispose(true); }
    }
    public class Signal<T> : IDisposable
    {
        public static Signal<T> operator +(Signal<T> signal, Action<T> action)
        {
            signal.Subscribe(action);
            return signal;
        }
        public static Signal<T> operator -(Signal<T> signal, Action<T> action)
        {
            signal.Unsubscribe(action);
            return signal;
        }
        public static Signal<T> operator ^(Signal<T> signal, Action<T> action)
        {
            signal.SubscribeOnce(action);
            return signal;
        }
        private readonly Func<Action<T>> dereference;
        private readonly Func<Action<T>, Action<T>> update;

        internal Signal(Func<Action<T>> trigger, Func<Action<T>, Action<T>> updateFn)
        {
            this.dereference = trigger;
            this.update = updateFn;
        }

        public void Subscribe(Action<T> action)
        {
            if (action != null)
            {
                update(dereference() + action);
            }
        }
        public void SubscribeOnce(Action<T> action)
        {
            if (action != null)
            {
                var realAction = new Action<T>[] { null };
                realAction[0] = (t) =>
                {
                    try
                    {
                        action(t);
                    }
                    finally
                    {
                        update(dereference() - realAction[0]);
                    }
                };
                update(dereference() + realAction[0]);
            }
        }
        public void SubscribeWhile(Action<T> action, Func<T, bool> predicate)
        {
            predicate.VerifyArgument("predicate").IsNotNull();
            if (action != null)
            {
                var realAction = new Action<T>[] { null };
                realAction[0] = t =>
                {
                    try
                    {
                        action(t);
                    }
                    finally
                    {
                        if (!predicate(t))
                        {
                            update(dereference() - realAction[0]);
                        }
                    }
                };
                update(dereference() + realAction[0]);
            }
        }
        public void Unsubscribe(Action<T> action)
        {
            if (action != null)
            {
                update(dereference() - action);
            }
        }

        public void Dispose(bool disposing)
        {
            update(null);
        }
        void IDisposable.Dispose() { Dispose(true); }
    }
    public class Signal<T1, T2> : IDisposable
    {
        public static Signal<T1, T2> operator +(Signal<T1, T2> signal, Action<T1, T2> action)
        {
            signal.Subscribe(action);
            return signal;
        }
        public static Signal<T1, T2> operator -(Signal<T1, T2> signal, Action<T1, T2> action)
        {
            signal.Unsubscribe(action);
            return signal;
        }
        public static Signal<T1, T2> operator ^(Signal<T1, T2> signal, Action<T1, T2> action)
        {
            signal.SubscribeOnce(action);
            return signal;
        }

        private readonly Func<Action<T1, T2>> defererence;
        private readonly Func<Action<T1, T2>, Action<T1, T2>> update;

        public Signal(Func<Action<T1, T2>> trigger, Func<Action<T1, T2>, Action<T1, T2>> updateFn)
        {
            this.defererence = trigger;
            this.update = updateFn;
        }

        public void Subscribe(Action<T1, T2> action)
        {
            if (action != null)
            {
                update(defererence() + action);
            }
        }
        public void SubscribeOnce(Action<T1, T2> action)
        {
            if (action != null)
            {
                var realAction = new Action<T1, T2>[] { null };
                realAction[0] = (t1, t2) =>
                {
                    try
                    {
                        action(t1, t2);
                    }
                    finally
                    {
                        update(defererence() - realAction[0]);
                    }
                };
                update(defererence() + realAction[0]);
            }
        }
        public void SubscribeWhile(Action<T1, T2> action, Func<T1, T2, bool> predicate)
        {
            predicate.VerifyArgument("predicate").IsNotNull();
            if (action != null)
            {
                var realAction = new Action<T1, T2>[] { null };
                realAction[0] = (t1, t2) =>
                {
                    try
                    {
                        action(t1, t2);
                    }
                    finally
                    {
                        if (!predicate(t1, t2))
                        {
                            update(defererence() - realAction[0]);
                        }
                    }
                };
                update(defererence() + realAction[0]);
            }
        }
        public void Unsubscribe(Action<T1, T2> action)
        {
            if (action != null)
            {
                update(defererence() - action);
            }
        }

        public void Dispose(bool disposing)
        {
            update(null);
        }
        void IDisposable.Dispose() { Dispose(true); }
    }
}
