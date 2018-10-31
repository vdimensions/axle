using Microsoft.AspNetCore.Http;


namespace Axle.Web.AspNetCore.Session
{
    [RequiresAspNetSession]
    public interface ISessionEventListener
    {
        void OnSessionStart(ISession session);
        void OnSessionEnd(string sessionId);
    }
}