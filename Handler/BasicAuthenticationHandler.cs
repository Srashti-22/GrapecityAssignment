using GrapecityAssignment.DataContext;
using GrapecityAssignment.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace GrapecityAssignment.Handler
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {

        }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Header doesnot found any Key");

            try
            {
                UserDetailsModel user = new UserDetailsModel();
                var data = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var decrytedValue = Encoding.UTF8.GetString(Convert.FromBase64String(data.Parameter)).Split(':');
                using (var context = new Context(new DbContextOptions<Context>()))
                    if (context != null)
                        user = context.UserDetails.Where(it => it.UserEmail == decrytedValue[0].ToString() && it.UserPassword == decrytedValue[1].ToString()).FirstOrDefault();
                if (user == null)
                    return AuthenticateResult.Fail("InValid UserId & Password");
                else
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, user.UserEmail) };
                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);

                    return AuthenticateResult.Success(ticket);
                }
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail(ex.Message);
            }
        }


    }
}
