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
    public class FeeCodeMapRepositoryTest
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

        //private Mock<IOSubCategoryFeesRepository> _mockOSubCategoryFeesMapRepository;
        private FeeCodeMapRepositoryData _mockData;


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
        public void FindByIDTest()
        {

            //Act 
            var FeecodeMapRows = _feeCodeMapTestRepository.FindByID("Rooms");

            //Assert
            Assert.IsNotNull(FeecodeMapRows); // Test if null
            Assert.IsTrue(FeecodeMapRows.Count() == 2);
        }
        [TestMethod]
        public void FindBycategoryIDTest()
        {

            //Act 
            var FeecodeMapRows = _feeCodeMapTestRepository.FindBycategoryID("Rooms", "Restaurant", 2);

            //Assert
            Assert.IsNotNull(FeecodeMapRows); // Test if null
            Assert.IsTrue(FeecodeMapRows.Count() == 1);
        }
        [TestMethod]
        public void FindBycategoryNameTest()
        {

            //Act 
            var FeecodeMapRows = _feeCodeMapTestRepository.FindBycategoryName("Rooms", "Restaurant");

            //Assert
            Assert.IsNotNull(FeecodeMapRows); // Test if null
            Assert.IsTrue(FeecodeMapRows.Count() == 4);
        }
      

        [TestMethod]
        public void CheckunitsTrueTest()
        {

            //Act 
            var FeecodeMapRows = _feeCodeMapTestRepository.Checkunits("ROOMS");

            //Assert
            Assert.IsNotNull(FeecodeMapRows); // Test if null
            Assert.IsTrue(FeecodeMapRows ==true);
        }
        [TestMethod]
        public void CheckunitsFalseTest()
        {

            //Act 
            var feecodeMapRows = _feeCodeMapTestRepository.Checkunits("Room");

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
            var feecodeMapRows = _feeCodeMapTestRepository.InsertUpdateFee(primaryPhysicallocationEnitity);

            //Assert
            Assert.IsNotNull(feecodeMapRows); // Test if null
            Assert.IsTrue(feecodeMapRows == true);

        }
        [TestMethod]
        public void InsertFeeCodeMapUnitOneTest()
        {
            //
            var primaryPhysicallocationEnitity = new PrimaryPhysicallocation
            {
                Fee_Code = "C",
                UnitOne = "Room"
            };

            //Act 
            var feecodeMapRows = _feeCodeMapTestRepository.InsertUpdateFee(primaryPhysicallocationEnitity);

            //Assert
            Assert.IsNotNull(feecodeMapRows); // Test if null
            Assert.IsTrue(feecodeMapRows == true);

        }
        [TestMethod]
        public void InsertFeeCodeMapUnitTwoTest()
        {
            //
            var primaryPhysicallocationEnitity = new PrimaryPhysicallocation
            {
                Fee_Code = "C",
                UnitTwo = "Room"
            };

            //Act 
            var feecodeMapRows = _feeCodeMapTestRepository.InsertUpdateFee(primaryPhysicallocationEnitity);

            //Assert
            Assert.IsNotNull(feecodeMapRows); // Test if null
            Assert.IsTrue(feecodeMapRows == true);

        }
        [TestMethod]
        public void UpdateFeeCodeMapUnitOneTrueTest()
        {
            //
            var primaryPhysicallocationEnitity = new PrimaryPhysicallocation
            {
                OldUnitOne = "Rooms",
                UnitOne = "Room"
            };

            //Act 
            var feecodeMapRows = _feeCodeMapTestRepository.UpdateFeecode(primaryPhysicallocationEnitity);

            //Assert
            Assert.IsNotNull(feecodeMapRows); // Test if null
            Assert.IsTrue(feecodeMapRows == true);

        }
        [TestMethod]
        public void UpdateFeeCodeMapUnitOneFalseTest()
        {
            //
            var primaryPhysicallocationEnitity = new PrimaryPhysicallocation
            {
                OldUnitOne = "Rooms",
                UnitTwo = "Room"
            };

            //Act 
            var feecodeMapRows = _feeCodeMapTestRepository.UpdateFeecode(primaryPhysicallocationEnitity);

            //Assert
            Assert.IsNotNull(feecodeMapRows); // Test if null
            Assert.IsTrue(feecodeMapRows == false);

        }
        [TestMethod]
        public void InsertFeeCodeMapTATest()
        {
            //
            var primaryPhysicallocationEnitity = new PrimaryPhysicallocation
            {
                Fee_Code = "TA",
                UnitOne = "Room"
            
            };

            //Act 
            var feecodeMapRows = _feeCodeMapTestRepository.InsertUpdateFee(primaryPhysicallocationEnitity);

            //Assert
            Assert.IsNotNull(feecodeMapRows); // Test if null
            Assert.IsTrue(feecodeMapRows == true);

        }
    }

    #region junk
   //  [TestClass]
   //public  class FeeCodeMapRepositoryTest
   //  {
   //      private readonly List<FeeCodeMap> list = new List<FeeCodeMap>()
   //      {
   //          new FeeCodeMap()
   //          {
   //               FeeCodeId =1,
   //     FeeCode = "T",
   //     Quantity = "Rooms",
   //     IsTier = true,
   //     Status = true
   //          },
   //           new FeeCodeMap()
   //          {
   //               FeeCodeId =2,
   //     FeeCode = "H",
   //     Quantity = "Kitchens",
   //     IsTier = false,
   //     Status = true
   //          }
   //      };

   //      Mock<IFeeCodeMapRepository> mockRepo=new Mock<IFeeCodeMapRepository>();
   //      List<OSub_Category_Fees> oSub_Category_FeesList = new List<OSub_Category_Fees>() {
   //         new OSub_Category_Fees() {
   //             OSub_Category = "1",
   //             OSub_Description = "Employer-Paid Personnel Service",
   //             Fee_Code = "S",
   //             Start = 0,
   //             End = 99999,
   //             License_Fee = 1300,
   //             Tier = null,
   //             App_Type = "B",
   //             Status = true
   //          },
   //           new OSub_Category_Fees() {
   //             OSub_Category = "10",
   //             OSub_Description = "Public Hall",
   //             Fee_Code = "S",
   //             Start = 0,
   //             End = 99999,
   //             License_Fee = 1300,
   //             Tier = null,
   //             App_Type = "B",
   //             Status = true
   //          },
   //         };

   //      //    public IEnumerable<OSub_Category_Fees> FindBycategoryID(string quantity,string description,int item)
   //      [TestMethod()]
   //      public void FindByIDTest()
   //      {
   //          string quantity = "Rooms";
   //          bool tier = true;
   //          mockRepo.Setup(m => m.FindByID(quantity,tier)).Returns(list);
   //          var runtimeOutput = mockRepo.Object.FindByID(quantity,tier);
   //          Assert.IsTrue(runtimeOutput != null);
   //          Assert.IsTrue(runtimeOutput.FirstOrDefault().Quantity == "Rooms");
   //          Assert.IsTrue(runtimeOutput.Last().Quantity == "Kitchens");
   //      }

   //      [TestMethod()]
   //      public void FindBycategoryIDTest()
   //      {
   //          string quantity = "Rooms";
   //          string description = "T";
   //          int item = 1;
   //          mockRepo.Setup(m => m.FindBycategoryID(quantity, description, item)).Returns(oSub_Category_FeesList);
   //          var runtimeOutput = mockRepo.Object.FindBycategoryID(quantity, description, item);
   //          Assert.IsTrue(runtimeOutput != null);
   //          Assert.IsTrue(runtimeOutput.FirstOrDefault().OSub_Description == "Employer-Paid Personnel Service");
   //          Assert.IsTrue(runtimeOutput.Last().OSub_Description == "Public Hall");
   //      }

   //      //    public bool Checkunits(string quantity)
   //      [TestMethod()]
   //      public void FindBycategoryNameTest()
   //      {
   //          string quantity = "Rooms";
   //          string description = "T";
   //          mockRepo.Setup(m => m.FindBycategoryName(quantity, description)).Returns(oSub_Category_FeesList);
   //          var runtimeOutput = mockRepo.Object.FindBycategoryName(quantity, description);
   //          Assert.IsTrue(runtimeOutput != null);
   //          Assert.IsTrue(runtimeOutput.FirstOrDefault().OSub_Description == "Employer-Paid Personnel Service");
   //          Assert.IsTrue(runtimeOutput.Last().OSub_Description == "Public Hall");
   //      }

   //      [TestMethod()]
   //      public void CheckunitsTest()
   //      {
   //          string quantity = "Rooms";
   //          mockRepo.Setup(m => m.Checkunits(quantity)).Returns(true);
   //          var runtimeOutput = mockRepo.Object.Checkunits(quantity);
   //          Assert.AreEqual<bool>(true, runtimeOutput);
   //      }
   // }
#endregion
}
