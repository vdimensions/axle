using System;
using Axle.Verification;

namespace Axle.Modularity
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ModuleCommandLineTriggerAttribute : Attribute
    {
        private int _argumentIndex = 0;
        private string _argumentValue = string.Empty;

        public int ArgumentIndex
        {
            get => _argumentIndex;
            set => _argumentIndex = value.VerifyArgument(nameof(ArgumentIndex)).IsGreaterThanOrEqualTo(0);
        }

        public string ArgumentValue
        {
            get => _argumentValue;
            set => _argumentValue = value?.VerifyArgument(nameof(ArgumentValue)).IsNotEmpty().Value;
        }
    }
}