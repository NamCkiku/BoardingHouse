using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BoaringHouse.API.Startup))]
namespace BoaringHouse.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
