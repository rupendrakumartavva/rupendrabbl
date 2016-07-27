using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using BusinessCenter.Data.Model;

namespace BusinessCenter.Data.Implementation
{
    public class SubmissionMasterRenewalRepository : GenericRepository<SubmissionMasterRenewal>, ISubmissionMasterRenewalRepository
    {
        protected IMasterRenewalStatusFeeRepository _masterrenewfee;
        public SubmissionMasterRenewalRepository(IUnitOfWork context, IMasterRenewalStatusFeeRepository masterrenewfee)
            : base(context)
        {
            _masterrenewfee = masterrenewfee;
        }

        /// <summary>
        /// This methid is used to Insert Renewal Data based on User Inputs
        /// </summary>
        /// <param name="rmodel"></param>
        /// <returns>Return string  value</returns>
        public string InsertRenewalDetails(RenewModel rmodel)
        {
            string status;
            try
            {
            var renwaldata =FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "").Trim() == rmodel.MasterId).ToList();
            if (renwaldata.Count == 0)
            {
                  var expiry=  _masterrenewfee.FindByStatus("EXPIRED").ToList();
                var lapsed=_masterrenewfee.FindByStatus("LAPSED").ToList();
                SubmissionMasterRenewal subMasRenewal = new SubmissionMasterRenewal();
                subMasRenewal.MasterId = (rmodel.MasterId ?? "").Trim();
                subMasRenewal.SubmissionLicense = (rmodel.LrenNumber ?? "").Trim();
                subMasRenewal.CorpNumber = (rmodel.CorpNumber ?? "").Trim();
                subMasRenewal.IsDcraCorpDivision = rmodel.IsCorp;
                if (rmodel.Extradays.ToUpper().Trim() == "EXPIRED")
                {
                 
                      if(expiry.Any())
                      {
                    rmodel.ExpiredAmount = Convert.ToDecimal(expiry.FirstOrDefault().FeeAmount); //250;
                      }else
                      { rmodel.ExpiredAmount =0; //250;
                      }
                      if (lapsed.Any())
                      {
                          rmodel.LapsedAmount = Convert.ToDecimal(lapsed.FirstOrDefault().FeeAmount); //250;
                      }
                      else
                      {
                          rmodel.LapsedAmount = 0; //250;
                      }
                  //  rmodel.LapsedAmount = Convert.ToDecimal(_masterrenewfee.FindByStatus("LAPSED").FirstOrDefault().FeeAmount);

                }
                else
                    if (rmodel.Extradays.ToUpper().Trim() == "LAPSED")
                    {
                        rmodel.ExpiredAmount = 0;
                        if (lapsed.Any())
                        {
                            rmodel.LapsedAmount = Convert.ToDecimal(lapsed.FirstOrDefault().FeeAmount); //250;
                        }
                        else
                        {
                            rmodel.LapsedAmount = 0; //250;
                        }
                    }
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                // ReSharper disable once ConvertIfStatementToNullCoalescingExpression
                if (rmodel.LapsedAmount == null)
                {
                    subMasRenewal.LapsedFee = 0; }
                else
                {
                     subMasRenewal.LapsedFee = rmodel.LapsedAmount;
                }
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                // ReSharper disable once ConvertIfStatementToNullCoalescingExpression
                if (rmodel.ExpiredAmount == null)
                {
                    subMasRenewal.ExpiredFee = 0;
                }
                else
                {
                    subMasRenewal.ExpiredFee = rmodel.ExpiredAmount;
                }
                
                subMasRenewal.Extradays = (rmodel.Extradays ?? "").Trim();
                subMasRenewal.IsCorpDocRegistration = false;
                Add(subMasRenewal);
                Save();
                status = "true";
            }
            else
            {
                var updateRenewal = (from paymentdetails in (
                         FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == rmodel.MasterId))
                                     select paymentdetails).First();
                if ((updateRenewal.CorpNumber != rmodel.CorpNumber) ||
                    (updateRenewal.IsDcraCorpDivision != rmodel.IsCorp))
                {
                  
                    updateRenewal.IsCorpDocRegistration = false;
                    status = "Change";
                }
                else
                {
                    status = "true"; 
                }
                updateRenewal.MasterId = (rmodel.MasterId ?? "").Trim();
                updateRenewal.CorpNumber = (rmodel.CorpNumber ?? "").Trim();
                updateRenewal.IsDcraCorpDivision = rmodel.IsCorp;
                Update(updateRenewal, updateRenewal.RenewalSubmId);
                Save();
            }
            }
            catch (Exception)
            {
                status = "false";
            }
            return status;
        }
       
        /// <summary>
        /// This method is used to Get specific Renewal submission Data based on Application Unique Id.
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns>Return specific SubmissionMasterRenewal </returns>
        public IEnumerable<SubmissionMasterRenewal> FindByID(string masterId)
        {
            var renewalMaster =
                FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == masterId);
            return renewalMaster;
        }
        /// <summary>
        /// This method is used to delete submission master renewal based on unique id.
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns>Return bool value</returns>
        public bool DeleteMasterRenewal(string masterId)
        {
            try
            {
                var submaster = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == masterId).ToList();
                if (submaster.Count() != 0)
                {
                    Delete(submaster.Single());
                    Save();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// This method is used to update is corportion document registration data in submission master renewal based on unique id.
        /// </summary>
        /// <param name="iscorpregistration"></param>
        /// <param name="masterid"></param>
        /// <returns>Return bool value</returns>
        public bool updateIscorpStatus(bool iscorpregistration, string masterid)
        {
            try
            {
                var userAssociate =FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == masterid).First();
                userAssociate.IsCorpDocRegistration = iscorpregistration;
                Update(userAssociate, Convert.ToInt64(userAssociate.RenewalSubmId));
                Save();
            }
            catch (Exception)
            { }
            return true;
        }

    }
}
