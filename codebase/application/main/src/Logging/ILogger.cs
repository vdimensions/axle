using System;


namespace Axle.Logging
{
    public interface ILogger
    {
        void Write(ILogEntry entry);

        Type TargetType { get; }
    }
}