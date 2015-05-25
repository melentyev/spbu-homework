using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;

using Owin;
using SocNetImg.Models;
using System.Threading.Tasks;

namespace SocNetImg
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            var facebookOptions = new Microsoft.Owin.Security.Facebook.FacebookAuthenticationOptions()
            {
                AppId = "1417929391843993",
                AppSecret = "41b8a4bd403b41854e912511579e2d0c",
                Provider = new Microsoft.Owin.Security.Facebook.FacebookAuthenticationProvider()
                {
                    OnAuthenticated = (context) =>
                        {
                            //context.Identity.AddClaim(new System.Security.Claims.Claim("urn:facebook:access_token", context.AccessToken, XmlSchemaString, "Facebook"));
                            CommonClasses.TemporaryTokensStorage.FbAccessToken = context.AccessToken;
                            return Task.FromResult(0);
                        }
                }
            };

            app.UseFacebookAuthentication(facebookOptions);

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
            var vkOptions = new Duke.Owin.VkontakteMiddleware.VkAuthenticationOptions()
            {
                AppId = "4784878",
                AppSecret = "v7kUKVf589cnav50xzqZ",
                Scope = "photos",
                Provider = new Duke.Owin.VkontakteMiddleware.Provider.VkAuthenticationProvider()
                {
                    OnAuthenticated = (context) => 
                        {
                            //context.Identity.AddClaim(new System.Security.Claims.Claim("urn:facebook:access_token", context.AccessToken, XmlSchemaString, "Facebook"));
                            CommonClasses.TemporaryTokensStorage.VkAccessToken = context.AccessToken;
                            return Task.FromResult(0);
                        }
                }
            };
            app.UseVkontakteAuthentication(vkOptions);
        }
    }
}