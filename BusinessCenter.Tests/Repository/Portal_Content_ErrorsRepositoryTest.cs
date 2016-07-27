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
 public   class Portal_Content_ErrorsRepositoryTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Repository declaration
        private Portal_Content_ErrorsRepository _portal_Content_ErrorsTestRepository;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);

            //Portal_Content_ErrorsRepository Initialization
            _portal_Content_ErrorsTestRepository = new Portal_Content_ErrorsRepository(_testUnitOfWork);

            //Setup  Portal_Content_ErrorsRepository FackData Initialization
            var testData = new Portal_Content_ErrorsRepositoryData();
            _testDbContext.Portal_Content_Errors.AddRange(testData.PortalContentEntitiesList);
            _testDbContext.SaveChanges();

        }
        [TestMethod]
        public void GetAllErrors_Return_Test()
        {

            //Act 
            var errorContentRows = _portal_Content_ErrorsTestRepository.GetAllErrors().ToList();

            //Assert
            Assert.IsNotNull(errorContentRows); // Test if null
            Assert.IsTrue(errorContentRows.Count() == 1);
        }
        [TestMethod]
        public void FindByMessageId_Return_Test()
        {
            //Initial Model Details
            var portalContentErrors = new PortaContentErrorsModel { MessageId = "0e04bb3e-51e6-441d-87a9-0d6bd29261fd" };

            //Act 
            var errorContentRows = _portal_Content_ErrorsTestRepository.FindByMessageId(portalContentErrors).ToList();

            //Assert
            Assert.IsNotNull(errorContentRows); // Test if null
            Assert.IsTrue(errorContentRows.Count() == 1);
        }
        [TestMethod]
        public void FindByMessageName_Return_Test()
        {
            //Initial Model Details
            var portalContentErrors = new PortaContentErrorsModel 
            {
                MessageType = "Success",
                ShortName = "Inserted"
            };

            //Act 
            var errorContentRows = _portal_Content_ErrorsTestRepository.FindByMessageName(portalContentErrors).ToList();

            //Assert
            Assert.IsNotNull(errorContentRows); // Test if null
            Assert.IsTrue(errorContentRows.Count() == 1);
        }
        [TestMethod]
        public void InsertContentErrors_Return_Test()
        {
            //Initial Model Details
            var portalContentErrors = new PortaContentErrorsModel
            {
                MessageType = "Maxlength",
                ShortName = "Telphone",
                ErrrorMessage="phone number mustbe 10characters."
            };

            //Act 
            var errorContentRows = _portal_Content_ErrorsTestRepository.InsertUpdateContentErrors(portalContentErrors);

            //Assert
            Assert.IsNotNull(errorContentRows); // Test if null
            Assert.IsTrue(errorContentRows);
        }
        [TestMethod]
        public void UpdateContentErrors_Return_Test()
        {
            //Initial Model Details
            var portalContentErrors = new PortaContentErrorsModel
            {
                MessageId = "0e04bb3e-51e6-441d-87a9-0d6bd29261fd",
                MessageType = "Maxlength",
                ShortName = "Telphone",
                ErrrorMessage = "phone number mustbe 10characters."
            };

            //Act 
            var errorContentRows = _portal_Content_ErrorsTestRepository.InsertUpdateContentErrors(portalContentErrors);

            //Assert
            Assert.IsNotNull(errorContentRows); // Test if null
            Assert.IsTrue(errorContentRows);
        }
        [TestMethod]
        public void DeleteContentErrors_Return_Test()
        {
            //Initial Model Details
            var portalContentErrors = new PortaContentErrorsModel
            {
                MessageId = "0e04bb3e-51e6-441d-87a9-0d6bd29261fd",
               IsActive=false
            };

            //Act 
            var errorContentRows = _portal_Content_ErrorsTestRepository.DeleteContentErrors(portalContentErrors);

            //Assert
            Assert.IsNotNull(errorContentRows); // Test if null
            Assert.IsTrue(errorContentRows);
        }
    }
}
