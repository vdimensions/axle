#if NETSTANDARD || NET20_OR_NEWER
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
    /// Represents an attempt delegate, that is, a delegate representing an action that may or may not succeed.
    /// Therefore, the action's result is passed as an output parameter instead of a return value.
    /// The delegate returns a <see cref="bool"/> value that indicates whether the action succeeded or not.
    /// </summary>
    /// <typeparam name="TResult">
    /// The type of the result object produced by the action represented by the current <see cref="Attempt{TResult}"/> delegate.
    /// </typeparam>
    /// <param name="result">
    /// An output parameter to contain the result of the attempt if it was successful.
    /// </param>
    /// <returns>
    /// <c>true</c> if the action represented by the attempt succeeded; <c>false</c> otherwise.
    /// </returns>
    public delegate bool Attempt<TResult>(out TResult result);

    /// <summary>
    /// Represents an attempt delegate, that is, a delegate representing an action that may or may not succeed.
    /// Therefore, the action's result is passed as an output parameter instead of a return value.
    /// The delegate returns a <see cref="bool"/> value that indicates whether the action succeeded or not.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the argument passed to the current <see cref="Attempt{TResult}"/> delegate.
    /// </typeparam>
    /// <typeparam name="TResult">
    /// The type of the result object produced by the action represented by the current <see cref="Attempt{TResult}"/> delegate.
    /// </typeparam>
    /// <param name="arg">
    /// The argument to the action represented bu the current <see cref="Attempt{TResult}"/> delegate.
    /// </param>
    /// <param name="result">
    /// An output parameter to contain the result of the attempt if it was successful.
    /// </param>
    /// <returns>
    /// <c>true</c> if the action represented by the attempt succeeded; <c>false</c> otherwise.
    /// </returns>
    public delegate bool Attempt<in T, TResult>(T arg, out TResult result);

    /// <summary>
    /// Represents an attempt delegate, that is, a delegate representing an action that may or may not succeed.
    /// Therefore, the action's result is passed as an output parameter instead of a return value.
    /// The delegate returns a <see cref="bool"/> value that indicates whether the action succeeded or not.
    /// </summary>
    /// <typeparam name="T1">
    /// The type of the first argument passed to the current <see cref="Attempt{TResult}"/> delegate.
    /// </typeparam>
    /// <typeparam name="T2">
    /// The type of the second argument passed to the current <see cref="Attempt{TResult}"/> delegate.
    /// </typeparam>
    /// <typeparam name="TResult">
    /// The type of the result object produced by the action represented by the current <see cref="Attempt{TResult}"/>
    /// delegate.
    /// </typeparam>
    /// <param name="arg1">
    /// The first argument to the action represented bu the current <see cref="Attempt{TResult}"/> delegate.
    /// </param>
    /// <param name="arg2">
    /// The second argument to the action represented bu the current <see cref="Attempt{TResult}"/> delegate.
    /// </param>
    /// <param name="result">
    /// An output parameter to contain the result of the attempt if it was successful.
    /// </param>
    /// <returns>
    /// <c>true</c> if the action represented by the attempt succeeded; <c>false</c> otherwise.
    /// </returns>
    public delegate bool Attempt<in T1, in T2, TResult>(T1 arg1, T2 arg2, out TResult result);

    /// <summary>
    /// Represents an attempt delegate, that is, a delegate representing an action that may or may not succeed.
    /// Therefore, the action's result is passed as an output parameter instead of a return value.
    /// The delegate returns a <see cref="bool"/> value that indicates whether the action succeeded or not.
    /// </summary>
    /// <typeparam name="T1">
    /// The type of the first argument passed to the current <see cref="Attempt{TResult}"/> delegate.
    /// </typeparam>
    /// <typeparam name="T2">
    /// The type of the second argument passed to the current <see cref="Attempt{TResult}"/> delegate.
    /// </typeparam>
    /// <typeparam name="T3">
    /// The type of the third argument passed to the current <see cref="Attempt{TResult}"/> delegate.
    /// </typeparam>
    /// <typeparam name="TResult">
    /// The type of the result object produced by the action represented by the current <see cref="Attempt{TResult}"/>
    /// delegate.
    /// </typeparam>
    /// <param name="arg1">
    /// The first argument to the action represented bu the current <see cref="Attempt{TResult}"/> delegate.
    /// </param>
    /// <param name="arg2">
    /// The second argument to the action represented bu the current <see cref="Attempt{TResult}"/> delegate.
    /// </param>
    /// <param name="arg3">
    /// The third argument to the action represented bu the current <see cref="Attempt{TResult}"/> delegate.
    /// </param>
    /// <param name="result">
    /// An output parameter to contain the result of the attempt if it was successful.
    /// </param>
    /// <returns>
    /// <c>true</c> if the action represented by the attempt succeeded; <c>false</c> otherwise.
    /// </returns>
    public delegate bool Attempt<in T1, in T2, in T3, TResult>(T1 arg1, T2 arg2, T3 arg3, out TResult result);

    /// <summary>
    /// Represents an attempt delegate, that is, a delegate representing an action that may or may not succeed.
    /// Therefore, the action's result is passed as an output parameter instead of a return value.
    /// The delegate returns a <see cref="bool"/> value that indicates whether the action succeeded or not.
    /// </summary>
    /// <typeparam name="T1">
    /// The type of the first argument passed to the current <see cref="Attempt{TResult}"/> delegate.
    /// </typeparam>
    /// <typeparam name="T2">
    /// The type of the second argument passed to the current <see cref="Attempt{TResult}"/> delegate.
    /// </typeparam>
    /// <typeparam name="T3">
    /// The type of the third argument passed to the current <see cref="Attempt{TResult}"/> delegate.
    /// </typeparam>
    /// <typeparam name="T4">
    /// The type of the fourth argument passed to the current <see cref="Attempt{TResult}"/> delegate.
    /// </typeparam>
    /// <typeparam name="TResult">
    /// The type of the result object produced by the action represented by the current <see cref="Attempt{TResult}"/>
    /// delegate.
    /// </typeparam>
    /// <param name="arg1">
    /// The first argument to the action represented bu the current <see cref="Attempt{TResult}"/> delegate.
    /// </param>
    /// <param name="arg2">
    /// The second argument to the action represented bu the current <see cref="Attempt{TResult}"/> delegate.
    /// </param>
    /// <param name="arg3">
    /// The third argument to the action represented bu the current <see cref="Attempt{TResult}"/> delegate.
    /// </param>
    /// <param name="arg4">
    /// The fourth argument to the action represented bu the current <see cref="Attempt{TResult}"/> delegate.
    /// </param>
    /// <param name="result">
    /// An output parameter to contain the result of the attempt if it was successful.
    /// </param>
    /// <returns>
    /// <c>true</c> if the action represented by the attempt succeeded; <c>false</c> otherwise.
    /// </returns>
    public delegate bool Attempt<in T1, in T2, in T3, in T4, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, out TResult result);
}
#endif