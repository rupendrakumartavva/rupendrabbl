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
   public class MasterSecondaryLicenseCategoryServiceTest
    {

        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private MasterSecondaryLicenseCategoryRepository _masterSecondaryLicenseCategoryTestRepository;

     
        private MasterSecondaryLicenseCategoryRepositoryData _mockData;
        private MasterSecondaryLicenseCategoryService _mockSecondaryLicenseCategoryTestService;
        [TestInitialize]
        public void Initialize()
        {

            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
          

            _masterSecondaryLicenseCategoryTestRepository = new MasterSecondaryLicenseCategoryRepository(_testUnitOfWork);

            //Mocking Repository
            _mockData = new MasterSecondaryLicenseCategoryRepositoryData();



           
            _mockSecondaryLicenseCategoryTestService = new MasterSecondaryLicenseCategoryService(_masterSecondaryLicenseCategoryTestRepository);

            //Setup Data
            var testData = new MasterSecondaryLicenseCategoryRepositoryData();
            _testDbContext.MasterSecondaryLicenseCategory.AddRange(testData.MasterSeconderyLicenseCategoryList);
            _testDbContext.SaveChanges();



        }
        [TestMethod]
        public void FindSecondaryCategoryById_Return_Test()
        {
            var submissionApplication = new SubmissionApplication { PrimaryID = "6C548EBE-59ED-40B8-BEBE-EDF79149295D"};


            //Act 
            var secondaryRows = _mockSecondaryLicenseCategoryTestService.FindSecondaryCategoryById(submissionApplication);

            //Assert
            Assert.IsNotNull(secondaryRows); // Test if null
            Assert.IsTrue(secondaryRows.Count() == 6);
        }
        [TestMethod]
        public void FindSecondaryCategoryById_With_PrimaryId_Return_Test()
        {
          

            //Act 
            var secondaryRows = _mockSecondaryLicenseCategoryTestService.FindSecondaryCategoryById("6C548EBE-59ED-40B8-BEBE-EDF79149295D");

            //Assert
            Assert.IsNotNull(secondaryRows); // Test if null
            Assert.IsTrue(secondaryRows.Count() == 6);
        }
        [TestMethod]
        public void DeleteSecondaryCategoryTest()
        {
            var masterSecondaryCategoryEntity = new SlCategoryEntity { SecondaryId = "1B69B449-ECAB-41AE-89A9-E2F76329A52B" };
            //Act 
            var secondaryRows = _mockSecondaryLicenseCategoryTestService.DeleteSecondaryCategory(masterSecondaryCategoryEntity);

            //Assert
            Assert.IsNotNull(secondaryRows); // Test if null
            Assert.IsTrue(secondaryRows == true);
        }
        [TestMethod]
        public void FindSecondaryId_With_SecondaryId_Return_Test()
        {


            //Act 
            var secondaryRows = _mockSecondaryLicenseCategoryTestService.FindSecondaryId("1DE53FC4-F444-44EA-99A4-5DA3F107D5DD");

            //Assert
            Assert.IsNotNull(secondaryRows); // Test if null
            Assert.IsTrue(secondaryRows.Count() == 1);
        }
        [TestMethod]
        public void FindBySecondaryBasedonPrimaryId_With_PrimaryId_Return_Test()
        {


            //Act 
            var secondaryRows = _mockSecondaryLicenseCategoryTestService.FindBySecondaryBasedonPrimaryId("6C548EBE-59ED-40B8-BEBE-EDF79149295D");

            //Assert
            Assert.IsNotNull(secondaryRows); // Test if null
            Assert.IsTrue(secondaryRows.Count() == 6);
        }
        [TestMethod]
        public void FFindBySecondaryBasedonSecondaryId_With_SecondaryId_Return_Test()
        {


            //Act 
            var secondaryRows = _mockSecondaryLicenseCategoryTestService.FindBySecondaryBasedonSecondaryId("1DE53FC4-F444-44EA-99A4-5DA3F107D5DD");

            //Assert
            Assert.IsNotNull(secondaryRows); // Test if null
            Assert.IsTrue(secondaryRows.Count() == 1);
        }

        [TestMethod]
        public void InsertSecondaryCategory_WithNewDetails_Test()
        {
            var masterSecondaryEntityEntity = new SlCategoryEntity { SecondaryId = "", PrimaryId = "0766D9CA-DA1D-463D-8873-DD10F33D5BF4", SecondaryLicenseCategory = "New Entertainment", Status = true };
            //Act 
            var secondaryRows = _mockSecondaryLicenseCategoryTestService.InsertUpdateSlCategory(masterSecondaryEntityEntity);

            //Assert
            Assert.IsNotNull(secondaryRows); // Test if null
            Assert.IsTrue(secondaryRows == 1);
        }

        [TestMethod]
        public void InsertSecondaryCategory_AlreadyExists_Test()
        {
            var masterSecondaryEntityEntity = new SlCategoryEntity { SecondaryId = "", PrimaryId = "BDABCA0B-8B80-4CC6-8836-6D5A751B6C54", SecondaryLicenseCategory = "General Business Licenses", Status = true };
            //Act 
            var secondaryRows = _mockSecondaryLicenseCategoryTestService.InsertUpdateSlCategory(masterSecondaryEntityEntity);

            //Assert
            Assert.IsNotNull(secondaryRows); // Test if null
            Assert.IsTrue(secondaryRows == 3);
        }
        [TestMethod]
        public void InsertSecondaryCategory_Updatestatus_Test()
        {
            var masterSecondaryEntityEntity = new SlCategoryEntity { SecondaryId = "1DE53FC4-F444-44EA-99A4-5DA3F107D5DD", PrimaryId = "6C548EBE-59ED-40B8-BEBE-EDF79149295D", SecondaryLicenseCategory = "Food Vending Machine", Status = false };
            //Act 
            var secondaryRows = _mockSecondaryLicenseCategoryTestService.InsertUpdateSlCategory(masterSecondaryEntityEntity);

            //Assert
            Assert.IsNotNull(secondaryRows); // Test if null
            Assert.IsTrue(secondaryRows == 2);
        }
    }

}
