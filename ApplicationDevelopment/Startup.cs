using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ApplicationDevelopment.Startup))]
namespace ApplicationDevelopment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
