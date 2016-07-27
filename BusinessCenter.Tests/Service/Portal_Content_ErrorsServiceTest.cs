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
    public class Portal_Content_ErrorsServiceTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Repository declaration
        private Portal_Content_ErrorsRepository _portal_Content_ErrorsTestRepository;
         //Service declaration
        private Portal_Content_ErrorsService _portal_Content_ErrorsTestService;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);

            //Portal_Content_ErrorsRepository Initialization
            _portal_Content_ErrorsTestRepository = new Portal_Content_ErrorsRepository(_testUnitOfWork);
            //Portal_Content_ErrorsService Initialization
            _portal_Content_ErrorsTestService = new Portal_Content_ErrorsService(_portal_Content_ErrorsTestRepository);

            //Setup  Portal_Content_ErrorsRepository FackData Initialization
            var testData = new Portal_Content_ErrorsRepositoryData();
            _testDbContext.Portal_Content_Errors.AddRange(testData.PortalContentEntitiesList);
            _testDbContext.SaveChanges();

        }
        [TestMethod]
        public void GetAllErrors_Return_Test()
        {

            //Act 
            var errorContentRows = _portal_Content_ErrorsTestService.GetAllContentErrors().ToList();

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
            var errorContentRows = _portal_Content_ErrorsTestService.FindByMessageId(portalContentErrors).ToList();

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
            var errorContentRows = _portal_Content_ErrorsTestService.FindByMessageName(portalContentErrors).ToList();

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
                ErrrorMessage = "phone number mustbe 10characters."
            };

            //Act 
            var errorContentRows = _portal_Content_ErrorsTestService.InsertUpdateContentErrors(portalContentErrors);

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
            var errorContentRows = _portal_Content_ErrorsTestService.InsertUpdateContentErrors(portalContentErrors);

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
                IsActive = false
            };

            //Act 
            var errorContentRows = _portal_Content_ErrorsTestService.DeleteContentErrors(portalContentErrors);

            //Assert
            Assert.IsNotNull(errorContentRows); // Test if null
            Assert.IsTrue(errorContentRows);
        }
    }
}
