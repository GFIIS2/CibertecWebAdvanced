using Microsoft.AspNet.WebApi.Extensions.Compression.Server;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Extensions.Compression.Core.Compressors;
using System.Web;
using System.Web.Http;

namespace Cibertec.RealTime.App_Start
{
    public class WebApiConfig
    {
        //se va encargar del enrutamiento de la WebApi
        public static void Register(HttpConfiguration config)
        {
            //Esta linea es para compression de información
            //Compresion de contenido estatico. (Para IIS, JSon es contenido estatico
            config.MessageHandlers.Insert(0, new ServerCompressionHandler(new GZipCompressor(), new DeflateCompressor()));


            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new {id= RouteParameter.Optional }
                );
        }
    }
}