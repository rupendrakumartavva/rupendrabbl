using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Interface;
using BusinessCenter.Tests.Setup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Common;
using System.Linq;

namespace BusinessCenter.Tests.Repository
{
    [TestClass]
    public class UserRepositoryTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private UserRepository _userTestRepository;
        private UserRoleRepository _userRoleTestRepository;
        private UserRepositoryData _mockData;
        private UserRoleRepositoryData _mockUserRoleData;
    //    private Mock<IUserRoleRepository> _mockUserRoleRepository;

        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);

           // _mockUserRoleRepository = new Mock<IUserRoleRepository>();

            _userRoleTestRepository = new UserRoleRepository(_testUnitOfWork);

            //_userTestRepository = new UserRepository(_testUnitOfWork, _mockUserRoleRepository.Object);
            _userTestRepository = new UserRepository(_testUnitOfWork, _userRoleTestRepository);

            //Mocking Repository
            _mockData = new UserRepositoryData();
            _mockUserRoleData = new UserRoleRepositoryData();

            //Setup Data
            var testData = new UserRepositoryData();
            _testDbContext.User.AddRange(testData.UsersEntitiesList);
            _testDbContext.SaveChanges();

            var usrRoleData = new UserRoleRepositoryData();
            _testDbContext.UserRole.AddRange(usrRoleData.UserRolesEntitiesList);
            _testDbContext.SaveChanges();
        }

        [TestMethod]
        public void Get_User_LookupAll_Test()
        {
            //Act
            var userRows = _userTestRepository.GetUserLookupAll();
            //Assert
            Assert.IsNotNull(userRows); // Test if null
            Assert.IsTrue(userRows.Count() == 5);
        }

        [TestMethod]
        public void Get_FindBy_UserId_Test()
        {
            //Initialize Entites
            string userId = "2AC53E53-87D0-468F-9628-4AEBA1120613";

            //Act
            var userRows = _userTestRepository.FindByID(userId);

            //Assert
            Assert.IsNotNull(userRows); // Test if null
            Assert.IsTrue(userRows.Count() == 1);
        }

        [TestMethod]
        public void Get_FindBy_UserName_Test()
        {
            //Initialize Entites
            string userName = "markhurd1";

            //Act
            var userRows = _userTestRepository.FindByUserName(userName);

            //Assert
            Assert.IsNotNull(userRows); // Test if null
            Assert.IsTrue(userRows.Count() == 1);
        }
        [TestMethod]
        public void GetUsersBasedOnId_ALLUsers_Test()
        {
           

            //Act
            var userRows = _userTestRepository.GetUsersBasedOnId("ALL",3);

            //Assert
            Assert.IsNotNull(userRows); // Test if null
            Assert.IsTrue(userRows.Count() == 1);
        }
        [TestMethod]
        public void GetUsersBasedOnId_ActiveUsers_Test()
        {


            //Act
            var userRows = _userTestRepository.GetUsersBasedOnId("ACTIVE", 1);

            //Assert
            Assert.IsNotNull(userRows); // Test if null
            Assert.IsTrue(userRows.Count() == 1);
        }
        [TestMethod]
        public void GetUsersBasedOnId_InActiveUsers_Test()
        {


            //Act
            var userRows = _userTestRepository.GetUsersBasedOnId("INACTIVE", 1);

            //Assert
            Assert.IsNotNull(userRows); // Test if null
            Assert.IsTrue(userRows.Count() == 0);
        }
        [TestMethod]
        public void GetuserByUserName_Test()
        {


            //Act
            var roleid = _userTestRepository.GetuserByUserName("markhurd1");

            //Assert
            Assert.IsNotNull(roleid); // Test if null
            Assert.IsTrue(roleid == 1);
        }
        [TestMethod]
        public void FindByLoginUsernameTest()
        { 
          //Act
            var userRows = _userTestRepository.FindByLoginUsername("markhurd1");

            //Assert
            Assert.IsNotNull(userRows);
            Assert.IsTrue(userRows.Count() == 1);
        
        }
       [TestMethod]
        public void UpdateLoggedInStatusTest()
        {
            //Initialize Entites
            string userId = "2AC53E53-87D0-468F-9628-4AEBA1120613";
            bool status = false;

            //Act
            var userRows = _userTestRepository.UpdateLoggedInStatus(userId, status);

            //Assert
            Assert.IsNotNull(userRows);
            Assert.IsTrue(userRows == true);
        
        }
    }
}