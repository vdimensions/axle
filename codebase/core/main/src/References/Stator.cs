#if NETSTANDARD || NET35_OR_NEWER
using Axle.ComponentModel;
using Axle.Verification;


namespace Axle.References
{
    /// <summary>
    /// A stator is a special type of a decorator around a <see langword="class"/>, which allows
    /// for exposing the class functionality without referring to the actual implementation type.
    /// The actual implementation type could be provided from an external source (such as dependency 
    /// injection container) when available. 
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <typeparam name="TComponentFallbackDriver"></typeparam>
    [ComponentStator]
    public abstract class Stator<TComponent, TComponentFallbackDriver> : IReference<TComponent> 
        where TComponentFallbackDriver: IComponentDriver<TComponent>, new()
    {
        private static TComponent Instantiate(IComponentDriver<TComponent> fallbackDriver)
        {
            return (DriverRegistry.Instance.GetDriver<TComponent>() ?? fallbackDriver).Resolve();
        }

        protected TComponent Target;

        private Stator(TComponent target)
        {
            Target = target.VerifyArgument(nameof(target)).IsNotNull();
        }
        public Stator() : this(Instantiate(new TComponentFallbackDriver())) { }

        TComponent IReference<TComponent>.Value => Target;
        object IReference.Value => Target;
    }
}
#endif