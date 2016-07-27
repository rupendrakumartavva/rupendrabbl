using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Model;
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
  public  class OSubCategoryFeeServiceTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        private OSubCategoryFeesRepository _osubcategoryFeesTestRepository;

        private MasterPrimaryCategoryRepository _masterPrimaryCategoryRepository;
        private MasterSecondaryLicenseCategoryRepository _mockMasterSecondaryLicenseCategoryRepository;
        private MasterCategoryDocumentRepository _mockMasterCategoryDocumentRepository;
        private MasterCategoryQuestionRepository _mockMasterCategoryQuestionRepository;

        private OSubCategoryFeeService _oSubcategoryFeesTestService;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);

            _mockMasterSecondaryLicenseCategoryRepository = new MasterSecondaryLicenseCategoryRepository(_testUnitOfWork);
            _mockMasterCategoryDocumentRepository = new MasterCategoryDocumentRepository(_testUnitOfWork);
            _mockMasterCategoryQuestionRepository = new MasterCategoryQuestionRepository(_testUnitOfWork);

            _masterPrimaryCategoryRepository = new MasterPrimaryCategoryRepository(_testUnitOfWork, _mockMasterSecondaryLicenseCategoryRepository,
                _mockMasterCategoryDocumentRepository,
                _mockMasterCategoryQuestionRepository);

            _osubcategoryFeesTestRepository = new OSubCategoryFeesRepository(_testUnitOfWork, _masterPrimaryCategoryRepository, _mockMasterSecondaryLicenseCategoryRepository);



            _oSubcategoryFeesTestService = new OSubCategoryFeeService(_osubcategoryFeesTestRepository);

            var testOsubcategoryFeeData = new OSubCategoryFeesData();
            _testDbContext.OSub_Category_Fees.AddRange(testOsubcategoryFeeData.OSubCategoryFeesEntitiesList);
            _testDbContext.SaveChanges();

            var testPrimaryCategoryData = new MasterPrimaryCategoryRepositoryData();
            _testDbContext.MasterPrimaryCategory.AddRange(testPrimaryCategoryData.MasterPrimaryCategoryEntitiesList);
            _testDbContext.SaveChanges();

            var secondaryLicenseData = new MasterSecondaryLicenseCategoryRepositoryData();
            _testDbContext.MasterSecondaryLicenseCategory.AddRange(secondaryLicenseData.MasterSeconderyLicenseCategoryList);
            _testDbContext.SaveChanges();

            var masterCategoryDocumentRepositoryData = new MasterCategoryDocumentRepositoryData();
            _testDbContext.MasterCategoryDocument.AddRange(masterCategoryDocumentRepositoryData.MasterCategoryDocumentList);
            _testDbContext.SaveChanges();


            //Setup Data
            var masterCategoryQuestionData = new MasterCategoryQuestionData();
            _testDbContext.MasterCategoryQuestion.AddRange(masterCategoryQuestionData.CategoryQuestionEntitiesList);
            _testDbContext.SaveChanges();
        }
        [TestMethod]
        public void UpdateSubFee_BasedOnCategoryFeeIdWithFindById_Return_Test()
        {

            //Initial Model Details
            var primaryPhysicallocation = new PrimaryPhysicallocation { OldCategoryName = "Restaurant", Description = "MyNew Restaurant" };

            //Act 
            var categoryFeeRows = _oSubcategoryFeesTestService.UpdateSubFee(primaryPhysicallocation);

            //Assert
            Assert.IsNotNull(categoryFeeRows); // Test if null
            Assert.IsTrue(categoryFeeRows == true);
        }
    }
}
