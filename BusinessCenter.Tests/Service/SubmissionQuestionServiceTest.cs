using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using BusinessCenter.Service.Implementation;
using BusinessCenter.Tests.Setup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Service
{
      [TestClass]
  public class SubmissionQuestionServiceTest
    {

        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private SubmissionQuestionRepository _submissionQuestionTestRepository;
        private Mock<ISubmissionQuestionRepository> _mocksubmissionQuestionRepository;
        private SubmissionQuestionRepositoryData _mockData;

        private SubmissionQuestionService _submissionQuestionTestService;

        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _submissionQuestionTestRepository = new SubmissionQuestionRepository(_testUnitOfWork);

            //Mocking Repository
            _mocksubmissionQuestionRepository = new Mock<ISubmissionQuestionRepository>();
            _mockData = new SubmissionQuestionRepositoryData();

            _submissionQuestionTestService = new SubmissionQuestionService(_submissionQuestionTestRepository);

            //Setup Data
            var testData = new SubmissionQuestionRepositoryData();
            _testDbContext.SubmissionQuestion.AddRange(testData.SubmissionQuestionEntitiesList);
            _testDbContext.SaveChanges();

        }
        [TestMethod]
        public void FindByIdTest()
        {
            var submissionQuestionModel = new SubmissionQuestionModel
            {
                SubmQuestionsId = 2

            };
            //Act 
            var submissionquestion = _submissionQuestionTestService.FindSubmissionQuestionId(submissionQuestionModel);

            //Assert
            Assert.IsNotNull(submissionquestion); // Test if null
            Assert.IsTrue(submissionquestion.Count() == 1);
        }
    }

}
