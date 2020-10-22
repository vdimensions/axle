namespace Axle.Configuration
{
    /// <summary>
    /// An interface representing a configuration's root object.
    /// Besides the significance of this being the top-level configuration entry,
    /// an <see cref="IConfiguration"/> has the same semantics as a <see cref="IConfigSection"/>. 
    /// </summary>
    /// <seealso cref="IConfigSection"/>
    public interface IConfiguration : IConfigSection { }
}