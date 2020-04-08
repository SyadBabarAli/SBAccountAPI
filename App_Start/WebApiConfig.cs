using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AspNetPagingExample.Models;

namespace SBAccountAPI
{
    public static class WebApiConfig
    {
        public static bool IsDevelopment = true;

        public static void Register(HttpConfiguration config)
        {
            ModelMapper.Init();

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            if (IsDevelopment == false)
            {
                config.Filters.Add(new AuthorizeAttribute());
            }
            config.EnableCors();
        }
    }
}
