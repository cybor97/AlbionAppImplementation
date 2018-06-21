using System;

namespace Albion.Data
{
    public class AuthToken
    {
        public AuthToken(string token)
        {
            Token = token;
        }

        public string Token { get; set; }
        public int CreatedTimestamp { get; set; } = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
    }
}
