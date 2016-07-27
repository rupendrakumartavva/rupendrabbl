using System.Data.Common;
using System.Linq;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Service.Implementation;
using BusinessCenter.Tests.Setup;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessCenter.Tests.Service
{
    [TestClass]
    public class SecurityQuestionsTestService
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private SecurityRepository _securityQuestionTestRepository;
        
        private SecurityQuestionService securityQuestionsService;

        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _securityQuestionTestRepository = new SecurityRepository(_testUnitOfWork);

            securityQuestionsService = new SecurityQuestionService(_securityQuestionTestRepository);

            //Setup Data
            var testData = new SecurityRepositoryData();
            _testDbContext.SecurityQuestion.AddRange(testData.SecurityQuestionEntitiesList);
            _testDbContext.SaveChanges();

        }

        [TestMethod]
        public void GetAllSecurityQuestionTest()
        {

            //Act 
            var securityQuestionRows = securityQuestionsService.GetAll();

            //Assert
            Assert.IsNotNull(securityQuestionRows); // Test if null
            Assert.IsTrue(securityQuestionRows.Count() == 4);
        }

    }
}