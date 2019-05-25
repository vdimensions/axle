#if NET20
/*
   Copyright 2014-2017 Virtual Dimensions

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.

 */
namespace Axle
{
    /// <summary>
    /// Represents an function delegate, that is, an operation that returns a result.
    /// </summary>
    /// <typeparam name="TResult">
    /// The type of the result object produced by the action represented by the current <see cref="Func{TResult}"/> delegate.
    /// </typeparam>
    /// <returns>
    /// The result of the operation.
    /// </returns>
    public delegate TResult Func<TResult>();

    /// <summary>
    /// Represents an function delegate, that is, an operation that returns a result.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the first argument passed to the current <see cref="Func{T, TResult}"/> delegate.
    /// </typeparam>
    /// <typeparam name="TResult">
    /// The type of the result object produced by the action represented by the current <see cref="Func{T, TResult}"/> delegate.
    /// </typeparam>
    /// <param name="arg">
    /// The first argument to the action represented bu the current <see cref="Func{T, TResult}"/> delegate.
    /// </param>
    /// <returns>
    /// The result of the operation.
    /// </returns>
    public delegate TResult Func<T, TResult>(T arg);

    /// <summary>
    /// Represents an function delegate, that is, an operation that returns a result.
    /// </summary>
    /// <typeparam name="T1">
    /// The type of the first argument passed to the current <see cref="Func{T1, T2, TResult}"/> delegate.
    /// </typeparam>
    /// <typeparam name="T2">
    /// The type of the second argument passed to the current <see cref="Func{T1, T2, TResult}"/> delegate.
    /// </typeparam>
    /// <typeparam name="TResult">
    /// The type of the result object produced by the action represented by the current <see cref="Func{T1, T2, TResult}"/> delegate.
    /// </typeparam>
    /// <param name="arg1">
    /// The first argument to the action represented bu the current <see cref="Func{T1, T2, TResult}"/> delegate.
    /// </param>
    /// <param name="arg2">
    /// The second argument to the action represented bu the current <see cref="Func{T1, T2, TResult}"/> delegate.
    /// </param>
    /// <returns>
    /// The result of the operation.
    /// </returns>
    public delegate TResult Func<T1, T2, TResult>(T1 arg1, T2 arg2);

    /// <summary>
    /// Represents an function delegate, that is, an operation that returns a result.
    /// </summary>
    /// <typeparam name="T1">
    /// The type of the first argument passed to the current <see cref="Func{T1, T2, T3, TResult}"/> delegate.
    /// </typeparam>
    /// <typeparam name="T2">
    /// The type of the second argument passed to the current <see cref="Func{T1, T2, T3, TResult}"/> delegate.
    /// </typeparam>
    /// <typeparam name="T3">
    /// The type of the third argument passed to the current <see cref="Func{T1, T2, T3, TResult}"/> delegate.
    /// </typeparam>
    /// <typeparam name="TResult">
    /// The type of the result object produced by the action represented by the current <see cref="Func{T1, T2, T3, TResult}"/> delegate.
    /// </typeparam>
    /// <param name="arg1">
    /// The first argument to the action represented bu the current <see cref="Func{T1, T2, T3, TResult}"/> delegate.
    /// </param>
    /// <param name="arg2">
    /// The second argument to the action represented bu the current <see cref="Func{T1, T2, T3, TResult}"/> delegate.
    /// </param>
    /// <param name="arg3">
    /// The third argument to the action represented bu the current <see cref="Func{T1, T2, T3, TResult}"/> delegate.
    /// </param>
    /// <returns>
    /// The result of the operation.
    /// </returns>
    public delegate TResult Func<T1, T2, T3, TResult>(T1 arg1, T2 arg2, T3 arg3);

    /// <summary>
    /// Represents an function delegate, that is, an operation that returns a result.
    /// </summary>
    /// <typeparam name="T1">
    /// The type of the first argument passed to the current <see cref="Func{T1, T2, T3, T4, TResult}"/> delegate.
    /// </typeparam>
    /// <typeparam name="T2">
    /// The type of the second argument passed to the current <see cref="Func{T1, T2, T3, T4, TResult}"/> delegate.
    /// </typeparam>
    /// <typeparam name="T3">
    /// The type of the third argument passed to the current <see cref="Func{T1, T2, T3, T4, TResult}"/> delegate.
    /// </typeparam>
    /// <typeparam name="T4">
    /// The type of the fourth argument passed to the current <see cref="Func{T1, T2, T3, T4, TResult}"/> delegate.
    /// </typeparam>
    /// <typeparam name="TResult">
    /// The type of the result object produced by the action represented by the current <see cref="Func{T1, T2, T3, T4, TResult}"/> delegate.
    /// </typeparam>
    /// <param name="arg1">
    /// The first argument to the action represented bu the current <see cref="Func{T1, T2, T3, T4, TResult}"/> delegate.
    /// </param>
    /// <param name="arg2">
    /// The second argument to the action represented bu the current <see cref="Func{T1, T2, T3, T4, TResult}"/> delegate.
    /// </param>
    /// <param name="arg3">
    /// The third argument to the action represented bu the current <see cref="Func{T1, T2, T3, T4, TResult}"/> delegate.
    /// </param>
    /// <param name="arg4">
    /// The fourth argument to the action represented bu the current <see cref="Func{T1, T2, T3, T4, TResult}"/> delegate.
    /// </param>
    /// <returns>
    /// The result of the operation.
    /// </returns>
    public delegate TResult Func<T1, T2, T3, T4, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
}
#endif