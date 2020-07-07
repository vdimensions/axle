using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Axle.Verification;


namespace Axle.Modularity
{
    /// <summary>
    /// An object that acts as a descriptor for an application module. 
    /// </summary>
    internal sealed class ModuleInfo
    {
        internal ModuleInfo(Type type,
            Type requiredApplicationHostType,
            ModuleMethod initMethod,
            ModuleMethod readyMethod,
            ModuleEntryMethod entryPointMethod,
            ModuleMethod terminateMethod,
            IEnumerable<ModuleCallback> initCallbacks,
            IEnumerable<ModuleCallback> terminateCallbacks,
            IModuleReferenceAttribute[] utilizedModules,
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
            ReadyMethod = readyMethod;
            EntryPointMethod = entryPointMethod;
            TerminateMethod = terminateMethod;
            
            DependencyInitializedMethods = initCallbacks.OrderByDescending(x => x.Priority).ToArray();
            DependencyTerminatedMethods = terminateCallbacks.OrderByDescending(x => x.Priority).ToArray();
            
            UtilizedModules = utilizedModules;
            ReportsToModules = reportsToModules;
            RequiredModules = requiredModules;
            
            CommandLineTrigger = commandLineTrigger;
            
            ConfigSectionInfo = configSectionInfo;
        }
        
        internal ModuleMethod InitMethod { get; }
        internal ModuleMethod ReadyMethod { get; }
        internal ModuleEntryMethod EntryPointMethod { get; }
        internal ModuleMethod TerminateMethod { get; }
        internal ModuleCallback[] DependencyInitializedMethods { get; }
        internal ModuleCallback[] DependencyTerminatedMethods { get; }
        internal ModuleCommandLineTriggerAttribute CommandLineTrigger { get; }
        internal ModuleConfigSectionAttribute ConfigSectionInfo { get; }
        
        internal Type RequiredApplicationHostType { get; }

        public Type Type { get; }

        #if NETSTANDARD || NET45_OR_NEWER
        public Assembly Assembly => Type.GetTypeInfo().Assembly;
        #else
        public Assembly Assembly => Type.Assembly;
        #endif

        public IModuleReferenceAttribute[] UtilizedModules { get; }

        public ReportsToAttribute[] ReportsToModules { get; }

        public ModuleInfo[] RequiredModules { get; internal set; }
    }
}