using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Verification;
using Microsoft.Extensions.Logging;


namespace Axle.Logging.Microsoft
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class MicrosoftLoggingService : Axle.Logging.ILoggingService
    {
        private readonly ILoggerFactory _loggerFactory;

        public MicrosoftLoggingService(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory.VerifyArgument(nameof(loggerFactory)).IsNotNull().Value;
        }

        public Axle.Logging.ILogger CreateLogger<T>() => new MicrosoftLogger(typeof(T), _loggerFactory.CreateLogger<T>());
        public Axle.Logging.ILogger CreateLogger(Type t) => new MicrosoftLogger(t, _loggerFactory.CreateLogger(t));
    }
}