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
  public  class SecurityQuestionServiceTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private SecurityRepository _securityQuestionTestRepository;

        private SecurityRepositoryData _mockData;

        private SecurityQuestionService _securityQuestionTestService;

        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _securityQuestionTestRepository = new SecurityRepository(_testUnitOfWork);

            _securityQuestionTestService = new SecurityQuestionService(_securityQuestionTestRepository);
            _mockData = new SecurityRepositoryData();
            //Setup Data
            var testData = new SecurityRepositoryData();
            _testDbContext.SecurityQuestion.AddRange(testData.SecurityQuestionEntitiesList);
            _testDbContext.SaveChanges();

        }
        [TestMethod]
        public void GetAllSecurityQuestionTest()
        {

            //Act 
            var securityQuestionRows = _securityQuestionTestService.GetAll();

            //Assert
            Assert.IsNotNull(securityQuestionRows); // Test if null
            Assert.IsTrue(securityQuestionRows.Count() == 4);
        }

    }
}
