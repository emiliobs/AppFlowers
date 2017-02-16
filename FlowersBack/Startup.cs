using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FlowersBack.Startup))]
namespace FlowersBack
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
