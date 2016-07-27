using BusinessCenter.Common;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
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
  public  class MasterTaxRevenueServiceTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
       // private MasterTaxRevenueRepository _masterTaxRevenueTestRepository;

       // private MasterTaxRevenueService _masterTaxRevenueTestService;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
           // _masterTaxRevenueTestRepository = new MasterTaxRevenueRepository(_testUnitOfWork);

           // _masterTaxRevenueTestService = new MasterTaxRevenueService(_masterTaxRevenueTestRepository);

            //Setup Data
            var taxRevenueData = new MasterTaxRevenueRepositoryData();
            _testDbContext.MasterTaxRevenue.AddRange(taxRevenueData.MasterTaxRevenueEntitiesList);
            _testDbContext.SaveChanges();

        }
        [TestMethod]
        public void GetValidateTaxNumber_WithStatus_Test()
        {
            //
            var submissionTaxRevenu = new SubmissionTaxRevenuEntity { TaxRevenueFfin = "11-1111111", TaxRevenueType = "FEIN" };

            //Act 
            var taxRevenuRows = _masterTaxRevenueTestService.ValidateFEINNumber(submissionTaxRevenu);

            //Assert
            Assert.IsNotNull(taxRevenuRows); // Test if null
            Assert.IsTrue(taxRevenuRows == "TRUE");
        }
        [TestMethod]
        public void GetValidateTaxRevenueNumber_WithWrongTaxNumber_Test()
        {
            //
            var submissionTaxRevenu = new SubmissionTaxRevenuEntity { TaxRevenueFfin = "11-11111112", TaxRevenueType = "FEIN" };

            //Act 
            var taxRevenuRows = _masterTaxRevenueTestService.ValidateFEINNumber(submissionTaxRevenu);

            //Assert
            Assert.IsNotNull(taxRevenuRows); // Test if null
            Assert.IsTrue(taxRevenuRows == "NODATA");
        }
        [TestMethod]
        public void GetValidateTaxRevenueNumber_WithWrongTaxRevenueType_Test()
        {
            //
            var submissionTaxRevenu = new SubmissionTaxRevenuEntity { TaxRevenueFfin = "11-1111111", TaxRevenueType = "SSIN" };

            //Act 
            var taxRevenuRows = _masterTaxRevenueTestService.ValidateFEINNumber(submissionTaxRevenu);

            //Assert
            Assert.IsNotNull(taxRevenuRows); // Test if null
            Assert.IsTrue(taxRevenuRows == "NODATA");
        }

        [TestMethod]
        public void GetValidateTaxRevenueNumber_WithWrongBothData_Test()
        {
            //
            var submissionTaxRevenu = new SubmissionTaxRevenuEntity { TaxRevenueFfin = "11-11111112", TaxRevenueType = "SSIN" };

            //Act 
            var taxRevenuRows = _masterTaxRevenueTestService.ValidateFEINNumber(submissionTaxRevenu);

            //Assert
            Assert.IsNotNull(taxRevenuRows); // Test if null
            Assert.IsTrue(taxRevenuRows == "NODATA");
        }
    }
}
