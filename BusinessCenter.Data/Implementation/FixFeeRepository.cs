using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using System.Collections.Generic;
using System.Linq;

namespace BusinessCenter.Data.Implementation
{
    public class FixFeeRepository : GenericRepository<FixFee>, IFixFeeRepository
    {
        public FixFeeRepository(IUnitOfWork context)
            : base(context)
        {
        }
        /// <summary>
        /// This method is used to Get All Fixfee(RAO Fee, Endorsement Fee, Application Fee,eHop Fee and Trade Reg Fee) data
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FixFee> AllFixFees()
        {
            return GetAll().AsQueryable();
        }
    }
}
