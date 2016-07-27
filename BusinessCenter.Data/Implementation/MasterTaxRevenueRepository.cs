using BusinessCenter.Common;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using System.Linq;
using BusinessCenter.Data.Model;

namespace BusinessCenter.Data.Implementation
{
    public class MasterTaxRevenueRepository : GenericRepository<MasterTaxRevenue>, IMasterTaxRevenueRepository
    {
        public MasterTaxRevenueRepository(IUnitOfWork context)
            : base(context)
        {
        }
        /// <summary>
        /// This methid is used to get status of the Tax Number based on Tax Number and Type
        /// </summary>
        /// <param name="submissionTaxRevenuModel"></param>
        /// <returns>Retrun result string</returns>
        public string ValidateFEINNumber(SubmissionTaxRevenuEntity submissionTaxRevenuModel )
        {
            string status = "";
           
           
            var taxnumber= (from taxRevenue in (
                     FindBy(x => x.TextFEINNumber.Replace(System.Environment.NewLine, "") == submissionTaxRevenuModel.TaxRevenueFfin &&
                     x.Type.Replace(System.Environment.NewLine, "").ToUpper() == submissionTaxRevenuModel.TaxRevenueType.ToUpper()))
                   select taxRevenue).ToList();

            if (taxnumber.Any())
            {
                var masterTaxRevenue = taxnumber.FirstOrDefault();
                if (masterTaxRevenue != null)
                    status = masterTaxRevenue.IsCleanHands.ToString().ToUpper();
            }
            else
            {
                status = "NODATA";
            }
            return status;
        }
        /// <summary>
        ///This methid is used to get status of the Tax Number based on Tax Number
        /// </summary>
        /// <param name="renewModel"></param>
        /// <returns></returns>
        public string ValidateTaxNumber(RenewModel renewModel)
        {
            var status = "NODATA";
            var taxnumber = (from taxRevenue in (FindBy(x => x.TextFEINNumber.Replace(System.Environment.NewLine, "") == renewModel.TaxNumber))
                             select taxRevenue).ToList();

            var taxData = taxnumber.FirstOrDefault();
            if (taxData != null)
                status = taxnumber.Any()
                    ? taxData.IsCleanHands.ToString().ToUpper() == "" ? "" : taxData.IsCleanHands.ToString().ToUpper()
                    : "NODATA"; 
            return status;
        }
    }
}
