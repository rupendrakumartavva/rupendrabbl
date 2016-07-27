using BusinessCenter.Common;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Service.Implementation;
using BusinessCenter.Tests.Setup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Service
{
     [TestClass]
   public class SubmissionIndividualServiceTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private SubmissionIndividualRepository _submissionIndividualTestRepository;
        private SubmissionMasterRepository _submissionMasterTestRepository;
        private SubmissionCategoryRepository _submissionCategoryRepository;
        private BblRepository _bblRepository;
        private UserBBLServiceRepository _userBblServiceRepository;
        private SubmissionQuestionRepository _submissionQuestionRepository;
        private MasterCategoryPhysicalLocationRepository _masterCategoryPhysicalLocationRepository;
        private MasterPrimaryCategoryRepository _masterPrimaryCategoryRepository;
        private SubmissionMasterApplicationChcekListRepository _submissionMasterApplicationChcekListRepository;
        private UserRepository _userRepository;
        private DCBC_ENTITY_BBL_RenewalsRepository _dcbcEntityBblRenewalsRepository;
        private MasterBusinessActivityRepository _masterBusinessActivityRepository;
        private SubmissionBblAssociationToUsersRepository _submissionBblAssociationToUsersRepository;
        private SubmissionToAccelaRepository _submissionToAccelaRepository;
        private MasterSecondaryLicenseCategoryRepository _masterSecondaryLicenseCategoryRepository;
        private Lookup_ExistingCategoriesRepository _lookupExistingCategoriesRepository;
        private MasterBblApplicationStatusRepository _masterBblApplicationStatusTestRepository;
     //   private MasterLicenseFEINRenewal masterLicenseFeinRenewal;
        private SubmissionIndividualData _mockData;
        private SubmissionLicenseNumberCounterRepository _submissionlicencecounter;
        private SubmissionIndividualService _submissionIndividualTestService;
         private OSubCategoryFeesRepository _oSubCategoryFeesRepository;
         private FixFeeRepository _fixFeeRepository;
         private MasterSubCategoryRepository _masterSubCategoryRepository;
         private FeeCodeMapRepository _feeCodeMapRepository;
         private SubmissionMasterRenewalRepository _submissionMasterRenewalRepository;
         private DCBC_ENTITY_BBL_Renewal_InvoiceRepository _dcbcEntityBblRenewalInvoiceRepository;
         private MasterCategoryQuestionRepository _masterCategoryQuestionRepository;
         private MasterCategoryDocumentRepository _masterCategoryDocumentRepository;
         private UserRoleRepository _userRoleRepository;
         private MasterRenewalStatusFeeRepository _masterRenewalStatusFeeRepository;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _bblRepository=new BblRepository(_testUnitOfWork);
            _userBblServiceRepository=new UserBBLServiceRepository(_testUnitOfWork);
            _masterCategoryQuestionRepository=new MasterCategoryQuestionRepository(_testUnitOfWork);
            _masterCategoryDocumentRepository=new MasterCategoryDocumentRepository(_testUnitOfWork);
            _userRoleRepository=new UserRoleRepository(_testUnitOfWork);
            _masterRenewalStatusFeeRepository=new MasterRenewalStatusFeeRepository(_testUnitOfWork);
            _submissionMasterRenewalRepository = new SubmissionMasterRenewalRepository(_testUnitOfWork, _masterRenewalStatusFeeRepository);
            _submissionlicencecounter = new SubmissionLicenseNumberCounterRepository(_testUnitOfWork);
            _submissionQuestionRepository=new SubmissionQuestionRepository(_testUnitOfWork);
            _dcbcEntityBblRenewalsRepository=new DCBC_ENTITY_BBL_RenewalsRepository(_testUnitOfWork);
            _submissionBblAssociationToUsersRepository=new SubmissionBblAssociationToUsersRepository(_testUnitOfWork);
            _submissionToAccelaRepository=new SubmissionToAccelaRepository(_testUnitOfWork);
            _masterSecondaryLicenseCategoryRepository=new MasterSecondaryLicenseCategoryRepository(_testUnitOfWork);
            _lookupExistingCategoriesRepository=new Lookup_ExistingCategoriesRepository(_testUnitOfWork);
            _dcbcEntityBblRenewalInvoiceRepository = new DCBC_ENTITY_BBL_Renewal_InvoiceRepository(_testUnitOfWork, _lookupExistingCategoriesRepository);
            _masterBblApplicationStatusTestRepository=new MasterBblApplicationStatusRepository(_testUnitOfWork);
            _fixFeeRepository=new FixFeeRepository(_testUnitOfWork);
            _masterSubCategoryRepository=new MasterSubCategoryRepository(_testUnitOfWork,_masterPrimaryCategoryRepository,_masterSecondaryLicenseCategoryRepository);
            _submissionMasterApplicationChcekListRepository = new SubmissionMasterApplicationChcekListRepository(_testUnitOfWork);
            _userRepository=new UserRepository(_testUnitOfWork,_userRoleRepository);
            _oSubCategoryFeesRepository = new OSubCategoryFeesRepository(_testUnitOfWork, _masterPrimaryCategoryRepository, _masterSecondaryLicenseCategoryRepository);
            _feeCodeMapRepository = new FeeCodeMapRepository(_testUnitOfWork, _oSubCategoryFeesRepository);
            _masterPrimaryCategoryRepository=new MasterPrimaryCategoryRepository(_testUnitOfWork,_masterSecondaryLicenseCategoryRepository,
                _masterCategoryDocumentRepository,_masterCategoryQuestionRepository);
            _masterBusinessActivityRepository = new MasterBusinessActivityRepository(_testUnitOfWork, _masterPrimaryCategoryRepository);
            _masterCategoryPhysicalLocationRepository=new MasterCategoryPhysicalLocationRepository(_testUnitOfWork,_masterPrimaryCategoryRepository,
                _masterCategoryQuestionRepository,_oSubCategoryFeesRepository,_masterSecondaryLicenseCategoryRepository);
            _submissionMasterTestRepository = new SubmissionMasterRepository(_testUnitOfWork, _submissionCategoryRepository, _bblRepository,
          _userBblServiceRepository, _submissionQuestionRepository, _masterCategoryPhysicalLocationRepository, _masterPrimaryCategoryRepository,
          _submissionMasterApplicationChcekListRepository, _userRepository, _dcbcEntityBblRenewalsRepository, _masterBusinessActivityRepository,
          _submissionBblAssociationToUsersRepository, _submissionToAccelaRepository, _masterSecondaryLicenseCategoryRepository,
          _lookupExistingCategoriesRepository, _submissionlicencecounter, _masterBblApplicationStatusTestRepository);
            _submissionIndividualTestRepository = new SubmissionIndividualRepository(_testUnitOfWork, _submissionMasterTestRepository);
            _submissionCategoryRepository = new SubmissionCategoryRepository(_testUnitOfWork, _masterPrimaryCategoryRepository,
                _masterSecondaryLicenseCategoryRepository, _oSubCategoryFeesRepository, _fixFeeRepository, _submissionQuestionRepository, _masterSubCategoryRepository,
                _masterCategoryPhysicalLocationRepository, _feeCodeMapRepository, _submissionMasterApplicationChcekListRepository, _submissionMasterRenewalRepository,
                _dcbcEntityBblRenewalInvoiceRepository,_lookupExistingCategoriesRepository);
         
            //Mocking Repository
            _mockData = new SubmissionIndividualData();

            _submissionIndividualTestService = new SubmissionIndividualService(_submissionIndividualTestRepository);

            //Setup Data
            var testData = new SubmissionIndividualData();
            _testDbContext.SubmissionIndividual.AddRange(testData.SubmissionIndividualDataEntitiesList);
            _testDbContext.SaveChanges();

        }
        [TestMethod]
        public void GetSubmissionIndividualDataTest()
        {
            var checklistModelEntity = new ChecklistModel { MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c" };
            //Act 
            var submissionindividualRows = _submissionIndividualTestService.GetSubmissionIndividualData(checklistModelEntity);

            //Assert
            Assert.IsNotNull(submissionindividualRows); // Test if null
            Assert.IsTrue(submissionindividualRows.Count() == 1);
        }
        [TestMethod]
        public void SubmissionIndividualDelete_Returns_True_Test()
        {

            //Act 
            var submissionindividualRows = _submissionIndividualTestService.SubmissionIndividualDelete("33b635b9-fec2-4b4f-9d07-686b8d6cc60c");

            //Assert
            Assert.IsNotNull(submissionindividualRows); // Test if null
            Assert.IsTrue(submissionindividualRows == true);
        }
        [TestMethod]
        public void SubmissionIndividualDelete_Returns_False_Test()
        {

            //Act 
            var submissionindividualRows = _submissionIndividualTestService.SubmissionIndividualDelete("11b635b9-aac2-4b4f-9d07-686b8d6cc60c");

            //Assert
            Assert.IsNotNull(submissionindividualRows); // Test if null
            Assert.IsTrue(submissionindividualRows == false);
        }
        [TestMethod]
        public void ValidateSubmission_Returns_True_Test()
        {

            //Act 
            var submissionindividualRows = _submissionIndividualTestService.ValidateSubmission("33b635b9-fec2-4b4f-9d07-686b8d6cc60c");

            //Assert
            Assert.IsNotNull(submissionindividualRows); // Test if null
            Assert.IsTrue(submissionindividualRows == true);
        }
        [TestMethod]
        public void ValidateSubmission_Returns_False_Test()
        {

            //Act 
            var submissionindividualRows = _submissionIndividualTestService.ValidateSubmission("11b635b9-aac2-4b4f-9d07-686b8d6cc60c");

            //Assert
            Assert.IsNotNull(submissionindividualRows); // Test if null
            Assert.IsTrue(submissionindividualRows == false);
        }
        [TestMethod]
        public void InsertUpdateSubmissionIndividual_WithNewDetails_Test()
        {
            var submissionIndividualEntity = new SubmissionIndividualEntity
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
            var submissionindividualRows = _submissionIndividualTestService.InsertUpdateSubmissionIndividual(submissionIndividualEntity);

            //Assert
            Assert.IsNotNull(submissionindividualRows); // Test if null
            Assert.IsTrue(submissionindividualRows == 3);
        }
        [TestMethod]
        public void InsertUpdateSubmissionIndividual_Update_Test()
        {
            var submissionIndividualEntity = new SubmissionIndividualEntity
            {
                SubmindividualId = 1,
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
                CompanyName = "IMA PIZZA STORE 13 LLC.",
                CompanyBusinessLicense = "931315000135",
                FirstName = "DCBCFIRST",
                MiddleName = "DCBCMIDDLE",
                LastName = "DCBCLAST",
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
            var submissionindividualRows = _submissionIndividualTestService.InsertUpdateSubmissionIndividual(submissionIndividualEntity);

            //Assert
            Assert.IsNotNull(submissionindividualRows); // Test if null
            Assert.IsTrue(submissionindividualRows == 1);
        }
    }

}
