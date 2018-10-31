using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Axle.IO.Extensions.Stream;
using Axle.IO.Serialization;

using Microsoft.AspNetCore.Http;


namespace Axle.Web.AspNetCore.Session
{
    internal sealed class SessionLifetime : IDisposable
    {
        const string SessionKey = "FirstSeen";

        private readonly ISerializer _serializer = new BinarySerializer();
        private readonly TimeSpan _sessionDuration;
        private readonly Timer _timer;
        private readonly ConcurrentDictionary<string, DateTime> _sessionTimeouts = new ConcurrentDictionary<string, DateTime>();
        private readonly IList<ISessionEventListener> _listeners = new List<ISessionEventListener>();

        public SessionLifetime(TimeSpan sessionDuration)
        {
            var resolution = sessionDuration.Minutes > 0 ? 10l : 1l;
            _sessionDuration = sessionDuration;
            _timer = new Timer(TimerTick, this, TimeSpan.Zero, new TimeSpan(sessionDuration.Ticks / resolution));
        }

        private void TimerTick(object state)
        {
            var keys = _sessionTimeouts.Keys.ToArray();
            var expiredSessions = new List<string>();
            var now = DateTime.UtcNow;
            foreach (var key in keys)
            {
                if (_sessionTimeouts.TryGetValue(key, out var sessionLastUpdate) && (now - sessionLastUpdate).TotalMilliseconds > _sessionDuration.TotalMilliseconds)
                {
                    expiredSessions.Add(key);
                }
            }

            foreach (var key in expiredSessions)
            {
                if (_sessionTimeouts.TryRemove(key, out var _))
                {
                    foreach (var listener in _listeners)
                    {
                        listener.OnSessionEnd(key);
                    }
                }
            }
        }

        private void UpdateSessionVisit(ISession session)
        {
            // TODO: new session notfy
            var res = DateTime.UtcNow;
            using (var memStream = new MemoryStream())
            {
                _serializer.Serialize(res, memStream);
                memStream.Flush();
                session.Set(SessionKey, memStream.ToByteArray());
                _sessionTimeouts.AddOrUpdate(session.Id, res, (_, __) => res);
            }
        }

        public Task Middleware(HttpContext context, Func<Task> func)
        {
            var session = context.Session;
            if (!session.TryGetValue(SessionKey, out _))
            {
                UpdateSessionVisit(session);
                foreach (var listener in _listeners)
                {
                    listener.OnSessionStart(session);
                }
            }
            else
            {
                UpdateSessionVisit(session);
            }
            return func.Invoke();
        }

        public SessionLifetime Subscribe(ISessionEventListener listener)
        {
            _listeners.Add(listener);
            return this;
        }
        public SessionLifetime Unsubscribe(ISessionEventListener listener)
        {
            _listeners.Remove(listener);
            return this;
        }

        public void Dispose()
        {
            _timer?.Dispose();
            _listeners.Clear();
        }
    }
}