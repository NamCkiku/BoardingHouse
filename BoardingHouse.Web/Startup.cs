using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BoardingHouse.Web.Startup))]
namespace BoardingHouse.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
