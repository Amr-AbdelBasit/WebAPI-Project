using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

[assembly: OwinStartup(typeof(Web_API_Project.Startup1))]

namespace Web_API_Project
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            app.UseCors(CorsOptions.AllowAll);

            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/login"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),

                Provider = new TokenCreate()
            });

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());


            // SetUp new Configuration and send it to WebApi
            HttpConfiguration configuration = new HttpConfiguration();

            configuration.MapHttpAttributeRoutes();

            configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            app.UseWebApi(configuration);

        }
    }

    internal class TokenCreate : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        //    public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        //    {
        //        UserStore<IdentityUser> store = new UserStore<IdentityUser>(new ApplicationDbContext());
        //        UserManager<IdentityUser> manager = new UserManager<IdentityUser>(store);
        //        IdentityUser user = await manager.FindAsync(context.UserName, context.Password);

        //        if (user != null)
        //        {
        //            ClaimsIdentity claims = new ClaimsIdentity(context.Options.AuthenticationType);
        //            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
        //            claims.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

        //            if (manager.IsInRole(user.Id, "Admin"))
        //            {
        //                claims.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
        //            }

        //            context.Validated(claims);
        //        }
        //        else
        //        {
        //            context.SetError("Grant_Error", "UserName Or Password is not valid");
        //        }
        //    }
    }
}
