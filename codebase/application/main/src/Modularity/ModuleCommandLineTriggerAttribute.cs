using System;
using Axle.Verification;

namespace Axle.Modularity
{
    /// <summary>
    /// An attribute that causes a module to be loaded only when certain command-line arguments are passed
    /// to the application.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ModuleCommandLineTriggerAttribute : Attribute
    {
        private int _argumentIndex;
        private string _argumentValue = string.Empty;

        /// <summary>
        /// The index of the command argument to check.
        /// </summary>
        public int ArgumentIndex
        {
            get => _argumentIndex;
            set => _argumentIndex = value.VerifyArgument(nameof(ArgumentIndex)).IsGreaterThanOrEqualTo(0);
        }

        /// <summary>
        /// A <see cref="string"/> value that must be equal to the passed by the command-line arguments value
        /// in order to activate the target module.
        /// </summary>
        public string ArgumentValue
        {
            get => _argumentValue;
            set => _argumentValue = value?.VerifyArgument(nameof(ArgumentValue)).IsNotEmpty().Value;
        }
    }
}