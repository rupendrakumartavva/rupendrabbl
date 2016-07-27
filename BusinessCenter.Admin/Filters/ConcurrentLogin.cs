
using BusinessCenter.Data;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BusinessCenter.Admin.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ConcurrentLogin : FilterAttribute, IActionFilter
    {
        private IdentityConnectionContext _ctx;
        string userName = null;
        public  void OnActionExecuted(ActionExecutedContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (filterContext.Exception == null)
            {
                if (BusinessCenter.Admin.Common.BusinessCenterSession.BusinessCenterUserName != string.Empty)
                {
                    userName = filterContext.HttpContext.User.Identity.Name;
                    _ctx = new IdentityConnectionContext();
                    UserRoleRepository u = new UserRoleRepository(new UnitOfWork(_ctx));
                    UserRepository userRepository = new UserRepository(new UnitOfWork(_ctx), u);
                    var userdetails = userRepository.FindByLoginUsername(userName).ToList();
                    string currentsession = BusinessCenter.Admin.Common.BusinessCenterSession.BusinessCenterLoginUserSessionId.ToString();
                    if (userdetails.Any())
                    {
                        var logeduserdetails = userdetails.FirstOrDefault();

                        string loggedsession = (logeduserdetails.LoginSessionId ?? "DCRA").ToString();
                        if (loggedsession == "DCRA")
                        {
                                    ctx.Session["Login"] = null;
                                    ctx.Session["UserName"] = null;
                                    ctx.Session["count"] = 0;
                                    BusinessCenter.Admin.Common.BusinessCenterSession.BusinessCenterLoginUserSessionId = null;
                                    BusinessCenter.Admin.Common.BusinessCenterSession.BusinessCenterUserName = null;
                                    //Userdetails userdetail = new Userdetails();
                                    //userdetail.UserName = userName.Trim();
                                    //userdetail.IsLogedIn = false;
                                    //userdetail.SessionId = currentsession;
                                    // userRepository.UpdateLoggedTime(userdetail);
                                    filterContext.Result = new RedirectToRouteResult(
                                             new RouteValueDictionary {
                                { "Controller", "Account" },
                                { "Action", "login" }
                                });
                        }
                        else
                        if (currentsession.ToUpper().Trim() != loggedsession.ToUpper().Trim())
                        {
                            ctx.Session["Login"] = null;
                            ctx.Session["UserName"] = null;
                            ctx.Session["count"] = 0;
                            BusinessCenter.Admin.Common.BusinessCenterSession.BusinessCenterLoginUserSessionId = null;
                            BusinessCenter.Admin.Common.BusinessCenterSession.BusinessCenterUserName = null;
                            //Userdetails userdetail = new Userdetails();
                            //userdetail.UserName = userName.Trim();
                            //userdetail.IsLogedIn = false;
                            //userdetail.SessionId = currentsession;
                            // userRepository.UpdateLoggedTime(userdetail);
                            filterContext.Result = new RedirectToRouteResult(
                                     new RouteValueDictionary {
                        { "Controller", "Account" },
                        { "Action", "SessionExpiry" }
                        });
                        }
                    }
                    }
                    else
                    {
                        ctx.Session["Login"] = null;
                        ctx.Session["UserName"] = null;
                        ctx.Session["count"] = 0;
                        BusinessCenter.Admin.Common.BusinessCenterSession.BusinessCenterLoginUserSessionId = null;
                        BusinessCenter.Admin.Common.BusinessCenterSession.BusinessCenterUserName = null;
                        //Userdetails userdetail = new Userdetails();
                        //userdetail.UserName = userName.Trim();
                        //userdetail.IsLogedIn = false;
                        //userdetail.SessionId = currentsession;
                        // userRepository.UpdateLoggedTime(userdetail);
                        filterContext.Result = new RedirectToRouteResult(
                                 new RouteValueDictionary {
                        { "Controller", "Account" },
                        { "Action", "login" }
                        });
                    }
                    //  bool result = userRepository.UpdateLoggedTime(userName);
               

            }
        }

        public  void OnActionExecuting(ActionExecutingContext filterContext)
        {
           
        }
    }
}