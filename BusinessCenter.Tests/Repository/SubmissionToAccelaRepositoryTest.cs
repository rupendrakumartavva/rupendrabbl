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
    public class SubmissionToAccelaRepositoryTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Repository declaration
        private SubmissionToAccelaRepository _submissionToAccelaTestRepository;
        private Mock<ISubmissionToAccelaRepository> _mocksubmissionToAccelaRepository;
        private SubmissionToAccelaRepositoryData _mockData;

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

            //Setup  SubmissiontoAccela FackData
            var testData = new SubmissionToAccelaRepositoryData();
            _testDbContext.SubmissiontoAccela.AddRange(testData.SubmissiontoAccelaEntitiesList);
            _testDbContext.SaveChanges();

        }
        [TestMethod]
        public void GetSubmissionsToAccela_Return_Test()
        {

            //Act 
            var accelaRows = _submissionToAccelaTestRepository.GetSubmissionsToAccela().ToList();

            //Assert
            Assert.IsNotNull(accelaRows); // Test if null
            Assert.IsTrue(accelaRows.Count() == 1);
        }
        [TestMethod]
        public void InsertSubmissionToAccela_ReturnTrue()
        {
            //EntityData is Added
            var submissiontoAccelaEntity = new SubmissiontoAccelaEntity
            {
                SubmissiontoAccelaId = 2,
                LicenseNumber = "9999999999",
                ApplicationCompleted = true,
                ApplicationCreated=false,
                ApplicationFeeMatched=false,
                RenewalPaymentUpdated=false,
                RenewalFeeMatched=false,
                AllDocumentsUpdated=true

            };
            //Act 
            var userbblService = _submissionToAccelaTestRepository.AddSubmissionToAccelaRepository(submissiontoAccelaEntity);

            //Assert
            Assert.IsNotNull(userbblService); // Test if null
            Assert.IsTrue(userbblService == true);
        }
       
        #region Junk
        //private readonly Mock<ISubmissionToAccelaRepository> mockRepo=new Mock<ISubmissionToAccelaRepository>();
        //private readonly SubmissiontoAccela submissiontoAccela=new SubmissiontoAccela();
        //private readonly SubmissiontoAccelaEntity submissiontoAccelaEntity = new SubmissiontoAccelaEntity();

        //[TestMethod()]
        //public void AddSubmissionToAccelaRepositoryTest()
        //{
        //    mockRepo.Setup(m => m.AddSubmissionToAccelaRepository(submissiontoAccelaEntity)).Returns(true);
        //    var runtimeOutput = mockRepo.Object.AddSubmissionToAccelaRepository(submissiontoAccelaEntity);
        //    Assert.AreEqual<bool>(true, runtimeOutput);
        //}
#endregion
    }
}
