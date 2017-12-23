using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(REMS.Website.Startup))]
namespace REMS.Website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
