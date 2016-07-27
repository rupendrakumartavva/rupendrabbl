using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BusinessCenter.Data.Implementation
{
    public class SecurityRepository : GenericRepository<SecurityQuestion>, ISecurityRepository
    {
        public SecurityRepository(IUnitOfWork context)
            : base(context)
        {
        }
        /// <summary>
        /// This method is used to retrive all Security Questions.
        /// </summary>
        /// <returns></returns>
        public  IEnumerable<SecurityQuestion> GetQuestions()
        {
            return GetAll().AsQueryable().AsNoTracking();
          
        }
    }
}