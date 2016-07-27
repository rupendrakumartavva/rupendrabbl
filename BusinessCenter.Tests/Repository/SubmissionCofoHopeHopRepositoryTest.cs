using BusinessCenter.Data;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Tests.Setup;

namespace BusinessCenter.Tests.Repository
{
    [TestClass]
    public class SubmissionCofoHopeHopRepositoryTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Repository declaration
        private SubmissionCofoHopeHopRepository _submissionCofofopehopTestRepository;
        private SubmissionCofoHopeHopAddressRepository _submissionCofoHopeHopAddressRepository;
        private SubmissionMasterApplicationChcekListRepository _submissionMasterApplicationChcekListRepository;
        private SubmissionMasterRepository _submissionMasterRepository;
        private FixFeeRepository _fixFeeRepository;
        private StreetTypesRepository _streetTypesRepository;
        private SubmissionDocumentRepository _submissionDocumentRepository;
        private CorpRespository _corpRespository;
        private MasterRegisterAgentRepository _masterRegisterAgentRepository;
        private SubmissionCorporationRepository _submissionCorporationRepository;
        private SubmissionQuestionRepository _submissionQuestionRepository;
        private SubmissionCategoryRepository _submissionCategoryRepository;
        private UserBBLServiceRepository _userBblServiceRepository;
        private BblRepository _bblRepository;
        private MasterCategoryPhysicalLocationRepository _masterCategoryPhysicalLocationRepository;
        private MasterPrimaryCategoryRepository _masterPrimaryCategoryRepository;
        private UserRepository _userRepository;
        private MasterBusinessActivityRepository _masterBusinessActivityRepository;
        private DCBC_ENTITY_BBL_RenewalsRepository _dcbcEntityBblRenewalsRepository;
        private SubmissionBblAssociationToUsersRepository _submissionBblAssociationToUsersRepository;
        private SubmissionToAccelaRepository _submissionToAccelaRepository;
        private MasterSecondaryLicenseCategoryRepository _masterSecondaryLicenseCategoryRepository;
        private MasterCategoryDocumentRepository _masterCategoryDocumentRepository;
        private SubmissionIndividualRepository _submissionIndividualRepository;
        private SubmissionDocumentToAccelaRepository _submissionDocumentToAccelaRepository;
        private Lookup_ExistingCategoriesRepository _lookupExistingCategoriesRepository;
       // private MasterLicenseFEINRenewal masterLicenseFeinRenewal;
        private MasterBblApplicationStatusRepository _masterBblApplicationStatusRepository;
        private SubmissionCorporationAgentAddressRepository _submissionCorporationAgentAddressRepository;
        private MasterCountryRepository _masterCountryRepository;
        private MasterStateRepository _masterStateRepository;
        private SubmissionLicenseNumberCounterRepository _submissionlicencecounter;
        private OSubCategoryFeesRepository _oSubCategoryFeesRepository;
        private MasterSubCategoryRepository _masterSubCategoryRepository;
        private FeeCodeMapRepository _feeCodeMapRepository;
        private SubmissionMasterRenewalRepository _submissionMasterRenewalRepository;
        private DCBC_ENTITY_BBL_Renewal_InvoiceRepository _dcbcEntityBblRenewalInvoiceRepository;
        private MasterCategoryQuestionRepository _masterCategoryQuestionRepository;
        private UserRoleRepository _userRoleRepository;
        private MasterRenewalStatusFeeRepository _masterRenewalStatusFeeRepository;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _corpRespository=new CorpRespository(_testUnitOfWork);
            _masterCountryRepository = new MasterCountryRepository(_testUnitOfWork);
            _masterStateRepository = new MasterStateRepository(_testUnitOfWork);
            _dcbcEntityBblRenewalsRepository = new DCBC_ENTITY_BBL_RenewalsRepository(_testUnitOfWork);
            _masterRegisterAgentRepository=new MasterRegisterAgentRepository(_testUnitOfWork);
            _userRoleRepository = new UserRoleRepository(_testUnitOfWork);
            _submissionlicencecounter = new SubmissionLicenseNumberCounterRepository(_testUnitOfWork);
            _submissionToAccelaRepository = new SubmissionToAccelaRepository(_testUnitOfWork);
            _masterCategoryDocumentRepository = new MasterCategoryDocumentRepository(_testUnitOfWork);
            _submissionDocumentToAccelaRepository = new SubmissionDocumentToAccelaRepository(_testUnitOfWork);
            _masterBblApplicationStatusRepository = new MasterBblApplicationStatusRepository(_testUnitOfWork);
            _feeCodeMapRepository=new FeeCodeMapRepository(_testUnitOfWork,_oSubCategoryFeesRepository);
            _masterCategoryQuestionRepository = new MasterCategoryQuestionRepository(_testUnitOfWork);
          
         //   masterLicenseFeinRenewal = new MasterLicenseFEINRenewal(_testUnitOfWork);
            //StreetTypesRepository Initialization
            _streetTypesRepository = new StreetTypesRepository(_testUnitOfWork);
            _userBblServiceRepository=new UserBBLServiceRepository(_testUnitOfWork);
            _submissionBblAssociationToUsersRepository = new SubmissionBblAssociationToUsersRepository(_testUnitOfWork);
            //SubmissionQuestionRepository Initialization
            _submissionQuestionRepository = new SubmissionQuestionRepository(_testUnitOfWork);
            _dcbcEntityBblRenewalInvoiceRepository = new DCBC_ENTITY_BBL_Renewal_InvoiceRepository(_testUnitOfWork, _lookupExistingCategoriesRepository);
            _userRepository = new UserRepository(_testUnitOfWork, _userRoleRepository);
            _masterRenewalStatusFeeRepository = new MasterRenewalStatusFeeRepository(_testUnitOfWork);
            _submissionMasterRenewalRepository = new SubmissionMasterRenewalRepository(_testUnitOfWork, _masterRenewalStatusFeeRepository);
            //Lookup_ExistingCategoriesRepository Initialization
            _lookupExistingCategoriesRepository = new Lookup_ExistingCategoriesRepository(_testUnitOfWork);

            //SubmissionMasterApplicationChcekListRepository Initialization
            _submissionMasterApplicationChcekListRepository = new SubmissionMasterApplicationChcekListRepository(_testUnitOfWork);

            //FixFeeRepository Initialization
            _fixFeeRepository = new FixFeeRepository(_testUnitOfWork);
            _bblRepository = new BblRepository(_testUnitOfWork);
            _masterSecondaryLicenseCategoryRepository = new MasterSecondaryLicenseCategoryRepository(_testUnitOfWork);
            _submissionCorporationAgentAddressRepository = new SubmissionCorporationAgentAddressRepository(_testUnitOfWork, _submissionMasterApplicationChcekListRepository, _submissionMasterRepository);
            _submissionIndividualRepository = new SubmissionIndividualRepository(_testUnitOfWork, _submissionMasterRepository);
            _masterPrimaryCategoryRepository = new MasterPrimaryCategoryRepository(_testUnitOfWork, _masterSecondaryLicenseCategoryRepository, _masterCategoryDocumentRepository, _masterCategoryQuestionRepository);
            //SubmissionCofoHopeHopAddressRepository Initialization
            _submissionCofoHopeHopAddressRepository = new SubmissionCofoHopeHopAddressRepository(_testUnitOfWork, _corpRespository, _submissionMasterRepository,
            _streetTypesRepository, _masterRegisterAgentRepository, _submissionCorporationRepository, 
            _submissionQuestionRepository,
            _submissionMasterApplicationChcekListRepository, _masterStateRepository, _masterCountryRepository);
            _masterSubCategoryRepository = new MasterSubCategoryRepository(_testUnitOfWork, _masterPrimaryCategoryRepository, _masterSecondaryLicenseCategoryRepository);
            _masterBusinessActivityRepository = new MasterBusinessActivityRepository(_testUnitOfWork, _masterPrimaryCategoryRepository);
            _oSubCategoryFeesRepository = new OSubCategoryFeesRepository(_testUnitOfWork, _masterPrimaryCategoryRepository, _masterSecondaryLicenseCategoryRepository);
            _masterCategoryPhysicalLocationRepository = new MasterCategoryPhysicalLocationRepository(_testUnitOfWork,
           _masterPrimaryCategoryRepository, _masterCategoryQuestionRepository, _oSubCategoryFeesRepository, _masterSecondaryLicenseCategoryRepository);
            //SubmissionMasterRepository Initialization
            _submissionMasterRepository = new SubmissionMasterRepository(_testUnitOfWork, _submissionCategoryRepository, _bblRepository,
            _userBblServiceRepository, _submissionQuestionRepository, _masterCategoryPhysicalLocationRepository, _masterPrimaryCategoryRepository,
            _submissionMasterApplicationChcekListRepository, _userRepository, _dcbcEntityBblRenewalsRepository,_masterBusinessActivityRepository,
            _submissionBblAssociationToUsersRepository, _submissionToAccelaRepository, _masterSecondaryLicenseCategoryRepository,
            _lookupExistingCategoriesRepository, _submissionlicencecounter, _masterBblApplicationStatusRepository);
            _submissionCategoryRepository = new SubmissionCategoryRepository(_testUnitOfWork, _masterPrimaryCategoryRepository,
     _masterSecondaryLicenseCategoryRepository, _oSubCategoryFeesRepository, _fixFeeRepository, _submissionQuestionRepository,
     _masterSubCategoryRepository, _masterCategoryPhysicalLocationRepository, _feeCodeMapRepository,
     _submissionMasterApplicationChcekListRepository, _submissionMasterRenewalRepository, _dcbcEntityBblRenewalInvoiceRepository,
      _lookupExistingCategoriesRepository);
            //SubmissionDocumentRepository Initialization
            _submissionDocumentRepository = new SubmissionDocumentRepository(_testUnitOfWork, _submissionCategoryRepository, _masterCategoryDocumentRepository,
            _masterPrimaryCategoryRepository, _masterSecondaryLicenseCategoryRepository, _submissionMasterRepository, _masterCategoryPhysicalLocationRepository,
            _submissionMasterApplicationChcekListRepository, _submissionIndividualRepository, _submissionDocumentToAccelaRepository);

            //SubmissionCofoHopeHopRepository Initialization
            _submissionCofofopehopTestRepository = new SubmissionCofoHopeHopRepository(_testUnitOfWork, _submissionCofoHopeHopAddressRepository,
            _submissionMasterApplicationChcekListRepository, _submissionMasterRepository, _streetTypesRepository,_fixFeeRepository,_submissionDocumentRepository);
            _submissionCorporationRepository = new SubmissionCorporationRepository(_testUnitOfWork,
          _submissionCorporationAgentAddressRepository,
          _submissionMasterApplicationChcekListRepository,
          _masterRegisterAgentRepository,
          _submissionQuestionRepository,
          _corpRespository,
          _submissionMasterRepository, _masterCountryRepository, _masterStateRepository
          );

            //Setup FackData SubmissionQuestion
            var masterQuestionsData = new SubmissionQuestionRepositoryData();
            _testDbContext.SubmissionQuestion.AddRange(masterQuestionsData.SubmissionQuestionEntitiesList);
            _testDbContext.SaveChanges();

            //Setup FackData MasterCategoryPhysicalLocation
            var masterCategoryPhysicalData = new MasterCategoryPhysicalLocationData();
            _testDbContext.MasterCategoryPhysicalLocation.AddRange(masterCategoryPhysicalData.MasterCategoryPhysicalLocationList);
            _testDbContext.SaveChanges();

            //Setup FackData SubmissionIndividual
            var individualData = new SubmissionIndividualData();
            _testDbContext.SubmissionIndividual.AddRange(individualData.SubmissionIndividualDataEntitiesList);
            _testDbContext.SaveChanges();


            //Setup FackData MasterCategoryDocument
            var masterCategoryDocumentData = new MasterCategoryDocumentRepositoryData();
            _testDbContext.MasterCategoryDocument.AddRange(masterCategoryDocumentData.MasterCategoryDocumentList);
            _testDbContext.SaveChanges();

            //Setup FackData SubmissionDocument
            var documentData = new SubmissionDocumentRepositoryData();
            _testDbContext.SubmissionDocument.AddRange(documentData.SubmissionDocumentList);
            _testDbContext.SaveChanges();

            //Setup FackData FixFee
            var fixFeeData = new FixFeeRepositoryData();
            _testDbContext.FixFee.AddRange(fixFeeData.FixFeeEntitiesList);
            _testDbContext.SaveChanges();

            //Setup FackData MasterSecondaryLicenseCategory
            var secondaryLicenceCategoryData = new MasterSecondaryLicenseCategoryRepositoryData();
            _testDbContext.MasterSecondaryLicenseCategory.AddRange(secondaryLicenceCategoryData.MasterSeconderyLicenseCategoryList);
            _testDbContext.SaveChanges();

            //Setup FackData SubmissiontoAccela
            var submissionToAccelaData = new SubmissionToAccelaRepositoryData();
            _testDbContext.SubmissiontoAccela.AddRange(submissionToAccelaData.SubmissiontoAccelaEntitiesList);
            _testDbContext.SaveChanges();

            //Setup FackData SubmissionBblAssociationToUsers
            var associatetoUserData = new SubmissionBblAssociationToUsersRepositoryData();
            _testDbContext.SubmissionBblAssociationToUsers.AddRange(associatetoUserData.SubmissionBblAssociationToUsersEntitiesList);
            _testDbContext.SaveChanges();

            //Setup FackData MasterBusinessActivity
            var businessActivityData = new MasterBusinessActivityData();
            _testDbContext.MasterBusinessActivity.AddRange(businessActivityData.MasterAcivityEntitiesList);
            _testDbContext.SaveChanges();


            //Setup FackData DCBC_ENTITY_BBL_Renewals
            var renewalData = new DCBC_ENTITY_BBL_RenewalsData();
            _testDbContext.DCBC_ENTITY_BBL_Renewals.AddRange(renewalData.DcbcEntityBblRenewalsList);
            _testDbContext.SaveChanges();


            //Setup FackData User
            var userData = new UserRepositoryData();
            _testDbContext.User.AddRange(userData.UsersEntitiesList);
            _testDbContext.SaveChanges();


            //Setup FackData MasterPrimaryCategory
            var masterPrimaryCategoryData = new MasterPrimaryCategoryRepositoryData();
            _testDbContext.MasterPrimaryCategory.AddRange(masterPrimaryCategoryData.MasterPrimaryCategoryEntitiesList);
            _testDbContext.SaveChanges();

            //Setup FackData MasterCategoryPhysicalLocation
            var masterCategoryPhysicalLocationData = new MasterCategoryPhysicalLocationData();
            _testDbContext.MasterCategoryPhysicalLocation.AddRange(masterCategoryPhysicalLocationData.MasterCategoryPhysicalLocationList);
            _testDbContext.SaveChanges();

            //Setup FackData UserBBLService
            var userbblServiceData = new UserBBLServiceRepositoryData();
            _testDbContext.UserBBLService.AddRange(userbblServiceData.UserBblServiceList);
            _testDbContext.SaveChanges();


            //Setup FackData DCBC_ENTITY_BBL
            var bblData = new BblRepositoryData();
            _testDbContext.DCBC_ENTITY_BBL.AddRange(bblData.BblEntitiesList);
            _testDbContext.SaveChanges();

            //Setup FackData SubmissionCategory
            var submissionCategoryData = new SubmissionCategoryRepositoryData();
            _testDbContext.SubmissionCategory.AddRange(submissionCategoryData.SubmissionCategoryEntitiesList);
            _testDbContext.SaveChanges();

            //Setup FackData SubmissionCofo_Hop_Ehop
            var testData = new SubmissionCofoHopeHopData();
            _testDbContext.SubmissionCofo_Hop_Ehop.AddRange(testData.SubmissionCofoHopEhopList);
            _testDbContext.SaveChanges();

            //Setup FackData DCBC_ENTITY_CORP
            var corpData = new CorpRepositoryData();
            _testDbContext.DCBC_ENTITY_CORP.AddRange(corpData.CorpEntitiesList);
            _testDbContext.SaveChanges();

            //Setup FackData SubmissionMaster
            var masterData = new SubmissionMasterRepositoryData();
            _testDbContext.SubmissionMaster.AddRange(masterData.SubmissionMasterEntitiesList);
            _testDbContext.SaveChanges();

            //Setup FackData StreetTypes
            var streetTypeData = new StreetTypesRepositoryData();
            _testDbContext.StreetTypes.AddRange(streetTypeData.ListAll);
            _testDbContext.SaveChanges();

            //Setup FackData MasterRegisteredAgent
            //var masterRegisterAgentData = new MasterRegisterAgentData();
            //_testDbContext.MasterRegisteredAgent.AddRange(masterRegisterAgentData.MasterRegisteredAgentEntitiesList);
            //_testDbContext.SaveChanges();

            //Setup FackData SubmissionCorporation_Agent
            var submissionCorporationData = new SubmissionCorporationRepositoryData();
            _testDbContext.SubmissionCorporation_Agent.AddRange(submissionCorporationData.SubmissionCorporationEntitiesList);
            _testDbContext.SaveChanges();

            //Setup FackData SubmissionQuestion
            var submissionQuestionData = new SubmissionQuestionRepositoryData();
            _testDbContext.SubmissionQuestion.AddRange(submissionQuestionData.SubmissionQuestionEntitiesList);
            _testDbContext.SaveChanges();


            //Setup FackData SubmissionMaster_ApplicationCheckList
            var submissionChecklistData = new SubmissionMasterApplicationChcekListRepositoryData();
            _testDbContext.SubmissionMaster_ApplicationCheckList.AddRange(
                submissionChecklistData.SubmissionMaster_ApplicationCheckListEntitiesList);
            _testDbContext.SaveChanges();

            //Setup FackData SubmissionCofo_Hop_Ehop_Address
            var submissionCofoHopeHopAddressData = new SubmissionCofoHopeHopAddressData();
            _testDbContext.SubmissionCofo_Hop_Ehop_Address.AddRange(
                submissionCofoHopeHopAddressData.SubmissionCofoHopEhopAddressList);
            _testDbContext.SaveChanges();
        }

        [TestMethod]
        public void FindByIDTest()
        {
            //Initial Model Details
            var generalBusiness = new GeneralBusiness
            {
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c"
               
            };
            //Act 
            var submissioncofo = _submissionCofofopehopTestRepository.FindByID(generalBusiness).ToList();

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
            Assert.IsTrue(submissioncofo.Count() == 1);
        }
        [TestMethod]
        public void FindByIDMasterIdTest()
        {

           
            //Act 
            var submissioncofo = _submissionCofofopehopTestRepository.FindByID("33b635b9-fec2-4b4f-9d07-686b8d6cc60c").ToList();

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
            Assert.IsTrue(submissioncofo.Count() == 1);
        }
        [TestMethod]
        public void InsertSubmissionLocationTest()
        {
            //Initial Model Details
            var cofohopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
                Type = "EHOP",
                StreetType = "Boulevard",
                StreetTypeId =2

            };

            //Act 
            var submissioncofo = _submissionCofofopehopTestRepository.InsertSubmissionLocation(cofohopDetailsModel);

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
            Assert.IsTrue(submissioncofo);
        }
        [TestMethod]
        public void InsertSubmissionLocationPrimesesTest()
        {
            //Initial Model Details
            var cofohopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
                Type = "EHOP",
                StreetType = "Boulevard",
                StreetTypeId = 2

            };

            //Act 
            var submissioncofo = _submissionCofofopehopTestRepository.InsertSubmissionLocation(cofohopDetailsModel);

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
            Assert.IsTrue(submissioncofo);
        }
       
        [TestMethod]
        public void InsertSubmissionLocationHOPTest()
        {
            //Initialize data to model
            var cofohopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
                Type = "HOP",
                StreetType = "Boulevard",
                Number = "HO0800066",
                DateofIssue = "2015-09-25 00:00:00.000",
                StreetTypeId = 2

            };

            //Act 
            var submissioncofo = _submissionCofofopehopTestRepository.InsertSubmissionLocation(cofohopDetailsModel);

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
            Assert.IsTrue(submissioncofo);
        }
        [TestMethod]
        public void InsertSubmissionLocationCOFOTest()
        {
            //Initialize data to model
            var cofohopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
                Type = "COFO",
                StreetType = "Boulevard",
                Number = "CO1501622",
                DateofIssue = "2015-09-25 00:00:00.000",
                StreetTypeId = 2

            };

            //Act 
            var submissioncofo = _submissionCofofopehopTestRepository.InsertSubmissionLocation(cofohopDetailsModel);

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
            Assert.IsTrue(submissioncofo);
        }

         [TestMethod]
        public void InsertSubmissionLocation_Test()
        {
            //Initialize data to model
            var cofohopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "4cb3a7f0-8e98-4b8d-86ae-a2d9dbe14959",
                Type = "HOP",
                StreetType = "Boulevard",
                Number = "HO0800066",
                DateofIssue = "2015-09-25 00:00:00.000",
                StreetTypeId = 2

            };

            //Act 
            var submissioncofo = _submissionCofofopehopTestRepository.InsertSubmissionLocation(cofohopDetailsModel);

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
            Assert.IsTrue(submissioncofo);
        }
         [TestMethod]
         public void InsertSubmissionLocationEhop_Test()
         {
             //Initialize data to model
             var cofohopDetailsModel = new CofoHopDetailsModel
             {
                 MasterId = "4cb3a7f0-8e98-4b8d-86ae-a2d9dbe14959",
                 Type = "eHOP",
                 StreetType = "Boulevard",
                 Number = "HO0800066",
                 DateofIssue = "2015-09-25 00:00:00.000",
                 StreetTypeId = 2

             };

             //Act 
             var submissioncofo = _submissionCofofopehopTestRepository.InsertSubmissionLocation(cofohopDetailsModel);

             //Assert
             Assert.IsNotNull(submissioncofo); // Test if null
             Assert.IsTrue(submissioncofo);
         }
        [TestMethod]
        public void InsertSubmissionLocationEHOPTest()
        {
            //Initialize data to model
            var cofohopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
                Type = "EHOP",
                StreetType = "Boulevard",
                Number = "EH1501622",
                DateofIssue = "2015-09-25 00:00:00.000",
                StreetTypeId = 2

            };

            //Act 
            var submissioncofo = _submissionCofofopehopTestRepository.InsertSubmissionLocation(cofohopDetailsModel);

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
            Assert.IsTrue(submissioncofo);
        }
        [TestMethod]
        public void InsertSubmissionLocationInsertTest()
        {
            //Initialize data to model
            var cofohopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
                Type = "EHOP",
                StreetType = "Boulevard",
                Number = "EH1501622",
                DateofIssue = "2015-09-25 00:00:00.000",
                StreetTypeId = 2

            };

            //Act 
            var submissioncofo = _submissionCofofopehopTestRepository.InsertSubmissionLocation(cofohopDetailsModel);

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
            Assert.IsTrue(submissioncofo);
        }
        [TestMethod]
        public void UpdateSubmissionLocationPrimesesTest()
        {
            //Initialize data to model
           var cofohopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
                Type = "EHOP",
                StreetType = "Boulevard",
                Number = "EH1501622",
                DateofIssue = "2015-09-25 00:00:00.000",
                StreetTypeId = 2

            };

            //Act 
            var submissioncofo = _submissionCofofopehopTestRepository.InsertSubmissionLocation(cofohopDetailsModel);

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
            Assert.IsTrue(submissioncofo);
        }
        [TestMethod]
        public void InsertSubmissionLocationNopoTest()
        {
            //Initialize data to model
            var cofohopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
                Type = "NOPO",
                StreetType = "Boulevard",
                Number = "",
                DateofIssue = "2015-09-25 00:00:00.000",
                StreetTypeId = 2

            };

            //Act 
            var submissioncofo = _submissionCofofopehopTestRepository.InsertSubmissionLocation(cofohopDetailsModel);

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
            Assert.IsTrue(submissioncofo);
        }
        [TestMethod]
        public void UpdateSubmissionLocationCofoTest()
        {
            //Initialize data to model
            var cofohopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
                Type = "HOP",
                StreetType = "Boulevard",
                Number = "",
                DateofIssue = "2015-09-25 00:00:00.000",
                StreetTypeId = 2

            };

            //Act 
            var submissioncofo = _submissionCofofopehopTestRepository.InsertSubmissionLocation(cofohopDetailsModel);

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
            Assert.IsTrue(submissioncofo);
        }
        [TestMethod]
        public void UpdateSubmissionLocationFalseTest()
        {
            //Initialize data to model
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
            var submissioncofo = _submissionCofofopehopTestRepository.InsertSubmissionLocation(cofohopDetailsModel);

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
            Assert.IsFalse(submissioncofo);
        }

        [TestMethod]
        public void GetPrimisessAddress()
        {
            //Initialize data to model
            var cofohopDetailsModel = new GeneralBusiness
            {
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c"
            };

            //Act 
            var submissioncofo = _submissionCofofopehopTestRepository.GetPrimisessAddress(cofohopDetailsModel);

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
            Assert.IsTrue(submissioncofo.AddressID == "306896");
        }
         [TestMethod]
        public void GetPrimisessAddresswithNull_Test()
        {
            //Initialize data to model
            var cofohopDetailsModel = new GeneralBusiness
            {
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97"
            };

            //Act 
            var submissioncofo = _submissionCofofopehopTestRepository.GetPrimisessAddress(cofohopDetailsModel);

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
            Assert.IsTrue(submissioncofo.FileNumber == "E1599417");
        }
        
        [TestMethod]
        public void DropDownBindTest()
        {
           
            //Act 
            var submissioncofo = _submissionCofofopehopTestRepository.DropDownBind();

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
            Assert.IsTrue(submissioncofo.FirstOrDefault().StreetType == "Alley");
        }
        [TestMethod]
        public void DeleteEHOPTest()
        {
            //Initialize data to model
            var cofohopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
                Type="EHOP"
            };

            //Act 
            var submissioncofo = _submissionCofofopehopTestRepository.DeleteCofo(cofohopDetailsModel);

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
            Assert.IsTrue(submissioncofo);
        }
        [TestMethod]
        public void DeleteCofoTest()
        {
            //Initialize data to model
            var cofohopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc611",
                Type = "COFO",
                DonothaveCof=false
            };

            //Act 
            var submissioncofo = _submissionCofofopehopTestRepository.DeleteCofo(cofohopDetailsModel);

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
            Assert.IsTrue(submissioncofo);
        }
        [TestMethod]
        public void DeleteCofoTrueTest()
        {
            //Initialize data to model
            var cofohopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc611",
                Type = "COFO",
                DonothaveCof = true
            };

            //Act 
            var submissioncofo = _submissionCofofopehopTestRepository.DeleteCofo(cofohopDetailsModel);

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
            Assert.IsTrue(submissioncofo);
        }
        [TestMethod]
        public void DeleteEHOPFalseTest()
        {
            //Initialize data to model
            var cofohopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
              
            };

            //Act 
            var submissioncofo = _submissionCofofopehopTestRepository.DeleteCofo(cofohopDetailsModel);

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
            Assert.IsFalse(submissioncofo);
        }
        [TestMethod]
        public void DeleteHOPTest()
        {
        

            //Act 
            var submissioncofo = _submissionCofofopehopTestRepository.DeleteHOP("cce0b056-d2a3-485e-a02a-e34c957c4e40");

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
            Assert.IsTrue(submissioncofo);
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
            var submissioncofo = _submissionCofofopehopTestRepository.DeleteHopPrimsesAddrssOnly(cofohopDetailsModel);

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
            Assert.IsTrue(submissioncofo);
        }

        [TestMethod]
        public void EhopNumberWithMasterIdTest()
        {
            //Act 
            var submissioncofo = _submissionCofofopehopTestRepository.EhopNumberWithMasterId("cce0b056-d2a3-485e-a02a-e34c957c4e40");

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
            Assert.IsTrue(submissioncofo.Count()==1);
        }
        [TestMethod]
        public void GetPrimisessAddressTest()
        {
            //Act 
            var submissioncofo = _submissionCofofopehopTestRepository.GetPrimisessAddress(1).ToList();

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
            Assert.IsTrue(submissioncofo.FirstOrDefault().AddressID == "306896");
        }
        [TestMethod]
        public void GetBusinessOwnerFullNameTest()
        {
            //Act 
            var submissioncofo = _submissionCofoHopeHopAddressRepository.GetBusinessOwnerFullName("4cb3a7f0-8e98-4b8d-86ae-a2d9dbe14959");

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
            Assert.IsTrue(submissioncofo == "");
        }
        [TestMethod]
        public void GetTradeNameWithSubmissionQuestionsTest()
        {
            //Act 
            var submissioncofo = _submissionCofoHopeHopAddressRepository.GetTradeNameWithSubmissionQuestions("4cb3a7f0-8e98-4b8d-86ae-a2d9dbe14959");

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
            Assert.IsTrue(submissioncofo == "");
        }
        [TestMethod]
        public void DisplayTaxAndRevenuWithPrimisessDetailsTest()
        {
            //Act 
            var submissioncofo = _submissionCofofopehopTestRepository.DisplayTaxAndRevenuWithPrimisessDetails("33b635b9-fec2-4b4f-9d07-686b8d6cc60c");

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
          Assert.IsTrue(submissioncofo.BussinessOwnerFullName == "");
        }
        [TestMethod]
        public void BusinessOwnerNameTest()
        {
            //Act 
            var submissioncofo = _submissionCofofopehopTestRepository.BusinessOwnerName("8c6deecc-3b72-485d-8af5-30939af94e97");

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
           Assert.IsTrue(submissioncofo == "");
        }
        [TestMethod]
        public void InsertSubmissionLocation_Return_Test()
        {
            //Initialize data to model
            var cofohopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
                Type = "EHOP",
                StreetType = "Boulevard",
                Number = "EH1501622",
                DateofIssue = "2015-09-25 00:00:00.000",
                StreetTypeId = 2

            };

            //Act 
            var submissioncofo = _submissionCofofopehopTestRepository.InsertSubmissionLocation(cofohopDetailsModel);

            //Assert
            Assert.IsNotNull(submissioncofo); // Test if null
            Assert.IsTrue(submissioncofo);
        }
        
    }
}
