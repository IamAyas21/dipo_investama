using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DIPO_INVESTAMA.Startup))]
namespace DIPO_INVESTAMA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
