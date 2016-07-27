using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using BusinessCenter.Api.Models;
using BusinessCenter.Data.Models;
using BusinessCenter.Service.Interface;
using Omu.ValueInjecter;
using BusinessCenter.Api.Common;
using BusinessCenter.Api.Filters;

namespace BusinessCenter.Api.Controllers
{
    [RoutePrefix("api/MySavedResults")]
    [UpdateTokenLifeTime]
    public class MySavedResultController : BaseApiController
    {

        private  IMyServiceDetails _searchService;

        public MySavedResultController(IMyServiceDetails searchService)
        {

            _searchService = searchService;

        }
        /// <summary>
        /// user want to save the particular regulatory as wish list 
        /// </summary>
        /// <param name="userSaveList"></param>
        /// <returns></returns>
        [Authorize]

        [Route("AddToMyList")]
        [HttpPost]
        public IHttpActionResult AddToMySavedList(UserServiceModel userSaveList)
        {

            var result = _searchService.UserServiceAdd(userSaveList);
            if (result == 1)
            {
                return Json(new { status = ResultMessages.success });
            }
            return Json(new { status = ResultMessages.alreadyexist });
        }

        /// <summary>
        /// user saved data of the each regulatory count
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Authorize]
        [Route("mycount")]
        [HttpPost]
        public IHttpActionResult MySavedListCount(DashBoardUserService userId)
        {

            var count = _searchService.UserSaveListCount(userId.UserId);
            return Json(new { status = count.ToString() });

        }
        /// <summary>
        ///  user saved data of the each regulatory count
        /// </summary>
        /// <param name="searchInput"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("SelectAll")]
        public async Task<IHttpActionResult> SearchData(UserServiceModel searchInput)
        {


            //_searchService.GetSearchData(searchInput);
            var result = _searchService.GetCount(searchInput);

            var searchDataViewModels = result as SearchDataViewModel[] ?? result.ToArray();
            if (result == null)
            {
                return NotFound();
            }
            return await Task.FromResult( Json(searchDataViewModels));
        }
        /// <summary>
        ///  user saved data of the each regulatory final list of data
        /// </summary>
        /// <param name="searchInput"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("All")]
        public async Task<IHttpActionResult> SearchAll(UserServiceModel searchInput)
        {

            List<CommonData> finalData = new List<CommonData>();
            finalData = _searchService.GetAllData(searchInput).ToList();
            var finaldata = finalData.ToList();
            return await Task.FromResult(Json(finaldata));

        }
        /// <summary>
        ///  user delete the  particular regulatory entity data of that user
        /// </summary>
        /// <param name="searchInput"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("Deletesingle")]
        public IHttpActionResult DeleteUserService(UserServiceModel searchInput)
        {
            //var searchServiceInput = new SearchServiceInput();
            //searchServiceInput.InjectFrom(searchInput);

            _searchService.DeleteUserService(searchInput);

            return Json(new { status = ResultMessages.success });

        }
        /// <summary>
        ///  user delete all regulatory entities of particular user
        /// </summary>
        /// <param name="searchInput"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]

        [Route("DeleteAll")]
        public IHttpActionResult DeleteUserService(DeleteServiceSingle singleUser)
        {

            _searchService.DeleteSingleUerService(singleUser.UserId);
            return Json(new { status = ResultMessages.success });


        }
        /// <summary>
        /// user delete selected regulatory entities of particular user
        /// </summary>
        /// <param name="searchInput"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("MultipleDelete")]
        public IHttpActionResult MultipleDeleteUserService(List<UserServiceModel> searchInput)
        {

            _searchService.DeleteUserService(searchInput);
            return Json(new { status = ResultMessages.success });


        }
    }
}
