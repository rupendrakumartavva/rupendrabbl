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
    public class SubmissionDocumentToAccelaRepositoryTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Repository declaration
        private SubmissionDocumentToAccelaRepository _submissionDocumentToAccelaTestRepository;

    


        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);

            //SubmissionDocumentToAccelaRepository Initialization
            _submissionDocumentToAccelaTestRepository = new SubmissionDocumentToAccelaRepository(_testUnitOfWork);



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
                LicenseNumber="LAPP101010",
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                MasterCategoryDocId=1,
                FileName="",
                CreatedDate=System.DateTime.Now,
                Status = true
            };



            //Act 
            var bblRows = _submissionDocumentToAccelaTestRepository.AddSubmissionDocumentsToAccelaRepository(submissionDocumentToAccelaEntity);

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
        var getSubmissionDocumentsToAccela=   _submissionDocumentToAccelaTestRepository.UpdateFinalDocumentsToAccela(submissionDocumentToAccelaEntity);

            //Assert
        Assert.IsNotNull(getSubmissionDocumentsToAccela); // Test if null
        Assert.IsTrue(getSubmissionDocumentsToAccela);

        }

    [TestMethod()]
    public void UpdateSubmissionDocumentsToAccelaRepository_FailToInsert_Test()
    {
        //Initial Data is added.
        var submissionDocumentToAccelaEntity = new AccelaDocument
        {

            LicenseNumber = "LAPP101010",
            MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
            MasterCategoryDocId = "DocumentData",
            FileName = ""

        };



        //Act 
        var getSubmissionDocumentsToAccela = _submissionDocumentToAccelaTestRepository.UpdateFinalDocumentsToAccela(submissionDocumentToAccelaEntity);

        //Assert
        Assert.IsNotNull(getSubmissionDocumentsToAccela); // Test if null
        Assert.IsTrue(getSubmissionDocumentsToAccela==false);

    }


    [TestMethod()]
    public void UpdateSubmissionDocumentsToAccelaRepository_null_Test()
    {
        //Initial Data is added.
        var submissionDocumentToAccelaEntity = new AccelaDocument
        {

          
            MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
         

        };



        //Act 
        var getSubmissionDocumentsToAccela = _submissionDocumentToAccelaTestRepository.UpdateFinalDocumentsToAccela(submissionDocumentToAccelaEntity);

        //Assert
        Assert.IsNotNull(getSubmissionDocumentsToAccela); // Test if null
        Assert.IsTrue(getSubmissionDocumentsToAccela);

    }
     
    }
}
