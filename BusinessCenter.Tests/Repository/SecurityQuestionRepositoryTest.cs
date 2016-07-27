using BusinessCenter.Data;
using BusinessCenter.Data.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Tests.Setup;

namespace BusinessCenter.Tests.Repository
{
    [TestClass]
    public class SecurityQuestionRepositoryTest
    {

        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private SecurityRepository _securityQuestionTestRepository;

    

       


        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _securityQuestionTestRepository = new SecurityRepository(_testUnitOfWork);



            //Setup Data
            var testData = new SecurityRepositoryData();
            _testDbContext.SecurityQuestion.AddRange(testData.SecurityQuestionEntitiesList);
            _testDbContext.SaveChanges();

        }



        [TestMethod]
        public void GetAllSecurityQuestionTest()
        {

            //Act 
            var securityQuestionRows = _securityQuestionTestRepository.GetQuestions();

            //Assert
            Assert.IsNotNull(securityQuestionRows); // Test if null
            Assert.IsTrue(securityQuestionRows.Count() ==4);
        }

    }
}