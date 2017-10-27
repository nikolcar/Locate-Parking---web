using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LocateParking.Startup))]
namespace LocateParking
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
