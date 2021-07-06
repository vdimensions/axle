namespace Axle.Web.AspNetCore.Session
{
    public interface ISessionReferenceProvider
    {
        SessionReference<T> CreateSessionReference<T>();
    }
}