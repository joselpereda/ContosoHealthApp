using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(zxdemoService.Startup))]

namespace zxdemoService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}