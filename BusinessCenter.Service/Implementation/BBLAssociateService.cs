using BusinessCenter.Common;
using BusinessCenter.Data;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using BusinessCenter.Data.Models;
using BusinessCenter.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Service.Implementation
{
    public class BBLAssociateService : IBBLAssociateService
    {
        //protected IBBLPinRepository _repository;
        protected IBblRepository bblRepository;

        protected IUserBBLServiceRepository bblServicerepository;
        protected ISubmissionDocumentRepository subdocumentsrepository;
        protected ISubmissionCofoHopeHopRepository sublocrepository;
        protected ISubmissionMasterApplicationChcekListRepository subchecklistrepository;
        protected ISubmissionCorporationRepository subcorprespository;
        protected ISubmissionVerficationRepository subverifyRepository;
        protected ISubmissionEHOPEligibilityRepository _subEhoprepository;
        protected IPaymentDetailsRepository _payDetailsrespository;
        protected ISubmissionCorporationAgentAddressRepository _corpAgentRepo;
        protected IDCBC_ENTITY_BBL_RenewalsRepository _BBL_RenewalsRepositor;
        protected IOSubCategoryFeesRepository _feesrepository;
        protected IUserRepository _userrepository;
        protected IDCBC_ENTITY_Cof_ORepository coforepository;
        protected IMasterCategoryQuestionRepository categoryQuestionrepository;
        protected ISubmissionBblAssociationToUsersRepository submissionAssociaterepo;
        protected IStreetTypesRepository _streetTypesRepository;

        //protected IMasterPrimaryCategoryRepository masterPrimaryCategoryRepository;
        /// <summary>
        /// Initializes a new Instance of SecurityQuestionService,
        /// </summary>
        /// <param name="repo"></param>
        public BBLAssociateService(IBblRepository bblrepo, IUserBBLServiceRepository bblservicerepo
            , ISubmissionDocumentRepository subdocumentsrepo, ISubmissionCorporationRepository subcorpresp,
            ISubmissionCofoHopeHopRepository sublocrepo, ISubmissionMasterApplicationChcekListRepository subchecklistrepo,
            ISubmissionCorporationAgentAddressRepository corpAgentRepo, ISubmissionEHOPEligibilityRepository subEhoprepo, ISubmissionVerficationRepository subverifyRepo,
            IPaymentDetailsRepository payDetailsResp, IDCBC_ENTITY_BBL_RenewalsRepository BBL_RenewalsRepositor,
            IOSubCategoryFeesRepository feerepo, IDCBC_ENTITY_Cof_ORepository coforepo, IMasterCategoryQuestionRepository categoryQuestionrepo,
            IUserRepository userrepo,
            ISubmissionBblAssociationToUsersRepository _submissionAssociaterepo, IStreetTypesRepository streetTypesRepository
            //IMasterPrimaryCategoryRepository _masterPrimaryCategoryRepository
            )
        {
            //  _repository = repo;
            bblRepository = bblrepo;
            bblServicerepository = bblservicerepo;
            subdocumentsrepository = subdocumentsrepo;
            sublocrepository = sublocrepo;
            subchecklistrepository = subchecklistrepo;
            subcorprespository = subcorpresp;
            _subEhoprepository = subEhoprepo;
            _corpAgentRepo = corpAgentRepo;
            subverifyRepository = subverifyRepo;
            _payDetailsrespository = payDetailsResp;
            _BBL_RenewalsRepositor = BBL_RenewalsRepositor;
            _feesrepository = feerepo;
            _userrepository = userrepo;
            coforepository = coforepo;
            categoryQuestionrepository = categoryQuestionrepo;
            submissionAssociaterepo = _submissionAssociaterepo;
            _streetTypesRepository = streetTypesRepository;
            //masterPrimaryCategoryRepository = _masterPrimaryCategoryRepository;
        }

        /// <summary>
        ///This method is used get specfic bbl renewals data based on user inputs
        /// </summary>
        /// <param name="bblAssociate"></param>
        /// <returns>Return DCBC_ENTITY_BBL_Renewals data</returns>
        public IEnumerable<DCBC_ENTITY_BBL_Renewals> FindByPin(BblAsscoiatePin bblAssociate)
        {
            var commandata = _BBL_RenewalsRepositor.FindByPin(bblAssociate);
            return commandata.ToList();
        }

        /// <summary>
        ///This method to is used to check the associate data exist or not based on user inputs.
        /// </summary>
        /// <param name="bblAssociate"></param>
        /// <returns>Return bool value</returns>
        public bool CheckAssociate(BblAsscoiatePin bblAssociate)
        {
            var commandata = _BBL_RenewalsRepositor.CheckAssociate(bblAssociate);
            return commandata;
        }

        /// <summary>
        ///This method is used to get license details based on license number.
        /// </summary>
        /// <param name="licenseNumber"></param>
        /// <returns>Return DCBC_ENTITY_BBL data</returns>
        public IEnumerable<DCBC_ENTITY_BBL> GetAssociteData(string licenseNumber)
        {
            var commandata = bblRepository.FindByLicense(licenseNumber);
            return commandata.ToList();
        }

        /// <summary>
        ///This method is used to get bbl data based on license number
        /// </summary>
        /// <param name="licenseNumber"></param>
        /// <returns>Return DCBC_ENTITY_BBL data</returns>
        public IEnumerable<DCBC_ENTITY_BBL> ValidateBblEntityWithLicense(string licenseNumber)
        {
            var commandata = bblRepository.FindBy(x => x.B1_ALT_ID.Trim() == licenseNumber.Trim());
            return commandata.ToList();
        }

        /// <summary>
        ///This method is used to get bbl data based on entity id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>Return DCBC_ENTITY_BBL data</returns>
        public IEnumerable<DCBC_ENTITY_BBL> GetBblDataOnEntityId(int entityId)
        {
            var commandata = bblRepository.FindByID(entityId);
            return commandata.ToList();
        }

        //public void UpdateUserAssociate(BblAsscoiateService bblassociate)
        //{ _repository.UpdateUserAssociate(bblassociate); }
        /// <summary>
        ///
        /// </summary>
        /// <param name="bblService"></param>
        /// <returns></returns>
        public string InsertAssociateBbl(BblAsscoiateService bblService)
        {
            var commandata = bblServicerepository.InsertAssociateBbl(bblService);
            return commandata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bblService"></param>
        /// <returns></returns>
        public bool DeleteUserService(BblAsscoiateService bblService)
        {
            var commandata = bblServicerepository.DeleteUserService(bblService);
            return commandata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bblServiceDocuments"></param>
        /// <returns></returns>
        public UploadStatus InsertServiceDocuments(BblServiceDocuments bblServiceDocuments)
        {
            var commandata = subdocumentsrepository.InsertServiceDocuments(bblServiceDocuments);
            return commandata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bbldoc"></param>
        /// <returns></returns>
        public IEnumerable<BblDocuments> DocumentList(BblDocuments bbldoc)
        {
            var commandata = subdocumentsrepository.DocumentList(bbldoc);
            return commandata.ToList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bbldoc"></param>
        /// <returns></returns>
        public bool UpdateSubmissionMaster(BblDocuments bbldoc)
        {
            var commandata = subdocumentsrepository.UpdateSubmissionMaster(bbldoc);
            return commandata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="generalbusiness"></param>
        /// <returns></returns>
        public bool InsertPhysicallocation(CofoHopDetailsModel generalbusiness)
        {
            var commandata = sublocrepository.InsertSubmissionLocation(generalbusiness);
            return commandata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cofoHopDetailsModel"></param>
        /// <returns></returns>
        public bool UpdateIsCofoinChecklistApp(CofoHopDetailsModel cofoHopDetailsModel)
        {
            var commondata = subchecklistrepository.UpdateIsCofo(cofoHopDetailsModel);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cofoHopDetailsModel"></param>
        /// <returns></returns>
        public bool UpdateAllCheckListConditions(CofoHopDetailsModel cofoHopDetailsModel)
        {
            var commondata = subchecklistrepository.UpdateAllCheckListConditions(cofoHopDetailsModel);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="DetailsModel"></param>
        /// <returns></returns>
        public bool InsertCorporationDetails(GeneralBusiness DetailsModel)
        {
            var commondata = subcorprespository.InsertCorporationDetails(DetailsModel);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="eligibilityModel"></param>
        /// <returns></returns>
        public bool InsertEHopEligibility(EligibilityModel eligibilityModel)
        {
            var commondata = _subEhoprepository.InsertEHopEligibility(eligibilityModel);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="subCorpAgentModel"></param>
        /// <returns></returns>
        public GeneralBusiness GetHeadQuarterAddress(GeneralBusiness subCorpAgentModel)
        {
            var Hqaddresses = subcorprespository.GetHQAddess(subCorpAgentModel);
            return Hqaddresses;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="subCorpAgentModel"></param>
        /// <returns></returns>
        public GeneralBusiness GetPrimisessAddress(GeneralBusiness subCorpAgentModel)
        {
            var PrimisessAddress = sublocrepository.GetPrimisessAddress(subCorpAgentModel);
            return PrimisessAddress;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="generalBusiness"></param>
        /// <returns></returns>
        public IEnumerable<GeneralBusiness> GetCorpBusinessDetails(GeneralBusiness generalBusiness)
        {
            var corpBusinessDetails = subcorprespository.GetCorpBusinessData(generalBusiness);
            return corpBusinessDetails;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ehopModel"></param>
        /// <returns></returns>
        public EhopModel MasterHopEligibility(EhopModel ehopModel)
        {
            var commondata = _subEhoprepository.MasterHopEligibility(ehopModel);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="SubVerfication"></param>
        /// <returns></returns>
        public SubmissionVerfication SubmissionDetails(SubmissionVerfication SubVerfication)
        {
            var subDetails = subverifyRepository.SubmissionDetails(SubVerfication);
            return subDetails;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="SubVerfication"></param>
        /// <returns></returns>
        public SubmissionVerfication SubmissionPayDetails(SubmissionVerfication SubVerfication)
        {
            var subDetails = subverifyRepository.SubmissionPayDetails(SubVerfication);
            return subDetails;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bbldoc"></param>
        /// <returns></returns>
        public bool DeleteDocuments(BblServiceDocuments bbldoc)
        {
            var subDetails = subdocumentsrepository.DeleteDocuments(bbldoc);
            return subDetails;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pDetails"></param>
        /// <returns></returns>
        public string InsertPaymentDetails(PaymentDetailsModel pDetails)
        {
            var subDetails = _payDetailsrespository.InsertPaymentDetails(pDetails);
            return subDetails;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="paymentDetails"></param>
        /// <returns></returns>
        public IEnumerable<PaymentDetails> FindByPaymentID(PaymentDetails paymentDetails)
        {
            var subDetails = _payDetailsrespository.FindByPaymentID(paymentDetails);
            return subDetails;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="generalBusiness"></param>
        /// <returns></returns>
        public List<GeneralBusiness> GetCorpAgent(GeneralBusiness generalBusiness)
        {
            var subDetails = subcorprespository.GetCorpAgent(generalBusiness);
            return subDetails;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pDetails"></param>
        /// <returns></returns>
        public bool UpdatePaymentDetails(PaymentDetailsModel pDetails)
        {
            var subDetails = _payDetailsrespository.UpdatePaymentDetails(pDetails);
            return subDetails;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="RModel"></param>
        /// <returns></returns>
        public ReceiptModel GetReceiptData(ReceiptModel RModel)
        {
            var subDetails = _payDetailsrespository.GetReceiptData(RModel);
            return subDetails;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cofoModel"></param>
        /// <returns></returns>
        public bool DeleteCofo(CofoHopDetailsModel cofoModel)
        {
            var subDetails = sublocrepository.DeleteCofo(cofoModel);
            return subDetails;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="documentid"></param>
        /// <returns></returns>
        public IEnumerable<MasterCategoryDocument> FindByDocID(int documentid)
        {
            var commondata = subdocumentsrepository.FindByDocID(documentid);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="categoryname"></param>
        /// <returns></returns>
        public IEnumerable<MasterCategoryDocument> FindByDocName(string categoryname)
        {
            var commondata = subdocumentsrepository.FindByDocName(categoryname);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="categoryname"></param>
        /// <returns></returns>
        public IEnumerable<MasterCategoryDocument> FindByID(string categoryname)
        {
            var commondata = subdocumentsrepository.FindByID(categoryname);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="categoryname"></param>
        /// <returns></returns>
        public IEnumerable<MasterCategoryDocument> FindByRenewID(string categoryname)
        {
            var commondata = subdocumentsrepository.FindByRenewID(categoryname);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="categoryDocumentModel"></param>
        /// <returns></returns>
        public int InsertUpdateCategoryDocuments(MasterCategoryDocumentModel categoryDocumentModel)
        {
            return subdocumentsrepository.InsertUpdateCategoryDocuments(categoryDocumentModel);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="categoryDocumentModel"></param>
        /// <returns></returns>
        public bool DeleteCategoryDocument(MasterCategoryDocumentModel categoryDocumentModel)
        {
            return subdocumentsrepository.DeleteCategoryDocument(categoryDocumentModel);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="oSub_Category_FeesEntity"></param>
        /// <returns></returns>
        public int InsertUpdateCategoryFees(OSub_Category_FeesEntity oSub_Category_FeesEntity)
        {
            return _feesrepository.InsertUpdateCategoryFees(oSub_Category_FeesEntity);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="PrimaryId"></param>
        /// <returns></returns>
        public IEnumerable<OSub_Category_Fees> FindFeesByPrimaryCategory(string PrimaryId)
        {
            return _feesrepository.FindFeesByPrimaryCategory(PrimaryId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="secondaryId"></param>
        /// <returns></returns>
        public IEnumerable<OSub_Category_Fees> FindFeesBySecondaryCategory(string secondaryId)
        {
            return _feesrepository.FindFeesBySecondaryCategory(secondaryId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="categoryFeeId"></param>
        /// <returns></returns>
        public IEnumerable<OSub_Category_Fees> FindByCategoryFeeId(string categoryFeeId)
        {
            return _feesrepository.FindByCategoryFeeId(categoryFeeId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OSub_Category_Fees> AllSubCategoryFees()
        {
            return _feesrepository.AllSubCategoryFees();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="entityID"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public IEnumerable<UserBBLService> CheckUserBBL(string entityID, string userid)
        {
            var commandata = bblServicerepository.CheckUserBBL(entityID, userid);
            return commandata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public IEnumerable<string> FindByUserName(string term)
        {
            return _userrepository.FindByUserName(term);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cofoModel"></param>
        /// <returns></returns>
        public bool DeleteHopPrimsesAddrss(CofoHopDetailsModel cofoModel)
        {
            var subDetails = sublocrepository.DeleteHopPrimsesAddrssOnly(cofoModel);
            return subDetails;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="masterId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool DeleteSubmissionCorpEmpty(string masterId, string type)
        {
            var subDetails = subcorprespository.DeleteSubmissionCorpEmpty(masterId, type);
            return subDetails;
        }

        //public void CofoServiceStatus(string Masterid)
        //{
        //    coforepository.CofoServiceStatus(Masterid);
        //}

        //public int InsertUpdateCategoryQuestions(CategoryQuestionModel categoryQuestionModel)
        //{
        //    return categoryQuestionrepository.InsertUpdateCategoryQuestions(categoryQuestionModel);
        //}
        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<User> FindByAdminId(string userId)
        {
            var commondata = _userrepository.FindByID(userId);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="primaryPhysicallocation"></param>
        /// <returns></returns>
        public string InsertUpdatePrimaryCategory(PrimaryPhysicallocation primaryPhysicallocation)
        {
            return _feesrepository.InsertUpdatePrimaryCategory(primaryPhysicallocation);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="Description"></param>
        /// <returns></returns>
        public IEnumerable<OSub_Category_Fees> FindFeesByDescription(string Description)
        {
            return _feesrepository.FindByDescription(Description);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
        public CorporationStatus CorpServiceStatus(string masterId)
        { return subcorprespository.CorpServiceStatus(masterId); }

        /// <summary>
        ///
        /// </summary>
        /// <param name="Masterid"></param>
        /// <param name="licenseNumber"></param>
        /// <returns></returns>
        public bool DocumentInsertion(string Masterid, string licenseNumber)
        {
            var commandata = subdocumentsrepository.DocumentInsertion(Masterid, licenseNumber);
            return commandata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="tranferlic"></param>
        /// <returns></returns>
        public bool InsertTransferLicense(Submissiontransfer tranferlic)
        {
            var commondata = submissionAssociaterepo.InsertTransferLicense(tranferlic);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="secondaryname"></param>
        /// <returns></returns>
        public IEnumerable<MasterCategoryQuestion> FindBySecondaryName(string secondaryname)
        {
            var commondata = categoryQuestionrepository.FindBySecondaryName(secondaryname);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="categoryname"></param>
        /// <returns></returns>
        public IEnumerable<MasterCategoryDocument> FindByDocNameBasedonCategoryName(string categoryname)
        {
            var commondata = subdocumentsrepository.FindByDocNameBasedonCategoryName(categoryname);
            return commondata;
        }

        //public bool UpdatePrimaryCategory(PrimaryPhysicallocation primaryPhysicallocation)
        //{
        //    var commondata = masterPrimaryCategoryRepository.UpdatePrimaryCategory(primaryPhysicallocation);
        //    return commondata;
        //}
        /// <summary>
        ///
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public IEnumerable<MasterCategoryDocument> FindByDocBasedonDocId(int documentId)
        {
            var commondata = subdocumentsrepository.FindByDocBasedonDocId(documentId);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="masterid"></param>
        /// <param name="submissionLicense"></param>
        /// <returns></returns>
        public bool RenewalStatuUpdation(string masterid, string submissionLicense)
        {
            var commandata = subdocumentsrepository.RenewalStatuUpdation(masterid, submissionLicense);
            return commandata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
        public IEnumerable<SubmissionCofo_Hop_Ehop> EhopNumberWithMasterId(string masterId)
        {
            try
            {
                var cofoHopDetails = sublocrepository.EhopNumberWithMasterId(masterId);
                return cofoHopDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="customTypeid"></param>
        /// <returns></returns>
        public IEnumerable<SubmissionCofo_Hop_Ehop_Address> GetPrimisessAddress(int customTypeid)
        {
            try
            {
                var cofoHopDetails = sublocrepository.GetPrimisessAddress(customTypeid);
                return cofoHopDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
        public TaxAndReneueInitailDisplay DisplayTaxAndRevenuWithPrimisessDetails(string masterId)
        {
            try
            {
                var taxAndReneueInitailDisplay = sublocrepository.DisplayTaxAndRevenuWithPrimisessDetails(masterId);
                return taxAndReneueInitailDisplay;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //bblServicerepository
        /// <summary>
        ///
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public IEnumerable<UserBBLService> UserbblServiceFindById(int serviceId)
        {
            return bblServicerepository.FindByID(serviceId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
        public string BusinessOwnerName(string masterId)
        {
            return sublocrepository.BusinessOwnerName(masterId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ehopData"></param>
        /// <returns></returns>
        public EhopData EhopData(EhopData ehopData)
        {
            var commondata = _subEhoprepository.EhopData(ehopData);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="basicBusinessLicense"></param>
        /// <returns></returns>
        public BasicBusinessLicense BusinessReceipt(BasicBusinessLicense basicBusinessLicense)
        {
            var receipt = subverifyRepository.BusinessReceipt(basicBusinessLicense);
            return receipt;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="renewBasicBusinessLicense"></param>
        /// <returns></returns>
        public RenewBasicBusinessLicense RenewBusinessReceipt(RenewBasicBusinessLicense renewBasicBusinessLicense)
        {
            var receipt = subverifyRepository.RenewBusinessReceipt(renewBasicBusinessLicense);
            return receipt;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="generalBusiness"></param>
        public void UpdateCorpDisplayStatus(GeneralBusiness generalBusiness)
        {
            subcorprespository.UpdateCorpDisplayStatus(generalBusiness);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="masterid"></param>
        /// <returns></returns>
        public IEnumerable<SubmissionMaster_ApplicationCheckList> FindByMasterId(string masterid)
        {
            var subchecklist = subchecklistrepository.FindByMasterId(masterid);
            return subchecklist;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StreetTypes> AllStreetTypes()
        {
            var subchecklist = _streetTypesRepository.AllStreetTypes();
            return subchecklist;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="corporationdetails"></param>
        /// <returns></returns>
        public string CorpOnlineSearch(CorporationDetails corporationdetails)
        {
            return subcorprespository.CorpOnlineSearch(corporationdetails);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="licenseNumber"></param>
        /// <returns></returns>
        public IEnumerable<DCBC_ENTITY_BBL_Renewals> FindByLicense(string licenseNumber)
        {
            var commondata = _BBL_RenewalsRepositor.FindByLicense(licenseNumber);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="masterid"></param>
        /// <returns></returns>
        public PaymentTransactionDetails FindAddressByPaymentId(string masterid)
        {
            var commondata = _payDetailsrespository.FindAddressByPaymentId(masterid);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public string GetRenewalLicenseNumber(string entityId)
        {
            string resultvalue = string.Empty;
            var getLicenseNumber = bblRepository.FindByID(Convert.ToInt32(entityId));
            if (getLicenseNumber.Any())
            {
                var getLrennumber = _BBL_RenewalsRepositor.FindByLicense(getLicenseNumber.FirstOrDefault().B1_ALT_ID).ToList();

                if (getLrennumber.Count > 0)
                {
                    var result = getLrennumber.FirstOrDefault();
                    resultvalue = result.b1_Alt_ID.Trim();
                }
            }

            return resultvalue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="stateCode"></param>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public string GetStateFullName(string stateCode, string countryCode)
        {
            //if (countryCode == "")
            //    return stateCode;

            //var getCountry = subverifyRepository.FindCountryBasedOnName(countryCode);
            //if (getCountry.Any())
            //    countryCode = getCountry.FirstOrDefault().CountryCode;
            var receipt = subverifyRepository.GetStateFullName(stateCode, countryCode);
            return receipt;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="statename"></param>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public string StateCode(string statename, string countryCode)
        {
            //if (countryCode == "")
            //    return stateCode;

            //var getCountry = subverifyRepository.FindCountryBasedOnName(countryCode);
            //if (getCountry.Any())
            //    countryCode = getCountry.FirstOrDefault().CountryCode;
            var receipt = subverifyRepository.GetStateCode(statename, countryCode);
            return receipt;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public string GetCountryFullName(string countryCode)
        {
            var receipt = subverifyRepository.GetCountryFullName(countryCode);
            return receipt;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bblassociatepin"></param>
        /// <returns></returns>
        public bool FindByLicenseTax(BblAsscoiatePin bblassociatepin)
        {
            var receipt = bblRepository.FindByLicenseTax(bblassociatepin);
            return receipt;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="licenseNumber"></param>
        /// <param name="lrenNumber"></param>
        /// <returns></returns>
        public IEnumerable<DCBC_ENTITY_BBL_Renewals> RenewalData(string licenseNumber, string lrenNumber)
        {
            var commandata = _BBL_RenewalsRepositor.FindBybblRenewBasedonLicensenumber(licenseNumber, lrenNumber);
            return commandata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DCBC_ENTITY_BBL> DailyMailAlarmToBBlLicenseUserPriorToExpired()
        {
            var commandata = bblRepository.DailyMailAlarmToBBlLicenseUsers().ToList();
            return commandata.ToList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserBBLService> MailAlarmAssociateUsers()
        {
            var commandata = bblServicerepository.GetAssociateUsers();
            return commandata.ToList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetUserDetails()
        {
            var commandata = _userrepository.GetUserLookupAll().ToList();
            return commandata.ToList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bblService"></param>
        /// <returns></returns>
        public IEnumerable<UserBBLService> GetTransferdata(BblAsscoiateService bblService)
        {
            var commandata = bblServicerepository.FindByEntityId(bblService);
            return commandata.ToList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public IEnumerable<UserBBLService> GetUserBBLData(int serviceId)
        {
            var commandata = bblServicerepository.FindByID(serviceId);
            return commandata.ToList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SubmissionBblAssociationToUsers> GetTransferHistory()
        {
            var commandata = submissionAssociaterepo.GetAllTransfer();
            return commandata.ToList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userdetail"></param>
        /// <returns></returns>
        public bool LastLoggedTimeUpdate(Userdetails userdetail)
        {
            return _userrepository.UpdateLoggedTime(userdetail);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="MasterId"></param>
        /// <returns></returns>
        public IEnumerable<SubmissionCorporation_Agent> SubmissionCorporation_Agent_ByMasterId(string MasterId)
        {
            return subcorprespository.FindById(MasterId.Trim());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="masterId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public IEnumerable<SubmissionCorporation_Agent_Address> MailAddresses(int masterId, string type)
        {
            return _corpAgentRepo.FindByTypewithMasterId(masterId, "NEWMAIL");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="licensenumber"></param>
        /// <returns></returns>
        public string GetRenewalLicense(string licensenumber)
        {
            string resultvalue = string.Empty;

            var getLrennumber = _BBL_RenewalsRepositor.FindByLicensenumber(licensenumber).ToList();

            if (getLrennumber.Any())
            {
                var result = getLrennumber.FirstOrDefault();
                resultvalue = result.License_Being_Renewed.Trim();
            }
            else
            { resultvalue = licensenumber; }

            return resultvalue;
        }
    }
}