using BusinessCenter.Api.App_Start;
using BusinessCenter.Api.Filters;
using BusinessCenter.Common;
using BusinessCenter.Data;
using BusinessCenter.Data.Model;
using BusinessCenter.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BusinessCenter.Api.Controllers
{
    [RoutePrefix("api/Renew")]
    [UpdateTokenLifeTime]
    public class RenewController : ApiController
    {
        //  private readonly IBBLAssociateService _bblAssociateService;
        private readonly IRenewalService _renewalrepo;

        private readonly ISubmissionMasterService _submissionMasterService;
        private readonly ISubmissionTaxRevenueService _submissionTaxRevenueService;
        private readonly IBBLAssociateService _bblAssociateService;

        public RenewController(IRenewalService renewalrepository, ISubmissionMasterService submissionMasterService,
            ISubmissionTaxRevenueService submissionTaxRevenueService, IBBLAssociateService bblAssociateService)
        {
            //  _bblAssociateService = bblAssociateService;
            _renewalrepo = renewalrepository;
            _submissionMasterService = submissionMasterService;
            _submissionTaxRevenueService = submissionTaxRevenueService;
            _bblAssociateService = bblAssociateService;
        }

        [Authorize]
        [HttpPost]
        [Route("GetRenewalData")]
        [DeflateCompression]
        public async Task<IHttpActionResult> GetRenewalData(RenewModel renewModel)
        {
            var getUserService = _submissionMasterService.GetBblServices(renewModel.UserBblAssociateId);

            var userBblServices = getUserService as UserBBLService[] ?? getUserService.ToArray();
            if (userBblServices.Any())
            {
                var bblservicedata = userBblServices.FirstOrDefault();
                if (bblservicedata != null)
                    renewModel.EntityId = bblservicedata.DCBC_ENTITY_ID.Trim();
                renewModel.SubmissionLicense = bblservicedata.SubmissionLicense.Trim();

                var BBLData = _renewalrepo.GetRenewData(renewModel);
                if (BBLData.Any())
                {
                    renewModel.CorpNumber =  renewModel.CorpNumber ?? "";
                    renewModel.IsCorp = false;
                    renewModel.EntityId = Convert.ToString(BBLData.FirstOrDefault().DCBC_ENTITY_ID);
                    string submssion = _submissionMasterService.GetmasterId(renewModel.SubmissionLicense, renewModel.UserId);
                    if (submssion != "")
                    {
                        renewModel.MasterId = submssion;
                        renewModel.MasterId = renewModel.MasterId ?? "";
                        if (renewModel.MasterId != "")
                        {
                            var backbuttondata = _renewalrepo.FindByID(renewModel.MasterId).ToList();
                            if (backbuttondata.Count() != 0)
                            {
                                renewModel.CorpNumber = backbuttondata.FirstOrDefault().CorpNumber ?? "";
                                renewModel.IsCorp = backbuttondata.FirstOrDefault().IsDcraCorpDivision.Value;
                                renewModel.IsCorpRegistration = backbuttondata.FirstOrDefault().IsCorpDocRegistration == null ? false :
                                    backbuttondata.FirstOrDefault().IsCorpDocRegistration.Value;
                            }
                        }
                    }

                    var bblData = BBLData.FirstOrDefault();
                    //int entityid = (bblData.License_Category ?? "").Trim().Replace("NA","");

                    string licensestatus = _renewalrepo.CheckCategoryStatus(bblData.DCBC_ENTITY_ID.ToString(), (bblservicedata.B1_ALT_ID ?? "").Trim(), (bblservicedata.SubmissionLicense ?? "").Trim());
                    //var bblrenew = _bblAssociateService.RenewalData((bblservicedata.B1_ALT_ID ?? "").Trim(), (bblservicedata.SubmissionLicense ?? "").Trim());
                    ////_bblAssociateService.GetAssociteData((dcbcEntityBbl.B1_ALT_ID ?? "").Trim());
                    //string bblcorpnumber = bblrenew.FirstOrDefault().Org_Number ?? "";
                    //if (bblcorpnumber == "")
                    //{
                    //    renewModel.IsBblCorp = false;
                    //}
                    //else
                    //{
                    //    renewModel.CorpNumber = bblrenew.FirstOrDefault().Org_Number ?? "";
                    //    renewModel.IsBblCorp = true;
                    //}
                    ////if (submssion == "")
                    ////{ renewModel.CorpNumber = bblrenew.FirstOrDefault().Org_Number ?? ""; }
                    //var bblrenewdata = bblrenew.FirstOrDefault();
                    string bblcorpnumber = (bblData.Org_Number ?? "").Trim();
                    if (bblData != null)
                        renewModel.CorpStatus = "";
                        if (bblcorpnumber == "")
                        {
                           
                        }
                        else
                        {
                            CorporationDetails corporationdetails=new CorporationDetails();
                            renewModel.CorpNumber = bblcorpnumber;
                           
                            corporationdetails.FileNumber = renewModel.CorpNumber;
                            renewModel.CorpStatus = _bblAssociateService.CorpOnlineSearch(corporationdetails);
                        }

                        return Json(new
                        {
                            isValidated = true,
                            entityId = bblData.DCBC_ENTITY_ID,
                            licenseNumber = bblData.B1_ALT_ID == null ? "" : bblData.B1_ALT_ID.ToUpper().Trim() == "NA" ? "" : (bblData.B1_ALT_ID ?? "").ToUpper().Trim(),
                            fName = bblData.Contact_FirstName == null ? "" : bblData.Contact_FirstName.ToUpper().Trim() == "N/A" ? "" : (bblData.Contact_FirstName ?? "").Trim(),
                            lName = bblData.Contact_LastName == null ? "" : bblData.Contact_LastName.ToUpper().Trim() == "N/A" ? "" : (bblData.Contact_LastName ?? "").Trim(),
                            businessName = bblData.OwnrApplicant_BUSINESS_NAME == null ? "" : (bblData.OwnrApplicant_BUSINESS_NAME ?? "").Trim(),
                            tradeName = bblData.Attr_TRADE_NAME == null ? "" : (bblData.Attr_TRADE_NAME ?? "").Trim(),
                            businessNameStructure = bblData.Business_Org == null ? "" : (bblData.Business_Org ?? "").Trim(),
                            expDate = bblData.Expiration_Date == null ? "" : bblData.Expiration_Date.ToUpper().Trim() == "NA" ? "" : (bblData.Expiration_Date ?? "").Trim(),
                            applicationstatus = bblData.B1_APPL_STATUS == null ? "" : bblData.B1_APPL_STATUS.ToUpper().Trim() == "NA" ? "" : (bblData.B1_APPL_STATUS ?? "").Trim(),
                            subcategory = bblData.License_Category == null ? "" : bblData.License_Category.ToUpper().Trim() == "NA" ? "" : (bblData.License_Category ?? "").Trim(),
                            activity = bblData.B1_PER_SUB_TYPE == null ? "" : bblData.B1_PER_SUB_TYPE.ToUpper().Trim() == "NA" ? "" : (bblData.B1_PER_SUB_TYPE ?? "").Trim(),
                            category = bblData.License_Category == null ? "" : bblData.License_Category.ToUpper().Trim() == "NA" ? "" : (bblData.License_Category ?? "").ToUpper().Trim(),
                            applicationtype = bblData.B1_PER_TYPE,
                            corpNumber = renewModel.CorpNumber,
                            iscorp = renewModel.IsCorp,
                            IsCorpRegistration = renewModel.IsCorpRegistration,
                            licenseCategorystatus = licensestatus,
                            IsBblCorp = renewModel.IsBblCorp,
                            Status= renewModel.CorpStatus
                        });
                }
            }
            return await Task.FromResult(Json(new { isValidated = false }));
        }

        [Authorize]
        [HttpPost]
        [Route("RemoveRenewalData")]
        [DeflateCompression]
        public async Task<IHttpActionResult> RemoveRenewalData(RenewModel renewModel)
        {
            var getUserService = _submissionMasterService.GetBblServices(renewModel.UserBblAssociateId);

            var userBblServices = getUserService as UserBBLService[] ?? getUserService.ToArray();
            if (userBblServices.Any())
            {
                var bblservicedata = userBblServices.FirstOrDefault();
                if (bblservicedata != null)
                    renewModel.EntityId = bblservicedata.DCBC_ENTITY_ID.Trim();
                renewModel.SubmissionLicense = bblservicedata.SubmissionLicense.Trim();
                string submssion = _submissionMasterService.GetmasterId(renewModel.SubmissionLicense, renewModel.UserId);
                if (submssion != "")
                {
                    renewModel.MasterId = submssion;
                    _submissionTaxRevenueService.DeleteTaxandRevenue(renewModel.MasterId);
                    var result = _renewalrepo.DeleteRenewal(renewModel);
                    renewModel.MasterId = "";
                    return Json(result);
                }
                return Json(true);
            }
            return await Task.FromResult(Json("false"));
        }

        [Authorize]
        [HttpPost]
        [Route("RenewalStatus")]
        public async Task<IHttpActionResult> RenewalStatus(RenewModel renewModel)
        {
            var getUserService = _submissionMasterService.GetBblServices(renewModel.UserBblAssociateId);

            var userBblServices = getUserService as UserBBLService[] ?? getUserService.ToArray();
            if (userBblServices.Any())
            {
                var bblservicedata = userBblServices.FirstOrDefault();
                if (bblservicedata != null)
                    renewModel.EntityId = bblservicedata.DCBC_ENTITY_ID.Trim();
                renewModel.SubmissionLicense = bblservicedata.SubmissionLicense.Trim();
                string submssion = _submissionMasterService.GetmasterId(renewModel.SubmissionLicense, renewModel.UserId);

                //if (submssion != "")
                //{
                renewModel.MasterId = submssion;
                var result = _renewalrepo.CheckRenewal(renewModel);
                if (result == null)
                {
                    return await Task.FromResult(Json("No Data"));
                }
                return await Task.FromResult(Json(result));
                //}
            }
            return await Task.FromResult(Json(false));
        }

        [Authorize]
        [HttpPost]
        [Route("CheckDocumentStatus")]
        public async Task<IHttpActionResult> CheckDocument(DocumentCheck documentCheck)
        {
            var result = _renewalrepo.CheckDocument(documentCheck);
            //if (result == null)
            //{
            //    return Json("No Data");
            //}
            return await Task.FromResult(Json(result));
        }

        [Authorize]
        [HttpPost]
        [Route("UpdateRenwalDocumentType")]
        public async Task<IHttpActionResult> UpdateRenwalDocumentType(DocumentCheck documentCheck)
        {
            var result = _renewalrepo.UpdateRenwalDocumentType(documentCheck);
            //if (result == null)
            //{
            //    return Json("No Data");
            //}
            return await Task.FromResult(Json(result));
        }

        [Authorize]
        [HttpPost]
        [Route("DeleteRenewalData")]
        public async Task<IHttpActionResult> DeleteRenewalData(RenewModel renewModel)
        {
            var result = _renewalrepo.DeleteRenewal(renewModel);
            //if (result == null)
            //{
            //    return Json("No Data");
            //}
            return await Task.FromResult(Json(result));
        }

        [Authorize]
        [HttpPost]
        [Route("GetTaxRevenue")]
        [DeflateCompression]
        public async Task<IHttpActionResult> GetTaxRevenue(RenewModel renewModel)
        {
            var getUserService = _submissionMasterService.GetBblServices(renewModel.UserBblAssociateId);

            var userBblServices = getUserService as UserBBLService[] ?? getUserService.ToArray();
            if (userBblServices.Any())
            {
                var bblservicedata = userBblServices.FirstOrDefault();
                if (bblservicedata != null)
                    renewModel.EntityId = bblservicedata.DCBC_ENTITY_ID.Trim();
                renewModel.SubmissionLicense = bblservicedata.SubmissionLicense.Trim();
                string submssion = _submissionMasterService.GetmasterId(renewModel.SubmissionLicense, renewModel.UserId);

                if (submssion != "")
                {
                    renewModel.MasterId = submssion;
                    var result = _submissionTaxRevenueService.FindByID(submssion);
                    return await Task.FromResult(Json(result));
                }
            }

            return await Task.FromResult(Json(false));
        }

        [Authorize]
        [HttpPost]
        [Route("CheckAmount")]
        [DeflateCompression]
        public async Task<IHttpActionResult> CheckAmount(RenewModel renewModel)
        {
            var getUserService = _submissionMasterService.GetBblServices(renewModel.UserBblAssociateId);

            var userBblServices = getUserService as UserBBLService[] ?? getUserService.ToArray();
            if (userBblServices.Any())
            {
                var bblservicedata = userBblServices.FirstOrDefault();
                if (bblservicedata != null)
                    renewModel.EntityId = bblservicedata.DCBC_ENTITY_ID.Trim();
                renewModel.SubmissionLicense = bblservicedata.SubmissionLicense.Trim();
                string submssion = _submissionMasterService.GetmasterId(renewModel.SubmissionLicense, renewModel.UserId);

                if (submssion != "")
                {
                    renewModel.MasterId = submssion;
                    var result = _renewalrepo.CheckAmount(renewModel);
                    return await Task.FromResult(Json(result));
                }
            }

            return await Task.FromResult(Json(false));
        }

        [Authorize]
        [HttpPost]
        [Route("CheckDocuments")]
        [DeflateCompression]
        public async Task<IHttpActionResult> CheckDocuments(RenewModel renewModel)
        {
            var getUserService = _submissionMasterService.GetBblServices(renewModel.UserBblAssociateId);

            var userBblServices = getUserService as UserBBLService[] ?? getUserService.ToArray();
            if (userBblServices.Any())
            {
                var bblservicedata = userBblServices.FirstOrDefault();
                if (bblservicedata != null)
                    renewModel.EntityId = bblservicedata.DCBC_ENTITY_ID.Trim();
                renewModel.SubmissionLicense = bblservicedata.SubmissionLicense.Trim();
                string submssion = _submissionMasterService.GetmasterId(renewModel.SubmissionLicense, renewModel.UserId);

                if (submssion != "")
                {
                    renewModel.MasterId = submssion;
                    var result = _renewalrepo.CheckDocument(renewModel);
                    return await Task.FromResult(Json(result));
                }
            }

            return await Task.FromResult(Json(false));
        }

        [Authorize]
        [HttpPost]
        [Route("RenewTaxValidation")]
        [DeflateCompression]
        public async Task<IHttpActionResult> TaxValidation(SubmissionTaxRevenuEntity taxrevenue)
        {
            try
            {
                var getUserService = _submissionMasterService.GetBblServices(taxrevenue.UserBblAssociateId);

                var userBblServices = getUserService as UserBBLService[] ?? getUserService.ToArray();
                if (userBblServices.Any())
                {
                    var bblservicedata = userBblServices.FirstOrDefault();
                    if (bblservicedata != null)
                        taxrevenue.EntityId = bblservicedata.DCBC_ENTITY_ID.Trim();
                    taxrevenue.SubmissionLicense = bblservicedata.SubmissionLicense.Trim();
                    string submssion = _submissionMasterService.GetmasterId(taxrevenue.SubmissionLicense, taxrevenue.UserId);

                    if (submssion != "")
                    {
                        taxrevenue.MasterId = submssion;
                        var taxes1 = _submissionTaxRevenueService.SubmissionTaxRevenuInsertUpdate(taxrevenue);
                        RenewModel renewModel = new RenewModel();
                        renewModel.MasterId = taxrevenue.MasterId;
                        renewModel.UserId = taxrevenue.UserId;
                        renewModel.UserBblAssociateId = taxrevenue.UserBblAssociateId;
                        renewModel.EntityId = taxrevenue.EntityId;
                        var result = _renewalrepo.CheckDocument(renewModel);
                        return await Task.FromResult(Json(result));
                    }
                }
                return await Task.FromResult(Json(false));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPost]
        [Route("fulladdress")]
        [DeflateCompression]
        public async Task<IHttpActionResult> BblAddress(TaxRevenueData taxRevenueData)
        {
            try
            {
                var getUserService = _submissionMasterService.GetBblServices(taxRevenueData.UserBblAssociateId);

                var userBblServices = getUserService as UserBBLService[] ?? getUserService.ToArray();
                if (userBblServices.Any())
                {
                    var bblservicedata = userBblServices.FirstOrDefault();
                    if (bblservicedata != null)
                        taxRevenueData.EntityId = bblservicedata.DCBC_ENTITY_ID.Trim();
                    taxRevenueData.SubmissionLicense = bblservicedata.SubmissionLicense.Trim();
                    string submssion = _submissionMasterService.GetmasterId(taxRevenueData.SubmissionLicense, taxRevenueData.UserId);

                    if (submssion != "")
                    {
                        taxRevenueData.MasterId = submssion;
                        var result = _submissionMasterService.GetAddressDetails(taxRevenueData);
                        var userdetails = _bblAssociateService.CheckUserBBL(taxRevenueData.SubmissionLicense, taxRevenueData.UserId).ToList();
                        if (userdetails.Count() != 0)
                        {
                            result.TaxType = (userdetails.FirstOrDefault().CleanHandsType_SSN_FEIN ?? "").Trim();
                            result.TaxNumber = (result.TaxNumber ?? "").Trim();
                            var index = result.TaxNumber.Length;
                            if (result.TaxType.ToUpper() == "FEIN" && result.TaxNumber != "" && index == 9)
                            {
                                result.TaxNumber = result.TaxNumber.Substring(0, 2).ToString() + "-" + result.TaxNumber.Remove(0, 2);
                            }
                            else if (result.TaxType.ToUpper() == "SSN" && result.TaxNumber != "" && index == 9)
                            {
                                string first = result.TaxNumber.Substring(0, 3).ToString();
                                string middle = result.TaxNumber.Remove(0, 3).ToString();
                                middle = middle.Substring(0, 2);
                                string last = result.TaxNumber.Remove(0, 5).ToString();
                                result.TaxNumber = first + "-" + middle + "-" + last;
                            }
                        }
                        return await Task.FromResult(Json(result));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(Json(false));
        }
    }
}