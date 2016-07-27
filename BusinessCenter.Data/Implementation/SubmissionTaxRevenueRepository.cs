using System;
using BusinessCenter.Common;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using System.Collections.Generic;
using System.Linq;

namespace BusinessCenter.Data.Implementation
{
    public class SubmissionTaxRevenueRepository : GenericRepository<SubmissionTaxRevenue>, ISubmissionTaxRevenueRepository
    {
        private readonly ISubmissionMasterApplicationChcekListRepository _submissionchecklistapprepo;
        public SubmissionTaxRevenueRepository(IUnitOfWork context,
            ISubmissionMasterApplicationChcekListRepository submissionChcekListAppRepository)
            : base(context)
        {
            _submissionchecklistapprepo = submissionChcekListAppRepository;
        }
        /// <summary>
        /// This method is used to get particular submission tax revenue data based on master id
        /// </summary>
        /// <param name="submissionTaxRevenu"></param>
        /// <returns>Return submission tax revenue data</returns>
        public IEnumerable<SubmissionTaxRevenue> FindByTaxRevenueNumber(SubmissionTaxRevenuEntity submissionTaxRevenu)
        {
            var taxrevenuedata =
                FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == submissionTaxRevenu.MasterId).ToList();
            return taxrevenuedata;
        }
       /// <summary>
       /// This method is used to insert/update submission tax revenue data based on submissiontaxrevenuentity
       /// </summary>
       /// <param name="submissionTaxRevenu"></param>
       /// <returns>Retrun bool value</returns>
        public bool SubmissionTaxRevenuInsertUpdate(SubmissionTaxRevenuEntity submissionTaxRevenu)
        {
            try
            {
                if (string.IsNullOrEmpty(submissionTaxRevenu.MasterId)) return false;
                if (string.IsNullOrEmpty(submissionTaxRevenu.TaxRevenueFfin)) return false;
                var validate =FindBy(x =>x.MasterId == submissionTaxRevenu.MasterId).ToList();
                if (!validate.Any())
                {
                    var taxRevenu = new SubmissionTaxRevenue
                    {
                        SubmissionTaxRevenueId = submissionTaxRevenu.SubmissionTaxRevenueId,
                        MasterId = submissionTaxRevenu.MasterId,
                        TaxRevenueNumber = submissionTaxRevenu.TaxRevenueFfin,
                        TaxRevenueType = submissionTaxRevenu.TaxRevenueType,
                        FullName = submissionTaxRevenu.FullName,
                        BusinessOwnerRoles = submissionTaxRevenu.BusinessOwnerRoles,
                        CreatdedDate = DateTime.Now,
                        UpdatedDate=DateTime.Now,
                        IsIAgree=submissionTaxRevenu.IsIAgree
                    };
                    Add(taxRevenu);
                    Save();
                    _submissionchecklistapprepo.UpdateCheckListApp(taxRevenu, true);
                }
                else
                {
                    var taxRevenu = new SubmissionTaxRevenue
                    {
                        MasterId = submissionTaxRevenu.MasterId,
                        SubmissionTaxRevenueId = validate.FirstOrDefault().SubmissionTaxRevenueId,
                        TaxRevenueNumber = submissionTaxRevenu.TaxRevenueFfin,
                        TaxRevenueType = submissionTaxRevenu.TaxRevenueType,
                        FullName = submissionTaxRevenu.FullName,
                        BusinessOwnerRoles = submissionTaxRevenu.BusinessOwnerRoles,
                        CreatdedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        IsIAgree = submissionTaxRevenu.IsIAgree
                    };
                    int taxRevenueId = validate.FirstOrDefault().SubmissionTaxRevenueId;
                    Update(taxRevenu, taxRevenueId);
                    Save();
                    _submissionchecklistapprepo.UpdateCheckListApp(taxRevenu, true);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// This method is used to get particular submission tax revenue data based on master id
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns>Return submission tax revenue</returns>
        public IEnumerable<SubmissionTaxRevenue> FindByID(string masterId)
        {
            var taxrevenuedata =FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "").Trim() == masterId.Trim()).ToList();
                   return taxrevenuedata;
        }
        /// <summary>
        /// This method is used to delete submission tax and revenue data based on master id.
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns>Return bool value</returns>
        public bool DeleteSubmissionTaxandRevenue(string masterId)
        {
            var deleteTax =FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "").Trim() == masterId.Trim()).ToList();
            try
            {
                if (deleteTax.Count != 0)
                {
                    Delete(deleteTax.FirstOrDefault());
                    Save();
                    _submissionchecklistapprepo.UpdateCheckListApp(deleteTax.FirstOrDefault(), false);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// This method is used to delete submission tax and revenue data based on master id.
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns>Return bool value</returns>
        public bool DeleteTaxandRevenue(string masterId)
        {
            var deleteTax =FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "").Trim() == masterId.Trim()).ToList();
            try
            {
                if (deleteTax.Count != 0)
                {
                    Delete(deleteTax.FirstOrDefault());
                    Save();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// This method is used to update tax and revenue data based on master id
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns>Return bool value</returns>
        public bool UpdateTaxAndRevenue(string masterId)
        {
            var UpdateTaxandRevenue = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "").Trim() == masterId.Trim()).ToList();
            if(UpdateTaxandRevenue.Any())
            {
                var taxrevenueUpdate = UpdateTaxandRevenue.FirstOrDefault();
                taxrevenueUpdate.IsIAgree = false;
                taxrevenueUpdate.UpdatedDate = DateTime.Now;
                Update(taxrevenueUpdate, taxrevenueUpdate.SubmissionTaxRevenueId);
                Save();
                SubmissionTaxRevenue taxrevenue = new SubmissionTaxRevenue();
                taxrevenue.MasterId = masterId;
                taxrevenue.TaxRevenueType = "";
                _submissionchecklistapprepo.UpdateCheckListApp(taxrevenue, false);
                return true;
            }
            return false;
        }
    }
}