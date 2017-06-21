using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LYT_Wx_Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
             
            config.Routes.MapHttpRoute(
                name: "DefaultApi2",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { action = @"[a-zA-Z]+" } //action必须为字母
            );
            
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
