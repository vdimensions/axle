#if NETSTANDARD || NET35_OR_NEWER
using System;

using Axle.Verification;


namespace Axle
{
    /// <summary>
    /// A class that represents a signal object; that is, a wrapper around a delegate (usually of type <see cref="Action"/>), 
    /// which enables special event subscription options around that delegate.
    /// </summary>
    public partial class Signal : IDisposable
    {
        /// <summary>
        /// An operator that acts as a shortcut to the <see cref="Signal.Subscribe(Action)"/> method.
        /// </summary>
        /// <param name="signal">
        /// The <see cref="Signal"/> instance to subscribe to. 
        /// </param>
        /// <param name="action">
        /// The subscribing delegate. Can be <c>null</c>. Passing null will have no effect on the existing subscriptions. 
        /// </param>
        public static Signal operator +(Signal signal, Action action)
        {
            signal.Subscribe(action);
            return signal;
        }

        /// <summary>
        /// An operator that acts as a shortcut to the <see cref="Signal.Unsubscribe(Action)"/> method.
        /// </summary>
        /// <param name="signal">
        /// The <see cref="Signal"/> instance to unsubscribe from. 
        /// </param>
        /// <param name="action">
        /// The unsubscribing delegate. Can be <c>null</c>. Passing null will have no effect on the existing subscriptions. 
        /// </param>
        public static Signal operator -(Signal signal, Action action)
        {
            signal.Unsubscribe(action);
            return signal;
        }

        /// <summary>
        /// An operator that acts as a shortcut to the <see cref="Signal.SubscribeOnce(Action)"/> method.
        /// </summary>
        /// <param name="signal">
        /// The <see cref="Signal"/> instance to subscribe to. 
        /// </param>
        /// <param name="action">
        /// The subscribing delegate. Can be <c>null</c>. Passing null will have no effect on the existing subscriptions. 
        /// </param>
        public static Signal operator ^(Signal signal, Action action)
        {
            signal.SubscribeOnce(action);
            return signal;
        }

        private readonly Func<Action> _dereference;
        private readonly Func<Action, Action> _update;

        private Signal(Func<Action> dereference, Func<Action, Action> update)
        {
            _dereference = dereference;
            _update = update;
        }

        /// <summary>
        /// Adds the provided by the <paramref name="action"/> delegate as a subscriber to the current <see cref="Signal"/> instance.
        /// </summary>
        /// <param name="action">
        /// The subscribing delegate. Can be <c>null</c>. Passing null will have no effect on the existing subscriptions. 
        /// </param>
        public void Subscribe(Action action)
        {
            if (action != null)
            {
                _update(_dereference() + action);
            }
        }

        /// <summary>
        /// Adds the provided by the <paramref name="action"/> delegate as a subscriber to the current <see cref="Signal"/> instance.
        /// The delegate will be executed only once when the signal is triggered. 
        /// </summary>
        /// <param name="action">
        /// The subscribing delegate. Can be <c>null</c>. Passing null will have no effect on the existing subscriptions. 
        /// </param>
        public void SubscribeOnce(Action action)
        {
            if (action != null)
            {
                // TODO: keep a map between the original action and the actual wrapper action, so that unsubscription will work for subscribe once calls
                var realAction = new Action[] { null };
                realAction[0] = () =>
                {
                    try
                    {
                        action();
                    }
                    finally
                    {
                        _update(_dereference() - realAction[0]);
                    }
                };
                _update(_dereference() + realAction[0]);
            }
        }

        /// <summary>
        /// Adds the provided by the <paramref name="action"/> delegate as a subscriber to the current <see cref="Signal"/> instance.
        /// The delegate will be executed on each signal trigger as long as the provided by the <paramref name="predicate"/> evaluates to <c>true</c>. 
        /// </summary>
        /// <param name="action">
        /// The subscribing delegate. Can be <c>null</c>. Passing <c>null</c> will have no effect on the existing subscriptions. 
        /// </param>
        /// <param name="predicate">
        /// A predicate that determines whether the subscription delegate provided by the <paramref name="action"/> will be invoked upon triggering the signal. 
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="predicate"/> is <c>null</c>. 
        /// </exception>
        public void SubscribeWhile(Action action, Func<bool> predicate)
        {
            predicate.VerifyArgument(nameof(predicate)).IsNotNull();
            if (action != null)
            {
                // TODO: keep a map between the original action and the actual wrapper action, so that unsubscription will work for subscribe once calls
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
                            _update(_dereference() - realAction[0]);
                        }
                    }
                };
                _update(_dereference() + realAction[0]);
            }
        }

        /// <summary>
        /// Removes the provided delegate from any subscriptions to the current <see cref="Signal"/> instance
        /// </summary>
        /// <param name="action">
        /// The unsubscribing delegate. Can be <c>null</c>. Passing <c>null</c> value will cause no effect. 
        /// </param>
        public void Unsubscribe(Action action)
        {
            if (action != null)
            {
                _update(_dereference() - action);
            }
        }

        internal void Dispose(bool disposing)
        {
            _update(null);
        }
        void IDisposable.Dispose() => Dispose(true);
    }

    /// <summary>
    /// A class that represents a signal object; that is, a wrapper around a delegate (usually of type <see cref="Action{T}"/>), 
    /// which enables special event subscription options around that delegate.
    /// </summary>
    public class Signal<T> : IDisposable
    {
        /// <summary>
        /// An operator that acts as a shortcut to the <see cref="Signal{T}.Subscribe(Action{T})"/> method.
        /// </summary>
        /// <param name="signal">
        /// The <see cref="Signal{T}"/> instance to subscribe to. 
        /// </param>
        /// <param name="action">
        /// The subscribing delegate. Can be <c>null</c>. Passing null will have no effect on the existing subscriptions. 
        /// </param>
        public static Signal<T> operator +(Signal<T> signal, Action<T> action)
        {
            signal.Subscribe(action);
            return signal;
        }

        /// <summary>
        /// An operator that acts as a shortcut to the <see cref="Signal{T}.Unsubscribe(Action{T})"/> method.
        /// </summary>
        /// <param name="signal">
        /// The <see cref="Signal{T}"/> instance to unsubscribe from. 
        /// </param>
        /// <param name="action">
        /// The unsubscribing delegate. Can be <c>null</c>. Passing null will have no effect on the existing subscriptions. 
        /// </param>
        public static Signal<T> operator -(Signal<T> signal, Action<T> action)
        {
            signal.Unsubscribe(action);
            return signal;
        }

        /// <summary>
        /// An operator that acts as a shortcut to the <see cref="Signal{T}.SubscribeOnce(Action{T})"/> method.
        /// </summary>
        /// <param name="signal">
        /// The <see cref="Signal{T}"/> instance to subscribe to. 
        /// </param>
        /// <param name="action">
        /// The subscribing delegate. Can be <c>null</c>. Passing null will have no effect on the existing subscriptions. 
        /// </param>
        public static Signal<T> operator ^(Signal<T> signal, Action<T> action)
        {
            signal.SubscribeOnce(action);
            return signal;
        }

        private readonly Func<Action<T>> _dereference;
        private readonly Func<Action<T>, Action<T>> _update;

        internal Signal(Func<Action<T>> trigger, Func<Action<T>, Action<T>> updateFn)
        {
            _dereference = trigger;
            _update = updateFn;
        }

        /// <summary>
        /// Adds the provided by the <paramref name="action"/> delegate as a subscriber to the current <see cref="Signal{T}"/> instance.
        /// </summary>
        /// <param name="action">
        /// The subscribing delegate. Can be <c>null</c>. Passing null will have no effect on the existing subscriptions. 
        /// </param>
        public void Subscribe(Action<T> action)
        {
            if (action != null)
            {
                _update(_dereference() + action);
            }
        }

        /// <summary>
        /// Adds the provided by the <paramref name="action"/> delegate as a subscriber to the current <see cref="Signal{T}"/> instance.
        /// The delegate will be executed only once when the signal is triggered. 
        /// </summary>
        /// <param name="action">
        /// The subscribing delegate. Can be <c>null</c>. Passing null will have no effect on the existing subscriptions. 
        /// </param>
        public void SubscribeOnce(Action<T> action)
        {
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
                        _update(_dereference() - realAction[0]);
                    }
                };
                _update(_dereference() + realAction[0]);
            }
        }

        /// <summary>
        /// Adds the provided by the <paramref name="action"/> delegate as a subscriber to the current <see cref="Signal{T}"/> instance.
        /// The delegate will be executed on each signal trigger as long as the provided by the <paramref name="predicate"/> evaluates to <c>true</c>. 
        /// </summary>
        /// <param name="action">
        /// The subscribing delegate. Can be <c>null</c>. Passing null will have no effect on the existing subscriptions. 
        /// </param>
        /// <param name="predicate">
        /// A predicate that determines whether the subscription delegate provided by the <paramref name="action"/> will be invoked upon triggering the signal. 
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="predicate"/> is <c>null</c>. 
        /// </exception>
        public void SubscribeWhile(Action<T> action, Func<T, bool> predicate)
        {
            predicate.VerifyArgument(nameof(predicate)).IsNotNull();
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
                            _update(_dereference() - realAction[0]);
                        }
                    }
                };
                _update(_dereference() + realAction[0]);
            }
        }

        /// <summary>
        /// Removes the provided delegate from any subscriptions to the current <see cref="Signal{T}"/> instance
        /// </summary>
        /// <param name="action">
        /// The unsubscribing delegate. Can be <c>null</c>. Passing <c>null</c> value will cause no effect. 
        /// </param>
        public void Unsubscribe(Action<T> action)
        {
            if (action != null)
            {
                _update(_dereference() - action);
            }
        }

        internal void Dispose(bool disposing)
        {
            _update(null);
        }
        void IDisposable.Dispose() => Dispose(true);
    }

    /// <summary>
    /// A class that represents a signal object; that is, a wrapper around a delegate (usually of type <see cref="Action{T1,T2}"/>), 
    /// which enables special event subscription options around that delegate.
    /// </summary>
    public class Signal<T1, T2> : IDisposable
    {
        /// <summary>
        /// An operator that acts as a shortcut to the <see cref="Signal{T1,T2}.Subscribe(Action{T1,T2})"/> method.
        /// </summary>
        /// <param name="signal">
        /// The <see cref="Signal{T1,T2}"/> instance to subscribe to. 
        /// </param>
        /// <param name="action">
        /// The subscribing delegate. Can be <c>null</c>. Passing null will have no effect on the existing subscriptions. 
        /// </param>
        public static Signal<T1, T2> operator +(Signal<T1, T2> signal, Action<T1, T2> action)
        {
            signal.Subscribe(action);
            return signal;
        }

        /// <summary>
        /// An operator that acts as a shortcut to the <see cref="Signal{T1, T2}.Unsubscribe(Action{T1,T2})"/> method.
        /// </summary>
        /// <param name="signal">
        /// The <see cref="Signal{T1, T2}"/> instance to subscribe from. 
        /// </param>
        /// <param name="action">
        /// The unsubscribing delegate. Can be <c>null</c>. Passing null will have no effect on the existing subscriptions. 
        /// </param>
        public static Signal<T1, T2> operator -(Signal<T1, T2> signal, Action<T1, T2> action)
        {
            signal.Unsubscribe(action);
            return signal;
        }

        /// <summary>
        /// An operator that acts as a shortcut to the <see cref="Signal{T1, T2}.SubscribeOnce(Action{T1, T2})"/> method.
        /// </summary>
        /// <param name="signal">
        /// The <see cref="Signal{T1,T2}"/> instance to subscribe to. 
        /// </param>
        /// <param name="action">
        /// The subscribing delegate. Can be <c>null</c>. Passing null will have no effect on the existing subscriptions. 
        /// </param>
        public static Signal<T1, T2> operator ^(Signal<T1, T2> signal, Action<T1, T2> action)
        {
            signal.SubscribeOnce(action);
            return signal;
        }

        private readonly Func<Action<T1, T2>> _defererence;
        private readonly Func<Action<T1, T2>, Action<T1, T2>> _update;

        internal Signal(Func<Action<T1, T2>> trigger, Func<Action<T1, T2>, Action<T1, T2>> updateFn)
        {
            _defererence = trigger;
            _update = updateFn;
        }

        /// <summary>
        /// Adds the provided by the <paramref name="action"/> delegate as a subscriber to the current <see cref="Signal{T1, T2}"/> instance.
        /// </summary>
        /// <param name="action">
        /// The subscribing delegate. Can be <c>null</c>. Passing null will have no effect on the existing subscriptions. 
        /// </param>
        public void Subscribe(Action<T1, T2> action)
        {
            if (action != null)
            {
                _update(_defererence() + action);
            }
        }

        /// <summary>
        /// Adds the provided by the <paramref name="action"/> delegate as a subscriber to the current <see cref="Signal{T1, T2}"/> instance.
        /// The delegate will be executed only once when the signal is triggered. 
        /// </summary>
        /// <param name="action">
        /// The subscribing delegate. Can be <c>null</c>. Passing null will have no effect on the existing subscriptions. 
        /// </param>
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
                        _update(_defererence() - realAction[0]);
                    }
                };
                _update(_defererence() + realAction[0]);
            }
        }

        /// <summary>
        /// Adds the provided by the <paramref name="action"/> delegate as a subscriber to the current <see cref="Signal{T1, T1}"/> instance.
        /// The delegate will be executed on each signal trigger as long as the provided by the <paramref name="predicate"/> evaluates to <c>true</c>. 
        /// </summary>
        /// <param name="action">
        /// The subscribing delegate. Can be <c>null</c>. Passing null will have no effect on the existing subscriptions. 
        /// </param>
        /// <param name="predicate">
        /// A predicate that determines whether the subscription delegate provided by the <paramref name="action"/> will be invoked upon triggering the signal. 
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="predicate"/> is <c>null</c>. </para>
        /// </exception>
        public void SubscribeWhile(Action<T1, T2> action, Func<T1, T2, bool> predicate)
        {
            predicate.VerifyArgument(nameof(predicate)).IsNotNull();
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
                            _update(_defererence() - realAction[0]);
                        }
                    }
                };
                _update(_defererence() + realAction[0]);
            }
        }

        /// <summary>
        /// Removes the provided delegate from any subscriptions to the current <see cref="Signal{T1, T2}"/> instance.
        /// </summary>
        /// <param name="action">
        /// The unsubscribing delegate. Can be <c>null</c>. Passing <c>null</c> value will cause no effect. 
        /// </param>
        public void Unsubscribe(Action<T1, T2> action)
        {
            if (action != null)
            {
                _update(_defererence() - action);
            }
        }

        internal void Dispose(bool disposing)
        {
            _update(null);
        }
        void IDisposable.Dispose() => Dispose(true);
    }
}
#endif