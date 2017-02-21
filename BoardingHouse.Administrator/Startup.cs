using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BoardingHouse.Administrator.Startup))]
namespace BoardingHouse.Administrator
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
