using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Models;
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
 public   class UserManagerServiceTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Repository declaration
        private UserManagerRepository _userManagerTestRepository;
        private UserRoleRepository _userRoleTestRepository;
        private UserRepository _userTestRepository;
        private RoleRepository _roleRepository;

        private UserManagerService  _userManagerTestService;



        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);

            //UserRoleRepository Initialization
            _userRoleTestRepository = new UserRoleRepository(_testUnitOfWork);

            //UserRepository Initialization
            _userTestRepository = new UserRepository(_testUnitOfWork, _userRoleTestRepository);

            //RoleRepository Initialization
            _roleRepository = new RoleRepository(_testUnitOfWork);

            //UserManagerRepository Initialization
            _userManagerTestRepository = new UserManagerRepository(_testUnitOfWork, _userRoleTestRepository, _userTestRepository, _roleRepository);

            //UserManagerService Initialization
            _userManagerTestService = new UserManagerService(_userManagerTestRepository);

            //Setup  UserRole FackData Initialization
            var userRoleData = new UserRoleRepositoryData();
            _testDbContext.UserRole.AddRange(userRoleData.UserRolesEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  User FackData Initialization
            var userData = new UserRepositoryData();
            _testDbContext.User.AddRange(userData.UsersEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  UserLoginHistory FackData Initialization
            var testData = new UserManagerRepositoryData();
            _testDbContext.UserLoginHistory.AddRange(testData.UserLoginEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  Role FackData Initialization
            var RoleData = new RoleRepositoryData();
            _testDbContext.Role.AddRange(RoleData.RoleEntitiesList);
            _testDbContext.SaveChanges();

        }
        [TestMethod]
        public void AddUserLoginHistory_Return_Test()
        {
            var userloginEntity = new UserLoginHistoryModel { UserId = "E5641464-F7A4-499C-9AF2-E450C8C26796", LastLoginDate = System.DateTime.Now };
            //Act 
            var userLoginRows = _userManagerTestService.UserLoginHistory(userloginEntity);

            //Assert
            Assert.IsNotNull(userLoginRows); // Test if null
            Assert.IsTrue(userLoginRows == 1);
        }
        [TestMethod]
        public async Task GetLoginHistoryCount_Return_Test()
        {

            //Act 
            var userLoginRows = await _userManagerTestService.GetLoginHistoryCount(System.DateTime.Now, System.DateTime.Now);

            //Assert
            Assert.IsNotNull(userLoginRows); // Test if null
            Assert.IsTrue(userLoginRows.Count() == 2);
        }
               [TestMethod]
        public async Task GetCreatedDeleteDateWiseCount_Return_Test()
        {

            //Act 
            var userLoginRows = await _userManagerTestService.GetCreatedDeleteDateWiseCount("2");

            //Assert
            Assert.IsNotNull(userLoginRows); // Test if null
            Assert.IsTrue(userLoginRows.Count() == 1);
        }
    }
}
