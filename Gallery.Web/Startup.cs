using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Gallery.Web.Startup))]
namespace Gallery.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
