using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Axle.Verification;


namespace Axle.Threading.Extensions.Tasks
{
	public static class TaskExtensions
    {
        #region ContinueWith(...)
        public static Task ContinueWith<T>(this Task<T> task, Action<T> action)
        {
            task.VerifyArgument(nameof(task)).IsNotNull();
            action.VerifyArgument(nameof(action)).IsNotNull();
            return task.ContinueWith(t => action(t.Result), TaskContinuationOptions.OnlyOnRanToCompletion);
        }
        public static Task ContinueWith<T>(this Task<T> task, Action<T> action, TaskContinuationOptions continuationOptions)
        {
            task.VerifyArgument(nameof(task)).IsNotNull();
            action.VerifyArgument(nameof(action)).IsNotNull();
            return task.ContinueWith(t => action(t.Result), TaskContinuationOptions.OnlyOnRanToCompletion|continuationOptions);
        }

        public static Task<TResult> ContinueWith<T, TResult>(this Task<T> task, Func<T, TResult> action)
        {
            task.VerifyArgument(nameof(task)).IsNotNull();
            action.VerifyArgument(nameof(action)).IsNotNull();
            return task.ContinueWith(t => action(t.Result), TaskContinuationOptions.OnlyOnRanToCompletion);
        }
        public static Task<TResult> ContinueWith<T, TResult>(this Task<T> task, Func<T, TResult> action, TaskContinuationOptions continuationOptions)
        {
            task.VerifyArgument(nameof(task)).IsNotNull();
            action.VerifyArgument(nameof(action)).IsNotNull();
            return task.ContinueWith(t => action(t.Result), TaskContinuationOptions.OnlyOnRanToCompletion|continuationOptions);
        }

        public static IEnumerable<Task> ContinueWith<T>(this IEnumerable<Task<T>> tasks, Action<T> action)
        {
            return tasks.VerifyArgument(nameof(tasks)).IsNotNull().Value.Select(x => x.ContinueWith(action));
        }
        public static IEnumerable<Task<TResult>> ContinueWith<T, TResult>(this IEnumerable<Task<T>> tasks, Func<T, TResult> action)
        {
            return tasks.VerifyArgument(nameof(tasks)).IsNotNull().Value.Select(x => x.ContinueWith(action));
        }
        #endregion

        #region Parallelize(...)
        /// <summary>
        /// Creates a collection of <see cref="Task">tasks</see> from the supplied collection of delegates.
        /// <remarks>
        /// The created tasks are not yet started. Use the <see cref="Start(System.Collections.Generic.IEnumerable{System.Threading.Tasks.Task})">Start</see>
        /// or the <see cref="RunSynchronously(System.Collections.Generic.IEnumerable{System.Threading.Tasks.Task})">RunSynchronously</see> methods upon the 
        /// resulting collection to execute them.
        /// </remarks>
        /// </summary>
        /// <param name="tasks">A collection of delegates to be executed in parallel.</param>
        /// <returns>
        /// A collection of <see cref="Task">tasks</see> created from each delegate supplied by the <paramref name="tasks"/> collection.
        /// </returns>
        public static IEnumerable<Task> Parallelize(this IEnumerable<Action> tasks)
        {
            return tasks.VerifyArgument(nameof(tasks)).IsNotNull().Value.Select(x => new Task(x));
        }
        public static IEnumerable<Task> Parallelize(this IEnumerable<Action> tasks, CancellationToken cancellationToken)
        {
            return tasks.VerifyArgument(nameof(tasks)).IsNotNull().Value.Select(x => new Task(x, cancellationToken));
        }
        public static IEnumerable<Task> Parallelize(this IEnumerable<Action<CancellationToken>> tasks, CancellationToken cancellationToken)
        {
            return tasks.VerifyArgument(nameof(tasks)).IsNotNull().Value.Select(x => new Task(() =>x(cancellationToken), cancellationToken));
        }
        public static IEnumerable<Task> Parallelize(this IEnumerable<Action> tasks, CancellationToken cancellationToken, TaskCreationOptions creationOptions)
        {
            return tasks.VerifyArgument(nameof(tasks)).IsNotNull().Value.Select(x => new Task(x, cancellationToken, creationOptions));
        }
        public static IEnumerable<Task> Parallelize(this IEnumerable<Action<CancellationToken>> tasks, CancellationToken cancellationToken, TaskCreationOptions creationOptions)
        {
            return tasks.VerifyArgument(nameof(tasks)).IsNotNull().Value.Select(x => new Task(() => x(cancellationToken), cancellationToken, creationOptions));
        }

        public static IEnumerable<Task<TResult>> Parallelize<TResult>(this IEnumerable<Func<TResult>> tasks)
        {
            return tasks.VerifyArgument(nameof(tasks)).IsNotNull().Value.Select(x => new Task<TResult>(x));
        }
        public static IEnumerable<Task<TResult>> Parallelize<TResult>(this IEnumerable<Func<TResult>> tasks, CancellationToken cancellationToken)
        {
            return tasks.VerifyArgument(nameof(tasks)).IsNotNull().Value.Select(x => new Task<TResult>(x, cancellationToken));
        }
        public static IEnumerable<Task<TResult>> Parallelize<TResult>(this IEnumerable<Func<CancellationToken, TResult>> tasks, CancellationToken cancellationToken)
        {
            return tasks.VerifyArgument(nameof(tasks)).IsNotNull().Value.Select(x => new Task<TResult>(() => x(cancellationToken), cancellationToken));
        }
        public static IEnumerable<Task<TResult>> Parallelize<TResult>(
            this IEnumerable<Func<TResult>> tasks, 
            CancellationToken cancellationToken, 
            TaskCreationOptions creationOptions)
        {
            return tasks.VerifyArgument(nameof(tasks)).IsNotNull().Value.Select(x => new Task<TResult>(x, cancellationToken, creationOptions));
        }
        public static IEnumerable<Task<TResult>> Parallelize<TResult>(
            this IEnumerable<Func<CancellationToken, TResult>> tasks,
            CancellationToken cancellationToken,
            TaskCreationOptions creationOptions)
        {
            return tasks.VerifyArgument(nameof(tasks)).IsNotNull().Value.Select(x => new Task<TResult>(() => x(cancellationToken), cancellationToken, creationOptions));
        }
        #endregion

        #region RunSynchronously(...)
        public static IEnumerable<Task> RunSynchronously(this IEnumerable<Task> tasks)
        {
            return tasks.VerifyArgument(nameof(tasks)).IsNotNull().Value.Select(x => { x.RunSynchronously(); return x; }).ToArray();
        }
        public static IEnumerable<Task> RunSynchronously(this IEnumerable<Task> tasks, TaskScheduler scheduler)
        {
            return tasks.VerifyArgument(nameof(tasks)).IsNotNull().IsNotNull().Value.Select(x => { x.RunSynchronously(scheduler); return x; }).ToArray();
        }
        public static IEnumerable<Task<TResult>> RunSynchronously<TResult>(this IEnumerable<Task<TResult>> tasks)
        {
            return tasks.VerifyArgument(nameof(tasks)).IsNotNull().Value.Select(x => { x.RunSynchronously(); return x; }).ToArray();
        }
        public static IEnumerable<Task<TResult>> RunSynchronously<TResult>(this IEnumerable<Task<TResult>> tasks, TaskScheduler scheduler)
        {
            return tasks.VerifyArgument(nameof(tasks)).IsNotNull().Value.Select(x => { x.RunSynchronously(scheduler); return x; }).ToArray();
        }
        public static IEnumerable<Task<TResult>> RunSynchronously<TResult>(this Task<TResult>[] tasks) => RunSynchronously(tasks as IEnumerable<Task<TResult>>);
        public static IEnumerable<Task<TResult>> RunSynchronously<TResult>(this Task<TResult>[] tasks, TaskScheduler scheduler)
        {
            return RunSynchronously(tasks as IEnumerable<Task<TResult>>, scheduler);
        }
        #endregion

        #region Start(...)
        public static IEnumerable<Task> Start(this IEnumerable<Task> tasks)
        {
            return tasks.VerifyArgument(nameof(tasks)).IsNotNull().Value.Select(x => { x.Start(); return x; }).ToArray();
        }
        public static IEnumerable<Task> Start(this IEnumerable<Task> tasks, TaskScheduler scheduler)
        {
            return tasks.VerifyArgument(nameof(tasks)).IsNotNull().IsNotNull().Value.Select(x => { x.Start(scheduler); return x; }).ToArray();
        }
        public static IEnumerable<Task<TResult>> Start<TResult>(this IEnumerable<Task<TResult>> tasks)
        {
            return tasks.VerifyArgument(nameof(tasks)).IsNotNull().Value.Select(x => { x.Start(); return x; }).ToArray();
        }
        public static IEnumerable<Task<TResult>> Start<TResult>(this IEnumerable<Task<TResult>> tasks, TaskScheduler scheduler)
        {
            return tasks.VerifyArgument(nameof(tasks)).IsNotNull().Value.Select(x => { x.Start(scheduler); return x; }).ToArray();
        }
        public static IEnumerable<Task<TResult>> Start<TResult>(this Task<TResult>[] tasks) => Start(tasks as IEnumerable<Task<TResult>>);
        public static IEnumerable<Task<TResult>> Start<TResult>(this Task<TResult>[] tasks, TaskScheduler scheduler)
        {
            return Start(tasks as IEnumerable<Task<TResult>>, scheduler);
        }
        #endregion

        #region WaitForAll(...)
        public static IEnumerable<Task> WaitForAll(this IEnumerable<Task> tasks)
        {
            var t = tasks.VerifyArgument(nameof(tasks)).IsNotNull().Value as Task[] ?? tasks.ToArray();
            if (t.Length > 0)
            {
                Task.WaitAll(t);
            }
            return t;
        }
        public static IEnumerable<Task<TResult>> WaitForAll<TResult>(this IEnumerable<Task<TResult>> tasks)
        {
            var t = tasks.VerifyArgument(nameof(tasks)).IsNotNull().Value as Task<TResult>[] ?? tasks.ToArray();
            if (t.Length > 0)
            {
                Task.WaitAll(t);
            }
            return t;
        }
        #endregion

        #region WaitForAny(...)
        public static IEnumerable<Task> WaitForAny(this IEnumerable<Task> tasks)
        {
            tasks.VerifyArgument(nameof(tasks)).IsNotNull();
            var t = tasks as Task[] ?? tasks.ToArray();
            if (t.Length > 0)
            {
                Task.WaitAny(t);
            }
            return t;
        }
        public static IEnumerable<Task<TResult>> WaitForAny<TResult>(this IEnumerable<Task<TResult>> tasks)
        {
            tasks.VerifyArgument(nameof(tasks)).IsNotNull();
            var t = tasks as Task<TResult>[] ?? tasks.ToArray();
            if (t.Length > 0)
            {
                Task.WaitAny(t);
            }
            return t;
        }
        #endregion
    }
}