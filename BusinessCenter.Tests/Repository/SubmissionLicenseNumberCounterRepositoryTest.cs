using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Model;
using BusinessCenter.Tests.Setup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Repository
{
     [TestClass]
  public  class SubmissionLicenseNumberCounterRepositoryTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Repository declaration
        private SubmissionLicenseNumberCounterRepository _submissionLicenseNumberCounterTestRepository;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);

            //SubmissionLicenseNumberCounterRepository Initialization
            _submissionLicenseNumberCounterTestRepository = new SubmissionLicenseNumberCounterRepository(_testUnitOfWork);

            //Setup  SubmissionLicenseNumberCounter FackData Initialization
            var testData = new SubmissionLicenseNumberCounterData();
            _testDbContext.SubmissionLicenseNumberCounter.AddRange(testData.SubmissionLicenseCounterEntitiesList);
            _testDbContext.SaveChanges();

        }
        [TestMethod]
        public void FindBy_Test()
        {
            //Initial Model Details
            var submissioncounter = new SubmissionCounter { Type = "DAPP" };

            //Act 
            var  LicenseNumberCounterRows= _submissionLicenseNumberCounterTestRepository.FindBy(submissioncounter).ToList();

            //Assert
            Assert.IsNotNull(LicenseNumberCounterRows); // Test if null
            Assert.IsTrue(LicenseNumberCounterRows.Count() == 2);
        }
        [TestMethod]
        public void InsertSubmissionCounter_Test()
        {
            //Initial Model Details
            var submissioncounter = new SubmissionCounter 
            {
                Type = "LAPP_IAPP"
            };

            //Act 
            var LicenseNumberCounterRows = _submissionLicenseNumberCounterTestRepository.InsertUpdateSubmissionCounter(submissioncounter);

            //Assert
            Assert.IsNotNull(LicenseNumberCounterRows); // Test if null
            Assert.IsTrue(LicenseNumberCounterRows=="3");
        }
        [TestMethod]
        public void UpdateSubmissionCounter_Test()
        {
            //Initial Model Details
            var submissioncounter = new SubmissionCounter
            {
                Type = "DAPP",
                PhysicalYear=14
            };

            //Act 
            var LicenseNumberCounterRows = _submissionLicenseNumberCounterTestRepository.InsertUpdateSubmissionCounter(submissioncounter);

            //Assert
            Assert.IsNotNull(LicenseNumberCounterRows); // Test if null
            Assert.IsTrue(LicenseNumberCounterRows == "1");
        }
        [TestMethod]
        public void UpdateSubmissionCounter_anotheryear_Test()
        {
            //Initial Model Details
            var submissioncounter = new SubmissionCounter
            {
                Type = "LAPP_IAPP",
                PhysicalYear = 15
            };

            //Act 
            var LicenseNumberCounterRows = _submissionLicenseNumberCounterTestRepository.InsertUpdateSubmissionCounter(submissioncounter);

            //Assert
            Assert.IsNotNull(LicenseNumberCounterRows); // Test if null
            Assert.IsTrue(LicenseNumberCounterRows == "3");
        }
        [TestMethod]
        public void InsertSubmissionCounter_anotheryear_Test()
        {
            //Initial Model Details
            var submissioncounter = new SubmissionCounter
            {
                Type = "LAPPIAPP",
                PhysicalYear = 15
            };

            //Act 
            var LicenseNumberCounterRows = _submissionLicenseNumberCounterTestRepository.InsertUpdateSubmissionCounter(submissioncounter);

            //Assert
            Assert.IsNotNull(LicenseNumberCounterRows); // Test if null
            Assert.IsTrue(LicenseNumberCounterRows == "4");
        }
    }
}
