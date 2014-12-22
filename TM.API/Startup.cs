using System;
using Microsoft.Owin;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System.Web.Http;
using TM.API.Ninject;
using TM.API.Providers;
using WebApiContrib.IoC.Ninject;

[assembly: OwinStartup(typeof(TM.API.Startup))]
namespace TM.API
{
    public class Startup
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        public static GoogleOAuth2AuthenticationOptions googleAuthOptions { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            ConfigureOAuth(app);
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            config.DependencyResolver = new NinjectResolver(NinjectConfig.CreateKernel());
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            AutoMapperConfig.CreateMappings();

            //use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie);
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {

                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new SimpleAuthorizationServerProvider(),
                RefreshTokenProvider = new SimpleRefreshTokenProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);

            //Configure Google External Login
            googleAuthOptions = new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "187897323933-js60krf5kf1if04icf0mb0pb1qureo0j.apps.googleusercontent.com",
                ClientSecret = "RQFo0SKcTFLd1Hk49XJE-1XU",
                Provider = new GoogleAuthProvider()
            };
            app.UseGoogleAuthentication(googleAuthOptions);
        }
    }
}