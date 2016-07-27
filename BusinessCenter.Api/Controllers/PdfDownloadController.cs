using BusinessCenter.Api.Common;
using BusinessCenter.Api.Filters;
using BusinessCenter.Api.Models;
using BusinessCenter.Api.Utility;
using BusinessCenter.Data;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Model;
using BusinessCenter.Data.Models;
using BusinessCenter.Service.Common;
using BusinessCenter.Service.Interface;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BusinessCenter.Api.Controllers
{
    [RoutePrefix("api/Download")]
    public class PdfDownloadController : ApiController
    {
        private readonly IBBLAssociateService _bblAssociateService;
        private readonly ISubmissionIndividualService _subIndividualService;
        private readonly ISubmissionGeneratedDocumentService _businessEntityGenarationPdfService;
        private readonly ISubmissionMasterService _submissionMasterService;
        private readonly ISearchService _searchService;
        private readonly IMyServiceDetails _myServiceDetails;

        //
        public PdfDownloadController(IBBLAssociateService bblAssociateService,
           ISubmissionIndividualService subIndividualService,
            ISubmissionGeneratedDocumentService businessEntityGenarationPdfService,
            ISubmissionMasterService submissionMasterService, ISearchService searchService,
            IMyServiceDetails myServiceDetails)
        {
            _bblAssociateService = bblAssociateService;
            _subIndividualService = subIndividualService;
            _businessEntityGenarationPdfService = businessEntityGenarationPdfService;
            _submissionMasterService = submissionMasterService;
            _searchService = searchService;
            _myServiceDetails = myServiceDetails;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="masterid"></param>
        /// <returns></returns>
        //[HttpGet]
        //[Route("applicationChecklist_GeneratedDocument")]
        //public PdfResult SubmissionInformationDetails(string masterid, string token)
        //{
        //    var client = new HttpClient()
        //    {
        //        BaseAddress = new Uri(AppDomain.CurrentDomain.BaseDirectory)
        //    };

        //    client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", token));
        //    var response = client.GetAsync("api/Download/applicationChecklist_GeneratedDocument1?masterid=" + masterid).Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var dto = response.Content.ReadAsStringAsync().Result;
        //    }
        //    return null;

        //    //var view = "ApplicationCheckListView";
        //    //var fileName = "Application_CheckList.pdf";
        //    //var bblDocuments = new BblDocuments { MasterId = masterid };
        //    //var commandata = _bblAssociateService.DocumentList(bblDocuments).ToList();
        //    //var validateResult = _subIndividualService.ValidateSubmission(masterid);
        //    //foreach (var item in commandata)
        //    //{
        //    //    item.IsIndividual = validateResult;

        //    //    item.physicalLocationValidate = PhysicalLocationValidation(item.IsSubmissionCofo, item.IsSubmissionHop,
        //    //        item.IsBPAddress,
        //    //        item.IsCorporateRegistration, item.TradeName, item.PremisesAddress, item.BusinessName);
        //    //}

        //    //return new PdfResult(view, commandata, fileName, null);
        //}
        [PdfAuthenticationFilter]
        [HttpGet]
        [Route("applicationChecklist_GeneratedDocument")]
        public PdfResult SubmissionInformationDetails(string reft, string masterid)
        {
            var view = "ApplicationCheckListView";
            var fileName = "Application_CheckList.pdf";
            var bblDocuments = new BblDocuments { MasterId = masterid };
            var commandata = _bblAssociateService.DocumentList(bblDocuments).ToList();
            var validateResult = _subIndividualService.ValidateSubmission(masterid);
            foreach (var item in commandata)
            {
                item.IsIndividual = validateResult;

                item.physicalLocationValidate = PhysicalLocationValidation(item.IsSubmissionCofo, item.IsSubmissionHop,
                    item.IsBPAddress,
                    item.IsCorporateRegistration, item.TradeName, item.PremisesAddress, item.BusinessName);
            }

            return new PdfResult(view, commandata, fileName, null);
        }

        //[HttpPost]
        //[Route("applicationChecklist_GeneratedDocument_Angular")]
        //public byte[] SubmissionInformationDetails_Angular(SubmissionEntityModel1 masterid)
        //{
        //    var view = "ApplicationCheckListView";
        //    var fileName = "Application_CheckList.pdf";
        //    var bblDocuments = new BblDocuments { MasterId = "e29ecebe-fedc-4e77-9fc3-fc9e0491b19d" };
        //    var commandata = _bblAssociateService.DocumentList(bblDocuments).ToList();
        //    var validateResult = _subIndividualService.ValidateSubmission("e29ecebe-fedc-4e77-9fc3-fc9e0491b19d");
        //    foreach (var item in commandata)
        //    {
        //        item.IsIndividual = validateResult;

        //        item.physicalLocationValidate = PhysicalLocationValidation(item.IsSubmissionCofo, item.IsSubmissionHop,
        //            item.IsBPAddress,
        //            item.IsCorporateRegistration, item.TradeName, item.PremisesAddress, item.BusinessName);
        //    }
        //    var myBytes = new PdfResult(view, commandata, fileName, "BHARATH").FileStorageSection();
        //    return myBytes;
        //}

        //[PdfAuthenticationFilter]
        //[HttpGet]
        //[Route("applicationChecklist_GeneratedDocument")]
        //public Task<HttpResponseMessage> SubmissionInformationDetails(string masterid, string token, string refreshtoken)
        //{
        //    var client = new HttpClient();
        //    client.DefaultRequestHeaders.Add("refreshTokenId", refreshtoken);
        //    client.DefaultRequestHeaders.Add("Authorization", token);
        //    var getbyteCode = client.GetAsync("http://localhost:40001/api/Download/GetApplicationChecklistPDF?masterid=" + masterid).Result;

        //    var result = getbyteCode.Content.ReadAsByteArrayAsync();
        //    var response = new HttpResponseMessage(HttpStatusCode.OK)
        //    {
        //        Content = new ByteArrayContent(result.Result)
        //    };

        //    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        //    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        //    {
        //        FileName = "Application_CheckList.pdf"
        //    };

        //    return Task.FromResult(response);
        //    // return Task.FromResult<HttpResponseMessage>(result).Result;
        //}

        [Authorize]
        [HttpGet]
        [Route("GetApplicationChecklistPDF")]
        public PdfResult GetApplicationChecklistPDF(string masterid)
        {
            var view = "ApplicationCheckListView";
            var fileName = "Application_CheckList.pdf";
            var bblDocuments = new BblDocuments { MasterId = masterid };
            var commandata = _bblAssociateService.DocumentList(bblDocuments).ToList();
            var validateResult = _subIndividualService.ValidateSubmission(masterid);
            foreach (var item in commandata)
            {
                item.IsIndividual = validateResult;

                item.physicalLocationValidate = PhysicalLocationValidation(item.IsSubmissionCofo, item.IsSubmissionHop,
                    item.IsBPAddress,
                    item.IsCorporateRegistration, item.TradeName, item.PremisesAddress, item.BusinessName);
            }
            return new PdfResult(view, commandata, fileName, null);
        }

        public bool PhysicalLocationValidation(bool isSubmissionCofo, bool isSubmissionHop, bool isBpAddress,
            bool isCorporateRegistration, string tradeName, string premisesAddress, string businessName)
        {
            if (businessName != null && ((isSubmissionHop ||
                                          isBpAddress ||
                                          isSubmissionCofo) && isCorporateRegistration && (tradeName == "NA" || tradeName != "") && (businessName != "" || businessName != null)))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="masterid"></param>
        /// <returns></returns>
        [PdfAuthenticationFilter]
        [HttpGet]
        [Route("InformationVerification_GeneratedDocument")]
        public PdfResult InformationVerificationPdfDownload(string masterid, string reft)
        {
            var view = "InformationVerificationdetailsView";
            var fileName = "InformationVerificationdetails.pdf";
            var subVerfication = new SubmissionVerfication { MasterID = masterid };
            var verficationdetails = _bblAssociateService.SubmissionDetails(subVerfication);
            verficationdetails.GrandTotals = verficationdetails.GrandTotal.ToString("#,##0.00");
            return new PdfResult(view, verficationdetails, fileName, null);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
        //[HttpGet]
        //[Route("SubmissionReceipt_GeneratedDocument")]
        //public PdfResult ViewReceipt(string masterId)
        //{
        //    var view = "PrintReciptView";
        //    var fileName = "ReciptView.pdf";

        //    var paymentDetails = new PaymentDetails { MasterId = masterId };
        //    paymentDetails = _bblAssociateService.FindByPaymentID(paymentDetails).FirstOrDefault();
        //    var receiptModel = new ReceiptModel();
        //    if (paymentDetails != null)
        //    {
        //        receiptModel.MasterID = masterId;
        //        receiptModel.PaymentID = paymentDetails.PaymentId;
        //        receiptModel.EmailAddress = paymentDetails.PaymentMailAddress;
        //        receiptModel = _bblAssociateService.GetReceiptData(receiptModel);
        //    }
        //    return new PdfResult(view, receiptModel, fileName, null);
        //}
        [PdfAuthenticationFilter]
        [HttpGet]
        [Route("SubmissionReceipt_GeneratedDocument")]
        public Task<HttpResponseMessage> ViewReceipt(string masterId, string userId, string type, string reft)
        {
            string getSubmitedType;
            if (type.ToUpper().Trim() == GenericEnums.GetEnumDescription(
                GenericEnums.SubmissionGenerationDocumentType.Renew).ToUpper().Trim())
            {
                getSubmitedType = GenericEnums.GetEnumDescription(
                    GenericEnums.SubmissionGenerationDocumentType.SubRenew).ToUpper();
            }
            else
            {
                getSubmitedType = GenericEnums.GetEnumDescription(
                      GenericEnums.SubmissionGenerationDocumentType.SubRec).ToUpper();
            }
            var getFileByteCode = _businessEntityGenarationPdfService.BusinessEntityGenarationPdf(masterId,
                userId, getSubmitedType);
            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new ByteArrayContent(getFileByteCode.FileBytes.ToArray()) };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = getFileByteCode.Filename
            };
            return Task.FromResult(response);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="masterId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [PdfAuthenticationFilter]
        [HttpGet]
        [Route("SubmissionsActive_GeneratedDocument")]
        public Task<HttpResponseMessage> BblViewReceipt(string masterId, string userId, string reft)
        {
            bool genaratePdfAfterActiveInBBL = true;
            var chcekSubMasterDetails = _submissionMasterService.FindByMasterID(masterId);
            var subMasterDetails = chcekSubMasterDetails as IList<SubmissionMaster> ?? chcekSubMasterDetails.ToList();
            if (chcekSubMasterDetails != null && subMasterDetails.Any())
            {
                var getNewSibmission = subMasterDetails.FirstOrDefault();
                if (getNewSibmission != null)
                {
                    var bbldetails = _bblAssociateService.ValidateBblEntityWithLicense(getNewSibmission.SubmissionLicense.Trim()).ToList();
                    if (bbldetails.Count > 0)
                    {
                        var getbblDetails = bbldetails.FirstOrDefault();
                        if (getbblDetails != null && getbblDetails.B1_APPL_STATUS.ToUpper().Trim() ==
                            getNewSibmission.Status.ToUpper().Trim())
                        {
                            genaratePdfAfterActiveInBBL = true;
                        }
                    }
                    else
                    {
                        if (_businessEntityGenarationPdfService.FindBblOrderDocuments(masterId,
                            GenericEnums.GetEnumDescription(GenericEnums.SubmissionGenerationDocumentType.Nodocs)))
                        {
                            genaratePdfAfterActiveInBBL = false;
                        }
                    }
                }
            }

            if (genaratePdfAfterActiveInBBL == false)
            {
                var getFileByteCode = _businessEntityGenarationPdfService.BusinessEntityGenarationPdf(masterId,
                    userId,
                    GenericEnums.GetEnumDescription(GenericEnums.SubmissionGenerationDocumentType.Nodocs).ToUpper());

                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(getFileByteCode.FileBytes.ToArray())
                };
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = getFileByteCode.Filename
                };
                return Task.FromResult(response);
            }
            else
            {
                if (subMasterDetails.Any())
                {
                    string licenseNumber = String.Empty;
                    // DateTime paymentDate = Convert.ToDateTime(chcekSubMasterDetails.FirstOrDefault().Updatedate);
                    DateTime paymentDate;
                    StringBuilder submissionOrderFileName = new StringBuilder();
                    submissionOrderFileName.Append(subMasterDetails.FirstOrDefault().SubmissionLicense);
                    var bblrespositorydata =
                        _submissionMasterService.BblDetails(subMasterDetails.FirstOrDefault().SubmissionLicense)
                            .ToList();
                    paymentDate = bblrespositorydata.Any() ? Convert.ToDateTime(!string.IsNullOrEmpty(bblrespositorydata.FirstOrDefault().B1_APPL_STATUS_DATE.ToString()) ? Convert.ToDateTime(bblrespositorydata.FirstOrDefault().B1_APPL_STATUS_DATE) : subMasterDetails.FirstOrDefault().Updatedate) : Convert.ToDateTime(subMasterDetails.FirstOrDefault().Updatedate);

                    submissionOrderFileName.Append(".pdf");

                    var getOrderByteCode = ViewReceipt(masterId, submissionOrderFileName.ToString(), paymentDate);

                    var response = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new ByteArrayContent(getOrderByteCode.ToArray())
                    };
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = submissionOrderFileName.ToString()
                    };
                    return Task.FromResult(response);
                }
                else
                {
                    return null;
                }
            }
        }

        //public Task<HttpResponseMessage> BblViewReceiptMyView(string masterId, string userId)
        //{
        //    //var response = new HttpResponseMessage(HttpStatusCode.OK)
        //    //{
        //    //    Content = new ByteArrayContent(getOrderByteCode.ToArray())
        //    //};
        //    //response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        //    //response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        //    //{
        //    //    FileName = submissionOrderFileName.ToString()
        //    //};
        //    //return Task.FromResult(response);
        //}

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

        public byte[] ViewRenewalReceipt(string masterId, string fileName, DateTime paymentDateTime)
        {
            var view = "RenewalOrderPdf";
            var renewBasicBusinessLicense = new RenewBasicBusinessLicense { MasterId = masterId };
            var bblreceipt = _bblAssociateService.RenewBusinessReceipt(renewBasicBusinessLicense);
            bblreceipt.ReceiptLogo = System.Configuration.ConfigurationManager.AppSettings["siteAddress"] + "/images/dcgov_logo.png";

            bblreceipt.Signature = System.Configuration.ConfigurationManager.AppSettings["siteAddress"] + "/images/ehopsignature.png";
            return new PdfResult(view, bblreceipt, fileName, fileName).FileStorageSection();
        }

        public byte[] ViewReceipt(string masterId, string fileName, DateTime paymentDate)
        {
            var view = "OrderDetailsView";
            BasicBusinessLicense basicBusinessLicense = new BasicBusinessLicense { MasterId = masterId };
            var bblreceipt = _bblAssociateService.BusinessReceipt(basicBusinessLicense);
            bblreceipt.ReceiptLogo = System.Configuration.ConfigurationManager.AppSettings["siteAddress"] + "/images/dcgov_logo.png";
            //if (bblreceipt != null)
            //{
            //    bblreceipt.DateIssued = Convert.ToDateTime(paymentDate).ToString("MM/dd/yyyy");
            //}

            bblreceipt.Signature = System.Configuration.ConfigurationManager.AppSettings["siteAddress"] + "/images/ehopsignature.png";
            return new PdfResult(view, bblreceipt, fileName, fileName).FileStorageSection();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
        [PdfAuthenticationFilter]
        [HttpGet]
        [Route("BblRenewelOrder")]
        public Task<HttpResponseMessage> BblViewRenewelReceipt(string masterId, string userId, string reft)
        {
            bool genaratePdfAfterActiveInBBL = true;
            var chcekSubMasterDetails = _submissionMasterService.FindByMasterID(masterId);
            var subMasterDetails = chcekSubMasterDetails as IList<SubmissionMaster> ?? chcekSubMasterDetails.ToList();
            if (chcekSubMasterDetails != null && subMasterDetails.Any())
            {
                var getNewSibmission = subMasterDetails.FirstOrDefault();
                if (getNewSibmission != null)
                {
                    string getB1_Alt_Id = string.Empty;
                    var getUserBblDetails =
                        _bblAssociateService.GetUserBBLData(Convert.ToInt32(getNewSibmission.UserBblAssociateId));
                    if (getUserBblDetails.Any())
                    {
                        getB1_Alt_Id = getUserBblDetails.FirstOrDefault().B1_ALT_ID.Trim();
                    }
                    var bbldetails = _bblAssociateService.ValidateBblEntityWithLicense(getB1_Alt_Id).ToList();
                    if (bbldetails.Count > 0)
                    {
                        var getbblDetails = bbldetails.FirstOrDefault();
                        if (getbblDetails != null && getbblDetails.B1_APPL_STATUS.ToUpper().Trim() ==
                            getNewSibmission.Status.ToUpper().Trim())
                        {
                            genaratePdfAfterActiveInBBL = true;
                        }
                    }
                    else
                    {
                        if (_businessEntityGenarationPdfService.FindBblOrderDocuments(masterId,
                GenericEnums.GetEnumDescription(GenericEnums.SubmissionGenerationDocumentType.RenewalNodocs)))
                        {
                            genaratePdfAfterActiveInBBL = false;
                        }
                    }
                }
            }

            if (genaratePdfAfterActiveInBBL == false)
            {
                var getFileByteCode = _businessEntityGenarationPdfService.BusinessEntityGenarationPdf(masterId,
                    userId,
                    GenericEnums.GetEnumDescription(GenericEnums.SubmissionGenerationDocumentType.RenewalNodocs)
                        .ToUpper());
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(getFileByteCode.FileBytes.ToArray())
                };
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = getFileByteCode.Filename
                };
                return Task.FromResult(response);
            }
            else
            {
                if (subMasterDetails.Any())
                {
                    StringBuilder renewalSubmissionOrderFileName = new StringBuilder();

                    int UserBblAssociateId = 0;
                    //_bblAssociateService
                    if (!string.IsNullOrEmpty(subMasterDetails.FirstOrDefault().UserBblAssociateId))
                    {
                        UserBblAssociateId = Convert.ToInt32(subMasterDetails.FirstOrDefault().UserBblAssociateId);
                    }

                    var getUserUserBbl = _bblAssociateService.UserbblServiceFindById(UserBblAssociateId);

                    if (getUserUserBbl != null && getUserUserBbl.Any())
                    {
                        renewalSubmissionOrderFileName.Append(getUserUserBbl.FirstOrDefault().B1_ALT_ID);
                    }
                    else
                    {
                        renewalSubmissionOrderFileName.Append(subMasterDetails.FirstOrDefault().SubmissionLicense);
                    }

                    DateTime paymentDate = Convert.ToDateTime(subMasterDetails.FirstOrDefault().Updatedate);
                    if (GetPresentMonth())
                    {
                        renewalSubmissionOrderFileName.Append(GenericEnums.GetEnumDescription(
                            GenericEnums.SubmissionGenerationDocumentType.RenewDocFileExtension));
                        renewalSubmissionOrderFileName.Append(
                            (DateTime.Now.Year + 1).ToString().Substring(2, 2));
                    }
                    else
                    {
                        renewalSubmissionOrderFileName.Append(GenericEnums.GetEnumDescription(
                             GenericEnums.SubmissionGenerationDocumentType.RenewDocFileExtension));
                        renewalSubmissionOrderFileName.Append((DateTime.Now.Year).ToString().Substring(2, 2));
                    }

                    renewalSubmissionOrderFileName.Append(".pdf");

                    var renewalByteCodedata = ViewRenewalReceipt(masterId, renewalSubmissionOrderFileName.ToString(),
                        paymentDate);

                    var response = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new ByteArrayContent(renewalByteCodedata.ToArray())
                    };
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = renewalSubmissionOrderFileName.ToString()
                    };
                    return Task.FromResult(response);
                }
                else
                {
                    StringBuilder renewalSubmissionOrderFileName = new StringBuilder();
                    var getBblDetails = _bblAssociateService.GetBblDataOnEntityId(Convert.ToInt32(masterId));
                    string statusDetails = string.Empty;
                    bool isSubmissionLicense = false;
                    if (getBblDetails.Any())
                    {
                        renewalSubmissionOrderFileName.Append(getBblDetails.FirstOrDefault().B1_ALT_ID);
                        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                        string expiryStatus =
                            textInfo.ToTitleCase(
                                ServiceSubmissionExpiryStatus(Convert.ToDateTime(getBblDetails.FirstOrDefault().Expiration_Date)))
                                .Trim();
                        if ((expiryStatus.Trim().ToUpper() == "EXPIRING SOON"))
                        {
                            isSubmissionLicense = true;
                        }
                    }
                    if (isSubmissionLicense == false)
                    {
                        if (GetPresentMonth())
                        {
                            renewalSubmissionOrderFileName.Append(GenericEnums.GetEnumDescription(
                                GenericEnums.SubmissionGenerationDocumentType.RenewDocFileExtension));
                            renewalSubmissionOrderFileName.Append(
                                (DateTime.Now.Year + 1).ToString().Substring(2, 2));
                        }
                        else
                        {
                            renewalSubmissionOrderFileName.Append(GenericEnums.GetEnumDescription(
                                GenericEnums.SubmissionGenerationDocumentType.RenewDocFileExtension));
                            renewalSubmissionOrderFileName.Append((DateTime.Now.Year).ToString().Substring(2, 2));
                        }
                    }

                    DateTime getDate = Convert.ToDateTime(getBblDetails.FirstOrDefault().B1_APPL_STATUS_DATE);
                    var renewalByteCodedata = ViewRenewalReceipt(masterId, renewalSubmissionOrderFileName.ToString(), getDate);

                    var response = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new ByteArrayContent(renewalByteCodedata.ToArray())
                    };
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = renewalSubmissionOrderFileName + ".pdf"
                    };
                    return Task.FromResult(response);
                    //return null;
                }
            }
        }

        public string ServiceSubmissionExpiryStatus(DateTime dateofExpiry)
        {
            try
            {
                DateTime expiryDate = dateofExpiry;
                string time = DateTime.Now.ToString("HH:mm:ss");
                string hour = time.Substring(0, 2);
                int hourInt = int.Parse(hour);
                DateTime checkTime = DateTime.Now;
                if (hourInt == 0 && checkTime.Minute < 31)
                {
                    checkTime = checkTime.AddDays(-1);
                }
                double dayperiod = Math.Ceiling((expiryDate - checkTime).TotalDays);

                if (dayperiod >= -30 && dayperiod <= -1)
                {
                    //Lapsed = Lapsed + 1;
                    return GenericEnums.ApplicationValidateStatus.Lapsed.ToString().Trim();
                }
                else if (dayperiod > 90)
                {
                    //  Active = Active + 1;
                    return GenericEnums.GetEnumDescription(GenericEnums.ApplicationStatus.Active).Trim();
                }
                else if (dayperiod >= 0 && dayperiod <= 90)
                {
                    // _expirySoon = _expirySoon + 1;
                    return GenericEnums.GetEnumDescription(GenericEnums.ApplicationStatus.ExpiringSoon).Trim();
                }
                else if (dayperiod < -30 && dayperiod >= -180)
                {
                    // Expired = Expired + 1;
                    return GenericEnums.ApplicationValidateStatus.Expired.ToString().Trim();
                }
                else
                {
                    return GenericEnums.GetEnumDescription(GenericEnums.ApplicationStatus.Renewalnotallowed).Trim();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="masterid"></param>
        /// <param name="userId"></param>
        /// <param name="submitedInput"></param>
        /// <returns></returns>
        [PdfAuthenticationFilter]
        [HttpGet]
        [Route("EHOP_GeneratedDocument")]
        public Task<HttpResponseMessage> DownloadEhopDetails(string masterid, string userId, string reft)
        {
            var getFileByteCode = _businessEntityGenarationPdfService.BusinessEntityGenarationPdf(masterid,
                userId, GenericEnums.GetEnumDescription(GenericEnums.SubmissionGenerationDocumentType.eHOP).ToUpper());
            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new ByteArrayContent(getFileByteCode.FileBytes.ToArray()) };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = getFileByteCode.Filename
            };
            return Task.FromResult(response);
        }

        [PdfAuthenticationFilter]
        [HttpGet]
        [Route("QuickSearch_GeneratedDocument")]
        public Task<HttpResponseMessage> GenarateQuickSearchPdf([FromUri]SearchInput searchInput)
        {
            try
            {
                Random rnd = new Random();
                string fileName = "Quick_Search_Results_" + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + rnd.Next(00000, 99999) + ".pdf";

                SearchInput inputs = new SearchInput();
                inputs.InjectFrom(searchInput);

                searchInput.CompanyName = searchInput.CompanyName == null ? "" : searchInput.CompanyName.ToUpper().Replace(" AND ", " ").Replace(" & ", " ");
                searchInput.LicenseName = searchInput.LicenseName == null ? "" : searchInput.LicenseName.ToUpper().Replace(" AND ", " ").Replace(" & ", " ");
                searchInput.FirstName = searchInput.FirstName == null ? "" : searchInput.FirstName.ToUpper().Replace(" AND ", " ").Replace(" & ", " ");
                searchInput.LastName = searchInput.LastName == null ? "" : searchInput.LastName.ToUpper().Replace(" AND ", " ").Replace(" & ", " ");
                var searchServiceInput = new SearchServiceInput();
                searchServiceInput.InjectFrom(searchInput);

                var pdfResult = _searchService.GetSearchData(searchServiceInput).Result.ToList();
                var getFileByteCode = QuickSearchPdfGenatedBytes(pdfResult, fileName, inputs);

                var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new ByteArrayContent(getFileByteCode.ToArray()) };
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName
                };
                return Task.FromResult(response);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        [Route("QuickSearch_GeneratedDocumentNA")]
        public Task<HttpResponseMessage> GenarateQuickSearchPdfNotAuthenticated([FromUri]SearchInput searchInput)
        {
            try
            {
                Random rnd = new Random();
                string fileName = "Quick_Search_Results_" + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + rnd.Next(00000, 99999) + ".pdf";

                SearchInput inputs = new SearchInput();
                inputs.InjectFrom(searchInput);

                searchInput.CompanyName = searchInput.CompanyName == null ? "" : searchInput.CompanyName.ToUpper().Replace(" AND ", " ").Replace(" & ", " ");
                searchInput.LicenseName = searchInput.LicenseName == null ? "" : searchInput.LicenseName.ToUpper().Replace(" AND ", " ").Replace(" & ", " ");
                searchInput.FirstName = searchInput.FirstName == null ? "" : searchInput.FirstName.ToUpper().Replace(" AND ", " ").Replace(" & ", " ");
                searchInput.LastName = searchInput.LastName == null ? "" : searchInput.LastName.ToUpper().Replace(" AND ", " ").Replace(" & ", " ");
                var searchServiceInput = new SearchServiceInput();
                searchServiceInput.InjectFrom(searchInput);

                var pdfResult = _searchService.GetSearchData(searchServiceInput).Result.ToList();
                var getFileByteCode = QuickSearchPdfGenatedBytes(pdfResult, fileName, inputs);

                var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new ByteArrayContent(getFileByteCode.ToArray()) };
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName
                };
                return Task.FromResult(response);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public byte[] QuickSearchPdfGenatedBytes(List<SearchData> quickSearchData, string fileName, SearchInput search)
        {
            try
            {
                var view = "QuickSearchDetailsView";

                SearchDataMvcViewModel model = new SearchDataMvcViewModel();
                //List<CommonDataModel> records = new List<CommonDataModel>();

                //records = quickSearchData.Select(i => new CommonDataModel().InjectFrom(i)).Cast<CommonDataModel>().ToList();
                //CommonDataModel cdm = new CommonDataModel();

                //foreach (var item in quickSearchData)
                //{
                //    foreach (var i in item.SearchFinalData)
                //    {
                //        cdm.InjectFrom(i);
                //        records.Add(cdm);
                //    }

                //}
                //var countRow = quickSearchData.FirstOrDefault();
                //var rowCount = countRow.SearchCount.FirstOrDefault();

                //var companyNames = countRow.SearchFinalData.FirstOrDefault();
                //companyNames.CompanyName =

                //model.ID = rowCount.ID.ToString();
                //model.RecordCount = rowCount.RecordCount;
                //model.NoofRecords = rowCount.NoofRecords;
                //model.ABRAID = rowCount.ABRAID;
                //model.Source = rowCount.Source;
                //model.Name = rowCount.Name;
                //model.ABRACount = rowCount.ABRACount;
                //model.BBLCount = rowCount.BBLCount;
                //model.CBECount = rowCount.CBECount;
                //model.CORPCount = rowCount.CORPCount;
                //model.OPLACount = rowCount.OPLACount;
                //records.RemoveAt(0);
                var searchData = quickSearchData.OrderBy(x => x.SearchFinalData).ToList();
                var data = searchData.ElementAt(0).SearchFinalData.ToList();
                model.SearchResult = data;
                model.LicenseCounts = quickSearchData.First().SearchCount.First();
                model.SearchData = search;

                Dictionary<string, string> names = new Dictionary<string, string>();
                names.Add("BBL", "Business License");
                names.Add("CORP", "Corporate Registration");
                names.Add("OPLA", "Professional License");
                names.Add("CBE", "Certified Business Enterprise");
                names.Add("ABRA", "Alcoholic Beverage License");
                names.Add("All", "All");
                if (search.DisplayType != null)
                {
                    var items = search.DisplayType.Split('-');
                    string[] search_types = new string[items.Length - 1];
                    for (int i = 0; i < items.Length - 1; i++)
                    {
                        search_types[i] = names[items[i]];
                    }
                    model.SearchCritiria = search_types;
                }

                #region Mvc code

                string keyword = search.FilterKeyword;
                if (string.IsNullOrEmpty(keyword))
                {
                    search.FilterKeyword = "All";
                    keyword = "All";
                }
                switch (keyword)
                {
                    case "All":
                        if (model.SearchResult.Count == 0)
                        {
                            model.SearchStatus = "No Results Found";
                        }
                        break;

                    case "Business License":
                        model.SearchResult = model.SearchResult.Where(j => j.Source == "BBL").ToList();
                        break;

                    case "Professional License":
                        model.SearchResult = model.SearchResult.Where(j => j.Source == "OPLA").ToList();
                        break;

                    case "Alcoholic Beverage License":
                        model.SearchResult = model.SearchResult.Where(j => j.Source == "ABRA").ToList();
                        break;

                    case "Certified Business Enterprise":
                        model.SearchResult = model.SearchResult.Where(j => j.Source == "CBE").ToList();
                        break;

                    case "Corporate Registration":
                        model.SearchResult = model.SearchResult.Where(j => j.Source == "CORP").ToList();
                        break;

                    default:
                        List<CommonData> recs = new List<CommonData>();
                        string key = keyword.ToUpper();

                        foreach (var item in model.SearchResult)
                        {
                            StringBuilder builder = new StringBuilder();
                            builder.Append(item.LeftNameResultTop == null ? "" : item.LeftNameResultTop.ToString());
                            builder.Append("δ");
                            builder.Append(item.LeftNameResultMiddle == null ? "" : item.LeftNameResultMiddle.ToString());
                            builder.Append("δ");
                            builder.Append(item.LeftNameResultBottom == null ? "" : item.LeftNameResultBottom.ToString());
                            builder.Append("δ");
                            builder.Append(item.LeftNameMiddle1Text == null ? "" : item.LeftNameMiddle1Text.ToString());
                            builder.Append("δ");
                            builder.Append(item.MiddleNameResultTop == null ? "" : item.MiddleNameResultTop.ToString());
                            builder.Append("δ");
                            builder.Append(item.RightNameResultTop == null ? "" : item.RightNameResultTop.ToString());
                            builder.Append("δ");
                            builder.Append(item.RightNameResultMiddle1 == null ? "" : item.RightNameResultMiddle1.ToString());
                            builder.Append("δ");
                            builder.Append(item.RightNameResultMiddle2 == null ? "" : item.RightNameResultMiddle2.ToString());
                            builder.Append("δ");
                            builder.Append(item.RightNameResultBottom == null ? "" : item.RightNameResultBottom.ToString());
                            builder.Append("δ");
                            builder.Append(item.LastUpdateDate == null ? "" : item.LastUpdateDate.ToString());
                            builder.Append("δ");
                            builder.Append(item.SourceFullName == null ? "" : item.SourceFullName.ToString());
                            builder.Append("δ");
                            builder.Append(item.ExpantionResult1 == null ? "" : item.ExpantionResult1.ToString());
                            builder.Append("δ");
                            builder.Append(item.ExpantionResult2 == null ? "" : item.ExpantionResult2.ToString());
                            builder.Append("δ");
                            builder.Append(item.ExpantionResult3 == null ? "" : item.ExpantionResult3.ToString());
                            builder.Append("δ");
                            builder.Append(item.ExpantionResult4 == null ? "" : item.ExpantionResult4.ToString());
                            builder.Append("δ");
                            builder.Append(item.ExpantionResult5 == null ? "" : item.ExpantionResult5.ToString());

                            if (builder.ToString().ToLower().Contains(key.ToLower()))
                            {
                                recs.Add(item);
                            }

                            // int count = recs.Count;

                            model.SearchResult = recs.ToList();
                        }
                        break;
                }
                model.SearchResult = model.SearchResult.Skip(search.PageSize * (search.PageIndex - 1)).Take(search.PageSize).ToList();

                if (search.FilterKeyword == "All")
                {
                    model.Keyword = "";
                }
                else { model.Keyword = search.FilterKeyword; }

                if (keyword == null || keyword == "" && model.SearchResult.Count == 0)
                {
                    model.SearchStatus = "No Results Found";
                }
                else if (keyword != null && keyword.Length > 100)
                {
                    model.SearchStatus = "Your keyword search can be no longer than 100 characters maximum";
                }
                else if (keyword != null && model.SearchResult.Count == 0)
                {
                    model.SearchStatus = "Sorry no search data found.Please verify search keyword and try again";
                }

                #endregion Mvc code

                // model.SearchResult = model.SearchStatus.Skip(search.PageSize * (search.PageIndex - 1)).Take(search.PageSize).ToList();

                return new PdfResult(view, model, fileName, fileName).FileStorageSection();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        [Route("QuickSearchSave_GeneratedDocument")]
        public Task<HttpResponseMessage> GenarateQuickSearchSavePdf([FromUri]UserServiceModel searchInput)
        {
            Random rnd = new Random();
            string fileName = "My_Saved_Search_Results_" + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + rnd.Next(00000, 99999) + ".pdf";
            //  {"pageSize":10,"pageIndex":1,"UserId":"8EB70E26-725E-4E52-9109-CF9C37F3B980","DisplayType":"All"}
            string displayType = searchInput.DisplayType;
            searchInput.DisplayType = "All";
            //switch (searchInput.DisplayType)
            //{
            //    case "All":
            //        searchInput.DisplayType = "All";
            //        break;
            //    case "Business License":
            //        searchInput.DisplayType = "BBL";
            //        break;
            //    case "Professional License":
            //        searchInput.DisplayType = "OPLA";
            //        break;
            //    case "Alcoholic Beverage License":
            //        searchInput.DisplayType = "ABRA";
            //        break;
            //    case "Certified Business Enterprise":
            //        searchInput.DisplayType = "CBE";
            //        break;
            //    case "Corporate Registration":
            //        searchInput.DisplayType = "CORP";
            //        break;
            //    default:
            //        searchInput.DisplayType = "All";
            //        break;
            //}

            var pdfResult = Task.Run(() => _myServiceDetails.GetAllData(searchInput)).Result;
            List<SearchData> data = new List<SearchData>();
            searchInput.DisplayType = displayType;
            var getFileByteCode = QuickSearchSavePdfGenatedBytes(pdfResult, fileName, searchInput);

            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new ByteArrayContent(getFileByteCode.ToArray()) };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = fileName
            };
            return Task.FromResult(response);
        }

        public byte[] QuickSearchSavePdfGenatedBytes(IEnumerable<CommonData> quickSearchData, string fileName, UserServiceModel search)
        {
            var view = "QuickSearchSaveView";
            SearchDataMvcViewModel model = new SearchDataMvcViewModel();
            var data = quickSearchData.ToList();

            model.SearchResult = data;
            model.UserServiceModel = search;

            Dictionary<string, string> names = new Dictionary<string, string>();
            names.Add("BBL", "Business License");
            names.Add("CORP", "Corporate Registration");
            names.Add("OPLA", "Professional License");
            names.Add("CBE", "Certified Business Enterprise");
            names.Add("ABRA", "Alcoholic Beverage License");
            names.Add("All", "All");
            if (search.DisplayType != null)
            {
                var items = search.DisplayType.Split('-');
                string[] search_types = new string[items.Length - 1];
                for (int i = 0; i < items.Length - 1; i++)
                {
                    search_types[i] = names[items[i]];
                }
                model.SearchCritiria = search_types;
            }

            #region Mvc code

            // below line has to be removed.
            //search.DisplayType = "All";
            string keyword = search.DisplayType;
            if (string.IsNullOrEmpty(keyword))
            {
                search.DisplayType = "All";
                keyword = "All";
            }
            switch (keyword)
            {
                case "All":
                    if (model.SearchResult.Count == 0)
                    {
                        model.SearchStatus = "No Results Found";
                    }
                    break;

                case "Business License":
                    model.SearchResult = model.SearchResult.Where(j => j.Source == "BBL").ToList();
                    break;

                case "Professional License":
                    model.SearchResult = model.SearchResult.Where(j => j.Source == "OPLA").ToList();
                    break;

                case "Alcoholic Beverage License":
                    model.SearchResult = model.SearchResult.Where(j => j.Source == "ABRA").ToList();
                    break;

                case "Certified Business Enterprise":
                    model.SearchResult = model.SearchResult.Where(j => j.Source == "CBE").ToList();
                    break;

                case "Corporate Registration":
                    model.SearchResult = model.SearchResult.Where(j => j.Source == "CORP").ToList();
                    break;

                default:
                    List<CommonData> recs = new List<CommonData>();
                    string key = keyword.ToUpper();
                    try
                    {
                        foreach (var item in model.SearchResult)
                        {
                            StringBuilder builder = new StringBuilder();
                            builder.Append(item.LeftNameResultTop == null ? "" : item.LeftNameResultTop.ToString());
                            builder.Append("δ");
                            builder.Append(item.LeftNameResultMiddle == null ? "" : item.LeftNameResultMiddle.ToString());
                            builder.Append("δ");
                            builder.Append(item.LeftNameResultBottom == null ? "" : item.LeftNameResultBottom.ToString());
                            builder.Append("δ");
                            builder.Append(item.LeftNameMiddle1Text == null ? "" : item.LeftNameMiddle1Text.ToString());
                            builder.Append("δ");
                            builder.Append(item.MiddleNameResultTop == null ? "" : item.MiddleNameResultTop.ToString());
                            builder.Append("δ");
                            builder.Append(item.RightNameResultTop == null ? "" : item.RightNameResultTop.ToString());
                            builder.Append("δ");
                            builder.Append(item.RightNameResultMiddle1 == null ? "" : item.RightNameResultMiddle1.ToString());
                            builder.Append("δ");
                            builder.Append(item.RightNameResultMiddle2 == null ? "" : item.RightNameResultMiddle2.ToString());
                            builder.Append("δ");
                            builder.Append(item.RightNameResultBottom == null ? "" : item.RightNameResultBottom.ToString());
                            builder.Append("δ");
                            builder.Append(item.LastUpdateDate == null ? "" : item.LastUpdateDate.ToString());
                            builder.Append("δ");
                            builder.Append(item.SourceFullName == null ? "" : item.SourceFullName.ToString());
                            builder.Append("δ");
                            builder.Append(item.ExpantionResult1 == null ? "" : item.ExpantionResult1.ToString());
                            builder.Append("δ");
                            builder.Append(item.ExpantionResult2 == null ? "" : item.ExpantionResult2.ToString());
                            builder.Append("δ");
                            builder.Append(item.ExpantionResult3 == null ? "" : item.ExpantionResult3.ToString());
                            builder.Append("δ");
                            builder.Append(item.ExpantionResult4 == null ? "" : item.ExpantionResult4.ToString());
                            builder.Append("δ");
                            builder.Append(item.ExpantionResult5 == null ? "" : item.ExpantionResult5.ToString());

                            if (builder.ToString().ToLower().Contains(key.ToLower()))
                            {
                                recs.Add(item);
                            }

                            // int count = recs.Count;

                            model.SearchResult = recs.ToList();
                        }
                        break;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
            }
            model.SearchResult = model.SearchResult.Skip(search.PageSize * (search.PageIndex - 1)).Take(search.PageSize).ToList();

            if (search.DisplayType == "All")
            {
                model.Keyword = "";
            }
            else { model.Keyword = search.DisplayType; }

            if (keyword == null || keyword == "" && model.SearchResult.Count == 0)
            {
                model.SearchStatus = "No Results Found";
            }
            else if (keyword != null && keyword.Length > 100)
            {
                model.SearchStatus = "Your keyword search can be no longer than 100 characters maximum";
            }
            else if (keyword != null && model.SearchResult.Count == 0)
            {
                model.SearchStatus = "Sorry no search data found.Please verify search keyword and try again";
            }

            #endregion Mvc code

            switch (search.DisplayType)
            {
                case "All":
                    search.DisplayType = "All";
                    break;

                case "BBL":
                    search.DisplayType = "Business License";
                    break;

                case "OPLA":
                    search.DisplayType = "Professional License";
                    break;

                case "ABRA":
                    search.DisplayType = "Alcoholic Beverage License";
                    break;

                case "CBE":
                    search.DisplayType = "Certified Business Enterprise";
                    break;

                case "CORP":
                    search.DisplayType = "Corporate Registration";
                    break;

                default:
                    search.DisplayType = "All";
                    break;
            }

            var countResult = Task.Run(() => _myServiceDetails.GetCount(search)).Result.ToList().FirstOrDefault();

            //   model.LicenseCounts.ABRACount = countResult.ABRACount;

            string bblcount = countResult.BBLCount;
            SearchDataViewModel searchDataViewModel = new SearchDataViewModel();
            searchDataViewModel.BBLCount = countResult.BBLCount;
            searchDataViewModel.ABRACount = countResult.ABRACount;
            searchDataViewModel.CBECount = countResult.CBECount;
            searchDataViewModel.CORPCount = countResult.CORPCount;
            searchDataViewModel.OPLACount = countResult.OPLACount;
            searchDataViewModel.RecordCount = countResult.RecordCount;
            searchDataViewModel.ExcededCount = countResult.ExcededCount;
            model.LicenseCounts = searchDataViewModel;

            //var data = quickSearchData..Select(i => new CommonDataModel().InjectFrom(i)).Cast<CommonDataModel>().ToList();

            // model.SearchResult = model.SearchStatus.Skip(search.PageSize * (search.PageIndex - 1)).Take(search.PageSize).ToList();

            return new PdfResult(view, model, fileName, fileName).FileStorageSection();
        }

        //[HttpGet]
        //[Route("QuickSearch_GeneratedDocument")]
        //public Task<HttpResponseMessage> GenarateQuickSearchPdf([FromUri]SearchInput searchInput, string refreshtoken)
        //{
        //    try
        //    {
        //        Random rnd = new Random();
        //        string fileName = "Quick_Search_Results_" + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + rnd.Next(00000, 99999) + ".pdf";

        //        SearchInput inputs = new SearchInput();
        //        inputs.InjectFrom(searchInput);

        //        searchInput.CompanyName = searchInput.CompanyName == null ? "" : searchInput.CompanyName.ToUpper().Replace(" AND ", " ").Replace(" & ", " ");
        //        searchInput.LicenseName = searchInput.LicenseName == null ? "" : searchInput.LicenseName.ToUpper().Replace(" AND ", " ").Replace(" & ", " ");
        //        searchInput.FirstName = searchInput.FirstName == null ? "" : searchInput.FirstName.ToUpper().Replace(" AND ", " ").Replace(" & ", " ");
        //        searchInput.LastName = searchInput.LastName == null ? "" : searchInput.LastName.ToUpper().Replace(" AND ", " ").Replace(" & ", " ");
        //        var searchServiceInput = new SearchServiceInput();
        //        searchServiceInput.InjectFrom(searchInput);

        //        var pdfResult = _searchService.PdfDataService(searchServiceInput).Result.ToList();
        //        //var getresult = pdfResult.Result.OrderBy(x => x.SearchFinalData).ToList();

        //        //  var getFileByteCode = QuickSearchPdfGenatedBytes(getresult.ToList(), "Quick_Search_Results.pdf", searchInput);
        //        var getFileByteCode = QuickSearchPdfGenatedBytes(pdfResult, fileName, inputs);

        //        var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new ByteArrayContent(getFileByteCode.ToArray()) };
        //        response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

        //        response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        //        {
        //            FileName = fileName
        //        };
        //        return Task.FromResult(response);
        //    }
        //    catch (Exception Ex)
        //    {
        //        throw Ex;
        //    }
        //}

        //public byte[] QuickSearchPdfGenatedBytes(List<SearchData> quickSearchData, string fileName, SearchInput search)
        //{
        //    try
        //    {
        //        var view = "QuickSearchDetailsView";

        //        SearchDataMvcViewModel model = new SearchDataMvcViewModel();
        //        //List<CommonDataModel> records = new List<CommonDataModel>();

        //        //records = quickSearchData.Select(i => new CommonDataModel().InjectFrom(i)).Cast<CommonDataModel>().ToList();
        //        //CommonDataModel cdm = new CommonDataModel();

        //        //foreach (var item in quickSearchData)
        //        //{
        //        //    foreach (var i in item.SearchFinalData)
        //        //    {
        //        //        cdm.InjectFrom(i);
        //        //        records.Add(cdm);
        //        //    }

        //        //}
        //        //var countRow = quickSearchData.FirstOrDefault();
        //        //var rowCount = countRow.SearchCount.FirstOrDefault();

        //        //var companyNames = countRow.SearchFinalData.FirstOrDefault();
        //        //companyNames.CompanyName =

        //        //model.ID = rowCount.ID.ToString();
        //        //model.RecordCount = rowCount.RecordCount;
        //        //model.NoofRecords = rowCount.NoofRecords;
        //        //model.ABRAID = rowCount.ABRAID;
        //        //model.Source = rowCount.Source;
        //        //model.Name = rowCount.Name;
        //        //model.ABRACount = rowCount.ABRACount;
        //        //model.BBLCount = rowCount.BBLCount;
        //        //model.CBECount = rowCount.CBECount;
        //        //model.CORPCount = rowCount.CORPCount;
        //        //model.OPLACount = rowCount.OPLACount;
        //        //records.RemoveAt(0);
        //        var searchData = quickSearchData.OrderBy(x => x.SearchFinalData).ToList();
        //        var data = searchData.ElementAt(0).SearchFinalData.ToList();
        //        model.SearchResult = data;
        //        model.LicenseCounts = quickSearchData.First().SearchCount.First();
        //        model.SearchData = search;

        //        Dictionary<string, string> names = new Dictionary<string, string>();
        //        names.Add("BBL", "Business License");
        //        names.Add("CORP", "Corporate Registration");
        //        names.Add("OPLA", "Professional License");
        //        names.Add("CBE", "Certified Business Enterprise");
        //        names.Add("ABRA", "Alcoholic Beverage License");
        //        names.Add("All", "All");
        //        if (search.DisplayType != null)
        //        {
        //            var items = search.DisplayType.Split('-');
        //            string[] search_types = new string[items.Length - 1];
        //            for (int i = 0; i < items.Length - 1; i++)
        //            {
        //                search_types[i] = names[items[i]];
        //            }
        //            model.SearchCritiria = search_types;
        //        }

        //        #region Mvc code

        //        string keyword = search.FilterKeyword;
        //        if (string.IsNullOrEmpty(keyword))
        //        {
        //            search.FilterKeyword = "All";
        //            keyword = "All";
        //        }
        //        switch (keyword)
        //        {
        //            case "All":
        //                if (model.SearchResult.Count == 0)
        //                {
        //                    model.SearchStatus = "No Results Found";
        //                }
        //                break;

        //            case "Business License":
        //                model.SearchResult = model.SearchResult.Where(j => j.Source == "BBL").ToList();
        //                break;

        //            case "Professional License":
        //                model.SearchResult = model.SearchResult.Where(j => j.Source == "OPLA").ToList();
        //                break;

        //            case "Alcoholic Beverage License":
        //                model.SearchResult = model.SearchResult.Where(j => j.Source == "ABRA").ToList();
        //                break;

        //            case "Certified Business Enterprise":
        //                model.SearchResult = model.SearchResult.Where(j => j.Source == "CBE").ToList();
        //                break;

        //            case "Corporate Registration":
        //                model.SearchResult = model.SearchResult.Where(j => j.Source == "CORP").ToList();
        //                break;

        //            default:
        //                List<CommonData> recs = new List<CommonData>();
        //                string key = keyword.ToUpper();

        //                foreach (var item in model.SearchResult)
        //                {
        //                    StringBuilder builder = new StringBuilder();
        //                    builder.Append(item.LeftNameResultTop == null ? "" : item.LeftNameResultTop.ToString());
        //                    builder.Append("δ");
        //                    builder.Append(item.LeftNameResultMiddle == null ? "" : item.LeftNameResultMiddle.ToString());
        //                    builder.Append("δ");
        //                    builder.Append(item.LeftNameResultBottom == null ? "" : item.LeftNameResultBottom.ToString());
        //                    builder.Append("δ");
        //                    builder.Append(item.LeftNameMiddle1Text == null ? "" : item.LeftNameMiddle1Text.ToString());
        //                    builder.Append("δ");
        //                    builder.Append(item.MiddleNameResultTop == null ? "" : item.MiddleNameResultTop.ToString());
        //                    builder.Append("δ");
        //                    builder.Append(item.RightNameResultTop == null ? "" : item.RightNameResultTop.ToString());
        //                    builder.Append("δ");
        //                    builder.Append(item.RightNameResultMiddle1 == null ? "" : item.RightNameResultMiddle1.ToString());
        //                    builder.Append("δ");
        //                    builder.Append(item.RightNameResultMiddle2 == null ? "" : item.RightNameResultMiddle2.ToString());
        //                    builder.Append("δ");
        //                    builder.Append(item.RightNameResultBottom == null ? "" : item.RightNameResultBottom.ToString());
        //                    builder.Append("δ");
        //                    builder.Append(item.LastUpdateDate == null ? "" : item.LastUpdateDate.ToString());
        //                    builder.Append("δ");
        //                    builder.Append(item.SourceFullName == null ? "" : item.SourceFullName.ToString());
        //                    builder.Append("δ");
        //                    builder.Append(item.ExpantionResult1 == null ? "" : item.ExpantionResult1.ToString());
        //                    builder.Append("δ");
        //                    builder.Append(item.ExpantionResult2 == null ? "" : item.ExpantionResult2.ToString());
        //                    builder.Append("δ");
        //                    builder.Append(item.ExpantionResult3 == null ? "" : item.ExpantionResult3.ToString());
        //                    builder.Append("δ");
        //                    builder.Append(item.ExpantionResult4 == null ? "" : item.ExpantionResult4.ToString());
        //                    builder.Append("δ");
        //                    builder.Append(item.ExpantionResult5 == null ? "" : item.ExpantionResult5.ToString());

        //                    if (builder.ToString().ToLower().Contains(key.ToLower()))
        //                    {
        //                        recs.Add(item);
        //                    }

        //                    // int count = recs.Count;

        //                    model.SearchResult = recs.ToList();
        //                }
        //                break;
        //        }
        //        model.SearchResult = model.SearchResult.Skip(search.PageSize * (search.PageIndex - 1)).Take(search.PageSize).ToList();

        //        if (search.FilterKeyword == "All")
        //        {
        //            model.Keyword = "";
        //        }
        //        else { model.Keyword = search.FilterKeyword; }

        //        if (keyword == null || keyword == "" && model.SearchResult.Count == 0)
        //        {
        //            model.SearchStatus = "No Results Found";
        //        }
        //        else if (keyword != null && keyword.Length > 100)
        //        {
        //            model.SearchStatus = "Your keyword search can be no longer than 100 characters maximum";
        //        }
        //        else if (keyword != null && model.SearchResult.Count == 0)
        //        {
        //            model.SearchStatus = "Sorry no search data found.Please verify search keyword and try again";
        //        }

        //        #endregion Mvc code

        //        // model.SearchResult = model.SearchStatus.Skip(search.PageSize * (search.PageIndex - 1)).Take(search.PageSize).ToList();

        //        return new PdfResult(view, model, fileName, fileName).FileStorageSection();
        //    }
        //    catch (Exception Ex)
        //    {
        //        throw Ex;
        //    }
        //}

        //[PdfAuthenticationFilter]
        //[HttpGet]
        //[Route("QuickSearchSave_GeneratedDocument")]
        //public Task<HttpResponseMessage> GenarateQuickSearchSavePdf([FromUri]UserServiceModel searchInput, string reft)
        //{
        //    Random rnd = new Random();
        //    string fileName = "My_Saved_Search_Results_" + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + rnd.Next(00000, 99999) + ".pdf";
        //    //  {"pageSize":10,"pageIndex":1,"UserId":"8EB70E26-725E-4E52-9109-CF9C37F3B980","DisplayType":"All"}
        //    string displayType = searchInput.DisplayType;
        //    searchInput.DisplayType = "All";
        //    //switch (searchInput.DisplayType)
        //    //{
        //    //    case "All":
        //    //        searchInput.DisplayType = "All";
        //    //        break;
        //    //    case "Business License":
        //    //        searchInput.DisplayType = "BBL";
        //    //        break;
        //    //    case "Professional License":
        //    //        searchInput.DisplayType = "OPLA";
        //    //        break;
        //    //    case "Alcoholic Beverage License":
        //    //        searchInput.DisplayType = "ABRA";
        //    //        break;
        //    //    case "Certified Business Enterprise":
        //    //        searchInput.DisplayType = "CBE";
        //    //        break;
        //    //    case "Corporate Registration":
        //    //        searchInput.DisplayType = "CORP";
        //    //        break;
        //    //    default:
        //    //        searchInput.DisplayType = "All";
        //    //        break;
        //    //}

        //    var pdfResult = Task.Run(() => _myServiceDetails.GetAllData(searchInput)).Result;
        //    List<SearchData> data = new List<SearchData>();
        //    searchInput.DisplayType = displayType;
        //    var getFileByteCode = QuickSearchSavePdfGenatedBytes(pdfResult, fileName, searchInput);

        //    var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new ByteArrayContent(getFileByteCode.ToArray()) };
        //    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        //    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        //    {
        //        FileName = fileName
        //    };
        //    return Task.FromResult(response);
        //}

        //public byte[] QuickSearchSavePdfGenatedBytes(IEnumerable<CommonData> quickSearchData, string fileName, UserServiceModel search)
        //{
        //    var view = "QuickSearchSaveView";
        //    SearchDataMvcViewModel model = new SearchDataMvcViewModel();
        //    var data = quickSearchData.ToList();

        //    model.SearchResult = data;
        //    model.UserServiceModel = search;

        //    Dictionary<string, string> names = new Dictionary<string, string>();
        //    names.Add("BBL", "Business License");
        //    names.Add("CORP", "Corporate Registration");
        //    names.Add("OPLA", "Professional License");
        //    names.Add("CBE", "Certified Business Enterprise");
        //    names.Add("ABRA", "Alcoholic Beverage License");
        //    names.Add("All", "All");
        //    if (search.DisplayType != null)
        //    {
        //        var items = search.DisplayType.Split('-');
        //        string[] search_types = new string[items.Length - 1];
        //        for (int i = 0; i < items.Length - 1; i++)
        //        {
        //            search_types[i] = names[items[i]];
        //        }
        //        model.SearchCritiria = search_types;
        //    }

        //    #region Mvc code

        //    // below line has to be removed.
        //    //search.DisplayType = "All";
        //    string keyword = search.DisplayType;
        //    if (string.IsNullOrEmpty(keyword))
        //    {
        //        search.DisplayType = "All";
        //        keyword = "All";
        //    }
        //    switch (keyword)
        //    {
        //        case "All":
        //            if (model.SearchResult.Count == 0)
        //            {
        //                model.SearchStatus = "No Results Found";
        //            }
        //            break;

        //        case "Business License":
        //            model.SearchResult = model.SearchResult.Where(j => j.Source == "BBL").ToList();
        //            break;

        //        case "Professional License":
        //            model.SearchResult = model.SearchResult.Where(j => j.Source == "OPLA").ToList();
        //            break;

        //        case "Alcoholic Beverage License":
        //            model.SearchResult = model.SearchResult.Where(j => j.Source == "ABRA").ToList();
        //            break;

        //        case "Certified Business Enterprise":
        //            model.SearchResult = model.SearchResult.Where(j => j.Source == "CBE").ToList();
        //            break;

        //        case "Corporate Registration":
        //            model.SearchResult = model.SearchResult.Where(j => j.Source == "CORP").ToList();
        //            break;

        //        default:
        //            List<CommonData> recs = new List<CommonData>();
        //            string key = keyword.ToUpper();
        //            try
        //            {
        //                foreach (var item in model.SearchResult)
        //                {
        //                    StringBuilder builder = new StringBuilder();
        //                    builder.Append(item.LeftNameResultTop == null ? "" : item.LeftNameResultTop.ToString());
        //                    builder.Append("δ");
        //                    builder.Append(item.LeftNameResultMiddle == null ? "" : item.LeftNameResultMiddle.ToString());
        //                    builder.Append("δ");
        //                    builder.Append(item.LeftNameResultBottom == null ? "" : item.LeftNameResultBottom.ToString());
        //                    builder.Append("δ");
        //                    builder.Append(item.LeftNameMiddle1Text == null ? "" : item.LeftNameMiddle1Text.ToString());
        //                    builder.Append("δ");
        //                    builder.Append(item.MiddleNameResultTop == null ? "" : item.MiddleNameResultTop.ToString());
        //                    builder.Append("δ");
        //                    builder.Append(item.RightNameResultTop == null ? "" : item.RightNameResultTop.ToString());
        //                    builder.Append("δ");
        //                    builder.Append(item.RightNameResultMiddle1 == null ? "" : item.RightNameResultMiddle1.ToString());
        //                    builder.Append("δ");
        //                    builder.Append(item.RightNameResultMiddle2 == null ? "" : item.RightNameResultMiddle2.ToString());
        //                    builder.Append("δ");
        //                    builder.Append(item.RightNameResultBottom == null ? "" : item.RightNameResultBottom.ToString());
        //                    builder.Append("δ");
        //                    builder.Append(item.LastUpdateDate == null ? "" : item.LastUpdateDate.ToString());
        //                    builder.Append("δ");
        //                    builder.Append(item.SourceFullName == null ? "" : item.SourceFullName.ToString());
        //                    builder.Append("δ");
        //                    builder.Append(item.ExpantionResult1 == null ? "" : item.ExpantionResult1.ToString());
        //                    builder.Append("δ");
        //                    builder.Append(item.ExpantionResult2 == null ? "" : item.ExpantionResult2.ToString());
        //                    builder.Append("δ");
        //                    builder.Append(item.ExpantionResult3 == null ? "" : item.ExpantionResult3.ToString());
        //                    builder.Append("δ");
        //                    builder.Append(item.ExpantionResult4 == null ? "" : item.ExpantionResult4.ToString());
        //                    builder.Append("δ");
        //                    builder.Append(item.ExpantionResult5 == null ? "" : item.ExpantionResult5.ToString());

        //                    if (builder.ToString().ToLower().Contains(key.ToLower()))
        //                    {
        //                        recs.Add(item);
        //                    }

        //                    // int count = recs.Count;

        //                    model.SearchResult = recs.ToList();
        //                }
        //                break;
        //            }
        //            catch (Exception ex)
        //            {
        //                throw ex;
        //            }
        //    }
        //    model.SearchResult = model.SearchResult.Skip(search.PageSize * (search.PageIndex - 1)).Take(search.PageSize).ToList();

        //    if (search.DisplayType == "All")
        //    {
        //        model.Keyword = "";
        //    }
        //    else { model.Keyword = search.DisplayType; }

        //    if (keyword == null || keyword == "" && model.SearchResult.Count == 0)
        //    {
        //        model.SearchStatus = "No Results Found";
        //    }
        //    else if (keyword != null && keyword.Length > 100)
        //    {
        //        model.SearchStatus = "Your keyword search can be no longer than 100 characters maximum";
        //    }
        //    else if (keyword != null && model.SearchResult.Count == 0)
        //    {
        //        model.SearchStatus = "Sorry no search data found.Please verify search keyword and try again";
        //    }

        //    #endregion Mvc code

        //    switch (search.DisplayType)
        //    {
        //        case "All":
        //            search.DisplayType = "All";
        //            break;

        //        case "BBL":
        //            search.DisplayType = "Business License";
        //            break;

        //        case "OPLA":
        //            search.DisplayType = "Professional License";
        //            break;

        //        case "ABRA":
        //            search.DisplayType = "Alcoholic Beverage License";
        //            break;

        //        case "CBE":
        //            search.DisplayType = "Certified Business Enterprise";
        //            break;

        //        case "CORP":
        //            search.DisplayType = "Corporate Registration";
        //            break;

        //        default:
        //            search.DisplayType = "All";
        //            break;
        //    }

        //    var countResult = Task.Run(() => _myServiceDetails.GetCount(search)).Result.ToList().FirstOrDefault();

        //    //   model.LicenseCounts.ABRACount = countResult.ABRACount;

        //    string bblcount = countResult.BBLCount;
        //    SearchDataViewModel searchDataViewModel = new SearchDataViewModel();
        //    searchDataViewModel.BBLCount = countResult.BBLCount;
        //    searchDataViewModel.ABRACount = countResult.ABRACount;
        //    searchDataViewModel.CBECount = countResult.CBECount;
        //    searchDataViewModel.CORPCount = countResult.CORPCount;
        //    searchDataViewModel.OPLACount = countResult.OPLACount;
        //    searchDataViewModel.RecordCount = countResult.RecordCount;
        //    searchDataViewModel.ExcededCount = countResult.ExcededCount;
        //    model.LicenseCounts = searchDataViewModel;

        //    //var data = quickSearchData..Select(i => new CommonDataModel().InjectFrom(i)).Cast<CommonDataModel>().ToList();

        //    // model.SearchResult = model.SearchStatus.Skip(search.PageSize * (search.PageIndex - 1)).Take(search.PageSize).ToList();

        //    return new PdfResult(view, model, fileName, fileName).FileStorageSection();
        //}

        //[Authorize]
        //[HttpGet]
        //[Route("EHOP_GeneratedDocument")]
        //public Task<HttpResponseMessage> DownloadEhopDetails(GenaralSubmissionEntity  submitedInput)
        //{
        //    var getFileByteCode = _businessEntityGenarationPdfService.BusinessEntityGenarationPdf(submitedInput.MasterId,
        //        submitedInput.UserId, GenericEnums.GetEnumDescription(GenericEnums.SubmissionGenerationDocumentType.eHOP).ToUpper());
        //    var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new ByteArrayContent(getFileByteCode.FileBytes.ToArray()) };
        //    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        //    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        //    {
        //        FileName = getFileByteCode.Filename
        //    };
        //    return Task.FromResult(response);
        //}

        //public IHttpActionResult GetFileAsync(int fileId)
        //{
        //    // NOTE: If there was any other 'async' stuff here, then you would need to return
        //    // a Task<IHttpActionResult>, but for this simple case you need not.

        //    return new FileActionResult(fileId);
        //}
    }

    //public class FileActionResult : IHttpActionResult
    //{
    //    public FileActionResult(int fileId)
    //    {
    //        this.FileId = fileId;
    //    }

    //    public int FileId { get; private set; }

    //    public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
    //    {
    //        HttpResponseMessage response = new HttpResponseMessage();
    //        response.Content = new StreamContent(File.OpenRead(@"<base path>" + FileId));
    //        response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");

    //        // NOTE: Here I am just setting the result on the Task and not really doing any async stuff.
    //        // But let's say you do stuff like contacting a File hosting service to get the file, then you would do 'async' stuff here.

    //        return Task.FromResult(response);
    //    }
    //}
}