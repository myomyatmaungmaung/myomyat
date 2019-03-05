using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AssignmentM4.Startup))]
namespace AssignmentM4
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
