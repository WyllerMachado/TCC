using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClassLogger.Startup))]
namespace ClassLogger
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
