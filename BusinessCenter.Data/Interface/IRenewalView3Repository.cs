using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data.Model;

namespace BusinessCenter.Data.Interface
{
   public interface IRenewalView3Repository
   {
       List<RenewalLicense> GetRenewData();
       IEnumerable<BblLicenseView3> FindByLicenseNumber(RenewalLicense businessLicense);
   }
}
