using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AdvertManager.Startup))]
namespace AdvertManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
