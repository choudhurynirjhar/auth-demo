namespace Auth.Demo.Controllers
{
    public class UserCred
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class RefreshCred
    {
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
    }
}