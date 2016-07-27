using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data.Model;

namespace BusinessCenter.Data.Interface
{
   public interface IBusinessInformationRepository
   {
       IEnumerable<BblLicenseView> GetAllViewData();
       List<BusinessLicense> GetSubmissionData();
       IEnumerable<BblLicenseView> FindByLicenseNumber(BusinessLicense businessLicense);
   }
}
