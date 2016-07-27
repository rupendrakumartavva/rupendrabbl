using BusinessCenter.Data.Common;
using BusinessCenter.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
    public interface ISearchRepository
    {
        Task<ICollection<SearchData>> GetSearchData(SearchServiceInputs searchinput);

        //    Task<IEnumerable<SearchResultData>> FillSearchData(SearchServiceInputs searchinput);

       Task<IQueryable<CommonData>> GetAllData(SearchServiceInputs searchinput);

        Task<IQueryable<string>> GetCompanyName(AutoFillKeyWord searchKeyWord);

        Task<IQueryable<string>> GetFirstName(AutoFillKeyWord searchKeyWord);

        Task<IQueryable<string>> GetLastName(AutoFillKeyWord searchKeyWord);

        List<string> GetLicenceNumber(AutoFillKeyWord inputSearchKeyWord);

   //     IEnumerable<DCBC_ENTITY_MultiColumn_LOOKUP_INDEX> GetMultiColumnLookupAll();

       // Task<IEnumerable<SearchData>> GetSearchPdfData(SearchServiceInputs searchinput);
    }
}