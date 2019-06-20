using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using SatisTakip.Models;
using SatisTakip.DAL;
using SatisTakip.Controllers;
using System.Web.Caching;
using System.Net.Mail;
using System.Net;

namespace SatisTakip
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            db.Database.Initialize(true);
            //JobManager.Initialize(new MyRegistry());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }


}
