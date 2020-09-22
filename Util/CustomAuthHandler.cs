using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Global_Intern.Util
{
    public class BasicAuthOptions : AuthenticationSchemeOptions
    {

    }

    public class CustomAuthHandler : AuthenticationHandler<BasicAuthOptions>
    {
        private readonly ICustomAuthManager cAuthManger;
        private readonly IHttpContextAccessor httpContextAccessor;
        private string _tokenSession;
        public CustomAuthHandler(
            IOptionsMonitor<BasicAuthOptions> options, ILoggerFactory logger, UrlEncoder encode, ISystemClock clock,
            ICustomAuthManager customAuthManager,
            IHttpContextAccessor _httpContextAccessor
            ) : base(options, logger, encode, clock)
        {

            cAuthManger = customAuthManager;
            httpContextAccessor = _httpContextAccessor;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {


            //if (!Request.Headers.ContainsKey("Authorization"))
            //    return AuthenticateResult.Fail("UnAuthorized");

            //string authHandler = Request.Headers["Authorization"];
            //if(string.IsNullOrEmpty(authHandler))
            //    return AuthenticateResult.Fail("UnAuthorize");

            //if(authHandler.StartsWith("bearer", StringComparison.OrdinalIgnoreCase))
            //    return AuthenticateResult.Fail("UnAuthorize");

            //string token = authHandler.Substring("bearer".Length).Trim();
            //if(string.IsNullOrEmpty(token))
            //    return AuthenticateResult.Fail("UnAuthorize");
            try
            {
                // The "UserToken" session get created when login 
                if (httpContextAccessor.HttpContext.Session.GetString("UserToken") != null)
                {
                    _tokenSession = httpContextAccessor.HttpContext.Session.GetString("UserToken");
                }
                return validateToken();
            }
            catch (Exception ex)
            {

                return AuthenticateResult.Fail(ex.Message);
            }
        }


        private AuthenticateResult validateToken()
        {
            // Making Claim with Identity 
            var validatedToken = cAuthManger.Tokens.FirstOrDefault(t => t.Key == _tokenSession);
            if (validatedToken.Key == null)
            {
                return AuthenticateResult.Fail("UnAuthorize");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, validatedToken.Value.Item1),
                new Claim(ClaimTypes.Role, validatedToken.Value.Item2)
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);

            var principle = new GenericPrincipal(identity, new[] { validatedToken.Value.Item2 });
            var ticket = new AuthenticationTicket(principle, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
