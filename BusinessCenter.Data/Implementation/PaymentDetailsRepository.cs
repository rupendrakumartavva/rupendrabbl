using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessCenter.Data.Implementation
{
    public class PaymentDetailsRepository : GenericRepository<PaymentDetails>, IPaymentDetailsRepository
    {
        protected IPaymentCardDetailsRepository _payCardResp;
        protected IPaymentAddressDetailsRepository _payAddressResp;
        protected ISubmissionMasterRepository _subMasterResp;
        protected ISubmissionCategoryRepository _catRepo;
        protected ISubmissionDocumentRepository _docRepo;
        protected ISubmissionMasterApplicationChcekListRepository SubChcekListAppRep;
        protected IFixFeeRepository fixfeeRepo;
        protected ISubmissionMasterRenewalRepository subrnewrepo;
        protected IDCBC_ENTITY_BBL_Renewal_InvoiceRepository dcrnewinovicerepo;
        protected IBblRepository BblRepository;
        protected IUserBBLServiceRepository UserRepository;
        protected ILookup_ExistingCategoriesRepository _lookupExistingCategoriesRepo;
        protected IPaymentHistoryDetailsRepository _paymentHistorydetails;

        public PaymentDetailsRepository(IUnitOfWork context, IPaymentCardDetailsRepository payCardRespository,
           IPaymentAddressDetailsRepository payAddressRespository, ISubmissionMasterRepository SubMasterRepository,
              ISubmissionCategoryRepository catRepository, ISubmissionDocumentRepository docRepository,
            ISubmissionMasterApplicationChcekListRepository subChcekListAppRep, IFixFeeRepository fixfeeRepository,
            ISubmissionMasterRenewalRepository subrnewrepsoiotry, IDCBC_ENTITY_BBL_Renewal_InvoiceRepository dcrnewInvoiceReposiotory,
           IBblRepository bblRepository, IUserBBLServiceRepository _UserRepository, ILookup_ExistingCategoriesRepository lookup_ExistingCategoriesRepo,
            IPaymentHistoryDetailsRepository paymentHistorydetails)
            : base(context)
        {
            _payCardResp = payCardRespository;
            _payAddressResp = payAddressRespository;
            _subMasterResp = SubMasterRepository;
            _docRepo = docRepository;
            _catRepo = catRepository;
            SubChcekListAppRep = subChcekListAppRep;
            fixfeeRepo = fixfeeRepository;
            subrnewrepo = subrnewrepsoiotry;
            dcrnewinovicerepo = dcrnewInvoiceReposiotory;
            BblRepository = bblRepository;
            UserRepository = _UserRepository;
            _lookupExistingCategoriesRepo = lookup_ExistingCategoriesRepo;
            _paymentHistorydetails = paymentHistorydetails;
        }

        /// <summary>
        /// This method is used to retrive Payment Details based on Application Unique Id.
        /// </summary>
        /// <param name="paymentDetails"></param>
        /// <returns>Specific Payment Details</returns>
        public IEnumerable<PaymentDetails> FindByPaymentID(PaymentDetails paymentDetails)
        {
            var payDetails = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == paymentDetails.MasterId);
            return payDetails;
        }

        /// <summary>
        /// This method is used to insert Payment Details based on PaymentDetailsModel
        /// </summary>
        /// <param name="pDetails"></param>
        /// <returns>Retrun Result in String</returns>
        public string InsertPaymentDetails(PaymentDetailsModel pDetails)
        {
            string Status = string.Empty;
            string guid = string.Empty;
            try
            {
                PaymentDetails paymentDetails = new PaymentDetails();
                paymentDetails.MasterId = pDetails.MasterId;
                var paymentExist = FindByPaymentID(paymentDetails).ToList();
                if (!paymentExist.Any())
                {
                    var payDetails = new PaymentDetails();
                    guid = Guid.NewGuid().ToString();
                    pDetails.PaymentId = guid;
                    payDetails.PaymentId = guid;
                    payDetails.MasterId = (pDetails.MasterId ?? "").Trim();
                    payDetails.Ordernumber = (pDetails.OrderNumber ?? "").Trim();
                    payDetails.PaymentFrom = (pDetails.PaymentType ?? "").Trim();
                    payDetails.PaymentMailAddress = (pDetails.PaymentMailAddress ?? "").Trim(); ;
                    payDetails.Signature = (pDetails.Signature ?? "").Trim(); ;
                    payDetails.IsAggree = pDetails.Signature != null && pDetails.IsAggree;
                    payDetails.TranscationId = (pDetails.TranscationId ?? "").Trim();
                    payDetails.PaymentStatus = (pDetails.PaymentStatus ?? "").Trim();
                    payDetails.PaymentDate = DateTime.Now;
                    payDetails.ApproveBy = (pDetails.ApproveBy ?? "").Trim();
                    payDetails.Description = (pDetails.Description ?? "").Trim();
                    payDetails.CreatedDate = DateTime.Now;
                    payDetails.UpdatedDate = DateTime.Now;
                    payDetails.Application_Transcation_Status = (pDetails.ApplicationTransactionStatus);
                    Add(payDetails);
                    Save();
                }
                else
                {
                    var payDetails = paymentExist.FirstOrDefault();
                    //guid
                    pDetails.PaymentId = payDetails.PaymentId;
                    //payDetails.PaymentId = guid;
                    //payDetails.MasterId = (pDetails.MasterId ?? "").Trim();
                    payDetails.Ordernumber = (pDetails.OrderNumber ?? "").Trim();
                    payDetails.PaymentFrom = (pDetails.PaymentType ?? "").Trim();
                    payDetails.PaymentMailAddress = (pDetails.PaymentMailAddress ?? "").Trim(); ;
                    payDetails.Signature = (pDetails.Signature ?? "").Trim(); ;
                    payDetails.IsAggree = pDetails.Signature != null && pDetails.IsAggree;
                    payDetails.TranscationId = (pDetails.TranscationId ?? "").Trim();
                    payDetails.PaymentStatus = (pDetails.PaymentStatus ?? "").Trim();
                    payDetails.PaymentDate = DateTime.Now;
                    payDetails.ApproveBy = (pDetails.ApproveBy ?? "").Trim();
                    payDetails.Description = (pDetails.Description ?? "").Trim();
                    // payDetails.CreatedDate = DateTime.Now;
                    payDetails.UpdatedDate = DateTime.Now;
                    payDetails.Application_Transcation_Status = (pDetails.ApplicationTransactionStatus);
                    Update(payDetails, payDetails.PaymentId);
                    Save();
                }
                if (pDetails.CardType != "NA")
                {
                    _payCardResp.InsertPaymentCardDetails(pDetails);
                }
                _payAddressResp.InsertPaymentAddressDetails(pDetails);
                Status = pDetails.PaymentId;
            }
            catch (Exception )
            { Status = ""; }

            return Status;
        }

        /// <summary>
        /// This method is used to Update the Payment Details based on PaymentDetailsModel
        /// </summary>
        /// <param name="pDetails"></param>
        /// <returns>Return Result bool</returns>
        public bool UpdatePaymentDetails(PaymentDetailsModel pDetails)
        {
            bool Status = false;
            try
            {
                var paymentupdate = (from paymentdetails in (FindBy(x => x.PaymentId.Replace(System.Environment.NewLine, "") == pDetails.PaymentId
                                         && x.MasterId.Replace(System.Environment.NewLine, "") == pDetails.MasterId
                                         ))
                                     select paymentdetails).FirstOrDefault();
                paymentupdate.Ordernumber = pDetails.OrderNumber;
                paymentupdate.TranscationId = pDetails.TranscationId;
                paymentupdate.PaymentStatus = pDetails.PaymentStatus;
                paymentupdate.PaymentDate = DateTime.Now;
                paymentupdate.Application_Transcation_Status = pDetails.ApplicationTransactionStatus;
                Update(paymentupdate, paymentupdate.PaymentId);
                Save();

                _paymentHistorydetails.InsertPaymentDetails(pDetails);
                Status = true;
            }
            catch (Exception )
            { Status = false; }
            return Status;
        }

        //public virtual void SaveChanges()
        //{
        //    Context.SaveChanges();
        //}
        /// <summary>
        /// This method is used to get The payment Details of the specific Application Unique Id
        /// </summary>
        /// <param name="RModel"></param>
        /// <returns>Return ReceiptModel Data</returns>
        //public ReceiptModel GetReceiptData(ReceiptModel RModel)
        //{
        //    try
        //    {
        //        string userId = string.Empty;
        //        var subMaster = _subMasterResp.FindByMasterID(RModel.MasterID).ToList();

        //        if (subMaster.Count() != 0)
        //        {
        //            RModel.MasterID = (RModel.MasterID ?? "").Trim();
        //            RModel.PaymentID = (RModel.PaymentID ?? "").Trim();

        //            var master = subMaster.FirstOrDefault();
        //            userId = (master.UserID ?? "").Trim();
        //            var getUserRepo = UserRepository.CheckUserBBL(master.SubmissionLicense, master.UserID).ToList();
        //            var userRepo = getUserRepo.FirstOrDefault();
        //            RModel.LicenseDuration = master.LicenseDuration.ToString();
        //            var subCheckList = SubChcekListAppRep.FindByMasterId(RModel.MasterID).ToList().FirstOrDefault();
        //            if (subCheckList != null)
        //            {
        //                RModel.IsEhopAllowed = Convert.ToBoolean(subCheckList.IsSubmissioneHop);
        //            }
        //            if (getUserRepo.Count() != 0)
        //            {
        //                if (userRepo.Type.Trim().ToUpper() == "A")
        //                {
        //                    var bblLicense = BblRepository.FindByID(Convert.ToInt32(userRepo.DCBC_ENTITY_ID)).FirstOrDefault();
        //                    var bbllren = subrnewrepo.FindByID(RModel.MasterID).ToList();
        //                    if (bbllren.Count() != 0)
        //                    {
        //                        RModel.SubNumber = bbllren.FirstOrDefault().SubmissionLicense ?? "";
        //                    }
        //                    else if (bblLicense != null)
        //                    {
        //                        RModel.SubNumber = bblLicense.B1_ALT_ID;
        //                    }
        //                }
        //                else
        //                {
        //                    if (master.SubmissionLicense.Contains("LA") || master.SubmissionLicense.Contains("DA"))
        //                    {
        //                        RModel.SubNumber = (master.SubmissionLicense ?? "").Trim();
        //                    }
        //                    else
        //                    {
        //                        RModel.SubNumber = master.SubmissionLicense;
        //                    }
        //                }
        //            }
        //            RModel.AmountCharged = master.GrandTotal == null ? 0 : Convert.ToDecimal(master.GrandTotal);
        //            RModel.DocType = master.DocSubmType == null ? "" : master.DocSubmType;
        //            var paymentdetails = FindBy(x => x.MasterId.Trim() == RModel.MasterID.Trim() && x.PaymentId.Trim() == RModel.PaymentID).ToList();
        //            var payment = paymentdetails.FirstOrDefault();
        //            if (paymentdetails.Count() != 0)
        //            {
        //                RModel.PaymentMailId = (payment.PaymentMailAddress ?? "").Trim();
        //                RModel.ReceiptDate = Convert.ToDateTime(payment.PaymentDate).ToString("MM/dd/yyyy");
        //                RModel.ExceptedFinalCheckingDate = Convert.ToDateTime(payment.PaymentDate).AddDays(30).ToString("MM/dd/yyyy");
        //                RModel.TransactionId = payment.TranscationId == null ? "" : payment.TranscationId.Trim();
        //                var cardDetails = _payCardResp.FindByID(RModel).ToList();
        //                if (cardDetails.Count() != 0)
        //                {
        //                    var card = cardDetails.FirstOrDefault();
        //                    RModel.CardNumber = "************" + (card.CardNumber == null ? "" : card.CardNumber.Substring(card.CardNumber.Length - 4));
        //                }
        //            }
        //            ServiceChecklist servicelist = new ServiceChecklist();
        //            servicelist.MasterId = RModel.MasterID.Trim();
        //            RModel.ServiceCheckList = _catRepo.ServiceCheckList(servicelist).DetailedCategoryList;

        //            foreach (var item in RModel.ServiceCheckList)
        //            {
        //                if (RModel.IsBackgroundInvestigation != true)
        //                {
        //                    RModel.IsBackgroundInvestigation = item.IsBackgroundInvestigation;
        //                }
        //            }
        //            var renewaldata = subrnewrepo.FindByID(servicelist.MasterId).ToList();
        //            if (renewaldata.Count() != 0)
        //            {
        //                var renewal = renewaldata.FirstOrDefault();
        //                decimal subrenewtotal = 0;
        //                var renewdata =
        //                    dcrnewinovicerepo.FindAmountByLicense(renewal.SubmissionLicense).ToList();
        //                if (renewdata.Count() != 0)
        //                {
        //                    var endoresement =
        //                        renewdata.Where(x => x.GF_DES.ToUpper().Trim().Contains("ENDORSEMENT")).ToList();
        //                    if (endoresement.Count() != 0)
        //                    {
        //                        RModel.EndorsementFee = Convert.ToDecimal(endoresement.FirstOrDefault().GF_FEE) / Convert.ToInt32(endoresement.FirstOrDefault().GF_UNIT);
        //                        RModel.ServiceCheckList[0].EndorsementFee = RModel.EndorsementFee;
        //                    }
        //                    else
        //                    {
        //                        RModel.EndorsementFee = 0;
        //                    }
        //                    var application =
        //                        renewdata.Where(x => x.GF_DES.ToUpper().Trim().Contains("APPLICATION")).ToList();
        //                    //     Application Fee
        //                    if (application.Count() != 0)
        //                    {
        //                        RModel.ApplicationFee = Convert.ToDecimal(application.FirstOrDefault().GF_FEE);
        //                        RModel.ServiceCheckList[0].ApplicationFee = RModel.ApplicationFee;
        //                    }
        //                    else
        //                    {
        //                        RModel.ApplicationFee = 0;
        //                    }
        //                    int count = 0;
        //                    decimal roafee = 0;
        //                    foreach (var service in RModel.ServiceCheckList)
        //                    {
        //                        if (count == 0)
        //                        {
        //                            var datadd = RModel.ServiceCheckList[0].LicenseCategory;

        //                            var raofee = renewdata.Where(x => x.GF_DES.ToUpper().Trim().Contains("RAO FEE")).ToList();
        //                            roafee = raofee.Count() != 0 ? Convert.ToDecimal(raofee.FirstOrDefault().GF_FEE) : 0;
        //                            decimal categoryfee = 0;
        //                            var lookupCategoryName = _lookupExistingCategoriesRepo.NewCategoryFindBy(datadd).ToList();
        //                            bool status = false;
        //                            foreach (var category in lookupCategoryName)
        //                            {
        //                                var catfee = renewdata.Where(x => x.GF_DES.ToUpper().Trim() == category.ExistingCategory.ToUpper().Trim()).ToList();
        //                                if (catfee.Count() != 0 && !status)
        //                                {
        //                                    categoryfee = Convert.ToDecimal(catfee.FirstOrDefault().GF_FEE) + roafee;
        //                                    RModel.ServiceCheckList[count].CategoryLicenseFee = Convert.ToDecimal(categoryfee);

        //                                    if (!string.IsNullOrEmpty(catfee.FirstOrDefault().GF_UNIT.ToString().Trim()))
        //                                    {
        //                                        RModel.ServiceCheckList[count].Units = (Convert.ToInt32(catfee.FirstOrDefault().GF_UNIT)).ToString().Replace("1",
        //                                        (Convert.ToInt32(catfee.FirstOrDefault().GF_UNIT)).ToString().Replace(".00", ""));
        //                                    }
        //                                    else
        //                                    {
        //                                        RModel.ServiceCheckList[count].Units = "1";
        //                                    }

        //                                    RModel.ServiceCheckList[count].SubTotal = categoryfee + RModel.EndorsementFee + RModel.ApplicationFee;
        //                                    RModel.ServiceCheckList[count].TechFee = (RModel.ServiceCheckList[0].SubTotal / 100) * 10;

        //                                    if (renewal.Extradays.ToUpper() == "EXPIRED")
        //                                    {
        //                                        RModel.ServiceCheckList[count].ExpiryFee = Convert.ToDecimal(renewal.ExpiredFee);
        //                                    }
        //                                    RModel.ServiceCheckList[count].LapsedFee = Convert.ToDecimal(renewal.LapsedFee);
        //                                    RModel.ServiceCheckList[count].TotalFee = RModel.ServiceCheckList[0].TechFee + RModel.ServiceCheckList[0].SubTotal + RModel.ServiceCheckList[count].LapsedFee + RModel.ServiceCheckList[count].ExpiryFee;
        //                                    status = true;
        //                                }
        //                            }
        //                            RModel.CategoryLicenseFee = categoryfee;
        //                        }
        //                        else
        //                        {
        //                            var datadd = RModel.ServiceCheckList[count].LicenseCategory;
        //                            decimal categoryfee = 0;
        //                            var lookupCategoryName = _lookupExistingCategoriesRepo.NewCategoryFindBy(datadd).ToList();
        //                            bool status = false;
        //                            foreach (var category in lookupCategoryName)
        //                            {
        //                                var catfee =
        //                                    renewdata.Where(x => x.GF_DES.ToUpper().Trim() == category.ExistingCategory.ToUpper().Trim())
        //                                        .ToList();
        //                                if (catfee.Count() != 0)
        //                                {
        //                                    categoryfee = Convert.ToDecimal(catfee.FirstOrDefault().GF_FEE);

        //                                    if (!string.IsNullOrEmpty(catfee.FirstOrDefault().GF_UNIT.ToString().Trim()))
        //                                    {
        //                                        //RModel.ServiceCheckList[count].Units = (Convert.ToInt32(catfee.FirstOrDefault().GF_UNIT)).ToString().Replace("1",
        //                                        //(Convert.ToInt32(catfee.FirstOrDefault().GF_UNIT)).ToString().Replace(".00", ""));

        //                                        RModel.ServiceCheckList[count].Units = (RModel.ServiceCheckList[count].Units).ToString().Replace("1",
        //                                        (Convert.ToInt32(catfee.FirstOrDefault().GF_UNIT)).ToString().Replace(".00", ""));
        //                                    }
        //                                    else
        //                                    {
        //                                        RModel.ServiceCheckList[count].Units = "1";
        //                                    }

        //                                    //   RModel.ServiceCheckList[count].Units = (RModel.ServiceCheckList[count].Units).Replace("1", (catfee.FirstOrDefault().GF_UNIT.ToString()).Replace(".00", ""));
        //                                    string endoresment = service.Endorsement.Trim();
        //                                    var endoresemetcheck = RModel.ServiceCheckList.Where(x => x.Endorsement.ToUpper().Trim() == endoresment.ToUpper()).ToList();
        //                                    if (endoresemetcheck.Count() == 1)
        //                                    {
        //                                        RModel.ServiceCheckList[count].EndorsementFee = RModel.EndorsementFee;
        //                                    }
        //                                    else
        //                                    { RModel.ServiceCheckList[count].EndorsementFee = 0; }
        //                                    RModel.ServiceCheckList[count].CategoryLicenseFee =
        //                                        Convert.ToDecimal(categoryfee);

        //                                    RModel.ServiceCheckList[count].SubTotal = categoryfee + RModel.ServiceCheckList[count].EndorsementFee;
        //                                    RModel.ServiceCheckList[count].TechFee =
        //                                        (RModel.ServiceCheckList[count].SubTotal / 100) * 10;
        //                                    RModel.ServiceCheckList[count].TotalFee =
        //                                        RModel.ServiceCheckList[count].TechFee +
        //                                        RModel.ServiceCheckList[count].SubTotal;
        //                                }
        //                            }
        //                        }
        //                        count = count + 1;
        //                    }
        //                    var subtotal = RModel.CategoryLicenseFee + RModel.EndorsementFee + RModel.ApplicationFee + roafee;
        //                    RModel.TechFee = (subtotal * 10) / 100;
        //                    if (renewaldata.FirstOrDefault().Extradays.ToUpper().Trim() == "LAPSED")
        //                    {
        //                        RModel.ExtraAmount = (renewaldata.FirstOrDefault().LapsedFee ?? 0) + (renewaldata.FirstOrDefault().ExpiredFee ?? 0);
        //                    }
        //                    else if (renewaldata.FirstOrDefault().Extradays.ToUpper().Trim() == "EXPIRED")
        //                    {
        //                        RModel.ExtraAmount = (renewaldata.FirstOrDefault().LapsedFee ?? 0) + (renewaldata.FirstOrDefault().ExpiredFee ?? 0);
        //                    }
        //                    else
        //                    {
        //                        RModel.ExtraAmount = 0;
        //                    }
        //                    var bblpenaltyexipred = renewdata.Where(x => x.GF_DES.Trim().ToUpper().Contains("PENALTY FEE (EXPIRED)")).ToList();
        //                    if (bblpenaltyexipred.Any())
        //                    {
        //                        if (bblpenaltyexipred.Count() == 1)
        //                        {
        //                            renewdata.Remove(
        //                                renewdata.Where(x => x.GF_DES.Trim().ToUpper().Contains("PENALTY FEE (EXPIRED)")).Single());
        //                        }
        //                        else
        //                        {
        //                            foreach (var penality in bblpenaltyexipred)
        //                            {
        //                                renewdata.Remove(renewdata.Where(x => x.GF_DES.Trim().ToUpper().Contains("PENALTY FEE (EXPIRED)") && x.GF_FEE == penality.GF_FEE).Single());
        //                            }
        //                        }
        //                    }
        //                    var bblpenaltylapsed = renewdata.Where(x => x.GF_DES.Trim().ToUpper().Contains("PENALTY FEE (LAPSE)")).ToList();
        //                    if (bblpenaltylapsed.Any())
        //                    {
        //                        if (bblpenaltylapsed.Count() == 1)
        //                        {
        //                            renewdata.Remove(
        //                            renewdata.Where(x => x.GF_DES.Trim().ToUpper().Contains("PENALTY FEE (LAPSE)")).Single());
        //                        }
        //                        else
        //                        {
        //                            foreach (var penality in bblpenaltylapsed)
        //                            {
        //                                renewdata.Remove(
        //                         renewdata.Where(x => x.GF_DES.Trim().ToUpper().Contains("PENALTY FEE (LAPSE)") && x.GF_FEE == penality.GF_FEE).Single());
        //                            }
        //                        }
        //                    }
        //                    foreach (var renewfee in renewdata)
        //                    {
        //                        subrenewtotal = subrenewtotal + Convert.ToDecimal(renewfee.GF_FEE);
        //                    }
        //                    RModel.TotalFee = subrenewtotal + RModel.ExtraAmount;
        //                }
        //            }
        //            else
        //            {
        //                RModel.ApplicationFee = servicelist.ApplicationFee;
        //                RModel.CategoryLicenseFee = servicelist.CategoryLicenseFee;
        //                RModel.EndorsementFee = servicelist.EndorsementFee;
        //                RModel.SubTotal = servicelist.ApplicationFee + servicelist.EndorsementFee +
        //                                  servicelist.CategoryLicenseFee;
        //                RModel.TechFee = (servicelist.SubTotal / 100) * 10;
        //                RModel.TotalFee = servicelist.TotalFee;
        //                RModel.ExtraAmount = servicelist.ExtraAmount;
        //                RModel.Extradays = servicelist.Extradays;
        //            }
        //            var checklist = SubChcekListAppRep.FindByMasterId(servicelist.MasterId).ToList();
        //            if (checklist.Count() != 0)
        //            {
        //                var checklistData = checklist.FirstOrDefault();
        //                RModel.IsSubmissionCofo = checklistData.IsSubmissionCofo ?? false;
        //                RModel.IsSubmissionHop = checklistData.IsSubmissionHop ?? false;
        //                RModel.IsSubmissioneHop = checklistData.IsSubmissioneHop ?? false;
        //                if (checklist.FirstOrDefault().IsSubmissioneHop.Value)
        //                {
        //                    RModel.Isehop = true;
        //                }
        //                else
        //                {
        //                    RModel.Isehop = false;
        //                }
        //            }
        //            else
        //            {
        //                RModel.Isehop = false;
        //            }
        //            var getUserDetails = _subMasterResp.FindUserDetails(userId).ToList();
        //            if (getUserDetails.Count() != 0)
        //                RModel.FullUserName = getUserDetails.FirstOrDefault().FirstName + " " +
        //                                      getUserDetails.FirstOrDefault().LastName;

        //            BblDocuments docs = new BblDocuments();
        //            docs.MasterId = RModel.MasterID.Trim();
        //            RModel.DocumentList = _docRepo.DocumentByID(RModel.MasterID);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return RModel;
        //}
        /// <summary>
        /// This method is used to get receipt data based on unique id
        /// </summary>
        /// <param name="RModel"></param>
        /// <returns>Return receipt model data</returns>
        public ReceiptModel GetReceiptData(ReceiptModel RModel)
        {
            try
            {
                string userId = string.Empty;
                var subMaster = _subMasterResp.FindByMasterID(RModel.MasterID).ToList();

                if (subMaster.Count() != 0)
                {
                    RModel.MasterID = (RModel.MasterID ?? "").Trim();
                    RModel.PaymentID = (RModel.PaymentID ?? "").Trim();
                   
                    var master = subMaster.FirstOrDefault();
                    RModel.SubmissionApplicationStatus = (master.Status ?? "").Trim();
                    userId = (master.UserID ?? "").Trim();
                    var getUserRepo = UserRepository.CheckUserBBL(master.SubmissionLicense, master.UserID).ToList();
                    var userRepo = getUserRepo.FirstOrDefault();
                    RModel.LicenseDuration = master.LicenseDuration.ToString();
                    RModel.SubmissionApplicationStatus = master.Status;
                    var subCheckList = SubChcekListAppRep.FindByMasterId(RModel.MasterID).ToList().FirstOrDefault();
                    if (subCheckList != null)
                    {
                        RModel.IsEhopAllowed = Convert.ToBoolean(subCheckList.IsSubmissioneHop);
                    }
                    if (getUserRepo.Count() != 0)
                    {
                        if (userRepo.Type.Trim().ToUpper() == "A")
                        {
                            var bblLicense = BblRepository.FindByID(Convert.ToInt32(userRepo.DCBC_ENTITY_ID)).FirstOrDefault();
                            var bbllren = subrnewrepo.FindByID(RModel.MasterID).ToList();
                            if (bbllren.Count() != 0)
                            {
                                RModel.SubNumber = bbllren.FirstOrDefault().SubmissionLicense ?? "";
                            }
                            else if (bblLicense != null)
                            {
                                RModel.SubNumber = bblLicense.B1_ALT_ID;
                            }
                        }
                        else
                        {
                            if (master.SubmissionLicense.Contains("LA") || master.SubmissionLicense.Contains("DA"))
                            {
                                RModel.SubNumber = (master.SubmissionLicense ?? "").Trim();
                            }
                            else
                            {
                                RModel.SubNumber = master.SubmissionLicense;
                            }
                        }
                    }
                    RModel.AmountCharged = master.GrandTotal == null ? 0 : Convert.ToDecimal(master.GrandTotal);
                    RModel.DocType = master.DocSubmType == null ? "" : master.DocSubmType;
                    var paymentdetails = FindBy(x => x.MasterId.Trim() == RModel.MasterID.Trim() && x.PaymentId.Trim() == RModel.PaymentID).ToList();
                    var payment = paymentdetails.FirstOrDefault();
                    if (paymentdetails.Count() != 0)
                    {
                        RModel.PaymentMailId = (payment.PaymentMailAddress ?? "").Trim();
                        RModel.ReceiptDate = Convert.ToDateTime(payment.PaymentDate).ToString("MM/dd/yyyy");
                        RModel.ExceptedFinalCheckingDate = Convert.ToDateTime(payment.PaymentDate).AddDays(30).ToString("MM/dd/yyyy");
                        RModel.TransactionId = payment.TranscationId == null ? "" : payment.TranscationId.Trim();
                        var cardDetails = _payCardResp.FindByID(RModel).ToList();
                        if (cardDetails.Count() != 0)
                        {
                            var card = cardDetails.FirstOrDefault();
                            RModel.CardNumber = "************" + (card.CardNumber == null ? "" : card.CardNumber.Substring(card.CardNumber.Length - 4));
                        }
                    }
                    ServiceChecklist servicelist = new ServiceChecklist();
                    servicelist.MasterId = RModel.MasterID.Trim();
                    RModel.ServiceCheckList = _catRepo.ServiceCheckList(servicelist).DetailedCategoryList;
                    foreach (var item in RModel.ServiceCheckList)
                    {
                        if (RModel.IsBackgroundInvestigation != true)
                        {
                            RModel.IsBackgroundInvestigation = item.IsBackgroundInvestigation;
                        }
                       
                    }
                    var renewaldata = subrnewrepo.FindByID(servicelist.MasterId).ToList();
                    if (renewaldata.Count() != 0)
                    {
                        RModel.ServiceCheckList[0].LicenseDuration = master.LicenseDuration.ToString();
                        var renewal = renewaldata.FirstOrDefault();
                        decimal subrenewtotal = 0;
                        var renewdata =
                            dcrnewinovicerepo.FindAmountByLicense(renewal.SubmissionLicense).ToList();
                        if (renewdata.Count() != 0)
                        {
                            var endoresement =
                                renewdata.Where(x => x.GF_DES.ToUpper().Trim().Contains("ENDORSEMENT")).ToList();
                            if (endoresement.Count() != 0)
                            {
                                RModel.EndorsementFee = Convert.ToDecimal(endoresement.FirstOrDefault().GF_FEE) / Convert.ToInt32(endoresement.FirstOrDefault().GF_UNIT);
                                RModel.ServiceCheckList[0].EndorsementFee = RModel.EndorsementFee;
                            }
                            else
                            {
                                RModel.EndorsementFee = 0;
                            }
                            var application =
                                renewdata.Where(x => x.GF_DES.ToUpper().Trim().Contains("APPLICATION")).ToList();
                            //     Application Fee
                            if (application.Count() != 0)
                            {
                                RModel.ApplicationFee = Convert.ToDecimal(application.FirstOrDefault().GF_FEE);
                                RModel.ServiceCheckList[0].ApplicationFee = RModel.ApplicationFee;
                            }
                            else
                            {
                                RModel.ApplicationFee = 0;
                            }
                            int count = 0;
                            decimal roafee = 0;
                            foreach (var service in RModel.ServiceCheckList)
                            {
                                if (count == 0)
                                {
                                    var datadd = RModel.ServiceCheckList[0].LicenseCategory;

                                    var raofee = renewdata.Where(x => x.GF_DES.ToUpper().Trim().Contains("RAO FEE")).ToList();
                                    roafee = raofee.Count() != 0 ? Convert.ToDecimal(raofee.FirstOrDefault().GF_FEE) : 0;
                                    decimal categoryfee = 0;
                                    var lookupCategoryName = _lookupExistingCategoriesRepo.NewCategoryFindBy(datadd).ToList();
                                  //  bool status = false;
                                    foreach (var category in lookupCategoryName)
                                    {
                                        var catfee = renewdata.Where(x => x.GF_DES.ToUpper().Trim() == category.ExistingCategory.ToUpper().Trim()).ToList();
                                        if (catfee.Count() != 0)
                                        {
                                            categoryfee = Convert.ToDecimal(catfee.FirstOrDefault().GF_FEE) + roafee;
                                            RModel.ServiceCheckList[count].CategoryLicenseFee = Convert.ToDecimal(categoryfee);

                                            //RModel.ServiceCheckList[count].Units =
                                            //    (RModel.ServiceCheckList[count].Units).Replace("1",
                                            //        (catfee.FirstOrDefault().GF_UNIT.ToString()).Replace(".00", ""));
                                            RModel.ServiceCheckList[count].SubTotal = categoryfee + RModel.EndorsementFee + RModel.ApplicationFee;
                                            RModel.ServiceCheckList[count].TechFee = (RModel.ServiceCheckList[0].SubTotal / 100) * 10;

                                            if (renewal.Extradays.ToUpper() == "EXPIRED")
                                            {
                                                RModel.ServiceCheckList[count].ExpiryFee = Convert.ToDecimal(renewal.ExpiredFee);
                                            }
                                            RModel.ServiceCheckList[count].LapsedFee = Convert.ToDecimal(renewal.LapsedFee);
                                            RModel.ServiceCheckList[count].TotalFee = RModel.ServiceCheckList[0].TechFee + RModel.ServiceCheckList[0].SubTotal + RModel.ServiceCheckList[count].LapsedFee + RModel.ServiceCheckList[count].ExpiryFee;
                                           // status = true;
                                        }
                                    }
                                    RModel.CategoryLicenseFee = categoryfee;
                                }
                                else
                                {
                                    RModel.ServiceCheckList[count].LicenseDuration = master.LicenseDuration.ToString();
                                    var datadd = RModel.ServiceCheckList[count].LicenseCategory;
                                    decimal categoryfee = 0;
                                    var lookupCategoryName = _lookupExistingCategoriesRepo.NewCategoryFindBy(datadd).ToList();
//bool status = false;
                                    foreach (var category in lookupCategoryName)
                                    {
                                        var catfee =
                                            renewdata.Where(x => x.GF_DES.ToUpper().Trim() == category.ExistingCategory.ToUpper().Trim())
                                                .ToList();
                                        if (catfee.Count() != 0)
                                        {
                                            categoryfee = Convert.ToDecimal(catfee.FirstOrDefault().GF_FEE);
                                            //    RModel.ServiceCheckList[count].Units = (RModel.ServiceCheckList[count].Units).Replace("1", (catfee.FirstOrDefault().GF_UNIT.ToString()).Replace(".00", ""));
                                            string endoresment = service.Endorsement.Trim();
                                            var endoresemetcheck = RModel.ServiceCheckList.Where(x => x.Endorsement.ToUpper().Trim() == endoresment.ToUpper()).ToList();
                                            if (endoresemetcheck.Count() == 1)
                                            {
                                                RModel.ServiceCheckList[count].EndorsementFee = RModel.EndorsementFee;
                                            }
                                            else
                                            { RModel.ServiceCheckList[count].EndorsementFee = 0; }
                                            RModel.ServiceCheckList[count].CategoryLicenseFee =
                                                Convert.ToDecimal(categoryfee);

                                            RModel.ServiceCheckList[count].SubTotal = categoryfee + RModel.ServiceCheckList[count].EndorsementFee;
                                            RModel.ServiceCheckList[count].TechFee =
                                                (RModel.ServiceCheckList[count].SubTotal / 100) * 10;
                                            RModel.ServiceCheckList[count].TotalFee =
                                                RModel.ServiceCheckList[count].TechFee +
                                                RModel.ServiceCheckList[count].SubTotal;
                                        }
                                    }
                                }
                                count = count + 1;
                            }
                            var subtotal = RModel.CategoryLicenseFee + RModel.EndorsementFee + RModel.ApplicationFee + roafee;
                            RModel.TechFee = (subtotal * 10) / 100;
                            if (renewaldata.FirstOrDefault().Extradays.ToUpper().Trim() == "LAPSED")
                            {
                                RModel.ExtraAmount = (renewaldata.FirstOrDefault().LapsedFee ?? 0) + (renewaldata.FirstOrDefault().ExpiredFee ?? 0);
                            }
                            else if (renewaldata.FirstOrDefault().Extradays.ToUpper().Trim() == "EXPIRED")
                            {
                                RModel.ExtraAmount = (renewaldata.FirstOrDefault().LapsedFee ?? 0) + (renewaldata.FirstOrDefault().ExpiredFee ?? 0);
                            }
                            else
                            {
                                RModel.ExtraAmount = 0;
                            }
                            var bblpenaltyexipred = renewdata.Where(x => x.GF_DES.Trim().ToUpper().Contains("PENALTY FEE (EXPIRED)")).ToList();
                            if (bblpenaltyexipred.Any())
                            {
                                if (bblpenaltyexipred.Count() == 1)
                                {
                                    renewdata.Remove(
                                        renewdata.Where(x => x.GF_DES.Trim().ToUpper().Contains("PENALTY FEE (EXPIRED)")).Single());
                                }
                                else
                                {
                                    foreach (var penality in bblpenaltyexipred)
                                    {
                                        renewdata.Remove(renewdata.Where(x => x.GF_DES.Trim().ToUpper().Contains("PENALTY FEE (EXPIRED)") && x.GF_FEE == penality.GF_FEE).Single());
                                    }
                                }
                            }
                            var bblpenaltylapsed = renewdata.Where(x => x.GF_DES.Trim().ToUpper().Contains("PENALTY FEE (LAPSE)")).ToList();
                            if (bblpenaltylapsed.Any())
                            {
                                if (bblpenaltylapsed.Count() == 1)
                                {
                                    renewdata.Remove(
                                    renewdata.Where(x => x.GF_DES.Trim().ToUpper().Contains("PENALTY FEE (LAPSE)")).Single());
                                }
                                else
                                {
                                    foreach (var penality in bblpenaltylapsed)
                                    {
                                        renewdata.Remove(
                                 renewdata.Where(x => x.GF_DES.Trim().ToUpper().Contains("PENALTY FEE (LAPSE)") && x.GF_FEE == penality.GF_FEE).Single());
                                    }
                                }
                            }
                            foreach (var renewfee in renewdata)
                            {
                                subrenewtotal = subrenewtotal + Convert.ToDecimal(renewfee.GF_FEE);
                            }
                            RModel.TotalFee = subrenewtotal + RModel.ExtraAmount;
                        }
                    }
                    else
                    {
                        RModel.ApplicationFee = servicelist.ApplicationFee;
                        RModel.CategoryLicenseFee = servicelist.CategoryLicenseFee;
                        RModel.EndorsementFee = servicelist.EndorsementFee;
                        RModel.SubTotal = servicelist.ApplicationFee + servicelist.EndorsementFee +
                                          servicelist.CategoryLicenseFee;
                        RModel.TechFee = (servicelist.SubTotal / 100) * 10;
                        RModel.TotalFee = servicelist.TotalFee;
                        RModel.ExtraAmount = servicelist.ExtraAmount;
                        RModel.Extradays = servicelist.Extradays;
                    }
                    var checklist = SubChcekListAppRep.FindByMasterId(servicelist.MasterId).ToList();
                    if (checklist.Count() != 0)
                    {
                        var checklistData = checklist.FirstOrDefault();
                        RModel.IsSubmissionCofo = checklistData.IsSubmissionCofo ?? false;
                        RModel.IsSubmissionHop = checklistData.IsSubmissionHop ?? false;
                        RModel.IsSubmissioneHop = checklistData.IsSubmissioneHop ?? false;
                        if (checklist.FirstOrDefault().IsSubmissioneHop.Value)
                        {
                            RModel.Isehop = true;
                        }
                        else
                        {
                            RModel.Isehop = false;
                        }
                    }
                    else
                    {
                        RModel.Isehop = false;
                    }
                    var getUserDetails = _subMasterResp.FindUserDetails(userId).ToList();
                    if (getUserDetails.Count() != 0)
                        RModel.FullUserName = getUserDetails.FirstOrDefault().FirstName + " " +
                                              getUserDetails.FirstOrDefault().LastName;

                    BblDocuments docs = new BblDocuments();
                    docs.MasterId = RModel.MasterID.Trim();
                    RModel.DocumentList = _docRepo.DocumentByID(RModel.MasterID);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return RModel;
        }
        /// <summary>
        /// This method is used get specific payment transaction details data based on unique id .
        /// </summary>
        /// <param name="masterid"></param>
        /// <returns>Return payment transaction detail data</returns>
        public PaymentTransactionDetails FindAddressByPaymentId(string masterid)
        {
            // var paymentDetails = new List<PaymentTransactionDetails>();
            PaymentTransactionDetails paymentDetails = new PaymentTransactionDetails();

            var payDetails = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == masterid).ToList();
            if (payDetails.Any())
            {
                paymentDetails.PaymentId = (payDetails.FirstOrDefault().PaymentId ?? "").Trim();
                paymentDetails.PaymentTransactionID = (payDetails.FirstOrDefault().TranscationId ?? "").Trim();
                paymentDetails.PaymentTransactionDate = Convert.ToDateTime(payDetails.FirstOrDefault().PaymentDate).ToString("MM/dd/yyyy");
                var paymentAddress = _payAddressResp.FindByPaymentId(paymentDetails.PaymentId);
                paymentDetails.PaymentAddressDetails = paymentAddress.ToList();
            }
            return paymentDetails;
        }
    }
}