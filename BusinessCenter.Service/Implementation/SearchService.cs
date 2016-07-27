using BusinessCenter.Data;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Models;
using BusinessCenter.Service.Common;
using BusinessCenter.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessCenter.Service.Implementation
{
    public class SearchService : ISearchService
    {
        public ISearchRepository Repository;
        private SearchServiceInputs _searchServiceInput;

        /// <summary>
        /// Initializes a new Instance of SecurityQuestionService
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="searchServiceInput"></param>
        public SearchService(ISearchRepository repo, SearchServiceInputs searchServiceInput)
        {
            Repository = repo;
            _searchServiceInput = searchServiceInput;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="searchInput"></param>
        /// <returns></returns>
        private SearchServiceInputs ServiceInput(SearchServiceInput searchInput)
        {
            var serviceInput = new SearchServiceInputs
            {
                SearchString = searchInput.SearchString,
                CompanyName = (searchInput.CompanyName == null ? "" : Regex.Replace(searchInput.CompanyName, "[^a-zA-Z0-9*]+", "")),
                LicenseName = (searchInput.LicenseName == null ? "" : Regex.Replace(searchInput.LicenseName, "[^a-zA-Z0-9*]+", "")),
                FirstName = (searchInput.FirstName == null ? "" : Regex.Replace(searchInput.FirstName, "[^a-zA-Z0-9*]+", "")),
                LastName = (searchInput.LastName == null ? "" : Regex.Replace(searchInput.LastName, "[^a-zA-Z0-9*]+", "")),
                SearchType = searchInput.SearchType,
                PageIndex = searchInput.PageIndex,
                PageSize = searchInput.PageSize,
                Userid = searchInput.Userid,
                IsChanged = searchInput.IsChanged,
                DisplayType = searchInput.DisplayType,
                KeyType = searchInput.KeyType
            };
            return serviceInput;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="searchInput"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SearchData>> GetSearchData(SearchServiceInput searchInput)
        {
            _searchServiceInput = ServiceInput(searchInput);
            var searchData = await Repository.GetSearchData(_searchServiceInput);
            return searchData;
        }

        //public async Task<IEnumerable<SearchResultData>> FillSearchData(SearchServiceInput searchinput)
        //{
        //    _searchServiceInput = ServiceInput(searchinput);
        //    var filldata = await Repository.FillSearchData(_searchServiceInput);
        //    return filldata.ToList();
        //}

        /// <summary>
        ///
        /// </summary>
        /// <param name="searchInput"></param>
        /// <returns></returns>
        //public async Task<IQueryable<CommonData>> GetAllData(SearchServiceInput searchInput)
        //{
        //    _searchServiceInput = ServiceInput(searchInput);
        //    var commandata = await Repository.GetAllData(_searchServiceInput);
        //    return commandata;
        //}


        /// <summary>
        ///
        /// </summary>
        /// <param name="inputSearchKeyWord"></param>
        /// <returns></returns>
        public async Task<IQueryable<string>> CompanyName(AutoFillKeyWord inputSearchKeyWord)
        {
            inputSearchKeyWord.SearchKeyWord = (inputSearchKeyWord.SearchKeyWord == null
            ? ""
            : Regex.Replace(inputSearchKeyWord.SearchKeyWord, "[^a-zA-Z0-9*]+", ""));
            var commandata = await Repository.GetCompanyName(inputSearchKeyWord);
            return commandata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="inputSearchKeyWord"></param>
        /// <returns></returns>
        public Task<IQueryable<string>> GetFirstName(AutoFillKeyWord inputSearchKeyWord)
        {
            inputSearchKeyWord.SearchKeyWord = (inputSearchKeyWord.SearchKeyWord == null
                ? ""
                : Regex.Replace(inputSearchKeyWord.SearchKeyWord, "[^a-zA-Z0-9*]+", ""));
            var commandata = Repository.GetFirstName(inputSearchKeyWord);
            return commandata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="inputSearchKeyWord"></param>
        /// <returns></returns>
        public Task<IQueryable<string>> GetLastName(AutoFillKeyWord inputSearchKeyWord)
        {
            inputSearchKeyWord.SearchKeyWord = (inputSearchKeyWord.SearchKeyWord == null
            ? ""
            : Regex.Replace(inputSearchKeyWord.SearchKeyWord, "[^a-zA-Z0-9*]+", ""));
            var commandata = Repository.GetLastName(inputSearchKeyWord);
            return commandata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="inputSearchKeyWord"></param>
        /// <returns></returns>
        public List<string> GetLicenceNumber(AutoFillKeyWord inputSearchKeyWord)
        {
            inputSearchKeyWord.SearchKeyWord = (inputSearchKeyWord.SearchKeyWord == null
            ? ""
            : Regex.Replace(inputSearchKeyWord.SearchKeyWord, "[^a-zA-Z0-9*]+", ""));
            var commandata = Repository.GetLicenceNumber(inputSearchKeyWord);
            return commandata;
        }

        //public IEnumerable<DCBC_ENTITY_MultiColumn_LOOKUP_INDEX> GetLookupAll()
        //{
        //    var filldata = Repository.GetMultiColumnLookupAll();
        //    return filldata.ToList();
        //}

        //public async Task<IEnumerable<SearchData>> PdfDataService(SearchServiceInput searchInput)
        //{
        //    _searchServiceInput = ServiceInput(searchInput);
        //    var searchData = await Repository.GetSearchPdfData(_searchServiceInput);
        //    return searchData;
        //}
    }
}