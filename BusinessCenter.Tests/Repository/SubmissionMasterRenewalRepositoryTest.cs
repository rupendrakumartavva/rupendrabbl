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
    public class SubmissionMasterRenewalRepositoryTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Repository declaration
        private SubmissionMasterRenewalRepository _submissionMasterRenewalTestRepository;
        private MasterRenewalStatusFeeRepository _masterrenewalstatusfee;


        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _masterrenewalstatusfee = new MasterRenewalStatusFeeRepository(_testUnitOfWork);
            //SubmissionMasterRenewalRepository Initialization
            _submissionMasterRenewalTestRepository = new SubmissionMasterRenewalRepository(_testUnitOfWork, _masterrenewalstatusfee);



            //Setup  SubmissionMasterRenewal FackData
            var testData = new SubmissionMasterRenewalData();
            _testDbContext.SubmissionMasterRenewal.AddRange(testData.MasterRenewalEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  MasterRenewalStatusFee FackData
            var renewalStatusFeeData = new MasterRenewalStatusFeeData();
            _testDbContext.MasterRenewalStatusFee.AddRange(renewalStatusFeeData.MasterRenewalStatusFeeEntitiesList);
            _testDbContext.SaveChanges();

        }
        [TestMethod]
        public void GetFindById_Return_Test()
        {
            var renewalEntityRows = _submissionMasterRenewalTestRepository.FindByID("8c6deecc-3b72-485d-8af5-30939af94e97");

            Assert.IsNotNull(renewalEntityRows);
            Assert.IsTrue(renewalEntityRows.Count() == 1);
        }

        [TestMethod]
        public void InsertRenewalDetails_Return_Test()
        {
            //Initialize Entites
            var renewModelEntity = new RenewModel
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                LrenNumber = "LREN15024280",
                CorpNumber = "C782151",
                IsCorpRegistration=false,
                ExpiredAmount = 0,
                LapsedAmount=58855,
                Extradays = "Lapsed"
            };

            //Act 
            var renewalEntityRows = _submissionMasterRenewalTestRepository.InsertRenewalDetails(renewModelEntity);

            //Assert
            Assert.IsNotNull(renewalEntityRows);
            Assert.IsTrue(renewalEntityRows == "true");
        }
        [TestMethod]
        public void InsertRenewalDetails_CheckNulls_Return_Test()
        {
            //Initialize Entites
            var renewModelEntity = new RenewModel
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                IsCorpRegistration = false,
               Extradays = "Lapsed"
            };

            //Act 
            var renewalEntityRows = _submissionMasterRenewalTestRepository.InsertRenewalDetails(renewModelEntity);

            //Assert
            Assert.IsNotNull(renewalEntityRows);
            Assert.IsTrue(renewalEntityRows == "true");
        }
        [TestMethod]
        public void UpdateRenewalDetails_Return_Test()
        {
            //Initialize Entites
            var renewModelEntity = new RenewModel
            {
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
                LrenNumber = "LREN15024280",
                CorpNumber = "C782151",
                IsCorpRegistration = false,
              ExpiredAmount = 0,
                LapsedAmount=58855,
                Extradays = "Lapsed"
            };

            //Act 
            var renewalEntityRows = _submissionMasterRenewalTestRepository.InsertRenewalDetails(renewModelEntity);

            //Assert
            Assert.IsNotNull(renewalEntityRows);
            Assert.IsTrue(renewalEntityRows == "Change");
        }
        [TestMethod]
        public void DeleteMasterRenewal_Return_Test()
        {
            //Act 
            var renewalEntityRows = _submissionMasterRenewalTestRepository.DeleteMasterRenewal("8c6deecc-3b72-485d-8af5-30939af94e97");

            //Assert
            Assert.IsNotNull(renewalEntityRows);
            Assert.IsTrue(renewalEntityRows);
        }
        [TestMethod]
        public void updateIscorpStatus_Return_Test()
        {
            //Act 
            var renewalEntityRows = _submissionMasterRenewalTestRepository.updateIscorpStatus(true, "8c6deecc-3b72-485d-8af5-30939af94e97");

            //Assert
            Assert.IsNotNull(renewalEntityRows);
            Assert.IsTrue(renewalEntityRows);
        }
        [TestMethod]
        public void InsertRenewalDetails_Return_expired_Test()
        {
            //Initialize Entites
            var renewModelEntity = new RenewModel
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                LrenNumber = "LREN15024280",
                CorpNumber = "C782151",
                IsCorpRegistration = false,
                ExpiredAmount = 0,
                LapsedAmount = 58855,
                Extradays = "Expired"
            };

            //Act 
            var renewalEntityRows = _submissionMasterRenewalTestRepository.InsertRenewalDetails(renewModelEntity);

            //Assert
            Assert.IsNotNull(renewalEntityRows);
            Assert.IsTrue(renewalEntityRows == "true");
        }

        [TestMethod]
        public void UpdateRenewalDetails_Return_expired_Test()
        {
            //Initialize Entites
            var renewModelEntity = new RenewModel
            {
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
                LrenNumber = "LREN15024280",
                CorpNumber = "900175",
                IsCorpRegistration = false,
                ExpiredAmount = 0,
                LapsedAmount = 58855,
                Extradays = "Expired",
                IsCorp=false
            };

            //Act 
            var renewalEntityRows = _submissionMasterRenewalTestRepository.InsertRenewalDetails(renewModelEntity);

            //Assert
            Assert.IsNotNull(renewalEntityRows);
            Assert.IsTrue(renewalEntityRows == "true");
        }
    }
}
