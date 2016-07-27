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
   public class MasterSubCategoryServiceTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;


        private MasterSubCategoryRepository _masterSubCategoryTestRepository;
        private MasterPrimaryCategoryRepository _masterPrimaryCategoryRepository;
        private MasterSecondaryLicenseCategoryRepository _masterSecondaryLicenseCategoryRepository;
        private MasterCategoryDocumentRepository _masterCategoryDocumentRepository;
        private MasterCategoryQuestionRepository _masterCategoryQuestionRepository;
        private MasterSubCategoryService _mockMasterSubCategoryTestService;



        [TestInitialize]
        public void Initialize()
        {

            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);


            _masterPrimaryCategoryRepository = new MasterPrimaryCategoryRepository(_testUnitOfWork,
             _masterSecondaryLicenseCategoryRepository,
             _masterCategoryDocumentRepository,
             _masterCategoryQuestionRepository);

            _masterSecondaryLicenseCategoryRepository = new MasterSecondaryLicenseCategoryRepository(_testUnitOfWork);

            _masterSubCategoryTestRepository = new MasterSubCategoryRepository(_testUnitOfWork,
                                                                           _masterPrimaryCategoryRepository,
                                                                           _masterSecondaryLicenseCategoryRepository);

            _mockMasterSubCategoryTestService = new MasterSubCategoryService(_masterSubCategoryTestRepository);
            _masterCategoryDocumentRepository = new MasterCategoryDocumentRepository(_testUnitOfWork);
            _masterCategoryQuestionRepository = new MasterCategoryQuestionRepository(_testUnitOfWork);

            //Mocking Repository
           // _mockData = new MasterSubCategoryRepositoryData();





            //Setup Data
            var subCategoryTestData = new MasterSubCategoryRepositoryData();
            _testDbContext.MasterSubCategory.AddRange(subCategoryTestData.MasterSubCategoryList);
            _testDbContext.SaveChanges();

            var masterPrimaryCategoryTestData = new MasterPrimaryCategoryRepositoryData();
            _testDbContext.MasterPrimaryCategory.AddRange(masterPrimaryCategoryTestData.MasterPrimaryCategoryEntitiesList);
            _testDbContext.SaveChanges();


            var masterSecondaryLicenseCategoryTestData = new MasterSecondaryLicenseCategoryRepositoryData();
            _testDbContext.MasterSecondaryLicenseCategory.AddRange(masterSecondaryLicenseCategoryTestData.MasterSeconderyLicenseCategoryList);
            _testDbContext.SaveChanges();



        }
        [TestMethod()]
        public void GetSuperSubCategory_Return_Test()
        {

            var subCategoryEntity = new SubmissionApplication { PrimaryID = "DB781A05-9B0F-4AA3-A7D6-C22F3E774C03" };

            //Act 
            var subcategories = _mockMasterSubCategoryTestService.GetSuperSubCategory(subCategoryEntity);

            //Assert
            Assert.IsNotNull(subcategories); // Test if null
            Assert.IsTrue(subcategories.Count() == 4);

        }
        [TestMethod]
        public void GetSuperSubCategory_primaryNotallowedSubcategory_Test()
        {
            var subCategoryEntity = new SubmissionApplication { PrimaryID = "BDABCA0B-8B80-4CC6-8836-6D5A751B6C54", Secondary = "1B69B449-ECAB-41AE-89A9-E2F76329A52B" };

            //Act 
            var subcategories = _mockMasterSubCategoryTestService.GetSuperSubCategory(subCategoryEntity);

            //Assert
            Assert.IsNotNull(subcategories); // Test if null
            Assert.IsTrue(subcategories.Count() == 4);

        }
        [TestMethod()]
        public void FindBySubCategoryID_SubCategoryID_Test()
        {


            //Act 
            var subcategories = _mockMasterSubCategoryTestService.FindBySubCategoryID("0495A756-2E3C-4444-8B6F-993445502401");

            //Assert
            Assert.IsNotNull(subcategories); // Test if null
            Assert.IsTrue(subcategories.Count() == 1);

        }
        [TestMethod()]
        public void SubCategories_Test()
        {


            //Act 
            var subcategories = _mockMasterSubCategoryTestService.SubCategories("General Business Licenses");

            //Assert
            Assert.IsNotNull(subcategories); // Test if null
            Assert.IsTrue(subcategories.Count() == 4);

        }
        [TestMethod()]
        public void FindBySubCategoriesBasedonPrimaryName_Test()
        {


            //Act 
            var subcategories = _mockMasterSubCategoryTestService.FindBySubCategoriesBasedonPrimaryName("General Business Licenses");

            //Assert
            Assert.IsNotNull(subcategories); // Test if null
            Assert.IsTrue(subcategories.Count() == 4);

        }
        [TestMethod()]
        public void FindBySubCategoryBasedonSubcatId_Test()
        {


            //Act 
            var subcategories = _mockMasterSubCategoryTestService.FindBySubCategoryBasedonSubcatId("0495A756-2E3C-4444-8B6F-993445502401");

            //Assert
            Assert.IsNotNull(subcategories); // Test if null
            Assert.IsTrue(subcategories.Count() == 1);

        }
        [TestMethod]
        public void DeleteSubCategoryTest()
        {
            var subCategoryEntity = new SubCategoryEntity { SubCatID = "0495A756-2E3C-4444-8B6F-993445502401", Status = false };
            //Act 
            var subcategories = _mockMasterSubCategoryTestService.DeleteSubCategory(subCategoryEntity);

            //Assert
            Assert.IsNotNull(subcategories); // Test if null
            Assert.IsTrue(subcategories == true);
        }
        [TestMethod]
        public void InsertSubCategory_WithNewDetails_Test()
        {
            var subCategoryEntity = new SubCategoryEntity { SubCatID = "", SubCategoryName = "SubCategory New", CustomCategoryName = "General Business Licenses", Status = true };
            //Act 
            var subcategories = _mockMasterSubCategoryTestService.InsertUpdateSubCategory(subCategoryEntity);
            //Assert
            Assert.IsNotNull(subcategories); // Test if null
            Assert.IsTrue(subcategories == 1);
        }
        [TestMethod]
        public void InsertSubCategory_SubCategoryName_AlreadyExists_Test()
        {
            var subCategoryEntity = new SubCategoryEntity { SubCatID = "", SubCategoryName = "Collection Agencies", CustomCategoryName = "General Business Licenses", Status = true };
            //Act 
            var subcategories = _mockMasterSubCategoryTestService.InsertUpdateSubCategory(subCategoryEntity);

            //Assert
            Assert.IsNotNull(subcategories); // Test if null
            Assert.IsTrue(subcategories == 3);
        }
        [TestMethod]
        public void InsertSubCategory_UpdateSubCategoryName_Test()
        {
            var subCategoryEntity = new SubCategoryEntity { SubCatID = "0AB4C44F-E720-4D96-A1FD-1BFA98752493", SubCategoryName = "Dog Day Care new", CustomCategoryName = "General Business Licenses", Status = true };
            //Act 
            var subcategories = _mockMasterSubCategoryTestService.InsertUpdateSubCategory(subCategoryEntity);

            //Assert
            Assert.IsNotNull(subcategories); // Test if null
            Assert.IsTrue(subcategories == 2);
        }
        [TestMethod]
        public void InsertSubCategory_UpdateSubCategoryNameAndStatusChange_Test()
        {
            var subCategoryEntity = new SubCategoryEntity { SubCatID = "0AB4C44F-E720-4D96-A1FD-1BFA98752493", SubCategoryName = "Dog Day Care ", CustomCategoryName = "General Business Licenses", Status = false };
            //Act 
            var subcategories = _mockMasterSubCategoryTestService.InsertUpdateSubCategory(subCategoryEntity);

            //Assert
            Assert.IsNotNull(subcategories); // Test if null
            Assert.IsTrue(subcategories == 2);
        }
        [TestMethod]
        public void InsertSubCategory_Update_OtherActivityNameExistsInTableAndStatusChange_Test()
        {
            var subCategoryEntity = new SubCategoryEntity { SubCatID = "0AB4C44F-E720-4D96-A1FD-1BFA98752493", SubCategoryName = "Shoe Cleaning/Repair", CustomCategoryName = "General Business Licenses", Status = false };
            //Act 
            var subcategories = _mockMasterSubCategoryTestService.InsertUpdateSubCategory(subCategoryEntity);

            //Assert
            Assert.IsNotNull(subcategories); // Test if null
            Assert.IsTrue(subcategories == 3);
        }
    }

}
