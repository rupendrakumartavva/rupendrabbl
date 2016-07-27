using System;
using System.Collections.Generic;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Tests.Setup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Common;
using BusinessCenter.Data;
namespace BusinessCenter.Tests.Repository
{
      [TestClass]
    public class SubmissionSelfCertificationRepositoryTest
    {
        //Initialization DBConnection
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Declaration  of Repositories
        private SubmissionSelfCertificationRepository _submissionSelfCertificationTestRepository;
        private SubmissionMasterApplicationChcekListRepository _submissionMasterApplicationChcekListTestRepository;

        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);

            //SubmissionMasterApplicationChcekListRepository Initialization
            _submissionMasterApplicationChcekListTestRepository = new SubmissionMasterApplicationChcekListRepository(_testUnitOfWork);

            //SubmissionSelfCertificationRepository Initialization
            _submissionSelfCertificationTestRepository = new SubmissionSelfCertificationRepository(_testUnitOfWork, _submissionMasterApplicationChcekListTestRepository);

            //Setup  SubmissionMaster_ApplicationCheckList FackData Initialization
            var ApplicationChcekListRepositoryData = new SubmissionMasterApplicationChcekListRepositoryData();
            _testDbContext.SubmissionMaster_ApplicationCheckList.AddRange(ApplicationChcekListRepositoryData.SubmissionMaster_ApplicationCheckListEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  SubmissionSelfCertification FackData Initialization
            var SubmissionSelfCertificationData = new SubmissionSelfCertificationData();
            _testDbContext.SubmissionSelfCertification.AddRange(SubmissionSelfCertificationData.SelfCertificationEntitiesList);
            _testDbContext.SaveChanges();
        }
        [TestMethod]
        public void GetSelfCertificationDetails_Returns_Test()
        {
            //Initialize Entites
            // var generalBusiness = new GeneralBusiness { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97" };

            //Act
            var selfCertificationRows = _submissionSelfCertificationTestRepository.GetSelfCertificationDetails();

            //Assert
            Assert.IsNotNull(selfCertificationRows); // Test if null
            // Assert.IsTrue(selfCertificationRows == 1);
        }
        [TestMethod]
        public void GetSelfCertificationOnMasterIds_Returns_Test()
        {
            //Initialize Entites
            var submissionSelfCertification = new SubmissionSelfCertification { MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40" };

            //Act
            var selfCertificationRows = _submissionSelfCertificationTestRepository.GetSelfCertificationOnMasterId(submissionSelfCertification);

            //Assert
            Assert.IsNotNull(selfCertificationRows); // Test if null
            //Assert.IsTrue(selfCertificationRows==1);
        }
        [TestMethod]
        public void DeleteSelfCertification_Returns_True_Test()
        {
            //Initialize Entites
            var submissionSelfCertification = new SubmissionSelfCertification
            {
                SelfCertificationId = "bfabb7c4-38e7-4225-b611-9eedede0f4c6",
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                MasterCategoryId = "FF0C5A9C-86B4-4378-BA8F-C1CE165B814E",
                IsPropertyOccupied = false,
                FullName = "Codeit Development",
                SelfCertificationOn = System.DateTime.Now,
                CreatedDate = System.DateTime.Now,
                UpdatedDate = System.DateTime.Now
            };

            //Act
            var selfCertificationRows = _submissionSelfCertificationTestRepository.DeleteSelfCertification(submissionSelfCertification);

            //Assert
            Assert.IsNotNull(selfCertificationRows); // Test if null
            Assert.IsTrue(selfCertificationRows == true);
        }
        [TestMethod]
        public void DeleteSelfCertification_Returns_False_Test()
        {
            //Initialize Entites
            var submissionSelfCertification = new SubmissionSelfCertification { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97" };

            //Act
            var selfCertificationRows = _submissionSelfCertificationTestRepository.DeleteSelfCertification(submissionSelfCertification);

            //Assert
            Assert.IsNotNull(selfCertificationRows); // Test if null
            Assert.IsTrue(selfCertificationRows == false);
        }
        [TestMethod]
        public void InsertUpdateSelfCertification_Returns_Test()
        {
            //Initialize Entites
            var submissionSelfCertification = new SubmissionSelfCertification { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97" };

            //Act
            var selfCertificationRows = _submissionSelfCertificationTestRepository.InsertUpdateSelfCertification(submissionSelfCertification);

            //Assert
            Assert.IsNotNull(selfCertificationRows); // Test if null
            Assert.IsTrue(selfCertificationRows == true);
        }
        [TestMethod]
        public void UpdateSelfCertification_Returns_Test()
        {
            //Initialize Entites
            var submissionSelfCertification = new SubmissionSelfCertification { MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40", SelfCertificationId = "bfabb7c4-38e7-4225-b611-9eedede0f4c6" };

            //Act
            var selfCertificationRows = _submissionSelfCertificationTestRepository.InsertUpdateSelfCertification(submissionSelfCertification);

            //Assert
            Assert.IsNotNull(selfCertificationRows); // Test if null
            Assert.IsTrue(selfCertificationRows == true);
        }
        [TestMethod]
        public void UpdateSelfCertificationTest()
        {
           

            //Act
            var selfCertificationRows = _submissionSelfCertificationTestRepository.UpdateSelfCertification("cce0b056-d2a3-485e-a02a-e34c957c4e40");

            //Assert
            Assert.IsNotNull(selfCertificationRows); // Test if null
            Assert.IsTrue(selfCertificationRows == true);
        }
    }
}