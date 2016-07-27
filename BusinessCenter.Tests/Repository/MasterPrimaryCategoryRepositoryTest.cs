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
    public class MasterPrimaryCategoryRepositoryTest
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
       // private Mock<IMasterPrimaryCategoryRepository> _mockMasterPrimaryCategoryTestRepository;

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
            _mockPrimaryData1=new MasterSecondaryLicenseCategoryRepositoryData();

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

            var primaryCategory = _masterPrimaryCategoryTestRepository.AllPrimaryCategories().ToList();

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
            var primaryCategoryRows = _masterPrimaryCategoryTestRepository.FindByID(submissionApplicationEntites);

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
            var primaryCategoryRows = _masterPrimaryCategoryTestRepository.ActiveFindById(submissionApplicationEntites).ToList();

            //Assert
            Assert.IsNotNull(primaryCategoryRows); // Test if null
            Assert.IsTrue(primaryCategoryRows.Count() == 1);
        }


        [TestMethod]
        public void GetFindByIDTest()
        {
         
            //Act 
            var primaryCategoryRows = _masterPrimaryCategoryTestRepository.FindByID("DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A");

            //Assert
            Assert.IsNotNull(primaryCategoryRows); // Test if null
            Assert.IsTrue(primaryCategoryRows.Count() == 1);
        }

        [TestMethod]
        public void GetFindByprimaryIdTest()
        {
            //Initialize Entites
            var submissionApplicationEntites = new SubmissionApplication { PrimaryID = "6C548EBE-59ED-40B8-BEBE-EDF79149295D" };

            //Act 
            var primaryCategoryRows = _masterPrimaryCategoryTestRepository.FindByprimaryID(submissionApplicationEntites);

            //Assert
            Assert.IsNotNull(primaryCategoryRows); // Test if null
            Assert.IsTrue(primaryCategoryRows.Count() == 1);
        }


        [TestMethod]
        public void GetFindByCategoryIdTest()
        {
           
            //Act 
            var primaryCategoryRows = _masterPrimaryCategoryTestRepository.FindByCategoryID("6C548EBE-59ED-40B8-BEBE-EDF79149295D");

            //Assert
            Assert.IsNotNull(primaryCategoryRows); // Test if null
            Assert.IsTrue(primaryCategoryRows.Count() == 1);
        }

        [TestMethod]
        public void GetSecondaryEndorsementTest()
        {

            //Act 
            var primaryCategoryRows = _masterPrimaryCategoryTestRepository.SecondaryEndorsement("Hotel");

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
            var primaryCategoryRows = _masterPrimaryCategoryTestRepository.FindBySecondaryPrimaryID(submissionApplicationEntites).ToList();

            //Assert
            Assert.IsNotNull(primaryCategoryRows); // Test if null
            Assert.IsTrue(primaryCategoryRows.Count()==1);
            
        }

        //[TestMethod]
        //public void GetFindBySecondaryPrimaryId_WithNA_Test()
        //{
        //    //Initialize Entites
        //    var submissionApplicationEntites = new SubmissionApplication { PrimaryID = "65868F7E-6053-4645-BA36-95635ACFEB4B" };

        //    //Act 
        //    var primaryCategoryRows = _masterPrimaryCategoryTestRepository.FindBySecondaryPrimaryID(submissionApplicationEntites).ToList();

        //    //Assert
        //    Assert.IsNotNull(primaryCategoryRows); // Test if null
        //    Assert.IsTrue(primaryCategoryRows.Count() == 3);

        //}

        [TestMethod]
        public void SecondaryCategoriesListTest()
        {
           
            //Act 
            var primaryCategoryRows = _masterPrimaryCategoryTestRepository.SecondaryCategoriesList("640C7E4B-8F90-4E59-9BA5-D1EA71E7B6FB").ToList();

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
            var primaryCategoryRows = _masterPrimaryCategoryTestRepository.ActiveSecondary(secondaryEntity);

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
            var primaryCategoryRows = _masterPrimaryCategoryTestRepository.ActiveSecondary(secondaryEntity);

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
            var primaryCategoryRows = _masterPrimaryCategoryTestRepository.ActiveSecondary(secondaryEntity);

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
             var primaryCategoryRows = _masterPrimaryCategoryTestRepository.ActiveSecondary(secondaryEntity);

             //Assert
             Assert.IsNotNull(primaryCategoryRows); // Test if null
             Assert.IsTrue(primaryCategoryRows == 1);

         }

         [TestMethod]
         public void UpdateSecondaryLicenseCategoryName_WithOutChangeCategoryName_WithStatusActive_Return_Test()
         {
             //
             var secondaryEntity = new SlCategoryEntity
             {
                 SecondaryId = "640C7E4B-8F90-4E59-9BA5-D1EA71E7B6AA",
                 PrimaryId = "640C7E4B-8F90-4E59-9BA5-D1EA71E7B6FB",
                 SecondaryLicenseCategory = "Food Vending Machine",
                 UnitOne = "",
                 UnitTwo = "",
                 Endorsement = "",
                 Status = true,
                 IsSuperSubCategoryAllow = false
             };

             //Act 
             var primaryCategoryRows = _masterPrimaryCategoryTestRepository.ActiveSecondary(secondaryEntity);
             //Assert
             Assert.IsNotNull(primaryCategoryRows); // Test if null
             Assert.IsTrue(primaryCategoryRows == 2);
         }


         [TestMethod]
         public void UpdateSecondaryLicenseCategoryName_WithChangeCategoryName_WithStatusActive_Return_Test()
         {
             //
             var secondaryEntity = new SlCategoryEntity
             {
                 SecondaryId = "1B69B449-ECAB-41AE-89A9-E2F76329A52B",
                 PrimaryId = "BDABCA0B-8B80-4CC6-8836-6D5A751B6C54",
                 SecondaryLicenseCategory = "General Business",
                 UnitOne = "",
                 UnitTwo = "",
                 Endorsement = "",
                 Status = true,
                 IsSuperSubCategoryAllow = false
             };

             //Act 
             var primaryCategoryRows = _masterPrimaryCategoryTestRepository.ActiveSecondary(secondaryEntity);

             //Assert
             Assert.IsNotNull(primaryCategoryRows); // Test if null
             Assert.IsTrue(primaryCategoryRows == 2);
         }

         [TestMethod]
         public void UpdateSecondaryLicenseCategoryName_NotInPrimaryCategory_Return_Test()
         {
             //
             var secondaryEntity = new SlCategoryEntity
             {
                 SecondaryId = "06E16E9C-5F17-46FB-A919-384C141F3EA7",
                 PrimaryId = "0766D9CA-DA1D-463D-8873-DD10F33D5BF4",
                 SecondaryLicenseCategory = "Hotel WithSpa",
                 UnitOne = "",
                 UnitTwo = "",
                 Endorsement = "",
                 Status = true,
                 IsSuperSubCategoryAllow = false
             };

             //Act 
             var primaryCategoryRows = _masterPrimaryCategoryTestRepository.ActiveSecondary(secondaryEntity);

             //Assert
             Assert.IsNotNull(primaryCategoryRows); // Test if null
             Assert.IsTrue(primaryCategoryRows == 0);
         }

         [TestMethod]
         public void FindByPrimaryName_Return_Description_Test()
         {
             //Act 
             var primaryCategoryRows = _masterPrimaryCategoryTestRepository.FindByPrimaryName("Hotel");

             //Assert
             Assert.IsNotNull(primaryCategoryRows); // Test if null
             Assert.IsTrue(primaryCategoryRows.Count()==1);
         }

         [TestMethod]
         public void FindByPrimaryName_Like_Return_Description_Test()
         {
             //Act 
             var primaryCategoryRows = _masterPrimaryCategoryTestRepository.FindByPrimaryCategory("ONE FAMILY RENTAL");

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
             var primaryCategoryRows = _masterPrimaryCategoryTestRepository.DeletePrimaryCategory(delPrimary);

             //Assert
             Assert.IsNotNull(primaryCategoryRows); // Test if null
             Assert.IsTrue(primaryCategoryRows == true);
         }

         [TestMethod]
         public void DeletePrimaryCategory_AlreadyDeleted_Description_Test()
         {
             //
             var delPrimary = new PrimaryCategoryEntity { PrimaryID = "796B2CFD-EBDC-4480-B45C-502A816860FB" };

             //Act 
             var primaryCategoryRows = _masterPrimaryCategoryTestRepository.DeletePrimaryCategory(delPrimary);

             //Assert
             Assert.IsNotNull(primaryCategoryRows); // Test if null
             Assert.IsTrue(primaryCategoryRows == true);
         }

         [TestMethod]
         public void FindByPrimaryIdbasedonActivity_Test()
         {
             //Act 
             var primaryCategoryRows = _masterPrimaryCategoryTestRepository.FindByPrimaryIdbasedonActivity("668F4B68-8437-431F-9052-60809E556028").ToList();

             //Assert
             Assert.IsNotNull(primaryCategoryRows); // Test if null
             Assert.IsTrue(primaryCategoryRows.Count() == 1);
         }

         [TestMethod]
         public void FindByCategoryIDBasedonPrimaryId_Return_PrimaryCategory_Test()
         {
             //Act 
             var primaryCategoryRows = _masterPrimaryCategoryTestRepository.FindByCategoryIDBasedonPrimaryId("BDABCA0B-8B80-4CC6-8836-6D5A751B6C54").ToList();

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
       PrimaryID="",
        ActivityID="D96ABBB9-7437-4476-BBA6-72EF6A94F327",
        Description="Hotel With New Trand",
        Endorsement="Hotel",
        CategoryCode="40404",
       UnitOne="NA",
      UnitTwo="NA",
       App_Type="B",

    OldUnitOne="",
      OldUnitTwo="",
        OldCategoryName=""

             };

            // Act 
             var primaryCategoryRows =
                 _masterPrimaryCategoryTestRepository.InsertUpdatePrimaryCategory(addPrimaryCategory);

             //Assert
             Assert.IsNotNull(primaryCategoryRows); // Test if null
             Assert.IsTrue(primaryCategoryRows!="0");
         }

         [TestMethod]
         public void InsertUpdatePrimaryCategoryWith_PrimaryCategoryalreadyexists_Test()
         {
             var addPrimaryCategory = new PrimaryPhysicallocation
             {
                 // Primary Category
                 PrimaryID = "",
                 ActivityID = "DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A",
                 Description = "Hotel",
                 Endorsement = "Housing: Transient",
                 CategoryCode = "5105",
                 UnitOne = "Rooms",
                 UnitTwo = "Kitchens",
                 App_Type = "B",
                 OldUnitOne = "",
                 OldUnitTwo = "",
                 OldCategoryName = ""
             };

             // Act 
             var primaryCategoryRows =
                 _masterPrimaryCategoryTestRepository.InsertUpdatePrimaryCategory(addPrimaryCategory);

             //Assert
             Assert.IsNotNull(primaryCategoryRows); // Test if null
             Assert.IsTrue(primaryCategoryRows == "3");
         }

         [TestMethod]
         public void InsertUpdatePrimaryCategoryWith_CategoryCodealreadyexists_Test()
         {
             var addPrimaryCategory = new PrimaryPhysicallocation
             {
                 // Primary Category
                 PrimaryID = "",
                 ActivityID = "DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A",
                 Description = "Hotels",
                 Endorsement = "Housing: Transient",
                 CategoryCode = "5107",
                 UnitOne = "Rooms",
                 UnitTwo = "Kitchens",
                 App_Type = "B",
                 OldUnitOne = "",
                 OldUnitTwo = "",
                 OldCategoryName = ""
             };

             // Act 
             var primaryCategoryRows =
                 _masterPrimaryCategoryTestRepository.InsertUpdatePrimaryCategory(addPrimaryCategory);

             //Assert
             Assert.IsNotNull(primaryCategoryRows); // Test if null
             Assert.IsTrue(primaryCategoryRows == "4");
         }

         [TestMethod]
         public void InsertUpdatePrimaryCategory_CheckNulls_Test()
         {
             var addPrimaryCategory = new PrimaryPhysicallocation
             {
                 PrimaryID = "",
                 ActivityID = "DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A",
                 Description = "Test Primary",
                 Endorsement = "Housing: Transient",
                 CategoryCode = "3007",
                 UnitOne = null,
                 UnitTwo = null,
                 App_Type = "B",
                 IsSecondaryLicenseCategory = true,
                 IsSubCategory = false,
                 Status = true
             };

             // Act 
             var primaryCategoryRows =
                 _masterPrimaryCategoryTestRepository.InsertUpdatePrimaryCategory(addPrimaryCategory);
             //Assert
             Assert.IsNotNull(primaryCategoryRows); // Test if null
             Assert.IsTrue(primaryCategoryRows != "");
         }

        // UpdatePrimaryCategory
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
                 _masterPrimaryCategoryTestRepository.UpdatePrimaryCategory(updatePrimaryCategory);
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
                Endorsement="Housing: Transient",
                 CategoryCode = "4001",
                UnitOne="Rooms",
                UnitTwo="Kitchens",
                App_Type = "B",
                IsSecondaryLicenseCategory=true,
                IsSubCategory=false,
                Status = true
             };

             // Act 
             var primaryCategoryRows =
                 _masterPrimaryCategoryTestRepository.UpdatePrimaryCategory(updatePrimaryCategory);
             //Assert
             Assert.IsNotNull(primaryCategoryRows); // Test if null
             Assert.IsTrue(primaryCategoryRows == "1");
         }

         [TestMethod]
         public void UpdatePrimaryCategory_CategoryCode_NotExist_Test()
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
                 _masterPrimaryCategoryTestRepository.UpdatePrimaryCategory(updatePrimaryCategory);
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
         //        _masterPrimaryCategoryTestRepository.UpdatePrimaryCategory(addPrimaryCategory);

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
         //        _masterPrimaryCategoryTestRepository.UpdatePrimaryCategory(addPrimaryCategory);
         //    //Assert
         //    Assert.IsNotNull(primaryCategoryRows); // Test if null
         //    Assert.IsTrue(primaryCategoryRows == true);
         //}
    }
}
