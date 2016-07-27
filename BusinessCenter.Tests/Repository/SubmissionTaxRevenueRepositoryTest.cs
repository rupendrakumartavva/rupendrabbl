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
    public class SubmissionTaxRevenueRepositoryTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Repository declaration
        private SubmissionTaxRevenueRepository _submissionTaxRevenueTestRepository;
        private SubmissionMasterApplicationChcekListRepository _submissionMasterApplicationChcekListRepository;


        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);


            _submissionMasterApplicationChcekListRepository=new SubmissionMasterApplicationChcekListRepository(_testUnitOfWork);
            _submissionTaxRevenueTestRepository = new SubmissionTaxRevenueRepository(_testUnitOfWork, _submissionMasterApplicationChcekListRepository);


            //Setup SubmissionTaxRevenue FackData Initialization
            var testData = new SubmissionTaxRevenueData();
            _testDbContext.SubmissionTaxRevenue.AddRange(testData.SubmissionTaxRevenueEntitiesList);
            _testDbContext.SaveChanges();

        }

        [TestMethod]
        public void Find_SubmissionTaxAndRevenue_WithMasterId_Return_Data()
        {

            //Act 
            var submissionTaxAndRevenueRow = _submissionTaxRevenueTestRepository.FindByID("cce0b056-d2a3-485e-a02a-e34c957c4e40");

            //Assert
            Assert.IsNotNull(submissionTaxAndRevenueRow); // Test if null
            Assert.IsTrue(submissionTaxAndRevenueRow.Count() == 1);
        }

        [TestMethod]
        public void Find_SubmissionTaxAndRevenue_SendMasterIdWithClass_Return_Data()
        {

            //EntityData is Added
            var taxandRevenueEntity = new SubmissionTaxRevenuEntity {MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40"};

            //Act 
            var submissionTaxAndRevenueRow = _submissionTaxRevenueTestRepository.FindByTaxRevenueNumber(taxandRevenueEntity);

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
            var submissionTaxAndRevenueRow = _submissionTaxRevenueTestRepository.SubmissionTaxRevenuInsertUpdate(taxandRevenueEntity);

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
            var submissionTaxAndRevenueRow = _submissionTaxRevenueTestRepository.SubmissionTaxRevenuInsertUpdate(taxandRevenueEntity);

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
            var submissionTaxAndRevenueRow = _submissionTaxRevenueTestRepository.SubmissionTaxRevenuInsertUpdate(taxandRevenueEntity);

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
            var submissionTaxAndRevenueRow = _submissionTaxRevenueTestRepository.SubmissionTaxRevenuInsertUpdate(taxandRevenueEntity);

            //Assert
            Assert.IsNotNull(submissionTaxAndRevenueRow); // Test if null
            Assert.IsTrue(submissionTaxAndRevenueRow);
        }

        [TestMethod]
        public void Delete_SubmissionTaxAndRevenue_FullDetails_Return_Data()
        {
           

            //Act 
            var submissionTaxAndRevenueRow = _submissionTaxRevenueTestRepository.DeleteSubmissionTaxandRevenue("cce0b056-d2a3-485e-a02a-e34c957c4e40");

            //Assert
            Assert.IsNotNull(submissionTaxAndRevenueRow); // Test if null
            Assert.IsTrue(submissionTaxAndRevenueRow);
        }
        [TestMethod]
        public void Delete_SubmissionTaxAndRevenue_NotMatchMasterId_Return_Data()
        {


            //Act 
            var submissionTaxAndRevenueRow = _submissionTaxRevenueTestRepository.DeleteSubmissionTaxandRevenue("cce0b056-d2a3-485e-a02a-e34c957c4e40-1");

            //Assert
            Assert.IsNotNull(submissionTaxAndRevenueRow); // Test if null
            Assert.IsTrue(submissionTaxAndRevenueRow==true);
        }
        [TestMethod]
        public void UpdateTaxAndRevenue_Return_True_Test()
        {


            //Act 
            var submissionTaxAndRevenueRow = _submissionTaxRevenueTestRepository.UpdateTaxAndRevenue("cce0b056-d2a3-485e-a02a-e34c957c4e40");

            //Assert
            Assert.IsNotNull(submissionTaxAndRevenueRow); // Test if null
            Assert.IsTrue(submissionTaxAndRevenueRow == true);
        }
        [TestMethod]
        public void UpdateTaxAndRevenue_Return_False_Test()
        {


            //Act 
            var submissionTaxAndRevenueRow = _submissionTaxRevenueTestRepository.UpdateTaxAndRevenue("cce0b056-d2a3-485e-a02a-e34c957c4e40-1");

            //Assert
            Assert.IsNotNull(submissionTaxAndRevenueRow); // Test if null
            Assert.IsTrue(submissionTaxAndRevenueRow == false);
        }

    }
}
