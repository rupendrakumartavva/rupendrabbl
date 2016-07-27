using BusinessCenter.Data;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Tests.Setup;
using Moq.Language.Flow;

namespace BusinessCenter.Tests.Repository
{
    [TestClass]
    public class UserRoleRepositoryTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private UserRoleRepository _userRoleRepositoryTestRepository;
        private UserRoleRepositoryData _mockData;

        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);

            _userRoleRepositoryTestRepository = new UserRoleRepository(_testUnitOfWork);

            //Mocking Repository
            _mockData = new UserRoleRepositoryData();

            //Setup Data
            var testData = new UserRoleRepositoryData();
            _testDbContext.UserRole.AddRange(testData.UserRolesEntitiesList);
            _testDbContext.SaveChanges();
        }

        [TestMethod]
        public void Get_UserRole_LookupAll_Test()
        {
            //Act 
            var userRoleRows = _userRoleRepositoryTestRepository.GetUserRoleLookupAll();
            //Assert
            Assert.IsNotNull(userRoleRows); // Test if null
            Assert.IsTrue(userRoleRows.Count() == 2);
        }

        [TestMethod]
        public void Get_FindBy_UserId_Test()
        {
            //Initialize Entites
            string userId = "2AC53E53-87D0-468F-9628-4AEBA1120613";

            //Act 
            var userRoleRows = _userRoleRepositoryTestRepository.FindByID(userId);

            //Assert
            Assert.IsNotNull(userRoleRows); // Test if null
            Assert.IsTrue(userRoleRows.Count() == 1);
        }

        [TestMethod]
        public void Get_FindBy_RoleId_Test()
        {
            //Initialize Entites
            string roleId = "1";

            //Act 
            var userRoleRows = _userRoleRepositoryTestRepository.FindBy(roleId);

            //Assert
            Assert.IsNotNull(userRoleRows); // Test if null
            Assert.IsTrue(userRoleRows.Count() == 1);
        }
    }
}
