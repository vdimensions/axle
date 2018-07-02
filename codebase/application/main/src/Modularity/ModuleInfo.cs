using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Axle.Verification;


namespace Axle.Application.Modularity
{
    public sealed class ModuleInfo
    {
        internal ModuleInfo(
            Type type, 
            ModuleMethod initMethod, 
            IEnumerable<ModuleCallback> initCallbacks, 
            IEnumerable<ModuleCallback> terminateCallbacks, 
            ModuleMethod readyMethod, 
            ModuleEntryMethod entryPointMethod, 
            params ModuleInfo[] requiredModules)
        {
            Type = type.VerifyArgument(nameof(type)).IsNotAbstract();
            InitMethod = initMethod;
            DependencyInitializedMethods = initCallbacks.OrderBy(x => x.Priority).ToArray();
            DependencyTerminatedMethods = terminateCallbacks.OrderBy(x => x.Priority).ToArray();
            ReadyMethod = readyMethod;
            EntryPointMethod = entryPointMethod;
            RequiredModules = requiredModules;
        }

        public Type Type { get; }
        #if NETSTANDARD || NET45_OR_NEWER
        public Assembly Assembly => Type.GetTypeInfo().Assembly;
        #else
        public Assembly Assembly => Type.Assembly;
        #endif
        public ModuleMethod InitMethod { get; }
        public ModuleCallback[] DependencyInitializedMethods { get; }
        public ModuleCallback[] DependencyTerminatedMethods { get; }
        public ModuleMethod ReadyMethod { get; }
        public ModuleEntryMethod EntryPointMethod { get; }
        public ModuleInfo[] RequiredModules { get; }
    }
}