using BusinessCenter.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data.Models;
using BusinessCenter.Data;
using BusinessCenter.Service.Common;
namespace BusinessCenter.Service.Interface
{
    public interface ISearchService
    {


        Task<IEnumerable<SearchData>> GetSearchData(SearchServiceInput searchInput);

    //    Task<IEnumerable<SearchResultData>> FillSearchData(SearchServiceInput searchinput);
      //  Task<IQueryable<CommonData>> GetAllData(SearchServiceInput searchinput);

        Task<IQueryable<string>> CompanyName(AutoFillKeyWord searchKeyWord);
        Task<IQueryable<string>> GetFirstName(AutoFillKeyWord searchKeyWord);
        Task<IQueryable<string>> GetLastName(AutoFillKeyWord searchKeyWord);
        List<string> GetLicenceNumber(AutoFillKeyWord inputSearchKeyWord);
      //  IEnumerable<DCBC_ENTITY_MultiColumn_LOOKUP_INDEX> GetLookupAll();
       // Task<IEnumerable<SearchData>> PdfDataService(SearchServiceInput searchInput);

    }
}
