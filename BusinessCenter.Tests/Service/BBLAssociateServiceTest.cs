using BusinessCenter.Data;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using BusinessCenter.Service.Implementation;
using BusinessCenter.Tests.Setup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Service
{
    [TestClass]
    public class BBLAssociateServiceTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        
        private BblRepository bblRepository;
        private UserBBLServiceRepository userBBLServiceRepository;
        private SubmissionDocumentRepository submissionDocumentRepository;
        private SubmissionCofoHopeHopRepository submissionCofoHopeHopRepository;
        private SubmissionMasterApplicationChcekListRepository submissionMasterApplicationChcekListRepository;
        private SubmissionCorporationRepository submissionCorporationRepository;
        private SubmissionVerficationRepository submissionVerficationRepository;
        private SubmissionEHOPEligibilityRepository submissionEHOPEligibilityRepository;
        private PaymentDetailsRepository paymentDetailsRepository;
        private SubmissionCorporationAgentAddressRepository submissionCorporationAgentAddressRepository;
        private DCBC_ENTITY_BBL_RenewalsRepository dCBC_ENTITY_BBL_RenewalsRepository;
        private OSubCategoryFeesRepository oSubCategoryFeesRepository;
        private UserRepository userRepository;
        private DCBC_ENTITY_Cof_ORepository dCBC_ENTITY_Cof_ORepository;
        private MasterCategoryQuestionRepository masterCategoryQuestionRepository;
        private SubmissionBblAssociationToUsersRepository submissionBblAssociationToUsersRepository;
        private MasterBusinessActivityRepository masterBusinessActivityRepository;
        private MasterCategoryDocumentRepository masterCategoryDocumentRepository;
        private MasterPrimaryCategoryRepository masterPrimaryCategoryRepository;
        private MasterSecondaryLicenseCategoryRepository masterSecondaryLicenseCategoryRepository;
        private MasterCategoryPhysicalLocationRepository masterCategoryPhysicalLocationRepository;
        private SubmissionMasterRepository submissionMasterRepository;
        private SubmissionMasterRepository submissionMasterTestRepository;
        private SubmissionCategoryRepository submissionCategoryRepository;
        private SubmissionCategoryRepository submissionCategoryTestRepository;
        private SubmissionIndividualRepository submissionIndividualRepository;
        private SubmissionDocumentToAccelaRepository submissionDocumentToAccelaRepository;
        private SubmissionCofoHopeHopAddressRepository submissionCofoHopeHopAddressRepository;
        private StreetTypesRepository streetTypesRepository;
        private FixFeeRepository fixFeeRepository;
        private MasterRegisterAgentRepository masterRegisterAgentRepository;
        private SubmissionQuestionRepository submissionQuestionRepository;
        private CorpRespository corpRespository;
        private SubmissionTaxRevenueRepository submissionTaxRevenueRepository;
        private MastereHopEligibilityRepository mastereHopEligibilityRepository;
        private PaymentCardDetailsRepository paymentCardDetailsRepository;
        private PaymentAddressDetailsRepository paymentAddressDetailsRepository;
        private SubmissionMasterRenewalRepository submissionMasterRenewalRepository;
        private BBLAssociateService _mockBblAsscoiateService;
        private MasterSubCategoryRepository masterSubCategoryRepository;
        private FeeCodeMapRepository feeCodeMapRepository;
        private SubmissionToAccelaRepository submissionToAccelaRepository;
        private UserRoleRepository userRoleRepository;
        private Mock<IDCBC_ENTITY_BBL_Renewal_InvoiceRepository> dCBC_ENTITY_BBL_Renewal_InvoiceRepository;
        private Lookup_ExistingCategoriesRepository _Lookup_ExistingCategoriesRepository;
        private MasterCountryRepository _masterCountryRepository;
        private MasterEhopOptionTypeRepository masterEhopOptionType;
        private DCBC_ENTITY_BBL_RenewalsRepository dcbcEntityBblRenewalsRepository;
        private DCBC_ENTITY_BBL_Renewal_InvoiceRepository dcbcEntityBblRenewalInvoiceRepository;
        private SubmissionLicenseNumberCounterRepository _submissionlicencecounter;
        private MasterStateRepository _masterStateRepository;
        protected MasterCountryRepository _MasterCountryRepository;
        private EtlAddressAndParcelRepository _etlAddressTestRepository;
        private PaymentHistoryDetailsRepository _paymentHistoryDetails;
        private MasterRenewalStatusFeeRepository _masterrenewalstatusfee;
        private MasterBblApplicationStatusRepository _masterBblApplicationStatusRepository;



        [TestInitialize]
        public void Initialize()
        {
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            
            _masterStateRepository=new MasterStateRepository(_testUnitOfWork);
            _submissionlicencecounter = new SubmissionLicenseNumberCounterRepository(_testUnitOfWork);
            _MasterCountryRepository = new MasterCountryRepository(_testUnitOfWork);
            bblRepository = new BblRepository(_testUnitOfWork);
            dcbcEntityBblRenewalsRepository=new DCBC_ENTITY_BBL_RenewalsRepository(_testUnitOfWork);
            masterEhopOptionType = new MasterEhopOptionTypeRepository(_testUnitOfWork);
            userBBLServiceRepository = new UserBBLServiceRepository(_testUnitOfWork);
            _Lookup_ExistingCategoriesRepository = new Lookup_ExistingCategoriesRepository(_testUnitOfWork);
            dcbcEntityBblRenewalInvoiceRepository = new DCBC_ENTITY_BBL_Renewal_InvoiceRepository(_testUnitOfWork, _Lookup_ExistingCategoriesRepository);
            _masterCountryRepository = new MasterCountryRepository(_testUnitOfWork);
            streetTypesRepository = new StreetTypesRepository(_testUnitOfWork);
            corpRespository = new CorpRespository(_testUnitOfWork);
            dCBC_ENTITY_BBL_Renewal_InvoiceRepository = new Mock<IDCBC_ENTITY_BBL_Renewal_InvoiceRepository>();
            masterCategoryDocumentRepository = new MasterCategoryDocumentRepository(_testUnitOfWork);
            masterSubCategoryRepository = new MasterSubCategoryRepository(_testUnitOfWork, masterPrimaryCategoryRepository, masterSecondaryLicenseCategoryRepository);
            feeCodeMapRepository = new FeeCodeMapRepository(_testUnitOfWork, oSubCategoryFeesRepository);
            submissionToAccelaRepository = new SubmissionToAccelaRepository(_testUnitOfWork);
            userRoleRepository = new UserRoleRepository(_testUnitOfWork);
            _masterBblApplicationStatusRepository=new MasterBblApplicationStatusRepository(_testUnitOfWork);
            _paymentHistoryDetails = new PaymentHistoryDetailsRepository(_testUnitOfWork);
            masterSecondaryLicenseCategoryRepository = new MasterSecondaryLicenseCategoryRepository(_testUnitOfWork);
            submissionMasterApplicationChcekListRepository = new SubmissionMasterApplicationChcekListRepository(_testUnitOfWork);
            masterCategoryQuestionRepository = new MasterCategoryQuestionRepository(_testUnitOfWork);
            submissionIndividualRepository = new SubmissionIndividualRepository(_testUnitOfWork, submissionMasterRepository);
            submissionBblAssociationToUsersRepository = new SubmissionBblAssociationToUsersRepository(_testUnitOfWork);
            submissionDocumentToAccelaRepository = new SubmissionDocumentToAccelaRepository(_testUnitOfWork);
            _etlAddressTestRepository = new EtlAddressAndParcelRepository(_testUnitOfWork);
            fixFeeRepository = new FixFeeRepository(_testUnitOfWork);
            masterRegisterAgentRepository = new MasterRegisterAgentRepository(_testUnitOfWork);
            submissionQuestionRepository = new SubmissionQuestionRepository(_testUnitOfWork);
            masterPrimaryCategoryRepository = new MasterPrimaryCategoryRepository(_testUnitOfWork, masterSecondaryLicenseCategoryRepository, masterCategoryDocumentRepository,
                masterCategoryQuestionRepository);
            submissionMasterTestRepository = new SubmissionMasterRepository(_testUnitOfWork, submissionCategoryRepository, bblRepository, userBBLServiceRepository,
            submissionQuestionRepository, masterCategoryPhysicalLocationRepository, masterPrimaryCategoryRepository, submissionMasterApplicationChcekListRepository,
            userRepository, dCBC_ENTITY_BBL_RenewalsRepository, masterBusinessActivityRepository, submissionBblAssociationToUsersRepository,
            submissionToAccelaRepository, masterSecondaryLicenseCategoryRepository, _Lookup_ExistingCategoriesRepository, _submissionlicencecounter, _masterBblApplicationStatusRepository);
            submissionCategoryTestRepository = new SubmissionCategoryRepository(_testUnitOfWork,
               masterPrimaryCategoryRepository, masterSecondaryLicenseCategoryRepository,
               oSubCategoryFeesRepository, fixFeeRepository, submissionQuestionRepository, masterSubCategoryRepository,
               masterCategoryPhysicalLocationRepository,
               feeCodeMapRepository, submissionMasterApplicationChcekListRepository, submissionMasterRenewalRepository,
               dCBC_ENTITY_BBL_Renewal_InvoiceRepository.Object
               , _Lookup_ExistingCategoriesRepository);

            submissionDocumentRepository = new SubmissionDocumentRepository(_testUnitOfWork, submissionCategoryTestRepository, masterCategoryDocumentRepository,
                masterPrimaryCategoryRepository, masterSecondaryLicenseCategoryRepository, submissionMasterTestRepository,
                masterCategoryPhysicalLocationRepository, submissionMasterApplicationChcekListRepository, submissionIndividualRepository,
                submissionDocumentToAccelaRepository);

            userBBLServiceRepository = new UserBBLServiceRepository(_testUnitOfWork);

            submissionQuestionRepository = new SubmissionQuestionRepository(_testUnitOfWork);

            mastereHopEligibilityRepository = new MastereHopEligibilityRepository(_testUnitOfWork);
            userRepository = new UserRepository(_testUnitOfWork, userRoleRepository);

            submissionMasterRepository = new SubmissionMasterRepository(_testUnitOfWork, submissionCategoryRepository, bblRepository, userBBLServiceRepository,
        submissionQuestionRepository, masterCategoryPhysicalLocationRepository, masterPrimaryCategoryRepository, submissionMasterApplicationChcekListRepository,
        userRepository, dCBC_ENTITY_BBL_RenewalsRepository, masterBusinessActivityRepository, submissionBblAssociationToUsersRepository,
        submissionToAccelaRepository, masterSecondaryLicenseCategoryRepository, _Lookup_ExistingCategoriesRepository, _submissionlicencecounter, _masterBblApplicationStatusRepository);

           
            submissionCofoHopeHopAddressRepository = new SubmissionCofoHopeHopAddressRepository(_testUnitOfWork, corpRespository, submissionMasterRepository,
              streetTypesRepository,
              masterRegisterAgentRepository, submissionCorporationRepository,
              submissionQuestionRepository, submissionMasterApplicationChcekListRepository, _masterStateRepository, _MasterCountryRepository);

            submissionCofoHopeHopRepository = new SubmissionCofoHopeHopRepository(_testUnitOfWork, submissionCofoHopeHopAddressRepository,
                submissionMasterApplicationChcekListRepository,
                submissionMasterRepository, streetTypesRepository, fixFeeRepository, submissionDocumentRepository);

           

            submissionCorporationAgentAddressRepository = new SubmissionCorporationAgentAddressRepository(_testUnitOfWork, submissionMasterApplicationChcekListRepository,
              submissionMasterRepository);

            submissionCorporationRepository = new SubmissionCorporationRepository(_testUnitOfWork, submissionCorporationAgentAddressRepository,
                submissionMasterApplicationChcekListRepository,
                masterRegisterAgentRepository, submissionQuestionRepository,
                corpRespository, submissionMasterRepository, _MasterCountryRepository, _masterStateRepository);

           

            submissionEHOPEligibilityRepository = new SubmissionEHOPEligibilityRepository(_testUnitOfWork, mastereHopEligibilityRepository,
                submissionMasterApplicationChcekListRepository,userRepository,
                submissionCofoHopeHopRepository, submissionDocumentRepository, masterEhopOptionType);

          

            masterCategoryPhysicalLocationRepository = new MasterCategoryPhysicalLocationRepository(_testUnitOfWork, masterPrimaryCategoryRepository, masterCategoryQuestionRepository,
                oSubCategoryFeesRepository, masterSecondaryLicenseCategoryRepository);

        

            masterBusinessActivityRepository = new MasterBusinessActivityRepository(_testUnitOfWork, masterPrimaryCategoryRepository);

           
            dCBC_ENTITY_BBL_RenewalsRepository=new DCBC_ENTITY_BBL_RenewalsRepository(_testUnitOfWork);

         
            oSubCategoryFeesRepository = new OSubCategoryFeesRepository(_testUnitOfWork, masterPrimaryCategoryRepository, masterSecondaryLicenseCategoryRepository);

        

            dCBC_ENTITY_Cof_ORepository = new DCBC_ENTITY_Cof_ORepository(_testUnitOfWork, submissionCofoHopeHopRepository, streetTypesRepository,
                submissionMasterRepository, submissionMasterApplicationChcekListRepository,submissionCofoHopeHopAddressRepository, _etlAddressTestRepository);
       

            submissionTaxRevenueRepository = new SubmissionTaxRevenueRepository(_testUnitOfWork, submissionMasterApplicationChcekListRepository);

          
          
            paymentCardDetailsRepository = new PaymentCardDetailsRepository(_testUnitOfWork);       
                                                                                                                
            paymentAddressDetailsRepository = new PaymentAddressDetailsRepository(_testUnitOfWork);
            _masterrenewalstatusfee = new MasterRenewalStatusFeeRepository(_testUnitOfWork);
            submissionMasterRenewalRepository = new SubmissionMasterRenewalRepository(_testUnitOfWork, _masterrenewalstatusfee);
            
        submissionCategoryRepository = new SubmissionCategoryRepository(_testUnitOfWork,
                masterPrimaryCategoryRepository, masterSecondaryLicenseCategoryRepository,
                oSubCategoryFeesRepository, fixFeeRepository, submissionQuestionRepository, masterSubCategoryRepository,
                masterCategoryPhysicalLocationRepository,
                feeCodeMapRepository, submissionMasterApplicationChcekListRepository, submissionMasterRenewalRepository,
                dCBC_ENTITY_BBL_Renewal_InvoiceRepository.Object
                , _Lookup_ExistingCategoriesRepository);

            paymentDetailsRepository = new PaymentDetailsRepository(_testUnitOfWork, paymentCardDetailsRepository, paymentAddressDetailsRepository,
          submissionMasterRepository, submissionCategoryRepository, submissionDocumentRepository, submissionMasterApplicationChcekListRepository,
          fixFeeRepository, submissionMasterRenewalRepository, dCBC_ENTITY_BBL_Renewal_InvoiceRepository.Object, bblRepository, userBBLServiceRepository,
          _Lookup_ExistingCategoriesRepository, _paymentHistoryDetails);

            submissionVerficationRepository = new SubmissionVerficationRepository(_testUnitOfWork, submissionCofoHopeHopRepository, submissionCorporationRepository,
             submissionTaxRevenueRepository, submissionCorporationAgentAddressRepository, submissionCofoHopeHopAddressRepository,
             submissionCategoryRepository, submissionDocumentRepository,
             submissionMasterApplicationChcekListRepository, fixFeeRepository, _masterCountryRepository, userBBLServiceRepository,
             bblRepository, dcbcEntityBblRenewalsRepository, dcbcEntityBblRenewalInvoiceRepository, _masterStateRepository, streetTypesRepository,
             paymentDetailsRepository, submissionMasterRenewalRepository);

            submissionDocumentRepository = new SubmissionDocumentRepository(_testUnitOfWork, submissionCategoryRepository,
                masterCategoryDocumentRepository, masterPrimaryCategoryRepository,
                masterSecondaryLicenseCategoryRepository, submissionMasterRepository, masterCategoryPhysicalLocationRepository,
                submissionMasterApplicationChcekListRepository, submissionIndividualRepository, submissionDocumentToAccelaRepository);

            _mockBblAsscoiateService = new BBLAssociateService(bblRepository, userBBLServiceRepository,
           submissionDocumentRepository, submissionCorporationRepository,
          submissionCofoHopeHopRepository, submissionMasterApplicationChcekListRepository,
          submissionCorporationAgentAddressRepository, submissionEHOPEligibilityRepository, submissionVerficationRepository,
          paymentDetailsRepository, dCBC_ENTITY_BBL_RenewalsRepository,
          oSubCategoryFeesRepository, dCBC_ENTITY_Cof_ORepository, masterCategoryQuestionRepository,
          userRepository, submissionBblAssociationToUsersRepository, streetTypesRepository);

            var bblRepositoryData = new BblRepositoryData();
            _testDbContext.DCBC_ENTITY_BBL.AddRange(bblRepositoryData.BblEntitiesList);
            _testDbContext.SaveChanges();

            var corpRepositoryData = new CorpRepositoryData();
            _testDbContext.DCBC_ENTITY_CORP.AddRange(corpRepositoryData.CorpEntitiesList);
            _testDbContext.SaveChanges();

            var dCBC_ENTITY_BBL_RenewalsData = new DCBC_ENTITY_BBL_RenewalsData();
            _testDbContext.DCBC_ENTITY_BBL_Renewals.AddRange(dCBC_ENTITY_BBL_RenewalsData.DcbcEntityBblRenewalsList);
            _testDbContext.SaveChanges();

            var dCBC_ENTITY_Cof_ORepositoryData = new DCBC_ENTITY_Cof_ORepositoryData();
            _testDbContext.DCBC_ENTITY_Cof_O.AddRange(dCBC_ENTITY_Cof_ORepositoryData.DCBCCofoEntitiesList);
            _testDbContext.SaveChanges();

            var feeCodeMapRepositoryData = new FeeCodeMapRepositoryData();
            _testDbContext.FeeCodeMap.AddRange(feeCodeMapRepositoryData.FeeCodeMapEntitiesList);
            _testDbContext.SaveChanges();

            var fixFeeRepositoryData = new FixFeeRepositoryData();
            _testDbContext.FixFee.AddRange(fixFeeRepositoryData.FixFeeEntitiesList);
            _testDbContext.SaveChanges();

            var masterBusinessActivityData = new MasterBusinessActivityData();
            _testDbContext.MasterBusinessActivity.AddRange(masterBusinessActivityData.MasterAcivityEntitiesList);
            _testDbContext.SaveChanges();

            var masterCategoryDocumentRepositoryData = new MasterCategoryDocumentRepositoryData();
            _testDbContext.MasterCategoryDocument.AddRange(masterCategoryDocumentRepositoryData.MasterCategoryDocumentList);
            _testDbContext.SaveChanges();

            var masterCategoryPhysicalLocationData = new MasterCategoryPhysicalLocationData();
            _testDbContext.MasterCategoryPhysicalLocation.AddRange(masterCategoryPhysicalLocationData.MasterCategoryPhysicalLocationList);
            _testDbContext.SaveChanges();

            var masterCategoryQuestionData = new MasterCategoryQuestionData();
            _testDbContext.MasterCategoryQuestion.AddRange(masterCategoryQuestionData.CategoryQuestionEntitiesList);
            _testDbContext.SaveChanges();

            var mastereHopEligibilityRepositoryData = new MastereHopEligibilityRepositoryData();
            _testDbContext.MastereHOPEligibility.AddRange(mastereHopEligibilityRepositoryData.MastereHOPEligibilityEntitiesList);
            _testDbContext.SaveChanges();

            //var masterLicenseFEINRenewalRepositoryData = new MasterLicenseFEINRenewalRepositoryData();
            //_testDbContext.masterLicense_Renewal_TaxRevenue.AddRange(masterLicenseFEINRenewalRepositoryData.MasterLicenseEntitiesList);
            //_testDbContext.SaveChanges();

            var masterPrimaryCategoryRepositoryData = new MasterPrimaryCategoryRepositoryData();
            _testDbContext.MasterPrimaryCategory.AddRange(masterPrimaryCategoryRepositoryData.MasterPrimaryCategoryEntitiesList);
            _testDbContext.SaveChanges();

            //var masterRegisterAgentData = new MasterRegisterAgentData();
            //_testDbContext.MasterRegisteredAgent.AddRange(masterRegisterAgentData.MasterRegisteredAgentEntitiesList);
            //_testDbContext.SaveChanges();

            var masterSecondaryLicenseCategoryRepositoryData = new MasterSecondaryLicenseCategoryRepositoryData();
            _testDbContext.MasterSecondaryLicenseCategory.AddRange(masterSecondaryLicenseCategoryRepositoryData.MasterSeconderyLicenseCategoryList);
            _testDbContext.SaveChanges();

            var masterSubCategoryRepositoryData = new MasterSubCategoryRepositoryData();
            _testDbContext.MasterSubCategory.AddRange(masterSubCategoryRepositoryData.MasterSubCategoryList);
            _testDbContext.SaveChanges();

            //var masterTaxRevenueRepositoryData = new MasterTaxRevenueRepositoryData();
            //_testDbContext.MasterTaxRevenue.AddRange(masterTaxRevenueRepositoryData.MasterTaxRevenueEntitiesList);
            //_testDbContext.SaveChanges();

            var oSubCategoryFeesData = new OSubCategoryFeesData();
            _testDbContext.OSub_Category_Fees.AddRange(oSubCategoryFeesData.OSubCategoryFeesEntitiesList);
            _testDbContext.SaveChanges();

            var paymentAddressDetailsData = new PaymentAddressDetailsData();
            _testDbContext.PaymentAddressDetails.AddRange(paymentAddressDetailsData.PaymentAddressDetailsList);
            _testDbContext.SaveChanges();

            var paymentCardDetailsData = new PaymentCardDetailsData();
            _testDbContext.PaymentCardDetails.AddRange(paymentCardDetailsData.PaymentDetailsList);
            _testDbContext.SaveChanges();

            //var paymentDetailsData = new PaymentDetailsData();
            //_testDbContext.PaymentCardDetails.AddRange(paymentDetailsData.p);
            //_testDbContext.SaveChanges();

            var streetTypesRepositoryData = new StreetTypesRepositoryData();
            _testDbContext.StreetTypes.AddRange(streetTypesRepositoryData.ListAll);
            _testDbContext.SaveChanges();

            var submissionBblAssociationToUsersRepositoryData = new SubmissionBblAssociationToUsersRepositoryData();
            _testDbContext.SubmissionBblAssociationToUsers.AddRange(submissionBblAssociationToUsersRepositoryData.SubmissionBblAssociationToUsersEntitiesList);
            _testDbContext.SaveChanges();

            var submissionCategoryRepositoryData = new SubmissionCategoryRepositoryData();
            _testDbContext.SubmissionCategory.AddRange(submissionCategoryRepositoryData.SubmissionCategoryEntitiesList);
            _testDbContext.SaveChanges();

            var submissionCofoHopeHopAddressData = new SubmissionCofoHopeHopAddressData();
            _testDbContext.SubmissionCofo_Hop_Ehop_Address.AddRange(submissionCofoHopeHopAddressData.SubmissionCofoHopEhopAddressList);
            _testDbContext.SaveChanges();

            var submissionCofoHopeHopData = new SubmissionCofoHopeHopData();
            _testDbContext.SubmissionCofo_Hop_Ehop.AddRange(submissionCofoHopeHopData.SubmissionCofoHopEhopList);
            _testDbContext.SaveChanges();

            var submissionCorporationAgentAddressRepositoryData = new SubmissionCorporationAgentAddressRepositoryData();
            _testDbContext.SubmissionCorporation_Agent_Address.AddRange(submissionCorporationAgentAddressRepositoryData.SubmissionCorporationAgentAddressEntitiesList);
            _testDbContext.SaveChanges();

            var submissionCorporationRepositoryData = new SubmissionCorporationRepositoryData();
            _testDbContext.SubmissionCorporation_Agent.AddRange(submissionCorporationRepositoryData.SubmissionCorporationEntitiesList);
            _testDbContext.SaveChanges();

            var submissionDocumentRepositoryData = new SubmissionDocumentRepositoryData();
            _testDbContext.SubmissionDocument.AddRange(submissionDocumentRepositoryData.SubmissionDocumentList);
            _testDbContext.SaveChanges();

            var submissionDocumentToAccelaData = new SubmissionDocumentToAccelaData();
            _testDbContext.SubmissionDocumentToAccela.AddRange(submissionDocumentToAccelaData.SubmissionDocumentToAccelaEntitiesList);
            _testDbContext.SaveChanges();

            var submissionEHOPEligibilityRepositoryData = new SubmissionEHOPEligibilityRepositoryData();
            _testDbContext.SubmissionEHOPEligibility.AddRange(submissionEHOPEligibilityRepositoryData.SubmissionEHOPEligibilityEntitiesList);
            _testDbContext.SaveChanges();

            var submissionIndividualData = new SubmissionIndividualData();
            _testDbContext.SubmissionIndividual.AddRange(submissionIndividualData.SubmissionIndividualDataEntitiesList);
            _testDbContext.SaveChanges();

            var submissionMasterApplicationChcekListRepositoryData = new SubmissionMasterApplicationChcekListRepositoryData();
            _testDbContext.SubmissionMaster_ApplicationCheckList.AddRange(submissionMasterApplicationChcekListRepositoryData.SubmissionMaster_ApplicationCheckListEntitiesList);
            _testDbContext.SaveChanges();

            var submissionMasterRepositoryData = new SubmissionMasterRepositoryData();
            _testDbContext.SubmissionMaster.AddRange(submissionMasterRepositoryData.SubmissionMasterEntitiesList);
            _testDbContext.SaveChanges();

            var submissionQuestionRepositoryData = new SubmissionQuestionRepositoryData();
            _testDbContext.SubmissionQuestion.AddRange(submissionQuestionRepositoryData.SubmissionQuestionEntitiesList);
            _testDbContext.SaveChanges();

            var submissionTaxRevenueData = new SubmissionTaxRevenueData();
            _testDbContext.SubmissionTaxRevenue.AddRange(submissionTaxRevenueData.SubmissionTaxRevenueEntitiesList);
            _testDbContext.SaveChanges();

            var submissionToAccelaRepositoryData = new SubmissionToAccelaRepositoryData();
            _testDbContext.SubmissiontoAccela.AddRange(submissionToAccelaRepositoryData.SubmissiontoAccelaEntitiesList);
            _testDbContext.SaveChanges();

            var userBBLServiceRepositoryData = new UserBBLServiceRepositoryData();
            _testDbContext.UserBBLService.AddRange(userBBLServiceRepositoryData.UserBblServiceList);
            _testDbContext.SaveChanges();

            var userRepositoryData = new UserRepositoryData();
            _testDbContext.User.AddRange(userRepositoryData.UsersEntitiesList);
            _testDbContext.SaveChanges();

            var userRoleRepositoryData = new UserRoleRepositoryData();
            _testDbContext.UserRole.AddRange(userRoleRepositoryData.UserRolesEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  EtlAddressAndParcelRepository FackData Initialization
            var testData = new EtlAddressAndParcelRepositoryData();
            _testDbContext.TBL_ETL_Address_And_Parcel.AddRange(testData.ETLAddessEntitiesList);
            _testDbContext.SaveChanges();

            var paymentdata = new PaymentDetailsData();
            _testDbContext.PaymentDetails.AddRange(paymentdata.PaymentDetailsList);
            _testDbContext.SaveChanges();

            //Setup  MasterState FackData Initialization
            var masterStateData = new MasterStateRepositoryData();
            _testDbContext.MasterState.AddRange(masterStateData.MasterStateEntitiesList);
            _testDbContext.SaveChanges();


            //Setup FackData Initialization
            var masterCountryData = new MasterCountryData();
            _testDbContext.MasterCountry.AddRange(masterCountryData.MasterCountryEntitiesList);
            _testDbContext.SaveChanges();
        }

        [TestMethod]
        public void FindByPin_Test()
        {
            var bblAsscoiatePin = new BblAsscoiatePin
            {
                LicenseNumber = "931315000136",
                PinNumber = "184952"
            };

            //Act 
            var bblAssociateService = _mockBblAsscoiateService.FindByPin(bblAsscoiatePin);

            //Assert
            Assert.IsNotNull(bblAssociateService); // Test if null
            Assert.IsTrue(bblAssociateService.Count() == 0);
        }

        [TestMethod]
        public void CheckAssociateTest()
        {
            var bblAsscoiatePin = new BblAsscoiatePin
            {
                LicenseNumber = "100112000001",
                TaxNumber = "11-1111111"
            };

            //Act 
            var renewalRepositoryRows = _mockBblAsscoiateService.CheckAssociate(bblAsscoiatePin);

            //Assert
            Assert.IsNotNull(renewalRepositoryRows); // Test if null
            Assert.IsTrue(renewalRepositoryRows == true);
        }

        [TestMethod()]
        public void GetAssociteDataTest()
        {
            //Act 
            var bblRows = _mockBblAsscoiateService.GetAssociteData("931315000136");

            //Assert
            Assert.IsNotNull(bblRows); // Test if null
            Assert.IsTrue(bblRows.Count() == 1);
        }

        [TestMethod]
        public void InsertAssociateBblTest()
        {
            var bblAsscoiateService = new BblAsscoiateService
            {
                SubmissionLicense = "112233445566",
                UserID = "abc123-def456-ghi789",
                LicenseExpirationDate = DateTime.Now
            };
            //Act 
            var userbblService = _mockBblAsscoiateService.InsertAssociateBbl(bblAsscoiateService);

            //Assert
            Assert.IsNotNull(userbblService); // Test if null
            Assert.IsTrue(userbblService != string.Empty);
        }

        [TestMethod]
        public void DeleteUserServiceTest()
        {
            var bblAsscoiateService = new BblAsscoiateService
            {
                UserID = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F",
                SubmissionLicense = "DAPP15985360",
                DCBC_ENTITY_ID = "DAPP15985360"
            };
            //Act 
            var userbblService = _mockBblAsscoiateService.DeleteUserService(bblAsscoiateService);

            //Assert
            Assert.IsNotNull(userbblService); // Test if null
            Assert.IsTrue(userbblService == true);
        }

        [TestMethod]
        public void InsertServiceDocuments_NotExistingDocument_Test()
        {
            var document = new BblServiceDocuments
            {
                SubmissionCategoryID = 8,
                CategoryID = "151",
                DocRequired = "Instructor's License",
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Description = "Applicant must furnish a Driving Instructor(s) license.",
                FileName = "5107_DAPP15986431_FEMS_FPID_FireMarshalInsp.pdf",
                FileLocation = "BBLUpload//"
            };
            //Act 
            var submissionDocumentsRows = _mockBblAsscoiateService.InsertServiceDocuments(document);
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows.FileName == "5107_DAPP15986431_FEMS_FPID_FireMarshalInsp.pdf");
        }


        [TestMethod]
        public void InsertServiceDocuments_UpdateExistingDocument_Test()
        {
            var document = new BblServiceDocuments
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                SubmissionCategoryID = 3,
                DocRequired = "Certified Food Supervisor Certification",
                FileName = "9313_DAPP15986431_DOH_HRLA_FoodSupCert12.pdf",
                FileLocation = "BBLUpload//",
                SubmissionId = "2",
                CategoryID = "304",

                Description = "Caterers must have Certified Food Supervisor(s) present on the premises during the hours the facility is open to the public. The name(s) and certification(s) of the food supervisor employee(s) must be provided to the Department of Health's inspectors.",
            };
            //Act 
            var submissionDocumentsRows = _mockBblAsscoiateService.InsertServiceDocuments(document);
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows.FileName == "9313_DAPP15986431_DOH_HRLA_FoodSupCert12.pdf");
        }

        // DocumentList pending

        //[TestMethod]
        //public void UpdateSubmissionMaster_InPerson_Test()
        //{
        //    var bblDocuments = new BblDocuments
        //    {
        //        MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
        //        DocSubType = "IN"
        //    };
        //    //Act 
        //    var submissionDocumentsRows = _mockBblAsscoiateService.UpdateSubmissionMaster(bblDocuments);
        //    //Assert
        //    Assert.IsNotNull(submissionDocumentsRows); // Test if null
        //    Assert.IsTrue(submissionDocumentsRows);
        //}

        //[TestMethod]
        //public void UpdateSubmissionMaster_Online_Test()
        //{
        //    var bblDocuments = new BblDocuments
        //    {
        //        MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
        //        DocSubType = "ON"
        //    };
        //    //Act 
        //    var submissionDocumentsRows = _mockBblAsscoiateService.UpdateSubmissionMaster(bblDocuments);
        //    //Assert
        //    Assert.IsNotNull(submissionDocumentsRows); // Test if null
        //    Assert.IsTrue(submissionDocumentsRows);
        //}

        //[TestMethod]
        //public void InsertSubmissionLocationTest()
        //{
        //    var cofohopDetailsModel = new CofoHopDetailsModel
        //    {
        //        MasterId = "4cb3a7f0-8e98-4b8d-86ae-a2d9dbe14959",
        //        Type = "EHOP",
        //        StreetType = "Boulevard",
        //        StreetTypeId = 2

        //    };

        //    //Act 
        //    var submissioncofo = _mockBblAsscoiateService.InsertPhysicallocation(cofohopDetailsModel);

        //    //Assert
        //    Assert.IsNotNull(submissioncofo); // Test if null
        //    Assert.IsTrue(submissioncofo);
        //}
        //[TestMethod]
        //public void InsertSubmissionLocationPrimesesTest()
        //{
        //    var cofohopDetailsModel = new CofoHopDetailsModel
        //    {
        //        MasterId = "4cb3a7f0-8e98-4b8d-86ae-a2d9dbe14959",
        //        Type = "EHOP",
        //        StreetType = "Boulevard",
        //        StreetTypeId = 2

        //    };

        //    //Act 
        //    var submissioncofo = _mockBblAsscoiateService.InsertPhysicallocation(cofohopDetailsModel);

        //    //Assert
        //    Assert.IsNotNull(submissioncofo); // Test if null
        //    Assert.IsTrue(submissioncofo);
        //}

        //[TestMethod]
        //public void InsertSubmissionLocationHOPTest()
        //{
        //    var cofohopDetailsModel = new CofoHopDetailsModel
        //    {
        //        MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
        //        Type = "HOP",
        //        StreetType = "Boulevard",
        //        Number = "HO0800066",
        //        DateofIssue = "2015-09-25 00:00:00.000",
        //        StreetTypeId = 2

        //    };

        //    //Act 
        //    var submissioncofo = _mockBblAsscoiateService.InsertPhysicallocation(cofohopDetailsModel);

        //    //Assert
        //    Assert.IsNotNull(submissioncofo); // Test if null
        //    Assert.IsTrue(submissioncofo);
        //}
        //[TestMethod]
        //public void InsertSubmissionLocationCOFOTest()
        //{
        //    var cofohopDetailsModel = new CofoHopDetailsModel
        //    {
        //        MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
        //        Type = "COFO",
        //        StreetType = "Boulevard",
        //        Number = "CO1501622",
        //        DateofIssue = "2015-09-25 00:00:00.000",
        //        StreetTypeId = 2

        //    };

        //    //Act 
        //    var submissioncofo = _mockBblAsscoiateService.InsertPhysicallocation(cofohopDetailsModel);

        //    //Assert
        //    Assert.IsNotNull(submissioncofo); // Test if null
        //    Assert.IsTrue(submissioncofo);
        //}
        //[TestMethod]
        //public void InsertSubmissionLocationEHOPTest()
        //{
        //    var cofohopDetailsModel = new CofoHopDetailsModel
        //    {
        //        MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
        //        Type = "EHOP",
        //        StreetType = "Boulevard",
        //        Number = "EH1501622",
        //        DateofIssue = "2015-09-25 00:00:00.000",
        //        StreetTypeId = 2

        //    };

        //    //Act 
        //    var submissioncofo = _mockBblAsscoiateService.InsertPhysicallocation(cofohopDetailsModel);

        //    //Assert
        //    Assert.IsNotNull(submissioncofo); // Test if null
        //    Assert.IsTrue(submissioncofo);
        //}
        //[TestMethod]
        //public void InsertSubmissionLocationInsertTest()
        //{
        //    var cofohopDetailsModel = new CofoHopDetailsModel
        //    {
        //        MasterId = "4cb3a7f0-8e98-4b8d-86ae-a2d9dbe14959",
        //        Type = "EHOP",
        //        StreetType = "Boulevard",
        //        Number = "EH1501622",
        //        DateofIssue = "2015-09-25 00:00:00.000",
        //        StreetTypeId = 2

        //    };

        //    //Act 
        //    var submissioncofo = _mockBblAsscoiateService.InsertPhysicallocation(cofohopDetailsModel);

        //    //Assert
        //    Assert.IsNotNull(submissioncofo); // Test if null
        //    Assert.IsTrue(submissioncofo);
        //}
        //[TestMethod]
        //public void UpdateSubmissionLocationPrimesesTest()
        //{
        //    var cofohopDetailsModel = new CofoHopDetailsModel
        //    {
        //        MasterId = "4cb3a7f0-8e98-4b8d-86ae-a2d9dbe14959",
        //        Type = "EHOP",
        //        StreetType = "Boulevard",
        //        Number = "EH1501622",
        //        DateofIssue = "2015-09-25 00:00:00.000",
        //        StreetTypeId = 2

        //    };

        //    //Act 
        //    var submissioncofo = _mockBblAsscoiateService.InsertPhysicallocation(cofohopDetailsModel);

        //    //Assert
        //    Assert.IsNotNull(submissioncofo); // Test if null
        //    Assert.IsTrue(submissioncofo);
        //}
        //[TestMethod]
        //public void InsertSubmissionLocationNopoTest()
        //{
        //    var cofohopDetailsModel = new CofoHopDetailsModel
        //    {
        //        MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
        //        Type = "NOPO",
        //        StreetType = "Boulevard",
        //        Number = "",
        //        DateofIssue = "2015-09-25 00:00:00.000",
        //        StreetTypeId = 2

        //    };

        //    //Act 
        //    var submissioncofo = _mockBblAsscoiateService.InsertPhysicallocation(cofohopDetailsModel);

        //    //Assert
        //    Assert.IsNotNull(submissioncofo); // Test if null
        //    Assert.IsTrue(submissioncofo);
        //}
        //[TestMethod]
        //public void UpdateSubmissionLocationCofoTest()
        //{
        //    var cofohopDetailsModel = new CofoHopDetailsModel
        //    {
        //        MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
        //        Type = "HOP",
        //        StreetType = "Boulevard",
        //        Number = "",
        //        DateofIssue = "2015-09-25 00:00:00.000",
        //        StreetTypeId = 2

        //    };

        //    //Act 
        //    var submissioncofo = _mockBblAsscoiateService.InsertPhysicallocation(cofohopDetailsModel);

        //    //Assert
        //    Assert.IsNotNull(submissioncofo); // Test if null
        //    Assert.IsTrue(submissioncofo);
        //}
        [TestMethod]
        public void UpdateSubmissionLocationFalseTest()
        {
            var cofohopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc658",
                Type = "COFO",
                StreetType = "Boulevard",
                Number = "",
                DateofIssue = "2015-09-25 00:00:00.000",
                StreetTypeId = 2

            };

            //Act 
            var submissioncofo = _mockBblAsscoiateService.InsertPhysicallocation(cofohopDetailsModel);

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
            Assert.IsFalse(submissioncofo);
        }

        [TestMethod()]
        public void UpdateIsCofoTest()
        {
            var cofoHopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Type = "COFO"
            };
            //Act 
            var serviceChecklist = _mockBblAsscoiateService.UpdateIsCofoinChecklistApp(cofoHopDetailsModel);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist == true);
        }
        [TestMethod()]
        public void UpdateIsHopTest()
        {
            var cofoHopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
                Type = "HOP"
            };
            //Act 
            var serviceChecklist = _mockBblAsscoiateService.UpdateIsCofoinChecklistApp(cofoHopDetailsModel);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist == true);
        }
        [TestMethod()]
        public void UpdateIsEHOPTest()
        {
            var cofoHopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Type = "EHOP"
            };
            //Act 
            var serviceChecklist = _mockBblAsscoiateService.UpdateIsCofoinChecklistApp(cofoHopDetailsModel);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist == true);
        }
        [TestMethod()]
        public void UpdateIsTest()
        {
            var cofoHopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Type = null
            };
            //Act 
            var serviceChecklist = _mockBblAsscoiateService.UpdateIsCofoinChecklistApp(cofoHopDetailsModel);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist == false);
        }

        [TestMethod()]
        public void UpdateAllCheckListConditionsTrueTest()
        {
            var cofoHopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97"
            };
            //Act 
            var serviceChecklist = _mockBblAsscoiateService.UpdateAllCheckListConditions(cofoHopDetailsModel);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist == true);
        }
        [TestMethod()]
        public void UpdateAllCheckListConditionTest()
        {
            var cofoHopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40"
            };
            //Act 
            var serviceChecklist = _mockBblAsscoiateService.UpdateAllCheckListConditions(cofoHopDetailsModel);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsFalse(serviceChecklist);
        }
        [TestMethod()]
        public void UpdateAllCheckListConditionsFalseTest()
        {
            var cofoHopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40"
            };
            //Act 
            var serviceChecklist = _mockBblAsscoiateService.UpdateAllCheckListConditions(cofoHopDetailsModel);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist == false);
        }
        [TestMethod()]
        public void UpdateAllCheckListConditionsTest()
        {
            var cofoHopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "8dda4efd-8504-4d"
            };
            //Act 
            var serviceChecklist = _mockBblAsscoiateService.UpdateAllCheckListConditions(cofoHopDetailsModel);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist == false);
        }

        //[TestMethod]
        //public void InsertCorporationDetails_WithHqAddress_returnStatusValue_Test()
        //{

        //    var insertCorpDetails = new GeneralBusiness
        //    {
        //        // SubCorporationRegId = 0,
        //        MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
        //        FileNumber = "C880040",
        //        BusinessName = "APROSERVE CORPORATION",
        //        TradeName = "",
        //        BusinessStructure = "Corporation (Non-Profit)",
        //        UserType = "Y-CORPAGENT",

        //        FirstName = "Auckland Park",
        //        MiddleName = "Peter",
        //        LastName = "Thiel",
        //        BusinessAddressLine1 = "Auckland Park",
        //        BusinessAddressLine2 = "NEW YORK",
        //        BusinessAddressLine3 = "AVENUE",
        //        BusinessCity = "San Jose",
        //        BusinessState = "California",
        //        BusinessCountry = "United States",
        //        Telphone = "123456789",
        //        ZipCode = "20008",
        //        Email = "",
        //        CorpStatus = "True",
        //        Quardrant = "SW",
        //        Unit = "1",
        //        AddressNumber = "5225",
        //        HQStatus = "True"
        //    };


        //    //Act
        //    var corporationRows = _mockBblAsscoiateService.InsertCorporationDetails(insertCorpDetails);

        //    //Assert
        //    Assert.IsNotNull(corporationRows); // Test if null
        //    Assert.IsTrue(corporationRows);
        //}

        [TestMethod]
        public void InsertEHopEligibility_Returns_Test()
        {
            //Initialize Entites
            var eligibilityModelEntites = new EligibilityModel { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97", EhopIds = "1,2,3,4,5,6,7,8,9,10,11,12", TypeId = 1 };

            //Act 
            var ehopRows = _mockBblAsscoiateService.InsertEHopEligibility(eligibilityModelEntites);

            //Assert
            Assert.IsNotNull(ehopRows); // Test if null
            Assert.IsTrue(ehopRows == true);
        }

        [TestMethod]
        public void UpdateEHopEligibility_Returns_Test()
        {
            //Initialize Entites
            var eligibilityModelEntites = new EligibilityModel { MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40", EhopIds = "1,2,3,4,5,6,7,8,9,10,11,12", TypeId = 1 };

            //Act 
            var ehopRows = _mockBblAsscoiateService.InsertEHopEligibility(eligibilityModelEntites);

            //Assert
            Assert.IsNotNull(ehopRows); // Test if null
            Assert.IsTrue(ehopRows == true);
        }

        //[TestMethod]
        //public void GetHQAddess_Returns__Test()
        //{
        //    //Initialize Entites
        //    var generalBusiness = new GeneralBusiness { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97", UserType = "Y-CORPAGENT", FileNumber = "C943266" };

        //    //Act
        //    var corporationRows = _mockBblAsscoiateService.GetHeadQuarterAddress(generalBusiness);

        //    //Assert
        //    Assert.IsNotNull(corporationRows); // Test if null
        //    Assert.IsTrue(corporationRows.BusinessName.Trim() == "Auckland Park");
        //}

        //[TestMethod]
        //public void GetPrimisessAddress()
        //{
        //    var cofohopDetailsModel = new GeneralBusiness
        //    {
        //        MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c"
        //    };

        //    //Act 
        //    var submissioncofo = _mockBblAsscoiateService.GetPrimisessAddress(cofohopDetailsModel);

        //    //Assert
        //    Assert.IsNotNull(submissioncofo); // Test if null
        //    Assert.IsTrue(submissioncofo.AddressID == "306896");
        //}

        //[TestMethod]
        //public void GetCorpBusinessData_Returns_corpAddressDetails_Test()
        //{
        //    //Initialize Entites
        //    var generalBusiness = new GeneralBusiness { MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40", FileNumber = "C943266" };

        //    //Act
        //    var corporationRows = _mockBblAsscoiateService.GetCorpBusinessDetails(generalBusiness);

        //    //Assert
        //    Assert.IsNotNull(corporationRows); // Test if null
        //    Assert.IsTrue(corporationRows.Count() == 1);
        //}


        //[TestMethod]
        //public void GetCorpBusinessData_WithAllTheAddressDetails_Returns_corpAddressDetails_Test()
        //{
        //    //Initialize Entites
        //    var generalBusiness = new GeneralBusiness
        //    {
        //        MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
        //        FileNumber = "C943266",
        //        UserType = "Y-CORPAGENT"

        //    };

        //    //Act
        //    var corporationRows = _mockBblAsscoiateService.GetCorpBusinessDetails(generalBusiness);

        //    //Assert
        //    Assert.IsNotNull(corporationRows); // Test if null
        //    Assert.IsTrue(corporationRows.Count() == 1);
        //}

        // [TestMethod]
        //public void GetCorpBusinessData_Returns_Test()
        //{
        //    //Initialize Entites
        //    var generalBusiness = new GeneralBusiness { MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c", FileNumber = "C943266" };

        //    //Act
        //    var corporationRows = _mockBblAsscoiateService.GetCorpBusinessDetails(generalBusiness);

        //    //Assert
        //    Assert.IsNotNull(corporationRows); // Test if null
        //    Assert.IsTrue(corporationRows.Count() == 1);
        //}

        //[TestMethod]
        //public void MasterHopEligibility_Returns_True_Test()
        //{
        //    //Initialize Entites
        //    var ehopModelEntites = new EhopModel { MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40" };

        //    //Act 
        //    var ehopRows = _mockBblAsscoiateService.MasterHopEligibility(ehopModelEntites);

        //    //Assert
        //    Assert.IsNotNull(ehopRows); // Test if null
        //    Assert.IsTrue(ehopRows.CheckList.Count() == 4);
        //    Assert.IsTrue(ehopRows.IsChecked == true);
        //}
        //[TestMethod]
        //public void MasterHopEligibility_Returns_False_Test()
        //{
        //    //Initialize Entites
        //    var ehopModelEntites = new EhopModel { MasterId = "dde0b056-d2a3-485e-a02a-e34c957c4e40" };

        //    //Act 
        //    var ehopRows = _mockBblAsscoiateService.MasterHopEligibility(ehopModelEntites);

        //    //Assert
        //    Assert.IsNotNull(ehopRows); // Test if null
        //    Assert.IsTrue(ehopRows.CheckList.Count() == 4);
        //    Assert.IsTrue(ehopRows.IsChecked == false);
        //}

        // SubmissionDetails  pending
        [TestMethod]
        public void DeleteDocuments_Test()
        {
            var document = new BblServiceDocuments
            {
                SubmissionCategoryID = 1,
                CategoryID = "151",
                DocRequired = "Fire Marshal Inspection Approval",
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Description = "All businesses that manufacture, wholesale, warehouse, distribute, sell or use toxic or flammable materials or provide services to large audiences (theatres, restaurants, and public halls) are subject to inspection to ensure the safety of customers, employees, and property."

            };
            //Act 
            var submissionDocumentsRows = _mockBblAsscoiateService.DeleteDocuments(document);
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows);
        }

        // InsertPaymentDetails pending

        // FindByPaymentID Pending
        //[TestMethod]
        //public void GetCorpAgent_Returns_SubmissionCorporationAgentDetails_Test()
        //{
        //    //Initialize Entites
        //    var generalBusiness = new GeneralBusiness { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97", FileNumber = "C943266" };

        //    //Act
        //    var corporationRows = _mockBblAsscoiateService.GetCorpAgent(generalBusiness);

        //    //Assert
        //    Assert.IsNotNull(corporationRows); // Test if null
        //    Assert.IsTrue(corporationRows.Count() == 2);
        //}

        //[TestMethod]
        //public void GetCorpAgent_Returns_MasterRegisterAgentDetails_Test()
        //{
        //    //Initialize Entites
        //    var generalBusiness = new GeneralBusiness { MasterId = "SDE0CC3C-95EB-42AC-93B8-C7DD78B9399A", FileNumber = "C943266" };

        //    //Act
        //    var corporationRows = _mockBblAsscoiateService.GetCorpAgent(generalBusiness);

        //    //Assert
        //    Assert.IsNotNull(corporationRows); // Test if null
        //    Assert.IsTrue(corporationRows.Count() == 1);
        //}

        //// UpdatePaymentDetails  pending
        //// GetReceiptData pending

        //[TestMethod]
        //public void DeleteEHOPTest()
        //{
        //    var cofohopDetailsModel = new CofoHopDetailsModel
        //    {
        //        MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
        //        Type = "EHOP"
        //    };

        //    //Act 
        //    var submissioncofo = _mockBblAsscoiateService.DeleteCofo(cofohopDetailsModel);

        //    //Assert
        //    Assert.IsNotNull(submissioncofo); // Test if null
        //    Assert.IsTrue(submissioncofo);
        //}
        //[TestMethod]
        //public void DeleteCofoTest()
        //{
        //    var cofohopDetailsModel = new CofoHopDetailsModel
        //    {
        //        MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc611",
        //        Type = "COFO",
        //        DonothaveCof = false
        //    };

        //    //Act 
        //    var submissioncofo = _mockBblAsscoiateService.DeleteCofo(cofohopDetailsModel);

        //    //Assert
        //    Assert.IsNotNull(submissioncofo); // Test if null
        //    Assert.IsTrue(submissioncofo);
        //}
        //[TestMethod]
        //public void DeleteCofoTrueTest()
        //{
        //    var cofohopDetailsModel = new CofoHopDetailsModel
        //    {
        //        MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc611",
        //        Type = "COFO",
        //        DonothaveCof = true
        //    };

        //    //Act 
        //    var submissioncofo = _mockBblAsscoiateService.DeleteCofo(cofohopDetailsModel);

        //    //Assert
        //    Assert.IsNotNull(submissioncofo); // Test if null
        //    Assert.IsTrue(submissioncofo);
        //}

        //[TestMethod]
        //public void FindByDocID_Test()
        //{
        //    //Act 
        //    var submissionDocumentsRows = _mockBblAsscoiateService.FindByDocID(10);
        //    //Assert
        //    Assert.IsNotNull(submissionDocumentsRows); // Test if null
        //    Assert.IsTrue(submissionDocumentsRows.Count() == 1);
        //}

        //[TestMethod]
        //public void FindByDocName_NoRecords_Test()
        //{
        //    //Act 
        //    var submissionDocumentsRows = _mockBblAsscoiateService.FindByDocName("Theater (Live)");
        //    //Assert
        //    Assert.IsNotNull(submissionDocumentsRows); // Test if null
        //    Assert.IsTrue(!submissionDocumentsRows.Any());
        //}

        //[TestMethod]
        //public void FindByDocName_Test()
        //{
        //    //Act 
        //    var submissionDocumentsRows = _mockBblAsscoiateService.FindByDocName("Charitable Exempt");
        //    //Assert
        //    Assert.IsNotNull(submissionDocumentsRows); // Test if null
        //    Assert.IsTrue(submissionDocumentsRows.Count() == 4);
        //}

        //[TestMethod]
        //public void FindByID_NoRecords_Test()
        //{
        //    //Act 
        //    var submissionDocumentsRows = _mockBblAsscoiateService.FindByID("Solid Waste Vehicle");
        //    //Assert
        //    Assert.IsNotNull(submissionDocumentsRows); // Test if null
        //    Assert.IsTrue(!submissionDocumentsRows.Any());
        //}

        //[TestMethod]
        //public void FindByID_Test()
        //{
        //    //Act 
        //    var submissionDocumentsRows = _mockBblAsscoiateService.FindByID("Hotel");
        //    //Assert
        //    Assert.IsNotNull(submissionDocumentsRows); // Test if null
        //    Assert.IsTrue(submissionDocumentsRows.Count() == 1);
        //}

        //[TestMethod]
        //public void FindByRenewID_NoRecords_Test()
        //{
        //    //Act 
        //    var submissionDocumentsRows = _mockBblAsscoiateService.FindByRenewID("Used Car Buyer Seller");
        //    //Assert
        //    Assert.IsNotNull(submissionDocumentsRows); // Test if null
        //    Assert.IsTrue(!submissionDocumentsRows.Any());
        //}

        //[TestMethod]
        //public void FindByRenewID_Test()
        //{
        //    //Act 
        //    var submissionDocumentsRows = _mockBblAsscoiateService.FindByRenewID("Hotel");
        //    //Assert
        //    Assert.IsNotNull(submissionDocumentsRows); // Test if null
        //    Assert.IsTrue(submissionDocumentsRows.Count() == 1);
        //}

        //[TestMethod]
        //public void InsertUpdateCategoryDocuments_ToInsertNewRecord_Test()
        //{
        //    MasterCategoryDocumentModel masterCategoryDocumentModel = new MasterCategoryDocumentModel()
        //    {
        //        MasterCategoryDocId = 20,
        //        CategoryName = "Rooming House",
        //        InitialLicense = "Y",
        //        PostLicensure = "Y",
        //        Renewal = "Y",
        //        Agency = "DCRAS",
        //        Agency_FullName = "Department of Consumer and Regulatory Affairs (DCRA)",
        //        Div = "FPID",
        //        DivisionFullName = "Fire Prevention Inspection Division",
        //        SupportingDocuments = "Fire Marshal Inspection Approval",
        //        ShortDocName = "FireMarshalInsp",
        //        Description = "All businesses that manufacture, wholesale, warehouse, distribute, sell or use toxic or flammable materials or provide services to large audiences (theatres, restaurants, and public halls) are subject to inspection to ensure the safety of customers, employees, and property.",
        //        Status = true
        //    };
        //    //Act 
        //    var submissionDocumentsRows = _mockBblAsscoiateService.InsertUpdateCategoryDocuments(masterCategoryDocumentModel);
        //    //Assert
        //    Assert.IsNotNull(submissionDocumentsRows); // Test if null
        //    Assert.IsTrue(submissionDocumentsRows == 1);
        //}

        //[TestMethod]
        //public void InsertUpdateCategoryDocuments_ToUpdateExistingRecord_Test()
        //{
        //    MasterCategoryDocumentModel masterCategoryDocumentModel = new MasterCategoryDocumentModel()
        //    {
        //        MasterCategoryDocId = 15,
        //        CategoryName = "Home Improvement Contractors abc",
        //        InitialLicense = "Y",
        //        PostLicensure = "Y",
        //        Renewal = "Y",
        //        Agency = "DCRAS",
        //        Agency_FullName = "Department of Consumer and Regulatory Affairs (DCRA)",
        //        Div = "FPID",
        //        DivisionFullName = "Fire Prevention Inspection Division",
        //        SupportingDocuments = "Fire Marshal Inspection Approval",
        //        ShortDocName = "FireMarshalInsp",
        //        Description = "All businesses that manufacture, wholesale, warehouse, distribute, sell or use toxic or flammable materials or provide services to large audiences (theatres, restaurants, and public halls) are subject to inspection to ensure the safety of customers, employees, and property.",
        //        Status = true
        //    };
        //    //Act 
        //    var submissionDocumentsRows = _mockBblAsscoiateService.InsertUpdateCategoryDocuments(masterCategoryDocumentModel);
        //    //Assert
        //    Assert.IsNotNull(submissionDocumentsRows); // Test if null
        //    Assert.IsTrue(submissionDocumentsRows == 2);
        //}

        //[TestMethod]
        //public void DeleteCategoryDocument_Test()
        //{
        //    MasterCategoryDocumentModel masterCategoryDocumentModel = new MasterCategoryDocumentModel()
        //    {
        //        MasterCategoryDocId = 18,
        //        CategoryName = "Rooming House",
        //        InitialLicense = "Y",
        //        PostLicensure = "Y",
        //        Renewal = "Y",
        //        Agency = "DCRAS",
        //        Agency_FullName = "Department of Consumer and Regulatory Affairs (DCRA)",
        //        Div = "FPID",
        //        DivisionFullName = "Fire Prevention Inspection Division",
        //        SupportingDocuments = "Fire Marshal Inspection Approval",
        //        ShortDocName = "FireMarshalInsp",
        //        Description = "All businesses that manufacture, wholesale, warehouse, distribute, sell or use toxic or flammable materials or provide services to large audiences (theatres, restaurants, and public halls) are subject to inspection to ensure the safety of customers, employees, and property.",
        //        Status = true
        //    };
        //    //Act 
        //    var submissionDocumentsRows = _mockBblAsscoiateService.DeleteCategoryDocument(masterCategoryDocumentModel);
        //    //Assert
        //    Assert.IsNotNull(submissionDocumentsRows); // Test if null
        //    Assert.IsTrue(submissionDocumentsRows);
        //}

        [TestMethod]
        public void InsertUpdatePrimaryCategory_InsertOSubCategoryCategoryFee_Return_Test()
        {


            var categoryfeeentity = new OSub_Category_FeesEntity
            {

                OSub_Category = "",
                OSub_Description = "Hotel",
                Fee_Code = "T",
                Start = 1,
                End = 10,
                License_Fee = (decimal?)450.0000,
                Tier = null,
                App_Type = "B",
                Status = true

            };
            //Act 
            var categoryFeeRows = _mockBblAsscoiateService.InsertUpdateCategoryFees(categoryfeeentity);

            //Assert
            Assert.IsNotNull(categoryFeeRows); // Test if null
            Assert.IsTrue(categoryFeeRows == 1);

        }

        [TestMethod]
        public void UpdateOSubCategoryFee_Return_Test()
        {
            var categoryfeeentity = new OSub_Category_FeesEntity
            {
                OSub_Category = "2",
                OSub_Description = "Restaurant",
                Fee_Code = "T",
                Start = 1,
                End = 50,
                License_Fee = (decimal?)550.0000,
                Tier = null,
                App_Type = "B",
                Status = true
            };
            //Act 
            var categoryFeeRows = _mockBblAsscoiateService.InsertUpdateCategoryFees(categoryfeeentity);

            //Assert
            Assert.IsNotNull(categoryFeeRows); // Test if null
            Assert.IsTrue(categoryFeeRows ==100);
        }

        [TestMethod]
        public void GetCategory_WithFindFeesByPrimaryCategory_Return_Test()
        {

            //Act 
            var categoryFeeRows = _mockBblAsscoiateService.FindFeesByPrimaryCategory("6C548EBE-59ED-40B8-BEBE-EDF79149295D").ToList();

            //Assert
            Assert.IsNotNull(categoryFeeRows); // Test if null
            Assert.IsTrue(categoryFeeRows.Count() == 1);
        }

        [TestMethod]
        public void GetCategory_WithFindFeesBySecondaryCategory_Return_Test()
        {
            //Act 
            var categoryFeeRows = _mockBblAsscoiateService.FindFeesBySecondaryCategory("891F943E-D4D9-4660-872C-26366BB1C197").ToList();

            //Assert
            Assert.IsNotNull(categoryFeeRows); // Test if null
            Assert.IsTrue(categoryFeeRows.Count() == 6);
        }

        [TestMethod]
        public void GetFeeDetails_BasedOnCategoryFeeIdWithFindById_Return_Test()
        {

            //Act 
            var categoryFeeRows = _mockBblAsscoiateService.FindByCategoryFeeId("5").ToList();

            //Assert
            Assert.IsNotNull(categoryFeeRows); // Test if null
            Assert.IsTrue(categoryFeeRows.Count() == 1);
        }

        [TestMethod]
        public void GetAllCategoryFeesTest()
        {

            //Act 
            var categoryFeeRows = _mockBblAsscoiateService.AllSubCategoryFees();

            //Assert
            Assert.IsNotNull(categoryFeeRows); // Test if null
            Assert.IsTrue(categoryFeeRows.Count() ==13);
        }

        [TestMethod]
        public void CheckUserBBLTest()
        {
            //Act 
            var userbblService = _mockBblAsscoiateService.CheckUserBBL("DAPP15985360", "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F");

            //Assert
            Assert.IsNotNull(userbblService); // Test if null
            Assert.IsTrue(userbblService.Count() == 1);
        }

        [TestMethod]
        public void Get_FindBy_UserName_Test()
        {
            //Initialize Entites
            string userName = "markhurd1";

            //Act
            var userRows = _mockBblAsscoiateService.FindByUserName(userName);

            //Assert
            Assert.IsNotNull(userRows); // Test if null
            Assert.IsTrue(userRows.Count() == 1);
        }

        //[TestMethod]
        //public void DeleteHopPrimsesAddrssOnlyTest()
        //{

        //    var cofohopDetailsModel = new CofoHopDetailsModel
        //    {
        //        MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",

        //    };
        //    //Act 
        //    var submissioncofo = _mockBblAsscoiateService.DeleteHopPrimsesAddrss(cofohopDetailsModel);

        //    //Assert
        //    Assert.IsNotNull(submissioncofo); // Test if null
        //    Assert.IsTrue(submissioncofo);
        //}

        //[TestMethod]
        //public void DeleteSubmissionCorpEmpty_PassingMaterIdWith_CorpNumberREVOKED_returnStatusValue_Test()
        //{

        //    //Act
        //    var corporationRows = _mockBblAsscoiateService.DeleteSubmissionCorpEmpty("8c6deecc-3b72-485d-8af5-30939af94e97", "Y-CORPREG");

        //    //Assert
        //    Assert.IsNotNull(corporationRows); // Test if null
        //    Assert.IsTrue(corporationRows);
        //}

        //[TestMethod]
        //public void DeleteSubmissionCorp_WithAgent_PassingMaterIdWith_CorpNumberREVOKED_returnStatusValue_Test()
        //{

        //    //Act
        //    var corporationRows = _mockBblAsscoiateService.DeleteSubmissionCorpEmpty("8c6deecc-3b72-485d-8af5-30939af94e97", "Y-CORPAGENT");

        //    //Assert
        //    Assert.IsNotNull(corporationRows); // Test if null
        //    Assert.IsTrue(corporationRows);
        //}
        //HQ ADDRESS

        [TestMethod]
        public void DeleteSubmissionCorp_WithHqAddress_PassingMaterIdWith_CorpNumberREVOKED_returnStatusValue_Test()
        {

            //Act
            var corporationRows = _mockBblAsscoiateService.DeleteSubmissionCorpEmpty("8c6deecc-3b72-485d-8af5-30939af94e97", "HQ ADDRESS");

            //Assert
            Assert.IsNotNull(corporationRows); // Test if null
            Assert.IsTrue(corporationRows == false);
        }

        //[TestMethod]
        //public void Get_FindBy_UserId_Test()
        //{
        //    //Initialize Entites
        //    string userId = "2AC53E53-87D0-468F-9628-4AEBA1120613";

        //    //Act
        //    var userRows = _mockBblAsscoiateService.FindByID(userId);

        //    //Assert
        //    Assert.IsNotNull(userRows); // Test if null
        //    Assert.IsTrue(userRows.Count() == 1);
        //}

        [TestMethod]
        public void InsertUpdatePrimaryCategory_AlreadyExists_AlongWithUpdateInsertOCategoryFee_Return_Test()
        {

            //Initial Model Details
            var addPrimaryCategory = new PrimaryPhysicallocation
            {
                // Primary Category
                PrimaryID = "",
                ActivityID = "C49EACE2-2180-430E-BA2F-8B891D659421",
                Description = "APARTMENT",
                Endorsement = "Hotel",
                CategoryCode = "4040",
                UnitOne = "NA",
                UnitTwo = "NA",
                App_Type = "B",

                OldUnitOne = "",
                OldUnitTwo = "",
                OldCategoryName = ""

            };

            //Act 
            var categoryFeeRows = _mockBblAsscoiateService.InsertUpdatePrimaryCategory(addPrimaryCategory);

            //Assert
            Assert.IsNotNull(categoryFeeRows); // Test if null
            Assert.IsTrue(categoryFeeRows == "3");
        }
        [TestMethod]
        public void InsertUpdatePrimaryCategory_AlreadyExistsCategoryCode_AlongWithUpdateInsertOCategoryFee_Return_Test()
        {

            //Initial Model Details
            var addPrimaryCategory = new PrimaryPhysicallocation
            {
                // Primary Category
                PrimaryID = "",
                ActivityID = "C49EACE2-2180-430E-BA2F-8B891D659421",
                Description = "Bed and Breakfast",
                Endorsement = "Hotel",
                CategoryCode = "5107",
                UnitOne = "NA",
                UnitTwo = "NA",
                App_Type = "B",

                OldUnitOne = "",
                OldUnitTwo = "",
                OldCategoryName = ""

            };

            //Act 
            var categoryFeeRows = _mockBblAsscoiateService.InsertUpdatePrimaryCategory(addPrimaryCategory);

            //Assert
            Assert.IsNotNull(categoryFeeRows); // Test if null
            Assert.IsTrue(categoryFeeRows == "4");
        }


        [TestMethod]
        public void InsertUpdatePrimaryCategory_InsertOCategoryFeeWithFeeCodeAsC_Return_Test()
        {

            //Initial Model Details
            var addPrimaryCategory = new PrimaryPhysicallocation
            {
                // Primary Category
                PrimaryID = "",
                ActivityID = "D96ABBB9-7437-4476-BBA6-72EF6A94F327",
                Description = "Hotel New Hotel",
                Endorsement = "Hotel",
                CategoryCode = "4045",
                UnitOne = "Rooms",
                UnitTwo = "NA",
                App_Type = "B",
                Fee_Code = "C",
                LicenseType = "B",
                License_Fee = (decimal)200.00,
                Tier = 5,
                UserQuestion1 = "How many Rooms in your restaurant",

                OldUnitOne = "",
                OldUnitTwo = "",
                OldCategoryName = ""

            };

            //Act 
            var categoryFeeRows = _mockBblAsscoiateService.InsertUpdatePrimaryCategory(addPrimaryCategory);

            //Assert
            Assert.IsNotNull(categoryFeeRows); // Test if null
            Assert.IsTrue(categoryFeeRows != "0");
            //InsertUpdatePrimaryCategory
        }

        [TestMethod]
        public void InsertUpdatePrimaryCategory_InsertOCategoryFeeWithFeeCodeAsT_Return_Test()
        {

            //Initial Model Details
            var addPrimaryCategory = new PrimaryPhysicallocation
            {
                // Primary Category
                PrimaryID = "",
                ActivityID = "D96ABBB9-7437-4476-BBA6-72EF6A94F327",
                Description = "Hotel New Hotel",
                Endorsement = "Hotel",
                CategoryCode = "4045",
                UnitOne = "Rooms",
                UnitTwo = "NA",
                App_Type = "B",
                Fee_Code = "T",
                LicenseType = "B",
                License_Fee = (decimal)200.00,
                Tier = 5,
                UserQuestion1 = "How many Rooms in your restaurant",

                OldUnitOne = "",
                OldUnitTwo = "",
                OldCategoryName = ""

            };

            //Act 
            var categoryFeeRows = _mockBblAsscoiateService.InsertUpdatePrimaryCategory(addPrimaryCategory);

            //Assert
            Assert.IsNotNull(categoryFeeRows); // Test if null
            Assert.IsTrue(categoryFeeRows != "0");
            //InsertUpdatePrimaryCategory
        }

        [TestMethod]
        public void InsertUpdatePrimaryCategory_InsertOCategoryFeeWithFeeCodeAscMissing_Return_Test()
        {

            //Initial Model Details
            var addPrimaryCategory = new PrimaryPhysicallocation
            {
                // Primary Category
                PrimaryID = "",
                ActivityID = "D96ABBB9-7437-4476-BBA6-72EF6A94F327",
                Description = "Hotel New Hotel",
                Endorsement = "Hotel",
                CategoryCode = "4045",
                UnitOne = "Rooms",
                UnitTwo = "NA",
                App_Type = "B",

                LicenseType = "B",
                License_Fee = (decimal)200.00,
                Tier = 5,
                UserQuestion1 = "How many Rooms in your restaurant",

                OldUnitOne = "",
                OldUnitTwo = "",
                OldCategoryName = ""

            };

            //Act 
            var categoryFeeRows = _mockBblAsscoiateService.InsertUpdatePrimaryCategory(addPrimaryCategory);

            //Assert
            Assert.IsNotNull(categoryFeeRows); // Test if null
            Assert.IsTrue(categoryFeeRows != "0");
            //InsertUpdatePrimaryCategory
        }

        //InsertUpdateCategoryFees

        //[TestMethod]
        //public void InsertUpdatePrimaryCategory_InsertOSubCategoryCategoryFee_Return_Test()
        //{


        //    var categoryfeeentity = new OSub_Category_FeesEntity
        //    {

        //        OSub_Category = "",
        //        OSub_Description = "Hotel",
        //        Fee_Code = "T",
        //        Start = 1,
        //        End = 10,
        //        License_Fee = (decimal?)450.0000,
        //        Tier = null,
        //        App_Type = "B",
        //        Status = true

        //    };
        //    //Act 
        //    var categoryFeeRows = _mockBblAsscoiateService.InsertUpdateCategoryFees(categoryfeeentity);

        //    //Assert
        //    Assert.IsNotNull(categoryFeeRows); // Test if null
        //    Assert.IsTrue(categoryFeeRows == 1);

        //}

        [TestMethod]
        public void GetCategory_FindByDescription_Return_Test()
        {

            //Act 
            var categoryFeeRows = _mockBblAsscoiateService.FindFeesByDescription("General Business Licenses").ToList();

            //Assert
            Assert.IsNotNull(categoryFeeRows); // Test if null
            Assert.IsTrue(categoryFeeRows.Count() == 1);
        }

        //[TestMethod]
        //public void CorporationStatus_PassingMaterId_returnStatusValue_Test()
        //{

        //    //Act
        //    var corporationRows = _mockBblAsscoiateService.CorpServiceStatus("SDE0CC3C-95EB-42AC-93B8-C7DD78B9399A");

        //    //Assert
        //    Assert.IsNotNull(corporationRows); // Test if null
        //    Assert.IsTrue(corporationRows == "ACTIVE");
        //}

        //[TestMethod]
        //public void CorporationStatus_PassingMaterIdWith_CorpNumberREVOKED_returnStatusValue_Test()
        //{

        //    //Act
        //    var corporationRows = _mockBblAsscoiateService.CorpServiceStatus("SDE0CC3C-95EB-42AC-93B8-C7DD78B9399AA");

        //    //Assert
        //    Assert.IsNotNull(corporationRows); // Test if null
        //    Assert.IsTrue(corporationRows == "REVOKED");
        //}

        //[TestMethod]
        //public void DocumentInsertion_Test()
        //{
        //    // Act 
        //    var submissionDocumentsRows =
        //        _mockBblAsscoiateService.DocumentInsertion("cce0b056-d2a3-485e-a02a-e34c957c4e40", "DAPP15985360");

        //    //Assert
        //    Assert.IsNotNull(submissionDocumentsRows); // Test if null
        //    Assert.IsTrue(submissionDocumentsRows);
        //}

        [TestMethod]
        public void InsertTransferLicense_WithNewDetails_Test()
        {
            var submissionBblAssociationToUsersEntity = new Submissiontransfer
            {
                // SubmissionBblAssociationOtherUid = "",
                MasterId = "887fb5ad-114e-4d83-b5c9-ca214d89f117",
                FromUserId = "FF6E6EC1-2976-4C34-B92D-22E4EB00A897",
                ToUserId = "44AF8B96-A586-4930-A211-42EB0B72C97311111",
                //  DateOfTransfer = null,
                CreatedBy = "2AC53E53-87D0-468F-9628-4AEBA1120613",
                ReasonForTransfer = "",
            };
            //Act 
            var submissionBblAssociationToUsersRows = _mockBblAsscoiateService.InsertTransferLicense(submissionBblAssociationToUsersEntity);
            //Assert
            Assert.IsNotNull(submissionBblAssociationToUsersRows); // Test if null
            Assert.IsTrue(submissionBblAssociationToUsersRows);
        }

        [TestMethod]
        public void InsertTransferLicense_WithExistingData_Test()
        {
            var submissionBblAssociationToUsersEntity = new Submissiontransfer
            {
                //  SubmissionBblAssociationOtherUid = "",
                MasterId = "887fb5ad-114e-4d83-b5c9-ca214d89f117",
                FromUserId = "FF6E6EC1-2976-4C34-B92D-22E4EB00A897",
                ToUserId = "8EB70E26-725E-4E52-9109-CF9C37F3B980",
                LicenseNumber = "LAPP16900002",
              
                CreatedBy = "2AC53E53-87D0-468F-9628-4AEBA1120613",
                ReasonForTransfer = "",
            };
            //Act 
            var submissionBblAssociationToUsersRows = _mockBblAsscoiateService.InsertTransferLicense(submissionBblAssociationToUsersEntity);
            //Assert
            Assert.IsNotNull(submissionBblAssociationToUsersRows); // Test if null
            Assert.IsTrue(submissionBblAssociationToUsersRows == true);
        }

        [TestMethod]
        public void FindBySecondaryNameTest()
        {

            //Act 
            var secondaryNameRows = _mockBblAsscoiateService.FindBySecondaryName("Hotel");

            //Assert
            Assert.IsNotNull(secondaryNameRows); // Test if null
            Assert.IsTrue(secondaryNameRows.Count() == 2);
        }

        //[TestMethod]
        //public void FindByDocNameBasedonCategoryName_WithCateogoryNotExist_Test()
        //{
        //    //Act 
        //    var submissionDocumentsRows =
        //        _mockBblAsscoiateService.FindByDocNameBasedonCategoryName("Bed and Breakfast");

        //    //Assert
        //    Assert.IsNotNull(submissionDocumentsRows); // Test if null
        //    Assert.IsTrue(!submissionDocumentsRows.Any());
        //}

        //[TestMethod]
        //public void FindByDocNameBasedonCategoryName_WithCateogoryExist_Test()
        //{
        //    //Act 
        //    var submissionDocumentsRows =
        //        _mockBblAsscoiateService.FindByDocNameBasedonCategoryName("Hotel");

        //    //Assert
        //    Assert.IsNotNull(submissionDocumentsRows); // Test if null
        //    Assert.IsTrue(submissionDocumentsRows.Count() == 2);
        //}

        //[TestMethod]
        //public void FindByDocBasedonDocId_NoDocuments_Test()
        //{
        //    //Act 
        //    var submissionDocumentsRows = _mockBblAsscoiateService.FindByDocBasedonDocId(30);
        //    //Assert
        //    Assert.IsNotNull(submissionDocumentsRows); // Test if null
        //    Assert.IsTrue(!submissionDocumentsRows.Any());
        //}

        //[TestMethod]
        //public void FindByDocBasedonDocId_Test()
        //{
        //    //Act 
        //    var submissionDocumentsRows = _mockBblAsscoiateService.FindByDocBasedonDocId(1);
        //    //Assert
        //    Assert.IsNotNull(submissionDocumentsRows); // Test if null
        //    Assert.IsTrue(submissionDocumentsRows.Count() == 1);
        //}

        //[TestMethod]
        //public void DocumentList_MasterId_ExistInSubmissionMaster_Test()
        //{
        //    //Act 
        //    var submissionDocumentsRows = _mockBblAsscoiateService.RenewalStatuUpdation("8c6deecc-3b72-485d-8af5-30939af94e97");
        //    //Assert
        //    Assert.IsNotNull(submissionDocumentsRows); // Test if null
        //    Assert.IsTrue(submissionDocumentsRows);
        //}

        [TestMethod]
        public void DocumentList_MasterId_DoesNotExistInSubmissionMaster_Test()
        {
            //Act 
            var submissionDocumentsRows = _mockBblAsscoiateService.RenewalStatuUpdation("8c6deecc-3b72-485d-8af5-30939af94e97", "LiceneseNumber");
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows);
        }

        [TestMethod]
        public void EhopNumberWithMasterIdTest()
        {
            //Act 
            var submissioncofo = _mockBblAsscoiateService.EhopNumberWithMasterId("cce0b056-d2a3-485e-a02a-e34c957c4e40");

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
            Assert.IsTrue(submissioncofo.Count() == 1);
        }
        [TestMethod]
        public void GetBblDataOnEntityIdTest()
        {
            //Act 
            var submissioncofo = _mockBblAsscoiateService.GetBblDataOnEntityId(1);

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
            Assert.IsTrue(submissioncofo.Count() == 1);
        }
        [TestMethod]
        public void DocumentListTest()
        {
             var bblDocuments = new BblDocuments
            {
                //  SubmissionBblAssociationOtherUid = "",
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40"
              
            };
            
            //Act 
            var submissioncofo = _mockBblAsscoiateService.DocumentList(bblDocuments);

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
            Assert.IsTrue(submissioncofo.Count() == 1);
        }



        [TestMethod]
        public void FindByDocNameBasedonCategoryName_Test()
        {
            //Act 
            var submissionDocumentsRows =
                _mockBblAsscoiateService.FindByDocNameBasedonCategoryName("Hotel");

            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows.Count() == 2);
        }
        [TestMethod]
        public void FindByDocBasedonDocId_Test()
        {
            //Act 
            var submissionDocumentsRows = _mockBblAsscoiateService.FindByDocBasedonDocId(1);
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows.Count() == 1);
        }
        [TestMethod]
        public void GetPrimisessAddressTest()
        {
            //Act 
            var submissioncofo = _mockBblAsscoiateService.GetPrimisessAddress(1).ToList();

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
            Assert.IsTrue(submissioncofo.FirstOrDefault().AddressID == "306896");
        }
        [TestMethod]
        public void DisplayTaxAndRevenuWithPrimisessDetailsTest()
        {
            //Act 
            var submissioncofo = _mockBblAsscoiateService.DisplayTaxAndRevenuWithPrimisessDetails("33b635b9-fec2-4b4f-9d07-686b8d6cc60c");

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
            Assert.IsTrue(submissioncofo.BussinessOwnerFullName == "");
        }
        [TestMethod]
        public void UserbblServiceFindByIdTest()
        {

            //Act 
            var userbblService = _mockBblAsscoiateService.UserbblServiceFindById(1);

            //Assert
            Assert.IsNotNull(userbblService); // Test if null
            Assert.IsTrue(userbblService.Count() == 1);
        }
        [TestMethod]
        public void BusinessOwnerNameTest()
        {
            //Act 
            var submissioncofo = _mockBblAsscoiateService.BusinessOwnerName("8c6deecc-3b72-485d-8af5-30939af94e97");

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
            Assert.IsTrue(submissioncofo == "");
        }
        [TestMethod]
        public void EhopData_Returns_Test()
        {
            //Initialize Entites
            var ehopDataEntity = new EhopData { MasterID = "cce0b056-d2a3-485e-a02a-e34c957c4e40" };

            //Act 
            var ehopRows = _mockBblAsscoiateService.EhopData(ehopDataEntity);

            //Assert
            Assert.IsNotNull(ehopRows); // Test if null

        }
        [TestMethod]
        public void BusinessReceipt_Test()
        {
            //Initialize Entites
            var BusinessLicense = new BasicBusinessLicense
            {

                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97"
            };

            // Act 
            var submissionRows =
                _mockBblAsscoiateService.BusinessReceipt(BusinessLicense);

            //Assert
            Assert.IsNotNull(submissionRows); // Test if null
            Assert.IsTrue(submissionRows.MasterId == "8c6deecc-3b72-485d-8af5-30939af94e97");
        }
        [TestMethod()]
        public void FindByMasterIdTest()
        {
            //Act 
            var serviceChecklist = _mockBblAsscoiateService.FindByMasterId("cce0b056-d2a3-485e-a02a-e34c957c4e40");

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist.Count() == 1);
        }
        [TestMethod]
        public void GetAllStreetTypesTest()
        {
            //Act 
            var streetTypesRows = _mockBblAsscoiateService.AllStreetTypes();

            //Assert
            Assert.IsNotNull(streetTypesRows); // Test if null
            Assert.IsTrue(streetTypesRows.Count() == 2);
        }
        [TestMethod]
        public void CorpOnlineSearch_Test()
        {
            //Initialize Entites
            var corporationdetails = new CorporationDetails { FileNumber = "C880040" };

            //Act
            var corporationRows = _mockBblAsscoiateService.CorpOnlineSearch(corporationdetails);

            //Assert
            Assert.IsNotNull(corporationRows); // Test if null
            Assert.IsTrue(corporationRows.ToUpper() == "REVOKED");
        }
        [TestMethod]
        public void FindByLicenseTest()
        {

            //Act 
            var renewalRepositoryRows = _mockBblAsscoiateService.FindByLicense("400312000307");

            //Assert
            Assert.IsNotNull(renewalRepositoryRows); // Test if null
            Assert.IsTrue(renewalRepositoryRows.Count() == 1);
        }
        [TestMethod]
        public void FindAddressByPaymentId_Return_Test()
        {


            // Act 
            var paymentEntityRows =
                _mockBblAsscoiateService.FindAddressByPaymentId("8c6deecc-3b72-485d-8af5-30939af94e97");

            //Assert
            Assert.IsNotNull(paymentEntityRows); // Test if null
            Assert.IsTrue(paymentEntityRows.PaymentAddressDetails.Count() == 1);
        }
        [TestMethod]
        public void GetStateFullName_Test()
        {


            //Act
            var corporationRows = _mockBblAsscoiateService.GetStateFullName("WA", "US");

            //Assert
            Assert.IsNotNull(corporationRows); // Test if null
            Assert.IsTrue(corporationRows.ToUpper() == "WASHINGTON");
        }
        [TestMethod]
        public void GetStateCode_Test()
        {


            //Act
            var corporationRows = _mockBblAsscoiateService.StateCode("Washington", "US");

            //Assert
            Assert.IsNotNull(corporationRows); // Test if null
            Assert.IsTrue(corporationRows.ToUpper() == "WA");
        }
        [TestMethod]
        public void GetCountryFullName_Test()
        {


            //Act
            var corporationRows = _mockBblAsscoiateService.GetCountryFullName("US");

            //Assert
            Assert.IsNotNull(corporationRows); // Test if null
            Assert.IsTrue(corporationRows.ToUpper() == "UNITED STATES");
        }
        [TestMethod]
        public void FindByLicenseTax_Test()
        {


            var bblassociatepin = new BblAsscoiatePin
            {
                LicenseNumber = "931313000266",
                CleanHandsType = "FEIN",
                TaxNumber = "217643170"
            };

            //Act 
            var bblRows = _mockBblAsscoiateService.FindByLicenseTax(bblassociatepin);

            //Assert
            Assert.IsNotNull(bblRows); // Test if null
            Assert.IsTrue(bblRows);
        }

        [TestMethod]
        public void RenewalData_Test()
        {

            //Act 
            var renewalRows = _mockBblAsscoiateService.RenewalData("931315000136", "LREN11002681");

            //Assert
            Assert.IsNotNull(renewalRows); // Test if null
            Assert.IsTrue(renewalRows.Count()==1);
        }
        [TestMethod]
        public void MailAlarmAssociateUsers_Test()
        {

            //Act 
            var bblAssociateRows = _mockBblAsscoiateService.MailAlarmAssociateUsers().ToList();

            //Assert
            Assert.IsNotNull(bblAssociateRows); // Test if null
            Assert.IsTrue(bblAssociateRows.Count() == 3);
        }
        [TestMethod]
        public void GetUserDetails_Test()
        {
            //Act
            var userRows = _mockBblAsscoiateService.GetUserDetails().ToList();
            //Assert
            Assert.IsNotNull(userRows); // Test if null
            Assert.IsTrue(userRows.Count() == 5);
        }
        [TestMethod]
        public void GetTransferdataTest()
        {
            //Initial Model Details
            var bblAsscoiateService = new BblAsscoiateService
            {
                UserID = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F",
                SubmissionLicense = "DAPP15985360",
                DCBC_ENTITY_ID = "1",
            };
            //Act 
            var userbblService = _mockBblAsscoiateService.GetTransferdata(bblAsscoiateService);

            //Assert
            Assert.IsNotNull(userbblService); // Test if null
            Assert.IsTrue(userbblService.Count()==1);
        }
        [TestMethod]
        public void GetTransferHistory_Test()
        {
            //Act
            var userAssociateRows = _mockBblAsscoiateService.GetTransferHistory().ToList();
            //Assert
            Assert.IsNotNull(userAssociateRows); // Test if null
            Assert.IsTrue(userAssociateRows.Count() == 2);
        }
        [TestMethod]
        public void LastLoggedTimeUpdateTest()
        {
            //Initial Model Details
            var userdetail = new Userdetails
            {
                UserName = "markhurd1",
                IsLogedIn = true,
                SessionId = "a563064e-4372-40a1-ac50-42789e307879"
            };
            //Act 
            var userbblService = _mockBblAsscoiateService.LastLoggedTimeUpdate(userdetail);

            //Assert
            Assert.IsNotNull(userbblService); // Test if null
            Assert.IsTrue(userbblService);
        }
        [TestMethod]
        public void GetRenewalLicense_Test()
        {


            //Act 
            var renewalLicense = _mockBblAsscoiateService.GetRenewalLicense("LREN11002680");

            //Assert
            Assert.IsNotNull(renewalLicense); // Test if null
            Assert.IsTrue(renewalLicense == "931315000136");
        }
        [TestMethod]
        public void UpdateSubmissionMaster_Test()
        {
            //Initial Data is added.
            var bblDocuments = new BblDocuments
            {

                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                DocSubType = "IN"
            };
            //Act 
            var submissionDocumentsRows = _mockBblAsscoiateService.UpdateSubmissionMaster(bblDocuments);
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows);
        }
        [TestMethod]
        public void InsertCorporationDetails_Test()
        {

            var insertCorpDetails = new GeneralBusiness
            {
                // SubCorporationRegId = 0,
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
                FileNumber = "C880040",
                BusinessName = "APROSERVE CORPORATION",
                TradeName = "",
                BusinessStructure = "Corporation (Non-Profit)",
                UserType = "Y-CORPAGENT",

                FirstName = "Auckland Park",
                MiddleName = "Peter",
                LastName = "Thiel",
                BusinessAddressLine1 = "Auckland Park",
                BusinessAddressLine2 = "NEW YORK",
                BusinessAddressLine3 = "AVENUE",
                BusinessCity = "San Jose",
                BusinessState = "California",
                BusinessCountry = "United States",
                Telphone = "123456789",
                ZipCode = "20008",
                Email = "",
                CorpStatus = "True",
                Quardrant = "SW",
                Unit = "1",
                AddressNumber = "5225",
                HQStatus = "True"
            };


            //Act
            var corporationRows = _mockBblAsscoiateService.InsertCorporationDetails(insertCorpDetails);

            //Assert
            Assert.IsNotNull(corporationRows); // Test if null
            Assert.IsTrue(corporationRows);
        }
        [TestMethod]
        public void GetHQAddess_Returns__Test()
        {
            //Initialize Entites
            var generalBusiness = new GeneralBusiness { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97", UserType = "Y-CORPAGENT", FileNumber = "C943266" };

            //Act
            var corporationRows = _mockBblAsscoiateService.GetHeadQuarterAddress(generalBusiness);

            //Assert
            Assert.IsNotNull(corporationRows); // Test if null
            Assert.IsTrue(corporationRows.BusinessName.Trim() == "Auckland Park");
        }
        [TestMethod]
        public void GetCorpBusinessDetails_Test()
        {
            //Initialize Entites
            var generalBusiness = new GeneralBusiness { MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40", FileNumber = "C943266" };

            //Act
            var corporationRows = _mockBblAsscoiateService.GetCorpBusinessDetails(generalBusiness);

            //Assert
            Assert.IsNotNull(corporationRows); // Test if null
            Assert.IsTrue(corporationRows.Count() == 1);
        }
        [TestMethod]
        public void MasterHopEligibility_Returns_Test()
        {
            //Initialize Entites
            var ehopModelEntites = new EhopModel { MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40" };

            //Act 
            var ehopRows = _mockBblAsscoiateService.MasterHopEligibility(ehopModelEntites);

            //Assert
            Assert.IsNotNull(ehopRows); // Test if null
            Assert.IsTrue(ehopRows.CheckList.Count() == 4);
            Assert.IsTrue(ehopRows.IsChecked == true);
        }
        [TestMethod]
        public void SubmissionDetails_HQAddress_Test()
        {
            //Initialize Entites
            var SubVerfication = new SubmissionVerfication
            {

                MasterID = "8c6deecc-3b72-485d-8af5-30939af94e97"
            };

            // Act 
            var submissionRows =
                _mockBblAsscoiateService.SubmissionDetails(SubVerfication);

            //Assert
            Assert.IsNotNull(submissionRows); // Test if null

            Assert.IsTrue(submissionRows.AgentAddress == "Auckland Park");
        }
        [TestMethod]
        public void SubmissionPayDetails_Test()
        {
            //Initialize Entites
            var SubVerfication = new SubmissionVerfication
            {

                MasterID = "8c6deecc-3b72-485d-8af5-30939af94e97"
            };

            // Act 
            var submissionRows =
                _mockBblAsscoiateService.SubmissionPayDetails(SubVerfication);

            //Assert
            Assert.IsNotNull(submissionRows); // Test if null
            Assert.IsTrue(submissionRows.MasterID == "8c6deecc-3b72-485d-8af5-30939af94e97");
        }
        [TestMethod]
        public void InsertPaymentDetails_Returns_Test()
        {
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

            // Act 
            var paymentEntityRows =
                _mockBblAsscoiateService.InsertPaymentDetails(paymentDetailsEntity);

            //Assert
            Assert.IsNotNull(paymentEntityRows); // Test if null

        }
        [TestMethod]
        public void FindByPaymentIDTest()
        {
            //Initialize Entites
            var paymentDetails = new PaymentDetails { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97" };

            //Act 
            var paymentEntityRows = _mockBblAsscoiateService.FindByPaymentID(paymentDetails).ToList();

            //Assert
            Assert.IsNotNull(paymentEntityRows); // Test if null
            Assert.IsTrue(paymentEntityRows.Count() == 1);
        }
        [TestMethod]
        public void GetCorpAgent_Returns_SubmissionCorporationAgentDetails_Test()
        {
            //Initialize Entites
            var generalBusiness = new GeneralBusiness { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97", FileNumber = "C943266" };

            //Act
            var corporationRows = _mockBblAsscoiateService.GetCorpAgent(generalBusiness);

            //Assert
            Assert.IsNotNull(corporationRows); // Test if null
            Assert.IsTrue(corporationRows.Count() == 2);
        }
        [TestMethod]
        public void GetCorpAgent_Returns_MasterRegisterAgentDetails_Test()
        {
            //Initialize Entites

            var generalBusiness = new GeneralBusiness { MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c", FileNumber = "C880040" };

            //Act
            var corporationRows = _mockBblAsscoiateService.GetCorpAgent(generalBusiness);

            //Assert
            Assert.IsNotNull(corporationRows); // Test if null
            Assert.IsTrue(corporationRows.Count() == 1);
        }
        [TestMethod]
        public void UpdatePaymentDetails_Returns_Test()
        {
            //Initialize Entites
            var paymentDetailsEntity = new PaymentDetailsModel
            {
                PaymentId = "34cd9f7d-988c-4cd9-81c9-d740255bbfc6",
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
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

            // Act 
            var paymentEntityRows =
                _mockBblAsscoiateService.UpdatePaymentDetails(paymentDetailsEntity);

            //Assert
            Assert.IsNotNull(paymentEntityRows); // Test if null
            Assert.IsTrue(paymentEntityRows == true);
        }
        [TestMethod]
        public void GetReceiptData_Return_Test()
        {
            //Initialize Entites
            var receiptModelEntity = new ReceiptModel
            {
                PaymentID = "34cd9f7d-988c-4cd9-81c9-d740255bbfc6",
                MasterID = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c"
            };

            // Act 
            var paymentEntityRows =
                _mockBblAsscoiateService.GetReceiptData(receiptModelEntity);

            //Assert
            Assert.IsNotNull(paymentEntityRows); // Test if null

        }
        [TestMethod]
        public void DeleteCofoTest()
        {
            //Initialize data to model
            var cofohopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc611",
                Type = "COFO",
                DonothaveCof = false
            };

            //Act 
            var submissioncofo = _mockBblAsscoiateService.DeleteCofo(cofohopDetailsModel);

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
            Assert.IsTrue(submissioncofo);
        }
        [TestMethod]
        public void FindByDocIDTest()
        {
            //Act 
            var submissionDocumentsRows = _mockBblAsscoiateService.FindByDocID(10);
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows.Count() == 1);
        }
        [TestMethod]
        public void FindByDocName_Test()
        {
            //Act 
            var submissionDocumentsRows = _mockBblAsscoiateService.FindByDocName("Charitable Exempt").ToList();
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows.Count() == 4);
        }
        [TestMethod]
        public void FindByIDTest()
        {
            //Act 
            var submissionDocumentsRows = _mockBblAsscoiateService.FindByID("Hotel");
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows.Count() == 1);
        }
        [TestMethod]
        public void FindByRenewIDTest()
        {
            //Act 
            var submissionDocumentsRows = _mockBblAsscoiateService.FindByRenewID("Hotel").ToList();
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows.Count() == 1);
        }
        [TestMethod]
        public void InsertUpdateCategoryDocumentsTest()
        {
            MasterCategoryDocumentModel masterCategoryDocumentModel = new MasterCategoryDocumentModel()
            {
                MasterCategoryDocId = 20,
                CategoryName = "Rooming House",
                InitialLicense = "Y",
                PostLicensure = "Y",
                Renewal = "Y",
                Agency = "DCRAS",
                Agency_FullName = "Department of Consumer and Regulatory Affairs (DCRA)",
                Div = "FPID",
                DivisionFullName = "Fire Prevention Inspection Division",
                SupportingDocuments = "Fire Marshal Inspection Approval",
                ShortDocName = "FireMarshalInsp",
                Description = "All businesses that manufacture, wholesale, warehouse, distribute, sell or use toxic or flammable materials or provide services to large audiences (theatres, restaurants, and public halls) are subject to inspection to ensure the safety of customers, employees, and property.",
                Status = true
            };
            //Act 
            var submissionDocumentsRows = _mockBblAsscoiateService.InsertUpdateCategoryDocuments(masterCategoryDocumentModel);
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows == 1);
        }
        [TestMethod]
        public void DeleteCategoryDocumentTest()
        {
            MasterCategoryDocumentModel masterCategoryDocumentModel = new MasterCategoryDocumentModel()
            {
                MasterCategoryDocId = 18,
                CategoryName = "Rooming House",
                InitialLicense = "Y",
                PostLicensure = "Y",
                Renewal = "Y",
                Agency = "DCRAS",
                Agency_FullName = "Department of Consumer and Regulatory Affairs (DCRA)",
                Div = "FPID",
                DivisionFullName = "Fire Prevention Inspection Division",
                SupportingDocuments = "Fire Marshal Inspection Approval",
                ShortDocName = "FireMarshalInsp",
                Description = "All businesses that manufacture, wholesale, warehouse, distribute, sell or use toxic or flammable materials or provide services to large audiences (theatres, restaurants, and public halls) are subject to inspection to ensure the safety of customers, employees, and property.",
                Status = true
            };
            //Act 
            var submissionDocumentsRows = _mockBblAsscoiateService.DeleteCategoryDocument(masterCategoryDocumentModel);
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows);
        }
        [TestMethod]
        public void DeleteHopPrimsesAddrssOnlyTest()
        {
            //Initialize data to model
            var cofohopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",

            };
            //Act 
            var submissioncofo = _mockBblAsscoiateService.DeleteHopPrimsesAddrss(cofohopDetailsModel);

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
            Assert.IsTrue(submissioncofo);
        }
        [TestMethod]
        public void Get_FindBy_UserId_Test()
        {
            //Initialize Entites
            string userId = "2AC53E53-87D0-468F-9628-4AEBA1120613";

            //Act
            var userRows = _mockBblAsscoiateService.FindByAdminId(userId);

            //Assert
            Assert.IsNotNull(userRows); // Test if null
            Assert.IsTrue(userRows.Count() == 1);
        }
        [TestMethod]
        public void CorporationStatus_Test()
        {

            //Act
            var corporationRows = _mockBblAsscoiateService.CorpServiceStatus("8c6deecc-3b72-485d-8af5-30939af94e97");

            //Assert
            Assert.IsNotNull(corporationRows); // Test if null
            Assert.IsTrue(corporationRows.OriginalCorpStatus == "Active");
        }
        [TestMethod]
        public void DocumentInsertion_Test()
        {
            // Act 
            var submissionDocumentsRows =
                _mockBblAsscoiateService.DocumentInsertion("cce0b056-d2a3-485e-a02a-e34c957c4e40", "DAPP15985360");

            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows);
        }
        [TestMethod]
        public void GetRenewalLicenseNumberTest()
        {
            // Act 
            var submissionDocumentsRows =
                _mockBblAsscoiateService.GetRenewalLicenseNumber("1");

            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows == "LREN11002680");
        }
       
        //[TestMethod()]
        //public void DailyMailAlarmToBBlLicenseUsers_Test()
        //{
        //    //Act 
        //    var bblRows = _mockBblAsscoiateService.DailyMailAlarmToBBlLicenseUserPriorToExpired();

        //    //Assert
        //    Assert.IsNotNull(bblRows); // Test if null
        //    Assert.IsTrue(bblRows.Count() == 1);
        //}
         
        //[TestMethod]
        //public void GetPrimisessAddressTest()
        //{
        //    //Act 
        //    var submissioncofo = _mockBblAsscoiateService.GetPrimisessAddress(1).ToList();

        //    //Assert
        //    Assert.IsNotNull(submissioncofo); // Test if null
        //    Assert.IsTrue(submissioncofo.FirstOrDefault().AddressID == "306896");
        //}

        // DisplayTaxAndRevenuWithPrimisessDetails pending

        [TestMethod]
        public void RenewBusinessReceipt_Test()
        {
            //Initialize Entites
            var BusinessLicense = new RenewBasicBusinessLicense
            {

                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97"
            };
            var testData = new SubmissionMasterRenewalData();
            _testDbContext.SubmissionMasterRenewal.AddRange(testData.MasterRenewalEntitiesList);
            _testDbContext.SaveChanges();

            // Act 
            var submissionRows =
                _mockBblAsscoiateService.RenewBusinessReceipt(BusinessLicense);

            //Assert
            Assert.IsNotNull(submissionRows); // Test if null
            Assert.IsTrue(submissionRows.MasterId == "8c6deecc-3b72-485d-8af5-30939af94e97");
        }

        [TestMethod]
        public void UpdateCorpDisplayStatus_Test()
        {
            var generalBusiness = new GeneralBusiness { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97", FileNumber = "C943266" };
            //Act 
            _mockBblAsscoiateService.UpdateCorpDisplayStatus(generalBusiness);

            ////Assert
            //Assert.IsNotNull(stateFullName); // Test if null
            //Assert.IsTrue(stateFullName == "Test");
        }

        [TestMethod]
        public void GetPrimisessAddress_Test()
        {
            //Initialize data to model
            var cofohopDetailsModel = new GeneralBusiness
            {
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c"
            };

            //Act 
            var submissioncofo = _mockBblAsscoiateService.GetPrimisessAddress(cofohopDetailsModel);

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
            Assert.IsTrue(submissioncofo.AddressID == "306896");
        }
    }
}
