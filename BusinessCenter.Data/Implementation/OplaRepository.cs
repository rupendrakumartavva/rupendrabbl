using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessCenter.Data.Implementation
{
    public class OplaRepository : GenericRepository<DCBC_ENTITY_OPLA>, IOplaRepository
    {
        public OplaRepository(IUnitOfWork context)
            : base(context)
        {
        }

        /// <summary>
        /// This method is used to retrive enitre Professional License License(OPLA) records
        /// </summary>
        /// <returns>List of Opla Data</returns>
        public IEnumerable<DCBC_ENTITY_OPLA> GetOplaLookupAll()
        {
            return GetAll().AsQueryable();
        }
        /// <summary>
        /// This method is used to get specific opla data based on user inputs
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>Return dcbc entity</returns>
        public new IQueryable<DCBC_ENTITY_OPLA> FindBy(System.Linq.Expressions.Expression<Func<DCBC_ENTITY_OPLA, bool>> predicate)
        {
            try
            {
                var query = DbSet.Where(predicate).AsQueryable();
                return query;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}