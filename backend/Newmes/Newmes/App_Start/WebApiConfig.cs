using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Cors; // ✅ Make sure this is here

namespace Newmes
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // ✔ enable attribute routing
            config.MapHttpAttributeRoutes();

            // ✔ enable CORS for all controllers
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            // existing default route …
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
