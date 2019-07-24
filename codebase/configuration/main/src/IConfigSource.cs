namespace Axle.Configuration
{
    public interface IConfigSource
    {
        IConfiguration LoadConfiguration();
    }
}