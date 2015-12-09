using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ManagerSales.Web.GUI.Startup))]
namespace ManagerSales.Web.GUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
