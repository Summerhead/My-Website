using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using asp.net_project_1.Models.Music;
using asp.net_project_1.Models.Schedule;

namespace asp.net_project_1 {
    public class MvcApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            Database.SetInitializer(new PlaylistDbInitializer());
            Database.SetInitializer(new ScheduleDbInitializer());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
