using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewCarRental.Startup))]
namespace NewCarRental
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
