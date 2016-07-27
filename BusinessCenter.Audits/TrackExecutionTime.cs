using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Audits
{
    public class TrackExecutionTime : ActionFilterAttribute, IExceptionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string message = "\n" + filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "->" + filterContext.ActionDescriptor.ActionName + "->On action Executing \t" + DateTime.Now.ToString() + "\n";
            LogExecutionTime(message);
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string message = "\n" + filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "->" + filterContext.ActionDescriptor.ActionName + "->On action Executed \t" + DateTime.Now.ToString() + "\n";
            LogExecutionTime(message);
        }
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            string message = "\n" + filterContext.RouteData.Values["controller"].ToString() + "->" + filterContext.RouteData.Values["action"].ToString() + "->On Result Executing \t" + DateTime.Now.ToString() + "\n";
            LogExecutionTime(message);
        }
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            string message = "\n" + filterContext.RouteData.Values["controller"].ToString() + "->" + filterContext.RouteData.Values["action"].ToString() + "->On Result Executed \t" + DateTime.Now.ToString() + "\n";
            LogExecutionTime(message);
            LogExecutionTime("---------------------------------------------------------------");
        }
        public void OnException(ExceptionContext filterContext)
        {
            string message = "\n" + filterContext.RouteData.Values["controller"].ToString() + "->" + filterContext.RouteData.Values["action"].ToString() + "->" + filterContext.Exception.Message + "\t" + DateTime.Now.ToString() + "\n";
            LogExecutionTime(message);
            LogExecutionTime("---------------------------------------------------------------");
        }
        private void LogExecutionTime(string data)
        {
            File.AppendAllText(HttpContext.Current.Server.MapPath("~/Data/Data.txt"), data);
        }
    }
}
