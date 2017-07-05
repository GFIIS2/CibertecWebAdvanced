using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cibertec.WebApi.Provider
{
    public class TokenProviderOptions
    {
        public string Path { get; set; } = "/token";
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public TimeSpan Expiration { get; set; } = TimeSpan.FromHours(8);
        public SigningCredentials SigningCredentials { get; set; }
    }
}
