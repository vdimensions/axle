using System;
using System.Collections.Concurrent;
using System.Linq;
using Axle.References;
using Axle.Verification;
using Microsoft.AspNetCore.Http;

namespace Axle.Web.AspNetCore.Session
{
    public abstract class SessionReference<T> : IDisposable, IReference<T>
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly ConcurrentDictionary<string, T> _perSessionData = new ConcurrentDictionary<string, T>(StringComparer.Ordinal);

        protected SessionReference(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        
        public void CompareReplace(T value, Func<T, T, T> compareFn)
        {
            compareFn.VerifyArgument(nameof(compareFn)).IsNotNull();
            _perSessionData.AddOrUpdate(SessionId, value, (_, existing) => compareFn(existing, value));
        }
        
        public  bool TryRemove(out T removed) => _perSessionData.TryRemove(SessionId, out removed);

        public bool TryGetValue(out T value) => _perSessionData.TryGetValue(SessionId, out value);

        public T Value
        {
            get => _perSessionData.TryGetValue(SessionId, out var result) ? result : default(T);
            set => _perSessionData.AddOrUpdate(SessionId, value, (_, existing) => value);
        }
        object IReference.Value => Value;

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

        public string SessionId => _accessor.HttpContext.Session.Id;
    }
}