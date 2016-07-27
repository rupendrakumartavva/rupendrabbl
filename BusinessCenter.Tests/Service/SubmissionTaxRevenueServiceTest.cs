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
   public class SubmissionTaxRevenueServiceTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Repository declaration
        private SubmissionTaxRevenueRepository _submissionTaxRevenueTestRepository;
        private SubmissionMasterApplicationChcekListRepository _submissionMasterApplicationChcekListRepository;

        //Service declaration
        private SubmissionTaxRevenueService _submissionTaxRevenueTestService;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);

            //SubmissionMasterApplicationChcekListRepository Initialization
            _submissionMasterApplicationChcekListRepository = new SubmissionMasterApplicationChcekListRepository(_testUnitOfWork);

            //SubmissionTaxRevenueRepository Initialization
            _submissionTaxRevenueTestRepository = new SubmissionTaxRevenueRepository(_testUnitOfWork, _submissionMasterApplicationChcekListRepository);

            //SubmissionTaxRevenueService Initialization
            _submissionTaxRevenueTestService = new SubmissionTaxRevenueService(_submissionTaxRevenueTestRepository);


            //Setup SubmissionTaxRevenue FackData Initialization
            var testData = new SubmissionTaxRevenueData();
            _testDbContext.SubmissionTaxRevenue.AddRange(testData.SubmissionTaxRevenueEntitiesList);
            _testDbContext.SaveChanges();

        }
        [TestMethod]
        public void Find_SubmissionTaxAndRevenue_SendMasterIdWithClass_Return_Data()
        {

            //EntityData is Added
            var taxandRevenueEntity = new SubmissionTaxRevenuEntity { MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40" };

            //Act 
            var submissionTaxAndRevenueRow = _submissionTaxRevenueTestService.FindByTaxRevenueNumber(taxandRevenueEntity);

            //Assert
            Assert.IsNotNull(submissionTaxAndRevenueRow); // Test if null
            Assert.IsTrue(submissionTaxAndRevenueRow.Count() == 1);
        }
        [TestMethod]
        public void InsertUpdate_SubmissionTaxAndRevenue_WithEmptyTaxNumber_Return_Data()
        {
            //EntityData is Added
            var taxandRevenueEntity = new SubmissionTaxRevenuEntity
            {
                SubmissionTaxRevenueId = 1,
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                TaxRevenueType = "FEIN",
                FullName = "Code IT INDIA",
                BusinessOwnerRoles = "Owner",

            };

            //Act 
            var submissionTaxAndRevenueRow = _submissionTaxRevenueTestService.SubmissionTaxRevenuInsertUpdate(taxandRevenueEntity);

            //Assert
            Assert.IsNotNull(submissionTaxAndRevenueRow); // Test if null
            Assert.IsTrue(submissionTaxAndRevenueRow == false);
        }
        [TestMethod]
        public void InsertUpdate_SubmissionTaxAndRevenue_WithEmptyMasterId_Return_Data()
        {
            //EntityData is Added
            var taxandRevenueEntity = new SubmissionTaxRevenuEntity
            {
                SubmissionTaxRevenueId = 1,
                TaxRevenueFfin = "11-1111111",
                TaxRevenueType = "FEIN",
                FullName = "Code IT INDIA",
                BusinessOwnerRoles = "Owner",

            };

            //Act 
            var submissionTaxAndRevenueRow = _submissionTaxRevenueTestService.SubmissionTaxRevenuInsertUpdate(taxandRevenueEntity);

            //Assert
            Assert.IsNotNull(submissionTaxAndRevenueRow); // Test if null
            Assert.IsTrue(submissionTaxAndRevenueRow == false);
        }
        [TestMethod]
        public void Insert_SubmissionTaxAndRevenue_FullDetails_Return_Data()
        {
            //EntityData is Added
            var taxandRevenueEntity = new SubmissionTaxRevenuEntity
            {
                SubmissionTaxRevenueId = 1,
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40-i",
                TaxRevenueFfin = "11-1111111",
                TaxRevenueType = "FEIN",
                FullName = "Code IT INDIA",
                BusinessOwnerRoles = "Owner",

            };

            //Act 
            var submissionTaxAndRevenueRow = _submissionTaxRevenueTestService.SubmissionTaxRevenuInsertUpdate(taxandRevenueEntity);

            //Assert
            Assert.IsNotNull(submissionTaxAndRevenueRow); // Test if null
            Assert.IsTrue(submissionTaxAndRevenueRow);
        }
        [TestMethod]
        public void Updatate_SubmissionTaxAndRevenue_FullDetails_Return_Data()
        {
            //EntityData is Added
            var taxandRevenueEntity = new SubmissionTaxRevenuEntity
            {
                SubmissionTaxRevenueId = 1,
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                TaxRevenueFfin = "11-1111111",
                TaxRevenueType = "FEIN",
                FullName = "Code IT INDIA",
                BusinessOwnerRoles = "Owner",

            };

            //Act 
            var submissionTaxAndRevenueRow = _submissionTaxRevenueTestService.SubmissionTaxRevenuInsertUpdate(taxandRevenueEntity);

            //Assert
            Assert.IsNotNull(submissionTaxAndRevenueRow); // Test if null
            Assert.IsTrue(submissionTaxAndRevenueRow);
        }
        [TestMethod]
        public void Delete_SubmissionTaxAndRevenue_FullDetails_Return_Data()
        {
            //EntityData is Added
            var taxandRevenueEntity = new SubmissionTaxRevenuEntity
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40"
            };

            //Act 
            var submissionTaxAndRevenueRow = _submissionTaxRevenueTestService.DeleteSubmissionTaxandRevenue(taxandRevenueEntity);

            //Assert
            Assert.IsNotNull(submissionTaxAndRevenueRow); // Test if null
            Assert.IsTrue(submissionTaxAndRevenueRow);
        }
        [TestMethod]
        public void DeleteTaxandRevenue_Return_Test()
        {
           

            //Act 
            var submissionTaxAndRevenueRow = _submissionTaxRevenueTestService.DeleteTaxandRevenue("cce0b056-d2a3-485e-a02a-e34c957c4e40");

            //Assert
            Assert.IsNotNull(submissionTaxAndRevenueRow); // Test if null
            Assert.IsTrue(submissionTaxAndRevenueRow);
        }
        [TestMethod]
        public void FindByID_Return_Test()
        {

            //Act 
            var submissionTaxAndRevenueRow = _submissionTaxRevenueTestService.FindByID("cce0b056-d2a3-485e-a02a-e34c957c4e40").ToList();

            //Assert
            Assert.IsNotNull(submissionTaxAndRevenueRow); // Test if null
            Assert.IsTrue(submissionTaxAndRevenueRow.Count() == 1);
        }
        [TestMethod]
        public void UpdateTaxAndRevenueTest()
        {


            //Act 
            var submissionTaxAndRevenueRow = _submissionTaxRevenueTestService.TaxAndRevenueUpdate("cce0b056-d2a3-485e-a02a-e34c957c4e40");

            //Assert
            Assert.IsNotNull(submissionTaxAndRevenueRow); // Test if null
            Assert.IsTrue(submissionTaxAndRevenueRow == true);
        }
    }
}
