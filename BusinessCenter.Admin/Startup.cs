using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BusinessCenter.Admin.Startup))]
namespace BusinessCenter.Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
