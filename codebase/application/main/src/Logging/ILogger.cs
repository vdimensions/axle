using System;


namespace Axle.Application.Logging
{
    public interface ILogger
    {
        void Write(ILogEntry entry);

        Type TargetType { get; }
    }
}