using System.Text;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Microsoft.IdentityModel.Tokens;

[assembly: OwinStartup(typeof(JWTAuthDemo.App_Start.Startup))]

namespace JWTAuthDemo.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var issuer = "YourIssuer";
            var secret = TextEncodings.Base64Url.Decode("YourBase64EncodedSecretKey");

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                TokenValidationParameters = new System.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidIssuer = issuer,
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(secret)
                }
            });

            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseWebApi(config);
        }
    }
}
