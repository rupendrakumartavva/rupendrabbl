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
  public  class UserPasswordTrackingserviceTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Repository declaration
        private UserPasswordTrackingRepository _userPasswordTrackingTestRepository;

        //Service declaration
        private UserPasswordTrackingService _userPasswordTrackingTestService;

        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);

            //UserPasswordTrackingRepository Initialization
            _userPasswordTrackingTestRepository = new UserPasswordTrackingRepository(_testUnitOfWork);

            //UserPasswordTrackingService Initialization
            _userPasswordTrackingTestService = new UserPasswordTrackingService(_userPasswordTrackingTestRepository);

            //Setup UserPasswordTracking FackData Initialization
            var testData = new UserPasswordTrackingRepositoryData();
            _testDbContext.UserPasswordTracking.AddRange(testData.UserPasswordTrackingEntitiesList);
            _testDbContext.SaveChanges();

        }
        [TestMethod]
        public void PasswordStatusTest_PasswordExitsTop3List()
        {

            //Act 
            var passwordStatusRows = _userPasswordTrackingTestService.PasswordStatus("FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F", "6699@pbk");

            //Assert
            Assert.IsNotNull(passwordStatusRows); // Test if null
            Assert.IsTrue(passwordStatusRows == true);
        }
        [TestMethod]
        public void PasswordStatusTest_NotPasswordExitsInTop3()
        {

            //Act 
            var passwordStatusRows = _userPasswordTrackingTestService.PasswordStatus("FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F",
                "6699@pbk1");

            //Assert
            Assert.IsNotNull(passwordStatusRows); // Test if null
            Assert.IsTrue(passwordStatusRows == false);
        }
          [TestMethod]
        public void PasswordStatusTest_PasswordInsert()
        {

            //Act 
            var passwordStatusRows = _userPasswordTrackingTestService.InsertUserPasswordTracking("FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F", "AQAKJ+o5Gpa6dGy9+vT4sAXBBihdfufIbtFLVQtIZ+GUtHfs1jKcQdK5j7qJ3BKgYxhQ==");

            //Assert
            Assert.IsNotNull(passwordStatusRows); // Test if null
            Assert.IsTrue(passwordStatusRows == true);
        }
    }
}
