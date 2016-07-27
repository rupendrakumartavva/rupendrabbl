using BusinessCenter.Common;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
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
   public  class SubmissionGeneratedDocumentRepositoryTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Repository declaration
        private SubmissionGeneratedDocumentRepository _submissionGeneratedDocumentTestRepository;

        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);

            //SubmissionGeneratedDocumentRepository Initialization
            _submissionGeneratedDocumentTestRepository = new SubmissionGeneratedDocumentRepository(_testUnitOfWork);

            //Setup  SubmissionGeneratedDocumentRepository FackData Initialization
            var testData = new SubmissionGeneratedDocumentRepositoryData();
            _testDbContext.SubmissionGeneratedDocument.AddRange(testData.SubmissionDocumentEntitiesList);
            _testDbContext.SaveChanges();

        }
        [TestMethod]
        public void AddBusinessEntityGenarationPdf_ExistingDetails_Return_Test()
        {
            byte[] arraytoinsert = new byte[1] {0};
            //Initial Model Details
            var businessEntityGenarationPdfEntity = new SubmissionGeneratedDocumentEntity
            {
                SubmissionGeneratedDocumentId="10",
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                SubmissionLicenseNumber = "DAPP15985360",
                UserId = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F",
                FileByteCode = arraytoinsert,
                 FileType="pdf",
                 CreatedDate=DateTime.Now,
                 UpdatedDate=DateTime.Now,
                Gen_DocumentFrom = "SUBRENEW"             
              
            };

            //Act 
            var documentRows = _submissionGeneratedDocumentTestRepository.AddBusinessEntityGenarationPdf(businessEntityGenarationPdfEntity);

            //Assert
            Assert.IsNotNull(documentRows); // Test if null
            Assert.IsTrue(documentRows);
        }
        [TestMethod]
        public void AddBusinessEntityGenarationPdf_ExistingDetails_Return_false_Test()
        {
            byte[] arraytoinsert = new byte[1] { 0 };
            //Initial Model Details
            var businessEntityGenarationPdfEntity = new SubmissionGeneratedDocumentEntity
            {
            
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                SubmissionLicenseNumber = "DAPP15985360",
                UserId = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F",
                FileByteCode = arraytoinsert,
                FileType = "pdf",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Gen_DocumentFrom = "SUBRENEW"

            };

            //Act 
            var documentRows = _submissionGeneratedDocumentTestRepository.AddBusinessEntityGenarationPdf(businessEntityGenarationPdfEntity);

            //Assert
            Assert.IsNotNull(documentRows); // Test if null
            Assert.IsTrue(!documentRows);
        }
        //[TestMethod]
        //public void AddBusinessEntityGenarationPdf_NewDetails_Return_Test()
        //{
        //    //Initial Model Details
        //    var businessEntityGenarationPdfEntity = new SubmissionGeneratedDocumentEntity
        //    {
        //        MasterId = "810b1def-6d1c-4dcf-b24f-d8f9afaecfc4",
        //        UserId = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F",
        //        Gen_DocumentFrom = "SUBRENEW",
        //        SubmissionLicenseNumber = "400316957915",
        //        SubmissionGeneratedDocumentId = "356ca00b-6472-4895-89a2-c02cfa4d62c6",
        //        FileType = "LREN13013234_RenewalReceipt.pdf",
        //        CreatedDate = System.DateTime.Now,
        //        UpdatedDate = System.DateTime.Now,
        //    };

        //    //Act 
        //    var documentRows = _submissionGeneratedDocumentTestRepository.AddBusinessEntityGenarationPdf(businessEntityGenarationPdfEntity);

        //    //Assert
        //    Assert.IsNotNull(documentRows); // Test if null
        //    Assert.IsTrue(documentRows == true);
        //}
        [TestMethod]
        public void AddBusinessEntityGenarationPdf_Test()
        {
            //Initial Model Details
            var businessEntityGenarationPdfEntity = new SubmissionGeneratedDocumentEntity
            {
                MasterId = "810b1def-6d1c-4dcf-b24f-d8f9afaecfc4",
                UserId = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F",
                Gen_DocumentFrom = "SUBRENEW"
            };

            //Act 
            var documentRows = _submissionGeneratedDocumentTestRepository.AddBusinessEntityGenarationPdf("8c6deecc-3b72-485d-8af5-30939af94e97", "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F", "SUBRENEW");

            //Assert
            Assert.IsNotNull(documentRows); // Test if null
            Assert.IsTrue(documentRows.Filename =="LREN13013234_RenewalReceipt.pdf");
        }
        [TestMethod]
        public void FindDocument_Test()
        {
            var documentRows = _submissionGeneratedDocumentTestRepository.FindDocument("cce0b056-d2a3-485e-a02a-e34c957c4e40", "SUBREC");
           
            //Assert
            Assert.IsNotNull(documentRows);
            Assert.IsTrue(documentRows);
        }
    }
}
