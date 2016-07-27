using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Models;
using BusinessCenter.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Service.Implementation
{
    public class SearchKeywordService : ISearchKeywordService
    {
        protected ISearchKeywordRepository _repository;

        /// <summary>
        ///
        /// </summary>
        /// <param name="repo"></param>
        public SearchKeywordService(ISearchKeywordRepository repo)
        {
            _repository = repo;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="DisplayType"></param>
        /// <returns></returns>
        public async Task<IQueryable<SearchKeywordModel>> GetKeywordSearchCount(DateTime FromDate, DateTime ToDate, string DisplayType)
        {
            var commandata = await _repository.GetKeywordSearchCount(FromDate, ToDate, DisplayType);
            return commandata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="DisplayType"></param>
        /// <returns></returns>
        public async Task<List<SearchKeywordModel>> AdminKeywordSearchCount(DateTime FromDate, DateTime ToDate, string DisplayType)
        {
            var commandata = await _repository.AdminKeywordSearchCount(FromDate, ToDate, DisplayType);
            return commandata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<IQueryable<KewordsCount>> GetDashBoardInvdvidualcountsCount()
        {
            var commandata = await _repository.GetDashBoardInvdvidualcountsCount();
            return commandata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<IQueryable<RegulatorCount>> GetDashBoardRegulatorCount()
        {
            var commandata = await _repository.GetDashBoardRegulatorCount();
            return commandata;
        }
    }
}