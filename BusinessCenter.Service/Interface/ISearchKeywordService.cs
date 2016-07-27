using BusinessCenter.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Service.Interface
{
  public interface ISearchKeywordService
    {
      Task<IQueryable<SearchKeywordModel>> GetKeywordSearchCount(DateTime FromDate, DateTime ToDate, string DisplayType);
      Task<List<SearchKeywordModel>> AdminKeywordSearchCount(DateTime FromDate, DateTime ToDate, string DisplayType);
      Task<IQueryable<KewordsCount>> GetDashBoardInvdvidualcountsCount();
      Task<IQueryable<RegulatorCount>> GetDashBoardRegulatorCount();
    }
}
