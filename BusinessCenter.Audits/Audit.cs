using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using BusinessCenter.Data;

namespace BusinessCenter.Audits
{
    public class Audit
    {
        //A new SessionId that will be used to link an entire users "Session" of Audit Logs
        //together to help identifer patterns involving erratic behavior
        public string SessionId { get; set; }
        public Guid AuditId { get; set; }
        public string IpAddress { get; set; }
        public string UserName { get; set; }
        public string UrlAccessed { get; set; }
        public DateTime TimeAccessed { get; set; }
        //A new Data property that is going to store JSON string objects that will later be able to
        //be deserialized into objects if necessary to view details about a Request
        public string Data { get; set; }

       
       
    }

    //public class AuditingContext : DbContext
    //{
    //    public DbSet<Audit> AuditRecords { get; set; }
    //}

    public class AuditAttribute : ActionFilterAttribute
    {
        //Our value to handle our AuditingLevel
        public int AuditingLevel { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Stores the Request in an Accessible object
            var request = filterContext.HttpContext.Request;

            //Generate the appropriate key based on the user's Authentication Cookie
            //This is overkill as you should be able to use the Authorization Key from
            //Forms Authentication to handle this. 
        //    var sessionIdentifier = string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(request.Cookies[FormsAuthentication.FormsCookieName].Value)).Select(s => s.ToString("x2")));

            //Generate an audit
            if (filterContext.HttpContext.Session != null)
            {
                Audit audit = new Audit()
                {
                    //   SessionId = sessionIdentifier,
                    AuditId = Guid.NewGuid(),
                    IpAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress,
                    UrlAccessed = request.RawUrl,
                    TimeAccessed = DateTime.UtcNow,
                    UserName = (string)((request.IsAuthenticated) ? filterContext.HttpContext.Session["UserName"] : "Anonymous"),
                    Data = SerializeRequest(request)
                };
                var rd = new AuditRecord();
                {
                    rd.AuditId = audit.AuditId.ToString();

                    rd.IpAddress = audit.IpAddress;
                    rd.UrlAccessed = audit.UrlAccessed;
                    rd.TimeAccessed = DateTime.UtcNow;
                    rd.UserName = audit.UserName;
                    rd.Data = audit.Data;
                }
                //Stores the Audit in the Database
                IdentityConnectionContext context = new IdentityConnectionContext();
                context.AuditRecord.Add(rd);
                context.SaveChanges();
            }

            base.OnActionExecuting(filterContext);
        }

        //This will serialize the Request object based on the level that you determine
        private string SerializeRequest(HttpRequestBase request)
        {
            switch (AuditingLevel)
            {
                //No Request Data will be serialized
                case 0:
                default:
                    return "";
                //Basic Request Serialization - just stores Data
                case 1:
                    return Json.Encode(new { request.Cookies, request.Headers, request.Files });
                //Middle Level - Customize to your Preferences
                case 2:
                    return Json.Encode(new { request.Cookies, request.Headers, request.Files, request.Form, request.QueryString, request.Params });
                //Highest Level - Serialize the entire Request object
                case 3:
                    //We can't simply just Encode the entire request string due to circular references as well
                    //as objects that cannot "simply" be serialized such as Streams, References etc.
                    //return Json.Encode(request);
                    return Json.Encode(new { request.Cookies, request.Headers, request.Files, request.Form, request.QueryString, request.Params });
            }
        }
    }
}