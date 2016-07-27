using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
    public interface ICorpRespository
    {
        IEnumerable<DCBC_ENTITY_CORP> GeCorpLookupAll();
       // IEnumerable<DCBC_ENTITY_CORP> FindByID(int enitityid);
       IQueryable<DCBC_ENTITY_CORP> FindBy(Expression<Func<DCBC_ENTITY_CORP, bool>> predicate);
        IEnumerable<DCBC_ENTITY_CORP> FindByFileNumber(string fileNumber);
        string CorpOnlineSearch(CorporationDetails corporationdetails);
    }
}
