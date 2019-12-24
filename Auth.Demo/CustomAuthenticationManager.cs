using System;
using System.Collections.Generic;
using System.Linq;

namespace Auth.Demo
{
    public interface ICustomAuthenticationManager
    {
        string Authenticate(string username, string password);

        IDictionary<string, string> Tokens { get; }
    }

    public class CustomAuthenticationManager : ICustomAuthenticationManager
    {
        private readonly IDictionary<string, string> users = new Dictionary<string, string>
        {
            { "test1", "password1" },
            { "test2", "password2" }
        };

        private readonly IDictionary<string, string> tokens = new Dictionary<string, string>();

        public IDictionary<string, string> Tokens => tokens;

        public string Authenticate(string username, string password)
        {
            if (!users.Any(u => u.Key == username && u.Value == password))
            {
                return null;
            }

            var token = Guid.NewGuid().ToString();

            tokens.Add(token, username);

            return token;
        }
    }
}
