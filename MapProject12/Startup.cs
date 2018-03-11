using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MapProject12.Startup))]
namespace MapProject12
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
