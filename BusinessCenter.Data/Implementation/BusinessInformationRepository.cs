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
    public class BusinessInformationRepository : GenericRepository<BblLicenseView>, IBusinessInformationRepository
    {
        public BusinessInformationRepository(IUnitOfWork context)
            : base(context)
        {
           

        }
        /// <summary>
        /// This method is used get all the view data order by descending
        /// </summary>
        /// <returns>Retrun View Data</returns>
        public IEnumerable<BblLicenseView> GetAllViewData()
        {
            var ViewData = GetAll().OrderByDescending(x => x.UpDated_Date).AsEnumerable();
            return ViewData;
        }
        ///<summary>
        /// This method is used to get all Business License 
        /// </summary>
        /// <returns>Return Business License</returns>
        public List<BusinessLicense> GetSubmissionData()
        {
            var businessLicenseData = new List<BusinessLicense>();
            try
            {
                var licenseData = GetAllViewData().OrderByDescending(x => x.UpDated_Date).ToList();
                foreach (var businessdata in licenseData)
                {
                    BusinessLicense businessLicense = new BusinessLicense
                    {
                        MasterId = (businessdata.Application_Unique_ID ?? "").Trim(),
                        LicenseNumber = (businessdata.Application_License_No_ ?? "").Trim(),
                        LicesnseType = (businessdata.Application_Type ?? "").Trim(),
                        Status = (businessdata.Online_App_License_Status ?? "").Trim(),
                        GrandTotal = (businessdata.TotalAmount).ToString().Trim(),
                        PaymentTransaction = (businessdata.Payment_Transaction_ID ?? "").Trim(),
                         PaymentDate = businessdata.Payment_Transaction_Date.ToString(),
                         // !string.IsNullOrEmpty(businessdata.Payment_Transaction_Date.ToString()) ? Convert.ToDateTime(businessdata.Payment_Transaction_Date).ToString("MM/dd/yyyy") : "01/01/1900",
                         CreatedDate = !string.IsNullOrEmpty(businessdata.Created_Date.ToString()) ? Convert.ToDateTime(businessdata.Created_Date).ToString("MM/dd/yyyy") : "01/01/1900",
                        FullName = businessdata.Full_Name.Trim(),
                            
                    };

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
        /// This method is used to retrieve BBlLicneseView using unique id .
        /// </summary>
        /// <param name="businessLicense"></param>
        /// <returns>Return Bbl License View</returns>
        public IEnumerable<BblLicenseView> FindByLicenseNumber(BusinessLicense businessLicense)
        {
            var ViewData = FindBy(x => x.Application_Unique_ID.Trim() == businessLicense.MasterId.Trim()).OrderByDescending(x=>x.UpDated_Date).AsEnumerable();
            return ViewData;
        }
    }
}
