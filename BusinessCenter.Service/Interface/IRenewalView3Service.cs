using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data;
using BusinessCenter.Data.Model;

namespace BusinessCenter.Service.Interface
{
   public interface IRenewalView3Service
   {
       List<RenewalLicense> GetRenewData();
       IEnumerable<BblLicenseView3> FindByLicenseNumber(RenewalLicense businessLicense);
   }
}
