using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SatisTakip.Startup))]
namespace SatisTakip
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
