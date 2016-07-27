using System.Data.Common;
using System.Linq;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Interface;
using BusinessCenter.Tests.Setup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BusinessCenter.Tests.Repository
{
     [TestClass]
    public class UserPasswordTrackingRepositoryTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Repository declaration
        private UserPasswordTrackingRepository _userPasswordTrackingRepository;
     

        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);

            //UserPasswordTrackingRepository Initialization
            _userPasswordTrackingRepository = new UserPasswordTrackingRepository(_testUnitOfWork);

            //Setup UserPasswordTracking FackData Initialization
            var testData = new UserPasswordTrackingRepositoryData();
            _testDbContext.UserPasswordTracking.AddRange(testData.UserPasswordTrackingEntitiesList);
            _testDbContext.SaveChanges();

        }

        [TestMethod]
        public void GetAllAbraEntitiesTest()
        {

            //Act 
            var passwordStatusRows = _userPasswordTrackingRepository.UserPasswordTrackingData();

            //Assert
            Assert.IsNotNull(passwordStatusRows); // Test if null
            Assert.IsTrue(passwordStatusRows.Count() == 4);
        }

        [TestMethod]
        public void PasswordStatusTest_PasswordExitsTop3List()
        {

            //Act 
            var passwordStatusRows = _userPasswordTrackingRepository.PasswordStatus("FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F", "6699@pbk");

            //Assert
            Assert.IsNotNull(passwordStatusRows); // Test if null
            Assert.IsTrue(passwordStatusRows == true);
        }
        [TestMethod]
        public void PasswordStatusTest_NotPasswordExitsInTop3()
        {

            //Act 
            var passwordStatusRows = _userPasswordTrackingRepository.PasswordStatus("FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F",
                "6699@pbk1");

            //Assert
            Assert.IsNotNull(passwordStatusRows); // Test if null
            Assert.IsTrue(passwordStatusRows == false);
        }
        //[TestMethod]
        //public void PasswordStatusTest_NotPasswordExitsInTop3List()
        //{

        //    //Act 
        //    var abraRows = _userPasswordTrackingRepository.PasswordStatus("FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F",
        //        "AH9adMQSqjQF/RLWqihFh2lj1CJHFRbICwBypog1Id9XhljXZqwQ2g7/vTBruzrTWw==");

        //    //Assert
        //    Assert.IsNotNull(abraRows); // Test if null
        //    Assert.IsTrue(abraRows);
        //}

        [TestMethod]
        public void PasswordStatusTest_PasswordExitsInTop3List()
        {

            //Act 
            var passwordStatusRows = _userPasswordTrackingRepository.PasswordStatus("FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F", "AKJ+o5Gpa6dGy9+vT4sAXBBihdfufIbtFLVQtIZ+GUtHfs1jKcQdK5j7qJ3BKgYxhQ==");

            //Assert
            Assert.IsNotNull(passwordStatusRows); // Test if null
            Assert.IsTrue(passwordStatusRows == false);
        }

        public void PasswordStatusTest_PasswordInsert()
        {

            //Act 
            var passwordStatusRows = _userPasswordTrackingRepository.InsertUserPasswordTracking("FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F", "AQAKJ+o5Gpa6dGy9+vT4sAXBBihdfufIbtFLVQtIZ+GUtHfs1jKcQdK5j7qJ3BKgYxhQ==");

            //Assert
            Assert.IsNotNull(passwordStatusRows); // Test if null
            Assert.IsTrue(passwordStatusRows == false);
        }
    }
}