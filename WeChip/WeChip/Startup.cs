using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WeChip.Startup))]
namespace WeChip
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
