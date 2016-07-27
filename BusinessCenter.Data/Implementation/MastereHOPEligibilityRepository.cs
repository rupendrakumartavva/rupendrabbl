using System.Collections.Generic;
using System.Linq;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;

namespace BusinessCenter.Data.Implementation
{
    public class MastereHopEligibilityRepository : GenericRepository<MastereHOPEligibility>, IMastereHopEligibilityRepository
    {
        public MastereHopEligibilityRepository(IUnitOfWork context)
            : base(context)
        {
        }
        /// <summary>
        /// This method is used to display the ehop conditions
        /// </summary>
        /// <returns>list of ehop conditions</returns>
        public IEnumerable<MastereHOPEligibility> GeMastereHopEligibility()
        {
          
                var ehop = GetAll().AsQueryable();
                return ehop;
         
        }
    }
}
