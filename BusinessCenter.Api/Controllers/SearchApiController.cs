using BusinessCenter.Api.App_Start;
using BusinessCenter.Api.Filters;
using BusinessCenter.Api.Models;
using BusinessCenter.Data.Models;
using BusinessCenter.Service.Common;
using BusinessCenter.Service.Interface;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.SessionState;

namespace BusinessCenter.Api.Controllers
{
    [System.Web.Http.RoutePrefix("api/Search")]
    [UpdateTokenLifeTime]
    public class SearchApiController : ApiController
    {
        ////private static IEnumerable<CommonData> _abraFinalData;
        ////private static IEnumerable<CommonData> _bblFinalData;
        ////private static IEnumerable<CommonData> _cbeFinalData;
        ////private static IEnumerable<CommonData> _oplaFinalData;
        ////private static IEnumerable<CommonData> _FinalData;

        private readonly ISearchService _searchService;

        public SearchApiController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        /// <summary>
        /// Using Isearchservice
        /// get all the data with search criteria and fill the count and return the count of the each regulatory count
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="searchType"></param>
        /// <param name="searchInput"></param>
        /// <returns></returns>
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [DeflateCompression]
        [System.Web.Http.Route("SelectAll")]
        public async Task<IHttpActionResult> SearchData(SearchServiceInput searchInput)
        {
            var obj = new Object();
            var lockWasTaken = false;
            var temp = obj;
            try
            {
                Monitor.Enter(temp, ref lockWasTaken);
                searchInput.CompanyName = searchInput.CompanyName == null ? "" : searchInput.CompanyName.ToUpper().Replace(" AND ", " ").Replace(" & ", " ").Replace("&QUOT;", " ");
                searchInput.LicenseName = searchInput.LicenseName == null ? "" : searchInput.LicenseName.ToUpper().Replace(" AND ", " ").Replace(" & ", " ").Replace("&QUOT;", " ");
                searchInput.FirstName = searchInput.FirstName == null ? "" : searchInput.FirstName.ToUpper().Replace(" AND ", " ").Replace(" & ", " ").Replace("&QUOT;", " ");
                searchInput.LastName = searchInput.LastName == null ? "" : searchInput.LastName.ToUpper().Replace(" AND ", " ").Replace(" & ", " ").Replace("&QUOT;", " ");
                var result = await GetSearchResultData(searchInput);

                if (result == null)
                {
                    return NotFound();
                }

                return Json(result);
            }
            finally
            {
                if (lockWasTaken)
                {
                    Monitor.Exit(temp);
                }
            }
        }

        private async Task<List<SearchServiceInput>> SearchServiceInputList(SearchServiceInput searchInput)
        {
            var resultSearchInput = new List<SearchServiceInput>();
            searchInput.CompanyName = searchInput.CompanyName == null ? "" : searchInput.CompanyName.ToUpper().Replace(" AND ", " ").Replace(" & ", " ").Replace("&QUOT;", " ");
            searchInput.LicenseName = searchInput.LicenseName == null ? "" : searchInput.LicenseName.ToUpper().Replace(" AND ", " ").Replace(" & ", " ").Replace("&QUOT;", " ");
            searchInput.FirstName = searchInput.FirstName == null ? "" : searchInput.FirstName.ToUpper().Replace(" AND ", " ").Replace(" & ", " ").Replace("&QUOT;", " ");
            searchInput.LastName = searchInput.LastName == null ? "" : searchInput.LastName.ToUpper().Replace(" AND ", " ").Replace(" & ", " ").Replace("&QUOT;", " ");
            resultSearchInput.Add(searchInput);
            return await Task.FromResult(resultSearchInput);
        }

        public async Task<IEnumerable<SearchData>> GetSearchResultData(SearchServiceInput searchInput)
        {
            return await _searchService.GetSearchData(searchInput).ConfigureAwait(false);
        }

        /// <summary>
        /// get the filter final data to bind the grid as json
        /// </summary>
        /// <param name="searchInput"></param>
        /// <returns></returns>
        //[System.Web.Http.Authorize]
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("All")]
        //[DeflateCompression]
        //public async Task<IHttpActionResult> SearchAll(SearchInput searchInput)
        //{
        //    var searchServiceInput = new SearchServiceInput();
        //    searchServiceInput.InjectFrom(searchInput);
        //    _FinalData = new List<CommonData>();

        //    _FinalData = await Task.Run(() => _searchService.GetAllData(searchServiceInput));
        //    var finaldata = _FinalData.ToList();
        //    return Json(finaldata);
        //}

        /// <summary>
        /// auto complete text box for the company name
        /// </summary>
        /// <param name="searchInput"></param>
        /// <returns></returns>
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("Company")]
        [DeflateCompression]
        public async Task<IHttpActionResult> CompanyNames(AutoFillKeyWord searchInput)
        {
            searchInput.SearchKeyWord = searchInput.SearchKeyWord == null ? "" : searchInput.SearchKeyWord.ToUpper().Replace(" AND ", " ").Replace(" & ", " ").Replace("&QUOT;", " ");
            var getData = await Task.Run(() => _searchService.CompanyName(searchInput));
            return Json(getData.ToList());
        }

        /// <summary>
        /// auto complete text box for the first name
        /// </summary>
        /// <param name="searchInput"></param>
        /// <returns></returns>
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("FirstName")]
        [DeflateCompression]
        public async Task<IHttpActionResult> FirstNames(AutoFillKeyWord searchInput)
        {
            searchInput.SearchKeyWord = searchInput.SearchKeyWord == null ? "" : searchInput.SearchKeyWord.ToUpper().Replace(" AND ", " ").Replace(" & ", " ").Replace("&QUOT;", " ");
            var getData = await Task.Run(() => _searchService.GetFirstName(searchInput));
            return Json(getData.ToList());
        }

        /// <summary>
        /// auto complete text box for the last name
        /// </summary>
        /// <param name="searchInput"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("LastName")]
        [DeflateCompression]
        public async Task<IHttpActionResult> LastNames(AutoFillKeyWord searchInput)
        {
            searchInput.SearchKeyWord = searchInput.SearchKeyWord == null ? "" : searchInput.SearchKeyWord.ToUpper().Replace(" AND ", " ").Replace(" & ", " ").Replace("&QUOT;", " ");
            var getData = await Task.Run(() => _searchService.GetLastName(searchInput));
            return Json(getData.ToList());
        }

        /// <summary>
        /// auto complete text box for the license number
        /// </summary>
        /// <param name="searchInput"></param>
        /// <returns></returns>
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("LicenceNumber")]
        [DeflateCompression]
        public IHttpActionResult LicenceNumbers(AutoFillKeyWord searchInput)
        {
            searchInput.SearchKeyWord = searchInput.SearchKeyWord == null ? "" : searchInput.SearchKeyWord.ToUpper().Replace(" AND ", " ").Replace(" & ", " ").Replace("&QUOT;", " ");
            var getData = _searchService.GetLicenceNumber(searchInput);
            return Json(getData.ToList());
        }
    }
}