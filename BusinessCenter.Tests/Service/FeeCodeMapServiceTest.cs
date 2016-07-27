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
   public class FeeCodeMapServiceTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private FeeCodeMapRepository _feeCodeMapTestRepository;
        private Mock<IFeeCodeMapRepository> _mockfeeCodeMapRepository;

        private MasterPrimaryCategoryRepository _masterPrimaryCategoryRepository;
        private MasterSecondaryLicenseCategoryRepository _mockMasterSecondaryLicenseCategoryRepository;
        private MasterCategoryDocumentRepository _mockMasterCategoryDocumentRepository;
        private MasterCategoryQuestionRepository _mockMasterCategoryQuestionRepository;
        private OSubCategoryFeesRepository _osubcategoryFeesTestRepository;

        
        private FeeCodeMapRepositoryData _mockData;

        private FeeCodeMapService _feeCodeMapTestService;
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


            _feeCodeMapTestRepository = new FeeCodeMapRepository(_testUnitOfWork, _osubcategoryFeesTestRepository);

            _feeCodeMapTestService = new FeeCodeMapService(_feeCodeMapTestRepository);

            //Mocking Repository
            _mockfeeCodeMapRepository = new Mock<IFeeCodeMapRepository>();
            _mockData = new FeeCodeMapRepositoryData();

            //Setup Data
            var testData = new FeeCodeMapRepositoryData();
            _testDbContext.FeeCodeMap.AddRange(testData.FeeCodeMapEntitiesList);
            _testDbContext.SaveChanges();


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
        public void CheckunitsTrueTest()
        {

            //Act 
            var FeecodeMapRows = _feeCodeMapTestService.Checkunits("ROOMS");

            //Assert
            Assert.IsNotNull(FeecodeMapRows); // Test if null
            Assert.IsTrue(FeecodeMapRows == true);
        }
        [TestMethod]
        public void CheckunitsFalseTest()
        {

            //Act 
            var feecodeMapRows = _feeCodeMapTestService.Checkunits("Room");

            //Assert
            Assert.IsNotNull(feecodeMapRows); // Test if null
            Assert.IsTrue(feecodeMapRows == false);
        }
       [TestMethod]
        public void InsertFeeCodeMapTest()
        {
            //
            var primaryPhysicallocationEnitity = new PrimaryPhysicallocation
            {
                Fee_Code = "C",
                UnitOne = "Room",
                UnitTwo = "Kitchen"
            };

            //Act 
            var feecodeMapRows = _feeCodeMapTestService.InsertUpdateFee(primaryPhysicallocationEnitity);

            //Assert
            Assert.IsNotNull(feecodeMapRows); // Test if null
            Assert.IsTrue(feecodeMapRows == true);

        }
        [TestMethod]
        public void UpdateFeeCodeMapTest()
        {
            //
            var primaryPhysicallocationEnitity = new PrimaryPhysicallocation
            {
                OldUnitOne = "Rooms",
                UnitOne = "Room"
            };

            //Act 
            var feecodeMapRows = _feeCodeMapTestService.UpdateFeecode(primaryPhysicallocationEnitity);

            //Assert
            Assert.IsNotNull(feecodeMapRows); // Test if null
            Assert.IsTrue(feecodeMapRows == true);

        }
    }

}
