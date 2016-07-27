using BusinessCenter.Common;
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
    public class MasterTaxRevenueRepositoryTest
    {


        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
    //    private MasterTaxRevenueRepository _fixedFeeTestRepository;

        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
           // _fixedFeeTestRepository = new MasterTaxRevenueRepository(_testUnitOfWork);



            //Setup Data
            var testData1 = new MasterTaxRevenueRepositoryData();
            _testDbContext.MasterTaxRevenue.AddRange(testData1.MasterTaxRevenueEntitiesList);
            _testDbContext.SaveChanges();

        }

        [TestMethod]
        public void GetAllMasterTaxRevenueEntitiesTest()
        {

            //Act 
            var fixedFeeRows = _fixedFeeTestRepository.GetAll();

            //Assert
            Assert.IsNotNull(fixedFeeRows); // Test if null
            Assert.IsTrue(fixedFeeRows.Count() == 4);
        }

        [TestMethod]
        public void GetValidateTaxNumber_WithStatus_Test()
        {
            //
            var submissionTaxRevenu = new SubmissionTaxRevenuEntity { TaxRevenueFfin = "11-1111111", TaxRevenueType = "FEIN" };

            //Act 
            var fixedFeeRows = _fixedFeeTestRepository.ValidateFEINNumber(submissionTaxRevenu);

            //Assert
            Assert.IsNotNull(fixedFeeRows); // Test if null
            Assert.IsTrue(fixedFeeRows == "TRUE");
        }
         [TestMethod]
        public void GetValidateTaxRevenueNumber_WithWrongTaxNumber_Test()
        {
            //
            var submissionTaxRevenu = new SubmissionTaxRevenuEntity { TaxRevenueFfin = "11-11111112", TaxRevenueType = "FEIN" };

            //Act 
            var fixedFeeRows = _fixedFeeTestRepository.ValidateFEINNumber(submissionTaxRevenu);

            //Assert
            Assert.IsNotNull(fixedFeeRows); // Test if null
            Assert.IsTrue(fixedFeeRows == "NODATA");
        }

         [TestMethod]
         public void GetValidateTaxRevenueNumber_WithWrongTaxRevenueType_Test()
         {
             //
             var submissionTaxRevenu = new SubmissionTaxRevenuEntity { TaxRevenueFfin = "11-1111111", TaxRevenueType = "SSIN" };

             //Act 
             var fixedFeeRows = _fixedFeeTestRepository.ValidateFEINNumber(submissionTaxRevenu);

             //Assert
             Assert.IsNotNull(fixedFeeRows); // Test if null
             Assert.IsTrue(fixedFeeRows == "NODATA");
         }

         [TestMethod]
         public void GetValidateTaxRevenueNumber_WithWrongBothData_Test()
         {
             //
             var submissionTaxRevenu = new SubmissionTaxRevenuEntity { TaxRevenueFfin = "11-11111112", TaxRevenueType = "SSIN" };

             //Act 
             var fixedFeeRows = _fixedFeeTestRepository.ValidateFEINNumber(submissionTaxRevenu);

             //Assert
             Assert.IsNotNull(fixedFeeRows); // Test if null
             Assert.IsTrue(fixedFeeRows == "NODATA");
         }


         [TestMethod]
         public void GetValidateTaxNumber_WithStatus_Renewal_Test()
         {
             //
             var submissionTaxRevenu = new RenewModel { TaxNumber = "111-11-1234" };

             //Act 
             var fixedFeeRows = _fixedFeeTestRepository.ValidateTaxNumber(submissionTaxRevenu);

             //Assert
             Assert.IsNotNull(fixedFeeRows); // Test if null
             Assert.IsTrue(fixedFeeRows == "TRUE");
         }


         [TestMethod]
         public void GetValidateTaxNumber_WithStatus_Renewal_TaxNumberNotInSystem_Test()
         {
             //
             var submissionTaxRevenu = new RenewModel { TaxNumber = "111-11-12347" };

             //Act 
             var fixedFeeRows = _fixedFeeTestRepository.ValidateTaxNumber(submissionTaxRevenu);

             //Assert
             Assert.IsNotNull(fixedFeeRows); // Test if null
             Assert.IsTrue(fixedFeeRows == "NODATA");
         }
         [TestMethod]
         public void GetValidateTaxNumber_WithStatus_Renewal_TaxNumberNotInSystem1_Test()
         {
             //
             var submissionTaxRevenu = new RenewModel { TaxNumber = "56-0832111" };

             //Act 
             var fixedFeeRows = _fixedFeeTestRepository.ValidateTaxNumber(submissionTaxRevenu);

             //Assert
             Assert.IsNotNull(fixedFeeRows); // Test if null
             Assert.IsTrue(fixedFeeRows == "");
         }

        //ValidateFEINNumber

//        private readonly List<MasterTaxRevenue> list = new List<MasterTaxRevenue>()
//        {
//            new MasterTaxRevenue(){
//            TaxRevenueId = 1,
//            TextFEINNumber = "11-1111111",
//            Type = "FEIN",
//            IsCleanHands = true,
//},
//  new MasterTaxRevenue(){
//            TaxRevenueId = 2,
//            TextFEINNumber = "22-1234567",
//            Type = "FEIN",
//            IsCleanHands = true,
//}
//        };
//        private readonly Mock<IMasterTaxRevenueRepository> mockRepo = new Mock<IMasterTaxRevenueRepository>();
//        private readonly SubmissionTaxRevenuEntity submissionTaxRevenuEntity = new SubmissionTaxRevenuEntity();
//        private readonly RenewModel renewModel = new RenewModel()
//        {
//            SubCategoryName = "Inn and Motel",
//            CategoryName = "General Contractor/Construction Manager",
//            ActivityName = "Parking Facility",
//            Endoresement = "General Service and Repair",
//            CategoryId = "36ac8c39-fe45-46b5-87ec-ba7cfd14675d",
//            ActivityId = "D96ABBB9-7437-4476-BBA6-72EF6A94F327",
//            SubCategoryID = "FF0C5A9C-86B4-4378-BA8F-C1CE165B814E",
//            LicenseAmount = 65,
//            EndorsementFee = 25,
//            ApplicationFee = 25,
//            TechFee = 25,
//            RAOFee = 25,
//        };

//        [TestMethod()]
//        public void ValidateFEINNumberTest()
//        {
//            mockRepo.Setup(m => m.ValidateFEINNumber(submissionTaxRevenuEntity)).Returns("true");
//            var runtimeOutput = mockRepo.Object.ValidateFEINNumber(submissionTaxRevenuEntity);
//            Assert.AreEqual<string>("true", runtimeOutput);
//        }
        
//        [TestMethod()]
//        public void ValidateTaxNumberTest()
//        {
//            mockRepo.Setup(m => m.ValidateTaxNumber(renewModel)).Returns("true");
//            var runtimeOutput = mockRepo.Object.ValidateTaxNumber(renewModel);
//            Assert.AreEqual<string>("true", runtimeOutput);
//        }
    }
}
