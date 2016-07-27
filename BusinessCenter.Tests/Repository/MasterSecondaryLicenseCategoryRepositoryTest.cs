using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data.Interface;
using Moq;
using BusinessCenter.Data;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Model;
using BusinessCenter.Tests.Setup;

namespace BusinessCenter.Tests.Repository
{
    [TestClass]
    public class MasterSecondaryLicenseCategoryRepositoryTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private MasterSecondaryLicenseCategoryRepository _masterSecondaryLicenseCatTestRepository;
        private Mock<IMasterSecondaryLicenseCategoryRepository> _mockSecondaryLicenseCatRepository;
        private MasterSecondaryLicenseCategoryRepositoryData _mockData;

         [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _masterSecondaryLicenseCatTestRepository = new MasterSecondaryLicenseCategoryRepository(_testUnitOfWork);

            //Mocking Repository
            _mockSecondaryLicenseCatRepository = new Mock<IMasterSecondaryLicenseCategoryRepository>();
            _mockData = new MasterSecondaryLicenseCategoryRepositoryData();
             
            //Setup Data
            var testData = new MasterSecondaryLicenseCategoryRepositoryData();
            _testDbContext.MasterSecondaryLicenseCategory.AddRange(testData.MasterSeconderyLicenseCategoryList);
            _testDbContext.SaveChanges();
        }

         [TestMethod]
         public void GetAll_SecondaryLicenses_Entities_Test()
         {
             //Act 
             var secondaries = _masterSecondaryLicenseCatTestRepository.FindByID("6C548EBE-59ED-40B8-BEBE-EDF79149295D");
             //Assert
             Assert.IsNotNull(secondaries); // Test if null
             Assert.IsTrue(secondaries.Count() == 6);
         }

         [TestMethod]
         public void GetFindByIdTest()
         {
             var masterSecondaryCategoryEntity = new SubmissionApplication { PrimaryID = "BDABCA0B-8B80-4CC6-8836-6D5A751B6C54", Status = "true" };
             //Act 
             var secondaries = _masterSecondaryLicenseCatTestRepository.FindByID(masterSecondaryCategoryEntity);
             //Assert
             Assert.IsNotNull(secondaries); // Test if null
             Assert.IsTrue(secondaries.Count() == 1);
         }

         [TestMethod]
         public void FindByPrimaryIdTest()
         {
             //Act 
             var secondaries = _masterSecondaryLicenseCatTestRepository.FindByPrimaryId("BDABCA0B-8B80-4CC6-8836-6D5A751B6C54");
             //Assert
             Assert.IsNotNull(secondaries); // Test if null
             Assert.IsTrue(secondaries.Count() == 1);
         }

         [TestMethod]
         public void FindBySecondaryIDTest()
         {
             //Act 
             var secondaries = _masterSecondaryLicenseCatTestRepository.FindBySecondaryID("1DE53FC4-F444-44EA-99A4-5DA3F107D5DD");
             //Assert
             Assert.IsNotNull(secondaries); // Test if null
             Assert.IsTrue(secondaries.Count() == 1);
         }

         [TestMethod]
         public void FindBySecondaryNameTest()
         {
             //Act 
             var secondaries = _masterSecondaryLicenseCatTestRepository.FindBySecondaryName("Food Vending Machine");
             //Assert
             Assert.IsNotNull(secondaries); // Test if null
             Assert.IsTrue(secondaries.Count() == 2);
         }

         [TestMethod]
         public void DeleteSecondaryCategoryTest()
         {
             var masterSecondaryCategoryEntity = new SlCategoryEntity { SecondaryId = "1B69B449-ECAB-41AE-89A9-E2F76329A52B" };
             //Act 
             var secondary = _masterSecondaryLicenseCatTestRepository.DeleteSecondaryCategory(masterSecondaryCategoryEntity);

             //Assert
             Assert.IsNotNull(secondary); // Test if null
             Assert.IsTrue(secondary == true);
         }

         [TestMethod]
         public void DeleteSecondaryByPrimaryTest()
         {
             //Act 
             var secondary = _masterSecondaryLicenseCatTestRepository.DeleteSecondaryByPrimary("General Business Licenses");

             //Assert
             Assert.IsNotNull(secondary); // Test if null
             Assert.IsTrue(secondary == true);
         }

         [TestMethod]
         public void UpdateCategoryNameTest()
         {
             //Act 
             var secondary = _masterSecondaryLicenseCatTestRepository.UpdateCategoryName("Food Vending Machine", "Food Selling Machine");

             //Assert
             Assert.IsNotNull(secondary); // Test if null
             Assert.IsTrue(secondary == true);
         }

         [TestMethod]
         public void FindBySecondaryBasedonPrimaryIdTest()
         {
             //Act 
             var secondaries = _masterSecondaryLicenseCatTestRepository.FindBySecondaryBasedonPrimaryId("BDABCA0B-8B80-4CC6-8836-6D5A751B6C54");

             //Assert
             Assert.IsNotNull(secondaries); // Test if null
             Assert.IsTrue(secondaries.Count() == 1);
         }

         [TestMethod]
         public void FindBySecondaryBasedonSecondaryIdTest()
         {
             //Act 
             var secondaries = _masterSecondaryLicenseCatTestRepository.FindBySecondaryBasedonSecondaryId("1DE53FC4-F444-44EA-99A4-5DA3F107D5DD");

             //Assert
             Assert.IsNotNull(secondaries); // Test if null
             Assert.IsTrue(secondaries.Count() == 1);
         }

         [TestMethod]
         public void InsertSecondaryCategory_WithNewDetails_Test()
         {
             var masterSecondaryEntityEntity = new SlCategoryEntity { SecondaryId = "", PrimaryId = "0766D9CA-DA1D-463D-8873-DD10F33D5BF4", SecondaryLicenseCategory = "New Entertainment", Status = true};
             //Act 
             var secondaries = _masterSecondaryLicenseCatTestRepository.InsertUpdateSlCategory(masterSecondaryEntityEntity);

             //Assert
             Assert.IsNotNull(secondaries); // Test if null
             Assert.IsTrue(secondaries == 1);
         }

         [TestMethod]
         public void InsertSecondaryCategory_AlreadyExists_Test()
         {
             var masterSecondaryEntityEntity = new SlCategoryEntity { SecondaryId = "", PrimaryId = "BDABCA0B-8B80-4CC6-8836-6D5A751B6C54", SecondaryLicenseCategory = "General Business Licenses", Status = true };
             //Act 
             var secondaries = _masterSecondaryLicenseCatTestRepository.InsertUpdateSlCategory(masterSecondaryEntityEntity);

             //Assert
             Assert.IsNotNull(secondaries); // Test if null
             Assert.IsTrue(secondaries == 3);
         }

         [TestMethod]
         public void InsertSecondaryCategory_Updatestatus_Test()
         {
             var masterSecondaryEntityEntity = new SlCategoryEntity { SecondaryId = "1DE53FC4-F444-44EA-99A4-5DA3F107D5DD", PrimaryId = "6C548EBE-59ED-40B8-BEBE-EDF79149295D", SecondaryLicenseCategory = "Food Vending Machine", Status = false };
             //Act 
             var secondaries = _masterSecondaryLicenseCatTestRepository.InsertUpdateSlCategory(masterSecondaryEntityEntity);

             //Assert
             Assert.IsNotNull(secondaries); // Test if null
             Assert.IsTrue(secondaries == 2);
         }
        
    }
}
