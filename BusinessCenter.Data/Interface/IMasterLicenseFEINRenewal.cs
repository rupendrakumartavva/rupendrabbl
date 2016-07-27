using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
 public interface IMasterLicenseFEINRenewal
 {
     IEnumerable<MasterLicense_Renewal_TaxRevenue> FindByLicense(RenewModel renewModel);
     bool FindByLicenseTax(string licenseNumber, string taxNumber);
 }
}
