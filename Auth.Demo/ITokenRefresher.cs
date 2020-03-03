using Auth.Demo.Controllers;

namespace Auth.Demo
{
    public interface ITokenRefresher
    {
        AuthenticationResponse Refresh(RefreshCred refreshCred);
    }
}