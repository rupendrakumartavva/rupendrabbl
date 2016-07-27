using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BusinessCenter.Data.Implementation
{
    public class SubmissionVerficationRepository : GenericRepository<SubmissionMaster>, ISubmissionVerficationRepository
    {
        protected ISubmissionCofoHopeHopRepository _subcorstrucResp;
        protected ISubmissionCorporationRepository _subCorpResp;
        protected ISubmissionTaxRevenueRepository _subTaxResp;
        protected ISubmissionCorporationAgentAddressRepository _corpDetailsResp;
        protected ISubmissionCofoHopeHopAddressRepository _bussinessAdrresRepo;
        protected ISubmissionCategoryRepository _catRepo;
        protected ISubmissionDocumentRepository _docRepo;
        protected ISubmissionMasterApplicationChcekListRepository subcheckrepo;
        protected IFixFeeRepository fixfeeRepo;
        protected IMasterCountryRepository _MasterCountryRepository;
        protected IUserBBLServiceRepository Userbblrepository;
        protected IBblRepository _bblRepository;
        protected IDCBC_ENTITY_BBL_RenewalsRepository _dcbcEntityBblRenewalsRepository;
        protected IDCBC_ENTITY_BBL_Renewal_InvoiceRepository _dcbcEntityBblRenewalInvoiceRepository;
        protected IMasterStateRepository _masterStateRepository;
        protected IStreetTypesRepository _streetTypesRespository;
        protected IPaymentDetailsRepository _paymentDetailsRepository;
        protected ISubmissionMasterRenewalRepository _submissionmasterRenewal;

        public SubmissionVerficationRepository(IUnitOfWork context, ISubmissionCofoHopeHopRepository subcorstrucRespository
            , ISubmissionCorporationRepository subCorpRespository, ISubmissionTaxRevenueRepository subTaxRespository,
            ISubmissionCorporationAgentAddressRepository corpDetailsRepository, ISubmissionCofoHopeHopAddressRepository bussinessAdrresRepository,
            ISubmissionCategoryRepository catRepository, ISubmissionDocumentRepository docRepository,
             ISubmissionMasterApplicationChcekListRepository subcheckreposoitory, IFixFeeRepository fixfeeRepository,
            IMasterCountryRepository masterCountryRepository, IUserBBLServiceRepository userbblrepo, IBblRepository bblRepository,
            IDCBC_ENTITY_BBL_RenewalsRepository dcbcEntityBblRenewalsRepository, IDCBC_ENTITY_BBL_Renewal_InvoiceRepository dcbcEntityBblRenewalInvoiceRepository,
            IMasterStateRepository masterStateRepository, IStreetTypesRepository streetTypesRespository, IPaymentDetailsRepository paymentDetailsRepository,
            ISubmissionMasterRenewalRepository submissionmasterRenewal)
            : base(context)
        {
            _subcorstrucResp = subcorstrucRespository;
            _subCorpResp = subCorpRespository;
            _subTaxResp = subTaxRespository;
            _corpDetailsResp = corpDetailsRepository;
            _bussinessAdrresRepo = bussinessAdrresRepository;
            _catRepo = catRepository;
            _docRepo = docRepository;
            subcheckrepo = subcheckreposoitory;
            fixfeeRepo = fixfeeRepository;
            _MasterCountryRepository = masterCountryRepository;
            Userbblrepository = userbblrepo;
            _bblRepository = bblRepository;
            _dcbcEntityBblRenewalsRepository = dcbcEntityBblRenewalsRepository;
            _dcbcEntityBblRenewalInvoiceRepository = dcbcEntityBblRenewalInvoiceRepository;
            _masterStateRepository = masterStateRepository;
            _streetTypesRespository = streetTypesRespository;
            _paymentDetailsRepository = paymentDetailsRepository;
            _submissionmasterRenewal = submissionmasterRenewal;
        }
        /// <summary>
        /// This method is used to get submission verfication data based on master id
        /// </summary>
        /// <param name="subVerfication"></param>
        /// <returns>Return submission verfication</returns>
        public SubmissionVerfication SubmissionDetails(SubmissionVerfication subVerfication)
        {
            try
            {
                int corporationId = 0;
                string mailType = string.Empty;
                int primisesId = 0;
                string businessName = string.Empty;
                string firstName = string.Empty;
                string middleName = string.Empty;
                string lastName = string.Empty;
                var submissionMasterDetails = FindBy(x => x.MasterId.ToString().Replace(System.Environment.NewLine, "").Trim() == subVerfication.MasterID.Trim()).ToList();
                if (submissionMasterDetails.Count() != 0)
                {
                    var masterDetails = submissionMasterDetails.FirstOrDefault();

                    subVerfication.TradeName = (masterDetails.TradeName ?? "").Trim();
                    mailType = (masterDetails.UserSelectMailAddressType ?? "").Trim();
                    subVerfication.DocType = (masterDetails.DocSubmType ?? "").Trim();
                    subVerfication.GrandTotal = masterDetails.GrandTotal ?? 0;
                    subVerfication.IsCofo = masterDetails.IsCofo ?? false;
                    subVerfication.IsHomeBased = masterDetails.IsHomeBased ?? false;
                    subVerfication.IsRaoFeeAdded = masterDetails.IsRaoFee_Applied ?? false;
                    subVerfication.IsFEIN = GetCleanHands_Fein_Ssn(masterDetails.SubmissionLicense, masterDetails.UserID);
                    subVerfication.LicenseNumber = (masterDetails.SubmissionLicense ?? "").Trim();
                    subVerfication.BusinessOwner = (masterDetails.BusinessName ?? "").Trim();
                }
                var occupancyDetails = _subcorstrucResp.FindByID(subVerfication.MasterID.Trim()).ToList();
                if (occupancyDetails.Count() != 0)
                {
                    var cofodetails = occupancyDetails.FirstOrDefault();
                    primisesId = cofodetails.SubCofoHopEhopId;
                    subVerfication.OccupanyNumber = (cofodetails.Number ?? "").Trim();
                    subVerfication.DateofIssue = cofodetails.DateOfIssuance == null ? "01/01/1900" : Convert.ToDateTime(cofodetails.DateOfIssuance).ToString("MM/dd/yyy");
                }
                var corporationDetails = _subCorpResp.FindById(subVerfication.MasterID.Trim()).ToList();
                if (corporationDetails.Count() != 0)
                {
                    var submissionCorporation = corporationDetails.FirstOrDefault();
                    corporationId = submissionCorporation.SubCorporationRegId;
                    subVerfication.OrgType = (submissionCorporation.BusinessStructure ?? "").Trim();
                    subVerfication.CorpFileNo = (submissionCorporation.FileNumber ?? "").Trim();
                }
                var taxDetails = _subTaxResp.FindByID(subVerfication.MasterID.Trim());
                if (taxDetails.Count() != 0)
                {
                    subVerfication.FienNumber = (taxDetails.FirstOrDefault().TaxRevenueNumber ?? "").Trim();
                }
                var corpAgentDetails = _corpDetailsResp.FindBySubID(corporationId);

                if (corpAgentDetails.Count() != 0)
                {
                    var headQuarterAddress = corpAgentDetails.Where(x => x.AddressType.Replace(System.Environment.NewLine, "").ToString().ToUpper().Trim().Contains("CORPREG"));
                    var hqAddress = headQuarterAddress.FirstOrDefault();
                    if (headQuarterAddress.Count() != 0)
                    {
                        subVerfication.HeadQBName = (hqAddress.BusinessName ?? "").Trim();
                        subVerfication.HeadQName = (hqAddress.FirstName ?? "").Trim() + " " + (hqAddress.MiddelName ?? "").Trim() + " " + (hqAddress.LastName ?? "").Trim();
                        subVerfication.HeadQAddress = (hqAddress.Address1 ?? "").Trim() + " " + (hqAddress.Address2 ?? "").Trim() + " " + (hqAddress.Address3 ?? "").Trim();
                        firstName = (hqAddress.FirstName ?? "").Trim();
                        lastName = (hqAddress.LastName ?? "").Trim();
                        middleName = (hqAddress.MiddelName ?? "").Trim();
                        subVerfication.HeadQFirstName = (hqAddress.FirstName ?? "").Trim();
                        subVerfication.HeadQLastName = (hqAddress.LastName ?? "").Trim();
                        subVerfication.HeadQMiddleName = (hqAddress.MiddelName ?? "").Trim();
                        businessName = (hqAddress.BusinessName ?? "").Trim();
                        subVerfication.HeadQAddressNumber = (hqAddress.Address1 ?? "").Trim();
                        subVerfication.HeadQStreetName = (hqAddress.Address2 ?? "").Trim();
                        subVerfication.HeadQStreetType = GetStreetFullName((hqAddress.Address3 ?? "").Trim());
                        subVerfication.HeadQAQuadrant = (hqAddress.Quadrant ?? "").Trim();
                        subVerfication.HeadQCity = (hqAddress.City ?? "").Trim();
                        subVerfication.HeadQState = GetStateCode((hqAddress.State ?? ""), (hqAddress.Country ?? "").Trim());
                        subVerfication.HeadQCountry = GetCountryFullName((hqAddress.Country ?? "").Trim());
                        subVerfication.HeadQZip = (hqAddress.ZipCode ?? "").Trim();
                        subVerfication.HeadQEmail = (hqAddress.Email ?? "").Trim();
                        subVerfication.HeadQTelePhone = (hqAddress.Telephone ?? "").Trim();
                        //if (mailType.ToUpper().Trim() == "HQ ADDRESS")
                        //{
                        //    SubVerfication.MailingBName = (hqAddress.BusinessName ?? "").Trim();
                        //    SubVerfication.MailingName = (hqAddress.FirstName ?? "").Trim() + " " + (hqAddress.MiddelName ?? "").Trim() + " " +
                        //        (hqAddress.LastName ?? "").Trim();
                        //    SubVerfication.MailingAddress = (hqAddress.Address1 ?? "").Trim() + " " + (hqAddress.Address2 ?? "").Trim() + " " +
                        //      (hqAddress.Address3 ?? "").Trim();
                        //    SubVerfication.MailingFirstName = (hqAddress.FirstName ?? "").Trim();
                        //    SubVerfication.MailingMiddleName = (hqAddress.MiddelName ?? "").Trim();
                        //    SubVerfication.MailingLastName = (hqAddress.LastName ?? "").Trim();
                        //    if (hqAddress.Address1 != string.Empty)
                        //        SubVerfication.MailingStreetName = (hqAddress.Address1 ?? "").Trim();
                        //    SubVerfication.MailingStreetNumber = (hqAddress.Address2 ?? "").Trim();
                        //    SubVerfication.MailingStreetType =GetStreetFullName ((hqAddress.Address3 ?? "").Trim());
                        //    SubVerfication.MailingQuadrant = (hqAddress.Quadrant ?? "").Trim();
                        //    SubVerfication.MailingUnit = (hqAddress.UnitNumber ?? "").Trim();
                        //    SubVerfication.MailingCity = (hqAddress.City ?? "").Trim();
                        //    SubVerfication.MailingState = GetStateCode((hqAddress.State ?? ""), (hqAddress.Country ?? "")).Trim();
                        //    SubVerfication.MailingCountry = GetCountryFullName((hqAddress.Country ?? "").Trim());
                        //    SubVerfication.MailingZip = (hqAddress.ZipCode ?? "").Trim();
                        //    SubVerfication.MailingEmail = (hqAddress.Email ?? "").Trim();
                        //    SubVerfication.MailingTelePhone = (hqAddress.Telephone ?? "").Trim();
                        //}
                    }
                    var corpAgentAddress = corpAgentDetails.Where(x => x.AddressType.Replace(System.Environment.NewLine, "").ToString().ToUpper().Trim().Contains("AGENT"));
                    if (corpAgentAddress.Count() != 0)
                    {
                        var AGENTAddress = corpAgentAddress.FirstOrDefault();
                        subVerfication.AgentBName = (AGENTAddress.BusinessName ?? "").Trim();
                        subVerfication.AgentFirstName = (AGENTAddress.FirstName ?? "").Trim();
                        subVerfication.AgentLastName = (AGENTAddress.LastName ?? "").Trim();
                        subVerfication.AgentMiddleName = (AGENTAddress.MiddelName ?? "").Trim();
                        subVerfication.AgentName = ((AGENTAddress.FirstName ?? "").Trim() + " " + (AGENTAddress.MiddelName ?? "").Trim() + " " +
                            (AGENTAddress.LastName ?? "").Trim()).Trim();
                        subVerfication.AgentAddress = (AGENTAddress.Address1 ?? "").Trim();
                        subVerfication.AgentAddressNumber = (AGENTAddress.AddressNumber ?? "").Trim();
                        subVerfication.AgentStreetName = (AGENTAddress.Address2 ?? "").Trim();
                        subVerfication.AgentStreetType = GetStreetFullName((AGENTAddress.Address3 ?? "").Trim());
                        subVerfication.AgentQuadrant = (AGENTAddress.Quadrant ?? "").Trim();
                        subVerfication.AgentUnit = (AGENTAddress.UnitNumber ?? "").Trim();
                        subVerfication.AgentCity = (AGENTAddress.City ?? "").Trim();
                        subVerfication.AgentState = GetStateCode((AGENTAddress.State ?? ""), (AGENTAddress.Country ?? "").Trim());
                        subVerfication.AgentCountry = GetCountryFullName((AGENTAddress.Country ?? "").Trim());
                        subVerfication.AgentZip = (AGENTAddress.ZipCode ?? "").Trim();
                        subVerfication.AgentEmail = (AGENTAddress.Email ?? "").Trim();
                        subVerfication.AgentTelePhone = (AGENTAddress.Telephone ?? "").Trim();
                    }
                    if (mailType.ToUpper().Trim() == "NEWMAIL")
                    {
                        var mailingAddress = corpAgentDetails.Where(x => x.AddressType.Replace(System.Environment.NewLine, "").ToString().ToUpper().Trim().Contains("NEWMAIL"));
                        if (mailingAddress.Count() != 0)
                        {
                            var mailAddress = mailingAddress.FirstOrDefault();
                            subVerfication.MailingBName = (mailAddress.BusinessName ?? "").Trim();
                            subVerfication.MailingName = (mailAddress.FirstName ?? "").Trim() + " " + (mailAddress.MiddelName ?? "").Trim() + " " +
                                (mailAddress.LastName ?? "").Trim();
                            subVerfication.MailingAddress = (mailAddress.Address1 ?? "").Trim() + " " + (mailAddress.Address2 ?? "").Trim() + " " +
                               (mailAddress.Address3 ?? "").Trim();
                            subVerfication.MailingFirstName = (mailAddress.FirstName ?? "").Trim();
                            subVerfication.MailingMiddleName = (mailAddress.MiddelName ?? "").Trim();
                            subVerfication.MailingLastName = (mailAddress.LastName ?? "").Trim();
                            if (mailAddress.Address1 != string.Empty)
                                subVerfication.MailingStreetName = (mailAddress.Address1 ?? "").Trim();
                            subVerfication.MailingStreetNumber = (mailAddress.Address2 ?? "").Trim();
                            subVerfication.MailingStreetType = (mailAddress.Address3 ?? "").Trim();
                            subVerfication.MailingQuadrant = (mailAddress.Quadrant ?? "").Trim();
                            subVerfication.MailingUnit = (mailAddress.UnitNumber ?? "").Trim();
                            subVerfication.MailingCity = (mailAddress.City ?? "").Trim();
                            subVerfication.MailingState = GetStateCode((mailAddress.State ?? ""), (mailAddress.Country ?? "").Trim());
                            subVerfication.MailingCountry = GetCountryFullName((mailAddress.Country ?? "").Trim());
                            subVerfication.MailingZip = (mailAddress.ZipCode ?? "").Trim();
                            subVerfication.MailingEmail = (mailAddress.Email ?? "").Trim();
                            subVerfication.MailingTelePhone = (mailAddress.Telephone ?? "").Trim();
                        }
                    }
                }

                var primesisDetails = _bussinessAdrresRepo.GetPrimisessAddress(primisesId);
                if (primesisDetails.Count() != 0)
                {
                    var primesesDetails = primesisDetails.FirstOrDefault();
                    subVerfication.PremiseBName = string.Empty;
                    subVerfication.PremiseName = (primesesDetails.Name ?? "").Trim();
                    subVerfication.PremiseAddress = (primesesDetails.AddressNumber ?? "").Trim() + " " + (primesesDetails.AddressNumberSufix ?? "").Trim() + " " + (primesesDetails.StreetName ?? "").Trim() +
                      " " + " " + " " + (primesesDetails.StreetType ?? "").Trim() + " " + (primesesDetails.Quadrant ?? "").Trim();
                    subVerfication.PremiseCity = (primesesDetails.City ?? "").Trim();
                    subVerfication.PremiseState = GetStateCode((primesesDetails.State ?? ""), (primesesDetails.Country ?? "").Trim());
                    subVerfication.PremiseCountry = GetCountryFullName((primesesDetails.Country ?? "").Trim());
                    subVerfication.PremiseZip = (primesesDetails.Zip ?? "").Trim();
                    subVerfication.PremiseEmail = string.Empty;
                    subVerfication.PremiseTelePhone = (primesesDetails.Telephone ?? "").Trim();
                    subVerfication.PremiseQuadrant = (primesesDetails.Quadrant ?? "").Trim();
                    subVerfication.PremiseUnitType = (primesesDetails.UnitType ?? "").Trim();
                    subVerfication.PremiseUnit = (primesesDetails.Unit ?? "").Trim();
                    subVerfication.PremiseAddressNumber = (primesesDetails.AddressNumber ?? "").Trim();
                    subVerfication.PremiseStreetType = GetStreetFullName((primesesDetails.StreetType ?? "").Trim());
                    subVerfication.PremiseStreetName = (primesesDetails.StreetName ?? "").Trim();
                    subVerfication.PremiseAddressNumberSufix = (primesesDetails.AddressNumberSufix ?? "").Trim();
                    subVerfication.PremiseStreetName = (primesesDetails.StreetName ?? "").Trim();
                    subVerfication.PremiseWard = (primesesDetails.Ward ?? "").Trim();
                    subVerfication.PremiseANC = (primesesDetails.Anc ?? "").Trim();
                    subVerfication.PremiseZone = (primesesDetails.Zone ?? "").Trim();
                    subVerfication.PremiseSsl = (primesesDetails.Ssl ?? "").Trim();
                    //if (mailType.ToUpper().Trim() == "PRIMSES ADDRESS")
                    //{
                    //    SubVerfication.MailingBName = businessName;
                    //    SubVerfication.MailingAddress = (primesesDetails.Street ?? "").Trim();
                    //    SubVerfication.MailingFirstName = firstName;
                    //    SubVerfication.MailingMiddleName = middleName;
                    //    SubVerfication.MailingLastName = lastName;
                    //    SubVerfication.MailingName = firstName + " " + middleName + " " + lastName.Replace("  ", " ");
                    //    SubVerfication.MailingStreetNumber = primesesDetails.AddressNumber == null ? "" : primesesDetails.AddressNumber.Trim();
                    //    SubVerfication.MailingStreetName = (primesesDetails.StreetName ?? "").Trim();
                    //    SubVerfication.MailingStreetType =GetStreetFullName ((primesesDetails.StreetType ?? "").Trim());
                    //    SubVerfication.MailingQuadrant = (primesesDetails.Quadrant ?? "").Trim();
                    //    SubVerfication.MailingUnit = (primesesDetails.Unit ?? "").Trim();
                    //    SubVerfication.MailingCity = (primesesDetails.City ?? "").Trim();
                    //    SubVerfication.MailingState = GetStateCode((primesesDetails.State ?? ""), (primesesDetails.Country ?? "").Trim());
                    //    SubVerfication.MailingCountry = GetCountryFullName((primesesDetails.Country ?? "").Trim());
                    //    SubVerfication.MailingZip = (primesesDetails.Zip ?? "").Trim();
                    //    SubVerfication.MailingEmail = string.Empty;
                    //    SubVerfication.MailingTelePhone = (primesesDetails.Telephone ?? "").Trim();
                    //    SubVerfication.MailingQuadrant = (primesesDetails.Quadrant ?? "").Trim();
                    //    SubVerfication.MailingUnitType = (primesesDetails.UnitType ?? "").Trim();
                    //    SubVerfication.MailingUnit = (primesesDetails.Unit ?? "").Trim();
                    //    SubVerfication.MailingAddressNumberSufix = (primesesDetails.AddressNumberSufix ?? "").Trim();
                    //}
                }
                ServiceChecklist servicelist = new ServiceChecklist();
                servicelist.MasterId = subVerfication.MasterID.Trim();
                subVerfication.ServiceCheckList = _catRepo.ServiceCheckList(servicelist).DetailedCategoryList;
                subVerfication.ApplicationFee = servicelist.ApplicationFee;
                subVerfication.CategoryLicenseFee = servicelist.CategoryLicenseFee;
                subVerfication.EndorsementFee = servicelist.EndorsementFee;
                subVerfication.SubTotal = servicelist.ApplicationFee + servicelist.EndorsementFee + servicelist.CategoryLicenseFee;
                subVerfication.TechFee = (servicelist.SubTotal / 100) * 10;
                subVerfication.TotalFee = servicelist.TotalFee;
                var checklist = subcheckrepo.FindByMasterId(servicelist.MasterId).ToList();
                if (checklist.Count() != 0)
                {
                    subVerfication.IsSubmissionCofo = checklist.FirstOrDefault().IsSubmissionCofo != null && Convert.ToBoolean(checklist.FirstOrDefault().IsSubmissionCofo);
                    subVerfication.IsSubmissionHop = checklist.FirstOrDefault().IsSubmissionHop != null && Convert.ToBoolean(checklist.FirstOrDefault().IsSubmissionHop);
                    subVerfication.IsSubmissioneHop = checklist.FirstOrDefault().IsSubmissioneHop != null && Convert.ToBoolean(checklist.FirstOrDefault().IsSubmissioneHop);
                    if (checklist.FirstOrDefault().IsSubmissioneHop.Value)
                    {
                        subVerfication.Isehop = true;
                    }
                    else
                    {
                        subVerfication.Isehop = false;
                    }
                }
                else
                {
                    subVerfication.Isehop = false;
                }
                BblDocuments docs = new BblDocuments();
                docs.MasterId = subVerfication.MasterID.Trim();
                subVerfication.DocumentList = _docRepo.DocumentList(docs).FirstOrDefault().BblServiceDoc.ToList();
            }
            catch (Exception)
            { }
            return subVerfication;
        }
        /// <summary>
        /// This method is used to check existence of the clean hands based on entity id and user id
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private bool GetCleanHands_Fein_Ssn(string entityId, string userId)
        {
            var getValue = Userbblrepository.CheckUserBBL(entityId, userId).ToList();
            if (getValue.Count > 0)
            {
                var getCleanHandsValue = getValue.FirstOrDefault().CleanHandsType_SSN_FEIN;
                return getCleanHandsValue == "FEIN";
            }
            return false;
        }
        /// <summary>
        /// This method is used to get submission verfication data based on master id
        /// </summary>
        /// <param name="subVerfication"></param>
        /// <returns>Return submission verfication</returns>
        public SubmissionVerfication SubmissionPayDetails(SubmissionVerfication subVerfication)
        {
            try
            {
            
                string mailType = string.Empty;
              //  int primisesId = 0;
                //string BusinessName;
                //string FirstName;
                //string MiddleName;
                //string LastName;

                var masterDetails = FindBy(x => x.MasterId.ToString().Replace(System.Environment.NewLine, "").Trim() == subVerfication.MasterID.Trim()).ToList();
                if (masterDetails.Count() != 0)
                {
                    mailType = (masterDetails.FirstOrDefault().UserSelectMailAddressType ?? "").Trim();
                }

                var corporationDetails = _subCorpResp.FindById(subVerfication.MasterID.Trim());
                if (corporationDetails.Count() != 0)
                {
                    int CorptionId = 0;
                    CorptionId = corporationDetails.FirstOrDefault().SubCorporationRegId;
                    var corpAgentDetails = _corpDetailsResp.FindBySubID(CorptionId).ToList();
                    if (corpAgentDetails.Count() != 0)
                    {
                        var headQuarterAddress = corpAgentDetails.Where(x => x.AddressType.Replace(System.Environment.NewLine, "").ToString().ToUpper().Trim().Contains("CORPREG"));
                        if (headQuarterAddress.Count() != 0)
                        {
                           // var hqAddress = headQuarterAddress.FirstOrDefault();
                            //FirstName = hqAddress.FirstName ?? "";
                            //LastName = hqAddress.LastName ?? "";
                            //MiddleName = hqAddress.MiddelName ?? "";
                            //BusinessName = hqAddress.BusinessName ?? "";

                            //if (MailType.ToUpper().Trim() == "HQ ADDRESS")
                            //{
                            //    SubVerfication.MailingBName = hqAddress.BusinessName ?? "";
                            //    SubVerfication.MailingName = (hqAddress.FirstName ?? "") + " " + (hqAddress.MiddelName ?? "") + " " + (hqAddress.LastName ?? "");
                            //    SubVerfication.MailingAddress = (hqAddress.Address1 ?? "") + " " + (hqAddress.Address2 ?? "") + " " + (hqAddress.Address3 ?? "");
                            //    SubVerfication.MailingFirstName = hqAddress.FirstName ?? "";
                            //    SubVerfication.MailingMiddleName = hqAddress.MiddelName ?? "";
                            //    SubVerfication.MailingLastName = hqAddress.LastName ?? "";
                            //    if (hqAddress.Address1 != string.Empty)
                            //        SubVerfication.MailingStreetName = hqAddress.Address1 ?? "";
                            //    SubVerfication.MailingStreetNumber = hqAddress.Address2 ?? "";
                            //    SubVerfication.MailingStreetType =GetStreetFullName( hqAddress.Address3 ?? "".Trim());
                            //    SubVerfication.MailingQuadrant = hqAddress.Quadrant ?? "";
                            //    SubVerfication.MailingUnit = hqAddress.UnitNumber ?? "";
                            //    SubVerfication.MailingCity = hqAddress.City ?? "";
                            //    SubVerfication.MailingState =GetStateCode((hqAddress.State ?? ""),(hqAddress.Country ?? "")).Trim();
                            //    SubVerfication.MailingCountry = GetCountryFullName((hqAddress.Country ?? "").Trim());
                            //    SubVerfication.MailingZip = hqAddress.ZipCode ?? "";
                            //    SubVerfication.MailingEmail = hqAddress.Email ?? "";
                            //    SubVerfication.MailingTelePhone = hqAddress.Telephone ?? "";
                            //}
                            //else
                            if (mailType.ToUpper().Trim() == "NEWMAIL")
                            {
                                var newMailAddress = corpAgentDetails.Where(x => x.AddressType.Replace(System.Environment.NewLine, "").ToString().ToUpper().Trim().Contains("NEWMAIL"));
                                if (newMailAddress.Count() != 0)
                                {
                                    var mailAddress = newMailAddress.FirstOrDefault();
                                    subVerfication.MailingBName = mailAddress.BusinessName ?? "";
                                    subVerfication.MailingName = (mailAddress.FirstName ?? "") + " " + (mailAddress.MiddelName ?? "") + " " + (mailAddress.LastName ?? "");
                                    subVerfication.MailingAddress = (mailAddress.Address1 ?? "") + " " + (mailAddress.Address2 ?? "") + " " + (mailAddress.Address3 ?? "");
                                    subVerfication.MailingFirstName = mailAddress.FirstName ?? "";
                                    subVerfication.MailingMiddleName = mailAddress.MiddelName ?? "";
                                    subVerfication.MailingLastName = mailAddress.LastName ?? "";
                                    if (mailAddress.Address1 != string.Empty)
                                        subVerfication.MailingStreetName = mailAddress.Address1 ?? "";
                                    subVerfication.MailingStreetNumber = mailAddress.Address2 ?? "";
                                    subVerfication.MailingStreetType = mailAddress.Address3 ?? "";
                                    subVerfication.MailingQuadrant = mailAddress.Quadrant ?? "";
                                    subVerfication.MailingUnit = mailAddress.UnitNumber ?? "";
                                    subVerfication.MailingCity = mailAddress.City ?? "";
                                    subVerfication.MailingState = GetStateCode((mailAddress.State ?? ""), (mailAddress.Country ?? "")).Trim();
                                    subVerfication.MailingCountry = GetCountryFullName((mailAddress.Country ?? "").Trim());
                                    subVerfication.MailingZip = mailAddress.ZipCode ?? "";
                                    subVerfication.MailingEmail = mailAddress.Email ?? "";
                                    subVerfication.MailingTelePhone = mailAddress.Telephone ?? "";
                                }
                            }
                            //else if (MailType.ToUpper().Trim() == "PRIMSES ADDRESS")
                            //{
                            //    var occupancyDetails = _subcorstrucResp.FindByID(SubVerfication.MasterID.Trim());
                            //    if (occupancyDetails.Count() != 0)
                            //    {
                            //        primisesId = occupancyDetails.FirstOrDefault().SubCofoHopEhopId;
                            //    }
                            //    var sbPrimsesFullAddress = new StringBuilder();
                            //    var primesesAddressDetails = _bussinessAdrresRepo.GetPrimisessAddress(primisesId);
                            //    if (primesesAddressDetails.Count() != 0)
                            //    {
                            //        var primesesDetails = primesesAddressDetails.FirstOrDefault();
                            //        SubVerfication.MailingBName = BusinessName;
                            //      //  SubVerfication.MailingAddress = (primesesDetails.Street ?? "");
                            //        sbPrimsesFullAddress.Append(primesesDetails.AddressNumber == null
                            //            ? ""
                            //            : primesesDetails.AddressNumber.ToString());
                            //        sbPrimsesFullAddress.Append(" ");
                            //        sbPrimsesFullAddress.Append(primesesDetails.AddressNumberSufix == null
                            //          ? ""
                            //          : primesesDetails.AddressNumberSufix.ToString());
                            //        sbPrimsesFullAddress.Append(" ");
                            //        sbPrimsesFullAddress.Append(primesesDetails.StreetName == null
                            //           ? ""
                            //           : primesesDetails.StreetName.ToString());
                            //        sbPrimsesFullAddress.Append(" ");
                            //        sbPrimsesFullAddress.Append(GetStreetFullName(primesesDetails.StreetType ?? "".Trim()));
                            //        sbPrimsesFullAddress.Append(" ");
                            //        sbPrimsesFullAddress.Append(primesesDetails.Quadrant ?? "");
                            //        sbPrimsesFullAddress.Append(" ");
                            //        SubVerfication.MailingAddress = sbPrimsesFullAddress.ToString().Trim();
                            //       // SubVerfication.MailingAddress = (primesesDetails.Street ?? "");
                            //        SubVerfication.MailingFirstName = FirstName;
                            //        SubVerfication.MailingMiddleName = MiddleName;
                            //        SubVerfication.MailingLastName = LastName;
                            //        SubVerfication.MailingName = FirstName + " " + MiddleName + " " + LastName.Replace("  ", " ");
                            //        SubVerfication.MailingStreetNumber = primesesDetails.AddressNumber == null ? "" : primesesDetails.AddressNumber.ToString().Trim();
                            //        SubVerfication.MailingStreetName = primesesDetails.StreetName ?? "";
                            //        SubVerfication.MailingStreetType =GetStreetFullName (primesesDetails.StreetType ?? "".Trim());
                            //        SubVerfication.MailingQuadrant = primesesDetails.Quadrant ?? "";
                            //        SubVerfication.MailingUnit = primesesDetails.Unit ?? "";
                            //        SubVerfication.MailingCity = primesesDetails.City ?? "";
                            //        SubVerfication.MailingState = GetStateCode((primesesDetails.State ?? ""),(primesesDetails.Country ?? ""));
                            //        SubVerfication.MailingCountry = GetCountryFullName((primesesDetails.Country ?? "").Trim());
                            //        SubVerfication.MailingZip = primesesDetails.Zip ?? "";
                            //        SubVerfication.MailingEmail = string.Empty;
                            //        SubVerfication.MailingTelePhone = primesesDetails.Telephone ?? "";
                            //        SubVerfication.MailingQuadrant = primesesDetails.Quadrant ?? "";
                            //        SubVerfication.MailingUnitType = primesesDetails.UnitType ?? "";
                            //        SubVerfication.MailingUnit = primesesDetails.Unit ?? "";
                            //        SubVerfication.MailingAddressNumberSufix = primesesDetails.AddressNumberSufix ?? "";
                            //    }
                            //}
                        }
                    }
                }
                ServiceChecklist servicelist = new ServiceChecklist();
                servicelist.MasterId = subVerfication.MasterID.Trim();
                subVerfication.ServiceCheckList = _catRepo.ServiceCheckList(servicelist).DetailedCategoryList;
                subVerfication.ApplicationFee = servicelist.ApplicationFee;
                subVerfication.CategoryLicenseFee = servicelist.CategoryLicenseFee;
                subVerfication.EndorsementFee = servicelist.EndorsementFee;
                subVerfication.SubTotal = servicelist.ApplicationFee + servicelist.EndorsementFee + servicelist.CategoryLicenseFee;
                subVerfication.TechFee = (servicelist.SubTotal / 100) * 10;
                subVerfication.TotalFee = servicelist.TotalFee;
            }
            catch (Exception)
            { }
            return subVerfication;
        }
        /// <summary>
        /// This method is used to get basic business license based on master id
        /// </summary>
        /// <param name="basicBusinessLicense"></param>
        /// <returns>Return basic business license</returns>
        public BasicBusinessLicense BusinessReceipt(BasicBusinessLicense basicBusinessLicense)
        {
            try
            {
                bool bblLiveDataFound = false;

                var submissionMaster = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "").Trim() == basicBusinessLicense.MasterId.Trim()).ToList();
                if (submissionMaster.Count != 0)
                {
                    var submissiondetails = submissionMaster.FirstOrDefault();

                    var validateBblData = _bblRepository.FindBy(x => x.B1_ALT_ID.Trim() == submissiondetails.SubmissionLicense.Trim()).ToList();
                    if (validateBblData.Count > 0)
                    {
                        var dcbcEntityBbl = validateBblData.FirstOrDefault();
                        if (submissiondetails != null && (dcbcEntityBbl != null && dcbcEntityBbl.B1_APPL_STATUS.ToUpper() == submissiondetails.Status.ToString().ToUpper()))
                            bblLiveDataFound = true;
                    }

                    SubmissionCategoryList submissionCategoryList = new SubmissionCategoryList();
                    var categoryData = _catRepo.SubmissionCategoryListWithStatus(submissionCategoryList, basicBusinessLicense.MasterId.Trim());
                    SubmissionVerfication submissionVerfication = new SubmissionVerfication();
                    submissionVerfication.MasterID = basicBusinessLicense.MasterId.Trim();
                    var businessDetails = SubmissionDetails(submissionVerfication);
                    var generalBusiness = new GeneralBusiness
                    {
                        MasterId = basicBusinessLicense.MasterId.Trim()
                    };
                    var cofodetails = _subcorstrucResp.GetPrimisessAddress(generalBusiness);

                    basicBusinessLicense.Category = (categoryData.PrimaryCategoryCode ?? "").Trim();
                    basicBusinessLicense.License = (submissiondetails.SubmissionLicense ?? "").Trim();
                    basicBusinessLicense.ApplicationStatus = (submissiondetails.Status ?? "").Trim();
                    if (bblLiveDataFound == true)
                    {
                        var getBblLiveDetails = validateBblData.FirstOrDefault();
                        if (getBblLiveDetails != null)
                        {
                            basicBusinessLicense.LicensePeriod = getBblLiveDetails.Period_Start_Date + " - " +
                                                             getBblLiveDetails.Expiration_Date;

                            #region BillingAddress Section Taken From  BBL ENTITY TABLE

                            basicBusinessLicense.BillingName = (getBblLiveDetails.Contact_FirstName == null ? "" : getBblLiveDetails.Contact_FirstName == "NA" ? "" :
                                                               getBblLiveDetails.Contact_FirstName.Trim()) + " " +
                                                               (getBblLiveDetails.Contact_MiddleName == null ? "" : getBblLiveDetails.Contact_MiddleName == "NA" ? "" :
                                                               getBblLiveDetails.Contact_MiddleName.Trim()) + " " +
                                                              (getBblLiveDetails.Contact_LastName == null ? "" : getBblLiveDetails.Contact_LastName == "NA" ? "" :
                                                             getBblLiveDetails.Contact_LastName.Trim());
                            basicBusinessLicense.BillingCompanyName = (getBblLiveDetails.Contact_Business_Name ?? "").Trim();

                            basicBusinessLicense.BillingAddress1 = (getBblLiveDetails.Billing_Address1 ?? "").Trim();
                            basicBusinessLicense.BillingAddress2 = (getBblLiveDetails.Billing_Address2 ?? "").Trim();
                            basicBusinessLicense.BillingAddress3 = (getBblLiveDetails.Billing_Address3 ?? "").Trim();
                            basicBusinessLicense.BillingAddress = ((getBblLiveDetails.Billing_CITY ?? "").Trim() + " " +
                                                                  (getBblLiveDetails.Billing_STATE ?? "").Trim() + " " +
                                                                  (getBblLiveDetails.Billing_ZIP ?? "").Trim()).Replace("  ", " ");

                            #endregion BillingAddress Section Taken From  BBL ENTITY TABLE

                            #region PremisesAddress Section Taken From  BBL ENTITY TABLE

                            basicBusinessLicense.PremisesName = string.Empty;

                            basicBusinessLicense.PremisesBusinessName = (getBblLiveDetails.OwnrApplicant_BUSINESS_NAME ?? "").Trim();

                            basicBusinessLicense.PremisesAddress1 = (getBblLiveDetails.B1_HSE_NBR_START ==
                                                                null
                                      ? ""
                                      : getBblLiveDetails.B1_HSE_NBR_START.ToString().ToUpper().Trim() ==
                                        "NA"
                                          ? ""
                                          : getBblLiveDetails.B1_HSE_NBR_START.ToString()
                                              .ToUpper()
                                              .Trim()) +
                                                               (getBblLiveDetails.B1_HSE_NBR_END ==
                                                                null
                                                                   ? ""
                                                                   : getBblLiveDetails.B1_HSE_NBR_END
                                                                       .ToString()
                                                                       .ToUpper()
                                                                       .Trim() ==
                                                                     "NA"
                                                                       ? ""
                                                                       : "-" +
                                                                         getBblLiveDetails
                                                                             .B1_HSE_NBR_END.ToString()
                                                                             .ToUpper()
                                                                             .Trim()) +
                                                               (getBblLiveDetails.B1_HSE_FRAC_NBR_START ==
                                                                null
                                                                   ? ""
                                                                   : getBblLiveDetails
                                                                       .B1_HSE_FRAC_NBR_START.ToUpper()
                                                                       .Trim() ==
                                                                     "NA"
                                                                       ? ""
                                                                       : " " +
                                                                         getBblLiveDetails
                                                                             .B1_HSE_FRAC_NBR_START
                                                                             .ToString()
                                                                             .ToUpper()
                                                                             .Trim()) +
                                                                (getBblLiveDetails.B1_UNIT_START == null
                                                                   ? ""
                                                                   : getBblLiveDetails.B1_UNIT_START
                                                                       .ToUpper().Trim() == "NA"
                                                                       ? ""
                                                                       : " " +
                                                                         getBblLiveDetails
                                                                             .B1_UNIT_START.ToUpper()
                                                                             .Trim()) +

                                                               (getBblLiveDetails.B1_STR_NAME == null
                                                                   ? ""
                                                                   : getBblLiveDetails.B1_STR_NAME
                                                                       .ToUpper().Trim() == "NA"
                                                                       ? ""
                                                                       : " " + " " +
                                                                         getBblLiveDetails.B1_STR_NAME
                                                                             .ToUpper()
                                                                             .ToUpper()
                                                                             .Trim()) +
                                                               (getBblLiveDetails.B1_STR_SUFFIX == null
                                                                   ? ""
                                                                   : getBblLiveDetails.B1_STR_SUFFIX
                                                                       .ToUpper().Trim() == "NA"
                                                                       ? ""
                                                                       : " " +
                                                                         getBblLiveDetails
                                                                             .B1_STR_SUFFIX.ToUpper()
                                                                             .Trim()) +
                                                               (getBblLiveDetails.B1_STR_SUFFIX_DIR ==
                                                                null
                                                                   ? ""
                                                                   : getBblLiveDetails
                                                                       .B1_STR_SUFFIX_DIR.ToUpper()
                                                                       .Trim() ==
                                                                     "NA"
                                                                       ? ""
                                                                       : " " +
                                                                         getBblLiveDetails
                                                                             .B1_STR_SUFFIX_DIR
                                                                             .ToUpper().Trim())
                                                                   .Replace("  ", " ")
                                                                   .Replace("  ", " ");

                            basicBusinessLicense.PremisesAddress = ((getBblLiveDetails.B1_SITUS_CITY ?? "").Trim() + " " + (getBblLiveDetails.B1_SITUS_STATE ?? "").Trim()
                                    + " " + (getBblLiveDetails.B1_SITUS_ZIP ?? "").Trim())
                                    .Replace("  ", " ");

                            #endregion PremisesAddress Section Taken From  BBL ENTITY TABLE

                            #region Agent Address Section Taken From  BBL ENTITY TABLE

                            var sbAgentName = new StringBuilder();

                            if (!string.IsNullOrEmpty(getBblLiveDetails.RegAgent_FNAME))
                            {
                                sbAgentName.Append(getBblLiveDetails.RegAgent_FNAME == null
                                    ? ""
                                    : getBblLiveDetails.RegAgent_FNAME == "NA"
                                        ? ""
                                        : getBblLiveDetails.RegAgent_FNAME.Trim());
                                sbAgentName.Append(" ");
                            }

                            if (!string.IsNullOrEmpty(getBblLiveDetails.RegAgent_MNAME))
                            {
                                sbAgentName.Append(getBblLiveDetails.RegAgent_MNAME == null ? "" : getBblLiveDetails.RegAgent_MNAME == "NA" ? "" :
                              getBblLiveDetails.RegAgent_MNAME.Trim());
                                sbAgentName.Append(" ");
                            }
                            if (!string.IsNullOrEmpty(getBblLiveDetails.RegAgent_LNAME))
                            {
                                sbAgentName.Append(getBblLiveDetails.RegAgent_LNAME == null ? "" : getBblLiveDetails.RegAgent_LNAME == "NA" ? "" :
                              getBblLiveDetails.RegAgent_LNAME.Trim());
                                sbAgentName.Append(" ");
                            }
                            basicBusinessLicense.AgentName = sbAgentName.ToString().Trim();
                            //basicBusinessLicense.AgentName = (getBblLiveDetails.RegAgent_FNAME == null ? "" : getBblLiveDetails.RegAgent_FNAME == "NA" ? "" :
                            //  getBblLiveDetails.RegAgent_FNAME.Trim() + "  " +
                            //  getBblLiveDetails.RegAgent_MNAME == null ? "" : getBblLiveDetails.RegAgent_MNAME == "NA" ? "" :
                            //  getBblLiveDetails.RegAgent_MNAME.Trim() + "  " +
                            //  getBblLiveDetails.RegAgent_LNAME == null ? "" : getBblLiveDetails.RegAgent_LNAME == "NA" ? "" :
                            //  getBblLiveDetails.RegAgent_LNAME.Trim()).Replace("  ", " ").Trim();

                            basicBusinessLicense.AgentBusinessName = !string.IsNullOrEmpty(getBblLiveDetails.RegAgent_BUSINESS_NAME) ? (getBblLiveDetails.RegAgent_BUSINESS_NAME ?? "").Trim() : string.Empty;

                            basicBusinessLicense.AgentAddress1 = !string.IsNullOrEmpty(getBblLiveDetails.RegAgent_Address1) ? (getBblLiveDetails.RegAgent_Address1 ?? "").Trim() : string.Empty;
                            basicBusinessLicense.AgentAddress2 = !string.IsNullOrEmpty(getBblLiveDetails.RegAgent_Address2) ? (getBblLiveDetails.RegAgent_Address2 ?? "").Trim() : string.Empty;
                            basicBusinessLicense.AgentAddress3 = !string.IsNullOrEmpty(getBblLiveDetails.RegAgent_Address3) ? (getBblLiveDetails.RegAgent_Address3 ?? "").Trim() : string.Empty;

                            var sbAgentCityStateZip = new StringBuilder();

                            if (!string.IsNullOrEmpty(getBblLiveDetails.RegAgent_CITY))
                            {
                                sbAgentCityStateZip.Append(getBblLiveDetails.RegAgent_CITY == null
                                    ? ""
                                    : getBblLiveDetails.RegAgent_CITY == "NA"
                                        ? ""
                                        : getBblLiveDetails.RegAgent_CITY.Trim());
                                sbAgentCityStateZip.Append(" ");
                            }

                            if (!string.IsNullOrEmpty(getBblLiveDetails.RegAgent_STATE))
                            {
                                sbAgentCityStateZip.Append(getBblLiveDetails.RegAgent_STATE == null
                                    ? ""
                                    : getBblLiveDetails.RegAgent_STATE == "NA"
                                        ? ""
                                        : getBblLiveDetails.RegAgent_STATE.Trim());
                                sbAgentCityStateZip.Append(" ");
                            }

                            if (!string.IsNullOrEmpty(getBblLiveDetails.RegAgent_ZIP))
                            {
                                sbAgentCityStateZip.Append(getBblLiveDetails.RegAgent_ZIP == null
                                    ? ""
                                    : getBblLiveDetails.RegAgent_ZIP == "NA"
                                        ? ""
                                        : getBblLiveDetails.RegAgent_ZIP.Trim());
                                sbAgentCityStateZip.Append(" ");
                            }
                            basicBusinessLicense.AgentAddress = sbAgentCityStateZip.ToString().Trim();
                            //basicBusinessLicense.AgentAddress1 = (getBblLiveDetails.RegAgent_Address1 ?? "").Trim();
                            //basicBusinessLicense.AgentAddress2 = (getBblLiveDetails.RegAgent_Address2 ?? "").Trim();
                            //basicBusinessLicense.AgentAddress3 = (getBblLiveDetails.RegAgent_Address3 ?? "").Trim();

                            //basicBusinessLicense.AgentAddress = ((getBblLiveDetails.RegAgent_CITY ?? "").Trim() + " " +
                            //                                     (getBblLiveDetails.RegAgent_STATE ?? "").Trim() + " " +
                            //                                      (getBblLiveDetails.RegAgent_ZIP ?? "").Trim()).Replace("  ", " ");

                            #endregion Agent Address Section Taken From  BBL ENTITY TABLE

                            basicBusinessLicense.OwnerName = getBblLiveDetails.OwnrApplicant_BUSINESS_NAME == null ? "" : getBblLiveDetails.OwnrApplicant_BUSINESS_NAME == "NA" ? "" :
                            getBblLiveDetails.OwnrApplicant_BUSINESS_NAME.Trim();
                            basicBusinessLicense.CorpName = (getBblLiveDetails.OwnrApplicant_BUSINESS_NAME ?? "").Trim();
                            basicBusinessLicense.TradeName = getBblLiveDetails.Attr_TRADE_NAME == null ? "" : getBblLiveDetails.Attr_TRADE_NAME == "NA" ? "" :
                                getBblLiveDetails.Attr_TRADE_NAME.Trim();
                            basicBusinessLicense.Ssl = (getBblLiveDetails.SSL ?? "").Trim();

                            basicBusinessLicense.DateIssued = string.IsNullOrEmpty(getBblLiveDetails.License_Issued_Date) ? string.Empty : Convert.ToDateTime(getBblLiveDetails.License_Issued_Date).ToString("MM/dd/yyyy");

                            string[] bblEntityTabledata_CategoryFull =
                                getBblLiveDetails.License_Category_Full.Replace("|", ",").Split(',');
                            bblEntityTabledata_CategoryFull = bblEntityTabledata_CategoryFull.Where(c => (c != "")).ToArray();
                            var myList = bblEntityTabledata_CategoryFull.Select(item => item.Split('-')).Select(categorylist => new CategoryDetails
                            {
                                Endoresment = (categorylist[0] ?? "").ToString().Trim(),
                                CategoryName = (categorylist[1] ?? "").ToString().Trim()
                            }).ToList();
                            basicBusinessLicense.CategoryDetailsList = myList;
                            if (!string.IsNullOrEmpty(getBblLiveDetails.WARD))
                            {
                                basicBusinessLicense.Ward = (getBblLiveDetails.WARD ?? "").Trim();
                            }
                            if (!string.IsNullOrEmpty((getBblLiveDetails.ANC)))
                            {
                                basicBusinessLicense.Anc = (getBblLiveDetails.ANC ?? "").Trim();
                            }

                            if (!string.IsNullOrEmpty((getBblLiveDetails.CofO_Number)))
                            {
                                basicBusinessLicense.CofoHopNumber = (getBblLiveDetails.CofO_Number ?? "").Trim();
                            }
                            if (!string.IsNullOrEmpty((getBblLiveDetails.H_O_P_Number)))
                            {
                                basicBusinessLicense.CofoHopNumber = (getBblLiveDetails.H_O_P_Number ?? "").Trim();
                            }
                            if (!string.IsNullOrEmpty((getBblLiveDetails.E_HOP_Number)))
                            {
                                basicBusinessLicense.CofoHopNumber = (getBblLiveDetails.E_HOP_Number ?? "").Trim();
                            }

                            if (!string.IsNullOrEmpty((getBblLiveDetails.ZONE)))
                            {
                                basicBusinessLicense.Zone = (getBblLiveDetails.ZONE ?? "").Trim();
                            }

                            //   basicBusinessLicense.CofoHopNumber = (cofodetails.FileNumber ?? "").Trim();

                            //string liveMultiplelicenses = getBblLiveDetails.License_Category == null
                            //            ? ""
                            //            : getBblLiveDetails.License_Category.Trim() == "NA"
                            //                ? ""
                            //                : " " + getBblLiveDetails.License_Category.Trim().Replace("|", ",");

                            //if (!string.IsNullOrEmpty(liveMultiplelicenses))
                            //{
                            //    if (liveMultiplelicenses.Split(',').Count() > 1)
                            //    {
                            //        servicelist.MultipleLicense = liveMultiplelicenses;
                            //        servicelist.MultipleLicense = liveMultiplelicenses + " ";
                            //        if (servicelist.MultipleLicense.EndsWith(", "))
                            //        {
                            //            servicelist.MultipleLicense = servicelist.MultipleLicense.Substring(0,
                            //                servicelist.MultipleLicense.Length - 2).Replace(",", ", ");
                            //        }
                            //    }
                            //    else
                            //    {
                            //        basicBusinessLicense.PrimaryCategoryName = liveMultiplelicenses.Replace(",", ", ");
                            //    }
                            //}

                            //string liveMultiplelicenses = getBblLiveDetails.License_Category == null
                            //           ? ""
                            //           : getBblLiveDetails.License_Category.Trim() == "NA"
                            //               ? ""
                            //               : " " + getBblLiveDetails.License_Category.Trim().Replace("|", ",");

                            //           if (!string.IsNullOrEmpty(liveMultiplelicenses))
                            //           {
                            //               if (liveMultiplelicenses.Split(',').Count() > 1)
                            //               {
                            //                   basicBusinessLicense.CategoryDetailsList = liveMultiplelicenses + " ";
                            //                   if (basicBusinessLicense.CategoryDetailsList.EndsWith(", "))
                            //                   {
                            //                       basicBusinessLicense.MultipleLicense = servicelist.MultipleLicense.Substring(0,
                            //                           servicelist.MultipleLicense.Length - 2).Replace(",", ", ");
                            //                   }
                            //               }
                            //               else
                            //               {
                            //                   servicelist.LicTypes = liveMultiplelicenses.Replace(",", ", ");
                            //                   servicelist.MultipleLicense = string.Empty;
                            //               }
                        }
                    }
                    else
                    {
                        int licenseperiod = Convert.ToInt32(submissiondetails.LicenseDuration);
                        var currentDate = Convert.ToDateTime(submissiondetails.ExpirationDate);
                        var currentmonth = currentDate.Month;
                        var currentYear = currentDate.Year;
                        var date = currentDate.Date;
                        var startOfMonth = new DateTime(currentYear, currentmonth, 1);
                        var endYear =
                           basicBusinessLicense.LicensePeriod = Convert.ToDateTime(startOfMonth).AddYears(-licenseperiod).AddMonths(1).ToString("MM/dd/yyyy") +
                        " - " + Convert.ToDateTime(currentDate).ToString("MM/dd/yyyy");

                        var getGillingAddress = _subCorpResp.FindById(basicBusinessLicense.MasterId.Trim());
                        if (getGillingAddress != null && getGillingAddress.Any())
                        {
                            var getMailingAddress = getGillingAddress.FirstOrDefault();

                            var billingaddress =
                                _corpDetailsResp.FindByTypewithMasterId(getMailingAddress.SubCorporationRegId, "NEWMAIL");
                            if (billingaddress.Any())
                            {
                                var maillingAddressDetails = billingaddress.FirstOrDefault();
                                basicBusinessLicense.BillingCompanyName = maillingAddressDetails.BusinessName.Trim();
                                basicBusinessLicense.BillingName = (maillingAddressDetails.FirstName == null ? "" : maillingAddressDetails.FirstName == "NA" ? "" :
                                 maillingAddressDetails.FirstName.Trim()) + " " +
                                (maillingAddressDetails.MiddelName == null ? "" : maillingAddressDetails.MiddelName == "NA" ? "" :
                                maillingAddressDetails.MiddelName.Trim()) + " " +
                               (maillingAddressDetails.LastName == null ? "" : maillingAddressDetails.LastName == "NA" ? "" :
                                maillingAddressDetails.LastName.Trim());

                                basicBusinessLicense.BillingAddress1 = (maillingAddressDetails.Address1 ?? "").Trim();
                                basicBusinessLicense.BillingAddress2 = (maillingAddressDetails.Address2 ?? "").Trim();
                                basicBusinessLicense.BillingAddress3 = (maillingAddressDetails.Address3 ?? "").Trim();
                                basicBusinessLicense.BillingAddress = ((maillingAddressDetails.City ?? "").Trim() + " " +
                                                                     GetStateCode((maillingAddressDetails.State ?? "").Trim(), (maillingAddressDetails.Country ?? "").Trim()) + " " +
                                                                     GetCountryFullName((maillingAddressDetails.Country ?? "").Trim()).Replace("United States", "") + " " +
                                                                      (maillingAddressDetails.ZipCode ?? "").Trim()).Replace("  ", " ");
                            }
                        }

                        // var billingaddress =
                        //_paymentDetailsRepository.FindAddressByPaymentId(basicBusinessLicense.MasterId.Trim());

                        // var mailingaddress = billingaddress.PaymentAddressDetails.FirstOrDefault();
                        // basicBusinessLicense.BillingName = (mailingaddress.ContactFirstName == null ? "" : mailingaddress.ContactFirstName == "NA" ? "" :
                        //  mailingaddress.ContactFirstName.Trim()) + " " +
                        // (mailingaddress.ContactMiddleName == null ? "" : mailingaddress.ContactMiddleName == "NA" ? "" :
                        // mailingaddress.ContactMiddleName.Trim()) + " " +
                        //(mailingaddress.ContactLastName == null ? "" : mailingaddress.ContactLastName == "NA" ? "" :
                        // mailingaddress.ContactLastName.Trim());

                        // basicBusinessLicense.BillingCompanyName = (mailingaddress.BusinessName ?? "").Trim();
                        // basicBusinessLicense.BillingAddress1 = (mailingaddress.StreetName ?? "").Trim();
                        // basicBusinessLicense.BillingAddress2 = (mailingaddress.StreetNumber ?? "").Trim();
                        // basicBusinessLicense.BillingAddress3 = GetStreetFullName((mailingaddress.StreetType ?? "").Trim());
                        // basicBusinessLicense.BillingAddress = ((mailingaddress.City ?? "").Trim() + " " +
                        //                                      GetStateCode((mailingaddress.State ?? "").Trim(), (mailingaddress.Country ?? "").Trim()) + " " +
                        //                                      GetCountryFullName((mailingaddress.Country ?? "").Trim()).Replace("United States", "") + " " +
                        //                                       (mailingaddress.Zip ?? "").Trim()).Replace("  ", " ");

                        basicBusinessLicense.PremisesName = (businessDetails.PremiseName ?? "").Trim();
                        basicBusinessLicense.PremisesBusinessName = (businessDetails.PremiseBName ?? "").Trim();
                        basicBusinessLicense.PremisesAddress1 = (businessDetails.PremiseAddress ?? "").Trim();
                        basicBusinessLicense.PremisesAddress = ((businessDetails.PremiseCity ?? "").Trim() + " " + (businessDetails.PremiseState ?? "").Trim()
                            + " " + (businessDetails.PremiseCountry ?? "").Trim().Replace("United States", "") + " " + (businessDetails.PremiseZip ?? "").Trim())
                            .Replace("  ", " ");

                        basicBusinessLicense.AgentName = businessDetails.AgentName == null ? "" : businessDetails.AgentName == "NA" ? "" :
                    businessDetails.AgentName.Trim();
                        basicBusinessLicense.AgentBusinessName = (businessDetails.AgentBName ?? "").Trim();
                        basicBusinessLicense.AgentAddress1 = (businessDetails.AgentAddress ?? "").Trim();
                        basicBusinessLicense.AgentAddress2 = (businessDetails.AgentAddressNumber ?? "").Trim();
                        basicBusinessLicense.AgentAddress3 = (businessDetails.AgentUnit ?? "").Trim();
                        basicBusinessLicense.AgentAddress = ((businessDetails.AgentCity ?? "").Trim() + " " +
                                                             (businessDetails.AgentState ?? "").Trim() + " " +
                                                            (businessDetails.AgentCountry ?? "").Trim().Replace("United States", "") + " " +
                                                              (businessDetails.AgentZip ?? "").Trim()).Replace("  ", " ");
                        basicBusinessLicense.OwnerName = submissiondetails.BusinessName == null ? "" : submissiondetails.BusinessName == "NA" ? "" :
                     submissiondetails.BusinessName.Trim();
                        basicBusinessLicense.CorpName = (submissiondetails.BusinessName ?? "").Trim();
                        basicBusinessLicense.TradeName = submissiondetails.TradeName == null ? "" : submissiondetails.TradeName == "NA" ? "" :
                            submissiondetails.TradeName.Trim();
                        basicBusinessLicense.Ssl = (cofodetails.SSL ?? "").Trim();

                        basicBusinessLicense.DateIssued = string.IsNullOrEmpty(submissiondetails.Updatedate.ToString()) ? string.Empty : Convert.ToDateTime(submissiondetails.Updatedate).ToString("MM/dd/yyyy");

                        basicBusinessLicense.CategoryDetailsList = categoryData.CategoryDetailsList;
                        basicBusinessLicense.PrimaryCategoryName = (categoryData.CategoryName ?? "").Trim();
                        basicBusinessLicense.Ward = (cofodetails.Ward ?? "").Trim();
                        basicBusinessLicense.Anc = (cofodetails.Anc ?? "").Trim();
                        basicBusinessLicense.CofoHopNumber = (cofodetails.FileNumber ?? "").Trim();
                        basicBusinessLicense.Zone = (cofodetails.Zone ?? "").Trim();
                    }

                    basicBusinessLicense.PermNo = string.Empty;
                    basicBusinessLicense.Units = _catRepo.Unitcount(basicBusinessLicense.MasterId.Trim()).ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return basicBusinessLicense;
        }
        /// <summary>
        /// This method is used to get renew basic business licnse based on master id
        /// </summary>
        /// <param name="renewBasicBusinessLicense"></param>
        /// <returns>Return renew basic business license</returns>
        public RenewBasicBusinessLicense RenewBusinessReceipt(RenewBasicBusinessLicense renewBasicBusinessLicense)
        {
            string licensenumber = string.Empty;
            string userid = string.Empty;
            bool bblLiveDataFound = false;
            var submissionMaster = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "").Trim() == renewBasicBusinessLicense.MasterId.Trim()).ToList();
            if (submissionMaster.Count != 0)
            {
                var submissiondetails = submissionMaster.FirstOrDefault();
                licensenumber = submissiondetails.SubmissionLicense;
                userid = submissiondetails.UserID;
                int licenseperiod = Convert.ToInt32(submissiondetails.LicenseDuration);

                var currentDate = Convert.ToDateTime(submissiondetails.ExpirationDate);
                var startdate = Convert.ToDateTime(currentDate).AddYears(-licenseperiod).AddDays(1);
                var checkleapyear = DateTime.IsLeapYear(startdate.Year);
                if (checkleapyear)
                {
                    if (startdate.Month.ToString() == "2")
                    {
                        if (startdate.Day.ToString() == "29")
                        {
                            startdate = startdate.AddDays(1);
                        }
                    }
                }

                if (submissiondetails.Status.ToUpper() ==
                    GenericEnums.ApplicationValidateStatus.Active.ToString().ToUpper())
                {
                    bblLiveDataFound = true;
                }
                else if (submissiondetails.Status.ToUpper() == "RENEW")
                {
                    bblLiveDataFound = true;
                }
            }
            else
            {
                var userbbldata = Userbblrepository.FindByRenewEntityId(renewBasicBusinessLicense.MasterId).ToList();
                if (userbbldata.Any())
                {
                    licensenumber = userbbldata.FirstOrDefault().SubmissionLicense;
                    userid = userbbldata.FirstOrDefault().UserID;
                    bblLiveDataFound = true;
                }
            }
            var userdetails = Userbblrepository.CheckUserBBL(licensenumber, userid).ToList();
            SubmissionCategoryList submissionCategoryList = new SubmissionCategoryList();

            var bblrepositorydata = _bblRepository.FindByID(Convert.ToInt32(userdetails.FirstOrDefault().DCBC_ENTITY_ID));
            var bbl_EntityData = bblrepositorydata.FirstOrDefault();
            var bblrenewdata = _dcbcEntityBblRenewalsRepository.FindBybblRenewBasedonLicensenumber(userdetails.FirstOrDefault().B1_ALT_ID,
                userdetails.FirstOrDefault().SubmissionLicense);
            var renewaldata = bblrenewdata.FirstOrDefault();
            var renewinvoice = _dcbcEntityBblRenewalInvoiceRepository.FindAmountByLicense(renewaldata.b1_Alt_ID ?? "").ToList();

            //renewBasicBusinessLicense.DateIssued = DateTime.Now.ToString("MM/dd/yyyy");

            //   renewBasicBusinessLicense.DateIssued = !string.IsNullOrEmpty(bblrepositorydata.FirstOrDefault().B1_APPL_STATUS_DATE) ? bblrepositorydata.FirstOrDefault().B1_APPL_STATUS_DATE : DateTime.Now.ToString("MM/dd/yyy");
            renewBasicBusinessLicense.LicenseNumber = (renewaldata.License_Being_Renewed ?? "").Trim();

            renewBasicBusinessLicense.License = (renewaldata.b1_Alt_ID ?? "").Trim();

            var sbBillingName = new StringBuilder();
            var sbPremisesAddress = new StringBuilder();
            var sbAgentAddress = new StringBuilder();
            var sbOwnerApplicantName = new StringBuilder();
            var billingAddress = new StringBuilder();
            // var startdate = Convert.ToDateTime(currentDate).AddYears(-licenseperiod);

            if (bblLiveDataFound)
            {
                renewBasicBusinessLicense.LicensePeriod = Convert.ToDateTime(bbl_EntityData.Period_Start_Date).ToString("MM/dd/yyyy") + " - " +
                                                       Convert.ToDateTime(bbl_EntityData.Expiration_Date).ToString("MM/dd/yyyy");
                renewBasicBusinessLicense.DateIssued = !string.IsNullOrEmpty(bbl_EntityData.License_Issued_Date) ? bbl_EntityData.License_Issued_Date : DateTime.Now.ToString("MM/dd/yyy");

                #region BillingAddress Section Taken From  BBL ENTITY TABLE

                renewBasicBusinessLicense.BillingName = ((bbl_EntityData.Contact_FirstName == null ? "" : bbl_EntityData.Contact_FirstName == "NA" ? "" :
                                                   bbl_EntityData.Contact_FirstName.Trim()) + " " +
                                                   (bbl_EntityData.Contact_MiddleName == null ? "" : bbl_EntityData.Contact_MiddleName == "NA" ? "" :
                                                   bbl_EntityData.Contact_MiddleName.Trim()) + " " +
                                                  (bbl_EntityData.Contact_LastName == null ? "" : bbl_EntityData.Contact_LastName == "NA" ? "" :
                                                 bbl_EntityData.Contact_LastName.Trim())).Trim();
                renewBasicBusinessLicense.BillingCompanyName = (bbl_EntityData.Contact_Business_Name ?? "").Trim();

                renewBasicBusinessLicense.BillingAddress1 = (bbl_EntityData.Billing_Address1 ?? "").Trim();
                renewBasicBusinessLicense.BillingAddress2 = (bbl_EntityData.Billing_Address2 ?? "").Trim();
                renewBasicBusinessLicense.BillingAddress3 = (bbl_EntityData.Billing_Address3 ?? "").Trim();

                renewBasicBusinessLicense.BillingAddress = ((bbl_EntityData.Billing_CITY ?? "").Trim() + " " +
                                                      (bbl_EntityData.Billing_STATE ?? "").Trim() + " " +
                                                      (bbl_EntityData.Billing_ZIP ?? "").Trim()).Replace("  ", " ");

                #endregion BillingAddress Section Taken From  BBL ENTITY TABLE

                #region PremisesAddress Section Taken From  BBL ENTITY TABLE

                renewBasicBusinessLicense.PremisesName = string.Empty;

                renewBasicBusinessLicense.PremisesBusinessName = (bbl_EntityData.OwnrApplicant_BUSINESS_NAME ?? "").Trim();

                renewBasicBusinessLicense.PremisesAddress1 = (bbl_EntityData.B1_HSE_NBR_START ==
                                                    null
                          ? ""
                          : bbl_EntityData.B1_HSE_NBR_START.ToString().ToUpper().Trim() ==
                            "NA"
                              ? ""
                              : bbl_EntityData.B1_HSE_NBR_START.ToString()
                                  .ToUpper()
                                  .Trim()) +
                                                   (bbl_EntityData.B1_HSE_NBR_END ==
                                                    null
                                                       ? ""
                                                       : bbl_EntityData.B1_HSE_NBR_END
                                                           .ToString()
                                                           .ToUpper()
                                                           .Trim() ==
                                                         "NA"
                                                           ? ""
                                                           : "-" +
                                                             bbl_EntityData
                                                                 .B1_HSE_NBR_END.ToString()
                                                                 .ToUpper()
                                                                 .Trim()) +
                                                   (bbl_EntityData.B1_HSE_FRAC_NBR_START ==
                                                    null
                                                       ? ""
                                                       : bbl_EntityData
                                                           .B1_HSE_FRAC_NBR_START.ToUpper()
                                                           .Trim() ==
                                                         "NA"
                                                           ? ""
                                                           : " " +
                                                             bbl_EntityData
                                                                 .B1_HSE_FRAC_NBR_START
                                                                 .ToString()
                                                                 .ToUpper()
                                                                 .Trim()) +
                                                    (bbl_EntityData.B1_UNIT_START == null
                                                       ? ""
                                                       : bbl_EntityData.B1_UNIT_START
                                                           .ToUpper().Trim() == "NA"
                                                           ? ""
                                                           : " " +
                                                             bbl_EntityData
                                                                 .B1_UNIT_START.ToUpper()
                                                                 .Trim()) +

                                                   (bbl_EntityData.B1_STR_NAME == null
                                                       ? ""
                                                       : bbl_EntityData.B1_STR_NAME
                                                           .ToUpper().Trim() == "NA"
                                                           ? ""
                                                           : " " + " " +
                                                             bbl_EntityData.B1_STR_NAME
                                                                 .ToUpper()
                                                                 .ToUpper()
                                                                 .Trim()) +
                                                   (bbl_EntityData.B1_STR_SUFFIX == null
                                                       ? ""
                                                       : bbl_EntityData.B1_STR_SUFFIX
                                                           .ToUpper().Trim() == "NA"
                                                           ? ""
                                                           : " " +
                                                             bbl_EntityData
                                                                 .B1_STR_SUFFIX.ToUpper()
                                                                 .Trim()) +
                                                   (bbl_EntityData.B1_STR_SUFFIX_DIR ==
                                                    null
                                                       ? ""
                                                       : bbl_EntityData
                                                           .B1_STR_SUFFIX_DIR.ToUpper()
                                                           .Trim() ==
                                                         "NA"
                                                           ? ""
                                                           : " " +
                                                             bbl_EntityData
                                                                 .B1_STR_SUFFIX_DIR
                                                                 .ToUpper().Trim())
                                                       .Replace("  ", " ")
                                                       .Replace("  ", " ");

                renewBasicBusinessLicense.PremisesAddress = ((bbl_EntityData.B1_SITUS_CITY ?? "").Trim() + " " + (bbl_EntityData.B1_SITUS_STATE ?? "").Trim()
                        + " " + (bbl_EntityData.B1_SITUS_ZIP ?? "").Trim())
                        .Replace("  ", " ");

                #endregion PremisesAddress Section Taken From  BBL ENTITY TABLE

                #region Agent Address Section Taken From  BBL ENTITY TABLE

                StringBuilder sbAgentName = new StringBuilder();

                if (!string.IsNullOrEmpty(bbl_EntityData.RegAgent_FNAME))
                {
                    sbAgentName.Append(bbl_EntityData.RegAgent_FNAME == null ? "" : bbl_EntityData.RegAgent_FNAME == "NA" ? "" :
                       bbl_EntityData.RegAgent_FNAME.Trim());
                    sbAgentName.Append("  ");
                }

                if (!string.IsNullOrEmpty(bbl_EntityData.RegAgent_MNAME))
                {
                    sbAgentName.Append(bbl_EntityData.RegAgent_MNAME == null ? "" : bbl_EntityData.RegAgent_MNAME == "NA" ? "" :
                       bbl_EntityData.RegAgent_MNAME.Trim());
                    sbAgentName.Append("  ");
                }

                if (!string.IsNullOrEmpty(bbl_EntityData.RegAgent_LNAME))
                {
                    sbAgentName.Append(bbl_EntityData.RegAgent_LNAME == null ? "" : bbl_EntityData.RegAgent_LNAME == "NA" ? "" :
                       bbl_EntityData.RegAgent_LNAME.Trim());
                    sbAgentName.Append(" ");
                }
                renewBasicBusinessLicense.AgentName = sbAgentName.ToString().Trim();

                //renewBasicBusinessLicense.AgentName = (bbl_EntityData.RegAgent_FNAME == null ? "" : bbl_EntityData.RegAgent_FNAME == "NA" ? "" :
                //  bbl_EntityData.RegAgent_FNAME.Trim() + "  " +
                //  bbl_EntityData.RegAgent_MNAME == null ? "" : bbl_EntityData.RegAgent_MNAME == "NA" ? "" :
                //  bbl_EntityData.RegAgent_MNAME.Trim() + "  " +
                //  bbl_EntityData.RegAgent_LNAME == null ? "" : bbl_EntityData.RegAgent_LNAME == "NA" ? "" :
                //  bbl_EntityData.RegAgent_LNAME.Trim()).Replace("  ", " ").Trim();

                renewBasicBusinessLicense.AgentBusinessName = (bbl_EntityData.RegAgent_BUSINESS_NAME ?? "").Trim();
                renewBasicBusinessLicense.AgentAddress1 = (bbl_EntityData.RegAgent_Address1 ?? "").Trim();
                renewBasicBusinessLicense.AgentAddress2 = (bbl_EntityData.RegAgent_Address2 ?? "").Trim();
                renewBasicBusinessLicense.AgentAddress3 = (bbl_EntityData.RegAgent_Address3 ?? "").Trim();
                renewBasicBusinessLicense.AgentAddress = ((bbl_EntityData.RegAgent_CITY ?? "").Trim() + " " +
                                                     (bbl_EntityData.RegAgent_STATE ?? "").Trim() + " " +
                                                      (bbl_EntityData.RegAgent_ZIP ?? "").Trim()).Replace("  ", " ");

                #endregion Agent Address Section Taken From  BBL ENTITY TABLE

                renewBasicBusinessLicense.OwnerName = bbl_EntityData.OwnrApplicant_BUSINESS_NAME == null
                  ? ""
                  : bbl_EntityData.OwnrApplicant_BUSINESS_NAME == "NA"
                      ? ""
                      : bbl_EntityData.OwnrApplicant_BUSINESS_NAME.Trim();

                renewBasicBusinessLicense.CorpName = (bbl_EntityData.OwnrApplicant_BUSINESS_NAME ?? "").Trim();
                renewBasicBusinessLicense.TradeName = bbl_EntityData.Attr_TRADE_NAME == null ? ""
                    : bbl_EntityData.Attr_TRADE_NAME == "NA" ? "" :
                        bbl_EntityData.Attr_TRADE_NAME.Trim();
                //  string[] bblEntityTabledata_CategoryFull1 =
                //bbl_EntityData.License_Category_Full.Replace("|", ",").Split(',');

                //  bblEntityTabledata_CategoryFull1 = bblEntityTabledata_CategoryFull1.Where(c => (c != "")).ToArray();

                //var myList1 = bblEntityTabledata_CategoryFull1.Select(item => item.Split('-')).Select(categorylist => new CategoryDetails
                //{
                //    Endoresment = (categorylist[0] ?? "").ToString().Trim(),
                //    CategoryName = (categorylist[1] ?? "").ToString().Trim()
                //}).ToList();
                //renewBasicBusinessLicense.CategoryDetailsList = myList1;
                // var categoryData = _catRepo.SubmissionCategoryListWithStatus(submissionCategoryList, renewBasicBusinessLicense.MasterId.Trim());
                //renewBasicBusinessLicense.Category = (categoryData.PrimaryCategoryCode ?? "").Trim();
                //     renewBasicBusinessLicense.CategoryDetailsList = categoryData.CategoryDetailsList;
            }
            else
            {
                // renewBasicBusinessLicense.LicensePeriod = startdate.ToString("MM/dd/yyyy") +
                //" - " + Convert.ToDateTime(currentDate).ToString("MM/dd/yyyy");
                renewBasicBusinessLicense.LicensePeriod = Convert.ToDateTime(bbl_EntityData.Period_Start_Date).ToString("MM/dd/yyyy") + " - " +
                                                      Convert.ToDateTime(bbl_EntityData.Expiration_Date).ToString("MM/dd/yyyy");

                renewBasicBusinessLicense.DateIssued = !string.IsNullOrEmpty(bbl_EntityData.License_Issued_Date) ? bbl_EntityData.License_Issued_Date : DateTime.Now.ToString("MM/dd/yyy");

                #region Billing Address On Active Licences With Respective On Submission

                var billingaddress =
                _paymentDetailsRepository.FindAddressByPaymentId(renewBasicBusinessLicense.MasterId.Trim());

                var mailingaddress = billingaddress.PaymentAddressDetails.FirstOrDefault();

                renewBasicBusinessLicense.ABL = string.Empty;

                sbBillingName.Append(mailingaddress.ContactFirstName == null ? "" : mailingaddress.ContactFirstName == "NA" ? "" :
                        mailingaddress.ContactFirstName.Trim());
                sbBillingName.Append(" ");
                sbBillingName.Append(mailingaddress.ContactMiddleName == null ? "" : mailingaddress.ContactMiddleName == "NA" ? "" :
                        mailingaddress.ContactMiddleName.Trim());
                sbBillingName.Append(" ");
                sbBillingName.Append(mailingaddress.ContactLastName == null ? "" : mailingaddress.ContactLastName == "NA" ? "" :
                        mailingaddress.ContactLastName.Trim());

                renewBasicBusinessLicense.BillingName = sbBillingName.ToString().Trim();

                if (string.IsNullOrEmpty(mailingaddress.StreetName))
                {
                    renewBasicBusinessLicense.BillingAddress1 = (mailingaddress.FullAddress ?? "").Trim();
                    renewBasicBusinessLicense.BillingAddress2 = string.Empty;
                    renewBasicBusinessLicense.BillingAddress3 = string.Empty;
                }
                else
                {
                    renewBasicBusinessLicense.BillingAddress1 = (mailingaddress.StreetName ?? "").Trim();
                    renewBasicBusinessLicense.BillingAddress2 = (mailingaddress.StreetNumber ?? "").Trim();
                    renewBasicBusinessLicense.BillingAddress3 = GetStreetFullName((mailingaddress.StreetType ?? "").Trim()) + " " + (mailingaddress.Quadrant ?? "").Trim()
                        + " " + (mailingaddress.UnitNumber ?? "").Trim();
                }

                renewBasicBusinessLicense.BillingCompanyName = (mailingaddress.BusinessName ?? "").Trim();

                billingAddress.Append((mailingaddress.City ?? "").Trim());
                billingAddress.Append(" ");
                billingAddress.Append(GetStateCode((mailingaddress.State ?? "").Trim(), (mailingaddress.Country ?? "").Trim()));
                billingAddress.Append(" ");
                string country = GetCountryFullName((mailingaddress.Country ?? "").Trim());
                if (country.ToUpper() != "UNITED STATES")
                {
                    billingAddress.Append(country.Trim());
                    billingAddress.Append(" ");
                }
                billingAddress.Append((mailingaddress.Zip ?? "").Trim());
                renewBasicBusinessLicense.BillingAddress = billingAddress.ToString();

                #endregion Billing Address On Active Licences With Respective On Submission

                #region Premises Address On Active Licences With Respective On Submission

                renewBasicBusinessLicense.PremisesName = string.Empty;

                renewBasicBusinessLicense.PremisesAddress1 = (renewaldata.B1_HSE_NBR_START.ToString() ?? "").Trim() + " " + (renewaldata.B1_HSE_FRAC_NBR_START ?? "").Trim()
                    + " " + (renewaldata.B1_UNIT_START ?? "").Trim() + " " + (renewaldata.B1_STR_NAME ?? "").Trim() + " " + (renewaldata.B1_STR_SUFFIX ?? "").Trim() + " " +
                                                            (renewaldata.B1_STR_SUFFIX_DIR ?? "").Trim();

                sbPremisesAddress.Append(renewaldata.B1_SITUS_CITY ?? "");
                sbPremisesAddress.Append(" ");
                sbPremisesAddress.Append(renewaldata.B1_SITUS_STATE ?? "");
                sbPremisesAddress.Append(" ");
                sbPremisesAddress.Append(renewaldata.B1_SITUS_ZIP ?? "");
                renewBasicBusinessLicense.PremisesAddress = sbPremisesAddress.ToString();

                #endregion Premises Address On Active Licences With Respective On Submission

                #region Agent Address On Active Licences with Respective of Submission Master table

                StringBuilder sbAgentName = new StringBuilder();
                if (!string.IsNullOrEmpty(renewaldata.RegAgent_FNAME))
                {
                    sbAgentName.Append(renewaldata.RegAgent_FNAME == null ? "" : renewaldata.RegAgent_FNAME == "NA" ? "" :
                       renewaldata.RegAgent_FNAME.Trim());
                    sbAgentName.Append("  ");
                }

                if (!string.IsNullOrEmpty(renewaldata.RegAgent_MNAME))
                {
                    sbAgentName.Append(renewaldata.RegAgent_MNAME == null ? "" : renewaldata.RegAgent_MNAME == "NA" ? "" :
                       renewaldata.RegAgent_MNAME.Trim());
                    sbAgentName.Append("  ");
                }

                if (!string.IsNullOrEmpty(renewaldata.RegAgent_LNAME))
                {
                    sbAgentName.Append(renewaldata.RegAgent_LNAME == null ? "" : renewaldata.RegAgent_LNAME == "NA" ? "" :
                       renewaldata.RegAgent_LNAME.Trim());
                    sbAgentName.Append(" ");
                }
                renewBasicBusinessLicense.AgentName = sbAgentName.ToString().Trim();

                renewBasicBusinessLicense.AgentBusinessName = !string.IsNullOrEmpty(renewaldata.RegAgent_BUSINESS_NAME) ? (renewaldata.RegAgent_BUSINESS_NAME.ToString() ?? "").Trim() : string.Empty;

                renewBasicBusinessLicense.AgentAddress1 = (renewaldata.RegAgent_Address1 ?? "").Trim();

                renewBasicBusinessLicense.AgentAddress2 = (renewaldata.RegAgent_Address2 ?? "").Trim();

                renewBasicBusinessLicense.AgentAddress3 = (renewaldata.RegAgent_Address3 ?? "").Trim();

                sbAgentAddress.Append(renewaldata.RegAgent_CITY ?? "");
                sbAgentAddress.Append(" ");
                sbAgentAddress.Append(renewaldata.RegAgent_STATE ?? "");
                sbAgentAddress.Append(" ");
                sbAgentAddress.Append(renewaldata.RegAgent_ZIP ?? "");
                sbAgentAddress.Append(" ");
                renewBasicBusinessLicense.AgentAddress = sbAgentAddress.ToString().Trim();

                #endregion Agent Address On Active Licences with Respective of Submission Master table

                //sbOwnerApplicantName.Append(renewaldata.OwnrApplicant_FNAME == null ? "" : renewaldata.OwnrApplicant_FNAME == "NA" ? "" :
                //renewaldata.OwnrApplicant_FNAME.Trim());
                //sbOwnerApplicantName.Append(" ");
                //sbOwnerApplicantName.Append(renewaldata.OwnrApplicant_MNAME == null ? "" : renewaldata.OwnrApplicant_MNAME == "NA" ? "" :
                //        renewaldata.OwnrApplicant_MNAME.Trim());
                //sbOwnerApplicantName.Append(" ");
                //sbOwnerApplicantName.Append(renewaldata.OwnrApplicant_LNAME == null ? "" : renewaldata.OwnrApplicant_LNAME == "NA" ? "" :
                //        renewaldata.OwnrApplicant_LNAME.Trim());
                //sbOwnerApplicantName.Append(" ");

                // renewBasicBusinessLicense.OwnerName = sbOwnerApplicantName.ToString().Trim();
                renewBasicBusinessLicense.OwnerName = renewaldata.OwnrApplicant_BUSINESS_NAME == null
                    ? ""
                    : renewaldata.OwnrApplicant_BUSINESS_NAME == "NA"
                        ? ""
                        : renewaldata.OwnrApplicant_BUSINESS_NAME.Trim();
                renewBasicBusinessLicense.CorpName = (renewaldata.OwnrApplicant_BUSINESS_NAME ?? "").Trim();
                renewBasicBusinessLicense.TradeName = renewaldata.Attr_TRADE_NAME == null ? ""
                    : renewaldata.Attr_TRADE_NAME == "NA" ? "" :
                        renewaldata.Attr_TRADE_NAME.Trim();
            }

            // ----------------------------------------------------------------------------------------

            //----------------------------------ALL THE FINE--------------------
            //renewBasicBusinessLicense.CofoHopNumber = (renewaldata.CofO_Number ?? "").Trim();
            //renewBasicBusinessLicense.CofoHopNumber = string.Empty;
            renewBasicBusinessLicense.Ssl = (bblrepositorydata.FirstOrDefault().SSL ?? "").Trim();

            if (!string.IsNullOrEmpty(bbl_EntityData.WARD))
            {
                renewBasicBusinessLicense.Ward = (bbl_EntityData.WARD ?? "").Trim();
            }
            if (!string.IsNullOrEmpty((bbl_EntityData.ANC)))
            {
                renewBasicBusinessLicense.Anc = (bbl_EntityData.ANC ?? "").Trim();
            }
            if (!string.IsNullOrEmpty((bbl_EntityData.CofO_Number)))
            {
                renewBasicBusinessLicense.CofoHopNumber = (bbl_EntityData.CofO_Number ?? "").Trim();
            }
            if (!string.IsNullOrEmpty((bbl_EntityData.H_O_P_Number)))
            {
                renewBasicBusinessLicense.CofoHopNumber = (bbl_EntityData.H_O_P_Number ?? "").Trim();
            }
            if (!string.IsNullOrEmpty((bbl_EntityData.E_HOP_Number)))
            {
                renewBasicBusinessLicense.CofoHopNumber = (bbl_EntityData.E_HOP_Number ?? "").Trim();
            }

            if (!string.IsNullOrEmpty((bbl_EntityData.ZONE)))
            {
                renewBasicBusinessLicense.Zone = (bbl_EntityData.ZONE ?? "").Trim();
            }

            renewBasicBusinessLicense.PermNo = string.Empty;

            string[] bblEntityTabledata_CategoryFull =
                bbl_EntityData.License_Category_Full.Replace("|", ",").Split(',');

            bblEntityTabledata_CategoryFull = bblEntityTabledata_CategoryFull.Where(c => (c != "")).ToArray();

            //    var h = bblEntityTabledata_CategoryFull;

            var myList = bblEntityTabledata_CategoryFull.Select(item => item.Split('-')).Select(categorylist => new CategoryDetails
            {
                Endoresment = (categorylist[0] ?? "").ToString().Trim(),
                CategoryName = (categorylist[1] ?? "").ToString().Trim()
            }).ToList();
            renewBasicBusinessLicense.CategoryDetailsList = myList;
            var categoryData = _catRepo.SubmissionCategoryListWithStatus(submissionCategoryList, renewBasicBusinessLicense.MasterId.Trim());

            if (categoryData.CategoryDetailsList.Count != 0)
            {
                renewBasicBusinessLicense.Category = (categoryData.PrimaryCategoryCode ?? "").Trim();
                renewBasicBusinessLicense.PrimaryCategoryName = (categoryData.CategoryName ?? "").Trim();
            }
            else
            {
                renewBasicBusinessLicense.PrimaryCategoryName = (myList[0].CategoryName.ToString() ?? "").Trim();
                renewBasicBusinessLicense.Category = _catRepo.CategoryCode(renewBasicBusinessLicense.PrimaryCategoryName);
            }
            //     renewBasicBusinessLicense.CategoryDetailsList = categoryData.CategoryDetailsList;

            decimal raofee = 0;
            decimal applicationfee = 0;
            decimal endoresemetfee = 0;
            decimal penaltyfee = 0;
            decimal enhancedfee = 0;
            int units = 0;
            var bblpenaltyexipred = renewinvoice.Where(x => x.GF_DES.Trim().ToUpper().Contains("PENALTY FEE (EXPIRED)")).ToList();
            if (bblpenaltyexipred.Any())
            {
                if (bblpenaltyexipred.Count() == 1)
                {
                    renewinvoice.Remove(renewinvoice.Where(x => x.GF_DES.Trim().ToUpper().Contains("PENALTY FEE (EXPIRED)")).Single());
                }
                else
                {
                    foreach (var penality in bblpenaltyexipred)
                    {
                        renewinvoice.Remove(
                         renewinvoice.Where(x => x.GF_DES.Trim().ToUpper().Contains("PENALTY FEE (EXPIRED)") && x.GF_FEE == penality.GF_FEE).Single());
                    }
                }
            }
            var bblpenaltylapsed = renewinvoice.Where(x => x.GF_DES.Trim().ToUpper().Contains("PENALTY FEE (LAPSE)")).ToList();
            if (bblpenaltylapsed.Any())
            {
                if (bblpenaltylapsed.Count() == 1)
                {
                    renewinvoice.Remove(renewinvoice.Where(x => x.GF_DES.Trim().ToUpper().Contains("PENALTY FEE (LAPSE)")).Single());
                }
                else
                {
                    foreach (var penality in bblpenaltylapsed)
                    {
                        renewinvoice.Remove(
                         renewinvoice.Where(x => x.GF_DES.Trim().ToUpper().Contains("PENALTY FEE (LAPSE)") && x.GF_FEE == penality.GF_FEE).Single());
                    }
                }
            }
            //var submissionrenewal = _submissionmasterRenewal.FindByID(renewBasicBusinessLicense.MasterId);
            //    decimal extraamount = Convert.ToDecimal((submissionrenewal.FirstOrDefault().LapsedFee ?? 0) + (submissionrenewal.FirstOrDefault().ExpiredFee ?? 0));
            decimal totalamount = renewinvoice.Aggregate<InvoiceModel, decimal>(0, (current, invoicerenew) => current + Convert.ToDecimal(invoicerenew.GF_FEE));

            if (renewinvoice.Where(x => x.GF_DES.Trim().ToUpper().Contains(GenericEnums.RenewalCheck.ENDORSEMENT.ToString().ToUpper())).Any())
            {
                endoresemetfee = Convert.ToDecimal(renewinvoice.FirstOrDefault().GF_FEE);
                renewinvoice.Remove(renewinvoice.Where(x => x.GF_DES.Trim().ToUpper().Contains(GenericEnums.RenewalCheck.ENDORSEMENT.ToString().ToUpper())).Single());
            }
            if (renewinvoice.Where(x => x.GF_DES.Trim().ToUpper().Contains(GenericEnums.RenewalCheck.ENHANCED.ToString().ToUpper())).Any())
            {
                enhancedfee = Convert.ToDecimal(renewinvoice.FirstOrDefault().GF_FEE);
                renewinvoice.Remove(renewinvoice.Where(x => x.GF_DES.Trim().ToUpper().Contains(GenericEnums.RenewalCheck.ENHANCED.ToString().ToUpper())).Single());
            }
            if (renewinvoice.Where(x => x.GF_DES.Trim().ToUpper().Contains(GenericEnums.RenewalCheck.APPLICATION.ToString().ToUpper())).Any())
            {
                applicationfee = Convert.ToDecimal(renewinvoice.FirstOrDefault().GF_FEE);
                renewinvoice.Remove(renewinvoice.Where(x => x.GF_DES.Trim().ToUpper().Contains(GenericEnums.RenewalCheck.APPLICATION.ToString().ToUpper())).Single());
            }
            if (renewinvoice.Where(x => x.GF_DES.Trim().ToUpper().Contains(GenericEnums.RenewalCheck.RAO.ToString().ToUpper())).Any())
            {
                raofee = Convert.ToDecimal(renewinvoice.FirstOrDefault().GF_FEE);
                renewinvoice.Remove(renewinvoice.Where(x => x.GF_DES.Trim().ToUpper().Contains(GenericEnums.RenewalCheck.RAO.ToString().ToUpper())).Single());
            }

            if (renewinvoice.Where(x => x.GF_DES.Trim().ToUpper().Contains("PENALTY FEE (EXPIRED)")).Any())
            {
                renewinvoice.Remove(renewinvoice.Where(x => x.GF_DES.Trim().ToUpper().Contains("PENALTY FEE (EXPIRED)")).Single());
            }
            if (renewinvoice.Where(x => x.GF_DES.Trim().ToUpper().Contains("PENALTY FEE (LAPSE)")).Any())
            {
                renewinvoice.Remove(renewinvoice.Where(x => x.GF_DES.Trim().ToUpper().Contains("PENALTY FEE (LAPSE)")).Single());
            }

            decimal licensefeeamount = 0;
            foreach (var licencefee in renewinvoice)
            {
                licensefeeamount = licensefeeamount + Convert.ToDecimal(licencefee.GF_FEE);
                //units = units + Convert.ToInt32(licencefee.GF_UNIT);
            }
            var primrayunits =
                renewinvoice.Where(
                    x =>
                        x.GF_DES.ToUpper()
                            .Contains(
                                (renewBasicBusinessLicense.PrimaryCategoryName)
                                    .ToUpper())).ToList();
            if (primrayunits.Any())
            {
                if (primrayunits.Count() == 2)
                {
                    var roomsdata =
                                                   primrayunits.Where(
                                                       x =>
                                                           x.GF_DES.ToUpper()
                                                               .Trim()
                                                               .Contains("ROOM")).ToList();
                    if (roomsdata.Count != 0)
                    {
                        units = Convert.ToInt32((primrayunits.FirstOrDefault().GF_UNIT));
                    }
                }
                else
                {
                    units = Convert.ToInt32((primrayunits.FirstOrDefault().GF_UNIT));
                }
            }

            renewBasicBusinessLicense.Units = units.ToString();
            renewBasicBusinessLicense.LicenseFee = licensefeeamount;
            renewBasicBusinessLicense.RaoFee = raofee;
            renewBasicBusinessLicense.ApplicationFee = applicationfee;
            renewBasicBusinessLicense.EndoresementFee = endoresemetfee;
            renewBasicBusinessLicense.LateFee = 0;
            renewBasicBusinessLicense.PenaltyFee = penaltyfee;
            renewBasicBusinessLicense.EnhancedFee = enhancedfee;
            renewBasicBusinessLicense.ApplicationExpiry = Convert.ToDateTime(bblrepositorydata.FirstOrDefault().Expiration_Date).ToString("MM/dd/yyyy");
            //  renewBasicBusinessLicense.TotalFee = totalamount + extraamount;
            renewBasicBusinessLicense.LapsedDate = Convert.ToDateTime(bblrepositorydata.FirstOrDefault().Expiration_Date).AddDays(1).ToString("MM/dd/yyyy");
            renewBasicBusinessLicense.LapsedFee = totalamount + 250;
            renewBasicBusinessLicense.ExpiryDate = Convert.ToDateTime(bblrepositorydata.FirstOrDefault().Expiration_Date).AddDays(31).ToString("MM/dd/yyyy");
            renewBasicBusinessLicense.ExpiryFee = totalamount + 500;

            return renewBasicBusinessLicense;
        }
        /// <summary>
        /// This method is used to get state name based on state code and country code.
        /// </summary>
        /// <param name="stateCode"></param>
        /// <param name="countryCode"></param>
        /// <returns>Return data as string</returns>
        public string GetStateFullName(string stateCode, string countryCode)
        {
            if (countryCode == "")
                return stateCode;

            var getCountry = _MasterCountryRepository.FindCountryBasedOnName(countryCode);
            if (getCountry.Any())
                countryCode = getCountry.FirstOrDefault().CountryCode;

            var statedata = _masterStateRepository.GetStateName(stateCode, countryCode).ToList();
            if (statedata.Any())
            {
                return statedata.FirstOrDefault().StateName;
            }
            else
            {
                return stateCode;
            }
        }
        /// <summary>
        /// This method is used to get Country full name based on country code
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns>Return data as string</returns>
        public string GetCountryFullName(string countryCode)
        {
            var countrydata = _MasterCountryRepository.FindCountryBasedOnCode(countryCode).ToList();
            if (countrydata.Any())
            {
                return countrydata.FirstOrDefault().CountryName;
            }
            else
            {
                return countryCode;
            }
        }
        /// <summary>
        /// This method is used to get street full name based on street code
        /// </summary>
        /// <param name="streetCode"></param>
        /// <returns>Return data as string</returns>
        public string GetStreetFullName(string streetCode)
        {
            var streetdata = _streetTypesRespository.FindStreetIdbyCode(streetCode).ToList();
            if (streetdata.Any())
            {
                return streetdata.FirstOrDefault().StreetType;
            }
            else
            {
                return streetCode;
            }
        }
        /// <summary>
        /// This method is used to get state code based on state name and country code
        /// </summary>
        /// <param name="stateName"></param>
        /// <param name="countryCode"></param>
        /// <returns>Return data as string</returns>
        public string GetStateCode(string stateName, string countryCode)
        {
            if (countryCode == "")
                return stateName;

            var getCountry = _MasterCountryRepository.FindCountryBasedOnName(countryCode);
            if (getCountry.Any())
                countryCode = getCountry.FirstOrDefault().CountryCode;

            var statedata = _masterStateRepository.GetStateCode(stateName, countryCode).ToList();
            if (statedata.Any())
            {
                return statedata.FirstOrDefault().StateCode;
            }
            else
            {
                return stateName;
            }
        }
    }
}