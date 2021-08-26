using System;
using System.Collections.Generic;
using Modular.Infrastructure.Auth;
using Modular.Infrastructure.Time;

namespace Modular.Shared.Tests
{
    public static class AuthHelper
    {
        private static readonly AuthManager AuthManager;

        static AuthHelper()
        {
            var options = OptionsHelper.GetOptions<AuthOptions>("auth");
            AuthManager = new AuthManager(options, new UtcClock());
        }

        public static string GenerateJwt(Guid userId, string role = null, string audience = null,
            IDictionary<string, IEnumerable<string>> claims = null)
            => AuthManager.CreateToken(userId, role, audience, claims).AccessToken;
    }
}