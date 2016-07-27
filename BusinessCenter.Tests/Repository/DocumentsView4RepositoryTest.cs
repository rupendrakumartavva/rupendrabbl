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
  public  class DocumentsView4RepositoryTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Repository declaration
        private DocumentsView4Repository _documentsView4TestRepository;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);

            //DocumentsView4Repository Initialization
            _documentsView4TestRepository = new DocumentsView4Repository(_testUnitOfWork);

            //Setup  DocumentsView4Repository FackData Initialization
            var testData = new DocumentsView4RepositoryData();
            _testDbContext.BblLicenseView4.AddRange(testData.BblLicenseView4EntitiesList);
            _testDbContext.SaveChanges();

        }
        [TestMethod]
        public void GetBblLicenseView4_Return_Test()
        {

            //Act 
            var documentRows = _documentsView4TestRepository.GetBblLicenseView4().ToList();

            //Assert
            Assert.IsNotNull(documentRows); // Test if null
            Assert.IsTrue(documentRows.Count() == 2);
        }
        [TestMethod]
        public void FindByFileNumber_Return_Test()
        {
            //Initial Model Details
            var documentData = new DocumentData { ApplicationNo = "1" };

            //Act 
            var documentRows = _documentsView4TestRepository.FindByFileNumber(documentData).ToList();

            //Assert
            Assert.IsNotNull(documentRows); // Test if null
            Assert.IsTrue(documentRows.Count() == 1);
        }
    }
     
}
