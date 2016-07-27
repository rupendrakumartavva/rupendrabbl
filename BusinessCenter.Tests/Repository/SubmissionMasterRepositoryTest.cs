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

namespace BusinessCenter.Tests.Repository
{
    [TestClass]
    public class SubmissionMasterRepositoryTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Repository declaration
        private SubmissionMasterRepository _submissionMasterTestRepository;
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
        private SubmissionQuestionRepository _submissionQuestionRepository;
        private SubmissionMasterApplicationChcekListRepository _submissionMasterApplicationChcekListRepository;
        private OSubCategoryFeesRepository _oSubCategoryFeesRepository;
        private FixFeeRepository _fixedFeeTestRepository;
        private MasterSubCategoryRepository _masterSubCategoryRepository;
        private FeeCodeMapRepository _feeCodeMapRepository;
        private SubmissionMasterRenewalRepository _submissionMasterRenewalRepository;
        private MasterCategoryQuestionRepository _masterCategoryQuestionRepository;
        private MasterCategoryDocumentRepository _masterCategoryDocumentRepository;
        private UserRoleRepository _userRoleRepository;
      //  private MasterLicenseFEINRenewal _masterLicenseFeinRenewal;
        private DCBC_ENTITY_BBL_Renewal_InvoiceRepository _dcbcEntitybblRenewalInvoiceRepository;
        private Lookup_ExistingCategoriesRepository _lookupExistingCategoriesRepository;
      //  private MasterLicenseFEINRenewal masterLicenseFeinRenewal;
        private SubmissionLicenseNumberCounterRepository _submissionlicencecounter;
      //  private SubmissionLicenseNumberCounterRepository _submissionLicenseNumberCounterTestRepository;
        private MasterBblApplicationStatusRepository _masterBblApplicationStatusTestRepository;
        private MasterRenewalStatusFeeRepository _masterRenewalStatusFeeRepository;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _masterRenewalStatusFeeRepository=new MasterRenewalStatusFeeRepository(_testUnitOfWork);
            _submissionlicencecounter = new SubmissionLicenseNumberCounterRepository(_testUnitOfWork);
            _masterCategoryQuestionRepository=new MasterCategoryQuestionRepository(_testUnitOfWork);
            _masterCategoryDocumentRepository=new MasterCategoryDocumentRepository(_testUnitOfWork);
         
            _userRoleRepository=new UserRoleRepository(_testUnitOfWork);
          //  masterLicenseFeinRenewal = new MasterLicenseFEINRenewal(_testUnitOfWork);
            _masterBblApplicationStatusTestRepository = new MasterBblApplicationStatusRepository(_testUnitOfWork);
            //BblRepository Initialization
            _bblRepository = new BblRepository(_testUnitOfWork);
            _fixedFeeTestRepository=new FixFeeRepository(_testUnitOfWork);
            //Lookup_ExistingCategoriesRepository Initialization
            _lookupExistingCategoriesRepository = new Lookup_ExistingCategoriesRepository(_testUnitOfWork);
            _dcbcEntitybblRenewalInvoiceRepository = new DCBC_ENTITY_BBL_Renewal_InvoiceRepository(_testUnitOfWork, _lookupExistingCategoriesRepository);
            _submissionMasterRenewalRepository = new SubmissionMasterRenewalRepository(_testUnitOfWork, _masterRenewalStatusFeeRepository);
            //MasterSecondaryLicenseCategoryRepository Initialization
            _masterSecondaryLicenseCategoryRepository = new MasterSecondaryLicenseCategoryRepository(_testUnitOfWork);

            //SubmissionLicenseNumberCounterRepository Initialization
          //  _submissionLicenseNumberCounterTestRepository = new SubmissionLicenseNumberCounterRepository(_testUnitOfWork);

            //MasterPrimaryCategoryRepository Initialization
            _masterPrimaryCategoryRepository = new MasterPrimaryCategoryRepository(_testUnitOfWork,
            _masterSecondaryLicenseCategoryRepository,
            _masterCategoryDocumentRepository, _masterCategoryQuestionRepository);
            _oSubCategoryFeesRepository=new OSubCategoryFeesRepository(_testUnitOfWork,_masterPrimaryCategoryRepository,_masterSecondaryLicenseCategoryRepository);
            _feeCodeMapRepository = new FeeCodeMapRepository(_testUnitOfWork, _oSubCategoryFeesRepository);
            _masterSubCategoryRepository = new MasterSubCategoryRepository(_testUnitOfWork, _masterPrimaryCategoryRepository, _masterSecondaryLicenseCategoryRepository);
            //UserBBLServiceRepository Initialization
            _userBblServiceRepository = new UserBBLServiceRepository(_testUnitOfWork);

            //SubmissionQuestionRepository Initialization
            _submissionQuestionRepository = new SubmissionQuestionRepository(_testUnitOfWork);

            //MasterCategoryPhysicalLocationRepository Initialization
            _masterCategoryPhysicalLocationRepository = new MasterCategoryPhysicalLocationRepository(_testUnitOfWork,
            _masterPrimaryCategoryRepository, _masterCategoryQuestionRepository, _oSubCategoryFeesRepository, _masterSecondaryLicenseCategoryRepository);

            //SubmissionCategoryRepository Initialization
            _submissionCategoryRepository = new SubmissionCategoryRepository(_testUnitOfWork, _masterPrimaryCategoryRepository,
            _masterSecondaryLicenseCategoryRepository, _oSubCategoryFeesRepository, _fixedFeeTestRepository, _submissionQuestionRepository,
            _masterSubCategoryRepository, _masterCategoryPhysicalLocationRepository, _feeCodeMapRepository,
            _submissionMasterApplicationChcekListRepository, _submissionMasterRenewalRepository, _dcbcEntitybblRenewalInvoiceRepository, _lookupExistingCategoriesRepository);


            //SubmissionMasterApplicationChcekListRepository Initialization
            _submissionMasterApplicationChcekListRepository = new SubmissionMasterApplicationChcekListRepository(_testUnitOfWork);

            //UserRepository Initialization
            _userRepository = new UserRepository(_testUnitOfWork, _userRoleRepository);

            //DCBC_ENTITY_BBL_RenewalsRepository Initialization
            _dcbcEntityBblRenewalsRepository = new DCBC_ENTITY_BBL_RenewalsRepository(_testUnitOfWork);

            //MasterBusinessActivityRepository Initialization
            _masterBusinessActivityRepository = new MasterBusinessActivityRepository(_testUnitOfWork, _masterPrimaryCategoryRepository);

            //SubmissionBblAssociationToUsersRepository Initialization
            _submissionBblAssociationToUsersRepository = new SubmissionBblAssociationToUsersRepository(_testUnitOfWork);

            //SubmissionToAccelaRepository Initialization
            _submissionToAccelaRepository = new SubmissionToAccelaRepository(_testUnitOfWork);

            //SubmissionMasterRepository Initialization
            _submissionMasterTestRepository = new SubmissionMasterRepository(_testUnitOfWork, _submissionCategoryRepository, _bblRepository,
            _userBblServiceRepository, _submissionQuestionRepository, _masterCategoryPhysicalLocationRepository, _masterPrimaryCategoryRepository,
            _submissionMasterApplicationChcekListRepository, _userRepository, _dcbcEntityBblRenewalsRepository, _masterBusinessActivityRepository,
            _submissionBblAssociationToUsersRepository, _submissionToAccelaRepository, _masterSecondaryLicenseCategoryRepository,
            _lookupExistingCategoriesRepository, _submissionlicencecounter, _masterBblApplicationStatusTestRepository);



            //Setup MasterSecondaryLicenseCategory FackData Initialization
            var secondaryLicenceCategoryData = new MasterSecondaryLicenseCategoryRepositoryData();
            _testDbContext.MasterSecondaryLicenseCategory.AddRange(secondaryLicenceCategoryData.MasterSeconderyLicenseCategoryList);
            _testDbContext.SaveChanges();

            //Setup SubmissiontoAccela FackData Initialization
            var submissionToAccelaData = new SubmissionToAccelaRepositoryData();
            _testDbContext.SubmissiontoAccela.AddRange(submissionToAccelaData.SubmissiontoAccelaEntitiesList);
            _testDbContext.SaveChanges();

            //Setup SubmissionBblAssociationToUsers FackData Initialization
            var associatetoUserData = new SubmissionBblAssociationToUsersRepositoryData();
            _testDbContext.SubmissionBblAssociationToUsers.AddRange(associatetoUserData.SubmissionBblAssociationToUsersEntitiesList);
            _testDbContext.SaveChanges();

            //Setup MasterBusinessActivity FackData Initialization
            var businessActivityData = new MasterBusinessActivityData();
            _testDbContext.MasterBusinessActivity.AddRange(businessActivityData.MasterAcivityEntitiesList);
            _testDbContext.SaveChanges();

            //Setup DCBC_ENTITY_BBL_Renewals FackData Initialization
            var renewalData = new DCBC_ENTITY_BBL_RenewalsData();
            _testDbContext.DCBC_ENTITY_BBL_Renewals.AddRange(renewalData.DcbcEntityBblRenewalsList);
            _testDbContext.SaveChanges();

            //Setup User FackData Initialization
            var userData = new UserRepositoryData();
            _testDbContext.User.AddRange(userData.UsersEntitiesList);
            _testDbContext.SaveChanges();

            //Setup SubmissionMaster_ApplicationCheckList FackData Initialization
            var submissionMasterApplicationChcekListRepositoryData = new SubmissionMasterApplicationChcekListRepositoryData();
            _testDbContext.SubmissionMaster_ApplicationCheckList
                .AddRange(submissionMasterApplicationChcekListRepositoryData.SubmissionMaster_ApplicationCheckListEntitiesList);
            _testDbContext.SaveChanges();

            //Setup MasterPrimaryCategory FackData Initialization
            var primaryCategoryData = new MasterPrimaryCategoryRepositoryData();
            _testDbContext.MasterPrimaryCategory.AddRange(primaryCategoryData.MasterPrimaryCategoryEntitiesList);
            _testDbContext.SaveChanges();

            //Setup MasterCategoryPhysicalLocation FackData Initialization
            var masterCategoryPhysicalLocationData = new MasterCategoryPhysicalLocationData();
            _testDbContext.MasterCategoryPhysicalLocation.AddRange(masterCategoryPhysicalLocationData.MasterCategoryPhysicalLocationList);
            _testDbContext.SaveChanges();

            //Setup SubmissionMaster FackData Initialization
            var testData = new SubmissionMasterRepositoryData();
            _testDbContext.SubmissionMaster.AddRange(testData.SubmissionMasterEntitiesList);
            _testDbContext.SaveChanges();

            //Setup SubmissionCategory FackData Initialization
            var submissionCategoryData = new SubmissionCategoryRepositoryData();
            _testDbContext.SubmissionCategory.AddRange(submissionCategoryData.SubmissionCategoryEntitiesList);
            _testDbContext.SaveChanges();

            //Setup DCBC_ENTITY_BBL FackData Initialization
            var bblData = new BblRepositoryData();
            _testDbContext.DCBC_ENTITY_BBL.AddRange(bblData.BblEntitiesList);
            _testDbContext.SaveChanges();

            //Setup UserBBLService FackData Initialization
            var userBBlData = new UserBBLServiceRepositoryData();
            _testDbContext.UserBBLService.AddRange(userBBlData.UserBblServiceList);
            _testDbContext.SaveChanges();

            //Setup SubmissionQuestion FackData Initialization
            var submissionQuestionData = new SubmissionQuestionRepositoryData();
            _testDbContext.SubmissionQuestion.AddRange(submissionQuestionData.SubmissionQuestionEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  SubmissionLicenseNumberCounter FackData Initialization
            var submissionLicenseCounterData = new SubmissionLicenseNumberCounterData();
            _testDbContext.SubmissionLicenseNumberCounter.AddRange(submissionLicenseCounterData.SubmissionLicenseCounterEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  MasterBblApplicationStatus FackData Initialization
            var masterBblApplicationStatusData = new MasterBblApplicationStatusData();
            _testDbContext.MasterBblApplicationStatus.AddRange(masterBblApplicationStatusData.MasterBblApplicationStatusEntitiesList);
            _testDbContext.SaveChanges();
        }

        [TestMethod]
        public void AllSubmissionMasterTest()
        {
            //Act
            var submissionmaster = _submissionMasterTestRepository.AllSubmissionMaster();

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster.Count() == 5);
        }

        //[TestMethod]
        //public void AllSubmissionsTest()
        //{
        //    //Act
        //    var submissionmaster = _submissionMasterTestRepository.AllSubmissions("FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F");

        //    //Assert
        //    Assert.IsNotNull(submissionmaster); // Test if null
        //    Assert.IsTrue(submissionmaster.Count() == 4);
        //}

        //[TestMethod]
        //public void AllSubmissionsUserTest()
        //{
        //    //Act
        //    var submissionmaster = _submissionMasterTestRepository.AllSubmissions(string.Empty);

        //    //Assert
        //    Assert.IsNotNull(submissionmaster); // Test if null
        //    Assert.IsTrue(submissionmaster.Count() == 4);
        //}

        [TestMethod]
        public void FindByIDTest()
        {
            //EntityData is Added
            var submissionMasterModel = new SubmissionMasterModel { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97" };
            //Act
            var submissionmaster = _submissionMasterTestRepository.FindByID(submissionMasterModel);

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster.Count() == 1);
        }

        [TestMethod]
        public void FindByEntityIDTest()
        {
            //Act
            var submissionmaster = _submissionMasterTestRepository.FindByEntityID("DAPP15985360");

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster.Count() == 1);
        }

        [TestMethod]
        public void FindByMasterIDTest()
        {
            //Act
            var submissionmaster = _submissionMasterTestRepository.FindByMasterID("8c6deecc-3b72-485d-8af5-30939af94e97");

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster.Count() == 1);
        }

        [TestMethod]
        public void GetMasterIdTest()
        {
            //Act
            var submissionmaster = _submissionMasterTestRepository.GetMasterId("DAPP15985360", "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F");

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster.Count() == 1);
        }

        [TestMethod]
        public void UpdateSubmissionMasterTest()
        {
            //EntityData is Added
            var paymentDetailsModel = new PaymentDetailsModel { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97", PaymentType = "Submission" };
            //Act
            var submissionmaster = _submissionMasterTestRepository.UpdateSubmissionMaster(paymentDetailsModel);

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster);
        }

        [TestMethod]
        public void UpdateSubmissionMasterInTest()
        {
            //EntityData is Added
            var paymentDetailsModel = new PaymentDetailsModel { MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40", PaymentType = "Submission" };
            //Act
            var submissionmaster = _submissionMasterTestRepository.UpdateSubmissionMaster(paymentDetailsModel);

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster);
        }

        [TestMethod]
        public void UpdateSubmissionMasterNullTest()
        {
            //EntityData is Added
            var paymentDetailsModel = new PaymentDetailsModel { MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40", PaymentType = "Submission" };
            //Act
            var submissionmaster = _submissionMasterTestRepository.UpdateSubmissionMaster(paymentDetailsModel);

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster);
        }

        [TestMethod]
        public void UpdateSubmissionMasterIndividualTest()
        {
            //EntityData is Added
            var paymentDetailsModel = new PaymentDetailsModel { MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c", PaymentType = "Submission" };
            //Act
            var submissionmaster = _submissionMasterTestRepository.UpdateSubmissionMaster(paymentDetailsModel);

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster);
        }

        [TestMethod]
        public void UpdateSubmissionMasterNodocTest()
        {
            //EntityData is Added
            var paymentDetailsModel = new PaymentDetailsModel { MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c", PaymentType = "Submission" };
            //Act
            var submissionmaster = _submissionMasterTestRepository.UpdateSubmissionMaster(paymentDetailsModel);

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster);
        }

        [TestMethod]
        public void UpdateSubmissionMasterRenewalTest()
        {
            //EntityData is Added
            var paymentDetailsModel = new PaymentDetailsModel { MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c", PaymentType = "RENEWAL" };
            //Act
            var submissionmaster = _submissionMasterTestRepository.UpdateSubmissionMaster(paymentDetailsModel);

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster);
        }

        [TestMethod]
        public void UpdateSubmissionMasterFalseTest()
        {
            //EntityData is Added
            var paymentDetailsModel = new PaymentDetailsModel { MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c" };
            //Act
            var submissionmaster = _submissionMasterTestRepository.UpdateSubmissionMaster(paymentDetailsModel);

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsFalse(submissionmaster);
        }

        [TestMethod]
        public void ValidateBusinessLocationNoTest()
        {
            //EntityData is Added
            var questions = new List<ScreeningQuestion>();
            ScreeningQuestion submissionQuestion = new ScreeningQuestion();
            submissionQuestion.Question = "Will this business be located in the District of Columbia?";
            submissionQuestion.Answer = "NO";
            questions.Add(submissionQuestion);


            var submissionApplication = new SubmissionApplication { SubQuestion = questions };
            //Act
            var submissionmaster = _submissionMasterTestRepository.ValidateBusinessLocation(submissionApplication, "");

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster == "NO");
        }

        [TestMethod]
        public void ValidateBusinessLocationNullTest()
        {
            //EntityData is Added
            var questions = new List<ScreeningQuestion>();
            ScreeningQuestion submissionQuestion = new ScreeningQuestion();
            submissionQuestion.Question = "Will this business be located in the District of Columbia?";
            submissionQuestion.Answer = null;
            questions.Add(submissionQuestion);

            var submissionApplication = new SubmissionApplication { SubQuestion = questions };


            //Act
            var submissionmaster = _submissionMasterTestRepository.ValidateBusinessLocation(submissionApplication, "");

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster == "YES");
        }

        [TestMethod]
        public void ValidateBusinessLocationYesTest()
        {
            //EntityData is Added
            var questions = new List<ScreeningQuestion>();
            var submissionQuestion = new ScreeningQuestion
            {
                Question = "Will this business be located in the District of Columbia?",
                Answer = "YES"
            };
            questions.Add(submissionQuestion);
            var submissionApplication = new SubmissionApplication { SubQuestion = questions };


            //Act
            var submissionmaster = _submissionMasterTestRepository.ValidateBusinessLocation(submissionApplication, "");

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster == "YES");
        }

        [TestMethod]
        public void ValidateBusinessLocationTest()
        {
            //EntityData is Added
            var questions = new List<ScreeningQuestion>();
            ScreeningQuestion submissionQuestion = new ScreeningQuestion();
            submissionQuestion.Question = "Will this business be located in the District of Columb?";
            submissionQuestion.Answer = "YES";
            questions.Add(submissionQuestion);
            var submissionApplication = new SubmissionApplication { SubQuestion = questions };


            //Act
            var submissionmaster = _submissionMasterTestRepository.ValidateBusinessLocation(submissionApplication, "");

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster == "");
        }

        [TestMethod]
        public void ValidateIsHomeBasedTest()
        {
            //EntityData is Added
            var questions = new List<ScreeningQuestion>();
            ScreeningQuestion submissionQuestion = new ScreeningQuestion();
            submissionQuestion.Question = "Will this Business be Home based?";
            submissionQuestion.Answer = "YES";
            questions.Add(submissionQuestion);
            var submissionApplication = new SubmissionApplication { SubQuestion = questions };


            //Act
            var submissionmaster = _submissionMasterTestRepository.ValidateIsHomeBased(submissionApplication);

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster == "YES");
        }

        [TestMethod]
        public void ValidateIsEHopAllowTest()
        {
            //EntityData is Added
            var questions = new List<ScreeningQuestion>();
            ScreeningQuestion submissionQuestion = new ScreeningQuestion();
            submissionQuestion.Question = "Do you have a Home Occupancy Permit (HOP) from Office of Zoning?";
            submissionQuestion.Answer = "YES";
            questions.Add(submissionQuestion);
            var submissionApplication = new SubmissionApplication { SubQuestion = questions };


            //Act
            var submissionmaster = _submissionMasterTestRepository.ValidateIsEHopAllow(submissionApplication);

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster == "YES");
        }

        [TestMethod]
        public void ValidateBusinessStructureTest()
        {
            //EntityData is Added
            var questions = new List<ScreeningQuestion>();
            ScreeningQuestion submissionQuestion = new ScreeningQuestion();
            submissionQuestion.Question = "What is your Business Structure ?";
            submissionQuestion.Answer = "YES";
            questions.Add(submissionQuestion);
            var submissionApplication = new SubmissionApplication { SubQuestion = questions };


            //Act
            var submissionmaster = _submissionMasterTestRepository.ValidateBusinessStructure(submissionApplication);

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster == "YES");
        }

        [TestMethod]
        public void ValidateBusinessStructureNullTest()
        {
            //EntityData is Added
            var questions = new List<ScreeningQuestion>();
            ScreeningQuestion submissionQuestion = new ScreeningQuestion();
            submissionQuestion.Question = "What is your Business Structure ?";
            submissionQuestion.Answer = null;
            questions.Add(submissionQuestion);
            var submissionApplication = new SubmissionApplication { SubQuestion = questions };


            //Act
            var submissionmaster = _submissionMasterTestRepository.ValidateBusinessStructure(submissionApplication);

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster == "");
        }

        [TestMethod]
        public void ValidateBusinessStructure_Null_Test()
        {
            //EntityData is Added
            var questions = new List<ScreeningQuestion>();
            ScreeningQuestion submissionQuestion = new ScreeningQuestion();
            submissionQuestion.Question = "What is your Business Structu ?";
            submissionQuestion.Answer = null;
            questions.Add(submissionQuestion);
            var submissionApplication = new SubmissionApplication { SubQuestion = questions };


            //Act
            var submissionmaster = _submissionMasterTestRepository.ValidateBusinessStructure(submissionApplication);

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster == "");
        }

        [TestMethod]
        public void ValidateBusinessTradeNameTest()
        {
            //EntityData is Added
            var questions = new List<ScreeningQuestion>();
            ScreeningQuestion submissionQuestion = new ScreeningQuestion();
            submissionQuestion.Question = "What is the Trade Name?";
            submissionQuestion.Answer = "TRADE NAME";
            questions.Add(submissionQuestion);
            var submissionApplication = new SubmissionApplication { SubQuestion = questions };

            //Act
            var submissionmaster = _submissionMasterTestRepository.ValidateBusinessTradeName(submissionApplication);

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster == "TRADE NAME");
        }

        [TestMethod]
        public void ValidateBusinessTradeNameNullTest()
        {
            //EntityData is Added
            var questions = new List<ScreeningQuestion>();
            ScreeningQuestion submissionQuestion = new ScreeningQuestion();
            submissionQuestion.Question = "What is the Trade Name?";
          //  submissionQuestion.Answer = null;
            questions.Add(submissionQuestion);
            var submissionApplication = new SubmissionApplication { SubQuestion = questions };


            //Act
            var submissionmaster = _submissionMasterTestRepository.ValidateBusinessTradeName(submissionApplication);

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster == "");
        }
        [TestMethod]
        public void ValidateBusinessTradeName_Null_Test()
        {
            //EntityData is Added
            var questions = new List<ScreeningQuestion>();
            ScreeningQuestion submissionQuestion = new ScreeningQuestion();
            submissionQuestion.Question = "What is the Trade Na?";
            //  submissionQuestion.Answer = null;
            questions.Add(submissionQuestion);
            var submissionApplication = new SubmissionApplication { SubQuestion = questions };


            //Act
            var submissionmaster = _submissionMasterTestRepository.ValidateBusinessTradeName(submissionApplication);

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster == "");
        }

        [TestMethod]
        public void ValidateCorporationDivisionTest()
        {
            //EntityData is Added
            var questions = new List<ScreeningQuestion>();
            ScreeningQuestion submissionQuestion = new ScreeningQuestion();
            submissionQuestion.Question = "Is this business already registered with DCRA’s Corporations Division?";
            submissionQuestion.Answer = "YES";
            questions.Add(submissionQuestion);
            var submissionApplication = new SubmissionApplication { SubQuestion = questions };


            //Act
            var submissionmaster = _submissionMasterTestRepository.ValidateCorporationDivision(submissionApplication);

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster);
        }
        [TestMethod]
        public void ValidateCorporationDivision_null_Test()
        {
            //EntityData is Added
            var questions = new List<ScreeningQuestion>();
            ScreeningQuestion submissionQuestion = new ScreeningQuestion();
            submissionQuestion.Question = "Is this business already registered with DCRA’s Corporations Division?";
          //  submissionQuestion.Answer = "YES";
            questions.Add(submissionQuestion);
            var submissionApplication = new SubmissionApplication { SubQuestion = questions };


            //Act
            var submissionmaster = _submissionMasterTestRepository.ValidateCorporationDivision(submissionApplication);

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster);
        }
        [TestMethod]
        public void ValidateCorporationDivisionNullTest()
        {
            //EntityData is Added
            var questions = new List<ScreeningQuestion>();
            ScreeningQuestion submissionQuestion = new ScreeningQuestion();
            submissionQuestion.Question = "Is this business already registered with DCRA’s Corporations Divisi?";
          //  submissionQuestion.Answer = null;
            questions.Add(submissionQuestion);
            var submissionApplication = new SubmissionApplication { SubQuestion = questions };


            //Act
            var submissionmaster = _submissionMasterTestRepository.ValidateCorporationDivision(submissionApplication);

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsFalse(submissionmaster);
        }
          [TestMethod]
        public void ValidateCorporationDivision_Null_Test()
        {
            //EntityData is Added
            var questions = new List<ScreeningQuestion>();
            ScreeningQuestion submissionQuestion = new ScreeningQuestion();
            submissionQuestion.Question = "Is this business already registered with DCRA’s Corporations Division?";
          //  submissionQuestion.Answer = null;
            questions.Add(submissionQuestion);
            var submissionApplication = new SubmissionApplication { SubQuestion = questions };


            //Act
            var submissionmaster = _submissionMasterTestRepository.ValidateCorporationDivision(submissionApplication);

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster);
        }
        
        //
        [TestMethod]
        public void ValidateIsHomeBasedYesTest()
        {
            //EntityData is Added
            var questions = new List<ScreeningQuestion>();
            ScreeningQuestion submissionQuestion = new ScreeningQuestion();
            submissionQuestion.Question = "Will this Business be Home based?";
            submissionQuestion.Answer = "YES";
            questions.Add(submissionQuestion);
            var submissionApplication = new SubmissionApplication { SubQuestion = questions };


            //Act
            var submissionmaster = _submissionMasterTestRepository.ValidateIsHomeBased(submissionApplication);

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster == "YES");
        }

        //

        [TestMethod]
        public void GetUserAssociateBblListCountTest()
        {
            //EntityData is Added
            var bblAsscoiateService = new BblAsscoiateService { UserID = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F" };


            //Act
            var submissionmaster = _submissionMasterTestRepository.GetUserAssociateBblListCount(bblAsscoiateService);

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster == 0);
        }

        [TestMethod]
        public void BBlServiceListTest()
        {
            //EntityData is Added
            var bblAsscoiateService = new BblAsscoiateService { UserID = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F" };

            string MultipleLicenseName = string.Empty;
            //Act
            var submissionmaster = _submissionMasterTestRepository.BBlServiceList(bblAsscoiateService).ToList();


            foreach (var item in submissionmaster[0].BblServiceList)
            {
                MultipleLicenseName = item.MultipleLicense;
            }

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(MultipleLicenseName == null);
        }

        [TestMethod]
        public void ServiceExpiryStatusLapsedTest()
        {
            //Act
            var submissionmaster = _submissionMasterTestRepository.ServiceExpiryStatus(DateTime.Now.AddDays(-28));

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster == "Lapsed");
        }

        [TestMethod]
        public void ServiceExpiryStatusActiveTest()
        {
            //Act
            var submissionmaster = _submissionMasterTestRepository.ServiceExpiryStatus(DateTime.Now.AddDays(92));

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster == "ACTIVE");
        }

        [TestMethod]
        public void ServiceExpiryStatusExpiringSoonTest()
        {
            //Act
            var submissionmaster = _submissionMasterTestRepository.ServiceExpiryStatus(DateTime.Now.AddDays(80));

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster == "Expiring Soon");
        }

        [TestMethod]
        public void ServiceExpiryStatusRenewalTest()
        {
            //Act
            var submissionmaster = _submissionMasterTestRepository.ServiceExpiryStatus(DateTime.Now.AddDays(-200));

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster == "Unable to Be Renewed");
        }

        [TestMethod]
        public void ServiceExpiryStatusExpiredTest()
        {
            //Act
            var submissionmaster = _submissionMasterTestRepository.ServiceExpiryStatus(DateTime.Now.AddDays(-110));

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster == "Expired");
        }

        [TestMethod]
        public void UpdateDocumentTypeTest()
        {
            //EntityData is Added
            var bblDocuments = new BblDocuments { MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c", DocSubType = "ON" };

            //Act
            var submissionmaster = _submissionMasterTestRepository.UpdateDocumentType(bblDocuments);

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster);
        }

        [TestMethod]
        public void UpdateUserSelectTest()
        {
            //EntityData is Added
            var generalBusiness = new GeneralBusiness { MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c", UserSelectTpe = "NEWMAIL" };

            //Act
            var submissionmaster = _submissionMasterTestRepository.UpdateUserSelect(generalBusiness);

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster);
        }


        [TestMethod]
        public void UpdateUserSelect_excception_Test()
        {
            //EntityData is Added
            var generalBusiness = new GeneralBusiness { MasterId = "33b635b9-fec2-4b4f-9d07-", UserSelectTpe = "NEWMAIL" };

            //Act
            var submissionmaster = _submissionMasterTestRepository.UpdateUserSelect(generalBusiness);

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster);
        }

        [TestMethod]
        public void GetMailTypeTest()
        {
            //EntityData is Added
            var generalBusiness = new GeneralBusiness { MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c", UserSelectTpe = "NEWMAIL" };

            //Act
            var submissionmaster = _submissionMasterTestRepository.GetMailType(generalBusiness);

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster.UserType == "NEWMAIL");
        }

        [TestMethod]
        public void UpdateBussinesStructureTest()
        {
            //EntityData is Added
            var generalBusiness = new GeneralBusiness { MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c", BusinessStructure = "Sole", TradeName = "Name" };


            //Act
            var submissionmaster = _submissionMasterTestRepository.UpdateBusinessStructure(generalBusiness);

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster);
        }

        [TestMethod]
        public void UpdateEhopTotalTest()
        {
            //Act
            var submissionmaster = _submissionMasterTestRepository.UpdateEhopTotal(500, "33b635b9-fec2-4b4f-9d07-686b8d6cc60c");

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster);
        }

        [TestMethod]
        public void ServiceExpiryStatusTest()
        {
            //Act
            var submissionmaster = _submissionMasterTestRepository.ServiceExpiryStatus(DateTime.Now.AddDays(-3));

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster == "Lapsed");
        }



        [TestMethod]
        public void UpdateEhopTest()
        {
            //EntityData is Added
            var cofoHopDetailsModel = new CofoHopDetailsModel { MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c" };

            //Act
            var submissionmaster = _submissionMasterTestRepository.UpdateEhop(cofoHopDetailsModel);

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster);
        }

        [TestMethod]
        public void ValidateBblLicenceTest()
        {
            //Act
            var submissionmaster = _submissionMasterTestRepository.ValidateBblLicence("931315000136");

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster == "IMA PIZZA STORE 13 LLC.");
        }

        [TestMethod]
        public void UpdateEhopNonSelectTest()
        {
            //Act
            var submissionmaster = _submissionMasterTestRepository.UpdateEhopNonSelect("33b635b9-fec2-4b4f-9d07-686b8d6cc60c");

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster);
        }

        [TestMethod]
        public void UpdateRenwalDocumentTypeTest()
        {
            //EntityData is Added
            var documentCheck = new DocumentCheck { MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c", DocType = "ON" };


            //Act
            var submissionmaster = _submissionMasterTestRepository.UpdateRenwalDocumentType(documentCheck);

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster);
        }

        [TestMethod]
        public void MasterStatusTest()
        {
            //Act
            var submissionmaster = _submissionMasterTestRepository.MasterStatus("33b635b9-fec2-4b4f-9d07-686b8d6cc60c");

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster.Status == "UnderReview");
        }

        [TestMethod]
        public void UpdateUserAssociateExpiryDateTest()
        {
            //Act
            var submissionmaster = _submissionMasterTestRepository.UpdateUserAssociateExpiryDate("8c6deecc-3b72-485d-8af5-30939af94e97");

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster);
        }
        
        [TestMethod]
        public void UpdateUserAssociate_ExpiryDateTest()
        {
            //Act
            var submissionmaster = _submissionMasterTestRepository.UpdateUserAssociateExpiryDate("1c6deeee-3b72-485d-8af5-30939af94d99");

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster);
        }

        [TestMethod]
        public void TransferSubmissionsTest()
        {
            //EntityData is Added
            var submissiontransfer = new Submissiontransfer { MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c" };

            //Act
            var submissionmaster = _submissionMasterTestRepository.TransferSubmissions(submissiontransfer);

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster);
        }
        [TestMethod]
        public void TransferSubmissions_No_Test()
        {
            //EntityData is Added
            var submissiontransfer = new Submissiontransfer { MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60", LicenseNumber = "LREN13018589", ToUserId = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F" };

            //Act
            var submissionmaster = _submissionMasterTestRepository.TransferSubmissions(submissiontransfer);

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster);
        }
        [TestMethod]
        public void GetmasterIdTest()
        {
            //Act
            var submissionmaster = _submissionMasterTestRepository.GetmasterId("DAPP15985360", "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F");

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster == "cce0b056-d2a3-485e-a02a-e34c957c4e40");
        }

        [TestMethod]
        public void UpdateNoDocStatusTest()
        {
            //Act
            var submissionmaster = _submissionMasterTestRepository.UpdateNoDocStatus("33b635b9-fec2-4b4f-9d07-686b8d6cc60c");

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster);
        }

        //
        [TestMethod]
        public void InsertAssociateBblServiceTest()
        {
            //EntityData is Added
            var questions = new List<ScreeningQuestion>();
            ScreeningQuestion submissionQuestion = new ScreeningQuestion();
            submissionQuestion.Question = "How many Seats in your Restaurant?";
            submissionQuestion.Answer = "100";
            questions.Add(submissionQuestion);
            ScreeningQuestion submissionQuestion1 = new ScreeningQuestion();
            submissionQuestion1.Question = "Would you like a two (2) or four (4) year license?";
            submissionQuestion1.Answer = "Four (4) year";
            questions.Add(submissionQuestion1);
            ScreeningQuestion submissionQuestion2 = new ScreeningQuestion();
            submissionQuestion2.Question = "Is this business already registered with DCRA’s Corporations Division?";
            submissionQuestion2.Answer = "YES";
            questions.Add(submissionQuestion2);
            ScreeningQuestion submissionQuestion3 = new ScreeningQuestion();
            submissionQuestion3.Question =
                "Which Tax Identification Number is associated with your business: Federal Employer Identification Number (FEIN) or  Social Security Number (SSN)?";
            submissionQuestion3.Answer = "FEIN";
            questions.Add(submissionQuestion3);
            ScreeningQuestion submissionQuestion4 = new ScreeningQuestion();
            submissionQuestion4.Question =
                " Will this business be located in the District of Columbia?";
            submissionQuestion4.Answer = "YES";
            questions.Add(submissionQuestion4);
            var detailedCategory = new List<DetailedCategoryList>();
            DetailedCategoryList detailedCategoryList = new DetailedCategoryList
            {
                CategoryId = "16FD71A3-7DD7-4B65-A006-777691BCB7FC"
            };
            detailedCategory.Add(detailedCategoryList);
            var submissionApplication = new SubmissionApplication
            {
                SubQuestion = questions,
                PrimaryID = "16FD71A3-7DD7-4B65-A006-777691BCB7FC",//"E037E31E-9DA2-47F0-A2AD-7FFF385EB1B3",
                App_Type = "B",
                DetailedCategoryList = detailedCategory,
                LicenseCategory = "SOLICITOR"
            };


            //Act
            var submissionmaster = _submissionMasterTestRepository.InsertAssociateBblService(submissionApplication);

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            //Assert.IsFalse(submissionmaster);
        }

        //[TestMethod]
        //public void InsertRenewDataTest()
        //{
        //    var renewModel = new RenewModel { EntityId="1" };
        //    //Act
        //    var submissionmaster = _submissionMasterTestRepository.InsertRenewData(renewModel);

        //    //Assert
        //    Assert.IsNotNull(submissionmaster);
        //  //  Assert.IsTrue(submissionmaster);
        //}
        //[TestMethod]
        //public void UpdateRenewDataTest()
        //{
        //    var renewModel = new RenewModel { EntityId = "1", UserId = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F" };
        //    //Act
        //    var submissionmaster = _submissionMasterTestRepository.InsertRenewData(renewModel);

        //    //Assert
        //    Assert.IsNotNull(submissionmaster);
        //    Assert.IsTrue(submissionmaster == "8c6deecc-3b72-485d-8af5-30939af94e97");
        //}
        [TestMethod]
        public void InsertAssociateBblServiceHopTest()
        {
            //EntityData is Added
            var questions = new List<ScreeningQuestion>();
            ScreeningQuestion submissionQuestion = new ScreeningQuestion();
            submissionQuestion.Question = "How many Seats in your Restaurant?";
            submissionQuestion.Answer = "100";
            questions.Add(submissionQuestion);
            ScreeningQuestion submissionQuestion1 = new ScreeningQuestion();
            submissionQuestion1.Question = "Would you like a two (2) or four (4) year license?";
            submissionQuestion1.Answer = "Four (4) year";
            questions.Add(submissionQuestion1);
            ScreeningQuestion submissionQuestion2 = new ScreeningQuestion();
            submissionQuestion2.Question = "Is this business already registered with DCRA’s Corporations Division?";
            submissionQuestion2.Answer = "YES";
            questions.Add(submissionQuestion2);
            ScreeningQuestion submissionQuestion3 = new ScreeningQuestion();
            submissionQuestion3.Question =
                "Which Tax Identification Number is associated with your business: Federal Employer Identification Number (FEIN) or  Social Security Number (SSN)?";
            submissionQuestion3.Answer = "FEIN";
            questions.Add(submissionQuestion3);
            ScreeningQuestion submissionQuestion4 = new ScreeningQuestion();
            submissionQuestion4.Question =
                " Will this business be located in the District of Columbia?";
            submissionQuestion4.Answer = "YES";
            questions.Add(submissionQuestion4);

            var detailedCategory = new List<DetailedCategoryList>();
            DetailedCategoryList detailedCategoryList = new DetailedCategoryList();
            detailedCategoryList.CategoryId = "BDABCA0B-8B80-4CC6-8836-6D5A751B6C54";
            detailedCategory.Add(detailedCategoryList);
            var submissionApplication = new SubmissionApplication
            {
                SubQuestion = questions,
                PrimaryID = "BDABCA0B-8B80-4CC6-8836-6D5A751B6C54",//"E037E31E-9DA2-47F0-A2AD-7FFF385EB1B3",
                Secondary = "1B69B449-ECAB-41AE-89A9-E2F76329A52B",
                App_Type = "B",
                DetailedCategoryList = detailedCategory,
                LicenseCategory = "SOLICITOR"
            };

            //Act
            var submissionmaster = _submissionMasterTestRepository.InsertAssociateBblService(submissionApplication);

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            //Assert.IsFalse(submissionmaster);
        }

        [TestMethod]
        public void InsertAssociateBblServiceCofoTest()
        {
            //EntityData is Added
            var questions = new List<ScreeningQuestion>();
            ScreeningQuestion submissionQuestion = new ScreeningQuestion();
            submissionQuestion.Question = "How many Seats in your Restaurant?";
            submissionQuestion.Answer = "100";
            questions.Add(submissionQuestion);
            ScreeningQuestion submissionQuestion1 = new ScreeningQuestion();
            submissionQuestion1.Question = "Would you like a two (2) or four (4) year license?";
            submissionQuestion1.Answer = "Four (4) year";
            questions.Add(submissionQuestion1);
            ScreeningQuestion submissionQuestion2 = new ScreeningQuestion();
            submissionQuestion2.Question = "Is this business already registered with DCRA’s Corporations Division?";
            submissionQuestion2.Answer = "YES";
            questions.Add(submissionQuestion2);
            ScreeningQuestion submissionQuestion3 = new ScreeningQuestion();
            submissionQuestion3.Question =
                "Which Tax Identification Number is associated with your business: Federal Employer Identification Number (FEIN) or  Social Security Number (SSN)?";
            submissionQuestion3.Answer = "FEIN";
            questions.Add(submissionQuestion3);
            ScreeningQuestion submissionQuestion4 = new ScreeningQuestion();
            submissionQuestion4.Question =
                " Will this business be located in the District of Columbia?";
            submissionQuestion4.Answer = "YES";
            questions.Add(submissionQuestion4);

            var detailedCategory = new List<DetailedCategoryList>();
            DetailedCategoryList detailedCategoryList = new DetailedCategoryList();
            detailedCategoryList.CategoryId = "796B2CFD-EBDC-4480-B45C-502A816860FB";
            detailedCategory.Add(detailedCategoryList);
            var submissionApplication = new SubmissionApplication
            {
                SubQuestion = questions,
                PrimaryID = "796B2CFD-EBDC-4480-B45C-502A816860FB",//"E037E31E-9DA2-47F0-A2AD-7FFF385EB1B3",
                Secondary = "4402E4B3-BA07-4D1E-8660-76A64D540702",
                App_Type = "B",
                DetailedCategoryList = detailedCategory,
                  LicenseCategory = "SOLICITOR"
            };

            //Act
            var submissionmaster = _submissionMasterTestRepository.InsertAssociateBblService(submissionApplication);

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            //Assert.IsFalse(submissionmaster);
        }

        [TestMethod]
        public void InsertAssociateBblServiceHomeTest()
        {
            //EntityData is Added
            var questions = new List<ScreeningQuestion>();
            ScreeningQuestion submissionQuestion = new ScreeningQuestion();
            submissionQuestion.Question = "How many Seats in your Restaurant?";
            submissionQuestion.Answer = "100";
            questions.Add(submissionQuestion);
            ScreeningQuestion submissionQuestion1 = new ScreeningQuestion();
            submissionQuestion1.Question = "Would you like a two (2) or four (4) year license?";
            submissionQuestion1.Answer = "Four (4) year";
            questions.Add(submissionQuestion1);
            ScreeningQuestion submissionQuestion2 = new ScreeningQuestion();
            submissionQuestion2.Question = "Is this business already registered with DCRA’s Corporations Division?";
            submissionQuestion2.Answer = "YES";
            questions.Add(submissionQuestion2);
            ScreeningQuestion submissionQuestion3 = new ScreeningQuestion();
            submissionQuestion3.Question =
                "Which Tax Identification Number is associated with your business: Federal Employer Identification Number (FEIN) or  Social Security Number (SSN)?";
            submissionQuestion3.Answer = "FEIN";
            questions.Add(submissionQuestion3);
            ScreeningQuestion submissionQuestion4 = new ScreeningQuestion();
            submissionQuestion4.Question =
                " Will this business be located in the District of Columbia?";
            submissionQuestion4.Answer = "YES";
            questions.Add(submissionQuestion4);
            ScreeningQuestion submissionQuestion5 = new ScreeningQuestion();
            submissionQuestion5.Question =
                "Will this Business be Home based?";
            submissionQuestion5.Answer = "YES";
            questions.Add(submissionQuestion5);

            var detailedCategory = new List<DetailedCategoryList>();
            DetailedCategoryList detailedCategoryList = new DetailedCategoryList();
            detailedCategoryList.CategoryId = "BDABCA0B-8B80-4CC6-8836-6D5A751B6C54";
            detailedCategory.Add(detailedCategoryList);
            var submissionApplication = new SubmissionApplication
            {
                SubQuestion = questions,
                PrimaryID = "BDABCA0B-8B80-4CC6-8836-6D5A751B6C54",//"E037E31E-9DA2-47F0-A2AD-7FFF385EB1B3",
                Secondary = "1B69B449-ECAB-41AE-89A9-E2F76329A52B",
                App_Type = "B",
                DetailedCategoryList = detailedCategory,
                LicenseCategory = "SOLICITOR"
            };


            //Act
            var submissionmaster = _submissionMasterTestRepository.InsertAssociateBblService(submissionApplication);

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            //Assert.IsFalse(submissionmaster);
        }

        [TestMethod]
        public void InsertAssociateBblServiceHomeHopTest()
        {
            //EntityData is Added
            var questions = new List<ScreeningQuestion>();
            ScreeningQuestion submissionQuestion = new ScreeningQuestion();
            submissionQuestion.Question = "How many Seats in your Restaurant?";
            submissionQuestion.Answer = "100";
            questions.Add(submissionQuestion);
            ScreeningQuestion submissionQuestion1 = new ScreeningQuestion();
            submissionQuestion1.Question = "Would you like a two (2) or four (4) year license?";
            submissionQuestion1.Answer = "Four (4) year";
            questions.Add(submissionQuestion1);
            ScreeningQuestion submissionQuestion2 = new ScreeningQuestion();
            submissionQuestion2.Question = "Is this business already registered with DCRA’s Corporations Division?";
            submissionQuestion2.Answer = "NO";
            questions.Add(submissionQuestion2);
            ScreeningQuestion submissionQuestion3 = new ScreeningQuestion();
            submissionQuestion3.Question =
                "Which Tax Identification Number is associated with your business: Federal Employer Identification Number (FEIN) or  Social Security Number (SSN)?";
            submissionQuestion3.Answer = "FEIN";
            questions.Add(submissionQuestion3);
            ScreeningQuestion submissionQuestion4 = new ScreeningQuestion();
            submissionQuestion4.Question =
                " Will this business be located in the District of Columbia?";
            submissionQuestion4.Answer = "YES";
            questions.Add(submissionQuestion4);
            ScreeningQuestion submissionQuestion5 = new ScreeningQuestion();
            submissionQuestion5.Question =
                "Will this Business be Home based?";
            submissionQuestion5.Answer = "YES";
            questions.Add(submissionQuestion5);
            ScreeningQuestion submissionQuestion6 = new ScreeningQuestion();
            submissionQuestion6.Question =
                "Do you have a Home Occupancy Permit (HOP) from Office of Zoning?";
            submissionQuestion6.Answer = "YES";
            questions.Add(submissionQuestion6);

            var detailedCategory = new List<DetailedCategoryList>();
            DetailedCategoryList detailedCategoryList = new DetailedCategoryList();
            detailedCategoryList.CategoryId = "BDABCA0B-8B80-4CC6-8836-6D5A751B6C54";
            detailedCategory.Add(detailedCategoryList);
            var submissionApplication = new SubmissionApplication
            {
                SubQuestion = questions,
                PrimaryID = "BDABCA0B-8B80-4CC6-8836-6D5A751B6C54",//"E037E31E-9DA2-47F0-A2AD-7FFF385EB1B3",
                Secondary = "1B69B449-ECAB-41AE-89A9-E2F76329A52B",
                App_Type = "B",
                DetailedCategoryList = detailedCategory,
                LicenseCategory = "SOLICITOR"
            };

            //Act
            var submissionmaster = _submissionMasterTestRepository.InsertAssociateBblService(submissionApplication);

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            //Assert.IsFalse(submissionmaster);
        }

        [TestMethod]
        public void InsertAssociateBblBusinessNoTest()
        {
            //EntityData is Added
            var questions = new List<ScreeningQuestion>();
            ScreeningQuestion submissionQuestion = new ScreeningQuestion();
            submissionQuestion.Question = "How many Seats in your Restaurant?";
            submissionQuestion.Answer = "100";
            questions.Add(submissionQuestion);
            ScreeningQuestion submissionQuestion1 = new ScreeningQuestion();
            submissionQuestion1.Question = "Would you like a two (2) or four (4) year license?";
            submissionQuestion1.Answer = "Four (4) year";
            questions.Add(submissionQuestion1);
            ScreeningQuestion submissionQuestion2 = new ScreeningQuestion();
            submissionQuestion2.Question = "Is this business already registered with DCRA’s Corporations Division?";
            submissionQuestion2.Answer = "NO";
            questions.Add(submissionQuestion2);
            ScreeningQuestion submissionQuestion3 = new ScreeningQuestion();
            submissionQuestion3.Question =
                "Which Tax Identification Number is associated with your business: Federal Employer Identification Number (FEIN) or  Social Security Number (SSN)?";
            submissionQuestion3.Answer = "FEIN";
            questions.Add(submissionQuestion3);
            ScreeningQuestion submissionQuestion4 = new ScreeningQuestion();
            submissionQuestion4.Question =
                " Will this business be located in the District of Columbia?";
            submissionQuestion4.Answer = "NO";
            questions.Add(submissionQuestion4);
           

            var detailedCategory = new List<DetailedCategoryList>();
            DetailedCategoryList detailedCategoryList = new DetailedCategoryList();
            detailedCategoryList.CategoryId = "BDABCA0B-8B80-4CC6-8836-6D5A751B6C54";
            detailedCategory.Add(detailedCategoryList);
            var submissionApplication = new SubmissionApplication
            {
                SubQuestion = questions,
                PrimaryID = "BDABCA0B-8B80-4CC6-8836-6D5A751B6C54",//"E037E31E-9DA2-47F0-A2AD-7FFF385EB1B3",
                Secondary = "1B69B449-ECAB-41AE-89A9-E2F76329A52B",
                App_Type = "B",
                DetailedCategoryList = detailedCategory
                ,
                LicenseCategory = "SOLICITOR"
            };


            //Act
            var submissionmaster = _submissionMasterTestRepository.InsertAssociateBblService(submissionApplication);

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            //Assert.IsFalse(submissionmaster);
        }

        [TestMethod]
        public void InsertAssociateBblBusinessIndividualTest()
        {
            //EntityData is Added
            var questions = new List<ScreeningQuestion>();
            ScreeningQuestion submissionQuestion = new ScreeningQuestion();
            submissionQuestion.Question = "How many Seats in your Restaurant?";
            submissionQuestion.Answer = "100";
            questions.Add(submissionQuestion);
            ScreeningQuestion submissionQuestion1 = new ScreeningQuestion();
            submissionQuestion1.Question = "Would you like a two (2) or four (4) year license?";
            submissionQuestion1.Answer = "Four (4) year";
            questions.Add(submissionQuestion1);
            ScreeningQuestion submissionQuestion2 = new ScreeningQuestion();
            submissionQuestion2.Question = "Is this business already registered with DCRA’s Corporations Division?";
            submissionQuestion2.Answer = "NO";
            questions.Add(submissionQuestion2);
            ScreeningQuestion submissionQuestion3 = new ScreeningQuestion();
            submissionQuestion3.Question =
                "Which Tax Identification Number is associated with your business: Federal Employer Identification Number (FEIN) or  Social Security Number (SSN)?";
            submissionQuestion3.Answer = "FEIN";
            questions.Add(submissionQuestion3);
            ScreeningQuestion submissionQuestion4 = new ScreeningQuestion();
            submissionQuestion4.Question =
                " Will this business be located in the District of Columbia?";
            submissionQuestion4.Answer = "NO";
            questions.Add(submissionQuestion4);
          

            var detailedCategory = new List<DetailedCategoryList>();
            DetailedCategoryList detailedCategoryList = new DetailedCategoryList();
            detailedCategoryList.CategoryId = "BDABCA0B-8B80-4CC6-8836-6D5A751B6C54";
            detailedCategory.Add(detailedCategoryList);
            var submissionApplication = new SubmissionApplication
            {
                SubQuestion = questions,
                PrimaryID = "BDABCA0B-8B80-4CC6-8836-6D5A751B6C54",//"E037E31E-9DA2-47F0-A2AD-7FFF385EB1B3",
                Secondary = "1B69B449-ECAB-41AE-89A9-E2F76329A52B",
                App_Type = "I",
                DetailedCategoryList = detailedCategory,
                LicenseCategory = "SOLICITOR"
            };


            //Act
            var submissionmaster = _submissionMasterTestRepository.InsertAssociateBblService(submissionApplication);

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            //Assert.IsFalse(submissionmaster);
        }

        [TestMethod]
        public void InsertAssociateBblBusinessIndividualSolocitarTest()
        {
            //EntityData is Added
            var questions = new List<ScreeningQuestion>();
            ScreeningQuestion submissionQuestion = new ScreeningQuestion();
            submissionQuestion.Question = "How many Seats in your Restaurant?";
            submissionQuestion.Answer = "100";
            questions.Add(submissionQuestion);
            ScreeningQuestion submissionQuestion1 = new ScreeningQuestion();
            submissionQuestion1.Question = "Would you like a two (2) or four (4) year license?";
            submissionQuestion1.Answer = "Four (4) year";
            questions.Add(submissionQuestion1);
            ScreeningQuestion submissionQuestion2 = new ScreeningQuestion();
            submissionQuestion2.Question = "Is this business already registered with DCRA’s Corporations Division?";
            submissionQuestion2.Answer = "NO";
            questions.Add(submissionQuestion2);
            ScreeningQuestion submissionQuestion3 = new ScreeningQuestion();
            submissionQuestion3.Question =
                "Which Tax Identification Number is associated with your business: Federal Employer Identification Number (FEIN) or  Social Security Number (SSN)?";
            submissionQuestion3.Answer = "FEIN";
            questions.Add(submissionQuestion3);
            ScreeningQuestion submissionQuestion4 = new ScreeningQuestion();
            submissionQuestion4.Question =
                " Will this business be located in the District of Columbia?";
            submissionQuestion4.Answer = "NO";
            questions.Add(submissionQuestion4);
            ScreeningQuestion submissionQuestion5 = new ScreeningQuestion();
            submissionQuestion5.Question =
                "Will this Business be Home based?";
            submissionQuestion5.Answer = "YES";
            questions.Add(submissionQuestion5);
            ScreeningQuestion submissionQuestion6 = new ScreeningQuestion();
            submissionQuestion6.Question =
                "Do you have a Home Occupancy Permit (HOP) from Office of Zoning?";
            submissionQuestion6.Answer = "YES";
            questions.Add(submissionQuestion6);

            var detailedCategory = new List<DetailedCategoryList>();
            DetailedCategoryList detailedCategoryList = new DetailedCategoryList();
            detailedCategoryList.CategoryId = "6C548EBE-59ED-40B8-BEBE-EDF79149295D";
            detailedCategory.Add(detailedCategoryList);
            var submissionApplication = new SubmissionApplication
            {
                SubQuestion = questions,
                PrimaryID = "6C548EBE-59ED-40B8-BEBE-EDF79149295D",//"E037E31E-9DA2-47F0-A2AD-7FFF385EB1B3",
                Secondary = "42D8D51F-6B22-4E7B-BC7F-CE0EA2E2814B",
                App_Type = "I",
                DetailedCategoryList = detailedCategory,
                LicenseCategory = "SOLICITOR"
            };


            //Act
            var submissionmaster = _submissionMasterTestRepository.InsertAssociateBblService(submissionApplication);

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            //Assert.IsFalse(submissionmaster);
        }

        [TestMethod]
        public void InsertAssociateBblBusinessNoDataTest()
        {
            //EntityData is Added
            var questions = new List<ScreeningQuestion>();
            ScreeningQuestion submissionQuestion = new ScreeningQuestion();
            submissionQuestion.Question = "How many Seats in your Restaurant?";
            submissionQuestion.Answer = "100";
            questions.Add(submissionQuestion);
            ScreeningQuestion submissionQuestion1 = new ScreeningQuestion();
            submissionQuestion1.Question = "Would you like a two (2) or four (4) year license?";
            submissionQuestion1.Answer = "Four (4) year";
            questions.Add(submissionQuestion1);
            ScreeningQuestion submissionQuestion2 = new ScreeningQuestion();
            submissionQuestion2.Question = "Is this business already registered with DCRA’s Corporations Division?";
            submissionQuestion2.Answer = "NO";
            questions.Add(submissionQuestion2);
            ScreeningQuestion submissionQuestion3 = new ScreeningQuestion();
            submissionQuestion3.Question =
                "Which Tax Identification Number is associated with your business: Federal Employer Identification Number (FEIN) or  Social Security Number (SSN)?";
            submissionQuestion3.Answer = "FEIN";
            questions.Add(submissionQuestion3);
            ScreeningQuestion submissionQuestion4 = new ScreeningQuestion();
            submissionQuestion4.Question =
                " Will this business be located in the District of Columbia?";
            submissionQuestion4.Answer = "NO";
            questions.Add(submissionQuestion4);
            ScreeningQuestion submissionQuestion5 = new ScreeningQuestion();
            submissionQuestion5.Question =
                "Will this Business be Home based?";
            submissionQuestion5.Answer = "YES";
            questions.Add(submissionQuestion5);
            ScreeningQuestion submissionQuestion6 = new ScreeningQuestion();
            submissionQuestion6.Question =
                "Do you have a Home Occupancy Permit (HOP) from Office of Zoning?";
            submissionQuestion6.Answer = "YES";
            questions.Add(submissionQuestion6);

            var detailedCategory = new List<DetailedCategoryList>();
            DetailedCategoryList detailedCategoryList = new DetailedCategoryList();
            detailedCategoryList.CategoryId = "6C548EBE-59ED-40B8-BEBE-EDF79149295D";
            detailedCategory.Add(detailedCategoryList);
            var submissionApplication = new SubmissionApplication
            {
                SubQuestion = questions,
                PrimaryID = "6C548EBE-59ED-40B8-BEBE-EDF79149295D",//"E037E31E-9DA2-47F0-A2AD-7FFF385EB1B3",
                Secondary = "42D8D51F-6B22-4E7B-BC7F-CE0EA2E2814B",
                App_Type = "I",
                DetailedCategoryList = detailedCategory,
                LicenseCategory = "SOLICITO"
            };


            //Act
            var submissionmaster = _submissionMasterTestRepository.InsertAssociateBblService(submissionApplication);

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            //Assert.IsFalse(submissionmaster);
        }

        [TestMethod]
        public void UpdateBusinessOwner()
        {
            //Act
            var submissionmaster = _submissionMasterTestRepository.UpdateBusinessOwner("33b635b9-fec2-4b4f-9d07-686b8d6cc60c","Test");

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster);
        }
        [TestMethod]
        public void UpdateBusinessName()
        {
            //Act
            var submissionmaster = _submissionMasterTestRepository.UpdateBusinessName("33b635b9-fec2-4b4f-9d07-686b8d6cc60c","Test");

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster);
        }
        [TestMethod]
        public void UpdatePremisesAddress()
        {
            //Act
            var submissionmaster = _submissionMasterTestRepository.UpdatePremisesAddress("33b635b9-fec2-4b4f-9d07-686b8d6cc60c","Test");

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster);
        }
        
        [TestMethod]
        public void GetAddressDetails_Test()
        {
            //EntityData is Added
            var taxRevenueData = new TaxRevenueData { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97", EntityId = "1", SubmissionLicense = "LREN11002680" };
            //Act
            var submissionmaster = _submissionMasterTestRepository.GetAddressDetails(taxRevenueData);

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster.BusinessType == "BUSINESS LICENSE");
        }
        [TestMethod]
        public void UpdateSubmissionOrder_Test()
        {
            
            //Act
            var submissionmaster = _submissionMasterTestRepository.UpdateSubmissionOrder("8c6deecc-3b72-485d-8af5-30939af94e97","","");

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster);
        }
        [TestMethod]
        public void UpdateLicenseFromAccela_Test()
        {

            //Act
            var submissionmaster = _submissionMasterTestRepository.UpdateLicenseFromAccela("8c6deecc-3b72-485d-8af5-30939af94e97", "400316947111", "UnderReview", Convert.ToDateTime("2017-12-05 07:39:42.147"),"1111111");

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster);
        }
        [TestMethod]
        public void UpdateUserSubmissionExpiryDate_Test()
        {

            //Act
            var submissionmaster = _submissionMasterTestRepository.UpdateUserSubmissionExpiryDate("8c6deecc-3b72-485d-8af5-30939af94e97");

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster);
        }
        [TestMethod]
        public void FindByUser_Test()
        {
           
            //Act
            var submissionmaster = _submissionMasterTestRepository.FindByUser().ToList();

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster.Count()==5);
        }
        [TestMethod]
        public void GetUserBblService_Test()
        {

            //Act
            var submissionmaster = _submissionMasterTestRepository.GetUserBblService().ToList();

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster.Count() == 6);
        }
        [TestMethod]
        public void GetApplicationReviewCounts_Test()
        {

            //Act
            var submissionmaster = _submissionMasterTestRepository.GetApplicationReviewCounts();

            //Assert
            Assert.IsNotNull(submissionmaster);
           Assert.IsTrue(submissionmaster.UnderReviewlistCount==1);
           Assert.IsTrue(submissionmaster.DraftlistCount == 4);
        }

        [TestMethod]
        public void LappLicenseGenerationTest()
        {

            //Act
            var submissionmaster = _submissionMasterTestRepository.LappLicenseGeneration("Test");

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster == "Test16900002");
            //Assert.IsTrue(submissionmaster.DraftlistCount == 3);
        }
         [TestMethod]
        public void CheckLappLicenseFalse()
        {

            //Act
            var submissionmaster = _submissionMasterTestRepository.CheckLappLicense("Test16900001");

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsFalse(submissionmaster);
            //Assert.IsTrue(submissionmaster.DraftlistCount == 3);
        }
         [TestMethod]
         public void CheckLappLicenseTrue()
         {

             //Act
             var submissionmaster = _submissionMasterTestRepository.CheckLappLicense("931315000136");

             //Assert
             Assert.IsNotNull(submissionmaster);
             Assert.IsTrue(submissionmaster);
             //Assert.IsTrue(submissionmaster.DraftlistCount == 3);
         }

         [TestMethod]
         public void UpdateSubmissionMasterNodoc()
         {
             //EntityData is Added
             var paymentDetailsModel = new PaymentDetailsModel { MasterId = "4cb3a7f0-8e98-4b8d-86ae-a2d9dbe14959", PaymentType = "Submission" };
             //Act
             var submissionmaster = _submissionMasterTestRepository.UpdateSubmissionMaster(paymentDetailsModel);

             //Assert
             Assert.IsNotNull(submissionmaster); // Test if null
             Assert.IsTrue(submissionmaster);
         }
         [TestMethod]
         public void GetRenewalSubmissionData_Test()
         {
             //EntityData is Added
             var renewalmodel = new RenewModel { UserBblAssociateId = "2", MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97", EntityId = "1" };
             //Act
             var submissionmaster = _submissionMasterTestRepository.GetRenewalSubmissionData(renewalmodel);

             //Assert
             Assert.IsNotNull(submissionmaster); // Test if null
             Assert.IsTrue(submissionmaster.UserBblAssociateId == "2");
         }
         [TestMethod]
         public void UpdateRenewData_Test()
         {
             //EntityData is Added
             var renewalmodel = new RenewModel { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97", EntityId = "1", UserId = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F", SubmissionLicense = "LREN11002680" };
             //Act
             var submissionmaster = _submissionMasterTestRepository.InsertRenewData(renewalmodel);

             //Assert
             Assert.IsNotNull(submissionmaster); // Test if null
             //  Assert.IsTrue(submissionmaster.MasterId == "8c6deecc-3b72-485d-8af5-30939af94e97");
         }
         [TestMethod]
         public void InsertRenewData_Test()
         {
             //EntityData is Added
             var renewalmodel = new RenewModel { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97", EntityId = "1", UserId = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F", SubmissionLicense = "LREN11002684" };
             //Act
             var submissionmaster = _submissionMasterTestRepository.InsertRenewData(renewalmodel);

             //Assert
             Assert.IsNotNull(submissionmaster); // Test if null
             //  Assert.IsTrue(submissionmaster.MasterId == "8c6deecc-3b72-485d-8af5-30939af94e97");
         }
         [TestMethod]
         public void BBlServiceList_userId_Test()
         {
             //EntityData is Added
             var bblAsscoiateService = new BblAsscoiateService { UserID = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57D" };

             string MultipleLicenseName = string.Empty;
             //Act
             var submissionmaster = _submissionMasterTestRepository.BBlServiceList(bblAsscoiateService).ToList();


             foreach (var item in submissionmaster[0].BblServiceList)
             {
                 MultipleLicenseName = item.MultipleLicense;
             }

             //Assert
             Assert.IsNotNull(submissionmaster); // Test if null
             Assert.IsTrue(submissionmaster.FirstOrDefault().BBLServiceCount.FirstOrDefault().Active == 0);
         }

         [TestMethod]
         public void GetAddressDetails_NoBusinessName_Test()
         {
             //EntityData is Added
             var taxRevenueData = new TaxRevenueData { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97", EntityId = "1", SubmissionLicense = "LREN11002681" };
             //Act
             var submissionmaster = _submissionMasterTestRepository.GetAddressDetails(taxRevenueData);

             //Assert
             Assert.IsNotNull(submissionmaster);
             Assert.IsTrue(submissionmaster.BusinessType == "BUSINESS LICENSE");
         }
         [TestMethod]
         public void GetCleanHands_Fein_Ssn_Test()
         {
             //EntityData is Added
           //  var taxRevenueData = new TaxRevenueData { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97", EntityId = "1", SubmissionLicense = "LREN11002681" };
             //Act
             var clearhands = _submissionMasterTestRepository.GetCleanHands_Fein_Ssn("1", "2AC53E53-87D0-468F-9628-4AEBA1120613");

             //Assert
             Assert.IsNotNull(clearhands);
             Assert.IsFalse(clearhands);
         }
         [TestMethod]
         public void GetLicenseNumberFromSubmission_Test()
         {
             //EntityData is Added
             //  var taxRevenueData = new TaxRevenueData { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97", EntityId = "1", SubmissionLicense = "LREN11002681" };
             //Act
             var licensenumbersubmission = _submissionMasterTestRepository.GetLicenseNumberFromSubmission("8c6deecc-3b72-485d-8af5-30939af94e9");

             //Assert
             Assert.IsNotNull(licensenumbersubmission);
             Assert.IsTrue(licensenumbersubmission==string.Empty);
         }
         [TestMethod]
         public void UpdateSubmissionStatusWithRenewal_Test()
         {
             //EntityData is Added
             //  var taxRevenueData = new TaxRevenueData { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97", EntityId = "1", SubmissionLicense = "LREN11002681" };
             //Act
            _submissionMasterTestRepository.UpdateSubmissionStatusWithRenewal("4cb3a7f0-8e98-4b8d-86ae-a2d9dbe14959");

             ////Assert
             //Assert.IsNotNull(licensenumbersubmission);
             //Assert.IsTrue(licensenumbersubmission==string.Empty);
         }
        [TestMethod]
         public void UpdateRenewNoDocStatus_Test()
         {
             //EntityData is Added
             //  var taxRevenueData = new TaxRevenueData { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97", EntityId = "1", SubmissionLicense = "LREN11002681" };
             //Act
             _submissionMasterTestRepository.UpdateRenewNoDocStatus("8c6deecc-3b72-485d-8af5-30939af94e97");

             ////Assert
             //Assert.IsNotNull(licensenumbersubmission);
             //Assert.IsTrue(licensenumbersubmission==string.Empty);
         }
        [TestMethod]
        public void MasterStatus_Test()
        {
            //Act
            var submissionmaster = _submissionMasterTestRepository.MasterStatus("4cb3a7f0-8e98-4b8d-86ae-a2d9dbe14959");

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster.AppType == "B");
        }
        [TestMethod]
        public void InsertAssociateBblService_Test()
        {
            //EntityData is Added
            var questions = new List<ScreeningQuestion>();
            ScreeningQuestion submissionQuestion = new ScreeningQuestion();
            submissionQuestion.Question = "How many Seats in your Restaurant?";
            submissionQuestion.Answer = "100";
            questions.Add(submissionQuestion);
            ScreeningQuestion submissionQuestion1 = new ScreeningQuestion();
            submissionQuestion1.Question = "Would you like a two (2) or four (4) year license?";
            submissionQuestion1.Answer = "Four (4) year";
            questions.Add(submissionQuestion1);
            ScreeningQuestion submissionQuestion2 = new ScreeningQuestion();
            submissionQuestion2.Question = "Is this business already registered with DCRA’s Corporations Division?";
            submissionQuestion2.Answer = "YES";
            questions.Add(submissionQuestion2);
            ScreeningQuestion submissionQuestion3 = new ScreeningQuestion();
            submissionQuestion3.Question =
                "Which Tax Identification Number is associated with your business: Federal Employer Identification Number (FEIN) or  Social Security Number (SSN)?";
            submissionQuestion3.Answer = "FEIN";
            questions.Add(submissionQuestion3);
            ScreeningQuestion submissionQuestion4 = new ScreeningQuestion();
            submissionQuestion4.Question =
                " Will this business be located in the District of Columbia?";
            submissionQuestion4.Answer = "YES";
            questions.Add(submissionQuestion4);
              ScreeningQuestion submissionQuestion5 = new ScreeningQuestion();
            submissionQuestion5.Question =
                "PLEASE ENTER THE FULL NAME OF THE BUSINESS OWNER (IF APPLYING FOR A BUSINESS LICENSE) OR FULL EMPLOYEE NAME( IF APPLYING FOR AN INDIVIDUAL LICENSE) AS IT WILL APPEAR ON THE BUSINESS LICENSE.  CANNOT BE A COMPANY OR TRADE NAME, MUST BE AN INDIVIDUAL.";
            submissionQuestion5.Answer = "";
            questions.Add(submissionQuestion5);
            

            var detailedCategory = new List<DetailedCategoryList>();
            DetailedCategoryList detailedCategoryList = new DetailedCategoryList();
            detailedCategoryList.CategoryId = "BDABCA0B-8B80-4CC6-8836-6D5A751B6C54";
            detailedCategory.Add(detailedCategoryList);
            var submissionApplication = new SubmissionApplication
            {
                SubQuestion = questions,
                PrimaryID = "BDABCA0B-8B80-4CC6-8836-6D5A751B6C54",//"E037E31E-9DA2-47F0-A2AD-7FFF385EB1B3",
                Secondary = "891F943E-D4D9-4660-872C-26366BB1C197",
                App_Type = "B",
                DetailedCategoryList = detailedCategory,
                LicenseCategory = "ONE FAMILY RENTAL",
                IseHOP=false,
                IsHomeBased=false,
                IsCofo = false
            };

            //Act
            var submissionmaster = _submissionMasterTestRepository.InsertAssociateBblService(submissionApplication);

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            //Assert.IsFalse(submissionmaster);
        }
    }
}