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
        private readonly ISerializer _serializer = new BinarySerializer();
        private readonly TimeSpan _sessionDuration;
        private readonly Timer _timer;
        private ConcurrentDictionary<string, DateTime> _sessionTimeouts = new ConcurrentDictionary<string, DateTime>();
        const string SessionKey = "FirstSeen";

        public event Action<string> SessionStart;
        public event Action<string> SessionEnd;

        public SessionLifetime(TimeSpan sessionDuration)
        {
            var resolution = sessionDuration.Minutes > 0 ? 10l : 1l;
            _sessionDuration = sessionDuration;
            _timer = new Timer(TimerTick, this, TimeSpan.Zero, new TimeSpan(sessionDuration.Ticks / resolution));
        }

        private void TimerTick(object state)
        {
            var keys = _sessionTimeouts.Keys.ToArray();
            var toRemove = new List<string>();
            var now = DateTime.UtcNow;
            foreach (var key in keys)
            {
                if (_sessionTimeouts.TryGetValue(key, out var sessionLastUpdate) && (sessionLastUpdate - now).TotalMilliseconds > _sessionDuration.TotalMilliseconds)
                {
                    toRemove.Add(key);
                }
            }

            foreach (var key in toRemove)
            {
                if (_sessionTimeouts.TryRemove(key, out var _))
                {
                    SessionEnd?.Invoke(key);
                }
            }
        }

        private DateTime UpdateSessionVisit(ISession session)
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
            return res;
        }

        public Task Middleware(HttpContext context, Func<Task> func)
        {
            var session = context.Session;
            if (!session.TryGetValue(SessionKey, out var dateBytes))
            {
                UpdateSessionVisit(session);
                SessionStart?.Invoke(session.Id);
            }
            else
            {
                UpdateSessionVisit(session);
            }
            return func.Invoke();
        }

        public void Dispose()
        {
            _timer?.Dispose();
            SessionStart = null;
            SessionEnd = null;
        }
    }
}