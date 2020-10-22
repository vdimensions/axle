using System;
using System.Collections.Concurrent;
using System.Linq;
using Axle.References;
using Axle.Verification;
using Microsoft.AspNetCore.Http;

namespace Axle.Web.AspNetCore.Session
{
    public sealed class SessionReference<T> : IReference<T>, IDisposable, ISessionEventListener
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly ConcurrentDictionary<string, T> _perSessionData = new ConcurrentDictionary<string, T>(StringComparer.Ordinal);
        private volatile bool _disposed;

        internal SessionReference(IHttpContextAccessor accessor) => _accessor = accessor;

        public void CompareReplace(T value, Func<T, T, T> compareFn)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("The session associated with the current reference no longer exists.");
            }
            compareFn.VerifyArgument(nameof(compareFn)).IsNotNull();
            var sessionId = SessionId;
            if (string.IsNullOrEmpty(sessionId))
            {
                throw new InvalidOperationException("No session available");
            }
            _perSessionData.AddOrUpdate(SessionId, value, (_, existing) => compareFn(existing, value));
        }
        
        public bool TryRemove(out T removed)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("The session associated with the current reference no longer exists.");
            }
            var sessionId = SessionId;
            if (string.IsNullOrEmpty(sessionId))
            {
                removed = default(T);
                return false;
            }
            return _perSessionData.TryRemove(sessionId, out removed);
        }

        public bool TryGetValue(out T value)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("The session associated with the current reference no longer exists.");
            }
            var sessionId = SessionId;
            if (string.IsNullOrEmpty(sessionId))
            {
                value = default(T);
                return false;
            }
            return _perSessionData.TryGetValue(sessionId, out value);
        }

        private void Dispose()
        {
            if (_disposed)
            {
                return;
            }
            _disposed = true;
            var sessionIds = _perSessionData.Keys.ToArray();
            _perSessionData.Clear();
            for (var i = 0; i < sessionIds.Length; i++)
            {
                DisposeSessionData(sessionIds[i]);
            }
        }
        void IDisposable.Dispose() => Dispose();

        private void DisposeSessionData(string sessionId)
        {
            if (_perSessionData.TryRemove(sessionId, out var value))
            {
                if (value is IDisposable d)
                {
                    try
                    {
                        d.Dispose();
                    }
                    catch { }
                }
            }
        }

        void ISessionEventListener.OnSessionStart(ISession session) { }
        void ISessionEventListener.OnSessionEnd(string sessionId) => DisposeSessionData(sessionId);

        public T Value
        {
            get
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException("The session associated with the current reference no longer exists.");
                }
                return _perSessionData.TryGetValue(SessionId, out var result) ? result : default(T);
            }
            set
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException("The session associated with the current reference no longer exists.");
                }
                _perSessionData.AddOrUpdate(SessionId, value, (_, existing) => value);
            }
        }

        /// <inheritdoc />
        bool IReference.HasValue
        {
            get
            {
                var sessionId = SessionId;
                return _disposed || (!string.IsNullOrEmpty(sessionId) && _perSessionData.ContainsKey(sessionId));
            }
        }

        object IReference.Value => Value;

        internal string SessionId => _accessor.HttpContext.Session?.Id;
    }
}