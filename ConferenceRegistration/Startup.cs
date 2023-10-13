using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ConferenceRegistration.Startup))]
namespace ConferenceRegistration
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
