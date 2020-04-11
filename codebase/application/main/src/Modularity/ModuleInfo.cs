using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Axle.Verification;


namespace Axle.Modularity
{
    internal sealed class ModuleInfo
    {
        internal ModuleInfo(
            Type type, 
            Type requiredApplicationHostType,
            ModuleMethod initMethod, 
            IEnumerable<ModuleCallback> initCallbacks, 
            IEnumerable<ModuleCallback> terminateCallbacks, 
            ModuleMethod terminateMethod, 
            ModuleEntryMethod entryPointMethod,
            IUsesAttribute[] utilizedModules,
            ReportsToAttribute[] reportsToModules,
            ModuleCommandLineTriggerAttribute commandLineTrigger, 
            ModuleConfigSectionAttribute configSectionInfo, 
            params ModuleInfo[] requiredModules)
        {
            Type = type.VerifyArgument(nameof(type)).IsNotAbstract();
            requiredApplicationHostType?
                .VerifyArgument(nameof(requiredApplicationHostType))
                .IsOfType<IApplicationHost>();
            RequiredApplicationHostType = requiredApplicationHostType;
            InitMethod = initMethod;
            DependencyInitializedMethods = initCallbacks.OrderBy(x => x.Priority).ToArray();
            DependencyTerminatedMethods = terminateCallbacks.OrderBy(x => x.Priority).ToArray();
            TerminateMethod = terminateMethod;
            EntryPointMethod = entryPointMethod;
            UtilizedModules = utilizedModules;
            ReportsToModules = reportsToModules;
            RequiredModules = requiredModules;
            CommandLineTrigger = commandLineTrigger;
            ConfigSectionInfo = configSectionInfo;
        }
        
        internal ModuleMethod InitMethod { get; }
        internal ModuleCallback[] DependencyInitializedMethods { get; }
        internal ModuleCallback[] DependencyTerminatedMethods { get; }
        internal ModuleMethod TerminateMethod { get; }
        internal ModuleEntryMethod EntryPointMethod { get; }
        internal ModuleCommandLineTriggerAttribute CommandLineTrigger { get; }
        internal ModuleConfigSectionAttribute ConfigSectionInfo { get; }
        
        internal Type RequiredApplicationHostType { get; }

        public Type Type { get; }

        #if NETSTANDARD || NET45_OR_NEWER
        public Assembly Assembly => Type.GetTypeInfo().Assembly;
        #else
        public Assembly Assembly => Type.Assembly;
        #endif

        public IUsesAttribute[] UtilizedModules { get; }

        public ReportsToAttribute[] ReportsToModules { get; }

        public ModuleInfo[] RequiredModules { get; internal set; }
    }
}