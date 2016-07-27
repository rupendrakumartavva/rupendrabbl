using System;
using System.Linq;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;

namespace BusinessCenter.Data.Implementation
{
    public class KeywordDetailsRepository : GenericRepository<KeywordDetails>, IKeywordDetailsRepository
    {
        public KeywordDetailsRepository(IUnitOfWork context)
            : base(context)
        {
        }

        public IQueryable<KeywordDetails> KeyDetailsGetAll()
          {
              return GetAll().AsQueryable();
          }
        /// <summary>
        /// This method is used to display keyword count, given dynamic search criteria.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>particular Keyworddetails data</returns>
          public new IQueryable<KeywordDetails> FindBy(System.Linq.Expressions.Expression<Func<KeywordDetails, bool>> predicate)
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
        /// <summary>
        /// This method is used to Add New keyword searched on a specific day based on keyid,count and created date.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
          public void AddKeyWord(KeywordDetails entity)
          {
              Add(entity);
              Save();
          }
        /// <summary>
        /// This method is used to Save the Data to the dta base
        /// </summary>
          public virtual void SaveChanges()
          {
              Save();
          }
    }
}