//using System;
//
//using Axle.DependencyInjection;
//
//
//namespace Axle.Application.IoC
//{
//    partial class Container : IContainer
//    {
//        IContainer IContainer.RegisterInstance(object instance, string name, params string[] aliases)
//        {
//            return RegisterInstance(instance, name, aliases);
//        }
//
//        IContainer IContainer.RegisterType(Type type, string name, params string[] aliases)
//        {
//            return Register(type, name, aliases);
//        }
//
//        object IContainer.Resolve(Type type, string name) { return this.Resolve(type, name); }
//
//        IContainer IContainer.Parent { get { return Parent; } }
//    }
//}