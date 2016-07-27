using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
    public interface IOplaRepository
    {
        IEnumerable<DCBC_ENTITY_OPLA> GetOplaLookupAll();

        //   IEnumerable<DCBC_ENTITY_OPLA> FindByID(int enitityid);
        //   IQueryable<DCBC_ENTITY_OPLA> FindBy(Expression<Func<DCBC_ENTITY_OPLA, bool>> predicate);
        IQueryable<DCBC_ENTITY_OPLA> FindBy(System.Linq.Expressions.Expression<Func<DCBC_ENTITY_OPLA, bool>> predicate);
    }
}