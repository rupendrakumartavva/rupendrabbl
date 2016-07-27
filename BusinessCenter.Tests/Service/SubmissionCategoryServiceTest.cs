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
   public class SubmissionCategoryServiceTest
    {
        //Initialization DBConnection
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        private SubmissionCategoryRepository _submissionCategoryTestRepository;
        private MasterPrimaryCategoryRepository _masterPrimaryCategoryRepository;
        private MasterSecondaryLicenseCategoryRepository _masterSecondaryLicenseCategoryRepository;
        private MasterCategoryDocumentRepository _masterCategoryDocumentRepository;
        private MasterCategoryQuestionRepository _masterCategoryQuestionRepository;
        private OSubCategoryFeesRepository _oSubCategoryFeesRepository;
        private FixFeeRepository _fixedFeeTestRepository;
        private SubmissionQuestionRepository _submissionQuestionRepository;
        private MasterSubCategoryRepository _masterSubCategoryRepository;
        private MasterCategoryPhysicalLocationRepository _masterCategoryPhysicalLocationRepository;
        private FeeCodeMapRepository _feeCodeMapRepository;
        private SubmissionMasterApplicationChcekListRepository _submissionMasterApplicationChcekListRepository;
        private SubmissionMasterRenewalRepository _submissionMasterRenewalRepository;
   private DCBC_ENTITY_BBL_Renewal_InvoiceRepository _dcbcEntityBblRenewalInvoiceRepository;
        private Lookup_ExistingCategoriesRepository _lookupExistingCategoriesRepository;
      //  private SubmissionCategoryRepositoryData _submissionCategoryRepositoryData;
       // private MasterPrimaryCategoryRepositoryData _masterPrimaryCategoryRepositoryData;
       // private MasterSecondaryLicenseCategoryRepositoryData _masterSecondaryLicenseCategoryRepositoryData;
       // private MasterCategoryDocumentRepositoryData _masterCategoryDocumentRepositoryData;
      //  private MasterCategoryQuestionData _masterCategoryQuestionData;
      //  private OSubCategoryFeesData _oSubCategoryFeesData;
       // private FixFeeRepositoryData _fixFeeRepositoryData;
     //   private SubmissionQuestionRepositoryData _submissionQuestionRepositoryData;
      //  private MasterSubCategoryRepositoryData _masterSubCategoryRepositoryData;
     //   private MasterCategoryPhysicalLocationData _masterCategoryPhysicalLocationRepositoryData;
       // private FeeCodeMapRepositoryData _feeCodeMapRepositoryData;
       // private SubmissionMasterApplicationChcekListRepositoryData _submissionMasterApplicationChcekListRepositoryData;

        private MasterRenewalStatusFeeRepository _masterRenewalStatusFeeRepository;
        private SubmissionCategoryService _submissionCategoryService;



        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);


            _masterRenewalStatusFeeRepository = new MasterRenewalStatusFeeRepository(_testUnitOfWork);
            _lookupExistingCategoriesRepository = new Lookup_ExistingCategoriesRepository(_testUnitOfWork);
            _masterSecondaryLicenseCategoryRepository = new MasterSecondaryLicenseCategoryRepository(_testUnitOfWork);
            _masterCategoryDocumentRepository = new MasterCategoryDocumentRepository(_testUnitOfWork);
            _masterCategoryQuestionRepository = new MasterCategoryQuestionRepository(_testUnitOfWork);
            _fixedFeeTestRepository = new FixFeeRepository(_testUnitOfWork);
            _submissionQuestionRepository = new SubmissionQuestionRepository(_testUnitOfWork);
            _submissionMasterApplicationChcekListRepository = new SubmissionMasterApplicationChcekListRepository(_testUnitOfWork);
            _submissionMasterRenewalRepository = new SubmissionMasterRenewalRepository(_testUnitOfWork, _masterRenewalStatusFeeRepository);

            _dcbcEntityBblRenewalInvoiceRepository = new DCBC_ENTITY_BBL_Renewal_InvoiceRepository(_testUnitOfWork,_lookupExistingCategoriesRepository);

            _masterPrimaryCategoryRepository = new MasterPrimaryCategoryRepository(_testUnitOfWork,
                             _masterSecondaryLicenseCategoryRepository,
                             _masterCategoryDocumentRepository, _masterCategoryQuestionRepository);


            _oSubCategoryFeesRepository = new OSubCategoryFeesRepository(_testUnitOfWork, _masterPrimaryCategoryRepository,
              _masterSecondaryLicenseCategoryRepository);


            _masterSubCategoryRepository = new MasterSubCategoryRepository(_testUnitOfWork,
                                                                               _masterPrimaryCategoryRepository,
                                                                               _masterSecondaryLicenseCategoryRepository);


            _masterCategoryPhysicalLocationRepository = new MasterCategoryPhysicalLocationRepository(_testUnitOfWork,
                                                            _masterPrimaryCategoryRepository,
                                                            _masterCategoryQuestionRepository
                                                            , _oSubCategoryFeesRepository
                                                            , _masterSecondaryLicenseCategoryRepository);
            _feeCodeMapRepository = new FeeCodeMapRepository(_testUnitOfWork, _oSubCategoryFeesRepository);

            _submissionCategoryTestRepository = new SubmissionCategoryRepository(_testUnitOfWork, _masterPrimaryCategoryRepository,
                _masterSecondaryLicenseCategoryRepository,
                _oSubCategoryFeesRepository, _fixedFeeTestRepository,
                _submissionQuestionRepository, _masterSubCategoryRepository, _masterCategoryPhysicalLocationRepository,
                _feeCodeMapRepository, _submissionMasterApplicationChcekListRepository, _submissionMasterRenewalRepository, _dcbcEntityBblRenewalInvoiceRepository,
               _lookupExistingCategoriesRepository);

            _submissionCategoryService = new SubmissionCategoryService(_submissionCategoryTestRepository);



            // Setup Data
            var testData = new SubmissionCategoryRepositoryData();
            _testDbContext.SubmissionCategory.AddRange(testData.SubmissionCategoryEntitiesList);
            _testDbContext.SaveChanges();

            var masterPriamryCategoryData = new MasterPrimaryCategoryRepositoryData();
            _testDbContext.MasterPrimaryCategory.AddRange(masterPriamryCategoryData.MasterPrimaryCategoryEntitiesList);
            _testDbContext.SaveChanges();

            var masterSecondaryCategoryData = new MasterSecondaryLicenseCategoryRepositoryData();
            _testDbContext.MasterSecondaryLicenseCategory.AddRange(masterSecondaryCategoryData.MasterSeconderyLicenseCategoryList);
            _testDbContext.SaveChanges();

            var masterCategoryDocumentRepositoryData = new MasterCategoryDocumentRepositoryData();
            _testDbContext.MasterCategoryDocument.AddRange(masterCategoryDocumentRepositoryData.MasterCategoryDocumentList);
            _testDbContext.SaveChanges();

            var masterCategoryQuestionData = new MasterCategoryQuestionData();
            _testDbContext.MasterCategoryQuestion.AddRange(masterCategoryQuestionData.CategoryQuestionEntitiesList);
            _testDbContext.SaveChanges();

            var oSubCategoryFeesData = new OSubCategoryFeesData();
            _testDbContext.OSub_Category_Fees.AddRange(oSubCategoryFeesData.OSubCategoryFeesEntitiesList);
            _testDbContext.SaveChanges();

            var fixFeeRepositoryData = new FixFeeRepositoryData();
            _testDbContext.FixFee.AddRange(fixFeeRepositoryData.FixFeeEntitiesList);
            _testDbContext.SaveChanges();

            var submissionQuestionRepositoryData = new SubmissionQuestionRepositoryData();
            _testDbContext.SubmissionQuestion.AddRange(submissionQuestionRepositoryData.SubmissionQuestionEntitiesList);
            _testDbContext.SaveChanges();

            var masterSubCategoryRepositoryData = new MasterSubCategoryRepositoryData();
            _testDbContext.MasterSubCategory.AddRange(masterSubCategoryRepositoryData.MasterSubCategoryList);
            _testDbContext.SaveChanges();

            var masterCategoryPhysicalLocationData = new MasterCategoryPhysicalLocationData();
            _testDbContext.MasterCategoryPhysicalLocation.AddRange(masterCategoryPhysicalLocationData.MasterCategoryPhysicalLocationList);
            _testDbContext.SaveChanges();

            var feeCodeMapRepositoryData = new FeeCodeMapRepositoryData();
            _testDbContext.FeeCodeMap.AddRange(feeCodeMapRepositoryData.FeeCodeMapEntitiesList);
            _testDbContext.SaveChanges();

            var submissionMasterApplicationChcekListRepositoryData = new SubmissionMasterApplicationChcekListRepositoryData();
            _testDbContext.SubmissionMaster_ApplicationCheckList.AddRange(submissionMasterApplicationChcekListRepositoryData.SubmissionMaster_ApplicationCheckListEntitiesList);
            _testDbContext.SaveChanges();
        }
        [TestMethod]
        public void GetAll_SubmissionCategories_Return_Test()
        {
            //Act 
            var primaryCategory = _submissionCategoryService.GetAllSubmissionCategories();

            //Assert
            Assert.IsNotNull(primaryCategory); // Test if null
            Assert.IsTrue(primaryCategory.Count() == 8);
        }
        [TestMethod]
        public void GetFindById_RecordsExist_Test()
        {
            //Initialize Entites
            var submissionCategoryModel = new SubmissionCategoryModel { SubmissionCategoryId = 1 };

            //Act 
            var submissionCategoryRows = _submissionCategoryService.FindBySubmissionCategoryId(submissionCategoryModel);

            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
            Assert.IsTrue(submissionCategoryRows.Count() == 1);
        }
        [TestMethod]
        public void GetFindById_RecordsDoesnotExist_Test()
       {
            //Initialize Entites
            var submissionCategoryModel = new SubmissionCategoryModel { SubmissionCategoryId = 8 };

            //Act 
            var submissionCategoryRows = _submissionCategoryService.FindBySubmissionCategoryId(submissionCategoryModel);

            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
            Assert.IsTrue(submissionCategoryRows.Any());
        }
        [TestMethod]
        public void GetTotalFees_ForFeeExempt_Test()
        {
            var detailedCategoryList = new List<DetailedCategoryList>()
            {
                new DetailedCategoryList()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14ED4",
                    EndorsementFee = Convert.ToDecimal(25.0000),
                    Endorsement = "General Sales"
                },
                 new DetailedCategoryList()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14EER",
                     EndorsementFee = Convert.ToDecimal(55.0000),
                    Endorsement = "Inspected Sales and Services"
                }
            };

            var screenQuestionList = new List<ScreeningQuestion>()
            {
                new ScreeningQuestion()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14ED4",
                    Question = "Would you like a two (2) or four (4) year license?",
                    Answer = "TWO (2) YEAR"
                    
                },
                  new ScreeningQuestion()
                {
                     CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14EER",
                     Question = "Will this business be located in the District of Columbia?",
                     Answer = "YES"
                }
            };

            var submissionApplication = new SubmissionApplication
            {
                // Primary Category
                PrimaryID = "BDABCA0B-8B80-4CC6-8836-6D5A751B6C54",
                MasterId = "9F92454C-ED4C-4062-B557-4125813B2BDD",
                Secondary = "1B69B449-ECAB-41AE-89A9-E2F76329A52B",
                DetailedCategoryList = detailedCategoryList,
                SubQuestion = screenQuestionList
            };

            // Act 
            var submissionCategoryRows =
                _submissionCategoryService.GetTotalFees(submissionApplication);

            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
            Assert.IsTrue(submissionCategoryRows.TotalFee == 0);
        }
        [TestMethod]
        public void GetTotalFees_Test()
        {
            var detailedCategoryList = new List<DetailedCategoryList>()
            {
                new DetailedCategoryList()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14ED4",
                    EndorsementFee = Convert.ToDecimal(25.0000),
                    Endorsement = "General Sales"
                },
                 new DetailedCategoryList()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14EER",
                     EndorsementFee = Convert.ToDecimal(55.0000),
                    Endorsement = "Inspected Sales and Services"
                }
            };

            var screenQuestionList = new List<ScreeningQuestion>()
            {
                new ScreeningQuestion()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14ED4",
                    Question = "Would you like a two (2) or four (4) year license?",
                    Answer = "TWO (2) YEAR"
                    
                },
                  new ScreeningQuestion()
                {
                     CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14EER",
                     Question = "Will this business be located in the District of Columbia?",
                     Answer = "YES"
                }
            };

            var submissionApplication = new SubmissionApplication
            {
                // Primary Category
                PrimaryID = "6C548EBE-59ED-40B8-BEBE-EDF79149295D",
                MasterId = "9F92454C-ED4C-4062-B557-4125813B2BDD",
                Secondary = "1DE53FC4-F444-44EA-99A4-5DA3F107D5DD",
                DetailedCategoryList = detailedCategoryList,
                SubQuestion = screenQuestionList
            };

            // Act 
            var submissionCategoryRows =
                _submissionCategoryService.GetTotalFees(submissionApplication);

            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
            Assert.IsTrue(submissionCategoryRows.TotalFee == 132);
            Assert.IsTrue(submissionCategoryRows.LicenseCategory == "Hotel");
        }
        [TestMethod]
        public void ServiceCheckList_Test()
        {
            ServiceChecklist servicechecklist = new ServiceChecklist()
            {
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
            };
            //Act 
            var submissionCategoryRows = _submissionCategoryService.ServiceCheckList(servicechecklist);

            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
            Assert.IsTrue(submissionCategoryRows.TotalFee == Convert.ToDecimal(72.6));
        }
        [TestMethod]
        public void CheckunitsTrueTest()
        {
            //Act 
            var submissionCategoryRows = _submissionCategoryService.Checkunits("ROOMS");
            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
            Assert.IsTrue(submissionCategoryRows);
        }
        [TestMethod]
        public void CheckunitsFalseTest()
        {
            //Act 
            var submissionCategoryRows = _submissionCategoryService.Checkunits("Room");
            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
            Assert.IsTrue(submissionCategoryRows == false);
        }
    }
}
