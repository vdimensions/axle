using System;

namespace Axle.Logging.Microsoft
{
    using IMSLogger = global::Microsoft.Extensions.Logging.ILogger;
    using IMSLoggerProvider = global::Microsoft.Extensions.Logging.ILoggerProvider;

    public sealed class Axle2MicrosoftLoggerProvider : IMSLoggerProvider
    {
        public IMSLogger CreateLogger(string categoryName)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
