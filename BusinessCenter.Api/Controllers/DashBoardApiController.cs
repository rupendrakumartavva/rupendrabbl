
using BusinessCenter.Api.Filters;
using BusinessCenter.Data.Models;
using BusinessCenter.Service.Interface;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BusinessCenter.Api.Controllers
{
   [RoutePrefix("api/DashBoard")]
   [UpdateTokenLifeTime]
    public class DashBoardApiController : ApiController
    {
        private ISearchKeywordService _keywordCount;
        private IUserManagerService _loginCount;
        public DashBoardApiController(ISearchKeywordService searchKeywordService,IUserManagerService userService)
        {

            _keywordCount = searchKeywordService;
            _loginCount = userService;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("KeywordCount")]
        public async Task<IHttpActionResult> KeywordCount(string DisplayType="ALL")
        {
            NameValueCollection KV = HttpUtility.ParseQueryString(Request.RequestUri.Query);
          //  string DisplayType="";
            DateTime FromDate = DateTime.Now.AddDays(-2);
            DateTime ToDate = DateTime.Now;
           // DateTime ToDate = Convert.ToDateTime(dt);
        var KeywordCount = await _keywordCount.GetKeywordSearchCount(FromDate, ToDate, KV.ToString());

            return Ok(KeywordCount);

        }
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("KeywordCounts")]
        //public async Task<IHttpActionResult> KeywordCounts(string DisplayType = "ALL")
        //{
        //    //  string DisplayType="";
        //    DateTime FromDate = DateTime.Now.AddDays(-2);
        //    DateTime ToDate = DateTime.Now;
        //    var KeywordCount = await _keywordCount.GetKeywordSearchCount(FromDate, ToDate, DisplayType);

        //    return Ok(KeywordCount);

        //}
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("LoginCount")]
        public async Task<IHttpActionResult> LoginCount()
        {
            DateTime FromDate = DateTime.Now.AddDays(-2);
            DateTime ToDate = DateTime.Now;
            var KeywordCount = await _loginCount.GetLoginHistoryCount(FromDate, ToDate);

            return Json(KeywordCount);

        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("AdminLoginCount")]
        public async Task<IHttpActionResult> AdminLoginCount()
        {
            DateTime FromDate = DateTime.Now.AddDays(-2);
            DateTime ToDate = DateTime.Now;
            var KeywordCount = await _loginCount.GetLoginHistoryCount(FromDate, ToDate);
            var AdminKeywordCount = KeywordCount.Where(x => x.UserRole != "Super Admin");
            return Json(AdminKeywordCount);

        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("IndividualCount")]
        public async Task<IHttpActionResult> GetDashBoardInvdvidualcountsCount()
        {
            //DateTime FromDate = DateTime.Now.AddDays(-2);
            //DateTime ToDate = DateTime.Now;
            var KeywordCount = await _keywordCount.GetDashBoardInvdvidualcountsCount();

            return Ok(KeywordCount);

        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("RegulatorCount")]
        public async Task<IHttpActionResult> GetDashBoardRegulatorCount()
        {
            var KeywordCount = await _keywordCount.GetDashBoardRegulatorCount();
            return Ok(KeywordCount);
        }
   }
}
