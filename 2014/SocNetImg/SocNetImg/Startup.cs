using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SocNetImg.Startup))]
namespace SocNetImg
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
