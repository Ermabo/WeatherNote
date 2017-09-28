using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WeatherNote.Startup))]
namespace WeatherNote
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
