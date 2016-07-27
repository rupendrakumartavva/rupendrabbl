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
   public class MasterPrimaryCategoryServiceTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private MasterPrimaryCategoryRepository _masterPrimaryCategoryTestRepository;

        private MasterPrimaryCategoryRepositoryData _mockPrimaryData;
        private MasterSecondaryLicenseCategoryRepositoryData _mockPrimaryData1;

        private MasterSecondaryLicenseCategoryRepository _mockMasterSecondaryLicenseCategoryRepository;
        private MasterCategoryDocumentRepository _mockMasterCategoryDocumentRepository;
        private MasterCategoryQuestionRepository _mockMasterCategoryQuestionRepository;

        private MasterPrimaryCategoryService _mockPrimaryCategoryTestService;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            //_mockMasterPrimaryCategoryRepository = new Mock<IMasterPrimaryCategoryRepository>();

            _mockMasterSecondaryLicenseCategoryRepository = new MasterSecondaryLicenseCategoryRepository(_testUnitOfWork);
            _mockMasterCategoryDocumentRepository = new MasterCategoryDocumentRepository(_testUnitOfWork);
            _mockMasterCategoryQuestionRepository = new MasterCategoryQuestionRepository(_testUnitOfWork);


            _masterPrimaryCategoryTestRepository = new MasterPrimaryCategoryRepository(_testUnitOfWork,
                _mockMasterSecondaryLicenseCategoryRepository,
                _mockMasterCategoryDocumentRepository,
                _mockMasterCategoryQuestionRepository);

            _mockPrimaryData = new MasterPrimaryCategoryRepositoryData();
            _mockPrimaryData1 = new MasterSecondaryLicenseCategoryRepositoryData();

            _mockPrimaryCategoryTestService = new MasterPrimaryCategoryService(_masterPrimaryCategoryTestRepository);

            //Setup Data
            var testData = new MasterPrimaryCategoryRepositoryData();
            _testDbContext.MasterPrimaryCategory.AddRange(testData.MasterPrimaryCategoryEntitiesList);
            _testDbContext.SaveChanges();

            // 
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
        public void GetAll_PrimaryCategoryDetails_Return_Test()
        {

            //Act 

            var primaryCategory = _mockPrimaryCategoryTestService.GetAllPrimaryCategories().ToList();

            //Assert
            Assert.IsNotNull(primaryCategory); // Test if null

            Assert.IsTrue(primaryCategory.Count() == 12);

        }
        [TestMethod]
        public void GetFindByIdTest()
        {
            //Initialize Entites
            var submissionApplicationEntites = new SubmissionApplication { ActivityID = "DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A" };

            //Act 
            var primaryCategoryRows = _mockPrimaryCategoryTestService.FindPrimaryCategoryById(submissionApplicationEntites);

            //Assert
            Assert.IsNotNull(primaryCategoryRows); // Test if null
            Assert.IsTrue(primaryCategoryRows.Count() == 1);
        }
        [TestMethod]
        public void GetActiveFindByIdTest()
        {
            //Initialize Entites
            var submissionApplicationEntites = new SubmissionApplication { ActivityID = "DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A" };

            //Act 
            var primaryCategoryRows = _mockPrimaryCategoryTestService.ActiveFindById(submissionApplicationEntites).ToList();

            //Assert
            Assert.IsNotNull(primaryCategoryRows); // Test if null
            Assert.IsTrue(primaryCategoryRows.Count() == 1);
        }
        [TestMethod]
        public void GetFindByIDTest()
        {

            //Act 
            var primaryCategoryRows = _mockPrimaryCategoryTestService.FindPrimaryCategoryById("DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A");

            //Assert
            Assert.IsNotNull(primaryCategoryRows); // Test if null
            Assert.IsTrue(primaryCategoryRows.Count() == 1);
        }
        //[TestMethod]
        //public void GetFindByprimaryIdTest()
        //{
        //    //Initialize Entites
        //    var submissionApplicationEntites = new SubmissionApplication { PrimaryID = "6C548EBE-59ED-40B8-BEBE-EDF79149295D" };

        //    //Act 
        //    var primaryCategoryRows = _mockPrimaryCategoryTestService.FindByprimaryID(submissionApplicationEntites);

        //    //Assert
        //    Assert.IsNotNull(primaryCategoryRows); // Test if null
        //    Assert.IsTrue(primaryCategoryRows.Count() == 1);
        //}
        [TestMethod]
        public void GetFindByCategoryIdTest()
        {

            //Act 
            var primaryCategoryRows = _mockPrimaryCategoryTestService.FindByCategoryID("6C548EBE-59ED-40B8-BEBE-EDF79149295D");

            //Assert
            Assert.IsNotNull(primaryCategoryRows); // Test if null
            Assert.IsTrue(primaryCategoryRows.Count() == 1);
        }
        [TestMethod]
        public void GetSecondaryEndorsementTest()
        {

            //Act 
            var primaryCategoryRows = _mockPrimaryCategoryTestService.SecondaryEndorsement("Hotel");

            //Assert
            Assert.IsNotNull(primaryCategoryRows); // Test if null
            Assert.IsTrue(primaryCategoryRows.Count() == 1);
        }
        [TestMethod]
        public void GetFindBySecondaryPrimaryIdTest()
        {
            //Initialize Entites
            var submissionApplicationEntites = new SubmissionApplication { PrimaryID = "640C7E4B-8F90-4E59-9BA5-D1EA71E7B6FB" };

            //Act 
            var primaryCategoryRows = _mockPrimaryCategoryTestService.FindSecondaryCategoryById(submissionApplicationEntites).ToList();

            //Assert
            Assert.IsNotNull(primaryCategoryRows); // Test if null
            Assert.IsTrue(primaryCategoryRows.Count() == 1);

        }

        [TestMethod]
        public void SecondaryCategoriesListTest()
        {

            //Act 
            var primaryCategoryRows = _mockPrimaryCategoryTestService.SecondaryCategoriesList("640C7E4B-8F90-4E59-9BA5-D1EA71E7B6FB").ToList();

            //Assert
            Assert.IsNotNull(primaryCategoryRows); // Test if null
            Assert.IsTrue(primaryCategoryRows.Count() == 1);

        }
        [TestMethod]
        public void AddSecondaryLicenseCategory_WithPrimaryCategory_WithStatusActive_Return_Test()
        {
            //
            var secondaryEntity = new SlCategoryEntity
            {
                SecondaryId = "",
                PrimaryId = "6C548EBE-59ED-40B8-BEBE-EDF79149295D",
                SecondaryLicenseCategory = "Hotel",
                UnitOne = "",
                UnitTwo = "",
                Endorsement = "",
                Status = true,
                IsSuperSubCategoryAllow = false
            };

            //Act 
            var primaryCategoryRows = _mockPrimaryCategoryTestService.ActiveSecondary(secondaryEntity);

            //Assert
            Assert.IsNotNull(primaryCategoryRows); // Test if null
            Assert.IsTrue(primaryCategoryRows == 1);

        }
        [TestMethod]
        public void SecondayCategoryName_Already_Existes_WithPrimaryCategory_Return_Test()
        {
            //
            var secondaryEntity = new SlCategoryEntity
            {
                SecondaryId = "",
                PrimaryId = "6C548EBE-59ED-40B8-BEBE-EDF79149295D",
                SecondaryLicenseCategory = "Hotel",
                UnitOne = "",
                UnitTwo = "",
                Endorsement = "",
                Status = true,
                IsSuperSubCategoryAllow = false
            };

            //Act 
            var primaryCategoryRows = _mockPrimaryCategoryTestService.ActiveSecondary(secondaryEntity);

            //Assert
            Assert.IsNotNull(primaryCategoryRows); // Test if null
            Assert.IsTrue(primaryCategoryRows == 1);

        }
        [TestMethod]
        public void SecondayCategoryName_NotInPrimaryCategoryExists_Return_Test()
        {
            //
            var secondaryEntity = new SlCategoryEntity
            {
                SecondaryId = "",
                PrimaryId = "0766D9CA-DA1D-463D-8873-DD10F33D5BF4",
                SecondaryLicenseCategory = "Restaurant With Spa",
                UnitOne = "",
                UnitTwo = "",
                Endorsement = "",
                Status = true,
                IsSuperSubCategoryAllow = false
            };

            //Act 
            var primaryCategoryRows = _mockPrimaryCategoryTestService.ActiveSecondary(secondaryEntity);

            //Assert
            Assert.IsNotNull(primaryCategoryRows); // Test if null
            Assert.IsTrue(primaryCategoryRows == 0);
        }
        [TestMethod]
        public void SecondayCategoryName_PrimaryCategoryName_WithPrimaryCategoryInActive_Return_Test()
        {
            //
            var secondaryEntity = new SlCategoryEntity
            {
                SecondaryId = "",
                PrimaryId = "640C7E4B-8F90-4E59-9BA5-D1EA71E7BSEA",
                SecondaryLicenseCategory = "Food Vending Machine",
                UnitOne = "",
                UnitTwo = "",
                Endorsement = "",
                Status = true,
                IsSuperSubCategoryAllow = false
            };

            //Act 
            var primaryCategoryRows = _mockPrimaryCategoryTestService.ActiveSecondary(secondaryEntity);

            //Assert
            Assert.IsNotNull(primaryCategoryRows); // Test if null
            Assert.IsTrue(primaryCategoryRows == 1);

        }
        [TestMethod]
        public void FindByPrimaryName_Return_Description_Test()
        {
            //Act 
            var primaryCategoryRows = _mockPrimaryCategoryTestService.FindByPrimaryName("Hotel");

            //Assert
            Assert.IsNotNull(primaryCategoryRows); // Test if null
            Assert.IsTrue(primaryCategoryRows.Count() == 1);
        }
        [TestMethod]
        public void FindByPrimaryName_Like_Return_Description_Test()
        {
            //Act 
            var primaryCategoryRows = _mockPrimaryCategoryTestService.FindByPrimaryCategory("APARTMENT");

            //Assert
            Assert.IsNotNull(primaryCategoryRows); // Test if null
            Assert.IsTrue(primaryCategoryRows.Count() == 1);
        }
        [TestMethod]
        public void DeletePrimaryCategory_Return_Test()
        {
            //
            var delPrimary = new PrimaryCategoryEntity { PrimaryID = "796B2CFD-EBDC-4480-B45C-502A816860FB" };

            //Act 
            var primaryCategoryRows = _mockPrimaryCategoryTestService.DeletePrimaryCategory(delPrimary);

            //Assert
            Assert.IsNotNull(primaryCategoryRows); // Test if null
            Assert.IsTrue(primaryCategoryRows == true);
        }
        [TestMethod]
        public void FindByPrimaryIdbasedonActivity_Test()
        {
            //Act 
            var primaryCategoryRows = _mockPrimaryCategoryTestService.FindByPrimaryIdbasedonActivity("668F4B68-8437-431F-9052-60809E556028").ToList();

            //Assert
            Assert.IsNotNull(primaryCategoryRows); // Test if null
            Assert.IsTrue(primaryCategoryRows.Count() == 1);
        }
        [TestMethod]
        public void FindByCategoryIDBasedonPrimaryId_Return_PrimaryCategory_Test()
        {
            //Act 
            var primaryCategoryRows = _mockPrimaryCategoryTestService.FindByCategoryIDBasedonPrimaryId("BDABCA0B-8B80-4CC6-8836-6D5A751B6C54").ToList();

            //Assert
            Assert.IsNotNull(primaryCategoryRows); // Test if null
            Assert.IsTrue(primaryCategoryRows.Count() == 1);
        }

        [TestMethod]
        public void InsertUpdatePrimaryCategoryWith_PrimaryCategoryIdIncrement_Test()
        {
            var addPrimaryCategory = new PrimaryPhysicallocation
            {
                // Primary Category
                PrimaryID = "",
                ActivityID = "D96ABBB9-7437-4476-BBA6-72EF6A94F327",
                Description = "Hotel With New Trand",
                Endorsement = "Hotel",
                CategoryCode = "40404",
                UnitOne = "NA",
                UnitTwo = "NA",
                App_Type = "B",

                OldUnitOne = "",
                OldUnitTwo = "",
                OldCategoryName = ""

            };

            // Act 
            var primaryCategoryRows =
                _mockPrimaryCategoryTestService.InsertUpdatePrimaryCategory(addPrimaryCategory);

            //Assert
            Assert.IsNotNull(primaryCategoryRows); // Test if null
            Assert.IsTrue(primaryCategoryRows != "0");
        }

        [TestMethod]
        public void UpdatePrimaryCategory_PrimaryId_NotExist_Test()
        {
            var updatePrimaryCategory = new PrimaryPhysicallocation
            {
                PrimaryID = "6C548EBE-59ED-40B8-BEBE-EDF79149789623",
                ActivityID = "DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A",
                Description = "Hotel",
                Endorsement = "Housing: Transient",
                CategoryCode = "4001",
                UnitOne = "Rooms",
                UnitTwo = "Kitchens",
                App_Type = "B",
                IsSecondaryLicenseCategory = true,
                IsSubCategory = false,
                Status = true
            };

            // Act 
            var primaryCategoryRows =
                _mockPrimaryCategoryTestService.UpdatePrimaryCategory(updatePrimaryCategory);
            //Assert
            Assert.IsNotNull(primaryCategoryRows); // Test if null
            Assert.IsTrue(primaryCategoryRows == "3");
        }


        [TestMethod]
        public void UpdatePrimaryCategory_CategoryCode_Exist_Test()
        {
            var updatePrimaryCategory = new PrimaryPhysicallocation
            {
                PrimaryID = "6C548EBE-59ED-40B8-BEBE-EDF79149295D",
                ActivityID = "DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A",
                Description = "Hotel",
                Endorsement = "Housing: Transient",
                CategoryCode = "4001",
                UnitOne = "Rooms",
                UnitTwo = "Kitchens",
                App_Type = "B",
                IsSecondaryLicenseCategory = true,
                IsSubCategory = false,
                Status = true
            };

            // Act 
            var primaryCategoryRows =
                _mockPrimaryCategoryTestService.UpdatePrimaryCategory(updatePrimaryCategory);
            //Assert
            Assert.IsNotNull(primaryCategoryRows); // Test if null
            Assert.IsTrue(primaryCategoryRows == "1");
        }

        [TestMethod]
        public void UpdatePrimaryCategory_NotExist_Test()
        {
            var updatePrimaryCategory = new PrimaryPhysicallocation
            {
                PrimaryID = "6C548EBE-59ED-40B8-BEBE-EDF79149295D",
                ActivityID = "DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A",
                Description = "Hotel",
                Endorsement = "Housing: Transient",
                CategoryCode = "4111",
                UnitOne = "Rooms",
                UnitTwo = "Kitchens",
                App_Type = "B",
                IsSecondaryLicenseCategory = true,
                IsSubCategory = false,
                Status = true
            };

            // Act 
            var primaryCategoryRows =
                _mockPrimaryCategoryTestService.UpdatePrimaryCategory(updatePrimaryCategory);
            //Assert
            Assert.IsNotNull(primaryCategoryRows); // Test if null
            Assert.IsTrue(primaryCategoryRows == "2");
          
        }


        //[TestMethod]
        //public void UpdatePrimaryCategory_WithoutChange_PrimaryCategoryName_Test()
        //{
        //    var addPrimaryCategory = new PrimaryPhysicallocation
        //    {
        //        PrimaryID = "6C548EBE-59ED-40B8-BEBE-EDF79149295D",
        //        ActivityID = "DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A",
        //        Description = "Hotel",
        //        Endorsement = "Housing: Transient",
        //        CategoryCode = "5107",
        //        UnitOne = "Rooms",
        //        UnitTwo = "Kitchens",
        //        App_Type = "B",
        //        IsSecondaryLicenseCategory = true,
        //        IsSubCategory = false,
        //        Status = false
        //    };

        //    // Act 
        //    var primaryCategoryRows =
        //        _mockPrimaryCategoryTestService.UpdatePrimaryCategory(addPrimaryCategory);

        //    //Assert
        //    Assert.IsNotNull(primaryCategoryRows); // Test if null
        //    Assert.IsTrue(primaryCategoryRows == true);
        //}
        //[TestMethod]
        //public void UpdatePrimaryCategory_WithChange_PrimaryCategoryName_Test()
        //{
        //    var addPrimaryCategory = new PrimaryPhysicallocation
        //    {
        //        PrimaryID = "6C548EBE-59ED-40B8-BEBE-EDF79149295D",
        //        ActivityID = "DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A",
        //        Description = "Hotel Update",
        //        Endorsement = "Housing: Transient",
        //        CategoryCode = "5107",
        //        UnitOne = "Rooms",
        //        UnitTwo = "Kitchens",
        //        App_Type = "B",
        //        IsSecondaryLicenseCategory = true,
        //        IsSubCategory = false,
        //        Status = false
        //    };

        //    // Act 
        //    var primaryCategoryRows =
        //        _mockPrimaryCategoryTestService.UpdatePrimaryCategory(addPrimaryCategory);
        //    //Assert
        //    Assert.IsNotNull(primaryCategoryRows); // Test if null
        //    Assert.IsTrue(primaryCategoryRows == true);
        //}
    }

}
