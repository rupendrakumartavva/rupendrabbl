using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;

namespace BusinessCenter.Data.Implementation
{
    public class RenewalView3Repository : GenericRepository<BblLicenseView3>, IRenewalView3Repository
    {
        public RenewalView3Repository(IUnitOfWork context)
            : base(context)
        {
        }
        /// <summary>
        /// This method is get the all view data 
        /// </summary>
        /// <returns>Retrun BblLicenseView3</returns>
        public IEnumerable<BblLicenseView3> GetAllViewData()
        {
            var ViewData = GetAll().OrderByDescending(x => x.UpDated_Date).AsEnumerable();
            return ViewData;
        }
        /// <summary>
        /// This method is used to get all renewal license 
        /// </summary>
        /// <returns>Return renewal license data</returns>
        public List<RenewalLicense> GetRenewData()
        {
            var businessLicenseData = new List<RenewalLicense>();
            try
            {
                var licenseData = GetAllViewData().OrderByDescending(x => x.UpDated_Date).ToList();
                foreach (var businessdata in licenseData)
                {
                    RenewalLicense businessLicense = new RenewalLicense();
                    businessLicense.MasterId = (businessdata.Application_Unique_ID ?? "").Trim();
                    businessLicense.LicenseNumber = (businessdata.Renewal_License_No_ ?? "").Trim();
                    businessLicense.CategoryName = (businessdata.Primary_Category ?? "").Trim();
                    businessLicense.PaymentDate = !string.IsNullOrEmpty(businessdata.Transaction_Payment_Date.ToString()) ? Convert.ToDateTime(businessdata.Transaction_Payment_Date).ToString("MM/dd/yyyy") : "01/01/1900";
                    businessLicense.CreatedDate = !string.IsNullOrEmpty(businessdata.Created_Date.ToString()) ? Convert.ToDateTime(businessdata.Created_Date).ToString("MM/dd/yyyy") : "01/01/1900";
                    businessLicense.FullName = (businessdata.Full_Name ?? "").Trim();
                    businessLicenseData.Add(businessLicense);
                }
                return businessLicenseData;
            }
            catch (Exception)
            {

                return businessLicenseData;
            }
        }
        /// <summary>
        /// This method is used to get specific renewal view data based on unique id .
        /// </summary>
        /// <param name="businessLicense"></param>
        /// <returns>Return BblLicenseView3 data</returns>
        public IEnumerable<BblLicenseView3> FindByLicenseNumber(RenewalLicense businessLicense)
        {
            var ViewData = FindBy(x => x.Application_Unique_ID.Trim() == businessLicense.MasterId.Trim()).OrderByDescending(x => x.UpDated_Date).AsEnumerable();
            return ViewData;
        }
    }
}
