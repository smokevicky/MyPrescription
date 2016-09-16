using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyPrescription.Services.Startup))]
namespace MyPrescription.Services
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
