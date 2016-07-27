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
  public  class SubmissionGeneratedDocumentServiceTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Repository declaration
        private SubmissionGeneratedDocumentRepository _submissionGeneratedDocumentTestRepository;
        //Service declaration
        private SubmissionGeneratedDocumentService _submissionGeneratedDocumentTestService;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);

            //SubmissionGeneratedDocumentRepository Initialization
            _submissionGeneratedDocumentTestRepository = new SubmissionGeneratedDocumentRepository(_testUnitOfWork);
            //SubmissionGeneratedDocumentService Initialization
            _submissionGeneratedDocumentTestService = new SubmissionGeneratedDocumentService(_submissionGeneratedDocumentTestRepository);

            //Setup  SubmissionGeneratedDocumentRepository FackData Initialization
            var testData = new SubmissionGeneratedDocumentRepositoryData();
            _testDbContext.SubmissionGeneratedDocument.AddRange(testData.SubmissionDocumentEntitiesList);
            _testDbContext.SaveChanges();

        }
        [TestMethod]
        public void AddBusinessEntityGenarationPdf_ExistingDetails_Return_Test()
        {
            //Initial Model Details
            var businessEntityGenarationPdfEntity = new SubmissionGeneratedDocumentEntity
            {
                MasterId = "2673d7fb-9621-4c14-a896-8c9b273a54cd",
                UserId = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F",
                Gen_DocumentFrom = "SUBRENEW"
            };

            //Act 
            var documentRows = _submissionGeneratedDocumentTestService.AddBusinessEntityGenarationPdf(businessEntityGenarationPdfEntity);

            //Assert
            Assert.IsNotNull(documentRows); // Test if null
            Assert.IsTrue(documentRows == false);
        }
        [TestMethod]
        public void BusinessEntityGenarationPdf_Test()
        {
            //Initial Model Details
            var businessEntityGenarationPdfEntity = new SubmissionGeneratedDocumentEntity
            {
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
                UserId = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F",
                Gen_DocumentFrom = "SUBRENEW"
            };

            //Act 
            var documentRows = _submissionGeneratedDocumentTestService.BusinessEntityGenarationPdf("8c6deecc-3b72-485d-8af5-30939af94e97", "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F", "SUBRENEW");

            //Assert
            Assert.IsNotNull(documentRows); // Test if null
            Assert.IsTrue(documentRows.Filename == "LREN13013234_RenewalReceipt.pdf");
        }
        [TestMethod]
        public void FindBblOrderDocuments_Test()
        {
            var documentRows = _submissionGeneratedDocumentTestService.FindBblOrderDocuments("cce0b056-d2a3-485e-a02a-e34c957c4e40", "SUBREC");

            //Assert
            Assert.IsNotNull(documentRows);
            Assert.IsTrue(documentRows);
        }
    }
}
