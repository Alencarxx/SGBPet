using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SGB_Beta1.Startup))]
namespace SGB_Beta1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
