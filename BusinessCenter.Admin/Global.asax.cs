using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BusinessCenter.Admin.Common;
using BusinessCenter.Data;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Identity;

namespace BusinessCenter.Admin
{
    public class MvcApplication : System.Web.HttpApplication
    {
       
        //private IdentityConnectionContext _ctx;
        protected void Application_Start()
        {
           
            //HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();
           // DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(MyRequiredAttribute), typeof(RequiredAttributeAdapter));
           // Database.SetInitializer<ApplicationDbContext>(null);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer<ApplicationDbContext>(null);
        }

       // [SessionExpire]
        protected void Session_End()
        {
//int k = 0;
           // ActionExecutingContext.Result = new RedirectResult("~/Account/Login");
            
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        public void Application_End()
        {

            string userId = System.Web.HttpContext.Current.Session["UserId"].ToString();
            //_ctx = new IdentityConnectionContext();
            //UserRoleRepository u = new UserRoleRepository(new UnitOfWork(_ctx));
            //UserRepository userRepository = new UserRepository(new UnitOfWork(_ctx), u);
            // bool result = userRepository.UpdateLoggedInStatus(userid, false);
            //if (result == true)
            //{
            //}
        }

      
    }
}
