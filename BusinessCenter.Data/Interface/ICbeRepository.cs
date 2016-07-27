using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
    public interface ICbeRepository
    {
        IEnumerable<DCBC_ENTITY_CBE> GetCbeLookupAll();

        // IEnumerable<DCBC_ENTITY_CBE> FindByID(int enitityid);

        // IQueryable<DCBC_ENTITY_CBE> FindBy(Expression<Func<DCBC_ENTITY_CBE, bool>> predicate);
        IQueryable<DCBC_ENTITY_CBE> FindBy(System.Linq.Expressions.Expression<Func<DCBC_ENTITY_CBE, bool>> predicate);
    }
}