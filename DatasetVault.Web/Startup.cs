using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DatasetVault.Web.Startup))]
namespace DatasetVault.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
