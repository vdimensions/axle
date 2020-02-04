using System;
using System.Diagnostics.CodeAnalysis;

using Axle.Verification;

using Microsoft.Extensions.Logging;


namespace Axle.Web.AspNetCore.Logging
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class MicrosoftLoggingServiceProvider : Axle.Logging.ILoggingServiceProvider
    {
        private readonly ILoggerFactory _loggerFactory;

        public MicrosoftLoggingServiceProvider(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory.VerifyArgument(nameof(loggerFactory)).IsNotNull().Value;
        }

        Axle.Logging.ILogger Axle.Logging.ILoggingServiceProvider.Create<T>() => CreateLogger<T>();
        Axle.Logging.ILogger Axle.Logging.ILoggingServiceProvider.Create(Type t) => CreateLogger(t);

        public Axle.Logging.ILogger CreateLogger<T>() => new MicrosoftLogger(typeof(T), _loggerFactory.CreateLogger<T>());
        public Axle.Logging.ILogger CreateLogger(Type t) => new MicrosoftLogger(t, _loggerFactory.CreateLogger(t));
    }
}