using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using Axle.Verification;

using Microsoft.AspNetCore.Http;


namespace Axle.Web.AspNetCore.Session
{
    public abstract class SessionScoped<T> : IDisposable
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly ConcurrentDictionary<string, T> _perSessionData = new ConcurrentDictionary<string, T>(StringComparer.Ordinal);

        protected SessionScoped(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public void AddOrReplace(string sessionId, T value, Func<T, T, T> replaceFn)
        {
            sessionId.VerifyArgument(nameof(sessionId)).IsNotNull();
            replaceFn.VerifyArgument(nameof(replaceFn)).IsNotNull();
            _perSessionData.AddOrUpdate(sessionId, value, (_, existing) => replaceFn(existing, value));
        }
        public bool TryRemove(string sessionId, out T removed)
        {
            sessionId.VerifyArgument(nameof(sessionId)).IsNotNull();
            return _perSessionData.TryRemove(sessionId, out removed);
        }

        public string SessionId => _accessor.HttpContext.Session.Id;

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public T Current => _perSessionData.TryGetValue(SessionId, out var result) ? result : default(T);

        public void Dispose()
        {
            var values = _perSessionData.Values.ToArray();
            for (var i = 0; i < values.Length; i++)
            {
                var v = values[i];
                if (v is IDisposable d)
                {
                    d.Dispose();
                }
            }
            _perSessionData.Clear();
        }
        void IDisposable.Dispose() => this.Dispose();
    }
}