using BusinessCenter.Common;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Interface;
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
   public class SubmissionToAccelaServiceTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private SubmissionToAccelaRepository _submissionToAccelaTestRepository;
        private Mock<ISubmissionToAccelaRepository> _mocksubmissionToAccelaRepository;
        private SubmissionToAccelaRepositoryData _mockData;


        private SubmissionToAccelaService _submissionToAccelaTestService;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _submissionToAccelaTestRepository = new SubmissionToAccelaRepository(_testUnitOfWork);

            //Mocking Repository
            _mocksubmissionToAccelaRepository = new Mock<ISubmissionToAccelaRepository>();
            _mockData = new SubmissionToAccelaRepositoryData();
            _submissionToAccelaTestService = new SubmissionToAccelaService(_submissionToAccelaTestRepository);

            //Setup Data
            var testData = new SubmissionToAccelaRepositoryData();
            _testDbContext.SubmissiontoAccela.AddRange(testData.SubmissiontoAccelaEntitiesList);
            _testDbContext.SaveChanges();

        }
        [TestMethod]
        public void SubmissionsAccelaDetails_Return_Test()
        {

            //Act 
            var accelaRows = _submissionToAccelaTestService.SubmissionsAccelaDetails().ToList();

            //Assert
            Assert.IsNotNull(accelaRows); // Test if null
            Assert.IsTrue(accelaRows.Count() == 1);
        }
        [TestMethod]
        public void InsertSubmissionToAccela_ReturnTrue()
        {
            var submissiontoAccelaEntity = new SubmissiontoAccelaEntity
            {
                SubmissiontoAccelaId = 2,
                LicenseNumber = "9999999999",
                ApplicationCompleted = true,
                ApplicationCreated = false,
                ApplicationFeeMatched = false,
                RenewalPaymentUpdated = false,
                RenewalFeeMatched = false,
                AllDocumentsUpdated = true

            };
            //Act 
            var userbblService = _submissionToAccelaTestService.AddSubmissionToAccelaRepository(submissiontoAccelaEntity);

            //Assert
            Assert.IsNotNull(userbblService); // Test if null
            Assert.IsTrue(userbblService == true);
        }

    }
}
