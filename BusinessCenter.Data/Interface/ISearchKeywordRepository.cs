using BusinessCenter.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
  public interface ISearchKeywordRepository
    {
      Task<IQueryable<SearchKeywordModel>> GetKeywordSearchCount(DateTime fromDate, DateTime toDate, string displayType);
      Task<List<SearchKeywordModel>> AdminKeywordSearchCount(DateTime fromDate, DateTime toDate, string displayType);
      Task<IQueryable<KewordsCount>> GetDashBoardInvdvidualcountsCount();
      Task<IQueryable<RegulatorCount>> GetDashBoardRegulatorCount();
      IQueryable<KeywordMaster> FindBy(Expression<Func<KeywordMaster, bool>> predicate);

      void  AddKeyWord(KeywordMaster entity);
     // void SaveChanges();
    }
}
