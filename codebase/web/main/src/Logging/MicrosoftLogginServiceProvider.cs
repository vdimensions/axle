using System;
using System.Diagnostics.CodeAnalysis;

using Axle.Verification;

using Microsoft.Extensions.Logging;


namespace Axle.Web.AspNetCore.Logging
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class MicrosoftLogginServiceProvider : Axle.Logging.ILoggingServiceProvider
    {
        private readonly ILoggerFactory _loggerFactory;

        public MicrosoftLogginServiceProvider(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory.VerifyArgument(nameof(loggerFactory)).IsNotNull().Value;
        }

        public Axle.Logging.ILogger Create<T>() => new MicrosoftLogger(typeof(T), _loggerFactory.CreateLogger<T>());
        public Axle.Logging.ILogger Create(Type t) => new MicrosoftLogger(t, _loggerFactory.CreateLogger(t));
    }
}