using BusinessCenter.Data;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using BusinessCenter.Tests.Setup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Repository
{
     [TestClass]
    public class SubmissionVerficationRepositoryTest
     {
         private DbConnection _connection;
         private BusinessCenter.Tests.Common.TestContext _testDbContext;
         private UnitOfWork _testUnitOfWork;
         private SubmissionVerficationRepository _submissionVerficationTestRepository;

         private SubmissionCofoHopeHopRepository _submissionCofoHopeHopTestRepository;
         private SubmissionCorporationRepository _submissionCorporationTestRepository;
         private SubmissionTaxRevenueRepository _submissionTaxRevenueTestRepository;
         private SubmissionCorporationAgentAddressRepository _corporationAgentAddressTestRepository;
         private SubmissionCofoHopeHopAddressRepository _submissionCofoHopeHopAddressTestRepository;
         private SubmissionCategoryRepository _submissionCategoryTestRepository;
         private SubmissionDocumentRepository _submissionDocumentTestRepository;
         private SubmissionMasterApplicationChcekListRepository _subChcekListAppTestRepository;
         private FixFeeRepository _fixfeeTestRepository;

         private SubmissionMasterRepository _subMasterTestRepository;
         private StreetTypesRepository _streetTypesTestRepository;
         private MasterRegisterAgentRepository _masterRegisterAgentTestRepository;
         private SubmissionQuestionRepository _submissionQuestionTestRepository;
         private CorpRespository _corpTestRespository;
         private MasterPrimaryCategoryRepository _masterPrimaryCategoryTestRepository;
         private MasterSecondaryLicenseCategoryRepository _masterSecondaryLicenseCategoryTestRepository;
         private OSubCategoryFeesRepository _oSubCategoryFeesTestRepository;
         private MasterSubCategoryRepository _masterSubCategoryTestRepository;
         private MasterCategoryPhysicalLocationRepository _masterCategoryPhysicalLocationTestRepository;
         private FeeCodeMapRepository _feeCodeMapTestRepository;
         private SubmissionMasterRenewalRepository _submissionMasterRenewalTestRepository;
         private DCBC_ENTITY_BBL_Renewal_InvoiceRepository _dCbcEntityBblRenewalInvoiceTestRepository;
         private MasterCategoryDocumentRepository _masterCategoryDocumentTestRepository;
         private SubmissionIndividualRepository _submissionIndividualTestRepository;
         private SubmissionDocumentToAccelaRepository _submissionDocumentToAccelaTestRepository;
         private MasterCategoryQuestionRepository _masterCategoryQuestionTestRepository;
         private UserBBLServiceRepository _userBblServiceTestRepository;
         private BblRepository _bblTestRepository;
         private UserRepository _userTestRepository;
         private DCBC_ENTITY_BBL_RenewalsRepository _dcbcEntityBblRenewalsTestRepository;
         private MasterBusinessActivityRepository _masterBusinessActivityTestRepository;
         private SubmissionBblAssociationToUsersRepository _submissionBblAssociationToUsersTestRepository;
         private SubmissionToAccelaRepository _submissionToAccelaTestRepository;
         private Lookup_ExistingCategoriesRepository _lookup_ExistingCategoriesRepository;
         private MasterCountryRepository _masterCountryRepository;
       //  private MasterLicenseFEINRenewal masterLicenseFeinRenewal;
         private Lookup_ExistingCategoriesRepository _lookupExistingCategoriesRepository;
         private BblRepository _bblRepository;
         private DCBC_ENTITY_BBL_RenewalsRepository _dcbcEntityBblRenewalsRepository;
         private DCBC_ENTITY_BBL_Renewal_InvoiceRepository _dcbcEntityBblRenewalInvoiceRepository;

         private MasterStateRepository _masterStateRepository;
         private UserRoleRepository _userRoleRepository;
         private StreetTypesRepository _streetTypesRepository;

         private PaymentDetailsRepository _paymentDetailsRepository;
         private PaymentCardDetailsRepository _paymentCardDetailsRepository;
         private PaymentAddressDetailsRepository _paymentAddressDetailsTestRepository;

         private SubmissionLicenseNumberCounterRepository _submissionlicencecounter;
         private PaymentHistoryDetailsRepository _paymentHistoryDetails;
         private MasterRenewalStatusFeeRepository _masterrenewalstatusfee;
         private MasterBblApplicationStatusRepository _masterBblApplicationStatusTestRepository;
         private OSubCategoryFeesRepository _oSubCategoryFeesRepository;
       //  private DCBC_ENTITY_BBL_RenewalsRepository _dcbcEntityBblRenewalsTestRepository;
        // private SubmissionMasterRenewalRepository _submissionMasterRenewalTestRepository;
         //private DCBC_ENTITY_BBL_Renewal_InvoiceRepository _dCBC_ENTITY_BBL_Renewal_InvoiceTestRepository;
         [TestInitialize]
         public void Initialize()
         {
             //Test Context Repository
             _connection = Effort.DbConnectionFactory.CreateTransient();
             _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
             _testUnitOfWork = new UnitOfWork(_testDbContext);
             _masterStateRepository = new MasterStateRepository(_testUnitOfWork);
             _bblRepository = new BblRepository(_testUnitOfWork);
          //   masterLicenseFeinRenewal = new MasterLicenseFEINRenewal(_testUnitOfWork);
             _masterCountryRepository = new MasterCountryRepository(_testUnitOfWork);
             _streetTypesRepository = new StreetTypesRepository(_testUnitOfWork);
             _submissionlicencecounter = new SubmissionLicenseNumberCounterRepository(_testUnitOfWork);
            // _submissionMasterRenewalTestRepository = new SubmissionMasterRenewalRepository(_testUnitOfWork);
            _lookup_ExistingCategoriesRepository = new Lookup_ExistingCategoriesRepository(_testUnitOfWork);
            _bblTestRepository=new BblRepository(_testUnitOfWork);
            _lookup_ExistingCategoriesRepository=new Lookup_ExistingCategoriesRepository(_testUnitOfWork);
            _dcbcEntityBblRenewalsTestRepository = new DCBC_ENTITY_BBL_RenewalsRepository(_testUnitOfWork);
             _userRoleRepository=new UserRoleRepository(_testUnitOfWork);
            
             _userTestRepository = new UserRepository(_testUnitOfWork, _userRoleRepository);
            
            _submissionBblAssociationToUsersTestRepository=new SubmissionBblAssociationToUsersRepository(_testUnitOfWork);
            _masterCategoryQuestionTestRepository=new MasterCategoryQuestionRepository(_testUnitOfWork);
           //  _submissionMasterApplicationChcekListRepository=new SubmissionMasterApplicationChcekListRepository(_testUnitOfWork);
            _submissionToAccelaTestRepository= new SubmissionToAccelaRepository(_testUnitOfWork);
             _dcbcEntityBblRenewalsRepository = new DCBC_ENTITY_BBL_RenewalsRepository(_testUnitOfWork);
             _lookupExistingCategoriesRepository=new Lookup_ExistingCategoriesRepository(_testUnitOfWork);
             _dcbcEntityBblRenewalInvoiceRepository = new DCBC_ENTITY_BBL_Renewal_InvoiceRepository(_testUnitOfWork, _lookup_ExistingCategoriesRepository);
             //SubmissionMasterApplicationChcekListRepository Initialization
             _subChcekListAppTestRepository = new SubmissionMasterApplicationChcekListRepository(_testUnitOfWork);
           _paymentAddressDetailsTestRepository=new PaymentAddressDetailsRepository(_testUnitOfWork);
             //FixFeeRepository Initialization
             _fixfeeTestRepository = new FixFeeRepository(_testUnitOfWork);
             _dCbcEntityBblRenewalInvoiceTestRepository=new DCBC_ENTITY_BBL_Renewal_InvoiceRepository(_testUnitOfWork,_lookupExistingCategoriesRepository);
             //MasterCountryRepository Initialization
             _masterCountryRepository = new MasterCountryRepository(_testUnitOfWork);
             _submissionDocumentToAccelaTestRepository=new SubmissionDocumentToAccelaRepository(_testUnitOfWork);
             _paymentCardDetailsRepository=new PaymentCardDetailsRepository(_testUnitOfWork);
             _streetTypesTestRepository = new StreetTypesRepository(_testUnitOfWork);
             //SubmissionQuestionRepository Initialization
             _submissionQuestionTestRepository = new SubmissionQuestionRepository(_testUnitOfWork);
             _corpTestRespository = new CorpRespository(_testUnitOfWork);
             _userBblServiceTestRepository=new UserBBLServiceRepository(_testUnitOfWork);
             //MasterSecondaryLicenseCategoryRepository Initialization
             _masterSecondaryLicenseCategoryTestRepository = new MasterSecondaryLicenseCategoryRepository(_testUnitOfWork);
             _dCbcEntityBblRenewalInvoiceTestRepository=new DCBC_ENTITY_BBL_Renewal_InvoiceRepository(_testUnitOfWork,_lookupExistingCategoriesRepository);

             //MasterSecondaryLicenseCategoryRepository Initialization
             _masterSubCategoryTestRepository = new MasterSubCategoryRepository(_testUnitOfWork,
                                                                            _masterPrimaryCategoryTestRepository,
                                                                            _masterSecondaryLicenseCategoryTestRepository);
             _oSubCategoryFeesRepository = new OSubCategoryFeesRepository(_testUnitOfWork, _masterPrimaryCategoryTestRepository, _masterSecondaryLicenseCategoryTestRepository);
             //MasterCategoryDocumentRepository Initialization
             _masterCategoryDocumentTestRepository = new MasterCategoryDocumentRepository(_testUnitOfWork);
             _masterrenewalstatusfee = new MasterRenewalStatusFeeRepository(_testUnitOfWork);
             _masterRegisterAgentTestRepository=new MasterRegisterAgentRepository(_testUnitOfWork);
             //SubmissionMasterRenewalRepository Initialization
             _submissionMasterRenewalTestRepository = new SubmissionMasterRenewalRepository(_testUnitOfWork, _masterrenewalstatusfee);

             //SubmissionIndividualRepository Initialization
             _submissionIndividualTestRepository = new SubmissionIndividualRepository(_testUnitOfWork, _subMasterTestRepository);

             _submissionCofoHopeHopAddressTestRepository = new SubmissionCofoHopeHopAddressRepository(_testUnitOfWork, _corpTestRespository, _subMasterTestRepository,
               _streetTypesTestRepository, _masterRegisterAgentTestRepository, _submissionCorporationTestRepository, _submissionQuestionTestRepository,
               _subChcekListAppTestRepository, _masterStateRepository, _masterCountryRepository);
             //SubmissionCofoHopeHopRepository Initialization
             _submissionCofoHopeHopTestRepository = new SubmissionCofoHopeHopRepository(_testUnitOfWork, _submissionCofoHopeHopAddressTestRepository,
             _subChcekListAppTestRepository, _subMasterTestRepository, _streetTypesTestRepository, _fixfeeTestRepository, _submissionDocumentTestRepository);
         
             //MasterCategoryPhysicalLocationRepository Initialization
             _masterCategoryPhysicalLocationTestRepository = new MasterCategoryPhysicalLocationRepository(_testUnitOfWork, _masterPrimaryCategoryTestRepository,
             _masterCategoryQuestionTestRepository,
             _oSubCategoryFeesTestRepository,
             _masterSecondaryLicenseCategoryTestRepository);
             _feeCodeMapTestRepository=new FeeCodeMapRepository(_testUnitOfWork,_oSubCategoryFeesRepository);

             //MasterPrimaryCategoryRepository Initialization
             _masterPrimaryCategoryTestRepository = new MasterPrimaryCategoryRepository(_testUnitOfWork, _masterSecondaryLicenseCategoryTestRepository, _masterCategoryDocumentTestRepository, _masterCategoryQuestionTestRepository);
             _masterCategoryPhysicalLocationTestRepository = new MasterCategoryPhysicalLocationRepository(_testUnitOfWork, _masterPrimaryCategoryTestRepository,
                 _masterCategoryQuestionTestRepository,
                 _oSubCategoryFeesTestRepository,
                 _masterSecondaryLicenseCategoryTestRepository);
             _masterBusinessActivityTestRepository = new MasterBusinessActivityRepository(_testUnitOfWork, _masterPrimaryCategoryTestRepository);
             _oSubCategoryFeesTestRepository = new OSubCategoryFeesRepository(_testUnitOfWork, _masterPrimaryCategoryTestRepository, _masterSecondaryLicenseCategoryTestRepository);
             //SubmissionCorporationRepository Initialization
             _submissionCorporationTestRepository = new SubmissionCorporationRepository(_testUnitOfWork,
                 _corporationAgentAddressTestRepository,
                 _subChcekListAppTestRepository,
                 _masterRegisterAgentTestRepository,
                 _submissionQuestionTestRepository,
                 _corpTestRespository,
                 _subMasterTestRepository, _masterCountryRepository, _masterStateRepository
                 );
             //UserBBLServiceRepository Initialization
             _userBblServiceTestRepository = new UserBBLServiceRepository(_testUnitOfWork);

             //SubmissionTaxRevenueRepository Initialization
             _submissionTaxRevenueTestRepository = new SubmissionTaxRevenueRepository(_testUnitOfWork,
                 _subChcekListAppTestRepository);

             //SubmissionCorporationAgentAddressRepository Initialization
             _corporationAgentAddressTestRepository = new SubmissionCorporationAgentAddressRepository(_testUnitOfWork,
                 _subChcekListAppTestRepository,
                _subMasterTestRepository);

             _masterBblApplicationStatusTestRepository=new MasterBblApplicationStatusRepository(_testUnitOfWork);
             //SubmissionCofoHopeHopAddressRepository Initialization
           

             //SubmissionMasterRepository Initialization
             _subMasterTestRepository = new SubmissionMasterRepository(_testUnitOfWork, _submissionCategoryTestRepository, _bblTestRepository,
             _userBblServiceTestRepository, _submissionQuestionTestRepository, _masterCategoryPhysicalLocationTestRepository, _masterPrimaryCategoryTestRepository,
             _subChcekListAppTestRepository, _userTestRepository, _dcbcEntityBblRenewalsTestRepository, _masterBusinessActivityTestRepository,
             _submissionBblAssociationToUsersTestRepository, _submissionToAccelaTestRepository, _masterSecondaryLicenseCategoryTestRepository,
             _lookup_ExistingCategoriesRepository, _submissionlicencecounter, _masterBblApplicationStatusTestRepository);

             //SubmissionCategoryRepository Initialization
             _submissionCategoryTestRepository = new SubmissionCategoryRepository(_testUnitOfWork, _masterPrimaryCategoryTestRepository,
              _masterSecondaryLicenseCategoryTestRepository,
              _oSubCategoryFeesTestRepository, _fixfeeTestRepository,
              _submissionQuestionTestRepository, _masterSubCategoryTestRepository, _masterCategoryPhysicalLocationTestRepository,
              _feeCodeMapTestRepository, _subChcekListAppTestRepository, _submissionMasterRenewalTestRepository, _dCbcEntityBblRenewalInvoiceTestRepository,
              _lookupExistingCategoriesRepository);

             //SubmissionDocumentRepository Initialization
             _submissionDocumentTestRepository = new SubmissionDocumentRepository(_testUnitOfWork,
           _submissionCategoryTestRepository,
           _masterCategoryDocumentTestRepository,
           _masterPrimaryCategoryTestRepository,
           _masterSecondaryLicenseCategoryTestRepository, _subMasterTestRepository,
           _masterCategoryPhysicalLocationTestRepository, _subChcekListAppTestRepository,
           _submissionIndividualTestRepository, _submissionDocumentToAccelaTestRepository);
             _paymentHistoryDetails = new PaymentHistoryDetailsRepository(_testUnitOfWork);
             _paymentDetailsRepository = new PaymentDetailsRepository(_testUnitOfWork, _paymentCardDetailsRepository,
                 _paymentAddressDetailsTestRepository,
                 _subMasterTestRepository, 
                 _submissionCategoryTestRepository,
                 _submissionDocumentTestRepository,
                 _subChcekListAppTestRepository, _fixfeeTestRepository,
                 _submissionMasterRenewalTestRepository, _dCbcEntityBblRenewalInvoiceTestRepository, _bblTestRepository,
                 _userBblServiceTestRepository, _lookup_ExistingCategoriesRepository, _paymentHistoryDetails
                 );

             _submissionVerficationTestRepository = new SubmissionVerficationRepository(_testUnitOfWork,
                 _submissionCofoHopeHopTestRepository,
                _submissionCorporationTestRepository,
               _submissionTaxRevenueTestRepository,
              _corporationAgentAddressTestRepository,
              _submissionCofoHopeHopAddressTestRepository,
             _submissionCategoryTestRepository,
            _submissionDocumentTestRepository,
           _subChcekListAppTestRepository,
          _fixfeeTestRepository, _masterCountryRepository, _userBblServiceTestRepository, _bblRepository,
          _dcbcEntityBblRenewalsRepository, _dcbcEntityBblRenewalInvoiceRepository, _masterStateRepository, _streetTypesRepository, _paymentDetailsRepository,
          _submissionMasterRenewalTestRepository);


             //Setup  SubmissionMaster FackData Initialization
             var submissionMasterRepositoryData = new SubmissionMasterRepositoryData();
             _testDbContext.SubmissionMaster.AddRange(submissionMasterRepositoryData.SubmissionMasterEntitiesList);
             _testDbContext.SaveChanges();

             //Setup  SubmissionCofo_Hop_Ehop FackData Initialization
             var submissionCofoHopeHopData = new SubmissionCofoHopeHopData();
             _testDbContext.SubmissionCofo_Hop_Ehop.AddRange(submissionCofoHopeHopData.SubmissionCofoHopEhopList);
             _testDbContext.SaveChanges();


             //Setup  SubmissionCorporation_Agent FackData Initialization
             var submissionCorporationData = new SubmissionCorporationRepositoryData();
             _testDbContext.SubmissionCorporation_Agent.AddRange(submissionCorporationData.SubmissionCorporationEntitiesList);
             _testDbContext.SaveChanges();

             //Setup  SubmissionTaxRevenue FackData Initialization
             var submissionTaxRevenueData = new SubmissionTaxRevenueData();
             _testDbContext.SubmissionTaxRevenue.AddRange(submissionTaxRevenueData.SubmissionTaxRevenueEntitiesList);
             _testDbContext.SaveChanges();

             //Setup  SubmissionCorporation_Agent_Address FackData Initialization
             var submissionCorporationAgentAddressData = new SubmissionCorporationAgentAddressRepositoryData();
             _testDbContext.SubmissionCorporation_Agent_Address.AddRange(submissionCorporationAgentAddressData.SubmissionCorporationAgentAddressEntitiesList);
             _testDbContext.SaveChanges();

             //Setup  SubmissionCofo_Hop_Ehop_Address FackData Initialization
             var submissionCofoHopeHopAddressData = new SubmissionCofoHopeHopAddressData();
             _testDbContext.SubmissionCofo_Hop_Ehop_Address.AddRange(submissionCofoHopeHopAddressData.SubmissionCofoHopEhopAddressList);
             _testDbContext.SaveChanges();

             //Setup  SubmissionCategory FackData Initialization
             var submissionCategoryRepositoryData = new SubmissionCategoryRepositoryData();
             _testDbContext.SubmissionCategory.AddRange(submissionCategoryRepositoryData.SubmissionCategoryEntitiesList);
             _testDbContext.SaveChanges();

             //Setup  SubmissionDocument FackData Initialization
             var submissionDocumentRepositoryData = new SubmissionDocumentRepositoryData();
             _testDbContext.SubmissionDocument.AddRange(submissionDocumentRepositoryData.SubmissionDocumentList);
             _testDbContext.SaveChanges();

             //Setup  SubmissionMaster_ApplicationCheckList FackData Initialization
             var checklistData = new SubmissionMasterApplicationChcekListRepositoryData();
             _testDbContext.SubmissionMaster_ApplicationCheckList.AddRange(checklistData.SubmissionMaster_ApplicationCheckListEntitiesList);
             _testDbContext.SaveChanges();

             //Setup  FixFee FackData Initialization
             var fixFeeRepositoryData = new FixFeeRepositoryData();
             _testDbContext.FixFee.AddRange(fixFeeRepositoryData.FixFeeEntitiesList);
             _testDbContext.SaveChanges();

             //Setup  SubmissionQuestion FackData Initialization
             var submissionQuestionData = new SubmissionQuestionRepositoryData();
             _testDbContext.SubmissionQuestion.AddRange(submissionQuestionData.SubmissionQuestionEntitiesList);
             _testDbContext.SaveChanges();

             //Setup  MasterCategoryPhysicalLocation FackData Initialization
             var categoryPhysicalLocationData = new MasterCategoryPhysicalLocationData();
             _testDbContext.MasterCategoryPhysicalLocation.AddRange(categoryPhysicalLocationData.MasterCategoryPhysicalLocationList);
             _testDbContext.SaveChanges();

             //Setup  MasterSecondaryLicenseCategory FackData Initialization
             var secondaryCategoryData = new MasterSecondaryLicenseCategoryRepositoryData();
             _testDbContext.MasterSecondaryLicenseCategory.AddRange(secondaryCategoryData.MasterSeconderyLicenseCategoryList);
             _testDbContext.SaveChanges();

             //Setup  MasterPrimaryCategory FackData Initialization
             var primaryCategoryData = new MasterPrimaryCategoryRepositoryData();
             _testDbContext.MasterPrimaryCategory.AddRange(primaryCategoryData.MasterPrimaryCategoryEntitiesList);
             _testDbContext.SaveChanges();

             //Setup  MasterSubCategory FackData Initialization
             var subCategoryTestData = new MasterSubCategoryRepositoryData();
             _testDbContext.MasterSubCategory.AddRange(subCategoryTestData.MasterSubCategoryList);
             _testDbContext.SaveChanges();

             //Setup  SubmissionMasterRenewal FackData Initialization
             var submissionMasterRenewalData = new SubmissionMasterRenewalData();
             _testDbContext.SubmissionMasterRenewal.AddRange(submissionMasterRenewalData.MasterRenewalEntitiesList);
             _testDbContext.SaveChanges();

             //Setup  MasterCategoryDocument FackData Initialization
             var masterDocumentData = new MasterCategoryDocumentRepositoryData();
             _testDbContext.MasterCategoryDocument.AddRange(masterDocumentData.MasterCategoryDocumentList);
             _testDbContext.SaveChanges();


             //Setup  UserBBLService FackData Initialization
             var bblServiceData = new UserBBLServiceRepositoryData();
             _testDbContext.UserBBLService.AddRange(bblServiceData.UserBblServiceList);
             _testDbContext.SaveChanges();

             //Setup  SubmissionIndividual FackData Initialization
             var testData = new SubmissionIndividualData();
             _testDbContext.SubmissionIndividual.AddRange(testData.SubmissionIndividualDataEntitiesList);
             _testDbContext.SaveChanges();

             var submissioncofohopaddress = new SubmissionCofoHopeHopAddressData();
             _testDbContext.SubmissionCofo_Hop_Ehop_Address.AddRange(submissioncofohopaddress.SubmissionCofoHopEhopAddressList);
             //_testDbContext..AddRange(testData.SubmissionIndividualDataEntitiesList);
             //_testDbContext.SaveChanges();

             var streetTypesData = new StreetTypesRepositoryData();
             _testDbContext.StreetTypes.AddRange(streetTypesData.ListAll);
             _testDbContext.SaveChanges();
             var paymentdata = new PaymentDetailsData();
             _testDbContext.PaymentDetails.AddRange(paymentdata.PaymentDetailsList);
             _testDbContext.SaveChanges();

             var paymentAddressDetailsData = new PaymentAddressDetailsData();
             _testDbContext.PaymentAddressDetails.AddRange(paymentAddressDetailsData.PaymentAddressDetailsList);
             _testDbContext.SaveChanges();

             //Setup Data
             var renewalData = new DCBC_ENTITY_BBL_RenewalsData();
             _testDbContext.DCBC_ENTITY_BBL_Renewals.AddRange(renewalData.DcbcEntityBblRenewalsList);
             _testDbContext.SaveChanges();

             var renwalInvoiceData = new DCBC_ENTITY_BBL_Renewal_InvoiceData();
             _testDbContext.DCBC_ENTITY_BBL_Renewal_Invoice.AddRange(renwalInvoiceData.DcbcEntityBblRenewalsList);
             _testDbContext.SaveChanges();


             //Setup  BBLRepository FackData Initialization
             var bblRepositoryData = new BblRepositoryData();
             _testDbContext.DCBC_ENTITY_BBL.AddRange(bblRepositoryData.BblEntitiesList);
             _testDbContext.SaveChanges();
             
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
                 _submissionVerficationTestRepository.SubmissionDetails(SubVerfication);

             //Assert
             Assert.IsNotNull(submissionRows); // Test if null

             Assert.IsTrue(submissionRows.AgentAddress == "Auckland Park");
         }
         [TestMethod]
         public void SubmissionDetails_PrimsesAddress_Test()
         {
             //Initialize Entites
             var SubVerfication = new SubmissionVerfication
             {

                 MasterID = "cce0b056-d2a3-485e-a02a-e34c957c4e40"
             };

             // Act 
             var submissionRows =
                 _submissionVerficationTestRepository.SubmissionDetails(SubVerfication);

             //Assert
             Assert.IsNotNull(submissionRows); // Test if null
            Assert.IsTrue(submissionRows.DocumentList.Count() == 1);
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
                 _submissionVerficationTestRepository.SubmissionPayDetails(SubVerfication);

             //Assert
             Assert.IsNotNull(submissionRows); // Test if null
             Assert.IsTrue(submissionRows.MasterID == "8c6deecc-3b72-485d-8af5-30939af94e97");
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
                 _submissionVerficationTestRepository.BusinessReceipt(BusinessLicense);

             //Assert
             Assert.IsNotNull(submissionRows); // Test if null
             Assert.IsTrue(submissionRows.MasterId == "8c6deecc-3b72-485d-8af5-30939af94e97");
         }
         [TestMethod]
         public void RenewBusinessReceipt_Test()
         {
             //Initialize Entites
             var BusinessLicense = new RenewBasicBusinessLicense
             {

                 MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97"
             };

             // Act 
             var submissionRows =
                 _submissionVerficationTestRepository.RenewBusinessReceipt(BusinessLicense);

             //Assert
             Assert.IsNotNull(submissionRows); // Test if null
             Assert.IsTrue(submissionRows.MasterId == "8c6deecc-3b72-485d-8af5-30939af94e97");
         }
   
     }
}
