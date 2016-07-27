using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Implementation
{
    public class MasterRenewalStatusFeeRepository : GenericRepository<MasterRenewalStatusFee>, IMasterRenewalStatusFeeRepository
    {
     public MasterRenewalStatusFeeRepository(IUnitOfWork context)
         : base(context)
     { 
     
     }
     /// <summary>
     /// This method is used to get specific master renewal status fee based on status type
     /// </summary>
     /// <param name="statusType"></param>
     /// <returns>Return MasterRenewalStatusFee data</returns>
     public IEnumerable<MasterRenewalStatusFee> FindByStatus(string statusType)
     {
         var renewalStatus = FindBy(x => x.StatusType.ToUpper().Trim() == statusType.ToUpper().Trim()).AsEnumerable();
         return renewalStatus;
     }
     /// <summary>
     /// This method is used to get specific master renewal status fee bewtween the start range and end range
     /// </summary>
     /// <param name="range"></param>
     /// <returns>Return MasterRenewalStatusFee data</returns>
     public IEnumerable<MasterRenewalStatusFee> FindByRange(int range)
     {
       
         var renewalStatus = FindBy(x => x.StartRange <= range && x.EndRange >= range).AsEnumerable();
         return renewalStatus;
     }
    }
}
