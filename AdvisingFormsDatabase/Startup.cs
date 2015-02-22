using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AdvisingFormsDatabase.Startup))]
namespace AdvisingFormsDatabase
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
