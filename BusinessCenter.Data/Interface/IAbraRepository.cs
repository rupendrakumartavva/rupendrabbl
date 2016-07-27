using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
    public interface IAbraRepository
    {
        IEnumerable<DCBC_ENTITY_ABRA> GeArbraLookupAll();

        IQueryable<DCBC_ENTITY_ABRA> FindBy(System.Linq.Expressions.Expression<Func<DCBC_ENTITY_ABRA, bool>> predicate);
    }
}