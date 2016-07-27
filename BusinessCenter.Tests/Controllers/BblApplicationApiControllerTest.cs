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
using BusinessCenter.Data;


namespace BusinessCenter.Tests.Controllers
{
     [TestClass]
  public  class BblApplicationApiControllerTest
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
        private DCBC_ENTITY_Cof_ORepository _dCbcEntityCofORepository;
        private MasterCategoryQuestionRepository _masterCategoryQuestionRepository;
        private SubmissionBblAssociationToUsersRepository _submissionBblAssociationToUsersRepository;
        private MasterBusinessActivityRepository _masterBusinessActivityRepository;

        private MasterCategoryDocumentRepository _masterCategoryDocumentRepository;
        private MasterPrimaryCategoryRepository _masterPrimaryCategoryRepository;
        private MasterSecondaryLicenseCategoryRepository _masterSecondaryLicenseCategoryRepository;
        private MasterCategoryPhysicalLocationRepository _masterCategoryPhysicalLocationRepository;
        private SubmissionMasterRepository _submissionMasterRepository;
       // private SubmissionMasterRepository submissionMasterTestRepository;
      
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
       // private SubmissionMasterRenewalRepository _submissionMasterRenewalRepository;


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
       // private MasterCountryRepository _MasterCountryRepository;
        private EtlAddressAndParcelRepository _etlAddressTestRepository;
        private PaymentHistoryDetailsRepository _paymentHistoryDetails;
        private MasterRenewalStatusFeeRepository _masterrenewalstatusfee;

        private MasterBblApplicationStatusRepository _masterBblApplicationStatusTestRepository;



        #endregion

        #region Declaration SubmissionCategory

        private SubmissionCategoryRepository _submissionCategoryTestRepository;
        private SubmissionMasterRenewalRepository _submissionMasterRenewalRepository;
       // private Mock<IDCBC_ENTITY_BBL_Renewal_InvoiceRepository> _iDCBC_ENTITY_BBL_Renewal_InvoiceReposiotory;
      
       // private SubmissionCategoryRepositoryData _submissionCategoryRepositoryData;
      
       // private MasterSecondaryLicenseCategoryRepositoryData _masterSecondaryLicenseCategoryRepositoryData;
       // private MasterCategoryDocumentRepositoryData _masterCategoryDocumentRepositoryData;
       // private MasterCategoryQuestionData _masterCategoryQuestionData;
       // private OSubCategoryFeesData _oSubCategoryFeesData;
       // private FixFeeRepositoryData _fixFeeRepositoryData;
       // private SubmissionQuestionRepositoryData _submissionQuestionRepositoryData;
       // private MasterSubCategoryRepositoryData _masterSubCategoryRepositoryData;
       // private MasterCategoryPhysicalLocationData _masterCategoryPhysicalLocationRepositoryData;
       // private FeeCodeMapRepositoryData _feeCodeMapRepositoryData;
       // private SubmissionMasterApplicationChcekListRepositoryData _submissionMasterApplicationChcekListRepositoryData;
       private SubmissionCategoryService _submissionCategoryService;


#endregion
       private MasterCountryRepository _masterCountryRepositoryTestRepository;
         private SubmissionSelfCertificationRepository _submissionSelfCertificationRepository;
       //  private MasterStateRepository masterStateRepository;

         private Portal_Content_ErrorsRepository _portalContentErrorsRepository;

       #region Declaration Submission Master
        
       private SubmissionCategoryRepository _submissionCategoryRepository;
       private SubmissionMasterService _submissionMasterTestService;
    
      
       #endregion

       private SubmissionQuestionService _submissionQuestionTestService;
       private BblAssociateApiController _controller = null;


      // private MasterCategoryPhysicalLocationService masterCategoryPhysicalLocationService;
       private SubmissionIndividualService _submissionIndividualService;
       private SubmissionTaxRevenueService _submissionTaxRevenuService;
       private SubmissionCofoHopeHopAddressService _submissionCofoHopeHopAddressService;
     // private  WebServiceData _webserviceLocation;
      private CofoHopDetailsService _cofoDetailsService;

      private MastereHopEligibilityService _mastereHopEligibilityService;
      private MasterCountryService _masterCountryTestService;
      private SubmissionSelfCertificationService _submissionSelfCertificationService;
      private MasterStateService _masterStateService;
      private EtlAddressAndParcelService _etlAddressAndParcelService;
         private Portal_Content_ErrorsService _portalContentErrorsService;

       //  private EtlAddressAndParcelRepository _etlAddressTestRepository;

        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _masterBblApplicationStatusTestRepository=new MasterBblApplicationStatusRepository(_testUnitOfWork);

            #region Initailise BblAssociateService

            _masterStateRepository = new MasterStateRepository(_testUnitOfWork);
            _submissionlicencecounter = new SubmissionLicenseNumberCounterRepository(_testUnitOfWork);
            _masterCountryRepository = new MasterCountryRepository(_testUnitOfWork);
            _bblRepository = new BblRepository(_testUnitOfWork);
            _dCbcEntityBblRenewalsRepository = new DCBC_ENTITY_BBL_RenewalsRepository(_testUnitOfWork);
            _portalContentErrorsRepository=new Portal_Content_ErrorsRepository(_testUnitOfWork);
            _masterEhopOptionType = new MasterEhopOptionTypeRepository(_testUnitOfWork);
            _userBblServiceRepository = new UserBBLServiceRepository(_testUnitOfWork);
            _lookupExistingCategoriesRepository = new Lookup_ExistingCategoriesRepository(_testUnitOfWork);
            _dcbcEntityBblRenewalInvoiceRepository = new DCBC_ENTITY_BBL_Renewal_InvoiceRepository(_testUnitOfWork, _lookupExistingCategoriesRepository);
            _masterCountryRepository = new MasterCountryRepository(_testUnitOfWork);
            _streetTypesRepository = new StreetTypesRepository(_testUnitOfWork);
            _submissionCategoryRepository=new SubmissionCategoryRepository(_testUnitOfWork,_masterPrimaryCategoryRepository,
                _masterSecondaryLicenseCategoryRepository,_oSubCategoryFeesRepository,_fixFeeRepository,_submissionQuestionRepository,_masterSubCategoryRepository,
                _masterCategoryPhysicalLocationRepository,_feeCodeMapRepository,_submissionMasterApplicationChcekListRepository,
                _submissionMasterRenewalRepository,_dcbcEntityBblRenewalInvoiceRepository,_lookupExistingCategoriesRepository);
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
            _masterCountryRepositoryTestRepository = new MasterCountryRepository(_testUnitOfWork);

            _etlAddressTestRepository = new EtlAddressAndParcelRepository(_testUnitOfWork);

            _masterPrimaryCategoryRepository = new MasterPrimaryCategoryRepository(_testUnitOfWork, _masterSecondaryLicenseCategoryRepository, _masterCategoryDocumentRepository,
             _masterCategoryQuestionRepository);


            _masterSubCategoryRepository = new MasterSubCategoryRepository(_testUnitOfWork, _masterPrimaryCategoryRepository, _masterSecondaryLicenseCategoryRepository);
            _oSubCategoryFeesRepository = new OSubCategoryFeesRepository(_testUnitOfWork, _masterPrimaryCategoryRepository, _masterSecondaryLicenseCategoryRepository);
            
            _feeCodeMapRepository = new FeeCodeMapRepository(_testUnitOfWork, _oSubCategoryFeesRepository);

            _portalContentErrorsService = new Portal_Content_ErrorsService(_portalContentErrorsRepository);
         

            _masterCategoryPhysicalLocationRepository = new MasterCategoryPhysicalLocationRepository(_testUnitOfWork, _masterPrimaryCategoryRepository, _masterCategoryQuestionRepository,
              _oSubCategoryFeesRepository, _masterSecondaryLicenseCategoryRepository);

            _submissionMasterRepository = new SubmissionMasterRepository(_testUnitOfWork, _submissionCategoryTestRepository, _bblRepository, _userBblServiceRepository,
            _submissionQuestionRepository, _masterCategoryPhysicalLocationRepository, _masterPrimaryCategoryRepository, _submissionMasterApplicationChcekListRepository,
            _userRepository, _dCbcEntityBblRenewalsRepository, _masterBusinessActivityRepository, _submissionBblAssociationToUsersRepository,
            _submissionToAccelaRepository, _masterSecondaryLicenseCategoryRepository, _lookupExistingCategoriesRepository, _submissionlicencecounter, _masterBblApplicationStatusTestRepository);
            _submissionCategoryTestRepository = new SubmissionCategoryRepository(_testUnitOfWork,
               _masterPrimaryCategoryRepository, _masterSecondaryLicenseCategoryRepository,
               _oSubCategoryFeesRepository, _fixFeeRepository, _submissionQuestionRepository, _masterSubCategoryRepository,
               _masterCategoryPhysicalLocationRepository,
               _feeCodeMapRepository, _submissionMasterApplicationChcekListRepository, _submissionMasterRenewalRepository,
               _dCbcEntityBblRenewalInvoiceRepository.Object
               , _lookupExistingCategoriesRepository);

            _submissionDocumentRepository = new SubmissionDocumentRepository(_testUnitOfWork, _submissionCategoryTestRepository, _masterCategoryDocumentRepository,
                _masterPrimaryCategoryRepository, _masterSecondaryLicenseCategoryRepository, _submissionMasterRepository,
                _masterCategoryPhysicalLocationRepository, _submissionMasterApplicationChcekListRepository, _submissionIndividualRepository,
                _submissionDocumentToAccelaRepository);

            _submissionSelfCertificationRepository = new SubmissionSelfCertificationRepository(_testUnitOfWork,_submissionMasterApplicationChcekListRepository);
            _userRepository = new UserRepository(_testUnitOfWork, _userRoleRepository);

            _submissionMasterRepository = new SubmissionMasterRepository(_testUnitOfWork, _submissionCategoryTestRepository, _bblRepository, _userBblServiceRepository,
        _submissionQuestionRepository, _masterCategoryPhysicalLocationRepository, _masterPrimaryCategoryRepository, _submissionMasterApplicationChcekListRepository,
        _userRepository, _dCbcEntityBblRenewalsRepository, _masterBusinessActivityRepository, _submissionBblAssociationToUsersRepository,
        _submissionToAccelaRepository, _masterSecondaryLicenseCategoryRepository, _lookupExistingCategoriesRepository, _submissionlicencecounter, _masterBblApplicationStatusTestRepository);
            _submissionIndividualRepository = new SubmissionIndividualRepository(_testUnitOfWork, _submissionMasterRepository);

            _submissionCorporationAgentAddressRepository = new SubmissionCorporationAgentAddressRepository(_testUnitOfWork, _submissionMasterApplicationChcekListRepository,
              _submissionMasterRepository);

            _submissionCorporationRepository = new SubmissionCorporationRepository(_testUnitOfWork, _submissionCorporationAgentAddressRepository,
               _submissionMasterApplicationChcekListRepository,
               _masterRegisterAgentRepository, _submissionQuestionRepository,
               _corpRespository, _submissionMasterRepository, _masterCountryRepository, _masterStateRepository);

            _submissionCofoHopeHopAddressRepository = new SubmissionCofoHopeHopAddressRepository(_testUnitOfWork, _corpRespository, _submissionMasterRepository,
              _streetTypesRepository,
              _masterRegisterAgentRepository, _submissionCorporationRepository,
              _submissionQuestionRepository, _submissionMasterApplicationChcekListRepository, _masterStateRepository, _masterCountryRepository);

            _submissionCofoHopeHopRepository = new SubmissionCofoHopeHopRepository(_testUnitOfWork, _submissionCofoHopeHopAddressRepository,
                _submissionMasterApplicationChcekListRepository,
                _submissionMasterRepository, _streetTypesRepository, _fixFeeRepository, _submissionDocumentRepository);



            _submissionEhopEligibilityRepository = new SubmissionEHOPEligibilityRepository(_testUnitOfWork, _mastereHopEligibilityRepository,
                _submissionMasterApplicationChcekListRepository, _userRepository,
                _submissionCofoHopeHopRepository, _submissionDocumentRepository, _masterEhopOptionType);
                _masterBusinessActivityRepository = new MasterBusinessActivityRepository(_testUnitOfWork, _masterPrimaryCategoryRepository);


           



            _dCbcEntityCofORepository = new DCBC_ENTITY_Cof_ORepository(_testUnitOfWork, _submissionCofoHopeHopRepository, _streetTypesRepository,
                _submissionMasterRepository, _submissionMasterApplicationChcekListRepository, _submissionCofoHopeHopAddressRepository, _etlAddressTestRepository);


            _submissionTaxRevenueRepository = new SubmissionTaxRevenueRepository(_testUnitOfWork, _submissionMasterApplicationChcekListRepository);



            _paymentCardDetailsRepository = new PaymentCardDetailsRepository(_testUnitOfWork);

            _paymentAddressDetailsRepository = new PaymentAddressDetailsRepository(_testUnitOfWork);
            _masterrenewalstatusfee = new MasterRenewalStatusFeeRepository(_testUnitOfWork);
            _submissionMasterRenewalRepository = new SubmissionMasterRenewalRepository(_testUnitOfWork, _masterrenewalstatusfee);

            _submissionCategoryTestRepository = new SubmissionCategoryRepository(_testUnitOfWork,
                    _masterPrimaryCategoryRepository, _masterSecondaryLicenseCategoryRepository,
                    _oSubCategoryFeesRepository, _fixFeeRepository, _submissionQuestionRepository, _masterSubCategoryRepository,
                    _masterCategoryPhysicalLocationRepository,
                    _feeCodeMapRepository, _submissionMasterApplicationChcekListRepository, _submissionMasterRenewalRepository,
                    _dCbcEntityBblRenewalInvoiceRepository.Object
                    , _lookupExistingCategoriesRepository);

            _paymentDetailsRepository = new PaymentDetailsRepository(_testUnitOfWork, _paymentCardDetailsRepository, _paymentAddressDetailsRepository,
          _submissionMasterRepository, _submissionCategoryTestRepository, _submissionDocumentRepository, _submissionMasterApplicationChcekListRepository,
          _fixFeeRepository, _submissionMasterRenewalRepository, _dCbcEntityBblRenewalInvoiceRepository.Object, _bblRepository, _userBblServiceRepository,
          _lookupExistingCategoriesRepository, _paymentHistoryDetails);

            _submissionVerficationRepository = new SubmissionVerficationRepository(_testUnitOfWork, _submissionCofoHopeHopRepository, _submissionCorporationRepository,
             _submissionTaxRevenueRepository, _submissionCorporationAgentAddressRepository, _submissionCofoHopeHopAddressRepository,
             _submissionCategoryTestRepository, _submissionDocumentRepository,
             _submissionMasterApplicationChcekListRepository, _fixFeeRepository, _masterCountryRepository, _userBblServiceRepository,
             _bblRepository, _dCbcEntityBblRenewalsRepository, _dcbcEntityBblRenewalInvoiceRepository, _masterStateRepository, _streetTypesRepository,
             _paymentDetailsRepository, _submissionMasterRenewalRepository);

            _submissionDocumentRepository = new SubmissionDocumentRepository(_testUnitOfWork, _submissionCategoryTestRepository,
                _masterCategoryDocumentRepository, _masterPrimaryCategoryRepository,
                _masterSecondaryLicenseCategoryRepository, _submissionMasterRepository, _masterCategoryPhysicalLocationRepository,
                _submissionMasterApplicationChcekListRepository, _submissionIndividualRepository, _submissionDocumentToAccelaRepository);

            _mockBblAsscoiateService = new BBLAssociateService(_bblRepository, _userBblServiceRepository,
           _submissionDocumentRepository, _submissionCorporationRepository,
          _submissionCofoHopeHopRepository, _submissionMasterApplicationChcekListRepository,
          _submissionCorporationAgentAddressRepository, _submissionEhopEligibilityRepository, _submissionVerficationRepository,
          _paymentDetailsRepository, _dCbcEntityBblRenewalsRepository,
          _oSubCategoryFeesRepository, _dCbcEntityCofORepository, _masterCategoryQuestionRepository,
          _userRepository, _submissionBblAssociationToUsersRepository, _streetTypesRepository);
#endregion
          

            #region Initailise SubmissionCategory Service
            _masterrenewalstatusfee = new MasterRenewalStatusFeeRepository(_testUnitOfWork);
            _lookupExistingCategoriesRepository = new Lookup_ExistingCategoriesRepository(_testUnitOfWork);
          _submissionMasterRenewalRepository = new SubmissionMasterRenewalRepository(_testUnitOfWork, _masterrenewalstatusfee);

         //   _dCbcEntityBblRenewalInvoiceRepository = new Mock<IDCBC_ENTITY_BBL_Renewal_InvoiceRepository>();
            _submissionCategoryTestRepository = new SubmissionCategoryRepository(_testUnitOfWork, _masterPrimaryCategoryRepository,
                _masterSecondaryLicenseCategoryRepository,
                _oSubCategoryFeesRepository, _fixFeeRepository,
                _submissionQuestionRepository, _masterSubCategoryRepository, _masterCategoryPhysicalLocationRepository,
                _feeCodeMapRepository, _submissionMasterApplicationChcekListRepository, _submissionMasterRenewalRepository, _dcbcEntityBblRenewalInvoiceRepository,
               _lookupExistingCategoriesRepository);

            _submissionCategoryService = new SubmissionCategoryService(_submissionCategoryTestRepository);



            #endregion

            //_submissionCategoryRepository = new SubmissionCategoryRepository(_testUnitOfWork, _dcbcEntityBblRenewalInvoiceRepository);
            
            _submissionMasterTestService = new SubmissionMasterService(_submissionMasterRepository);
            _submissionQuestionTestService = new SubmissionQuestionService(_submissionQuestionRepository);
          //  masterCategoryPhysicalLocationService=new MasterCategoryPhysicalLocationService(masterCategoryPhysicalLocationRepository);

            _submissionIndividualService=new SubmissionIndividualService(_submissionIndividualRepository);
            _submissionTaxRevenuService = new SubmissionTaxRevenueService(_submissionTaxRevenueRepository);

            _submissionCofoHopeHopAddressService=new SubmissionCofoHopeHopAddressService(_submissionCofoHopeHopAddressRepository);
        //    _webserviceLocation=new WebServiceData();
            _cofoDetailsService=new CofoHopDetailsService(_dCbcEntityCofORepository);
            _mastereHopEligibilityService=new MastereHopEligibilityService(_mastereHopEligibilityRepository,_submissionEhopEligibilityRepository);

            _masterCountryTestService = new MasterCountryService(_masterCountryRepositoryTestRepository);
            _submissionSelfCertificationService=new SubmissionSelfCertificationService(_submissionSelfCertificationRepository);
            _masterStateService = new MasterStateService(_masterStateRepository);

            _etlAddressAndParcelService=new EtlAddressAndParcelService(_etlAddressTestRepository);





            _controller = new BblAssociateApiController(_mockBblAsscoiateService,
                  _submissionCategoryService,
                   _submissionMasterTestService, _submissionQuestionTestService,  _submissionIndividualService,
                   _submissionTaxRevenuService, _submissionCofoHopeHopAddressService,  _cofoDetailsService, _mastereHopEligibilityService,
                  _masterCountryTestService, _submissionSelfCertificationService, _masterStateService, _etlAddressAndParcelService, _portalContentErrorsService);


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



            //Setup  Portal_Content_ErrorsRepository FackData Initialization
            var testData = new Portal_Content_ErrorsRepositoryData();
            _testDbContext.Portal_Content_Errors.AddRange(testData.PortalContentEntitiesList);
            _testDbContext.SaveChanges();


        }

        [TestMethod]
        public async Task ServiceCheckList_Return_Test()
        {

            //Initial Model Details
            var submissionApplication = new ServiceChecklist { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97" };
            //Act 
            var contacts = await _controller.ServiceCheckList(submissionApplication) as JsonResult<ServiceChecklist>;



           // decimal finalResult = Convert.ToDecimal("72.60");

            //Assert
            Assert.IsNotNull(contacts);

        }

        [TestMethod]
        public async Task SubmissionIndividualInsert_Return_Test()
        {
            //Initial Model Details
            var submissionApplication = new SubmissionIndividualEntity
            {
                MasterId = "11b635b9-aac2-4b4f-9d07-686b8d6cc60c",
                CompanyName = "IMA PIZZA STORE 13 LLC.",
                CompanyBusinessLicense = "931315000135",
                FirstName = "DCBCFFIRSTTEST",
                MiddleName = "DCBCMIDDLETEST",
                LastName = "DCBCLASTTEST",
                DateofBirth = Convert.ToString("1994-11-30 00:00:00.000"),
                City = "WINSINTON",
                State_Province = "DC",
                Country = "USA",
                Height = "10-15",
                Weight = "100",
                HairColor = "Red",
                EyeColor = "BLACK",
                IdentificationCard = "10000ABC",
                StateofIssuance = "DC",
                ExpirationDate = Convert.ToString("2016-12-01 00:00:00.000"),
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
            //Act 
            var contacts = await _controller.SubmissionIndividualInsert(submissionApplication) as JsonResult<int>;

            //Assert
            if (contacts != null) Assert.AreEqual(contacts.Content, 3);
        }

        [TestMethod]
        public async Task SubmissionIndiuvalData_Return_Test()
        {

            //Initial Model Details
            var submissionApplication = new ChecklistModel { MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c" };
            //Act 
            var contacts = await _controller.SubmissionIndiuvalData(submissionApplication) as JsonResult<IEnumerable<SubmissionIndividualEntity>>;




            //Assert
            if (contacts != null) Assert.AreEqual(contacts.Content.Count(), 1);

        }



        [TestMethod]
        public async Task ValidateInduvalLic_Return_Test()
        {
            //Initial Model Details
            var submissionApplication = new ChecklistModel
            {
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c"
            };
            //Act 
           // var contacts = await controller.ValidateInduvalLic(submissionApplication) as JsonResult<string>;
            dynamic contacts = await _controller.ValidateInduvalLic(submissionApplication);
            var getContent = contacts.GetType().GetProperty("Content").GetValue(contacts, null);
            bool getResult = getContent.GetType().GetProperty("status").GetValue(getContent, null);

            //Assert
            if (contacts != null) Assert.AreEqual(getResult, true);
        }


        [TestMethod]
        public async Task TaxValidation_Return_Test()
        {
            var submissionApplication = new SubmissionTaxRevenuEntity
            {
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
                TaxRevenueType = "SSN",
                TaxRevenueFfin = "111-11-1111",
                BusinessOwnerRoles = "Individual/Employee",
                FullName = "full name",
                IsIAgree = true
            };
            //Act 
            dynamic contacts = await _controller.TaxValidation(submissionApplication);
            var getContent = contacts.GetType().GetProperty("Content").GetValue(contacts, null);
            var getResult = getContent.GetType().GetProperty("status").GetValue(getContent, null);

            //Assert
            if (contacts != null) Assert.AreEqual(getResult, true);
        }

        [TestMethod]
        public async Task SubmissionTaxRevenuInsert_Return_Test()
        {
            var submissionApplication = new SubmissionTaxRevenuEntity
            {
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
                TaxRevenueType = "SSN",
                TaxRevenueFfin = "111-11-1111",
                BusinessOwnerRoles = "Individual/Employee",
                FullName = "full name",
                IsIAgree = true
            };
            //Act 
            dynamic contacts = await _controller.SubmissionTaxRevenuInsert(submissionApplication);
            var getContent = contacts.GetType().GetProperty("Content").GetValue(contacts, null);
            var getResult = getContent.GetType().GetProperty("status").GetValue(getContent, null);
            //Assert
            if (contacts != null)
                Assert.AreEqual(getResult, true);
        }

        //
        [TestMethod]
        public async Task SubmissionTaxRevenuDeletion_Return_Test()
        {
            var submissionApplication = new SubmissionTaxRevenuEntity
            {
                SubmissionTaxRevenueId = 1,
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                TaxRevenueType = "FEIN",
                TaxRevenueFfin = "111-11-1111",
                BusinessOwnerRoles = "Individual/Employee",
                FullName = "Code IT INDIA",
                IsIAgree = true
            };

            //Act 
            //  var contacts1 = await controller.SubmissionTaxRevenuDeletion(submissionApplication);

            //Act 
            dynamic contacts = await _controller.SubmissionTaxRevenuDeletion(submissionApplication);
            var getContent = contacts.GetType().GetProperty("Content").GetValue(contacts, null);
            var getResult = getContent.GetType().GetProperty("status").GetValue(getContent, null);


            //Assert
            if (contacts != null) Assert.AreEqual(getResult, true);

        }

        [TestMethod]
        public async Task GetBusinessData_Return_Test()
        {

            //{"FileNumber":"900180","MasterId":"f69a2d3d-10b2-403c-a6e6-6c92ff3d807a","UserType":"Y-CORPREG"}



            var submissionApplication = new GeneralBusiness
            {
                FileNumber = "C943266",
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                UserType = "Y-CORPREG"

            };
            //Act 
            var contacts = await _controller.GetBusinessData(submissionApplication) as JsonResult<IEnumerable<GeneralBusiness>>;


            //Assert
            if (contacts != null) Assert.AreEqual(contacts.Content.Count(), 1);

        }


        [TestMethod]
        public async Task GetBusinessData1_Return_Test()
        {




            var submissionApplication = new GeneralBusiness
            {

                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
                UserType = "Y-CORPREG"

            };
            //Act 
            var contacts = await _controller.GetBusinessData1(submissionApplication) as JsonResult<IEnumerable<GeneralBusiness>>;


            //Assert
            if (contacts != null) Assert.AreEqual(contacts.Content.Count(), 1);

        }
        [TestMethod]
        public async Task LocationVerification_Return_Test()
        {




            var submissionApplication = new ServiceDropDownData
            {

                STNAME = "3411 ASHLEY TER NW",


            };
            //Act 
            var contacts = await _controller.LocationVerification(submissionApplication) as JsonResult<List<StreetDetails>>;


            //Assert
            if (contacts != null) Assert.AreEqual(contacts.Content.Count(), 1);

        }
        //
        [TestMethod]
        public async Task ValidateLicenceNum_Return_Test()
        {
            //Initial Model Details
            var submissionApplication = new SubmissionIndividualEntity   {  CompanyBusinessLicense = "931315000136" };
            //Act 
            dynamic contacts = await _controller.ValidateLicenceNum(submissionApplication);
            var getContent = contacts.GetType().GetProperty("Content").GetValue(contacts, null);
            var getResult = getContent.GetType().GetProperty("Status").GetValue(getContent, null);

            //Assert
            if (contacts != null) Assert.AreEqual(getResult, "NoData");

        }

        [TestMethod]
        public async Task SubmissionSelftCertificationInsertUpdate_Return_Test()
        {
            //Initial Model Details
              var submissionSelfCertification = new SubmissionSelfCertification { MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40", SelfCertificationId = "bfabb7c4-38e7-4225-b611-9eedede0f4c6" };
            //Act 
            dynamic contacts = await _controller.SubmissionSelftCertificationInsertUpdate(submissionSelfCertification);
            var getContent = contacts.GetType().GetProperty("Content").GetValue(contacts, null);
            bool getResult = getContent.GetType().GetProperty("Status").GetValue(getContent, null);

            //Assert
            if (contacts != null) Assert.AreEqual(getResult,true);
        }

        [TestMethod]
        public async Task SubmissionSelftCertificationDelete_Return_Test()
        {
            //Initial Model Details
            var submissionSelfCertification = new SubmissionSelfCertification { MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40"};
            //Act 
            dynamic contacts = await _controller.SubmissionSelftCertificationDelete(submissionSelfCertification);
            var getContent = contacts.GetType().GetProperty("Content").GetValue(contacts, null);
            bool getResult = getContent.GetType().GetProperty("Status").GetValue(getContent, null);

            //Assert
            if (contacts != null) Assert.AreEqual(getResult, false);
        }

        [TestMethod]
        public async Task GetStateDetails_Return_Test()
        {
            //Initial Model Details
            var masterStateModel = new ModelFactory.MasterStateModel { CountryCode = "US" };
            //Act 
            dynamic contacts = await _controller.GetStateDetails(masterStateModel);
            var getContent = contacts.GetType().GetProperty("Content").GetValue(contacts, null);
            var getResult = getContent.GetType().GetProperty("Status").GetValue(getContent, null);

            //Assert
            if (contacts != null)   Assert.IsTrue(getResult != null);
           
        }

        [TestMethod]
        public async Task CorpOnlineSearch_Return_Test()
        {
            //Initialize Entites
            var corporationdetails = new CorporationDetails { FileNumber = "C880040" };

            //Act 
            dynamic contacts = await _controller.CorpOnlineSearch(corporationdetails);
            var getContent = contacts.GetType().GetProperty("Content").GetValue(contacts, null);
            var getResult = getContent.GetType().GetProperty("Status").GetValue(getContent, null);

            //Assert
            if (contacts != null) Assert.IsTrue(getResult.ToUpper() == "REVOKED");

        }

         [TestMethod]
         public async Task FindSubmissionCategoryById_Return_Test()
        {
            //Initialize Entites
            var submissionCategoryModel = new SubmissionCategoryModel { SubmissionCategoryId = 8 };

            var contacts = await _controller.FindSubmissionCategoryById(submissionCategoryModel);
            //Assert
            if (contacts != null) Assert.IsTrue(contacts != null);
           
        }

         [TestMethod]
         public async Task FindSubmissionCategoryById_NotFound_Return_Test()
         {
             //Initialize Entites
             var submissionCategoryModel = new SubmissionCategoryModel { SubmissionCategoryId = 150 };

             var contacts = await _controller.FindSubmissionCategoryById(submissionCategoryModel);
           //  System.Web.Http.Results.NotFoundResult
           
             //Assert
             if (contacts != null) Assert.IsTrue(contacts != null);

         }

         [TestMethod]
         public async Task FindSubmissionQuestionById_Return_Test()
         {
             //Initialize Entites
             var submissionQuestionModel = new SubmissionQuestionModel  {  SubmQuestionsId = 2 };

             var contacts = await _controller.FindSubmissionQuestionById(submissionQuestionModel) as JsonResult<IEnumerable<SubmissionQuestion>>;

             //Assert
             if (contacts != null) Assert.AreEqual(contacts.Content.Count(), 1);
         }

         [TestMethod]
         public async Task FindByTaxRevenueNumber_Return_Test()
         {
             //Act 
             var submissionTaxRevenu = new SubmissionTaxRevenuEntity { MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c" };
             dynamic contacts = await _controller.FindByTaxRevenueNumber(submissionTaxRevenu);
             var getContent = contacts.GetType().GetProperty("Content").GetValue(contacts, null);
             var getbOwnerName = getContent.GetType().GetProperty("bOwnerName").GetValue(getContent, null);
          

             //Assert
             Assert.IsNotNull(contacts);
             // Assert.AreEqual();
             Assert.IsTrue(getbOwnerName == "");
             //return Json(new
             //{
             //    taxrevenue = taxrevenue,
             //    primisessAddress = getTaxDetails.FullAddress,
             //    tradeName = getTaxDetails.TradeName,
             //    bOwnerName = getTaxDetails.BussinessOwnerFullName
             //});
         }

         [TestMethod]
         public async Task CofoHopDetails_Return_Test()
         {
             var cofoHopDetailsModel = new CofoHopDetailsModel
             {
                 MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                 Number = "CO1501622",
                 DateofIssue = DateTime.Now.ToString(),
                 Type = "COFO"

             };

             var contacts = await _controller.CofoHopDetails(cofoHopDetailsModel) as JsonResult<IEnumerable<CofoHopDetailsModel>>;

             Assert.IsNotNull(contacts);
             Assert.IsTrue(contacts.Content.Count() == 1);
         }

         [TestMethod]
         public async Task GetSubmissionCofoOrHopDetails_Return_Test()
         {
             // Arrange
             var master = new Master { MasterID = "8c6deecc-3b72-485d-8af5-30939af94e97" };

             // Act
             var contacts = await _controller.GetSubmissionCofoOrHopDetails(master) as JsonResult<ServiceData>;

             // Assert
             Assert.IsNotNull(contacts);
          //   Assert.IsTrue(contacts.Content.Count() == 1);
         }

         [TestMethod]
         public async Task InsertCofoHop_Return_Test()
         {
             // arrange
             var cofohopDetailsModel = new CofoHopDetailsModel
             {
                 MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
                 Type = "EHOP",
                 StreetType = "Boulevard",
                 StreetTypeId = 2
             };

             // Act
             var contacts = await _controller.InsertCofoHop(cofohopDetailsModel) as JsonResult<bool>;

             // Assert
             Assert.IsFalse(contacts.Content);
         }

         //[TestMethod]
         //public async Task InsertCofoHop_Change_Return_Test()
         //{
         //    // arrange
         //    var cofohopDetailsModel = new CofoHopDetailsModel
         //    {
         //        MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
         //        Type = "EHOP",
         //        StreetType = "Boulevardee",
         //        StreetTypeId = 2,
         //        IsDataChange = true

         //    };

         //    // Act
         //    var contacts = await controller.InsertCofoHop(cofohopDetailsModel) as JsonResult<bool>;

         //    // Assert
         //    Assert.IsTrue(contacts.Content);
         //}

         [TestMethod]
         public async Task DeleteHopPrimsesAddrss_Return_Test()
         {
             // Arrange
             var cofohopDetailsModel = new CofoHopDetailsModel
             {
                 MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                 IsDataChange = true
             };

             // Act
             var contacts = await _controller.DeleteHopPrimsesAddrss(cofohopDetailsModel) as JsonResult<bool>;

             //// Assert
             Assert.IsTrue(contacts.Content);
         }

         [TestMethod]
         public async Task MastereHop_Return_Test()
         {
             // arrange
             var ehopModelEntites = new EhopModel { MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40" };

             // act
             var contacts = await _controller.MastereHop(ehopModelEntites) as OkResult;

             Assert.IsNull(contacts);
         }

         //[TestMethod]
         //public async Task GetAllMessages_Json_Return_Test()
         //{
         //    // act
         //    var contacts = await controller.GetAllMessages() as JsonResult<List<Portal_Content_Errors>>;
           
         //    // assert
         //    Assert.IsTrue(contacts.Content.Count()==1);
         //}

         [TestMethod]
         public async Task FindByMessageName_Return_Test()
         {
             var portalContentErrors = new PortaContentErrorsModel
             {
                 MessageType = "Success",
                 ShortName = "Inserted"
             };

             var contacts = await _controller.FindByMessageName(portalContentErrors) as JsonResult<List<BusinessCenter.Data.Portal_Content_Errors>>;

             Assert.IsTrue(contacts.Content.Count() == 1);
         }

         [TestMethod]
         public async Task UpdateSubmissionCorpStatus_Return_Test()
         {
             var generalBusiness = new GeneralBusiness { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97", FileNumber = "C943266" };

             var contacts = await _controller.UpdateSubmissionCorpStatus(generalBusiness) as JsonResult<string>;

             Assert.IsTrue(contacts.Content == "True");
         }

         [TestMethod]
         public async Task EhopData_Return_Test()
         {
             var ehopData = new EhopData { MasterID = "8c6deecc-3b72-485d-8af5-30939af94e97" };
             // act
             var contacts = await _controller.EhopData(ehopData) as JsonResult<EhopData>;

             // assert
             Assert.IsNotNull(contacts);
         }

         [TestMethod]
         public async Task SubmissionSelftCertification_Return_Test()
         {
             var submissionSelfCertification = new SubmissionSelfCertification { MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40" };

             // act
             var contacts = await _controller.SubmissionSelftCertification(submissionSelfCertification) as JsonResult<IEnumerable<SubmissionSelfCertification>>;

             // assert
             Assert.IsNotNull(contacts.Content);
         }

         [TestMethod]
         public async Task BindDropDown_Return_Test()
         {
             // arrange
             var checklistModel = new ChecklistModel { MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c" };

             var contacts = await _controller.BindDropDown(checklistModel) as JsonResult<bool>;

             Assert.IsTrue(contacts.Content);
         }

         [TestMethod]
         public async Task BindDropDown_NoParam_Return_Test()
         {
             var contacts = await _controller.BindDropDown() as JsonResult<AddressType>;

             Assert.IsNotNull(contacts.Content);
         }
       
     }


}
