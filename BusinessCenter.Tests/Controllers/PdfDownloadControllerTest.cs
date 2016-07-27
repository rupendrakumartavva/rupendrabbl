using System;
using System.Collections.Generic;
using BusinessCenter.Data.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;
using BusinessCenter.Api.Controllers;
using BusinessCenter.Api.Models;
using BusinessCenter.Api.Utility;
using BusinessCenter.Common;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Model;
using BusinessCenter.Service.Implementation;
using BusinessCenter.Tests.Setup;
using BusinessCenter.Data.Models;
using BusinessCenter.Service.Common;
using System.Web;
using System.Web.Http;

namespace BusinessCenter.Tests.Controllers
{
    [TestClass]
    public class PdfDownloadControllerTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        #region BBL AssociateService


        private BblRepository _bblRepository;
        private UserBBLServiceRepository _userBblServiceRepository;
        private SubmissionDocumentRepository _submissionDocumentRepository;
        private SubmissionCofoHopeHopRepository _submissionCofoHopeHopRepository;
        private SubmissionMasterApplicationChcekListRepository _submissionMasterApplicationChcekListRepository;
        private SubmissionCorporationRepository _submissionCorporationRepository;
        private SubmissionVerficationRepository _submissionVerficationRepository;
        private SubmissionEHOPEligibilityRepository _submissionEhopEligibilityRepository;
        private PaymentDetailsRepository _paymentDetailsRepository;
        private SubmissionCorporationAgentAddressRepository _submissionCorporationAgentAddressRepository;
        private DCBC_ENTITY_BBL_RenewalsRepository _dCbcEntityBblRenewalsRepository;
        private OSubCategoryFeesRepository _oSubCategoryFeesRepository;
        private UserRepository _userRepository;
        private DCBC_ENTITY_Cof_ORepository _dCbcEntityCofoRepository;
        private MasterCategoryQuestionRepository _masterCategoryQuestionRepository;
        private SubmissionBblAssociationToUsersRepository _submissionBblAssociationToUsersRepository;
        private MasterBusinessActivityRepository _masterBusinessActivityRepository;

        private MasterCategoryDocumentRepository _masterCategoryDocumentRepository;
        private MasterPrimaryCategoryRepository _masterPrimaryCategoryRepository;
        private MasterSecondaryLicenseCategoryRepository _masterSecondaryLicenseCategoryRepository;
        private MasterCategoryPhysicalLocationRepository _masterCategoryPhysicalLocationRepository;
        private SubmissionMasterRepository _submissionMasterRepository;
        private SubmissionMasterRepository _submissionMasterTestRepository;

        private SubmissionIndividualRepository _submissionIndividualRepository;
        private SubmissionDocumentToAccelaRepository _submissionDocumentToAccelaRepository;
        private SubmissionCofoHopeHopAddressRepository _submissionCofoHopeHopAddressRepository;
        private StreetTypesRepository _streetTypesRepository;
        private FixFeeRepository _fixFeeRepository;
        private MasterRegisterAgentRepository _masterRegisterAgentRepository;
        private SubmissionQuestionRepository _submissionQuestionRepository;
        private CorpRespository _corpRespository;
        private SubmissionTaxRevenueRepository _submissionTaxRevenueRepository;
        private MastereHopEligibilityRepository _mastereHopEligibilityRepository;
        private PaymentCardDetailsRepository _paymentCardDetailsRepository;
        private PaymentAddressDetailsRepository _paymentAddressDetailsRepository;
        private SubmissionMasterRenewalRepository _submissionMasterRenewalRepository;


        private BBLAssociateService _mockBblAsscoiateService;
        private MasterSubCategoryRepository _masterSubCategoryRepository;
        private FeeCodeMapRepository _feeCodeMapRepository;
        private SubmissionToAccelaRepository _submissionToAccelaRepository;
        private UserRoleRepository _userRoleRepository;
        private Mock<IDCBC_ENTITY_BBL_Renewal_InvoiceRepository> _dCbcEntityBblRenewalInvoiceRepository;
        private Lookup_ExistingCategoriesRepository _lookupExistingCategoriesRepository;
        private MasterCountryRepository _masterCountryRepository;
        private MasterEhopOptionTypeRepository _masterEhopOptionType;

        private DCBC_ENTITY_BBL_Renewal_InvoiceRepository _dcbcEntityBblRenewalInvoiceRepository;
        private SubmissionLicenseNumberCounterRepository _submissionlicencecounter;
        private MasterStateRepository _masterStateRepository;

        private EtlAddressAndParcelRepository _etlAddressTestRepository;
        private PaymentHistoryDetailsRepository _paymentHistoryDetails;
        private MasterRenewalStatusFeeRepository _masterrenewalstatusfee;

        private MasterBblApplicationStatusRepository _masterBblApplicationStatusTestRepository;



        #endregion

        #region Declaration SubmissionIndividualService
        private SubmissionCategoryRepository _submissionCategoryRepository;
        private SubmissionIndividualService _submissionIndividualTestService;
        #endregion

        #region Declaration SubmissionGeneratedDocumentService
        private SubmissionGeneratedDocumentRepository _submissionGeneratedDocumentTestRepository;
        private SubmissionGeneratedDocumentService _submissionGeneratedDocumentTestService;
        #endregion

        #region Declaration Submission Master

        private SubmissionMasterService _submissionMasterTestService;

        #endregion

        #region Declaration SearchService
        private SearchRepository _searchTestRepository;
        private AbraRepository _abraTestRepository;
        private CbeRepository _cbeTestRepository;
        private OplaRepository _oplaTestRepository;
        private SearchKeywordRepository _keywordTestRepository;
        private KeywordDetailsRepository _keywordDetailsTestRepository;
        private UserServicesRepository _userServicesTestRepository;

        private SearchService _searchTestService;
        private SearchServiceInputs _searchServiceInput;
        #endregion

        #region Declaration MyServiceDetails
        private MyServiceDetails _mockMyServiceDetailsService;
        #endregion

        private SubmissionSelfCertificationRepository _submissionSelfCertificationRepository;
        private SubmissionSelfCertificationService _submissionSelfCertificationService;

        private PdfDownloadController _controller = null;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);

         
            #region Initialise SubmissionGeneratedDocumentService
            _submissionGeneratedDocumentTestRepository = new SubmissionGeneratedDocumentRepository(_testUnitOfWork);
            _submissionGeneratedDocumentTestService = new SubmissionGeneratedDocumentService(_submissionGeneratedDocumentTestRepository);
            #endregion

          

            #region Initailise BblAssociateService

            _masterStateRepository = new MasterStateRepository(_testUnitOfWork);
            _submissionlicencecounter = new SubmissionLicenseNumberCounterRepository(_testUnitOfWork);
            _masterCountryRepository = new MasterCountryRepository(_testUnitOfWork);
            _bblRepository = new BblRepository(_testUnitOfWork);
            _dCbcEntityBblRenewalsRepository = new DCBC_ENTITY_BBL_RenewalsRepository(_testUnitOfWork);
            _masterEhopOptionType = new MasterEhopOptionTypeRepository(_testUnitOfWork);
            _userBblServiceRepository = new UserBBLServiceRepository(_testUnitOfWork);
            _lookupExistingCategoriesRepository = new Lookup_ExistingCategoriesRepository(_testUnitOfWork);
            _dcbcEntityBblRenewalInvoiceRepository = new DCBC_ENTITY_BBL_Renewal_InvoiceRepository(_testUnitOfWork, _lookupExistingCategoriesRepository);

            _streetTypesRepository = new StreetTypesRepository(_testUnitOfWork);
            _corpRespository = new CorpRespository(_testUnitOfWork);
            _dCbcEntityBblRenewalInvoiceRepository = new Mock<IDCBC_ENTITY_BBL_Renewal_InvoiceRepository>();
            _masterCategoryDocumentRepository = new MasterCategoryDocumentRepository(_testUnitOfWork);
            _masterSecondaryLicenseCategoryRepository = new MasterSecondaryLicenseCategoryRepository(_testUnitOfWork);
            _submissionToAccelaRepository = new SubmissionToAccelaRepository(_testUnitOfWork);
            _userRoleRepository = new UserRoleRepository(_testUnitOfWork);
            _paymentHistoryDetails = new PaymentHistoryDetailsRepository(_testUnitOfWork);
            _submissionMasterApplicationChcekListRepository = new SubmissionMasterApplicationChcekListRepository(_testUnitOfWork);
            _masterCategoryQuestionRepository = new MasterCategoryQuestionRepository(_testUnitOfWork);
            _submissionBblAssociationToUsersRepository = new SubmissionBblAssociationToUsersRepository(_testUnitOfWork);
            _submissionDocumentToAccelaRepository = new SubmissionDocumentToAccelaRepository(_testUnitOfWork);
            _fixFeeRepository = new FixFeeRepository(_testUnitOfWork);
            _masterRegisterAgentRepository = new MasterRegisterAgentRepository(_testUnitOfWork);
            _submissionQuestionRepository = new SubmissionQuestionRepository(_testUnitOfWork);
            _userBblServiceRepository = new UserBBLServiceRepository(_testUnitOfWork);
            _mastereHopEligibilityRepository = new MastereHopEligibilityRepository(_testUnitOfWork);


            _etlAddressTestRepository = new EtlAddressAndParcelRepository(_testUnitOfWork);

            _masterPrimaryCategoryRepository = new MasterPrimaryCategoryRepository(_testUnitOfWork, _masterSecondaryLicenseCategoryRepository, _masterCategoryDocumentRepository,
             _masterCategoryQuestionRepository);


            _masterSubCategoryRepository = new MasterSubCategoryRepository(_testUnitOfWork, _masterPrimaryCategoryRepository, _masterSecondaryLicenseCategoryRepository);
            _oSubCategoryFeesRepository = new OSubCategoryFeesRepository(_testUnitOfWork, _masterPrimaryCategoryRepository, _masterSecondaryLicenseCategoryRepository);

            _feeCodeMapRepository = new FeeCodeMapRepository(_testUnitOfWork, _oSubCategoryFeesRepository);




            _masterCategoryPhysicalLocationRepository = new MasterCategoryPhysicalLocationRepository(_testUnitOfWork, _masterPrimaryCategoryRepository, _masterCategoryQuestionRepository,
              _oSubCategoryFeesRepository, _masterSecondaryLicenseCategoryRepository);

            _submissionMasterTestRepository = new SubmissionMasterRepository(_testUnitOfWork, _submissionCategoryRepository, _bblRepository, _userBblServiceRepository,
            _submissionQuestionRepository, _masterCategoryPhysicalLocationRepository, _masterPrimaryCategoryRepository, _submissionMasterApplicationChcekListRepository,
            _userRepository, _dCbcEntityBblRenewalsRepository, _masterBusinessActivityRepository, _submissionBblAssociationToUsersRepository,
            _submissionToAccelaRepository, _masterSecondaryLicenseCategoryRepository, _lookupExistingCategoriesRepository, _submissionlicencecounter, _masterBblApplicationStatusTestRepository);
            _submissionCategoryRepository = new SubmissionCategoryRepository(_testUnitOfWork,
               _masterPrimaryCategoryRepository, _masterSecondaryLicenseCategoryRepository,
               _oSubCategoryFeesRepository, _fixFeeRepository, _submissionQuestionRepository, _masterSubCategoryRepository,
               _masterCategoryPhysicalLocationRepository,
               _feeCodeMapRepository, _submissionMasterApplicationChcekListRepository, _submissionMasterRenewalRepository,
               _dCbcEntityBblRenewalInvoiceRepository.Object
               , _lookupExistingCategoriesRepository);
            _submissionIndividualRepository = new SubmissionIndividualRepository(_testUnitOfWork, _submissionMasterTestRepository);

            _submissionDocumentRepository = new SubmissionDocumentRepository(_testUnitOfWork, _submissionCategoryRepository, _masterCategoryDocumentRepository,
                _masterPrimaryCategoryRepository, _masterSecondaryLicenseCategoryRepository, _submissionMasterTestRepository,
                _masterCategoryPhysicalLocationRepository, _submissionMasterApplicationChcekListRepository, _submissionIndividualRepository,
                _submissionDocumentToAccelaRepository);

            _submissionSelfCertificationRepository = new SubmissionSelfCertificationRepository(_testUnitOfWork, _submissionMasterApplicationChcekListRepository);
            _userRepository = new UserRepository(_testUnitOfWork, _userRoleRepository);

            _submissionMasterRepository = new SubmissionMasterRepository(_testUnitOfWork, _submissionCategoryRepository, _bblRepository, _userBblServiceRepository,
        _submissionQuestionRepository, _masterCategoryPhysicalLocationRepository, _masterPrimaryCategoryRepository, _submissionMasterApplicationChcekListRepository,
        _userRepository, _dCbcEntityBblRenewalsRepository, _masterBusinessActivityRepository, _submissionBblAssociationToUsersRepository,
        _submissionToAccelaRepository, _masterSecondaryLicenseCategoryRepository, _lookupExistingCategoriesRepository, _submissionlicencecounter, _masterBblApplicationStatusTestRepository);
          

            _submissionCofoHopeHopAddressRepository = new SubmissionCofoHopeHopAddressRepository(_testUnitOfWork, _corpRespository, _submissionMasterRepository,
              _streetTypesRepository,
              _masterRegisterAgentRepository, _submissionCorporationRepository,
              _submissionQuestionRepository, _submissionMasterApplicationChcekListRepository, _masterStateRepository, _masterCountryRepository);

            _submissionCofoHopeHopRepository = new SubmissionCofoHopeHopRepository(_testUnitOfWork, _submissionCofoHopeHopAddressRepository,
                _submissionMasterApplicationChcekListRepository,
                _submissionMasterRepository, _streetTypesRepository, _fixFeeRepository, _submissionDocumentRepository);



            _submissionCorporationAgentAddressRepository = new SubmissionCorporationAgentAddressRepository(_testUnitOfWork, _submissionMasterApplicationChcekListRepository,
              _submissionMasterRepository);

            _submissionCorporationRepository = new SubmissionCorporationRepository(_testUnitOfWork, _submissionCorporationAgentAddressRepository,
                _submissionMasterApplicationChcekListRepository,
                _masterRegisterAgentRepository, _submissionQuestionRepository,
                _corpRespository, _submissionMasterRepository, _masterCountryRepository, _masterStateRepository);



            _submissionEhopEligibilityRepository = new SubmissionEHOPEligibilityRepository(_testUnitOfWork, _mastereHopEligibilityRepository,
                _submissionMasterApplicationChcekListRepository, _userRepository,
                _submissionCofoHopeHopRepository, _submissionDocumentRepository, _masterEhopOptionType);
            _masterBusinessActivityRepository = new MasterBusinessActivityRepository(_testUnitOfWork, _masterPrimaryCategoryRepository);






            _dCbcEntityCofoRepository = new DCBC_ENTITY_Cof_ORepository(_testUnitOfWork, _submissionCofoHopeHopRepository, _streetTypesRepository,
                _submissionMasterRepository, _submissionMasterApplicationChcekListRepository, _submissionCofoHopeHopAddressRepository, _etlAddressTestRepository);


            _submissionTaxRevenueRepository = new SubmissionTaxRevenueRepository(_testUnitOfWork, _submissionMasterApplicationChcekListRepository);



            _paymentCardDetailsRepository = new PaymentCardDetailsRepository(_testUnitOfWork);

            _paymentAddressDetailsRepository = new PaymentAddressDetailsRepository(_testUnitOfWork);
            _masterrenewalstatusfee = new MasterRenewalStatusFeeRepository(_testUnitOfWork);
            _submissionMasterRenewalRepository = new SubmissionMasterRenewalRepository(_testUnitOfWork, _masterrenewalstatusfee);

            _submissionCategoryRepository = new SubmissionCategoryRepository(_testUnitOfWork,
                    _masterPrimaryCategoryRepository, _masterSecondaryLicenseCategoryRepository,
                    _oSubCategoryFeesRepository, _fixFeeRepository, _submissionQuestionRepository, _masterSubCategoryRepository,
                    _masterCategoryPhysicalLocationRepository,
                    _feeCodeMapRepository, _submissionMasterApplicationChcekListRepository, _submissionMasterRenewalRepository,
                    _dCbcEntityBblRenewalInvoiceRepository.Object
                    , _lookupExistingCategoriesRepository);

            _paymentDetailsRepository = new PaymentDetailsRepository(_testUnitOfWork, _paymentCardDetailsRepository, _paymentAddressDetailsRepository,
          _submissionMasterRepository, _submissionCategoryRepository, _submissionDocumentRepository, _submissionMasterApplicationChcekListRepository,
          _fixFeeRepository, _submissionMasterRenewalRepository, _dCbcEntityBblRenewalInvoiceRepository.Object, _bblRepository, _userBblServiceRepository,
          _lookupExistingCategoriesRepository, _paymentHistoryDetails);

            _submissionVerficationRepository = new SubmissionVerficationRepository(_testUnitOfWork, _submissionCofoHopeHopRepository, _submissionCorporationRepository,
             _submissionTaxRevenueRepository, _submissionCorporationAgentAddressRepository, _submissionCofoHopeHopAddressRepository,
             _submissionCategoryRepository, _submissionDocumentRepository,
             _submissionMasterApplicationChcekListRepository, _fixFeeRepository, _masterCountryRepository, _userBblServiceRepository,
             _bblRepository, _dCbcEntityBblRenewalsRepository, _dcbcEntityBblRenewalInvoiceRepository, _masterStateRepository, _streetTypesRepository,
             _paymentDetailsRepository, _submissionMasterRenewalRepository);

            _submissionDocumentRepository = new SubmissionDocumentRepository(_testUnitOfWork, _submissionCategoryRepository,
                _masterCategoryDocumentRepository, _masterPrimaryCategoryRepository,
                _masterSecondaryLicenseCategoryRepository, _submissionMasterRepository, _masterCategoryPhysicalLocationRepository,
                _submissionMasterApplicationChcekListRepository, _submissionIndividualRepository, _submissionDocumentToAccelaRepository);

            _mockBblAsscoiateService = new BBLAssociateService(_bblRepository, _userBblServiceRepository,
           _submissionDocumentRepository, _submissionCorporationRepository,
          _submissionCofoHopeHopRepository, _submissionMasterApplicationChcekListRepository,
          _submissionCorporationAgentAddressRepository, _submissionEhopEligibilityRepository, _submissionVerficationRepository,
          _paymentDetailsRepository, _dCbcEntityBblRenewalsRepository,
          _oSubCategoryFeesRepository, _dCbcEntityCofoRepository, _masterCategoryQuestionRepository,
          _userRepository, _submissionBblAssociationToUsersRepository, _streetTypesRepository);
            #endregion

            #region Initialize SearchService

            _abraTestRepository = new AbraRepository(_testUnitOfWork);

            _cbeTestRepository = new CbeRepository(_testUnitOfWork);
            _oplaTestRepository = new OplaRepository(_testUnitOfWork);

            _keywordTestRepository = new SearchKeywordRepository(_testUnitOfWork, _keywordDetailsTestRepository, _userRepository, _userServicesTestRepository);
            _keywordDetailsTestRepository = new KeywordDetailsRepository(_testUnitOfWork);
            _userServicesTestRepository = new UserServicesRepository(_testUnitOfWork, _abraTestRepository, _bblRepository,
                _cbeTestRepository, _oplaTestRepository, _corpRespository);

            _searchTestRepository = new SearchRepository(_testUnitOfWork,
                _abraTestRepository,
                _bblRepository,
                _cbeTestRepository,
                _oplaTestRepository,
                _corpRespository,
                _keywordTestRepository,
                _keywordDetailsTestRepository,
                _userServicesTestRepository);

            _searchTestService = new SearchService(_searchTestRepository, _searchServiceInput);
            #endregion

            #region Initialize SubmissionMasterService
            _submissionMasterTestService = new SubmissionMasterService(_submissionMasterRepository);
            #endregion

            #region Initialize MyServiceDetails
            _mockMyServiceDetailsService = new MyServiceDetails(_userServicesTestRepository);
            #endregion


            #region Initialize SubmissionIndividualService
            _submissionIndividualTestService = new SubmissionIndividualService(_submissionIndividualRepository);
            #endregion

            _submissionSelfCertificationService = new SubmissionSelfCertificationService(_submissionSelfCertificationRepository);
            _masterBblApplicationStatusTestRepository = new MasterBblApplicationStatusRepository(_testUnitOfWork);
            SearchResultData searchData = new SearchResultData();





            _controller = new PdfDownloadController(_mockBblAsscoiateService, _submissionIndividualTestService, _submissionGeneratedDocumentTestService,

                _submissionMasterTestService, _searchTestService, _mockMyServiceDetailsService);

            var masterBusinessActivityData = new MasterBusinessActivityData();
            _testDbContext.MasterBusinessActivity.AddRange(masterBusinessActivityData.MasterAcivityEntitiesList);
            _testDbContext.SaveChanges();

            // //Setup Data
            var masterPrimaryCategoryRepositoryDataData = new MasterPrimaryCategoryRepositoryData();
            _testDbContext.MasterPrimaryCategory.AddRange(masterPrimaryCategoryRepositoryDataData.MasterPrimaryCategoryEntitiesList);
            _testDbContext.SaveChanges();

            // // 
            var secondaryLicenseData = new MasterSecondaryLicenseCategoryRepositoryData();
            _testDbContext.MasterSecondaryLicenseCategory.AddRange(secondaryLicenseData.MasterSeconderyLicenseCategoryList);
            _testDbContext.SaveChanges();

            var masterCategoryDocumentRepositoryData = new MasterCategoryDocumentRepositoryData();
            _testDbContext.MasterCategoryDocument.AddRange(masterCategoryDocumentRepositoryData.MasterCategoryDocumentList);
            _testDbContext.SaveChanges();



            var masterCategoryQuestionData = new MasterCategoryQuestionData();
            _testDbContext.MasterCategoryQuestion.AddRange(masterCategoryQuestionData.CategoryQuestionEntitiesList);
            _testDbContext.SaveChanges();




            var submissionToAccelaData = new SubmissionToAccelaRepositoryData();
            _testDbContext.SubmissiontoAccela.AddRange(submissionToAccelaData.SubmissiontoAccelaEntitiesList);
            _testDbContext.SaveChanges();

            var associatetoUserData = new SubmissionBblAssociationToUsersRepositoryData();
            _testDbContext.SubmissionBblAssociationToUsers.AddRange(associatetoUserData.SubmissionBblAssociationToUsersEntitiesList);
            _testDbContext.SaveChanges();



            var renewalData = new DCBC_ENTITY_BBL_RenewalsData();
            _testDbContext.DCBC_ENTITY_BBL_Renewals.AddRange(renewalData.DcbcEntityBblRenewalsList);
            _testDbContext.SaveChanges();


            var userData = new UserRepositoryData();
            _testDbContext.User.AddRange(userData.UsersEntitiesList);
            _testDbContext.SaveChanges();

            var submissionMasterApplicationChcekListRepositoryData = new SubmissionMasterApplicationChcekListRepositoryData();
            _testDbContext.SubmissionMaster_ApplicationCheckList
                .AddRange(submissionMasterApplicationChcekListRepositoryData.SubmissionMaster_ApplicationCheckListEntitiesList);
            _testDbContext.SaveChanges();



            var masterCategoryPhysicalLocationData = new MasterCategoryPhysicalLocationData();
            _testDbContext.MasterCategoryPhysicalLocation.AddRange(masterCategoryPhysicalLocationData.MasterCategoryPhysicalLocationList);
            _testDbContext.SaveChanges();

            var submissionMasterRepositoryData = new SubmissionMasterRepositoryData();
            _testDbContext.SubmissionMaster.AddRange(submissionMasterRepositoryData.SubmissionMasterEntitiesList);
            _testDbContext.SaveChanges();

            var submissionCategoryData = new SubmissionCategoryRepositoryData();
            _testDbContext.SubmissionCategory.AddRange(submissionCategoryData.SubmissionCategoryEntitiesList);
            _testDbContext.SaveChanges();



            var userBBlData = new UserBBLServiceRepositoryData();
            _testDbContext.UserBBLService.AddRange(userBBlData.UserBblServiceList);
            _testDbContext.SaveChanges();

            var submissionQuestionData = new SubmissionQuestionRepositoryData();
            _testDbContext.SubmissionQuestion.AddRange(submissionQuestionData.SubmissionQuestionEntitiesList);
            _testDbContext.SaveChanges();



            var testOsubcategoryFeeData = new OSubCategoryFeesData();
            _testDbContext.OSub_Category_Fees.AddRange(testOsubcategoryFeeData.OSubCategoryFeesEntitiesList);
            _testDbContext.SaveChanges();

            var submissionIndividualData = new SubmissionIndividualData();
            _testDbContext.SubmissionIndividual.AddRange(submissionIndividualData.SubmissionIndividualDataEntitiesList);
            _testDbContext.SaveChanges();

            var corpRepositoryData = new CorpRepositoryData();
            _testDbContext.DCBC_ENTITY_CORP.AddRange(corpRepositoryData.CorpEntitiesList);
            _testDbContext.SaveChanges();


            ////------------------









            var masterSubCategoryRepositoryData = new MasterSubCategoryRepositoryData();
            _testDbContext.MasterSubCategory.AddRange(masterSubCategoryRepositoryData.MasterSubCategoryList);
            _testDbContext.SaveChanges();


            var feeCodeMapRepositoryData = new FeeCodeMapRepositoryData();
            _testDbContext.FeeCodeMap.AddRange(feeCodeMapRepositoryData.FeeCodeMapEntitiesList);
            _testDbContext.SaveChanges();


            //Setup Data
            var testData = new SearchMultiColumnLookUpIndexData();
            _testDbContext.DCBC_ENTITY_MultiColumn_LOOKUP_INDEX.AddRange(testData.MultiColumnLookupIndexEntitiesList);
            _testDbContext.SaveChanges();

            // 
            var abraRepositoryData = new AbraRepositoryData();
            _testDbContext.DCBC_ENTITY_ABRA.AddRange(abraRepositoryData.AbraEntitiesList);
            _testDbContext.SaveChanges();

            // 
            var bblRepositoryData = new BblRepositoryData();
            _testDbContext.DCBC_ENTITY_BBL.AddRange(bblRepositoryData.BblEntitiesList);
            _testDbContext.SaveChanges();

            // 
            var cbeRepositoryData = new CbeRepositoryData();
            _testDbContext.DCBC_ENTITY_CBE.AddRange(cbeRepositoryData.CbeEntitiesList);
            _testDbContext.SaveChanges();

            // 
            var oplaRepositoryData = new OplaRepositoryData();
            _testDbContext.DCBC_ENTITY_OPLA.AddRange(oplaRepositoryData.OplaEntitiesList);
            _testDbContext.SaveChanges();




            var userServiceData = new UserServicesRepositoryData();
            _testDbContext.UserService.AddRange(userServiceData.UserServiceEntitiesList);
            _testDbContext.SaveChanges();

            //Setup Data
            var keywordDetailsData = new SearchKeywordRepositoryData();
            _testDbContext.KeywordMaster.AddRange(keywordDetailsData.KeyWordEntitiesList);
            _testDbContext.SaveChanges();

            //Setup Data
            var keywordData = new KeywordDetailsRepositoryData();
            _testDbContext.KeywordDetails.AddRange(keywordData.KeyDetailsEntitiesList);
            _testDbContext.SaveChanges();

            var generatedocumentdata = new SubmissionGeneratedDocumentRepositoryData();
            _testDbContext.SubmissionGeneratedDocument.AddRange(generatedocumentdata.SubmissionDocumentEntitiesList);
            _testDbContext.SaveChanges();

            var paymentdetails = new PaymentDetailsData();
            _testDbContext.PaymentDetails.AddRange(paymentdetails.PaymentDetailsList);
            _testDbContext.SaveChanges();
            var paymentaddressdetails = new PaymentAddressDetailsData();
            _testDbContext.PaymentAddressDetails.AddRange(paymentaddressdetails.PaymentAddressDetailsList);
            _testDbContext.SaveChanges();
            var paymentCarddetails = new PaymentCardDetailsData();
            _testDbContext.PaymentCardDetails.AddRange(paymentCarddetails.PaymentDetailsList);
            _testDbContext.SaveChanges();

        }

        [TestMethod]
        public void SubmissionInformationDetails_Test()
        {
            //Act 
            var contacts = _controller.SubmissionInformationDetails("cce0b056-d2a3-485e-a02a-e34c957c4e40","");
          
            //Assert
           Assert.IsNotNull(contacts); // Test if null
           Assert.IsTrue(contacts.ToString() == "BusinessCenter.Api.Utility.PdfResult");
        }

        [TestMethod]
        public void PhysicalLocationValidation_Test()
        {
            //Act 
            var contacts = _controller.PhysicalLocationValidation(true,true,true,true,"trade name","address","bname");
            
            //Assert
            Assert.IsNotNull(contacts); // Test if null
            Assert.IsTrue(contacts);
        }
        [TestMethod]
        public void InformationVerificationPdfDownload_Test()
        {
            //Act 
            var contacts = _controller.InformationVerificationPdfDownload("cce0b056-d2a3-485e-a02a-e34c957c4e40","");
          
            //Assert
            Assert.IsNotNull(contacts); // Test if null
            Assert.IsTrue(contacts.ToString() == "BusinessCenter.Api.Utility.PdfResult");
        }
        [TestMethod]
        public void ViewReceipt_Renew_test()
        {
            //Act 
            var contacts = _controller.ViewReceipt("8c6deecc-3b72-485d-8af5-30939af94e97", "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F", "RENEW","");

            //Assert
            Assert.IsNotNull(contacts); // Test if null
           // Assert.IsTrue(contacts.Id.ToString() == "1");
            Assert.IsTrue(contacts.Result.IsSuccessStatusCode);
        }
        [TestMethod]
        public void ViewReceipt_SUBREC_test()
        {
            //Act 
            var contacts = _controller.ViewReceipt("cce0b056-d2a3-485e-a02a-e34c957c4e40", "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F", "SUBREC","");

            //Assert
            Assert.IsNotNull(contacts); // Test if null
          //  Assert.IsTrue(contacts.Id.ToString() == "1");
            Assert.IsTrue(contacts.Result.IsSuccessStatusCode);
        }
        [TestMethod]
        public void GetApplicationChecklistPDF_Test()
        {
            //Act 
            var contacts = _controller.GetApplicationChecklistPDF("cce0b056-d2a3-485e-a02a-e34c957c4e40");

            //Assert
            Assert.IsNotNull(contacts); // Test if null
            Assert.IsTrue(contacts.ToString() == "BusinessCenter.Api.Utility.PdfResult");
        }
        [TestMethod]
        public void DownloadEhopDetails_test()
        {
            //Act 
            var contacts = _controller.DownloadEhopDetails("cce0b056-d2a3-485e-a02a-e34c957c4e40", "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F", "");

            //Assert
            Assert.IsNotNull(contacts); // Test if null
            Assert.IsTrue(contacts.Result.IsSuccessStatusCode);
        }
     
        //[TestMethod]
        //public void BblViewRenewelReceipt_test()
        //{
        //    //Act 
        //    var contacts = controller.BblViewRenewelReceipt("cce0b056-d2a3-485e-a02a-e34c957c4e40", "2AC53E53-87D0-468F-9628-4AEBA1120613", "4hFZuEDUmoAQPDe4EeTmMS8x/ecIxYRDatgyGnmMlLo=");

        //    //Assert
        //    Assert.IsNotNull(contacts); // Test if null
        //    Assert.IsTrue(contacts.Id.ToString() == "1");
        //}
       
        //[TestMethod]
        //public void BblViewReceipt_test()
        //{
        //    //Act 
        //    var contacts = controller.BblViewReceipt("8c6deecc-3b72-485d-8af5-30939af94e97", "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F");

        //    //Assert
        //    Assert.IsNotNull(contacts); // Test if null
        //    Assert.IsTrue(contacts.Id.ToString() == "1");
        //}
        //  BblViewReceipt
    }
}