using BusinessCenter.Data;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using BusinessCenter.Tests.Setup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Repository
{
    [TestClass]
    public class MasterCategoryPhysicalLocationRepositoryTest
    {
        //Initialization DBConnection
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Initialization of Repositories
        private MasterCategoryPhysicalLocationRepository _masterCategoryPhysicalLocationTestRepository;
        private MasterPrimaryCategoryRepository _mockMasterPrimaryCategoryRepository;
        private MasterCategoryQuestionRepository _mockMasterCategoryQuestionRepository;
        private  OSubCategoryFeesRepository _mockSubCategoryFeesRepository;
        private MasterSecondaryLicenseCategoryRepository _mockMasterSecondaryLicenseCategoryRepository;
     //   private MasterCategoryPhysicalLocationData _masterCategoryPhysicalLocationData;

        private MasterCategoryDocumentRepository _masterCategoryDocument;



        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _mockMasterSecondaryLicenseCategoryRepository = new MasterSecondaryLicenseCategoryRepository(_testUnitOfWork);



            _masterCategoryDocument=new MasterCategoryDocumentRepository(_testUnitOfWork);
            _mockMasterCategoryQuestionRepository = new MasterCategoryQuestionRepository(_testUnitOfWork);
            _mockMasterPrimaryCategoryRepository = new MasterPrimaryCategoryRepository(_testUnitOfWork, 
                                _mockMasterSecondaryLicenseCategoryRepository,
                                _masterCategoryDocument, _mockMasterCategoryQuestionRepository);






       
          
        

            

            _mockSubCategoryFeesRepository = new OSubCategoryFeesRepository(_testUnitOfWork, _mockMasterPrimaryCategoryRepository,
              _mockMasterSecondaryLicenseCategoryRepository);





            _masterCategoryPhysicalLocationTestRepository = new MasterCategoryPhysicalLocationRepository(_testUnitOfWork,
                                                            _mockMasterPrimaryCategoryRepository,
                                                            _mockMasterCategoryQuestionRepository
                                                            , _mockSubCategoryFeesRepository
                                                            , _mockMasterSecondaryLicenseCategoryRepository);

            //Mocking Repository
        //    _mockData = new MasterCategoryPhysicalLocationData();

            //Setup Data
            var testPrimaryCategoryData = new MasterPrimaryCategoryRepositoryData();
            _testDbContext.MasterPrimaryCategory.AddRange(testPrimaryCategoryData.MasterPrimaryCategoryEntitiesList);
            _testDbContext.SaveChanges();

            var masterCategoryQuestionData = new MasterCategoryQuestionData();
            _testDbContext.MasterCategoryQuestion.AddRange(masterCategoryQuestionData.CategoryQuestionEntitiesList);
            _testDbContext.SaveChanges();

            var testOsubcategoryFeeData = new OSubCategoryFeesData();
            _testDbContext.OSub_Category_Fees.AddRange(testOsubcategoryFeeData.OSubCategoryFeesEntitiesList);
            _testDbContext.SaveChanges();

            var secondaryLicenseData = new MasterSecondaryLicenseCategoryRepositoryData();
            _testDbContext.MasterSecondaryLicenseCategory.AddRange(secondaryLicenseData.MasterSeconderyLicenseCategoryList);
            _testDbContext.SaveChanges();

            var data = new MasterCategoryPhysicalLocationData();
            _testDbContext.MasterCategoryPhysicalLocation.AddRange(data.MasterCategoryPhysicalLocationList);
            _testDbContext.SaveChanges();
                       
        }


        //AllCategoryPhysicallocations

        [TestMethod]
        public void GetAllCategoryPhysicallocationsTest()
        {

            //Act 
            var physicalLocationRows = _masterCategoryPhysicalLocationTestRepository.AllCategoryPhysicallocations();

            //Assert
            Assert.IsNotNull(physicalLocationRows); // Test if null
            Assert.IsTrue(physicalLocationRows.Count() == 9);
        }

        [TestMethod]
        public void FindBy_CategoryPhysicallocationsTest()
        {
            var s = new SubmissionApplication { PrimaryID = "6C548EBE-59ED-40B8-BEBE-EDF79149295D" };
            //Act 
            var physicalLocationRows = _masterCategoryPhysicalLocationTestRepository.FindByID(s);

            //Assert
            Assert.IsNotNull(physicalLocationRows); // Test if null
            Assert.IsTrue(physicalLocationRows.Count() == 1);
        }

        [TestMethod]
        public void FindBy_PassString_CategoryPhysicallocationsTest()
        {
           
            //Act 
            var physicalLocationRows = _masterCategoryPhysicalLocationTestRepository.FindCategoryID("6C548EBE-59ED-40B8-BEBE-EDF79149295D");

            //Assert
            Assert.IsNotNull(physicalLocationRows); // Test if null
            Assert.IsTrue(physicalLocationRows.Count() == 1);
        }

        [TestMethod]
        public void AllScreeningQuestions_Retrun_Test()
        {
            var h = new SubmissionApplication { ActivityID = "DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A",
                PrimaryID = "6C548EBE-59ED-40B8-BEBE-EDF79149295D",
                Secondary = "42D8D51F-6B22-4E7B-BC7F-CE0EA2E2814B,891F943E-D4D9-4660-872C-26366BB1C197" };
            //Act 
            var physicalLocationRows = _masterCategoryPhysicalLocationTestRepository.AllScreeningQuestions(h);

            //Assert
            Assert.IsNotNull(physicalLocationRows); // Test if null
            Assert.IsTrue(physicalLocationRows.Count() == 1);
        }
        //InsertUpdatePhysicalLocation

        [TestMethod]
        public void UpdatePhysicalLocation_ExitingOne_Retrun_Test()
        {
            var physicalLocationUpdate = new PrimaryPhysicallocation
            {
              
                PrimaryID = "6C548EBE-59ED-40B8-BEBE-EDF79149295D",
                BusinessMustbeinDC = "Yes",
                CofORequired = "Yes",
                HOP_EHOPAllowed = "No",
                ExemptfromAllFees = "No",
                LicenseType = "B",
                Status = true
            };
            //Act 
            var physicalLocationRows = _masterCategoryPhysicalLocationTestRepository.InsertUpdatePhysicalLocation(physicalLocationUpdate);

            //Assert
            Assert.IsNotNull(physicalLocationRows); // Test if null
            Assert.IsTrue(physicalLocationRows == "2");
        }

        [TestMethod]
        public void InsertPhysicalLocation_Retrun_Test()
        {
            var physicalLocationInsert = new PrimaryPhysicallocation
            {

                PrimaryID = "6C548EBE-59ED-40B8-BEBE-EDF79149295D24",
                BusinessMustbeinDC = "Yes",
                CofORequired = "Yes",
                HOP_EHOPAllowed = "No",
                ExemptfromAllFees = "No",
                LicenseType = "B",
                Status = true
            };
            //Act 
            var physicalLocationRows = _masterCategoryPhysicalLocationTestRepository.InsertUpdatePhysicalLocation(physicalLocationInsert);

            //Assert
            Assert.IsNotNull(physicalLocationRows); // Test if null
            Assert.IsTrue(physicalLocationRows == "1");
        }


        [TestMethod]
        public void AllScreeningQuestions_Retrun_WithOutHaveUnitOneAndTwo_Test()
        {
            var h = new SubmissionApplication { ActivityID = "668F4B68-8437-431F-9052-60809E556028", PrimaryID = "16FD71A3-7DD7-4B65-A006-777691BCB7FC", Secondary = "" };
            //Act 
            var physicalLocationRows = _masterCategoryPhysicalLocationTestRepository.AllScreeningQuestions(h);

            //Assert
            Assert.IsNotNull(physicalLocationRows); // Test if null
            Assert.IsTrue(physicalLocationRows.Count() == 1);
        }

        [TestMethod]
        public void AllScreeningQuestions_Retrun_CheckingWithSolicitor_Test()
        {
            var h = new SubmissionApplication { ActivityID = "1ED33685-B54E-4D7A-BBCE-1C93BD762CB1", PrimaryID = "26E9FC09-2763-4751-B880-EC4E805DF281" };
            //Act 
            var physicalLocationRows = _masterCategoryPhysicalLocationTestRepository.AllScreeningQuestions(h);

            //Assert
            Assert.IsNotNull(physicalLocationRows); // Test if null
            Assert.IsTrue(physicalLocationRows.Count() == 1);
        }
    }
}
