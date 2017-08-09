using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Cibertec.RealTime.App_Start;
using Microsoft.Owin.Cors;

//[assembly: OwinStartup(typeof(Cibertec.RealTime.Startup))]

namespace Cibertec.RealTime
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //En la configuracion, lo primero que debe configurarse es CORS
            app.UseCors(CorsOptions.AllowAll);
            
            var httpConfig = new HttpConfiguration();
            //este httpconfiguration es el que configura todo el OWIN
            WebApiConfig.Register(httpConfig);
            app.MapSignalR();
            //Aqui le dice que con toda la configuracion ejecute la webapi
            app.UseWebApi(httpConfig);
        }
    }
}
