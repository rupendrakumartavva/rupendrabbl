using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace BusinessCenter.Admin.Common
{
    //public class SessionExpire : ActionFilterAttribute
    //{
    //    public override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {


    //        if (HttpContext.Current.Session["UserName"] == null)
    //        {
    //            FormsAuthentication.SignOut();
    //            filterContext.Result =
    //           new RedirectToRouteResult(new RouteValueDictionary   
    //        {  
    //         { "action", "Login" },  
    //        { "controller", "Account" },  
    //        { "returnUrl", filterContext.HttpContext.Request.RawUrl}  
    //         });

    //            return;
    //        }
    //    }

    //} 

    //public class SessionExpireFilterAttribute : ActionFilterAttribute 
    //{
    
        //public override void OnActionExecuting( ActionExecutingContext filterContext ) {
        //    HttpContext ctx = HttpContext.Current;


        //    // check if session is supported
        //    if ( ctx.Session != null ) {


        //        // check if a new session id was generated
        //        if ( ctx.Session.IsNewSession ) {


        //            // If it says it is a new session, but an existing cookie exists, then it must
        //            // have timed out
        //            string sessionCookie = ctx.Request.Headers[ "Cookie" ];
        //            if ( ( null != sessionCookie ) && ( sessionCookie.IndexOf("ASP.NET_SessionId", StringComparison.Ordinal) >= 0 ) ) {


        //                ctx.Response.Redirect ( "~/Home/Login" );
        //            }
        //        }
        //    }


        //    base.OnActionExecuting ( filterContext );
        //}

        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    HttpContext ctx = HttpContext.Current;

        //    // check  sessions here
        //    if (HttpContext.Current.Session["username"] == null)
        //    {
        //        filterContext.Result = new RedirectResult("~/Account/Login");
        //        return;
        //    }

        //    base.OnActionExecuting(filterContext);
        //}

      
//    }

    //public class SessionExpireFilterAttribute : ActionFilterAttribute
    //{

    //    public override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {
    //        HttpContext ctx = HttpContext.Current;

    //        // check if session is supported
    //        if (ctx.Session != null)
    //        {

    //            // check if a new session id was generated
    //            if (ctx.Session.IsNewSession)
    //            {

    //                // If it says it is a new session, but an existing cookie exists, then it must
    //                // have timed out
    //                string sessionCookie = ctx.Request.Headers["Cookie"];
    //                if ((null != sessionCookie) && (sessionCookie.IndexOf("ASP.NET_SessionId") >= 0))
    //                {

    //                    ctx.Response.Redirect("~/Account/Login");
    //                }
    //            }
    //        }

    //        base.OnActionExecuting(filterContext);
    //    }
    //}

    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class SessionExpireFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext context = HttpContext.Current;
            if (context.Session != null)
            {
                if (context.Session.IsNewSession)
                {
                    string sessionCookie = context.Request.Headers["Cookie"];

                    if ((sessionCookie != null) && (sessionCookie.IndexOf("ASP.NET_SessionId") >= 0))
                    {
                        FormsAuthentication.SignOut();
                        string redirectTo = "~/Account/Login";
                        if (!string.IsNullOrEmpty(context.Request.RawUrl))
                        {
                            redirectTo = string.Format("~/Account/Login?ReturnUrl={0}", HttpUtility.UrlEncode(context.Request.RawUrl));
                            filterContext.Result = new RedirectResult(redirectTo);
                            return;
                        }

                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
