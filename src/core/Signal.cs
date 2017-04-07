using System;

using Axle.Verification;

namespace Axle
{
    public class Signal
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

        private readonly Func<Action> trigger;
        private readonly Func<Action, Action> updateFn;

        private Signal(Func<Action> trigger, Func<Action, Action> updateFn)
        {
            this.trigger = trigger;
            this.updateFn = updateFn;
        }

        public void Subscribe(Action action)
        {
            if (action != null)
            {
                updateFn(trigger() + action);
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
                        updateFn(trigger() - realAction[0]);
                    }
                };
                updateFn(trigger() + realAction[0]);
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
                            updateFn(trigger() - realAction[0]);
                        }
                    }
                };
                updateFn(trigger() + realAction[0]);
            }
        }
        public void Unsubscribe(Action action)
        {
            if (action != null)
            {
                updateFn(trigger() - action);
            }
        }
    }
    public class Signal<T>
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
        private readonly Func<Action<T>> trigger;
        private readonly Func<Action<T>, Action<T>> updateFn;

        internal Signal(Func<Action<T>> trigger, Func<Action<T>, Action<T>> updateFn)
        {
            this.trigger = trigger;
            this.updateFn = updateFn;
        }

        public void Subscribe(Action<T> action)
        {
            if (action != null)
            {
                updateFn(trigger() + action);
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
                        updateFn(trigger() - realAction[0]);
                    }
                };
                updateFn(trigger() + realAction[0]);
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
                            updateFn(trigger() - realAction[0]);
                        }
                    }
                };
                updateFn(trigger() + realAction[0]);
            }
        }
        public void Unsubscribe(Action<T> action)
        {
            if (action != null)
            {
                updateFn(trigger() - action);
            }
        }
    }
    public class Signal<T1, T2>
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

        private readonly Func<Action<T1, T2>> trigger;
        private readonly Func<Action<T1, T2>, Action<T1, T2>> updateFn;

        public Signal(Func<Action<T1, T2>> trigger, Func<Action<T1, T2>, Action<T1, T2>> updateFn)
        {
            this.trigger = trigger;
            this.updateFn = updateFn;
        }

        public void Subscribe(Action<T1, T2> action)
        {
            if (action != null)
            {
                updateFn(trigger() + action);
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
                        updateFn(trigger() - realAction[0]);
                    }
                };
                updateFn(trigger() + realAction[0]);
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
                            updateFn(trigger() - realAction[0]);
                        }
                    }
                };
                updateFn(trigger() + realAction[0]);
            }
        }
        public void Unsubscribe(Action<T1, T2> action)
        {
            if (action != null)
            {
                updateFn(trigger() - action);
            }
        }
    }
}
