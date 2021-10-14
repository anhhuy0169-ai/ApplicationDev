using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ApplicationDev.Startup))]
namespace ApplicationDev
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
