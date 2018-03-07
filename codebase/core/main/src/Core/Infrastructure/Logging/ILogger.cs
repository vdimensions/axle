using System;


namespace Axle.Core.Infrastructure.Logging
{
    public interface ILogger
    {
        void Write(ILogEntry entry);

        Type TargetType { get; }
    }
}