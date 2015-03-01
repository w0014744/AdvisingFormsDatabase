using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CMPSAdvising.Startup))]
namespace CMPSAdvising
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
