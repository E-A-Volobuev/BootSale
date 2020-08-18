using Microsoft.Owin;
using Owin;
using BootSale.Models;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using BootSale.App_Start;

[assembly: OwinStartup(typeof(Startup))]

namespace BootSale.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // настраиваем контекст и менеджер
            app.CreatePerOwinContext<ProductContext>(ProductContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }
    }
}