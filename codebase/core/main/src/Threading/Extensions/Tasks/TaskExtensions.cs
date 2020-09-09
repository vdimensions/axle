#if NETSTANDARD || NET35_OR_NEWER
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Axle.Verification;

namespace Axle.Threading.Extensions.Tasks
{
    /// <summary>
    /// A static class containing extension methods to the <see cref="Task"/> <see cref="Task{TResult}"/> classes.
    /// </summary>
    public static class TaskExtensions
    {
        #region ContinueWith(...)
        /// <summary>
        /// Creates a continuation for the current <paramref name="task"/>.
        /// </summary>
        /// <remarks>
        /// The <see cref="TaskContinuationOptions.OnlyOnRanToCompletion"/> option is internally set by default.
        /// </remarks>
        /// <param name="task">
        /// The <see cref="Task{T}"/> instance to create a continuation for.
        /// </param>
        /// <param name="action">
        /// An action to run containing the continuation logic.
        /// When run, the delegate will be passed the completed task's result as an argument.
        /// </param>
        /// <typeparam name="T">
        /// The type of the <paramref name="task"/>'s result.
        /// </typeparam>
        /// <returns>
        /// A new continuation <see cref="Task"/>.
        /// </returns>
        public static Task ContinueWith<T>(this Task<T> task, Action<T> action)
        {
            task.VerifyArgument(nameof(task)).IsNotNull();
            action.VerifyArgument(nameof(action)).IsNotNull();
            return task.ContinueWith(t => action(t.Result), TaskContinuationOptions.OnlyOnRanToCompletion);
        }
        /// <summary>
        /// Creates a continuation that executes according the condition specified in
        /// <paramref name="continuationOptions"/>.
        /// </summary>
        /// <param name="task">
        /// The <see cref="Task{T}"/> instance to create a continuation for.
        /// </param>
        /// <param name="action">
        /// An action to run containing the continuation logic.
        /// When run, the delegate will be passed the completed task's result as an argument.
        /// </param>
        /// <param name="continuationOptions">
        /// Options for when the continuation is scheduled and how it behaves. This includes criteria,
        /// such as <see cref="TaskContinuationOptions.OnlyOnRanToCompletion"/>, as well as execution options, such as
        /// <see cref="TaskContinuationOptions.ExecuteSynchronously"/>.
        /// <para>
        /// <remarks>
        /// The <see cref="TaskContinuationOptions.OnlyOnRanToCompletion"/> option is internally enabled by default.
        /// </remarks>
        /// </para>
        /// </param>
        /// <typeparam name="T">
        /// The type of the <paramref name="task"/>'s result.
        /// </typeparam>
        /// <returns>
        /// A new continuation <see cref="Task"/>.
        /// </returns>
        public static Task ContinueWith<T>(this Task<T> task, Action<T> action, TaskContinuationOptions continuationOptions)
        {
            task.VerifyArgument(nameof(task)).IsNotNull();
            action.VerifyArgument(nameof(action)).IsNotNull();
            return task.ContinueWith(t => action(t.Result), TaskContinuationOptions.OnlyOnRanToCompletion|continuationOptions);
        }
        /// <summary>
        /// Creates a continuation for the current <paramref name="task"/>.
        /// </summary>
        /// <remarks>
        /// The <see cref="TaskContinuationOptions.OnlyOnRanToCompletion"/> option is internally set by default.
        /// </remarks>
        /// <param name="task">
        /// The <see cref="Task{T}"/> instance to create a continuation for.
        /// </param>
        /// <param name="continuationFunction">
        /// An function to run containing the continuation logic.
        /// When run, the delegate will be passed the completed task's result as an argument.
        /// </param>
        /// <typeparam name="T">
        /// The type of the <paramref name="task"/>'s result.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the result returned by the <paramref name="continuationFunction"/>.
        /// </typeparam>
        /// <returns>
        /// A new continuation <see cref="Task{T}"/>.
        /// </returns>
        public static Task<TResult> ContinueWith<T, TResult>(this Task<T> task, Func<T, TResult> continuationFunction)
        {
            task.VerifyArgument(nameof(task)).IsNotNull();
            continuationFunction.VerifyArgument(nameof(continuationFunction)).IsNotNull();
            return task.ContinueWith(t => continuationFunction(t.Result), TaskContinuationOptions.OnlyOnRanToCompletion);
        }
        /// <summary>
        /// Creates a continuation for the current <paramref name="task"/>.
        /// </summary>
        /// <remarks>
        /// The <see cref="TaskContinuationOptions.OnlyOnRanToCompletion"/> option is internally set by default.
        /// </remarks>
        /// <param name="task">
        /// The <see cref="Task{T}"/> instance to create a continuation for.
        /// </param>
        /// <param name="continuationFunction">
        /// An function to run containing the continuation logic.
        /// When run, the delegate will be passed the completed task's result as an argument.
        /// </param>
        /// <param name="continuationOptions">
        /// Options for when the continuation is scheduled and how it behaves. This includes criteria,
        /// such as <see cref="TaskContinuationOptions.OnlyOnRanToCompletion"/>, as well as execution options, such as
        /// <see cref="TaskContinuationOptions.ExecuteSynchronously"/>.
        /// <para>
        /// <remarks>
        /// The <see cref="TaskContinuationOptions.OnlyOnRanToCompletion"/> option is internally enabled by default.
        /// </remarks>
        /// </para>
        /// </param>
        /// <typeparam name="T">
        /// The type of the <paramref name="task"/>'s result.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the result returned by the <paramref name="continuationFunction"/>.
        /// </typeparam>
        /// <returns>
        /// A new continuation <see cref="Task{T}"/>.
        /// </returns>
        public static Task<TResult> ContinueWith<T, TResult>(this Task<T> task, Func<T, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
        {
            task.VerifyArgument(nameof(task)).IsNotNull();
            continuationFunction.VerifyArgument(nameof(continuationFunction)).IsNotNull();
            return task.ContinueWith(t => continuationFunction(t.Result), TaskContinuationOptions.OnlyOnRanToCompletion|continuationOptions);
        }

        /// <summary>
        /// Creates continuations for the current collection of <paramref name="tasks"/>.
        /// </summary>
        /// <remarks>
        /// The <see cref="TaskContinuationOptions.OnlyOnRanToCompletion"/> option is internally set by default.
        /// </remarks>
        /// <param name="tasks">
        /// The collection of <see cref="Task{T}"/> to create a continuations for.
        /// </param>
        /// <param name="action">
        /// An action to run containing the continuation logic.
        /// When run, the delegate will be passed the completed task's result as an argument.
        /// </param>
        /// <typeparam name="T">
        /// The type of the <paramref name="tasks"/>'s result.
        /// </typeparam>
        /// <returns>
        /// A collection of the new continuation <see cref="Task"/>s.
        /// </returns>
        public static IEnumerable<Task> ContinueWith<T>(this IEnumerable<Task<T>> tasks, Action<T> action)
        {
            tasks.VerifyArgument(nameof(tasks)).IsNotNull();
            return tasks.Select(x => x.ContinueWith(action));
        }
        /// <summary>
        /// Creates continuations for the current collection of <paramref name="tasks"/>.
        /// </summary>
        /// <remarks>
        /// The <see cref="TaskContinuationOptions.OnlyOnRanToCompletion"/> option is internally set by default.
        /// </remarks>
        /// <param name="tasks">
        /// The collection of <see cref="Task{T}"/> to create a continuations for.
        /// </param>
        /// <param name="continuationFunction">
        /// An function to run containing the continuation logic.
        /// When run, the delegate will be passed the completed task's result as an argument.
        /// </param>
        /// <typeparam name="T">
        /// The type of the <paramref name="tasks"/>'s result.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the result returned by the <paramref name="continuationFunction"/>.
        /// </typeparam>
        /// <returns>
        /// A collection of the new continuation <see cref="Task{T}"/>s.
        /// </returns>
        public static IEnumerable<Task<TResult>> ContinueWith<T, TResult>(this IEnumerable<Task<T>> tasks, Func<T, TResult> continuationFunction)
        {
            tasks.VerifyArgument(nameof(tasks)).IsNotNull();
            return tasks.Select(x => x.ContinueWith(continuationFunction));
        }
        #endregion

        #region Parallelize(...)
        /// <summary>
        /// Creates a collection of <see cref="Task">tasks</see> from the supplied collection of delegates.
        /// <remarks>
        /// The created tasks are not yet started. Use the
        /// <see cref="Start(System.Collections.Generic.IEnumerable{System.Threading.Tasks.Task})" /> or the
        /// <see cref="RunSynchronously(System.Collections.Generic.IEnumerable{System.Threading.Tasks.Task})" />
        /// methods upon the resulting collection to execute them.
        /// </remarks>
        /// </summary>
        /// <param name="tasks">
        /// A collection of delegates to be executed in parallel.
        /// </param>
        /// <returns>
        /// A collection of <see cref="Task">tasks</see> created from each delegate supplied by the
        /// <paramref name="tasks"/> collection.
        /// </returns>
        public static IEnumerable<Task> Parallelize(this IEnumerable<Action> tasks)
        {
            return tasks.VerifyArgument(nameof(tasks)).IsNotNull().Value.Select(x => new Task(x));
        }
        /// <summary>
        /// Creates a collection of <see cref="Task">tasks</see> from the supplied collection of delegates
        /// and <see cref="CancellationToken"/>.
        /// <remarks>
        /// The created tasks are not yet started. Use the
        /// <see cref="Start(System.Collections.Generic.IEnumerable{System.Threading.Tasks.Task})" /> or the
        /// <see cref="RunSynchronously(System.Collections.Generic.IEnumerable{System.Threading.Tasks.Task})" />
        /// methods upon the resulting collection to execute them.
        /// </remarks>
        /// </summary>
        /// <param name="tasks">
        /// A collection of delegates to be executed in parallel.
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> the returned tasks will observe.
        /// </param>
        /// <returns>
        /// A collection of <see cref="Task">tasks</see> created from each delegate supplied by the
        /// <paramref name="tasks"/> collection.
        /// </returns>
        public static IEnumerable<Task> Parallelize(this IEnumerable<Action> tasks, CancellationToken cancellationToken)
        {
            return tasks.VerifyArgument(nameof(tasks)).IsNotNull().Value.Select(x => new Task(x, cancellationToken));
        }
        /// <summary>
        /// Creates a collection of <see cref="Task">tasks</see> from the supplied collection of delegates
        /// and <see cref="CancellationToken"/>.
        /// <remarks>
        /// The created tasks are not yet started. Use the
        /// <see cref="Start(System.Collections.Generic.IEnumerable{System.Threading.Tasks.Task})" /> or the
        /// <see cref="RunSynchronously(System.Collections.Generic.IEnumerable{System.Threading.Tasks.Task})" />
        /// methods upon the resulting collection to execute them.
        /// </remarks>
        /// </summary>
        /// <param name="tasks">
        /// A collection of delegates to be executed in parallel.
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> the returned tasks will observe.
        /// </param>
        /// <returns>
        /// A collection of <see cref="Task">tasks</see> created from each delegate supplied by the
        /// <paramref name="tasks"/> collection.
        /// </returns>
        public static IEnumerable<Task> Parallelize(this IEnumerable<Action<CancellationToken>> tasks, CancellationToken cancellationToken)
        {
            return tasks.VerifyArgument(nameof(tasks)).IsNotNull().Value.Select(x => new Task(() =>x(cancellationToken), cancellationToken));
        }
        /// <summary>
        /// Creates a collection of <see cref="Task">tasks</see> from the supplied collection of delegates
        /// and <see cref="CancellationToken"/>.
        /// <remarks>
        /// The created tasks are not yet started. Use the
        /// <see cref="Start(System.Collections.Generic.IEnumerable{System.Threading.Tasks.Task})" /> or the
        /// <see cref="RunSynchronously(System.Collections.Generic.IEnumerable{System.Threading.Tasks.Task})" />
        /// methods upon the resulting collection to execute them.
        /// </remarks>
        /// </summary>
        /// <param name="tasks">
        /// A collection of delegates to be executed in parallel.
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> the returned tasks will observe.
        /// </param>
        /// <param name="creationOptions">
        /// The <see cref="TaskCreationOptions"/> used to customize the task behavior.
        /// </param>
        /// <returns>
        /// A collection of <see cref="Task">tasks</see> created from each delegate supplied by the
        /// <paramref name="tasks"/> collection.
        /// </returns>
        public static IEnumerable<Task> Parallelize(this IEnumerable<Action> tasks, CancellationToken cancellationToken, TaskCreationOptions creationOptions)
        {
            return tasks.VerifyArgument(nameof(tasks)).IsNotNull().Value.Select(x => new Task(x, cancellationToken, creationOptions));
        }
        /// <summary>
        /// Creates a collection of <see cref="Task">tasks</see> from the supplied collection of delegates
        /// and <see cref="CancellationToken"/>.
        /// <remarks>
        /// The created tasks are not yet started. Use the
        /// <see cref="Start(System.Collections.Generic.IEnumerable{System.Threading.Tasks.Task})" /> or the
        /// <see cref="RunSynchronously(System.Collections.Generic.IEnumerable{System.Threading.Tasks.Task})" />
        /// methods upon the resulting collection to execute them.
        /// </remarks>
        /// </summary>
        /// <param name="tasks">
        /// A collection of delegates to be executed in parallel.
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> the returned tasks will observe.
        /// </param>
        /// <param name="creationOptions">
        /// The <see cref="TaskCreationOptions"/> used to customize the task behavior.
        /// </param>
        /// <returns>
        /// A collection of <see cref="Task">tasks</see> created from each delegate supplied by the
        /// <paramref name="tasks"/> collection.
        /// </returns>
        public static IEnumerable<Task> Parallelize(this IEnumerable<Action<CancellationToken>> tasks, CancellationToken cancellationToken, TaskCreationOptions creationOptions)
        {
            return tasks.VerifyArgument(nameof(tasks)).IsNotNull().Value.Select(x => new Task(() => x(cancellationToken), cancellationToken, creationOptions));
        }
        /// <summary>
        /// Creates a collection of <see cref="Task">tasks</see> from the supplied collection of delegates.
        /// <remarks>
        /// The created tasks are not yet started. Use the
        /// <see cref="Start(System.Collections.Generic.IEnumerable{System.Threading.Tasks.Task})" /> or the
        /// <see cref="RunSynchronously(System.Collections.Generic.IEnumerable{System.Threading.Tasks.Task})" />
        /// methods upon the resulting collection to execute them.
        /// </remarks>
        /// </summary>
        /// <param name="tasks">
        /// A collection of delegates to be executed in parallel.
        /// </param>
        /// <returns>
        /// A collection of <see cref="Task">tasks</see> created from each delegate supplied by the
        /// <paramref name="tasks"/> collection.
        /// </returns>
        public static IEnumerable<Task<TResult>> Parallelize<TResult>(this IEnumerable<Func<TResult>> tasks)
        {
            return tasks.VerifyArgument(nameof(tasks)).IsNotNull().Value.Select(x => new Task<TResult>(x));
        }
        /// <summary>
        /// Creates a collection of <see cref="Task">tasks</see> from the supplied collection of delegates
        /// and <see cref="CancellationToken"/>.
        /// <remarks>
        /// The created tasks are not yet started. Use the
        /// <see cref="Start(System.Collections.Generic.IEnumerable{System.Threading.Tasks.Task})" /> or the
        /// <see cref="RunSynchronously(System.Collections.Generic.IEnumerable{System.Threading.Tasks.Task})" />
        /// methods upon the resulting collection to execute them.
        /// </remarks>
        /// </summary>
        /// <param name="tasks">
        /// A collection of delegates to be executed in parallel.
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> the returned tasks will observe.
        /// </param>
        /// <returns>
        /// A collection of <see cref="Task">tasks</see> created from each delegate supplied by the
        /// <paramref name="tasks"/> collection.
        /// </returns>
        public static IEnumerable<Task<TResult>> Parallelize<TResult>(this IEnumerable<Func<TResult>> tasks, CancellationToken cancellationToken)
        {
            return tasks.VerifyArgument(nameof(tasks)).IsNotNull().Value.Select(x => new Task<TResult>(x, cancellationToken));
        }
        /// <summary>
        /// Creates a collection of <see cref="Task">tasks</see> from the supplied collection of delegates
        /// and <see cref="CancellationToken"/>.
        /// <remarks>
        /// The created tasks are not yet started. Use the
        /// <see cref="Start(System.Collections.Generic.IEnumerable{System.Threading.Tasks.Task})" /> or the
        /// <see cref="RunSynchronously(System.Collections.Generic.IEnumerable{System.Threading.Tasks.Task})" />
        /// methods upon the resulting collection to execute them.
        /// </remarks>
        /// </summary>
        /// <param name="tasks">
        /// A collection of delegates to be executed in parallel.
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> the returned tasks will observe.
        /// </param>
        /// <returns>
        /// A collection of <see cref="Task">tasks</see> created from each delegate supplied by the
        /// <paramref name="tasks"/> collection.
        /// </returns>
        public static IEnumerable<Task<TResult>> Parallelize<TResult>(
            this IEnumerable<Func<CancellationToken, TResult>> tasks, 
            CancellationToken cancellationToken)
        {
            return tasks.VerifyArgument(nameof(tasks)).IsNotNull().Value.Select(x => new Task<TResult>(() => x(cancellationToken), cancellationToken));
        }
        /// <summary>
        /// Creates a collection of <see cref="Task">tasks</see> from the supplied collection of delegates
        /// and <see cref="CancellationToken"/>.
        /// <remarks>
        /// The created tasks are not yet started. Use the
        /// <see cref="Start(System.Collections.Generic.IEnumerable{System.Threading.Tasks.Task})" /> or the
        /// <see cref="RunSynchronously(System.Collections.Generic.IEnumerable{System.Threading.Tasks.Task})" />
        /// methods upon the resulting collection to execute them.
        /// </remarks>
        /// </summary>
        /// <param name="tasks">
        /// A collection of delegates to be executed in parallel.
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> the returned tasks will observe.
        /// </param>
        /// <param name="creationOptions">
        /// The <see cref="TaskCreationOptions"/> used to customize the task behavior.
        /// </param>
        /// <returns>
        /// A collection of <see cref="Task">tasks</see> created from each delegate supplied by the
        /// <paramref name="tasks"/> collection.
        /// </returns>
        public static IEnumerable<Task<TResult>> Parallelize<TResult>(
            this IEnumerable<Func<TResult>> tasks, 
            CancellationToken cancellationToken, 
            TaskCreationOptions creationOptions)
        {
            return tasks.VerifyArgument(nameof(tasks)).IsNotNull().Value.Select(x => new Task<TResult>(x, cancellationToken, creationOptions));
        }
        /// <summary>
        /// Creates a collection of <see cref="Task">tasks</see> from the supplied collection of delegates
        /// and <see cref="CancellationToken"/>.
        /// <remarks>
        /// The created tasks are not yet started. Use the
        /// <see cref="Start(System.Collections.Generic.IEnumerable{System.Threading.Tasks.Task})" /> or the
        /// <see cref="RunSynchronously(System.Collections.Generic.IEnumerable{System.Threading.Tasks.Task})" />
        /// methods upon the resulting collection to execute them.
        /// </remarks>
        /// </summary>
        /// <param name="tasks">
        /// A collection of delegates to be executed in parallel.
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> the returned tasks will observe.
        /// </param>
        /// <param name="creationOptions">
        /// The <see cref="TaskCreationOptions"/> used to customize the task behavior.
        /// </param>
        /// <returns>
        /// A collection of <see cref="Task">tasks</see> created from each delegate supplied by the
        /// <paramref name="tasks"/> collection.
        /// </returns>
        public static IEnumerable<Task<TResult>> Parallelize<TResult>(
            this IEnumerable<Func<CancellationToken, TResult>> tasks,
            CancellationToken cancellationToken,
            TaskCreationOptions creationOptions)
        {
            return tasks.VerifyArgument(nameof(tasks)).IsNotNull().Value.Select(x => new Task<TResult>(() => x(cancellationToken), cancellationToken, creationOptions));
        }
        #endregion

        #region RunSynchronously(...)
        /// <summary>
        /// Runs the current <paramref name="tasks"/> synchronously on the current task scheduler.
        /// </summary>
        /// <param name="tasks">
        /// The collection of <see cref="Task"/> objects to execute.
        /// </param>
        /// <returns>
        /// A collection of the running <see cref="Task"/>s.
        /// </returns>
        public static IEnumerable<Task> RunSynchronously(this IEnumerable<Task> tasks)
        {
            return tasks.VerifyArgument(nameof(tasks)).IsNotNull().Value.Select(x => { x.RunSynchronously(); return x; }).ToArray();
        }
        /// <summary>
        /// Runs the current <paramref name="tasks"/> synchronously on the provided task <paramref name="scheduler"/>.
        /// </summary>
        /// <param name="tasks">
        /// The collection of <see cref="Task"/> objects to execute.
        /// </param>
        /// <param name="scheduler">
        /// The <see cref="TaskScheduler"/> on which to attempt to run this task inline.
        /// </param>
        /// <returns>
        /// A collection of the running <see cref="Task"/>s.
        /// </returns>
        public static IEnumerable<Task> RunSynchronously(this IEnumerable<Task> tasks, TaskScheduler scheduler)
        {
            return tasks.VerifyArgument(nameof(tasks)).IsNotNull().IsNotNull().Value.Select(x => { x.RunSynchronously(scheduler); return x; }).ToArray();
        }
        /// <summary>
        /// Runs the current <paramref name="tasks"/> synchronously on the current task scheduler.
        /// </summary>
        /// <param name="tasks">
        /// The collection of <see cref="Task"/> objects to execute.
        /// </param>
        /// <returns>
        /// A collection of the running <see cref="Task"/>s.
        /// </returns>
        public static IEnumerable<Task<TResult>> RunSynchronously<TResult>(this IEnumerable<Task<TResult>> tasks)
        {
            return tasks.VerifyArgument(nameof(tasks)).IsNotNull().Value.Select(x => { x.RunSynchronously(); return x; }).ToArray();
        }
        /// <summary>
        /// Runs the current <paramref name="tasks"/> synchronously on the provided task <paramref name="scheduler"/>.
        /// </summary>
        /// <param name="tasks">
        /// The collection of <see cref="Task"/> objects to execute.
        /// </param>
        /// <param name="scheduler">
        /// The <see cref="TaskScheduler"/> on which to attempt to run this task inline.
        /// </param>
        /// <returns>
        /// A collection of the running <see cref="Task{T}"/>s.
        /// </returns>
        public static IEnumerable<Task<TResult>> RunSynchronously<TResult>(this IEnumerable<Task<TResult>> tasks, TaskScheduler scheduler)
        {
            return tasks.VerifyArgument(nameof(tasks)).IsNotNull().Value.Select(x => { x.RunSynchronously(scheduler); return x; }).ToArray();
        }
        /// <summary>
        /// Runs the current <paramref name="tasks"/> synchronously on the current task scheduler.
        /// </summary>
        /// <param name="tasks">
        /// The collection of <see cref="Task"/> objects to execute.
        /// </param>
        /// <returns>
        /// A collection of the running <see cref="Task"/>s.
        /// </returns>
        public static IEnumerable<Task<TResult>> RunSynchronously<TResult>(this Task<TResult>[] tasks) => RunSynchronously(tasks as IEnumerable<Task<TResult>>);
        /// <summary>
        /// Runs the current <paramref name="tasks"/> synchronously on the provided task <paramref name="scheduler"/>.
        /// </summary>
        /// <param name="tasks">
        /// The collection of <see cref="Task"/> objects to execute.
        /// </param>
        /// <param name="scheduler">
        /// The <see cref="TaskScheduler"/> on which to attempt to run this task inline.
        /// </param>
        /// <returns>
        /// A collection of the running <see cref="Task"/>s.
        /// </returns>
        public static IEnumerable<Task<TResult>> RunSynchronously<TResult>(this Task<TResult>[] tasks, TaskScheduler scheduler)
        {
            return RunSynchronously(tasks as IEnumerable<Task<TResult>>, scheduler);
        }
        #endregion

        #region Start(...)
        /// <summary>
        /// Starts the collection of <paramref name="tasks"/>, scheduling it for execution to the default task
        /// scheduler.
        /// </summary>
        /// <param name="tasks">
        /// The collection of tasks to run.
        /// </param>
        /// <returns>
        /// A collection of the running <see cref="Task"/>s.
        /// </returns>
        public static IEnumerable<Task> Start(this IEnumerable<Task> tasks)
        {
            tasks.VerifyArgument(nameof(tasks)).IsNotNull();
            return tasks.Select(x => { x.Start(); return x; }).ToArray();
        }
        /// <summary>
        /// Starts the collection of <paramref name="tasks"/>, scheduling it for execution to the specified task
        /// <paramref name="scheduler"/>.
        /// </summary>
        /// <param name="tasks">
        /// The collection of tasks to run.
        /// </param>
        /// <param name="scheduler">
        /// The <see cref="TaskScheduler"/> with which to associate and execute the <paramref name="tasks"/>.
        /// </param>
        /// <returns>
        /// A collection of the running <see cref="Task"/>s.
        /// </returns>
        public static IEnumerable<Task> Start(this IEnumerable<Task> tasks, TaskScheduler scheduler)
        {
            tasks.VerifyArgument(nameof(tasks)).IsNotNull().IsNotNull();
            return tasks.Select(x => { x.Start(scheduler); return x; }).ToArray();
        }
        /// <summary>
        /// Starts the collection of <paramref name="tasks"/>, scheduling it for execution to the default task
        /// scheduler.
        /// </summary>
        /// <param name="tasks">
        /// The collection of tasks to run.
        /// </param>
        /// <returns>
        /// A collection of the running <see cref="Task"/>s.
        /// </returns>
        public static IEnumerable<Task<TResult>> Start<TResult>(this IEnumerable<Task<TResult>> tasks)
        {
            tasks.VerifyArgument(nameof(tasks)).IsNotNull();
            return tasks.Select(x => { x.Start(); return x; }).ToArray();
        }
        /// <summary>
        /// Starts the collection of <paramref name="tasks"/>, scheduling it for execution to the specified task
        /// <paramref name="scheduler"/>.
        /// </summary>
        /// <param name="tasks">
        /// The collection of tasks to run.
        /// </param>
        /// <param name="scheduler">
        /// The <see cref="TaskScheduler"/> with which to associate and execute the <paramref name="tasks"/>.
        /// </param>
        /// <returns>
        /// A collection of the running <see cref="Task"/>s.
        /// </returns>
        public static IEnumerable<Task<TResult>> Start<TResult>(this IEnumerable<Task<TResult>> tasks, TaskScheduler scheduler)
        {
            tasks.VerifyArgument(nameof(tasks)).IsNotNull();
            return tasks.Select(x => { x.Start(scheduler); return x; }).ToArray();
        }
        /// <summary>
        /// Starts the collection of <paramref name="tasks"/>, scheduling it for execution to the default task
        /// scheduler.
        /// </summary>
        /// <param name="tasks">
        /// The collection of tasks to run.
        /// </param>
        /// <returns>
        /// A collection of the running <see cref="Task"/>s.
        /// </returns>
        public static IEnumerable<Task<TResult>> Start<TResult>(this Task<TResult>[] tasks) => Start(tasks as IEnumerable<Task<TResult>>);
        /// <summary>
        /// Starts the collection of <paramref name="tasks"/>, scheduling it for execution to the specified task
        /// <paramref name="scheduler"/>.
        /// </summary>
        /// <param name="tasks">
        /// The collection of tasks to run.
        /// </param>
        /// <param name="scheduler">
        /// The <see cref="TaskScheduler"/> with which to associate and execute the <paramref name="tasks"/>.
        /// </param>
        /// <returns>
        /// A collection of the running <see cref="Task"/>s.
        /// </returns>
        public static IEnumerable<Task<TResult>> Start<TResult>(this Task<TResult>[] tasks, TaskScheduler scheduler)
        {
            return Start(tasks as IEnumerable<Task<TResult>>, scheduler);
        }
        #endregion

        #region WaitForAll(...)
        /// <summary>
        /// Waits for all of the provided <see cref="Task"/> objects to complete execution.
        /// </summary>
        /// <param name="tasks">
        /// An collection of <see cref="Task"/> instances on which to wait.
        /// </param>
        /// <returns>
        /// A collection of <see cref="Task"/> representing the completed tasks.
        /// </returns>
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static IEnumerable<Task> WaitForAll(this IEnumerable<Task> tasks)
        {
            var t = tasks.VerifyArgument(nameof(tasks)).IsNotNull().Value as Task[] ?? tasks.ToArray();
            if (t.Length > 0)
            {
                Task.WaitAll(t);
            }
            return t;
        }
        /// <summary>
        /// Waits for all of the provided <see cref="Task{TResult}"/> objects to complete execution.
        /// </summary>
        /// <param name="tasks">
        /// An collection of <see cref="Task{TResult}"/> instances on which to wait.
        /// </param>
        /// <typeparam name="TResult">
        /// The type of result a particular task from the <paramref name="tasks"/> would produce.
        /// </typeparam>
        /// <returns>
        /// A collection of <see cref="Task{TResult}"/> representing the completed tasks.
        /// </returns>
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
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
        public static Task WaitForAny(this IEnumerable<Task> tasks)
        {
            tasks.VerifyArgument(nameof(tasks)).IsNotNull();
            var t = tasks as Task[] ?? tasks.ToArray();
            if (t.Length > 0)
            {
                var index = Task.WaitAny(t);
                return t[index];
            }
            return null;
        }
        public static Task<TResult> WaitForAny<TResult>(this IEnumerable<Task<TResult>> tasks)
        {
            tasks.VerifyArgument(nameof(tasks)).IsNotNull();
            var t = tasks as Task<TResult>[] ?? tasks.ToArray();
            if (t.Length > 0)
            {
                var index = Task.WaitAny(t);
                return t[index];
            }
            return null;
        }
        #endregion
    }
}
#endif