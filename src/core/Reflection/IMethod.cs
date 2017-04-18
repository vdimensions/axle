using System;
using System.Reflection;


namespace Axle.Reflection
{
    /// <summary>
    /// An interface that represents a reflected method. Enables quering the reflected metadata and allows 
    /// the invocation of the reflected member.
    /// </summary>
    /// <seealso cref="IInvokable"/>
    /// <seealso cref="IMember"/>
    /// <seealso cref="System.Reflection.MethodInfo"/>
    //[Maturity(CodeMaturity.Stable)]
    public interface IMethod : IInvokable, IMember, IReflected<MethodInfo>
    {   
        IGenericMethod MakeGeneric(params Type[] types);

        bool IsGeneric { get; }
        bool HasGenericDefinition { get; }
        Type ReturnType { get; }
    }
}