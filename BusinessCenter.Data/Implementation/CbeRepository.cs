using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessCenter.Data.Implementation
{
    public class CbeRepository : GenericRepository<DCBC_ENTITY_CBE>, ICbeRepository
    {
        public CbeRepository(IUnitOfWork context)
            : base(context)
        {
        }

        /// <summary>
        /// This method is used to retrive enitre Certified Business Enterprise(CBE) records.
        /// </summary>
        /// <returns>All CBE records</returns>
        public IEnumerable<DCBC_ENTITY_CBE> GetCbeLookupAll()
        {
            return GetAll().AsQueryable();
        }
        /// <summary>
        /// This method is used to retrieve a particular CBE record based on any dynamic column for single value.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public new IQueryable<DCBC_ENTITY_CBE> FindBy(System.Linq.Expressions.Expression<Func<DCBC_ENTITY_CBE, bool>> predicate)
        {
            try
            {
                var corpdata = DbSet.Where(predicate).AsQueryable();
                return corpdata;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in get cbe",ex);
            }
        }
    }
}