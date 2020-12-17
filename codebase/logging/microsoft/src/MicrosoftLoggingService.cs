using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Verification;
using Microsoft.Extensions.Logging;


namespace Axle.Logging.Microsoft
{
    /// <summary>
    /// A <see cref="ILoggingService"/> that provides <see cref="ILogger"/> implementations backed by 
    /// a <see cref="global::Microsoft.Extensions.Logging.ILogger"/> instances.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class MicrosoftLoggingService : ILoggingService
    {
        private readonly ILoggerFactory _loggerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="MicrosoftLoggingService"/> class.
        /// </summary>
        /// <param name="loggerFactory">
        /// The <see cref="ILoggerFactory"/> that will be used to create 
        /// <see cref="global::Microsoft.Extensions.Logging.ILogger"/> instances.
        /// </param>
        public MicrosoftLoggingService(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory.VerifyArgument(nameof(loggerFactory)).IsNotNull().Value;
        }

        /// <inheritdoc/>
        public ILogger CreateLogger<T>() => new MicrosoftLogger(typeof(T), _loggerFactory.CreateLogger<T>());
        /// <inheritdoc/>
        public ILogger CreateLogger(Type t) => new MicrosoftLogger(t, _loggerFactory.CreateLogger(t));
    }
}