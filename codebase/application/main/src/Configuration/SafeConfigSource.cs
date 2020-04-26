namespace Axle.Configuration
{
    internal sealed class SafeConfigSource : IConfigSource
    {
        private readonly IConfigSource _configSource;

        public SafeConfigSource(IConfigSource configSource)
        {
            _configSource = configSource;
        }

        public IConfiguration LoadConfiguration()
        {
            try
            {
                return _configSource.LoadConfiguration();
            }
            catch
            {
                return null;
            }
        }
    }
}