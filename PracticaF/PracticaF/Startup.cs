using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PracticaF.Startup))]
namespace PracticaF
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
