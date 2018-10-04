using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SAT.HR.Startup))]
namespace SAT.HR
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
