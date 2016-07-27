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
    public class OSubCategoryFeesRepositoryTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Repository declaration
        private OSubCategoryFeesRepository _osubcategoryFeesTestRepository;
        private MasterPrimaryCategoryRepository _masterPrimaryCategoryRepository;
        private MasterSecondaryLicenseCategoryRepository _mockMasterSecondaryLicenseCategoryRepository;
        private MasterCategoryDocumentRepository _mockMasterCategoryDocumentRepository;
        private MasterCategoryQuestionRepository _mockMasterCategoryQuestionRepository;


        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);


            //MasterSecondaryLicenseCategoryRepository Initialization
            _mockMasterSecondaryLicenseCategoryRepository = new MasterSecondaryLicenseCategoryRepository(_testUnitOfWork);

            //MasterCategoryDocumentRepository Initialization
            _mockMasterCategoryDocumentRepository = new MasterCategoryDocumentRepository(_testUnitOfWork);

            //MasterCategoryQuestionRepository Initialization
            _mockMasterCategoryQuestionRepository = new MasterCategoryQuestionRepository(_testUnitOfWork);

            //MasterPrimaryCategoryRepository Initialization
            _masterPrimaryCategoryRepository = new MasterPrimaryCategoryRepository(_testUnitOfWork, _mockMasterSecondaryLicenseCategoryRepository,
                _mockMasterCategoryDocumentRepository, 
                _mockMasterCategoryQuestionRepository);

            //OSubCategoryFeesRepository Initialization
            _osubcategoryFeesTestRepository = new OSubCategoryFeesRepository(_testUnitOfWork, _masterPrimaryCategoryRepository, _mockMasterSecondaryLicenseCategoryRepository);



            //Setup FackData OSub_Category_Fees
            var testOsubcategoryFeeData = new OSubCategoryFeesData();
            _testDbContext.OSub_Category_Fees.AddRange(testOsubcategoryFeeData.OSubCategoryFeesEntitiesList);
            _testDbContext.SaveChanges();

            //Setup FackData MasterPrimaryCategory
            var testPrimaryCategoryData = new MasterPrimaryCategoryRepositoryData();
            _testDbContext.MasterPrimaryCategory.AddRange(testPrimaryCategoryData.MasterPrimaryCategoryEntitiesList);
            _testDbContext.SaveChanges();

            //Setup FackData MasterSecondaryLicenseCategory
            var secondaryLicenseData = new MasterSecondaryLicenseCategoryRepositoryData();
            _testDbContext.MasterSecondaryLicenseCategory.AddRange(secondaryLicenseData.MasterSeconderyLicenseCategoryList);
            _testDbContext.SaveChanges();

            //Setup FackData MasterCategoryDocument
            var masterCategoryDocumentRepositoryData = new MasterCategoryDocumentRepositoryData();
            _testDbContext.MasterCategoryDocument.AddRange(masterCategoryDocumentRepositoryData.MasterCategoryDocumentList);
            _testDbContext.SaveChanges();


            //Setup FackData MasterCategoryQuestion
            var masterCategoryQuestionData = new MasterCategoryQuestionData();
            _testDbContext.MasterCategoryQuestion.AddRange(masterCategoryQuestionData.CategoryQuestionEntitiesList);
            _testDbContext.SaveChanges();
        }

        [TestMethod]
        public void GetAllCategoryFeesTest()
        {

            //Act 
            var categoryFeeRows = _osubcategoryFeesTestRepository.AllSubCategoryFees();

            //Assert
            Assert.IsNotNull(categoryFeeRows); // Test if null
            Assert.IsTrue(categoryFeeRows.Count() == 13);
        }

        [TestMethod]
        public void GetCategory_WithQuantityIsPassing_Return_Test()
        {

            //Act 
            var categoryFeeRows = _osubcategoryFeesTestRepository.FindByCateogry("APARTMENT", 1);

            //Assert
            Assert.IsNotNull(categoryFeeRows); // Test if null
           // Assert.IsTrue(categoryFeeRows.Count() == 0);
        }

        [TestMethod]
        public void GetCategory_WithQuantityIsPassingWithZero_Return_Test()
        {

            //Act 
            var categoryFeeRows = _osubcategoryFeesTestRepository.FindByCateogry("General Business Licenses", 0).ToList();

            //Assert
            Assert.IsNotNull(categoryFeeRows); // Test if null
            Assert.IsTrue(categoryFeeRows.Count() ==1);
        }

        [TestMethod]
        public void GetCategory_WithFindByDescription_Return_Test()
        {

            //Act 
            var categoryFeeRows = _osubcategoryFeesTestRepository.FindByCateogry("General Business Licenses", 0).ToList();

            //Assert
            Assert.IsNotNull(categoryFeeRows); // Test if null
            Assert.IsTrue(categoryFeeRows.Count() == 1);
        }

        [TestMethod]
        public void GetCategory_FindByDescription_Return_Test()
        {

            //Act 
            var categoryFeeRows = _osubcategoryFeesTestRepository.FindByDescription("General Business Licenses").ToList();

            //Assert
            Assert.IsNotNull(categoryFeeRows); // Test if null
            Assert.IsTrue(categoryFeeRows.Count() == 1);
        }
        //

        [TestMethod]
        public void GetCategory_WithFindFeesByPrimaryCategory_Return_Test()
        {

            //Act 
            var categoryFeeRows = _osubcategoryFeesTestRepository.FindFeesByPrimaryCategory("6C548EBE-59ED-40B8-BEBE-EDF79149295D").ToList();

            //Assert
            Assert.IsNotNull(categoryFeeRows); // Test if null
            Assert.IsTrue(categoryFeeRows.Count() == 1);
        }

        [TestMethod]
        public void GetCategory_WithFindFeesBySecondaryCategory_Return_Test()
        {

            //Act 
            var categoryFeeRows = _osubcategoryFeesTestRepository.FindFeesBySecondaryCategory("891F943E-D4D9-4660-872C-26366BB1C197").ToList();

            //Assert
            Assert.IsNotNull(categoryFeeRows); // Test if null
            Assert.IsTrue(categoryFeeRows.Count() == 6);
        }
        [TestMethod]
        public void GetFeeDetails_BasedOnCategoryNameWithFeeCodeAndItemNumber_Return_Test()
        {

            //Act 
            var categoryFeeRows = _osubcategoryFeesTestRepository.FindByFeeCode("Restaurant","T",51).ToList();

            //Assert
            Assert.IsNotNull(categoryFeeRows); // Test if null
            Assert.IsTrue(categoryFeeRows.Count() == 1);
        }
        //

        [TestMethod]
        public void GetFeeDetails_BasedOnCategoryFeeIdWithFindById_Return_Test()
        {

            //Act 
            var categoryFeeRows = _osubcategoryFeesTestRepository.FindByCategoryFeeId("5").ToList();

            //Assert
            Assert.IsNotNull(categoryFeeRows); // Test if null
            Assert.IsTrue(categoryFeeRows.Count() == 1);
        }

        [TestMethod]
        public void UpdateSubFee_BasedOnCategoryFeeIdWithFindById_Return_Test()
        {

            //Initial Model Details
            var primaryPhysicallocation = new PrimaryPhysicallocation { OldCategoryName = "Restaurant", Description="MyNew Restaurant" };

            //Act 
            var categoryFeeRows = _osubcategoryFeesTestRepository.UpdateSubFee(primaryPhysicallocation);

            //Assert
            Assert.IsNotNull(categoryFeeRows); // Test if null
            Assert.IsTrue(categoryFeeRows==true);
        }

       
       

        [TestMethod]
        public void InsertUpdatePrimaryCategory_AlreadyExists_AlongWithUpdateInsertOCategoryFee_Return_Test()
        {

            //Initial Model Details
            var addPrimaryCategory = new PrimaryPhysicallocation
            {
                // Primary Category
                PrimaryID = "",
                ActivityID = "C49EACE2-2180-430E-BA2F-8B891D659421",
                Description = "APARTMENT",
                Endorsement = "Hotel",
                CategoryCode = "4040",
                UnitOne = "NA",
                UnitTwo = "NA",
                App_Type = "B",

                OldUnitOne = "",
                OldUnitTwo = "",
                OldCategoryName = ""

            };

            //Act 
            var categoryFeeRows = _osubcategoryFeesTestRepository.InsertUpdatePrimaryCategory(addPrimaryCategory);

            //Assert
            Assert.IsNotNull(categoryFeeRows); // Test if null
            Assert.IsTrue(categoryFeeRows == "3");
        }
        [TestMethod]
        public void InsertUpdatePrimaryCategory_AlreadyExistsCategoryCode_AlongWithUpdateInsertOCategoryFee_Return_Test()
        {

            //Initial Model Details
            var addPrimaryCategory = new PrimaryPhysicallocation
            {
                // Primary Category
                PrimaryID = "",
                ActivityID = "C49EACE2-2180-430E-BA2F-8B891D659421",
                Description = "Bed and Breakfast",
                Endorsement = "Hotel",
                CategoryCode = "5107",
                UnitOne = "NA",
                UnitTwo = "NA",
                App_Type = "B",

                OldUnitOne = "",
                OldUnitTwo = "",
                OldCategoryName = ""

            };

            //Act 
            var categoryFeeRows = _osubcategoryFeesTestRepository.InsertUpdatePrimaryCategory(addPrimaryCategory);

            //Assert
            Assert.IsNotNull(categoryFeeRows); // Test if null
            Assert.IsTrue(categoryFeeRows == "4");
        }


        [TestMethod]
        public void InsertUpdatePrimaryCategory_InsertOCategoryFeeWithFeeCodeAsC_Return_Test()
        {

            //Initial Model Details
            var addPrimaryCategory = new PrimaryPhysicallocation
            {
                // Primary Category
                PrimaryID = "",
                ActivityID = "D96ABBB9-7437-4476-BBA6-72EF6A94F327",
                Description = "Hotel New Hotel",
                Endorsement = "Hotel",
                CategoryCode = "4045",
                UnitOne = "Rooms",
                UnitTwo = "NA",
                App_Type = "B",
                Fee_Code = "C",
                LicenseType = "B",
                License_Fee = (decimal) 200.00,
                Tier=5,
                UserQuestion1="How many Rooms in your restaurant",

                OldUnitOne = "",
                OldUnitTwo = "",
                OldCategoryName = ""

            };

            //Act 
            var categoryFeeRows = _osubcategoryFeesTestRepository.InsertUpdatePrimaryCategory(addPrimaryCategory);

            //Assert
            Assert.IsNotNull(categoryFeeRows); // Test if null
            Assert.IsTrue(categoryFeeRows != "0");
            //InsertUpdatePrimaryCategory
        }

        [TestMethod]
        public void InsertUpdatePrimaryCategory_InsertOCategoryFeeWithFeeCodeAsT_Return_Test()
        {

            //Initial Model Details
            var addPrimaryCategory = new PrimaryPhysicallocation
            {
                // Primary Category
                PrimaryID = "",
                ActivityID = "D96ABBB9-7437-4476-BBA6-72EF6A94F327",
                Description = "Hotel New Hotel",
                Endorsement = "Hotel",
                CategoryCode = "4045",
                UnitOne = "Rooms",
                UnitTwo = "NA",
                App_Type = "B",
                Fee_Code = "T",
                LicenseType = "B",
                License_Fee = (decimal)200.00,
                Tier = 5,
                UserQuestion1 = "How many Rooms in your restaurant",

                OldUnitOne = "",
                OldUnitTwo = "",
                OldCategoryName = ""

            };

            //Act 
            var categoryFeeRows = _osubcategoryFeesTestRepository.InsertUpdatePrimaryCategory(addPrimaryCategory);

            //Assert
            Assert.IsNotNull(categoryFeeRows); // Test if null
            Assert.IsTrue(categoryFeeRows != "0");
            //InsertUpdatePrimaryCategory
        }

        [TestMethod]
        public void InsertUpdatePrimaryCategory_InsertOCategoryFeeWithFeeCodeAscMissing_Return_Test()
        {

            //Initial Model Details
            var addPrimaryCategory = new PrimaryPhysicallocation
            {
                // Primary Category
                PrimaryID = "",
                ActivityID = "D96ABBB9-7437-4476-BBA6-72EF6A94F327",
                Description = "Hotel New Hotel",
                Endorsement = "Hotel",
                CategoryCode = "4045",
                UnitOne = "Rooms",
                UnitTwo = "NA",
                App_Type = "B",
             
                LicenseType = "B",
                License_Fee = (decimal)200.00,
                Tier = 5,
                UserQuestion1 = "How many Rooms in your restaurant",

                OldUnitOne = "",
                OldUnitTwo = "",
                OldCategoryName = ""

            };

            //Act 
            var categoryFeeRows = _osubcategoryFeesTestRepository.InsertUpdatePrimaryCategory(addPrimaryCategory);

            //Assert
            Assert.IsNotNull(categoryFeeRows); // Test if null
            Assert.IsTrue(categoryFeeRows != "0");
            //InsertUpdatePrimaryCategory
        }

        //InsertUpdateCategoryFees

        [TestMethod]
        public void InsertUpdatePrimaryCategory_InsertOSubCategoryCategoryFee_Return_Test()
        {

            //Initial Model Details
            var categoryfeeentity = new OSub_Category_FeesEntity
            {
               
                OSub_Category = "",
                OSub_Description = "Hotel",
                Fee_Code = "T",
                Start = 1,
                End = 10,
                License_Fee = (decimal?)450.0000,
                Tier = null,
                App_Type = "B",
                Status = true

            };
            //Act 
            var categoryFeeRows = _osubcategoryFeesTestRepository.InsertUpdateCategoryFees(categoryfeeentity);

            //Assert
            Assert.IsNotNull(categoryFeeRows); // Test if null
            Assert.IsTrue(categoryFeeRows ==1);
           
        }

        [TestMethod]
        public void UpdateOSubCategoryFee_Return_Test()
        {

            //Initial Model Details
            var categoryfeeentity = new OSub_Category_FeesEntity
            {

                OSub_Category = "2",
                OSub_Description = "Restaurant",
                Fee_Code = "T",
                Start = 1,
                End = 50,
                License_Fee = (decimal?)550.0000,
                Tier = null,
                App_Type = "B",
                Status = true

            };
            //Act 
            var categoryFeeRows = _osubcategoryFeesTestRepository.InsertUpdateCategoryFees(categoryfeeentity);

            //Assert
            Assert.IsNotNull(categoryFeeRows); // Test if null
            Assert.IsTrue(categoryFeeRows == 100);

        }

        [TestMethod]
        public void UpdateOSubCategoryFee_CategoryFee_WithOutChangingStartAndEndRangeUpdate_Return_Test()
        {

            //Initial Model Details
            var categoryfeeentity = new OSub_Category_FeesEntity
            {

                OSub_Category = "2",
                OSub_Description = "Restaurant",
                Fee_Code = "T",
                Start = 1,
                End = 10,
                License_Fee = (decimal?)475.0000,
                Tier = null,
                App_Type = "B",
                Status = true

            };
            //Act 
            var categoryFeeRows = _osubcategoryFeesTestRepository.InsertUpdateCategoryFees(categoryfeeentity);

            //Assert
            Assert.IsNotNull(categoryFeeRows); // Test if null
            Assert.IsTrue(categoryFeeRows == 100);

        }

        [TestMethod]
        public void UpdateOSubCategoryFee_CategoryFee_WithOutChangingStartAndEndRangeUpdate1_Return_Test()
        {

            //Initial Model Details
            var categoryfeeentity = new OSub_Category_FeesEntity
            {

                OSub_Category = "2",
                OSub_Description = "Restaurant",
                Fee_Code = "T",
                Start = 1,
                End = 100,
                License_Fee = (decimal?)475.0000,
                Tier = null,
                App_Type = "B",
                Status = true

            };
            //Act 
            var categoryFeeRows = _osubcategoryFeesTestRepository.InsertUpdateCategoryFees(categoryfeeentity);

            //Assert
            Assert.IsNotNull(categoryFeeRows); // Test if null
            Assert.IsTrue(categoryFeeRows == 100);

        }

        [TestMethod]
        public void UpdateOSubCategoryFee_CategoryFee_WithOutChangingStartAndEndRangeUpdate7_Return_Test()
        {

            //Initial Model Details
            var categoryfeeentity = new OSub_Category_FeesEntity
            {

                OSub_Category = "3",
                OSub_Description = "Restaurant",
                Fee_Code = "T",
                Start = 1,
                End = 99999,
                License_Fee = (decimal?)475.0000,
                Tier = null,
                App_Type = "B",
                Status = true

            };
            //Act 
            var categoryFeeRows = _osubcategoryFeesTestRepository.InsertUpdateCategoryFees(categoryfeeentity);

            //Assert
            Assert.IsNotNull(categoryFeeRows); // Test if null
            Assert.IsTrue(categoryFeeRows == 100);

        }


        [TestMethod]
        public void UpdateOSubCategoryFee_CategoryFee_WithOutChangingStartAndEndRangeUpdateException_Return_Test()
        {

            //Initial Model Details
            var categoryfeeentity = new OSub_Category_FeesEntity
            {

                OSub_Category = "4",
                OSub_Description = "Restaurant",
                Fee_Code = "TA",
                Start = 1001,
                End = 99999,
                License_Fee = (decimal?)475.0000,
                Tier = null,
                App_Type = "B",
                Status = true

            };
            //Act 
            var categoryFeeRows = _osubcategoryFeesTestRepository.InsertUpdateCategoryFees(categoryfeeentity);

            //Assert
            Assert.IsNotNull(categoryFeeRows); // Test if null
            Assert.IsTrue(categoryFeeRows == 100);

        }


        [TestMethod]
        public void UpdateOSubCategoryFee_CategoryFee_WithOutChangingStartAndUpdateTA_Return_Test()
        {

            //Initial Model Details
            var categoryfeeentity = new OSub_Category_FeesEntity
            {

                OSub_Category = "4",
                OSub_Description = "Restaurant",
                Fee_Code = "TA",
                Start = 101,
                End = 99999,
                License_Fee = (decimal?)475.0000,
                Tier = null,
                App_Type = "B",
                Status = true

            };
            //Act 
            var categoryFeeRows = _osubcategoryFeesTestRepository.InsertUpdateCategoryFees(categoryfeeentity);

            //Assert
            Assert.IsNotNull(categoryFeeRows); // Test if null
            Assert.IsTrue(categoryFeeRows == 100);

        }

        [TestMethod]
        public void UpdateOSubCategoryFee_CategoryFee__ReturnTAToT_Test()
        {

            //Initial Model Details
            var categoryfeeentity = new OSub_Category_FeesEntity
            {

                OSub_Category = "7",
                OSub_Description = "Food Vending Machine",
                Fee_Code = "T",
                Start = 0,
                End = 99999,
                License_Fee = (decimal?)675.0000,
                Tier = null,
                App_Type = "B",
                Status = true

            };
            //Act 
            var categoryFeeRows = _osubcategoryFeesTestRepository.InsertUpdateCategoryFees(categoryfeeentity);

            //Assert
            Assert.IsNotNull(categoryFeeRows); // Test if null
            Assert.IsTrue(categoryFeeRows ==100);

        }
    }
}
