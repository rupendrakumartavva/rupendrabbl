using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessCenter.Data.Implementation
{
    public class AbraRepository : GenericRepository<DCBC_ENTITY_ABRA>, IAbraRepository
    {
        public AbraRepository(IUnitOfWork context)
            : base(context)
        {
        }

        /// <summary>
        /// This method is used to retrive enitre Alcoholic Beverage License(ABRA) records.
        /// </summary>
        /// <returns>All ABRA Records</returns>
        public IEnumerable<DCBC_ENTITY_ABRA> GeArbraLookupAll()
        {
            return GetAll().AsQueryable();
        }
        /// <summary>
        /// This method is used to retrieve a particular ABRA record based on any dynamic column.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public new IQueryable<DCBC_ENTITY_ABRA> FindBy(System.Linq.Expressions.Expression<Func<DCBC_ENTITY_ABRA, bool>> predicate)
        {
            try
            {
                var corpdata = DbSet.Where(predicate).AsQueryable();
                return corpdata;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}