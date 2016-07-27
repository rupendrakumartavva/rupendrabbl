using System;
//using BusinessCenter.Data;
//using BusinessCenter.Data.Interface;
using BusinessCenter.Email;
//using BusinessCenter.Identity.Interfaces;
//using BusinessCenter.Service.Interface;
//using BussinessCenter.reCaptcha;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
//using System.Net.Http;
using System.Threading.Tasks;
//using System.Web;
//using System.Web.Http;
using System.Web.Http.Results;
//using System.Web.Mvc;
using BusinessCenter.Api.Controllers;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Model;
using BusinessCenter.Service.Implementation;
using BusinessCenter.Tests.Setup;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
using BusinessCenter.Api.PayPalUtility;
//using BusinessCenter.Api.Utility;


namespace BusinessCenter.Tests.Controllers
{
      [TestClass]
  public  class BblAppAssociationControllerTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

     
        private MasterRenewalStatusFeeRepository _masterRenewalStatusFeeRepository;

        #region Declaration Of BBLAssociateService
        private BBLAssociateService _bblAssociateTestService;

        private BblRepository _bblRepository;
        private UserBBLServiceRepository _userBblServiceTestRepository;
        private SubmissionDocumentRepository _submissionDocumentRepository;
        private SubmissionCofoHopeHopRepository _submissionCofoHopeHopRepository;
        private SubmissionMasterApplicationChcekListRepository _submissionMasterApplicationChcekListRepository;
        private SubmissionCorporationRepository _submissionCorporationRepository;
        private SubmissionVerficationRepository _submissionVerficationRepository;
        private SubmissionEHOPEligibilityRepository _submissionEhopEligibilityRepository;
        private PaymentDetailsRepository _paymentDetailsRepository;
        private SubmissionCorporationAgentAddressRepository _submissionCorporationAgentAddressRepository;
        private DCBC_ENTITY_BBL_RenewalsRepository _dCbcEntityBblRenewalsRepository;
        private UserRepository _userRepository;
        private DCBC_ENTITY_Cof_ORepository _dCbcEntityCofORepository;
        private MasterCategoryQuestionRepository _masterCategoryQuestionRepository;
        private SubmissionBblAssociationToUsersRepository _submissionBblAssociationToUsersRepository;
      //  private MasterBusinessActivityRepository _masterBusinessActivityRepository;
        private MasterCategoryDocumentRepository _masterCategoryDocumentRepository;
       // private MasterPrimaryCategoryRepository masterPrimaryCategoryRepository;
        private MasterSecondaryLicenseCategoryRepository _masterSecondaryLicenseCategoryRepository;
        private MasterCategoryPhysicalLocationRepository _masterCategoryPhysicalLocationRepository;
        private SubmissionMasterRepository _submissionMasterRepository;
        private SubmissionCategoryRepository _submissionCategoryRepository;
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
      //  private MasterPrimaryCategoryRepositoryData _masterPrimaryCategoryRepository;
    //    private MasterSecondaryLicenseCategoryRepositoryData _mockPrimaryData1;
      //  private DCBC_ENTITY_BBL_RenewalsData _mockDcbcBblRenewal;
        private MasterSubCategoryRepository _masterSubCategoryRepository;
        private FeeCodeMapRepository _feeCodeMapRepository;
        private SubmissionToAccelaRepository _submissionToAccelaRepository;
        private UserRoleRepository _userRoleRepository;
       // private Mock<IDCBC_ENTITY_BBL_Renewal_InvoiceRepository> _dCbcEntityBblRenewalInvoiceRepository;
      //  private MasterCountryRepository _masterCountryRepository;
        private MasterEhopOptionTypeRepository _masterEhopOptionType;
     
        private DCBC_ENTITY_BBL_Renewal_InvoiceRepository _dcbcEntityBblRenewalInvoiceRepository;

        private MasterPrimaryCategoryRepository _masterPrimaryCategoryRepository;
        private MasterStateRepository _masterStateRepository;
        private MasterCountryRepository _masterCountryRepository;
       // private EtlAddressAndParcelRepository _etlAddressTestRepository;
        private PaymentHistoryDetailsRepository _paymentHistoryDetails;
    
        #endregion


        #region Declaration Of MasterBusinessActivityService
      //  private MasterBusinessActivityService _mockBusinessActivityTestService;

        private MasterBusinessActivityRepository _masterBusinessActivityTestRepository;
        #endregion


        #region SubmissionMasterRepo
     
        private Lookup_ExistingCategoriesRepository _lookupExistingCategoriesRepository;
        
          private MasterSecondaryLicenseCategoryRepository _masterSecondaryLicenseCategory;
        private SubmissionMasterService _submissionMasterTestService;
       
    
        //Repository declaration
        private SubmissionLicenseNumberCounterRepository _submissionLicenseNumberCounterRepository;
        private MasterBblApplicationStatusRepository _masterBblApplicationStatusRepository;
        #endregion

        #region Declaration Of SubmissionGeneratedDocumentService
        private SubmissionGeneratedDocumentRepository _submissionGeneratedDocumentTestRepository;
        private SubmissionGeneratedDocumentService _submissionGeneratedDocumentTestService;
        #endregion

        #region Declaration Of SubmissionDocumentToAccelaService
        private SubmissionDocumentToAccelaService _submissionDocumentToAccelaTestService;
        #endregion

        #region Declaration Of MailTemplateService
        private MailTemplateRepository _mailTemplateTestRepository;
        private MailTemplateService _mailTemplateTestService;
        #endregion

        private PayPalHelper _payhelper;
        private  EmailTemplate _mailDetails;
          private EmailSending _sendemail;
        #region Declaration Of SubmissionTaxRevenueService
        private SubmissionTaxRevenueService _submissionTaxRevenueTestService;
        #endregion

          private OSubCategoryFeesRepository _oSubCategoryFeesRepository;
        private BblAppAssociationController _controller ;
          private EtlAddressAndParcelRepository _etlAddressAndParcelRepository;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
          
            _sendemail = new EmailSending();
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _masterCategoryQuestionRepository = new MasterCategoryQuestionRepository(_testUnitOfWork);
            _mailDetails = new EmailTemplate(_sendemail);
            _submissionCorporationAgentAddressRepository = new SubmissionCorporationAgentAddressRepository(_testUnitOfWork, _submissionMasterApplicationChcekListRepository, _submissionMasterRepository);
            _streetTypesRepository=new StreetTypesRepository(_testUnitOfWork);
            _submissionVerficationRepository=new SubmissionVerficationRepository(_testUnitOfWork,_submissionCofoHopeHopRepository,_submissionCorporationRepository,_submissionTaxRevenueRepository,
                _submissionCorporationAgentAddressRepository,_submissionCofoHopeHopAddressRepository,_submissionCategoryRepository,
                _submissionDocumentRepository,_submissionMasterApplicationChcekListRepository,_fixFeeRepository,_masterCountryRepository,
                _userBblServiceTestRepository,_bblRepository,_dCbcEntityBblRenewalsRepository,_dcbcEntityBblRenewalInvoiceRepository,_masterStateRepository,
                _streetTypesRepository,_paymentDetailsRepository,_submissionMasterRenewalRepository);
            _etlAddressAndParcelRepository = new EtlAddressAndParcelRepository(_testUnitOfWork);
            _dCbcEntityCofORepository=new DCBC_ENTITY_Cof_ORepository(_testUnitOfWork,_submissionCofoHopeHopRepository,_streetTypesRepository,
                _submissionMasterRepository, _submissionMasterApplicationChcekListRepository, _submissionCofoHopeHopAddressRepository, _etlAddressAndParcelRepository);
            _masterStateRepository=new MasterStateRepository(_testUnitOfWork);
            _masterCountryRepository=new MasterCountryRepository(_testUnitOfWork);
            _masterSecondaryLicenseCategory=new MasterSecondaryLicenseCategoryRepository(_testUnitOfWork);
            _lookupExistingCategoriesRepository=new Lookup_ExistingCategoriesRepository(_testUnitOfWork);
            _masterPrimaryCategoryRepository = new MasterPrimaryCategoryRepository(_testUnitOfWork, _masterSecondaryLicenseCategory, _masterCategoryDocumentRepository,_masterCategoryQuestionRepository);
            //_dCbcEntityBblRenewalInvoiceRepository=new DCBC_ENTITY_BBL_Renewal_InvoiceRepository(_testUnitOfWork,_Lookup_ExistingCategoriesRepository);
            _masterBblApplicationStatusRepository=new MasterBblApplicationStatusRepository(_testUnitOfWork);
            _userRoleRepository = new UserRoleRepository(_testUnitOfWork);
            _masterRegisterAgentRepository=new MasterRegisterAgentRepository(_testUnitOfWork);
            _userRepository = new UserRepository(_testUnitOfWork, _userRoleRepository);
            _masterCategoryQuestionRepository = new MasterCategoryQuestionRepository(_testUnitOfWork);
            _submissionMasterApplicationChcekListRepository = new SubmissionMasterApplicationChcekListRepository(_testUnitOfWork);
            _userBblServiceTestRepository = new UserBBLServiceRepository(_testUnitOfWork);
            //_masterBusinessActivityRepository=new MasterBusinessActivityRepository(_testUnitOfWork,_masterPrimaryCategoryRepository);
            _bblRepository = new BblRepository(_testUnitOfWork);
            _mastereHopEligibilityRepository=new MastereHopEligibilityRepository(_testUnitOfWork);
            _masterSubCategoryRepository=new MasterSubCategoryRepository(_testUnitOfWork,_masterPrimaryCategoryRepository,_masterSecondaryLicenseCategory);
            _payhelper = new PayPalHelper();
        
            
            _masterEhopOptionType = new MasterEhopOptionTypeRepository(_testUnitOfWork);
            #region Initialization Of MasterBusinessActivityService
            _masterBusinessActivityTestRepository = new MasterBusinessActivityRepository(_testUnitOfWork, _masterPrimaryCategoryRepository);
            _submissionCofoHopeHopAddressRepository = new SubmissionCofoHopeHopAddressRepository(_testUnitOfWork, _corpRespository, _submissionMasterRepository,
                _streetTypesRepository,_masterRegisterAgentRepository,_submissionCorporationRepository,_submissionQuestionRepository,
                _submissionMasterApplicationChcekListRepository,_masterStateRepository,_masterCountryRepository);
            //_mockBusinessActivityTestService = new MasterBusinessActivityService(_masterBusinessActivityTestRepository);
        #endregion
            #region Initialization Of SubmissionDocumentToAccelaService
            _submissionDocumentToAccelaRepository = new SubmissionDocumentToAccelaRepository(_testUnitOfWork);
            _submissionDocumentToAccelaTestService = new SubmissionDocumentToAccelaService(_submissionDocumentToAccelaRepository);
            #endregion

            #region Initialization Of SubmissionTaxRevenueService
            _submissionTaxRevenueRepository = new SubmissionTaxRevenueRepository(_testUnitOfWork,_submissionMasterApplicationChcekListRepository);
            _submissionTaxRevenueTestService = new SubmissionTaxRevenueService(_submissionTaxRevenueRepository);
            #endregion

            _masterSecondaryLicenseCategoryRepository = new MasterSecondaryLicenseCategoryRepository(_testUnitOfWork);
            _submissionMasterApplicationChcekListRepository = new SubmissionMasterApplicationChcekListRepository(_testUnitOfWork);
            _masterCategoryDocumentRepository = new MasterCategoryDocumentRepository(_testUnitOfWork);

            _masterPrimaryCategoryRepository = new MasterPrimaryCategoryRepository(_testUnitOfWork,
           _masterSecondaryLicenseCategoryRepository,
           _masterCategoryDocumentRepository, _masterCategoryQuestionRepository);
            //CorpRespository Initialization
            _corpRespository = new CorpRespository(_testUnitOfWork);

            _submissionQuestionRepository = new SubmissionQuestionRepository(_testUnitOfWork);
            _masterRenewalStatusFeeRepository = new MasterRenewalStatusFeeRepository(_testUnitOfWork);
            _submissionMasterRenewalRepository = new SubmissionMasterRenewalRepository(_testUnitOfWork, _masterRenewalStatusFeeRepository);
             _lookupExistingCategoriesRepository = new Lookup_ExistingCategoriesRepository(_testUnitOfWork);
             _dcbcEntityBblRenewalInvoiceRepository = new DCBC_ENTITY_BBL_Renewal_InvoiceRepository(_testUnitOfWork, _lookupExistingCategoriesRepository);
             _fixFeeRepository = new FixFeeRepository(_testUnitOfWork);
              _dCbcEntityBblRenewalsRepository = new DCBC_ENTITY_BBL_RenewalsRepository(_testUnitOfWork);

            _submissionBblAssociationToUsersRepository = new SubmissionBblAssociationToUsersRepository(_testUnitOfWork);
            _submissionToAccelaRepository = new SubmissionToAccelaRepository(_testUnitOfWork);
            _oSubCategoryFeesRepository = new OSubCategoryFeesRepository(_testUnitOfWork, _masterPrimaryCategoryRepository,_masterSecondaryLicenseCategory);
            _feeCodeMapRepository = new FeeCodeMapRepository(_testUnitOfWork, _oSubCategoryFeesRepository);
            #region Initialization Of MasterCategoryPhysicalLocationRepository
            _masterCategoryPhysicalLocationRepository = new MasterCategoryPhysicalLocationRepository(_testUnitOfWork,
              _masterPrimaryCategoryRepository, _masterCategoryQuestionRepository, _oSubCategoryFeesRepository, _masterSecondaryLicenseCategoryRepository);
            #endregion





            _submissionMasterRepository = new SubmissionMasterRepository(_testUnitOfWork, _submissionCategoryRepository, _bblRepository,
               _userBblServiceTestRepository, _submissionQuestionRepository, _masterCategoryPhysicalLocationRepository, _masterPrimaryCategoryRepository,
               _submissionMasterApplicationChcekListRepository, _userRepository, _dCbcEntityBblRenewalsRepository, _masterBusinessActivityTestRepository,
               _submissionBblAssociationToUsersRepository, _submissionToAccelaRepository, _masterSecondaryLicenseCategoryRepository,
                _lookupExistingCategoriesRepository, _submissionLicenseNumberCounterRepository, _masterBblApplicationStatusRepository);

            _submissionIndividualRepository = new SubmissionIndividualRepository(_testUnitOfWork, _submissionMasterRepository);

             #region submissionCorporationRepository

             _submissionCorporationRepository = new SubmissionCorporationRepository(_testUnitOfWork,
            _submissionCorporationAgentAddressRepository,
            _submissionMasterApplicationChcekListRepository,
            _masterRegisterAgentRepository,
            _submissionQuestionRepository,
            _corpRespository,
            _submissionMasterRepository, _masterCountryRepository, _masterStateRepository
            );
             #endregion

             #region Initialization Of SubmissionCategoryRepository
             _submissionCategoryRepository = new SubmissionCategoryRepository(_testUnitOfWork, _masterPrimaryCategoryRepository,
       _masterSecondaryLicenseCategoryRepository, _oSubCategoryFeesRepository, _fixFeeRepository, _submissionQuestionRepository,
       _masterSubCategoryRepository, _masterCategoryPhysicalLocationRepository, _feeCodeMapRepository,
       _submissionMasterApplicationChcekListRepository, _submissionMasterRenewalRepository, _dcbcEntityBblRenewalInvoiceRepository,
        _lookupExistingCategoriesRepository);
            #endregion

            #region Initialization of submissionDocumentRepository
            _submissionDocumentRepository = new SubmissionDocumentRepository(_testUnitOfWork, _submissionCategoryRepository, _masterCategoryDocumentRepository,
              _masterPrimaryCategoryRepository, _masterSecondaryLicenseCategoryRepository, _submissionMasterRepository, _masterCategoryPhysicalLocationRepository,
              _submissionMasterApplicationChcekListRepository, _submissionIndividualRepository, _submissionDocumentToAccelaRepository);
            #endregion
            _paymentCardDetailsRepository = new PaymentCardDetailsRepository(_testUnitOfWork);
            _paymentAddressDetailsRepository = new PaymentAddressDetailsRepository(_testUnitOfWork);
            _paymentHistoryDetails = new PaymentHistoryDetailsRepository(_testUnitOfWork);

            #region  Initialization of paymentDetailsRepository
            _paymentDetailsRepository = new PaymentDetailsRepository(_testUnitOfWork,
            _paymentCardDetailsRepository,
            _paymentAddressDetailsRepository,
            _submissionMasterRepository,
           _submissionCategoryRepository,
           _submissionDocumentRepository,
           _submissionMasterApplicationChcekListRepository,
          _fixFeeRepository,
          _submissionMasterRenewalRepository,
          _dcbcEntityBblRenewalInvoiceRepository,
         _bblRepository,
        _userBblServiceTestRepository,
        _lookupExistingCategoriesRepository, _paymentHistoryDetails);
            #endregion
           

            #region Initialization of SubmissionCofoHopeHopRepository
            _submissionCofoHopeHopRepository = new SubmissionCofoHopeHopRepository(_testUnitOfWork, _submissionCofoHopeHopAddressRepository, _submissionMasterApplicationChcekListRepository,
                _submissionMasterRepository, _streetTypesRepository, _fixFeeRepository, _submissionDocumentRepository);
            #endregion

            _submissionEhopEligibilityRepository = new SubmissionEHOPEligibilityRepository(_testUnitOfWork, _mastereHopEligibilityRepository, _submissionMasterApplicationChcekListRepository,
               _userRepository, _submissionCofoHopeHopRepository, _submissionDocumentRepository, _masterEhopOptionType);

            #region Initialization Of BBLAssociateService
            _bblAssociateTestService = new BBLAssociateService(_bblRepository, _userBblServiceTestRepository,
            _submissionDocumentRepository, _submissionCorporationRepository,
           _submissionCofoHopeHopRepository, _submissionMasterApplicationChcekListRepository,
           _submissionCorporationAgentAddressRepository, _submissionEhopEligibilityRepository, _submissionVerficationRepository,
           _paymentDetailsRepository, _dCbcEntityBblRenewalsRepository,
           _oSubCategoryFeesRepository, _dCbcEntityCofORepository, _masterCategoryQuestionRepository,
           _userRepository, _submissionBblAssociationToUsersRepository, _streetTypesRepository);
            #endregion

            #region SubmissionMasterRepoInitialization

            _submissionLicenseNumberCounterRepository = new SubmissionLicenseNumberCounterRepository(_testUnitOfWork);


            _submissionMasterRepository = new SubmissionMasterRepository(_testUnitOfWork, _submissionCategoryRepository, _bblRepository,
                _userBblServiceTestRepository, _submissionQuestionRepository, _masterCategoryPhysicalLocationRepository, _masterPrimaryCategoryRepository,
                _submissionMasterApplicationChcekListRepository, _userRepository, _dCbcEntityBblRenewalsRepository, _masterBusinessActivityTestRepository,
                _submissionBblAssociationToUsersRepository, _submissionToAccelaRepository, _masterSecondaryLicenseCategoryRepository,
                 _lookupExistingCategoriesRepository, _submissionLicenseNumberCounterRepository, _masterBblApplicationStatusRepository);

            _submissionMasterTestService = new SubmissionMasterService(_submissionMasterRepository);

            #endregion
          


            #region Initialization Of SubmissionGeneratedDocumentService
            _submissionGeneratedDocumentTestRepository = new SubmissionGeneratedDocumentRepository(_testUnitOfWork);
            _submissionGeneratedDocumentTestService = new SubmissionGeneratedDocumentService(_submissionGeneratedDocumentTestRepository);
        #endregion

            

            #region Initialization Of MailTemplateService
            _mailTemplateTestRepository = new MailTemplateRepository(_testUnitOfWork);
            _mailTemplateTestService = new MailTemplateService(_mailTemplateTestRepository);
            #endregion
          
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




            ////------------------






            var fixFeeRepositoryData = new FixFeeRepositoryData();
            _testDbContext.FixFee.AddRange(fixFeeRepositoryData.FixFeeEntitiesList);
            _testDbContext.SaveChanges();


            var masterSubCategoryRepositoryData = new MasterSubCategoryRepositoryData();
            _testDbContext.MasterSubCategory.AddRange(masterSubCategoryRepositoryData.MasterSubCategoryList);
            _testDbContext.SaveChanges();


            var feeCodeMapRepositoryData = new FeeCodeMapRepositoryData();
            _testDbContext.FeeCodeMap.AddRange(feeCodeMapRepositoryData.FeeCodeMapEntitiesList);
            _testDbContext.SaveChanges();

            var mailTemplateData = new MailTemplateRepositoryData();
            _testDbContext.MailTemplate.AddRange(mailTemplateData.MailTemplateEntitiesList);
            _testDbContext.SaveChanges();


            //Setup  SubmissionGeneratedDocumentRepository FackData Initialization
            var submissionDocumentData = new SubmissionGeneratedDocumentRepositoryData();
            _testDbContext.SubmissionGeneratedDocument.AddRange(submissionDocumentData.SubmissionDocumentEntitiesList);
            _testDbContext.SaveChanges();


            //Setup  SubmissionDocumentToAccela FackData Initialization
            var submissionAccelaData = new SubmissionDocumentToAccelaData();
            _testDbContext.SubmissionDocumentToAccela.AddRange(submissionAccelaData.SubmissionDocumentToAccelaEntitiesList);
            _testDbContext.SaveChanges();

            //Setup SubmissionTaxRevenue FackData Initialization
            var submissionTaxRevenueData = new SubmissionTaxRevenueData();
            _testDbContext.SubmissionTaxRevenue.AddRange(submissionTaxRevenueData.SubmissionTaxRevenueEntitiesList);
            _testDbContext.SaveChanges();

           
            var payMentDetailsData = new PaymentDetailsData();
            _testDbContext.PaymentDetails.AddRange(payMentDetailsData.PaymentDetailsList);
            _testDbContext.SaveChanges();

           
            var paymentCardData = new PaymentCardDetailsData();
            _testDbContext.PaymentCardDetails.AddRange(paymentCardData.PaymentDetailsList);
            _testDbContext.SaveChanges();

            var paymentAddressDetailsData = new PaymentAddressDetailsData();
            _testDbContext.PaymentAddressDetails.AddRange(paymentAddressDetailsData.PaymentAddressDetailsList);
            _testDbContext.SaveChanges();

            //Setup  SubmissionCorporation_Agent FackData Initialization
            var submissionCorpData = new SubmissionCorporationRepositoryData();
            _testDbContext.SubmissionCorporation_Agent.AddRange(submissionCorpData.SubmissionCorporationEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  DCBC_ENTITY_CORP FackData Initialization
            var corpData = new CorpRepositoryData();
            _testDbContext.DCBC_ENTITY_CORP.AddRange(corpData.CorpEntitiesList);
            _testDbContext.SaveChanges();

            //Setup FackData SubmissionCofo_Hop_Ehop
            var submissionCofoData = new SubmissionCofoHopeHopData();
            _testDbContext.SubmissionCofo_Hop_Ehop.AddRange(submissionCofoData.SubmissionCofoHopEhopList);
            _testDbContext.SaveChanges();

            //Setup  SubmissionMaster_ApplicationCheckList FackData Initialization
            var submissionCheckListData = new SubmissionMasterApplicationChcekListRepositoryData();
            _testDbContext.SubmissionMaster_ApplicationCheckList.AddRange(submissionCheckListData.SubmissionMaster_ApplicationCheckListEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  SubmissionEHOPEligibility FackData Initialization
            // ReSharper disable once InconsistentNaming
            var submissionEHOPData = new SubmissionEHOPEligibilityRepositoryData();
            _testDbContext.SubmissionEHOPEligibility.AddRange(submissionEHOPData.SubmissionEHOPEligibilityEntitiesList);
            _testDbContext.SaveChanges();

            var bblData= new BblRepositoryData();
            _testDbContext.DCBC_ENTITY_BBL.AddRange(bblData.BblEntitiesList);
            _testDbContext.SaveChanges();
           


            _controller = new BblAppAssociationController(_bblAssociateTestService,
                      _submissionMasterTestService, _submissionTaxRevenueTestService,
                        _payhelper, _mailDetails, _submissionGeneratedDocumentTestService, _submissionDocumentToAccelaTestService, _mailTemplateTestService);

        }
        [TestMethod]
        public async Task SubmissionStatus_Return_Test()
        {
            //Initial Model Details
            var generalbusiness = new GeneralBusiness { MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40" };
            //Act 
            var contacts = await _controller.SubmissionStatus(generalbusiness) as JsonResult<IEnumerable<SubmissionData>>;
            //Assert
            if (contacts != null) Assert.AreEqual(contacts.Content.ToList().Count(),1);
        }

        [TestMethod]
        public async Task RemoveCofoTest()
        {
            //Initialize data to model
            var cofohopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc611",
                Type = "COFO",
                DonothaveCof = false
            };

            //Act 
            var submissioncofo = await _controller.RemoveCofo(cofohopDetailsModel) as JsonResult<bool>;

            //Assert
            if (submissioncofo != null) Assert.AreEqual(submissioncofo.Content, true);
        }
        [TestMethod]
        public async Task BblRemoveServiceListTest()
        {
            //Initial Model Details
            var bblAsscoiateService = new BblAsscoiateService
            {
                UserID = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F",
                SubmissionLicense = "DAPP15985360",
                DCBC_ENTITY_ID = "1",
            };
            //Act 
            var userbblService = await _controller.BblRemoveServiceList(bblAsscoiateService) as JsonResult<bool>;

            //Assert
            if (userbblService != null) Assert.AreEqual(userbblService.Content, true);
        }
        [TestMethod()]
        public async Task UpdateIsCofoTest()
        {
            //Initial Model data Details
            var cofoHopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Type = "COFO"
            };
            //Act 
            var serviceChecklist = await _controller.UpdateIsCofo(cofoHopDetailsModel) as JsonResult<bool>;

            //Assert
            if (serviceChecklist != null) Assert.AreEqual(serviceChecklist.Content, true);
        }
        [TestMethod()]
        public async Task UpdateAllCheckListConditionsTrueTest()
        {
            //Initial Model Details
            var cofoHopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97"
            };

            //Act 
            var serviceChecklist = await _controller.UpdateChecklistAppConditions(cofoHopDetailsModel) as JsonResult<bool>;

            //Assert
            if (serviceChecklist != null) Assert.AreEqual(serviceChecklist.Content, true);
        }
        [TestMethod]
        public async Task SubmitHAorPaTest()
        {
            //Initial Model Details
            var generalBusiness = new GeneralBusiness { MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c", UserSelectTpe = "NEWMAIL" };

            //Act 
            var submissionmaster = await _controller.SubmitHAorPa(generalBusiness) as JsonResult<bool>;

            //Assert
            if (submissionmaster != null) Assert.AreEqual(submissionmaster.Content, true);
        }
        [TestMethod]
        public async Task InsertEhopEligibilities_Return_Test()
        {
           //Initialize Entites
            var eligibilityModelEntites = new EligibilityModel { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97", EhopIds = "1,2,3,4,5,6,7,8,9,10,11,12", TypeId = 1 };

            //Act 
            dynamic ehopRows = await _controller.InsertEhopEligibilities(eligibilityModelEntites);

            var content = ehopRows.GetType().GetProperty("Content").GetValue(ehopRows, null);
            var result = content.GetType().GetProperty("Result").GetValue(content, null);

            //Assert
            Assert.IsNotNull(ehopRows);
            Assert.AreEqual(result, true);
        
        }
        [TestMethod]
        public async Task UpdateSubmissionMasterTest()
        {
            //Initial Data is added.
            var bblDocuments = new BblDocuments
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                DocSubType = "IN"
            };
            //Act 
            var submissionDocumentsRows = await _controller.UpdateSubmissionMaster(bblDocuments) as JsonResult<bool>;
            //Assert
            if (submissionDocumentsRows != null) Assert.AreEqual(submissionDocumentsRows.Content, true);
        }
        //[TestMethod]
        //public async Task InsertServiceDocuments_UpdateExistingDocument_Test()
        //{
        //    //Initialize Entites
        //    var document = new BblServiceDocuments
        //    {
        //        MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
        //        SubmissionCategoryID = 3,
        //        DocRequired = "Certified Food Supervisor Certification",
        //        FileName = "9313_DAPP15986431_DOH_HRLA_FoodSupCert12.pdf",
        //        FileLocation = "BBLUpload//",
        //        SubmissionId = "2",
        //        CategoryID = "304",

        //        Description = "Caterers must have Certified Food Supervisor(s) present on the premises during the hours the facility is open to the public. The name(s) and certification(s) of the food supervisor employee(s) must be provided to the Department of Health's inspectors.",
        //    };

        //    //Act 
        //    var submissionDocumentsRows = await controller.BblServiceDocumentList() as JsonResult<UploadStatus>;
        //    //Assert
        //    Assert.IsNotNull(submissionDocumentsRows); // Test if null
        //    Assert.IsTrue(submissionDocumentsRows.Content.FileName == "9313_DAPP15986431_DOH_HRLA_FoodSupCert12.pdf");
        //}
        [TestMethod]
        public async Task BblRequiredDocuments_Return_Test()
        {
            //Initialize Entities
            var bblDocuments = new BblDocuments
            {
                
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40"

            };

            //Act 
            dynamic submissionDocumentsRows = await _controller.BblRequiredDocuments(bblDocuments);

            var content = submissionDocumentsRows.GetType().GetProperty("Content").GetValue(submissionDocumentsRows, null);
            // ReSharper disable once UnusedVariable
            var result = content.GetType().GetProperty("result").GetValue(content, null);
            var validateCorpFileStatus = content.GetType().GetProperty("validateCorpFileStatus").GetValue(content, null);
            var corpChangeStatus = content.GetType().GetProperty("CorpChangeStatus").GetValue(content, null);// REVOKED


            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.AreEqual(validateCorpFileStatus, "REVOKED");
            Assert.AreEqual(corpChangeStatus, "REVOKED");
        }
        [TestMethod]
        public async Task BblRequiredDocumentsTest()
        {
            //Initialize Entities
            var generalBusiness = new GeneralBusiness { MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c", UserSelectTpe = "NEWMAIL" };

            //Act 
            var documentRows = await _controller.BblRequiredDocuments(generalBusiness) as JsonResult<GeneralBusiness>;

            //Assert
            Assert.IsNotNull(documentRows);
            Assert.IsTrue(documentRows.Content.UserType == "NEWMAIL");
        }
        [TestMethod]
        public async Task DeleteDocuments_Test()
        {
            //Initialize Entities
            var document = new BblServiceDocuments
            {
                SubmissionCategoryID = 1,
                CategoryID = "151",
                DocRequired = "Fire Marshal Inspection Approval",
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Description = "All businesses that manufacture, wholesale, warehouse, distribute, sell or use toxic or flammable materials or provide services to large audiences (theatres, restaurants, and public halls) are subject to inspection to ensure the safety of customers, employees, and property."

            };
            //Act 
            var submissionDocumentsRows = await _controller.DeleteDocument(document) as JsonResult<bool>;
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows.Content);
        }
          [TestMethod]
        public async Task SubmitPayment_Return_Test()
        {
            //Initialize Entities
            var paymentDetailsEntity = new PaymentDetailsModel
            {

                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                OrderNumber = "DCRABBLORDER15953919",
                PaymentType = "submission",
                PaymentMailAddress = "Test123@gmail.com",
                Signature = "Codeit",
                IsAggree = true,
                TranscationId = "Test-Transaction",
                PaymentStatus = "Test Approval",
                PaymentDate = Convert.ToDateTime("2015-12-15 07:52:14.063"),
                ApproveBy = "",
                Description = ""
            };

            //Act
            dynamic paymetRows = await _controller.SubmitPayment(paymentDetailsEntity);

            var content = paymetRows.GetType().GetProperty("Content").GetValue(paymetRows, null);
            var trasactionresult = content.GetType().GetProperty("trasactionresult").GetValue(content, null);
            var finalsuccess = content.GetType().GetProperty("finalsuccess").GetValue(content, null);
            var masterTextRevenue = content.GetType().GetProperty("masterTextRevenue").GetValue(content, null);
            var validateCorpFileStatus = content.GetType().GetProperty("validateCorpFileStatus").GetValue(content, null);

            //Assert
            Assert.IsNotNull(paymetRows);
            Assert.AreEqual(trasactionresult, false);
            Assert.AreEqual(finalsuccess, "NO");
            Assert.AreEqual(masterTextRevenue, false);
            Assert.AreEqual(validateCorpFileStatus, "REVOKED");
        }
          [TestMethod]
          public async Task InsertPaymentDetails_Return_Test()
          {

              //Initialize Entities
              var paymentDetailsEntity = new PaymentDetailsModel
              {

                  MasterId = "87e53e0c-c940-4a7f-a05b-6c79a0f82346",
                  TotalAmount = "154",
                  CardType = "1",
                  CardName = "abc",
                  CardNumber = "1111-1111-1111-1111",
                  OrderNumber = "DCRABBLORDER15953919",
                  CardExpYear = "2020",
                  CardExpMonth = "12",
                  CvvNumber = "111",
                  FullName = "abc",
                  BusinessName = "abc",
                  FullAddress = "FullAddress",
                  ContactFirstName = "ContactFirstName",
                  ContactMiddleName = "ContactMiddleName",
                  ContactLastName = "ContactLastName",
                  Quadrant = "",
                  StreetName = "StreetName",
                  StreetNumber = "StreetNumber",
                  StreetType = "StreetType",
                  UnitNumber = "",
                  City = "City",
                  Zip = "13123",
                  ContactNumber1 = "2132132133",
                  ContactNumber2 = "2132132133",
                  Email = "Test@g.in",
                  State = "Ak",
                  Country = "UnitedStates",
                  PaymentType = "submission",
                  PaymentMailAddress = "Test123@gmail.com",
                  Signature = "Codeit",
                  IsAggree = true,
                  TranscationId = "Test-Transaction",
                  PaymentStatus = "Test Approval",
                  PaymentDate = Convert.ToDateTime("2015-12-15 07:52:14.063"),
                  ApproveBy = "",
                  Description = ""
              };
              //Act
              var paymetRows = await _controller.SubmitPayment(paymentDetailsEntity);

              var content = paymetRows.GetType().GetProperty("Content").GetValue(paymetRows, null);
              // ReSharper disable once UnusedVariable
              var trasactionresult = content.GetType().GetProperty("trasactionresult").GetValue(content, null);
              var finalsuccess = content.GetType().GetProperty("finalsuccess").GetValue(content, null);
              var masterTextRevenue = content.GetType().GetProperty("masterTextRevenue").GetValue(content, null);
              var validateCorpFileStatus = content.GetType().GetProperty("validateCorpFileStatus").GetValue(content, null);

              //Assert
              Assert.IsNotNull(paymetRows);
            //  Assert.AreEqual(trasactionresult, false);
              Assert.AreEqual(finalsuccess, "YES");
              Assert.AreEqual(masterTextRevenue, false);
              Assert.AreEqual(validateCorpFileStatus, "ACTIVE");


          }


          [TestMethod]
          public async Task GetRenewMasterUserAssociateID_Return_Test()
          {
              //Initialize Entities
              var renewModel = new RenewModel { UserBblAssociateId = "1" };

              //Act 
              dynamic renewalrows = await _controller.GetRenewMasterUserAssociateId(renewModel);
              var content = renewalrows.GetType().GetProperty("Content").GetValue(renewalrows, null);
              // ReSharper disable once UnusedVariable
              var result = content.GetType().GetProperty("Result").GetValue(content, null);
              //Assert
              Assert.IsNotNull(renewalrows);
            
          }
          [TestMethod]
          public async Task DailyMailAlarmToBBlLicenseUserPriorToExpired_Return_Test()
          {
             

              //Act 
              var submissionmaster = await _controller.DailyMailAlarmToBBlLicenseUserPriorToExpired();
          

              //Assert
              Assert.IsNotNull(submissionmaster);
              Assert.AreEqual(submissionmaster, "OK");

          }
          //[TestMethod]
          //public async Task GetReceiptDetails_Return_Test()
          //{
          //    //Initialize Entites
          //    var receiptModelEntity = new ReceiptModel
          //    {
          //        PaymentID = "34cd9f7d-988c-4cd9-81c9-d740255bbfc6",
          //        MasterID = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c"
          //    };

          //    //Act
          //    var submissionMaster = await controller.GetReceiptDetails(receiptModelEntity) as JsonResult<ReceiptModel>;
          //}


    }
}
