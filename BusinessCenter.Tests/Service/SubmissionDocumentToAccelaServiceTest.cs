using BusinessCenter.Common;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Model;
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
  public  class SubmissionDocumentToAccelaServiceTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Repository declaration
        private SubmissionDocumentToAccelaRepository _submissionDocumentToAccelaTestRepository;
        //Sevice declaration
        private SubmissionDocumentToAccelaService _submissionDocumentToAccelaTestService;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);

            //SubmissionDocumentToAccelaRepository Initialization
            _submissionDocumentToAccelaTestRepository = new SubmissionDocumentToAccelaRepository(_testUnitOfWork);

            //SubmissionDocumentToAccelaService Initialization
            _submissionDocumentToAccelaTestService = new SubmissionDocumentToAccelaService(_submissionDocumentToAccelaTestRepository);


            //Setup  SubmissionDocumentToAccela FackData Initialization
            var testData = new SubmissionDocumentToAccelaData();
            _testDbContext.SubmissionDocumentToAccela.AddRange(testData.SubmissionDocumentToAccelaEntitiesList);
            _testDbContext.SaveChanges();

        }
        [TestMethod()]
        public void AddSubmissionDocumentsToAccelaRepository_Test()
        {
            //Initial Data is added.
            var submissionDocumentToAccelaEntity = new SubmissionDocumentToAccelaEntity
            {
                SubmisionDocToAccelaId = 0,
                LicenseNumber = "LAPP101010",
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                MasterCategoryDocId = 1,
                FileName = "",
                CreatedDate = System.DateTime.Now,
                Status = true
            };

            //Act 
            var bblRows = _submissionDocumentToAccelaTestService.SubmissionDocumentsToAccela(submissionDocumentToAccelaEntity);

            //Assert
            Assert.IsNotNull(bblRows); // Test if null
            Assert.IsTrue(bblRows);

        }
        [TestMethod()]
        public void UpdateSubmissionDocumentsToAccelaRepository_Test()
        {
            //Initial Data is added.
            var submissionDocumentToAccelaEntity = new AccelaDocument
            {

                LicenseNumber = "LAPP101010",
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                MasterCategoryDocId = "1",
                FileName = ""

            };



            //Act 
            var getSubmissionDocumentsToAccela = _submissionDocumentToAccelaTestService.InsertSubmissionDocumentsToAccela(submissionDocumentToAccelaEntity);

            //Assert
            Assert.IsNotNull(getSubmissionDocumentsToAccela); // Test if null
            Assert.IsTrue(getSubmissionDocumentsToAccela);

        }
    }
}
