namespace Axle.Security.Authentication
{
    public interface IAuthenticationTokenProcessor
    {
        bool CanProcessToken(IAuthenticationToken token);
        void SignIn(IAuthenticationToken token);
    }

    public interface IAuthenticationTokenProcessor<T> : IAuthenticationTokenProcessor where T: IAuthenticationToken
    {
        bool CanProcessToken(T token);
        void SignIn(T token);
    }
}
