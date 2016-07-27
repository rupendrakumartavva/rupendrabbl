using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data;
using BusinessCenter.Data.Model;

namespace BusinessCenter.Service.Interface
{
   public interface IBusinessInformationService
   {
       List<BusinessLicense> GetSubmissionData();
       IEnumerable<BblLicenseView> FindByLicenseNumber(BusinessLicense businessLicense);
   }
}
