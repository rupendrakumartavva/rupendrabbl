using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessCenter.Data.Implementation
{
    public class SubmissionMasterApplicationChcekListRepository : GenericRepository<SubmissionMaster_ApplicationCheckList>, ISubmissionMasterApplicationChcekListRepository
    {
        public SubmissionMasterApplicationChcekListRepository(IUnitOfWork context): base(context)
        {
        }
        /// <summary>
        /// This method is used to Get specific Check list Data based on Application Unique Id.
        /// </summary>
        /// <param name="masterid"></param>
        /// <returns>Retrun Application List Data</returns>
        public IEnumerable<SubmissionMaster_ApplicationCheckList> FindByMasterId(string masterid)
        {
            var submissionChecklist = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "").ToString().Trim() == masterid).ToList();
            return submissionChecklist;
        }
        /// <summary>
        /// This method is used to Check List for Cofo based on CofohopDetails.
        /// </summary>
        /// <param name="cofoHopDetailsModel"></param>
        /// <returns>Retrun Bool Result</returns>
        public bool UpdateAllCheckListConditions(CofoHopDetailsModel cofoHopDetailsModel)
        {
            bool result = false;
                var subchecklistapp = FindBy(x => x.MasterId.Replace(System.Environment.NewLine,"") == cofoHopDetailsModel.MasterId).FirstOrDefault();
                if(subchecklistapp != null)
                {
                    if (subchecklistapp.IsSubmissionCofo == true && subchecklistapp.IsCleanHandsVerify == true && subchecklistapp.IsCorporateRegistration == true &&
                       subchecklistapp.IsBHAddress == true && subchecklistapp.IsBPAddress == true && subchecklistapp.IsMailAddress == true)
                    {
                        result = true;
                    }
                }
            return result;
        }
        /// <summary>
        /// This method is used to Update Cofo,Hop and Ehop Check list based on Application Unique Id.
        /// </summary>
        /// <param name="cofoHopDetailsModel"></param>
        /// <returns>Return Bool Result</returns>
        public bool UpdateIsCofo(CofoHopDetailsModel cofoHopDetailsModel)
        {
            bool result = false;
            try
            {
                var subcheckli = (from subchecklist in (FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == cofoHopDetailsModel.MasterId)) select subchecklist).First();
               if (subcheckli != null)
               {
                    switch (cofoHopDetailsModel.Type.ToUpper())
                    {
                        case "COFO":
                            subcheckli.IsSubmissionCofo = true;
                            subcheckli.IsSubmissionHop = false;
                            subcheckli.IsSubmissioneHop=false;
                            break;
                        case "HOP":
                            subcheckli.IsSubmissionCofo = false;
                            subcheckli.IsSubmissionHop = true;
                            subcheckli.IsSubmissioneHop=false;
                            break;
                        case "EHOP":
                            subcheckli.IsSubmissionCofo = false;
                            subcheckli.IsSubmissionHop = false;
                            subcheckli.IsSubmissioneHop = true;
                            break;
                    }
                   subcheckli.IsSubmissionCofo = true;
                   Update(subcheckli, subcheckli.SubMaster_ApplicationCheckListId);
                   Save();
                   result = true;
               }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// This method is used insert the Application check List with Application Unique Id.
        /// </summary>
        /// <param name="submissionApp"></param>
        /// <returns>Retrun Bool Result</returns>
        public bool InsertSubmissionChecklist(SubmissionApplication submissionApp)
        {
            bool result;
                var subchecklist = new SubmissionMaster_ApplicationCheckList
                {
                    MasterId = submissionApp.MasterId,
                    FEIN_SSN = false,
                    IsCleanHandsVerify = false,
                    IsCorporateRegistration = false,
                    IsBHAddress = false,
                    IsBPAddress = false,
                    IsMailAddress = false,
                    IsResidentAgent = false,
                    IsDocForCleanHands = false,
                    IsDocForCofo = false,
                    IsDocForHop = false,
                    IsDocForeHop = false,
                    IsSubmissionCofo = false,
                    IsSubmissionHop = false,
                    IsSubmissioneHop = false
                };
                Add(subchecklist);
                Save();
                result = true;
            return result;
        }
        /// <summary>
        /// This method is used to Update Check list for Clean Hands based on Application Unique Id.
        /// </summary>
        /// <param name="taxRevenu"></param>
        /// <param name="validate"></param>
        /// <returns>Retrun Application Check list</returns>
        public SubmissionMaster_ApplicationCheckList UpdateCheckListApp(SubmissionTaxRevenue taxRevenu, bool validate)
        {
            var subchecklist = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == taxRevenu.MasterId).ToList();
            try
            {
                var subcheckli = subchecklist.FirstOrDefault();
                if (subcheckli != null)
                {
                    if (validate == true)
                    {
                        subcheckli.FEIN_SSN = true;
                        subcheckli.IsDocForCleanHands = false;
                        subcheckli.IsCleanHandsVerify = true;
                    }
                    else
                    {
                        subcheckli.FEIN_SSN = false;
                        subcheckli.IsCleanHandsVerify = false;
                        subcheckli.IsDocForCleanHands = false;
                    }
                    taxRevenu.TaxRevenueType = taxRevenu.TaxRevenueType ?? "";
                    if (taxRevenu.TaxRevenueType.ToUpper() == "NODATA")
                    {
                        subcheckli.IsCleanHandsVerify = false;
                        subcheckli.IsCleanHandsVerify = false;
                        subcheckli.FEIN_SSN = false;
                    }
                    Update(subcheckli, subcheckli.SubMaster_ApplicationCheckListId);
                    Save();
                }
                return subcheckli;
            }
            catch (Exception)
            {
                SubmissionMaster_ApplicationCheckList submissionMasterApplicationCheck =
                    new SubmissionMaster_ApplicationCheckList();
                return submissionMasterApplicationCheck;
            }
        }
        /// <summary>
        /// This method is used to Update  the Cofo, Hop and Ehop Status in Check List based on Application Unique Id.
        /// </summary>
        /// <param name="generalbusiness"></param>
        /// <returns>Retrun Bool Result</returns>
        public bool UpdateDetails(CofoHopDetailsModel generalbusiness)
        {
            bool status = false;
            try { 
            var subcheckli = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == generalbusiness.MasterId).First();
            if (subcheckli != null)
            {
                if (generalbusiness != null)
                    {
                    switch (generalbusiness.Type.ToUpper())
                    {
                                case "COFO":
                                    subcheckli.IsSubmissionHop = false;
                                    subcheckli.IsSubmissioneHop = false;
                                    subcheckli.IsSubmissionCofo = true;
                                    subcheckli.IsBPAddress = true;
                                    if (generalbusiness.IsUploadSupportDoc == true && generalbusiness.IsValid == true)
                                    {                                     
                                        subcheckli.IsDocForCofo = true;                                      
                                        subcheckli.IsDocForHop = false;
                                        subcheckli.IsDocForeHop = false;
                                    }
                                    else
                                    {
                                        subcheckli.IsDocForCofo = false;
                                        subcheckli.IsDocForHop = false;
                                        subcheckli.IsDocForeHop = false;
                                    }
                                    break;
                                case "HOP":
                                    subcheckli.IsSubmissionHop = true;
                                    subcheckli.IsSubmissioneHop = false;
                                    subcheckli.IsSubmissionCofo = false;
                                    subcheckli.IsBPAddress = true;
                                    if (generalbusiness.IsUploadSupportDoc == true && generalbusiness.IsValid == true)
                                    {
                                        subcheckli.IsDocForCofo = false;
                                        subcheckli.IsDocForHop = true;
                                        subcheckli.IsDocForeHop = false;
                                    }
                                    else
                                    {
                                        subcheckli.IsDocForCofo = false;
                                        subcheckli.IsDocForHop = false;
                                        subcheckli.IsDocForeHop = false;
                                    }
                                    break;
                                case "EHOP":
                                    subcheckli.IsSubmissionHop = false;
                                    subcheckli.IsDocForCofo = false;
                                      subcheckli.IsDocForHop = false;
                                      subcheckli.IsDocForeHop = false;
                                    subcheckli.IsSubmissionCofo = false;
                                   subcheckli.IsBPAddress = true;
                                        subcheckli.IsSubmissioneHop = true;
                                
                                    break;
                                case "EEHOP":
                                    subcheckli.IsSubmissionHop = false;
                                    subcheckli.IsDocForCofo = false;
                                    subcheckli.IsDocForHop = false;
                                    subcheckli.IsDocForeHop = false;
                                    subcheckli.IsSubmissionCofo = false;
                                    subcheckli.IsSubmissioneHop = true;
                                    break;
                                case "NOPO":
                                      subcheckli.IsBPAddress = true;
                                    break;
                                default:
                                    subcheckli.IsSubmissionCofo = true;
                                    subcheckli.IsDocForCofo = false;
                                    subcheckli.IsDocForHop = false;
                                    subcheckli.IsDocForeHop = false;
                                    break;
                            }
                    }
                Update(subcheckli, subcheckli.SubMaster_ApplicationCheckListId);
                Save();
               status = true;
            }
            }
            catch (Exception)
            { status = false; }
            return status;
        }
        /// <summary>
        /// This method is used to Update  the Corporation  Status in Check List based on Application Unique Id.
        /// </summary>
        /// <param name="masterId"></param>
        /// <param name="submittedType"></param>
        /// <param name="updateSubmittedValue"></param>
        /// <returns>Retrun Bool Result</returns>
        public bool ChcekListUpdateStatus(string masterId, string submittedType, bool updateSubmittedValue)
        {

            bool status = false;
            try
            {
                var subcheckli = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == masterId).First();
                if (subcheckli != null)
                {
                    if (submittedType != null)
                    {
                        switch (submittedType.ToUpper())
                        {
                            case "Y-CORPREG":
                                subcheckli.IsCorporateRegistration = true;
                                subcheckli.IsBHAddress = true;
                                break;
                            case "Y-CORPAGENT":
                                subcheckli.IsResidentAgent = updateSubmittedValue;

                                break;
                            case "N-CORPAGNT":
                                subcheckli.IsResidentAgent = updateSubmittedValue;
                                break;
                                case "N-CORPAGENT":
                                subcheckli.IsResidentAgent = updateSubmittedValue;
                                break;
                           
                            case "N-CORPREG":
                                subcheckli.IsCorporateRegistration = true;
                                subcheckli.IsBHAddress = true;
                                break;
                            case "SEARCHCORP":
                                subcheckli.IsCorporateRegistration = false;
                                subcheckli.IsBHAddress = false;
                                break;
                            case "NEWMAIL":
                                break;
                            default:
                                subcheckli.IsCorporateRegistration = false;
                                break;
                        }
                    }
                    Update(subcheckli, subcheckli.SubMaster_ApplicationCheckListId);
                    Save();
                    status = true;
                }
            }
            catch (Exception)
            { status = false; }
            return status;
        }

        //public virtual void SaveChanges()
        //{
        //    Context.SaveChanges();
        //}
        /// <summary>
        ///  This method is used to Update  the Corporation  Status in Check List based on Application Unique Id.
        /// </summary>
        /// <param name="detailsModel"></param>
        /// <returns>Retrun bool Result</returns>
        public bool UpdateIsCorporation(GeneralBusiness detailsModel)
        {
            bool result = false;
            try
            {
                var subcheckli = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == detailsModel.MasterId).First();
                if (subcheckli != null)
                {
                    subcheckli.IsCorporateRegistration = true;
                    subcheckli.IsBPAddress = true;
                    if (detailsModel.UserType == "Y-CORPAGENT")
                    {
                        subcheckli.IsResidentAgent = true;
                    }
                    else
                    if (detailsModel.UserType == "N-CORPREG")
                    {
                        subcheckli.IsCorporateRegistration = true;
                        subcheckli.IsBHAddress = true;
                    }
                    else
                    if (detailsModel.UserType.ToUpper().Trim() == "N-CORPAGENT")
                    {
                        subcheckli.IsResidentAgent = true;
                    }
                    Update(subcheckli, subcheckli.SubMaster_ApplicationCheckListId);
                    Save();
                    result = true;
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// This method is used to Update Resident Agent Status based on Application Unique Id.
        /// </summary>
        /// <param name="detailsModel"></param>
        /// <returns>Retrun bool Result</returns>
        public bool UpdateIsCorporationAgent(GeneralBusiness detailsModel)
        {
            bool result = false;
            try
            {
                var subcheckli = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == detailsModel.MasterId).First();
                if (subcheckli != null)
                {
                    subcheckli.IsResidentAgent = true;
                    Update(subcheckli, subcheckli.SubMaster_ApplicationCheckListId);
                    Save();
                    result = true;
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// This method is used to Update Mail Address Status based on Application Unique Id.
        /// </summary>
        /// <param name="detailsModel"></param>
        /// <returns>Retrun bool Result</returns>
        public bool UpdateIsMailAddress(GeneralBusiness detailsModel)
        {
            bool result = false;
            try
            {
                var subcheckli =FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == detailsModel.MasterId).First();
                if (subcheckli != null)
                {
                    subcheckli.IsMailAddress = true;
                    Update(subcheckli, subcheckli.SubMaster_ApplicationCheckListId);
                    Save();
                    result = true;
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// This method is used to Update Cofo,Hop and Ehop Status in Submission Checklikst based on Application Unique Id.
        /// </summary>
        /// <param name="cofoModel"></param>
        /// <returns>Retrun Bool Result</returns>
        public bool UpdateCofodetails(CofoHopDetailsModel cofoModel)
        {
            bool result = false;
            try
            {
                var subcheckli = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == cofoModel.MasterId).FirstOrDefault();
                if (subcheckli != null)
                {
                    subcheckli.IsSubmissionCofo = false;
                    subcheckli.IsSubmissionHop = false;
                    subcheckli.IsSubmissioneHop = false;
                    subcheckli.IsBPAddress = false;
                    subcheckli.IsDocForCofo = false;
                    subcheckli.IsDocForHop = false;
                    subcheckli.IsDocForeHop = false;
                    Update(subcheckli, subcheckli.SubMaster_ApplicationCheckListId);
                    Save();
                    result = true;
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// This method is used to Update Submission Check list Based on Renewal Application Unique Id.
        /// </summary>
        /// <param name="renewModel"></param>
        /// <returns>Retrun Bool Result</returns>
        public bool InsertRenewChecklist(RenewModel renewModel)
        {
            bool result;
            try
            {
                var subchecklist = new SubmissionMaster_ApplicationCheckList
                {
                    MasterId = renewModel.MasterId,
                    FEIN_SSN = false,
                    IsCleanHandsVerify = false,
                    IsCorporateRegistration = false,
                    IsBHAddress = false,
                    IsBPAddress = false,
                    IsMailAddress = false,
                    IsResidentAgent = false,
                    IsDocForCleanHands = false,
                    IsDocForCofo = false,
                    IsDocForHop = false,
                    IsDocForeHop = false,
                    IsSubmissionCofo = false,
                    IsSubmissionHop = false,
                    IsSubmissioneHop = false
                };
                Add(subchecklist);
                Save();
                result = true;
            }
            catch (Exception)
            {

                result = false;
            }
            
            return result;
        }
        /// <summary>
        /// This method is used to Update Corporation Status based on Application Unique Id, Submission Type and Mail Type.
        /// </summary>
        /// <param name="masterId"></param>
        /// <param name="submittedType"></param>
        /// <param name="mailtype"></param>
        /// <returns>Retrun Bool Result</returns>
        public bool UpdateCorpCheckStatus(string masterId, string submittedType, string mailtype)
        {
            bool result;
            try
            {
            
            var subcheckli = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == masterId).First();
            if (subcheckli != null)
            {
                if (submittedType != null)
                {
                    switch (submittedType.ToUpper())
                    {
                        case "Y-CORPREG":
                            subcheckli.IsCorporateRegistration = false;
                            subcheckli.IsBHAddress = false;
                            subcheckli.IsResidentAgent = false;
                            break;
                        case "Y-CORPAGENT":
                            subcheckli.IsResidentAgent = false;
                            break;
                        case "N-CORPAGENTEMPTY":
                            subcheckli.IsResidentAgent = false;
                            subcheckli.IsMailAddress = false;
                            break;
                        case "EHOPADDRESS":
                            subcheckli.IsBPAddress = false;
                            break;
                        case "NOPO":
                            subcheckli.IsBPAddress = false;
                            break;
                        case "N-CORPREG-EMPTY":
                            subcheckli.IsCorporateRegistration = false;
                            subcheckli.IsBHAddress = false;
                            subcheckli.IsResidentAgent = false;
                            break;
                        case "NEWMAIL":
                            subcheckli.IsMailAddress = true;
                            break;
                        case "N-CORPREG":
                            break;
                        case "N-CORPAGENT":
                            break;
                        default:
                             subcheckli.IsCorporateRegistration = false;
                            subcheckli.IsBHAddress = false;
                            subcheckli.IsResidentAgent = false;
                            if (mailtype.ToUpper()=="HQ ADDRESS")
                            {
                                subcheckli.IsMailAddress = false;
                            }
                            break;
                    }
                }
                Update(subcheckli, subcheckli.SubMaster_ApplicationCheckListId);
                Save();
            }
                result = true;
            }
            catch (Exception)
            {

                result = false;
            }
            return result;
        }
        /// <summary>
        /// This method is used to coporate registrion, head quater address and resident agent based on condition and unique id.
        /// </summary>
        /// <param name="masterId"></param>
        /// <param name="submittedType"></param>
        /// <returns>Retrun bool value</returns>
        public bool UpdateCorpsHqstatusandagentStatus(string masterId, string submittedType)
        {
            bool result;
            try
            {

                var subcheckli = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == masterId).First();
                if (subcheckli != null)
                {
                    if (submittedType != null)
                    {
                        switch (submittedType.ToUpper())
                        {
                            case "N-CORPREG":
                                subcheckli.IsCorporateRegistration = false;
                                 subcheckli.IsBHAddress = false;                         
                                break;
                            case "N-CORPAGENT":
                                subcheckli.IsResidentAgent = false;
                                break;
                            default:
                                break;
                        }
                    }
                    Update(subcheckli, subcheckli.SubMaster_ApplicationCheckListId);
                    Save();
                  
                }
                result = true;
            }
             catch (Exception)
            {

                result = false;
            }
            return result;
        }
        /// <summary>
        /// This method is used to update Mail address Status based on Application Unique Id.
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
        public bool UpdateMailStatus(string masterId)
        {
            bool result = false;
            try
            {
                var subcheckli = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == masterId.Trim()).First();
                if (subcheckli != null)
                {
                    subcheckli.IsMailAddress = false;
                    Update(subcheckli, subcheckli.SubMaster_ApplicationCheckListId);
                    Save();
                    result = true;
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// This method is used to Update the MailAddress Status in Application Check List based on Application Unique Id.
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns>Return Bool Result</returns>
        public bool UpdateMailTrueStatus(string masterId)
        {
            bool result = false;
            try
            {
                var subcheckli = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == masterId.Trim()).First();
                if (subcheckli != null)
                {
                    subcheckli.IsMailAddress = true;
                    Update(subcheckli, subcheckli.SubMaster_ApplicationCheckListId);
                    Save();
                    result = true;
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// This method is used to Update status of Cofo,Hop and Ehop in Check List based on Application Unique Id
        /// </summary>
        /// <param name="masterid"></param>
        /// <param name="mailtype"></param>
        public void CofoServiceStatus(string masterid, string mailtype)
        {
            var subcheckli = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == masterid).First();
           
                    subcheckli.IsSubmissionHop = false;
                    subcheckli.IsDocForCofo = false;
                    subcheckli.IsDocForHop = false;
                    subcheckli.IsDocForeHop = false;
                    subcheckli.IsSubmissionCofo = false;
                    subcheckli.IsBPAddress = false;
                    if (mailtype.ToUpper() == "PRIMSES ADDRESS")
                    {
                        subcheckli.IsMailAddress = false;
                    }
            Update(subcheckli, subcheckli.SubMaster_ApplicationCheckListId);
            Save();
        }
        /// <summary>
        /// This method is used to Update the status of Clean Hand Verify in Check List Based on Application Unique Id.
        /// </summary>
        /// <param name="masterid"></param>
        /// <param name="updateStatus"></param>
        public void TaxServiceStatus(string masterid,bool updateStatus)
        {
            var subcheckli = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == masterid).First();
            if (subcheckli.IsDocForCleanHands != true)
            {
                if (updateStatus)
                {
                    subcheckli.IsCleanHandsVerify = false;
                }
                else
                {
                    subcheckli.IsCleanHandsVerify = true;
                }
            }
            Update(subcheckli, subcheckli.SubMaster_ApplicationCheckListId);
            Save();
        }
        /// <summary>
        /// This method is used to Update Corporation Status in Check List based on Application Unique Id.
        /// </summary>
        /// <param name="masterid"></param>
        /// <returns>Return Bool Result</returns>
        public bool UpdateCorpSearchStatus(string masterid)
        {
            try
            {
                var subcheckli = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == masterid).First();
                subcheckli.IsCorporateRegistration = null;
                subcheckli.IsResidentAgent = null;
                subcheckli.IsBHAddress = null;
                Update(subcheckli, subcheckli.SubMaster_ApplicationCheckListId);
                Save();
                return true;
            }
            catch (Exception)
            {
                
                return false;
            }
            
        }
        /// <summary>
        /// This method is used update Clean hand condition based on unique id.
        /// </summary>
        /// <param name="submittedType"></param>
        /// <param name="updateStatus"></param>
        /// <param name="masterId"></param>
        public void UpdateChcekList(string submittedType,bool updateStatus,string masterId)
        {
              var subcheckli = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == masterId).First();
            if (subcheckli != null)
            {
                if (submittedType != null)
                {
                    switch (submittedType.ToUpper())
                    {
                        case "SELFCERTIFICATION":
                            subcheckli.IsSelfCertification = updateStatus;
                            break;
                    }
                }
                Update(subcheckli, subcheckli.SubMaster_ApplicationCheckListId);
                Save();
            }
        }
        /// <summary>
        /// This method is used to delte entier entry of the submission check list based on unique id.
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns>Return bool value</returns>
        public bool DeleteSubmissionCheckList(string masterId)
        {
            try
            {
                var submasterchecklist = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == masterId).ToList();
                if (submasterchecklist.Count() != 0)
                {
                    Delete(submasterchecklist.Single());
                    Save();
                }
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }
    }
}