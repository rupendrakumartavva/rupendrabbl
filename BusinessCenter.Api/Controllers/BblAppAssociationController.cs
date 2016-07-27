//using BusinessCenter.Api.App_Start;
//using BusinessCenter.Api.Common;
using BusinessCenter.Api.Filters;
using BusinessCenter.Api.Models;
using BusinessCenter.Api.PayPalUtility;
using BusinessCenter.Api.Utility;
using BusinessCenter.Common;
using BusinessCenter.Data;
using BusinessCenter.Data.Common;
//using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using BusinessCenter.Email;
using BusinessCenter.Service.Interface;
using RazorEngine;
using System;
using System.Collections.Generic;
using System.Configuration;
//using System.Data.Entity.Core.Objects;
//using System.IdentityModel.Protocols.WSTrust;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BusinessCenter.Api.Controllers
{
    [RoutePrefix("api/BBLAssociation")]
    [UpdateTokenLifeTime]
    public class BblAppAssociationController : ApiController
    {
        private readonly IBBLAssociateService _bblAssociateService;
     //   private readonly IMasterBusinessActivityService _bblActivities;
        private readonly ISubmissionMasterService _submissionMaster;
        private readonly ISubmissionGeneratedDocumentService _businessEntityGenarationPdfService;
        private readonly ISubmissionDocumentToAccelaService _submissionDocumentToAccelaService;
        private readonly IMailTemplateService _mailTemplateService;

        public int DocInt;

        protected IPayPalHelper Payhelper;
        protected readonly IEmailTemplate MailDetails;
        private readonly ISubmissionTaxRevenueService _taxRevenueService;
        public decimal TotalApplicationFee1 = 0;
        public decimal TotalEndosementFee1 = 0;
        public decimal TotalLicenseFee1 = 0;
        public decimal TotalTechFee1 = 0;
        public decimal GrandTotal1 = 0;

        public BblAppAssociationController(IBBLAssociateService bblAssociateService,
          ISubmissionMasterService submissionMaster, ISubmissionTaxRevenueService taxRevenueService,
         IPayPalHelper payPalHelper,
            IEmailTemplate mailDetails, ISubmissionGeneratedDocumentService businessEntityGenarationPdfService,
            ISubmissionDocumentToAccelaService submissionDocumentToAccelaService, IMailTemplateService mailTemplateService
          )
        {
            _bblAssociateService = bblAssociateService;
       
            _submissionMaster = submissionMaster;
            Payhelper = payPalHelper;
            MailDetails = mailDetails;
            _taxRevenueService = taxRevenueService;
            _businessEntityGenarationPdfService = businessEntityGenarationPdfService;
            _submissionDocumentToAccelaService = submissionDocumentToAccelaService;

            _mailTemplateService = mailTemplateService;
        }

        /// <summary>
        /// Validating licenseNumbers and PinNumbers
        /// This Method is called in AssociateBBL Contoller
        /// </summary>
        /// <param name="bblAssociate"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("CheckAssociate")]
        [DeflateCompression]
        public async Task<IHttpActionResult> CheckBBlAssociate(BblAsscoiatePin bblAssociate)
        {
            string data;
            var getBblListData = _bblAssociateService.GetAssociteData(bblAssociate.LicenseNumber);
            var dcbcEntityBbls = getBblListData as IList<DCBC_ENTITY_BBL> ?? getBblListData.ToList();
            // ReSharper disable once UseCollectionCountProperty
            if (getBblListData != null && dcbcEntityBbls.Count() != 0)
            {
                var dcbcEntityBbl = dcbcEntityBbls.FirstOrDefault();
                if (dcbcEntityBbl != null)
                    bblAssociate.EntityId = Convert.ToString(dcbcEntityBbl.DCBC_ENTITY_ID);
                if (_bblAssociateService.FindByLicenseTax(bblAssociate))
                {
                    if (dcbcEntityBbls.Any())
                    {
                        var checkuser = _bblAssociateService.FindByPin(bblAssociate).ToList();
                        if (checkuser.Any())
                        {
                            var checkuserdata = checkuser.FirstOrDefault();
                            if (checkuserdata != null)
                            {
                                var userdata = _bblAssociateService.CheckUserBBL((checkuserdata.b1_Alt_ID ?? ""), bblAssociate.UserId).ToList();
                                if (userdata.Any())
                                {
                                    data = "Already Associate";
                                    return Json(new { status = data });
                                }
                                else
                                {
                                    //   var bblrenewData = checkuser.FirstOrDefault();
                                    // _bblAssociate.UpdateUserAssociate(bblAssociate);
                                    var bblData = dcbcEntityBbls.FirstOrDefault(); if (bblData != null)
                                        return Json(new
                                        {
                                            isValidated = true,
                                            entityId = bblData.DCBC_ENTITY_ID,
                                            // ReSharper disable once RedundantToStringCall
                                            licenseNumber = bblData.B1_ALT_ID == null ? "" : bblData.B1_ALT_ID.ToString().ToUpper().Trim() == "NA"? "" : bblData.B1_ALT_ID.ToString().Trim(),
                                            // ReSharper disable once RedundantToStringCall
                                            fName = bblData.Contact_FirstName == null ? "" : bblData.Contact_FirstName.ToString().ToUpper() == "N/A" ? "" : bblData.Contact_FirstName.ToString().Trim(),
                                            // ReSharper disable once RedundantToStringCall
                                            lName = bblData.Contact_LastName == null ? "" : bblData.Contact_LastName.ToString().ToUpper() == "N/A" ? "" : bblData.Contact_LastName.ToString().Trim(),
                                            // ReSharper disable once RedundantToStringCall
                                            businessName = bblData.OwnrApplicant_BUSINESS_NAME == null ? "" : bblData.OwnrApplicant_BUSINESS_NAME.ToString().ToUpper().Trim() == "NA"
                                                ? "" : bblData.OwnrApplicant_BUSINESS_NAME.ToString().Trim(),
                                            // ReSharper disable once RedundantToStringCall
                                            tradeName = bblData.Attr_TRADE_NAME == null ? "NA" : bblData.Attr_TRADE_NAME.ToString().Trim(),
                                            // ReSharper disable once RedundantToStringCall
                                            businessNameStructure = bblData.Business_Org == null ? "" : bblData.Business_Org.ToString().ToUpper().Trim() == "NA"
                                                ? "" : bblData.Business_Org.ToString().Trim(),
                                            expDate = bblData.Expiration_Date == null ? "" : bblData.Expiration_Date.Trim(),
                                            // ReSharper disable once RedundantToStringCall
                                            status = bblData.B1_APPL_STATUS == null ? "" : bblData.B1_APPL_STATUS.ToString().ToUpper().Trim() == "NA"
                                                ? "" : bblData.B1_APPL_STATUS.ToString().Trim(),
                                            CleanHandsType = bblAssociate.CleanHandsType.ToUpper().Trim(),
                                            B1_Alt_Id = checkuserdata.License_Being_Renewed,
                                            SubmissionLicenseNumber = checkuserdata.b1_Alt_ID
                                        });
                                }
                            }
                        }
                    }
                }
            }
            data = "NODATA";
            return await Task.FromResult(Json(new { status = data }));
            // return Json(new { isValidated = false });
        }

        [Authorize]
        [HttpPost]
        [Route("UserServiceDetailsOnId")]
        [DeflateCompression]
        public async Task<IHttpActionResult> UserAssociateBblBasedOnServiceId(BblAsscoiatePin bblAssociate)
        {
            string data;
            var getbblServicedata = _bblAssociateService.UserbblServiceFindById(Convert.ToInt32(bblAssociate.UserBblAssociateId)).ToList();

            //var dcbcEntityBbls = getbblServicedata as IList<UserBBLService> ?? getbblServicedata.ToList();
            if (getbblServicedata != null && getbblServicedata.Any())
            {
                var bblservicedata = getbblServicedata.FirstOrDefault();
                var getBblListData = _bblAssociateService.GetBblDataOnEntityId(Convert.ToInt32(bblservicedata.DCBC_ENTITY_ID));

               // var dcbcEntityBbl = getbblServicedata.FirstOrDefault();
               // if (dcbcEntityBbl != null)
                    bblAssociate.EntityId = Convert.ToString(bblservicedata.DCBC_ENTITY_ID);

                if (getbblServicedata.Any())
                {
                    //var bblrenew = _bblAssociateService.RenewalData((dcbcEntityBbl.B1_ALT_ID ?? "").Trim(), (dcbcEntityBbl.SubmissionLicense ?? "").Trim())
                    //    .OrderByDescending(x => x.B1_APPL_STATUS_DATE);
                    //_bblAssociateService.GetAssociteData((dcbcEntityBbl.B1_ALT_ID ?? "").Trim());

                    //  var bblrenewdata = bblrenew.FirstOrDefault();
                    //var userdata = _bblAssociateService.CheckUserBBL(getbblServicedata.FirstOrDefault().SubmissionLicense, bblAssociate.UserId).ToList();
                    //if (userdata.Count() != 0)
                    //{
                    //    data = "Already Associate";
                    //    return Json(new { status = data });
                    //}
                    //else
                    //{
                    var bblData = getBblListData.FirstOrDefault();
                    if (bblData != null)
                        return Json(new
                        {
                            isValidated = true,
                            entityId = bblData.DCBC_ENTITY_ID,
                            licenseNumber = bblservicedata.B1_ALT_ID == null ? "" : bblservicedata.B1_ALT_ID.ToString().ToUpper().Trim() == "NA"
                                        ? "" : bblservicedata.B1_ALT_ID.ToString().Trim(),
                            fName = bblData.Contact_FirstName == null ? "" : bblData.Contact_FirstName == "N/A" ? "" : bblData.Contact_FirstName.ToString().Trim(),
                            lName = bblData.Contact_LastName == null ? "" : bblData.Contact_LastName == "N/A" ? "" : bblData.Contact_LastName.ToString().Trim(),
                            businessName = bblData.OwnrApplicant_BUSINESS_NAME == null ? "" : bblData.OwnrApplicant_BUSINESS_NAME.ToString().ToUpper().Trim() == "NA"
                                        ? "" : bblData.OwnrApplicant_BUSINESS_NAME.ToString().Trim(),
                            tradeName = bblData.Attr_TRADE_NAME == null ? "NA" : bblData.Attr_TRADE_NAME.ToString().Trim(),
                            businessNameStructure = bblData.Business_Org == null ? "" : bblData.Business_Org.ToString().ToUpper().Trim() == "NA"
                                        ? "" : bblData.Business_Org.ToString().Trim(),
                            expDate = bblData.Expiration_Date == null ? "" : bblData.Expiration_Date.Trim(),
                            status = bblData.B1_APPL_STATUS == null ? "" : bblData.B1_APPL_STATUS.ToString().ToUpper().Trim() == "NA"
                                        ? "" : bblData.B1_APPL_STATUS.ToString().Trim(),
                            B1_Alt_Id = bblservicedata.B1_ALT_ID,
                            SubmissionLicenseNumber = bblservicedata.SubmissionLicense
                        });
                    //  }
                }
            }
            // }
            //   }
            data = "NODATA";
            return await Task.FromResult(Json(new { status = data }));
            // return Json(new { isValidated = false });
        }

        /// <summary>
        /// Associate the license into the particular user in the userbblservice
        /// </summary>
        /// <param name="bblService"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("AssociateBblService")]
        [DeflateCompression]
        public async Task<IHttpActionResult> AssociateBblService(BblAsscoiateService bblService)
        {
            var result = _bblAssociateService.InsertAssociateBbl(bblService);
            return await Task.FromResult(Json(new { Result = result }));
        }

        /// <summary>
        /// This  method is Used for display the MYbbl data
        /// This  method is called in Mybbl controller
        /// </summary>
        /// <param name="bblService"></param>
        /// <returns></returns>

        [Authorize]
        [HttpPost]
        // [AllowAnonymous]
        [Route("BblServiceList")]
        public async Task<IHttpActionResult> BblServiceList(BblAsscoiateService bblService)
        {
            var result = _submissionMaster.GetBblService(bblService).ToList();
            foreach (var bblservice in result[0].BblServiceList)
            {
                if (bblservice.UserAssociateType.ToUpper().Trim() == "S" && bblservice.ChcekEhopAllow.ToUpper().Trim() == "YES")
                {
                    var cofodata = _bblAssociateService.EhopNumberWithMasterId(bblservice.MasterId.Trim()).ToList();
                    if (cofodata.Any())
                    {
                        var cofodatadetails = cofodata.FirstOrDefault();
                        if (cofodatadetails != null && cofodatadetails.UserSelectType.Replace(Environment.NewLine, "").Trim().ToUpper() == "EHOP")
                        {
                            bblservice.IsEhop = true;
                            bblservice.EhopNumber =
                                cofodatadetails.Number.Replace(Environment.NewLine, "").Trim() ?? "";
                            bblservice.EhopType = "EHOP";
                            result[0].BBLServiceCount[0].eHOP = result[0].BBLServiceCount[0].eHOP + 1;

                            bblservice.BusinessName = (bblservice.BusinessName ?? "").Trim();
                            bblservice.PremiseAddress = (bblservice.PremiseAddress ?? "").Trim();
                        }
                    }
                    else
                    {
                        bblservice.IsEhop = false;
                        bblservice.EhopNumber = string.Empty;
                    }
                }
            }

            if (!result.Any())
            {
                return await Task.FromResult(Json("No Data"));
            }
            return await Task.FromResult(Json(new { Result = result }));
        }

        [Authorize]
        [HttpPost]
        [Route("UserBblExpCount")]
        public async Task<IHttpActionResult> UserBblAssociateCount(BblAsscoiateService bblService)
        {
            var result = _submissionMaster.GetUserAssociateBblCount(bblService);
            return await Task.FromResult(Json(new { Result = result }));
        }

        //
        /// <summary>
        /// This  method is Used for Remove the BBLs
        /// This  method is called in Mybbl controller
        /// </summary>
        /// <param name="bblService"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("BblRemoveServiceList")]
        [DeflateCompression]
        public async Task<IHttpActionResult> BblRemoveServiceList(BblAsscoiateService bblService)
        {
            var result = _bblAssociateService.DeleteUserService(bblService);
            //if (result == null)
            //{
            //    return await Task.FromResult(Json("No Data"));
            //}
            return await Task.FromResult(Json(new { Result = result }));
        }

        [Authorize]
        [HttpPost]
        [Route("CofoChecklistApp")]
        [DeflateCompression]
        public async Task<IHttpActionResult> UpdateIsCofo(CofoHopDetailsModel cofoHopDetailsModel)
        {
            var iscofoupdate = _bblAssociateService.UpdateIsCofoinChecklistApp(cofoHopDetailsModel);
            if (iscofoupdate == false)
            {
                return await Task.FromResult(Json("No Data"));
            }
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            return await Task.FromResult(Json(new { Result = iscofoupdate }));
        }

        [Authorize]
        [HttpPost]
        [Route("ChecklistAppConditions")]
        [DeflateCompression]
        public async Task<IHttpActionResult> UpdateChecklistAppConditions(CofoHopDetailsModel cofoHopDetailsModel)
        {
            var ischecklistupdate = _bblAssociateService.UpdateAllCheckListConditions(cofoHopDetailsModel);
            if (ischecklistupdate == false)
            {
                return await Task.FromResult(Json("No Data"));
            }
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            return await Task.FromResult(Json(new { Result = ischecklistupdate }));
        }

        [Authorize]
        [HttpPost]
        [Route("SubmitHAorPA")]
        [DeflateCompression]
        public async Task<IHttpActionResult> SubmitHAorPa(GeneralBusiness bbldoc)
        {
            var ischecklistupdate = _submissionMaster.UpdateUserSelect(bbldoc);
            if (ischecklistupdate == false)
            {
                return await Task.FromResult(Json("No Data"));
            }
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            return await Task.FromResult(Json(ischecklistupdate));
        }

        [Authorize]
        [HttpPost]
        [Route("EhopEligibility")]
        [DeflateCompression]
        public async Task<IHttpActionResult> InsertEhopEligibilities(EligibilityModel eligibilityModel)
        {
            var eligibles = _bblAssociateService.InsertEHopEligibility(eligibilityModel);
            if (eligibles == false)
            {
                return await Task.FromResult(Json("No Data"));
            }
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            return await Task.FromResult(Json(new { Result = eligibles }));
        }

        [Authorize]
        [HttpPost]
        [Route("SubmissionUpdate")]
        [DeflateCompression]
        public async Task<IHttpActionResult> UpdateSubmissionMaster(BblDocuments bbldoc)
        {
            var result = _bblAssociateService.UpdateSubmissionMaster(bbldoc);

            //if (result == null)
            //{
            //    return await Task.FromResult(Json("No Data"));
            //}
            return await Task.FromResult(Json(result));
        }

        [HttpPost]
        [Route("BblServiceDocument")]
        [DeflateCompression]
        public async Task<IHttpActionResult> BblServiceDocumentList()
        {
           // string activityresult = "No Data";
            //foreach (var document in bblServiceDocuments.BblServiceDoc)
            //{(bblServiceDocuments.FileName, Destinationpath)

            BblServiceDocuments docs = GenaralConvertion.fileupload();
            if (docs != null)
            {
                var result = _bblAssociateService.InsertServiceDocuments(docs);

                if (result == null)
                {
                    return await Task.FromResult(Json("No Data"));
                }
              //  activityresult = result.ToString();
                return Json(result);
            }
            return await Task.FromResult(Json("Not Upload"));
            // }
        }

        [Authorize]
        [HttpPost]
        [Route("BblRequiredDocuments")]
        [DeflateCompression]
        public async Task<IHttpActionResult> BblRequiredDocuments(BblDocuments bbldoc)
        {
            var validateCorpFileStatus = _bblAssociateService.CorpServiceStatus(bbldoc.MasterId);
            DateTime updatedate = Convert.ToDateTime(DateTime.Now).Date;
            DateTime presentdate = Convert.ToDateTime(DateTime.Now).Date;
            var resultTextNumberValidate = _taxRevenueService.FindByID(bbldoc.MasterId).ToList();
            if (resultTextNumberValidate.Any())
            {
                updatedate = Convert.ToDateTime(resultTextNumberValidate.First().UpdatedDate).Date;
                presentdate = Convert.ToDateTime(DateTime.Now).Date;

                if (updatedate != presentdate)
                {
                    _taxRevenueService.TaxAndRevenueUpdate(bbldoc.MasterId);
                }
            }
            var result = _bblAssociateService.DocumentList(bbldoc).ToList();
            //if (resultTextNumberValidate.Any())
            //{
            foreach (var checktax in result)
            {
                if (updatedate != presentdate)
                {
                    checktax.IsTaxReattested = true;
                }
                else
                {
                    checktax.IsTaxReattested = false;
                }
            }
            // }

            if (!result.Any())
            {
                return await Task.FromResult(Json("No Data"));
            }
            return await Task.FromResult(Json(new { result = result, validateCorpFileStatus = validateCorpFileStatus.OriginalCorpStatus, CorpChangeStatus = validateCorpFileStatus.ChangeCorpStatus }));
        }

        [Authorize]
        [HttpPost]
        [Route("GetMailType")]
        [DeflateCompression]
        public async Task<IHttpActionResult> BblRequiredDocuments(GeneralBusiness detailsModel)
        {
            var result = _submissionMaster.GetMailType(detailsModel);

            if (result == null)
            {
                return await Task.FromResult(Json("No Data"));
            }
            return await Task.FromResult(Json(result));
        }

        [Authorize]
        [HttpPost]
        [Route("DeleteDocument")]
        [DeflateCompression]
        public async Task<IHttpActionResult> DeleteDocument(BblServiceDocuments bbldoc)
        {
            var result = _bblAssociateService.DeleteDocuments(bbldoc);

            //if (result == null)
            //{
            //    return await Task.FromResult(Json("No Data"));
            //}
            return await Task.FromResult(Json(result));
        }

        public Ehopdetails UploadEhopDocuments(string masterid, string destinationEhopFileName, DateTime paymentDate)
        {
            Ehopdetails ehopdetails = new Ehopdetails();
            var view = "EhopAddressDetails";
            string fileName ;
            var ehopData = new EhopData { MasterID = masterid };
            var ehopDetails = _bblAssociateService.EhopData(ehopData);

            var submissionmaster = _submissionMaster.FindByMasterID(masterid).ToList();
            if (submissionmaster.Count != 0)
            {
                var masterdata = submissionmaster.FirstOrDefault();
                if (masterdata != null)
                {
                    ehopDetails.Name = (masterdata.BusinessName ?? "").Trim();
                    ehopDetails.UserName = (masterdata.BusinessName ?? "").Trim();
                }
                ehopDetails.CreatedDate = Convert.ToDateTime(paymentDate).ToString("MM/dd/yyyy");
            }
            destinationEhopFileName = ehopData.PermitNumber + ".pdf";
            fileName = destinationEhopFileName;
            ehopDetails.DCLogo = System.Configuration.ConfigurationManager.AppSettings["siteAddress"] + "/images/dcgov_logo.png";
            ehopDetails.EhopSignature = System.Configuration.ConfigurationManager.AppSettings["siteAddress"] + "/images/ehopsignature.png";
            ehopdetails.EhopByteCode = new PdfResult(view, ehopDetails, fileName, destinationEhopFileName).FileStorageSection();
            ehopdetails.EhopFilename = fileName;
            return ehopdetails;
        }

        public byte[] ViewReceipt(string masterId, string fileName, DateTime paymentDate)
        {
            var view = "OrderDetailsView";
            BasicBusinessLicense basicBusinessLicense = new BasicBusinessLicense { MasterId = masterId };
            var bblreceipt = _bblAssociateService.BusinessReceipt(basicBusinessLicense);

            // bblreceipt.ReceiptLogo = System.Configuration.ConfigurationManager.AppSettings["siteAddress"] + "/images/" + System.Configuration.ConfigurationManager.AppSettings["logoPath"];
            bblreceipt.ReceiptLogo = System.Configuration.ConfigurationManager.AppSettings["siteAddress"] + "/images/dcgov_logo.png";
            if (bblreceipt != null)
            {
                bblreceipt.DateIssued = Convert.ToDateTime(paymentDate).ToString("MM/dd/yyyy");
            }

            bblreceipt.Signature = System.Configuration.ConfigurationManager.AppSettings["siteAddress"] + "/images/ehopsignature.png";
            return new PdfResult(view, bblreceipt, fileName, fileName).FileStorageSection();
        }

        public byte[] ViewRenewalReceipt(string masterId, string fileName, DateTime paymentDateTime)
        {
            var view = "RenewalOrderPdf";
            var renewBasicBusinessLicense = new RenewBasicBusinessLicense { MasterId = masterId };
            var bblreceipt = _bblAssociateService.RenewBusinessReceipt(renewBasicBusinessLicense);
            bblreceipt.ReceiptLogo = System.Configuration.ConfigurationManager.AppSettings["siteAddress"] + "/images/dcgov_logo.png";
            // bblreceipt.ReceiptLogo = System.Configuration.ConfigurationManager.AppSettings["siteAddress"] + "/images/" + System.Configuration.ConfigurationManager.AppSettings["logoPath"];
            bblreceipt.Signature = System.Configuration.ConfigurationManager.AppSettings["siteAddress"] + "/images/ehopsignature.png";
            return new PdfResult(view, bblreceipt, fileName, fileName).FileStorageSection();
        }

        [Authorize]
        [HttpPost]
        [Route("SubmitPayment")]
        [DeflateCompression]
        public async Task<IHttpActionResult> SubmitPayment(PaymentDetailsModel pDetails)
        {
            var resultValidateCorpFileStatus = "Active";
            var resultTextNumberValidate = false;
            string renewalLicenceNumber = string.Empty;
            if (pDetails.PaymentType.ToUpper() != GenericEnums.GetEnumDescription(
                                           GenericEnums.CategoryTypes.Renewal).ToUpper())
            {
                resultValidateCorpFileStatus = _bblAssociateService.CorpServiceStatus(pDetails.MasterId).OriginalCorpStatus;
            }
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if ((resultValidateCorpFileStatus.ToUpper() == GenericEnums.GetEnumDescription(
                                           GenericEnums.ApplicationStatus.Active).ToUpper()) && (resultTextNumberValidate == false))
            {
                var result = _bblAssociateService.InsertPaymentDetails(pDetails);

                if (result == null)
                {
                    return await Task.FromResult(Json("No Data"));
                }
                bool finalPaymentSuccess = false;
                DateTime finalPaymentPaiddate;
                string paymentTranscationId;
                string paymentStatus;
                PayPalTransaction paymentTransactionResult = new PayPalTransaction();
                decimal chcekFinalAmount = pDetails.TotalAmount == null ? 0 : Convert.ToDecimal(pDetails.TotalAmount);
                if (chcekFinalAmount == 0)
                {
                    Random rnd = new Random();
                    finalPaymentPaiddate = DateTime.Now;
                    paymentTranscationId = "CH" + Guid.NewGuid().ToString().Substring(0, 2).ToUpper() +
                                           rnd.Next(000, 999) +
                                           Guid.NewGuid().ToString().Substring(0, 3).ToUpper() +
                                           DateTime.Now.Year.ToString().Substring(2, 2);
                    paymentStatus = "Approved";
                    finalPaymentSuccess = true;
                    paymentTransactionResult.Id = paymentTranscationId;
                    paymentTransactionResult.Time = finalPaymentPaiddate;
                    paymentTransactionResult.Message = paymentStatus;
                    paymentTransactionResult.Success = true;
                }
                else
                {
                    var payModel = new PaymentModel
                    {
                        Amount = pDetails.TotalAmount == null ? 0 : Convert.ToDecimal(pDetails.TotalAmount),
                        BillingCity = pDetails.City == null ? "" : pDetails.City.Trim(),
                        BillingState = pDetails.State ?? "",
                        BillingStreetAddress = pDetails.StreetName == null ? "" : pDetails.StreetName.Trim(),
                        BillingZip = pDetails.Zip == null ? "" : pDetails.Zip.Trim(),
                        BillToCountry = pDetails.Country == null ? "" : pDetails.Country.Trim(),
                        BillToPhone2 = pDetails.ContactNumber1 == null ? "" : pDetails.ContactNumber1.Trim(),
                        BillToStreet2 = "",
                        CardSecurityCode = pDetails.CvvNumber == null ? "" : pDetails.CvvNumber.Trim(),
                        CreditCardNumber = pDetails.CardNumber == null ? "" : pDetails.CardNumber.Trim().Replace("-", "").Trim(),
                        City = pDetails.City == null ? "" : pDetails.City.Trim(),
                        CompanyName = pDetails.BusinessName == null ? "" : pDetails.BusinessName.Trim(),
                        Email = pDetails.Email == null ? "" : pDetails.Email.Trim(),
                        ExpirationDate =
                            (pDetails.CardExpMonth == null ? "" : pDetails.CardExpMonth.Trim()) +
                            (pDetails.CardExpYear == null ? "" : pDetails.CardExpYear.Substring(2, 2).Trim())
                    };
                    var payresult = Payhelper.Pay(payModel);
                    paymentTransactionResult = payresult;
                    if (payresult.Success)
                    {
                        finalPaymentPaiddate = payresult.Time;
                        paymentTranscationId = payresult.Id ?? "";
                        paymentStatus = payresult.Message;
                        finalPaymentSuccess = payresult.Success;
                    }
                    else
                    {
                        finalPaymentPaiddate = payresult.Time;
                        paymentTranscationId = payresult.Id ?? "";
                        paymentStatus = payresult.Message;
                    }
                }

                if (finalPaymentSuccess)
                {
                    Random rnd = new Random();

                    pDetails.OrderNumber = "DCRABBLORDER" + DateTime.Now.Year.ToString().Substring(2, 2) + "9" +
                                           rnd.Next(00000, 99999);
                    pDetails.PaymentDate = finalPaymentPaiddate;
                    pDetails.TranscationId = paymentTranscationId ?? "";
                    pDetails.PaymentStatus = paymentStatus;
                    pDetails.ApplicationTransactionStatus = true;
                    _bblAssociateService.UpdatePaymentDetails(pDetails);
                    _submissionMaster.UpdateSubmissionMaster(pDetails);
                    var userdetails = _submissionMaster.UserBblDetails(pDetails.MasterId).ToList();
                    if (pDetails.PaymentType.ToUpper() != GenericEnums.GetEnumDescription(GenericEnums.CategoryTypes.Renewal).ToUpper())
                    {
                        _submissionMaster.UpdateUserSubmissionExpiryDate(pDetails.MasterId);
                        _bblAssociateService.DocumentInsertion(pDetails.MasterId, pDetails.SubmissionLicense);
                    }
                    else
                    {
                        _submissionMaster.UpdateUserAssociateExpiryDate(pDetails.MasterId);

                        _bblAssociateService.RenewalStatuUpdation(pDetails.MasterId, userdetails.FirstOrDefault().SubmissionLicense);
                        renewalLicenceNumber = userdetails.FirstOrDefault().B1_ALT_ID;
                    }

                    if (pDetails.PaymentMailAddress != null)
                    {
                        var getSubmission = _submissionMaster.FindByMasterID(pDetails.MasterId).ToList();
                        if (getSubmission.Any())
                        {
                            bool ehop = false;
                            var checklist = _bblAssociateService.FindByMasterId(pDetails.MasterId).ToList();
                            if (checklist.Count != 0)
                            {
                                ehop = Convert.ToBoolean(checklist.FirstOrDefault().IsSubmissioneHop);
                            }
                            string noSupportingDocuments = getSubmission.FirstOrDefault().DocSubmType;
                            var sbEhopFileName = new StringBuilder();
                            var nosupportingDocumentFileName = new StringBuilder();
                          //  var rn = new Random();
                            if (ehop)
                            {
                                var ehopFileByteCode = UploadEhopDocuments(pDetails.MasterId,
                                    sbEhopFileName.ToString(), pDetails.PaymentDate);
                                SubmissionGeneratedDocumentDetails(getSubmission.FirstOrDefault().MasterId,
                                getSubmission.FirstOrDefault().SubmissionLicense,
                                getSubmission.FirstOrDefault().UserID, ehopFileByteCode.EhopByteCode, ehopFileByteCode.EhopFilename,
                                GenericEnums.GetEnumDescription(
                                    GenericEnums.SubmissionGenerationDocumentType.eHOP).ToUpper());
                                sbEhopFileName.Append(ehopFileByteCode.EhopFilename);
                                var accelaResult = InsertSubmissionDocumentsToAccela(pDetails.MasterId,
                                       getSubmission.FirstOrDefault().SubmissionLicense
                                       , ehopFileByteCode.EhopFilename, GenericEnums.GetEnumDescription(
                                          GenericEnums.ManualMasterDocumentsId.ManualEhopId));
                            }

                            if (noSupportingDocuments == "")
                            {
                                nosupportingDocumentFileName.Append(getSubmission.FirstOrDefault().SubmissionLicense);
                                nosupportingDocumentFileName.Append(".pdf");

                                if (pDetails.PaymentType.ToUpper() == GenericEnums.GetEnumDescription(
                                      GenericEnums.CategoryTypes.Renewal).ToUpper())
                                {
                                    nosupportingDocumentFileName = new StringBuilder();
                                    nosupportingDocumentFileName.Append(renewalLicenceNumber);

                                    if (GetPresentMonth())
                                    {
                                        nosupportingDocumentFileName.Append(GenericEnums.GetEnumDescription(
                                            GenericEnums.SubmissionGenerationDocumentType.RenewDocFileExtension));
                                        nosupportingDocumentFileName.Append(
                                            (DateTime.Now.Year + 1).ToString().Substring(2, 2));
                                    }
                                    else
                                    {
                                        nosupportingDocumentFileName.Append(GenericEnums.GetEnumDescription(
                                             GenericEnums.SubmissionGenerationDocumentType.RenewDocFileExtension));
                                        nosupportingDocumentFileName.Append((DateTime.Now.Year).ToString().Substring(2, 2));
                                    }

                                    nosupportingDocumentFileName.Append(".pdf");
                                    var nosupportingDocByteCode = ViewRenewalReceipt(pDetails.MasterId, nosupportingDocumentFileName.ToString(),
                                       pDetails.PaymentDate);
                                    //nosupportingDocumentFileName.Append(".pdf");
                                    SubmissionGeneratedDocumentDetails(getSubmission.FirstOrDefault().MasterId,
                                        pDetails.SubmissionLicense,
                                        getSubmission.FirstOrDefault().UserID, nosupportingDocByteCode, nosupportingDocumentFileName.ToString(),
                                        GenericEnums.GetEnumDescription(
                                            GenericEnums.SubmissionGenerationDocumentType.RenewalNodocs).ToUpper());
                                    var accelaResult = InsertSubmissionDocumentsToAccela(pDetails.MasterId,
                                    pDetails.SubmissionLicense
                                     , nosupportingDocumentFileName.ToString(), GenericEnums.GetEnumDescription(
                                           GenericEnums.ManualMasterDocumentsId.ManualRenewalReceiptId));
                                }
                                else
                                {
                                    var nosupportingDocByteCode = ViewReceipt(pDetails.MasterId, nosupportingDocumentFileName.ToString(), pDetails.PaymentDate);
                                    SubmissionGeneratedDocumentDetails(getSubmission.FirstOrDefault().MasterId,
                                        getSubmission.FirstOrDefault().SubmissionLicense,
                                        getSubmission.FirstOrDefault().UserID, nosupportingDocByteCode, nosupportingDocumentFileName.ToString(),
                                        GenericEnums.GetEnumDescription(
                                            GenericEnums.SubmissionGenerationDocumentType.Nodocs).ToUpper());
                                    var accelaResult = InsertSubmissionDocumentsToAccela(pDetails.MasterId,
                                     getSubmission.FirstOrDefault().SubmissionLicense
                                     , nosupportingDocumentFileName.ToString(), GenericEnums.GetEnumDescription(
                                           GenericEnums.ManualMasterDocumentsId.ManualReceiptId));
                                }
                            }
                        }
                        if (pDetails.PaymentType.ToUpper() != GenericEnums.GetEnumDescription(
                            GenericEnums.CategoryTypes.Renewal).ToUpper())
                        {
                            pDetails.PaymentType = "NewSubmission";
                        }
                        else
                        {
                            pDetails.PaymentType = GenericEnums.GetEnumDescription(
                                GenericEnums.CategoryTypes.Renewal).ToUpper();
                        }

                        string siteAddress = ConfigurationManager.AppSettings["siteAddress"];
                        var body = OrderPlaceing(pDetails.Signature, siteAddress,
                            pDetails.MasterId, pDetails.PaymentId, pDetails.PaymentMailAddress.ToLower().Trim(), pDetails.GrandTotal,
                            pDetails.TotalLicenseFee, pDetails.TotalTechFee, pDetails.TotalEndosementFee,
                            pDetails.TotalApplicationFee, pDetails.PaymentType.ToUpper());
                        var mailTemplateModel = new MailTemplateModel
                        {
                            UserId = userdetails.FirstOrDefault().UserID.ToString().Trim(),
                            CustomApplicationId = pDetails.MasterId,
                            Type = "ORDER CONFIRMATION",
                            Subject = "Order Confirm",
                            MailSentFailCount = 0,
                            MailContent = body,
                            IsMailSent = true
                        };
                        _mailTemplateService.InsertUpdateMailTemplate(mailTemplateModel);
                        MailDetails.MailSending("Order Confirm", body,
                            pDetails.PaymentMailAddress.ToLower().Trim());
                    }
                }
                else
                {
                    pDetails.OrderNumber = string.Empty;
                    pDetails.PaymentDate = finalPaymentPaiddate;
                    pDetails.TranscationId = paymentTranscationId ?? "";
                    pDetails.PaymentStatus = paymentStatus;
                    pDetails.ApplicationTransactionStatus = false;
                    _bblAssociateService.UpdatePaymentDetails(pDetails);
                }
                return Json(new
                {
                    paymentId = result,
                    trasactionresult = paymentTransactionResult,
                    finalsuccess = "YES",
                    masterTextRevenue = resultTextNumberValidate,
                    validateCorpFileStatus = resultValidateCorpFileStatus
                });
            }
            else
            {
                return await Task.FromResult(Json(new
                {
                    trasactionresult = false,
                    finalsuccess = "NO",
                    masterTextRevenue = false,
                    validateCorpFileStatus = resultValidateCorpFileStatus
                }));
            }
        }

        public bool GetPresentMonth()
        {
            bool status = false;
            switch (DateTime.Now.Month.ToString())
            {
                case "10":
                    status = true;
                    break;

                case "11":
                    status = true;
                    break;

                case "12":
                    status = true;
                    break;

                default:
                    status = false;
                    break;
            }
            return status;
        }

        private bool InsertSubmissionDocumentsToAccela(string masterId, string licenseNumber, string fileName, string masterCategoryId)
        {
            var submissionDocumentToAccelaEntity = new AccelaDocument
            {
                MasterId = masterId,
                LicenseNumber = licenseNumber,
                MasterCategoryDocId = masterCategoryId,
                FileName = fileName
            };
            return _submissionDocumentToAccelaService.InsertSubmissionDocumentsToAccela(submissionDocumentToAccelaEntity);
        }

        private void SubmissionGeneratedDocumentDetails(string masterId, string licenseNumber,
            string userId, byte[] fileByteCode,
            string fileName, string generatedDocumentType)
        {
            var businessEntityGenaration =
                                      new SubmissionGeneratedDocumentEntity
                                      {
                                          SubmissionGeneratedDocumentId = Guid.NewGuid().ToString(),
                                          MasterId = masterId,
                                          SubmissionLicenseNumber = licenseNumber,
                                          UserId = userId,
                                          FileByteCode = fileByteCode,
                                          FileType = fileName,
                                          CreatedDate = System.DateTime.Now,
                                          Gen_DocumentFrom = generatedDocumentType
                                      };

            _businessEntityGenarationPdfService.AddBusinessEntityGenarationPdf(
                businessEntityGenaration);
        }

        private string OrderPlaceing(string fullName, string url,
             string masterId, string paymentId, string sendMail,
            decimal grandTotal, decimal totalLicenseFee, decimal totalTechFee, decimal totalEndosementFee, decimal totalApplicationFee, string submitFrom)
        {
            var template = File.ReadAllText(HttpContext.Current.Server.MapPath("~/EmailTemplates/Receipts/OrderConfirmation.cshtml"));
            var getReceiptModel = new ReceiptModel { MasterID = masterId, PaymentID = paymentId }; ;
            var result = _bblAssociateService.GetReceiptData(getReceiptModel);
            var emailDtlCategoryList = result.ServiceCheckList.ToList().Select(y => new EmailDetailedCategoryList
            {
                ApplicationFee = y.ApplicationFee,
                CategoryCode = y.CategoryCode,
                CategoryId = y.CategoryId,
                CategoryLicenseFee = Convert.ToDecimal((y.CategoryLicenseFee).ToString("#,##0.00")),
                Endorsement = y.Endorsement,
                EndorsementFee = Convert.ToDecimal((y.EndorsementFee).ToString("#,##0.00")),
                IsRaoFeeApplied = y.IsRaoFeeApplied,
                LicenseCategory = y.LicenseCategory,
                LicenseDuration = y.LicenseDuration,
                SubTotal = Convert.ToDecimal((y.SubTotal).ToString("#,##0.00")),
                TechFee = Convert.ToDecimal((y.TechFee).ToString("#,##0.00")),
                TotalFee = Convert.ToDecimal((y.TotalFee).ToString("#,##0.00")),
                Units = y.Units,
                LapsedFee = Convert.ToDecimal((y.LapsedFee).ToString("#,##0.00")),
                ExpiryFee = Convert.ToDecimal((y.ExpiryFee).ToString("#,##0.00")),
            }).ToList();
            var documents = new List<EmailBblServiceDocuments>();
            foreach (var x in result.DocumentList.ToList())
            {
                var document = new EmailBblServiceDocuments();
                DocInt = DocInt + 1;
                document.Sno = DocInt;
                document.Agency = x.Agency;
                document.ApprovedBy = x.ApprovedBy;
                document.CategoryCode = x.CategoryCode;
                document.CategoryID = x.CategoryID;
                document.CheckListType = x.CheckListType;
                document.Description = x.Description;
                document.Div = x.Div;
                document.Division = x.Division;
                document.DocRequired = x.DocRequired;
                document.DocStatus = x.DocStatus;
                document.Endorsement = x.Endorsement;
                document.FileLocation = x.FileLocation;
                document.FileName = x.FileName;
                document.IsUpload = x.IsUpload;
                document.License = x.License;
                document.LicenseName = x.LicenseName;
                document.MasterId = x.MasterId;
                document.ShortName = x.ShortName;
                document.SubmissionCategoryID = x.SubmissionCategoryID;
                document.SubmissionId = x.SubmissionId;
                document.UploadFileName = x.UploadFileName;

                documents.Add(document);
            }

            string emailbodyList;

            var viewModel = new EmailReceiptModel
               {
                   ApplicationSubmit = submitFrom.ToUpper(),
                   FullName = result.FullUserName,
                   InnerHtml = "Your Transaction is Complete!",
                   EmailAddress = sendMail,
                   MasterID = masterId,
                   TransactionId = result.TransactionId,
                   AmountCharged = Convert.ToDecimal((result.AmountCharged).ToString("#,##0.00")),
                   GrandTotal = Convert.ToDecimal((result.TotalFee).ToString("#,##0.00")),
                   GrandTotals = (result.TotalFee).ToString("#,##0.00"),
                   TotalLicenseFee = Convert.ToDecimal((result.CategoryLicenseFee).ToString("#,##0.00")),
                   TotalTechFee = Convert.ToDecimal((result.TechFee).ToString("#,##0.00")),
                   TotalLicenseFees = (result.CategoryLicenseFee).ToString("#,##0.00"),
                   TotalTechFees = (result.TechFee).ToString("#,##0.00"),
                   TotalEndosementFee = Convert.ToDecimal((result.EndorsementFee).ToString("#,##0.00")),
                   TotalApplicationFee = Convert.ToDecimal((result.ApplicationFee).ToString("#,##0.00")),
                   SubNumber = result.SubNumber,
                   ReceiptDate = result.ReceiptDate,
                   CardNumber = result.CardNumber,
                   DocType = result.DocType,
                   ExceptedFinalCheckingDate = result.ExceptedFinalCheckingDate,
                   ServiceCheckList = emailDtlCategoryList,
                   Isehop = result.Isehop,
                   IsEhopAllowed = result.IsEhopAllowed,
                   DocumentList = documents,
                   SiteUrl = url,
                   ExtraAmount = result.ExtraAmount,
                   LicenseDuration = result.LicenseDuration,
                   ApplicationStatus = result.SubmissionApplicationStatus,
                   IsBackgroundInvestigation = result.IsBackgroundInvestigation
               };

#pragma warning disable 618
            try
            {
                emailbodyList = Razor.Parse(template, viewModel);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Order Placeing", ex);
            }

#pragma warning restore 618
            return emailbodyList;
        }

        //[Authorize]
        [HttpPost]
        [Route("GetReceiptDetails")]
        [DeflateCompression]
        public async Task<IHttpActionResult> GetReceiptDetails(ReceiptModel rModel)
        {
            var result = _bblAssociateService.GetReceiptData(rModel);
            var view = "PrintReciptView";
            if (result.SubNumber != "")
            {
                result.SubmitTypeFrom = (result.SubmitTypeFrom ?? "");
                result.DocType = (result.DocType ?? "");
                result.GrandTotals = (result.TotalFee).ToString("#,##0.00");
                string getSubmitedType = string.Empty;
                result.ApplicationStatus = (result.SubmissionApplicationStatus ?? "");
                string fileName;
                string destinationFileName;
                if (rModel.SubmitTypeFrom.ToUpper() == GenericEnums.GetEnumDescription(
                        GenericEnums.SubmissionGenerationDocumentType.Renew).ToUpper())
                {
                    getSubmitedType = GenericEnums.GetEnumDescription(
                        GenericEnums.SubmissionGenerationDocumentType.SubRenew).ToUpper();
                    destinationFileName = result.SubNumber + GenericEnums.GetEnumDescription(
                  GenericEnums.SubmissionGenerationDocumentType.RenewalReceipt) + ".pdf";
                    fileName = destinationFileName;
                }
                else
                {
                    getSubmitedType = GenericEnums.GetEnumDescription(
                        GenericEnums.SubmissionGenerationDocumentType.SubRec).ToUpper();
                    destinationFileName = result.SubNumber + GenericEnums.GetEnumDescription(
                  GenericEnums.SubmissionGenerationDocumentType.SubmitReceipt) + ".pdf";
                    fileName = destinationFileName;
                }

                byte[] submitionReceiptByteCode =
                    new PdfResult(view, result, fileName, destinationFileName).FileStorageSection();

                SubmissionGeneratedDocumentDetails(result.MasterID,
                    result.SubNumber,
                    rModel.UserId, submitionReceiptByteCode, destinationFileName.ToString(),
                   getSubmitedType);
            }
            if (result == null)
            {
                return await Task.FromResult(Json("No Data"));
            }
            return await Task.FromResult(Json(result));
        }

        [Authorize]
        [HttpPost]
        [Route("RemoveCofo")]
        [DeflateCompression]
        public async Task<IHttpActionResult> RemoveCofo(CofoHopDetailsModel cofoModel)
        {
            var result = _bblAssociateService.DeleteCofo(cofoModel);

            //if (result == null)
            //{
            //    return await Task.FromResult(Json("No Data"));
            //}
            return await Task.FromResult(Json(result));
        }

        [Authorize]
        [HttpPost]
        [Route("SubmissionStatus")]
        public async Task<IHttpActionResult> SubmissionStatus(GeneralBusiness generalbusiness)
        {
            var result = _submissionMaster.MasterStatus(generalbusiness.MasterId);

            PaymentDetails paymentdetails = new PaymentDetails { MasterId = generalbusiness.MasterId };
            var paymentstatus = _bblAssociateService.FindByPaymentID(paymentdetails).ToList();
            if (paymentstatus.Count() != 0)
            {
                var paymentDetailStatus = paymentstatus.FirstOrDefault();
                if (paymentDetailStatus != null)
                {
                    result.PaymentId = paymentDetailStatus.PaymentId ?? "";
                    result.PaymentStatus = (paymentDetailStatus.PaymentStatus ?? "").Trim().ToUpper();
                }
            }
            // result.BusinessOwnerName = _bblAssociateService.BusinessOwnerName(generalbusiness.MasterId);
            result.CorporationStatus = (_bblAssociateService.CorpServiceStatus(generalbusiness.MasterId).OriginalCorpStatus ?? "").Trim().ToUpper();
            result.CurrentYear = (DateTime.Now.Year.ToString());
            result.CreatedDate = (DateTime.Now.ToString("MM/dd/yyyy"));
            if (result == null)
            {
                return await Task.FromResult(Json("No Data"));
            }
            return await Task.FromResult(Json(result));
        }

        [Authorize]
        [HttpPost]
        [Route("GetMasterBasedonUserAssociateId")]
        public async Task<IHttpActionResult> GetRenewMasterUserAssociateId(RenewModel renewModel)
        {
            var result = _submissionMaster.GetRenewMasterUserAssociateID(renewModel);
            return await Task.FromResult(Json(new { Result = result }));
        }

        // [Authorize]
        [HttpGet]
        [Authorize]
        [Route("MailAlarm")]
        public async Task<string> DailyMailAlarmToBBlLicenseUserPriorToExpired()
        {
            var userMailListing = (from userbbllist in _bblAssociateService.MailAlarmAssociateUsers()
                                   join bbldata in _bblAssociateService.DailyMailAlarmToBBlLicenseUserPriorToExpired()
                                on userbbllist.B1_ALT_ID.ToString().Trim() equals bbldata.B1_ALT_ID.ToString().Trim()
                                   join userslist in _bblAssociateService.GetUserDetails() on userbbllist.UserID.Trim() equals userslist.Id
                                   select new
                                   {
                                       userslist.Email,
                                       userbbllist.UserID,
                                       userslist.FirstName,
                                       userslist.LastName,
                                       userbbllist.SubmissionLicense,
                                       userbbllist.DCBC_ENTITY_ID,
                                       bbldata.OwnrApplicant_BUSINESS_NAME,
                                       userbbllist.ServiceID,
                                       bbldata.Expiration_Date,
                                       bbldata.B1_ALT_ID,
                                       userbbllist.LicenseExpirationDate
                                   }).ToList();

            foreach (var items in userMailListing)
            {
                bool checkdata = true;
                var submissionmaster = _submissionMaster.SubmissionDetailsBasedonLicenseandUserId(items.SubmissionLicense, items.UserID).ToList();
                if (submissionmaster.Any())
                {
                    var masterdta = submissionmaster.FirstOrDefault();
                    if (masterdta != null && (masterdta.Status.ToUpper().ToString() == "UNDERREVIEW" || masterdta.Status.ToUpper().ToString() == "ACTIVE"))
                    {
                        checkdata = false;
                    }
                }
                if (checkdata)
                {
                    var mailAlert = new DailyMailAlertModel
                    {
                        UserFullName = items.FirstName + ' ' + items.LastName,
                        SubmissionLicenseNumber = items.SubmissionLicense,
                        UserId = items.UserID,
                        Expiration_Date = items.Expiration_Date
                    };
                    var mailstatuscheck = new MailTemplateModel
                    {
                        UserId = mailAlert.UserId,
                        MailSentDate = System.DateTime.Now,
                        CustomApplicationId = mailAlert.SubmissionLicenseNumber,
                        Type = "DailyMailAlert",
                        Subject = "MailAlert",
                        IsMailSent = true
                    };
                    bool mailstatus = false;
                    var status = _mailTemplateService.FindByMailStatusCheck(mailstatuscheck).ToList();

                    if (status.Any())
                    {
                        var statuscheck = status.FirstOrDefault();
                        if (statuscheck != null) mailstatus = statuscheck.IsMailSent ?? false;
                    }
                    if (!mailstatus)
                    {
                        var boday = MailSedingToAssociateLicenseUsers(mailAlert.SubmissionLicenseNumber,
                            mailAlert.UserFullName, items.Expiration_Date, items.OwnrApplicant_BUSINESS_NAME);
                        var templateModel = new MailTemplateModel
                        {
                            UserId = mailAlert.UserId,
                            MailContent = boday,
                            MailSentDate = System.DateTime.Now,
                            CustomApplicationId = mailAlert.SubmissionLicenseNumber,
                            Type = "RENEWAL",
                            Subject = "Your license is due for renewal",
                            IsMailSent = true
                        };

                        _mailTemplateService.InsertUpdateMailTemplate(templateModel);
                        MailDetails.MailSending(" Your license is due for renewal", boday, items.Email);
                    }
                }
            }

            return await Task.FromResult("OK");
        }

        private string MailSedingToAssociateLicenseUsers(string licenseNumber, string userFullName, string expireDate, string businessName)
        {
            var template = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/EmailTemplates/DailyMailService.cshtml"));
            var viewModel = new DailyMailAlertModel
            {
                UserFullName = userFullName,
                SubmissionLicenseNumber = licenseNumber,
                Expiration_Date = expireDate,
                BusinessOwnerName = businessName
            };

            string body1;
#pragma warning disable 618
            body1 = Razor.Parse(template, viewModel);
#pragma warning restore 618

            return body1;
        }
    }
}