﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebAPI.Models;

namespace WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            DatabaseConnection.Server = System.Configuration.ConfigurationManager.AppSettings["Server"];
            DatabaseConnection.Database = System.Configuration.ConfigurationManager.AppSettings["Database"];
            DatabaseConnection.Username = System.Configuration.ConfigurationManager.AppSettings["Username"];
            DatabaseConnection.Password = System.Configuration.ConfigurationManager.AppSettings["Password"];

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}