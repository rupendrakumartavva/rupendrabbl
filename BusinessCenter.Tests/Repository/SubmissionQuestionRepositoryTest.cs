using BusinessCenter.Data;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Tests.Setup;

namespace BusinessCenter.Tests.Repository
{
    [TestClass]
    public class SubmissionQuestionRepositoryTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Repository declaration
        private SubmissionQuestionRepository _submissionQuestionTestRepository;
        private Mock<ISubmissionQuestionRepository> _mocksubmissionQuestionRepository;
        private SubmissionQuestionRepositoryData _mockData;

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


            //Setup SubmissionQuestion FackData Initialization
            var testData = new SubmissionQuestionRepositoryData();
            _testDbContext.SubmissionQuestion.AddRange(testData.SubmissionQuestionEntitiesList);
            _testDbContext.SaveChanges();

        }
        [TestMethod]
        public void FindByIdTest()
        {
            //EntityModelData is added.
            var submissionQuestionModel = new SubmissionQuestionModel
            {
                SubmQuestionsId = 2

            };
            //Act 
            var submissionquestion = _submissionQuestionTestRepository.FindByID(submissionQuestionModel);

            //Assert
            Assert.IsNotNull(submissionquestion); // Test if null
            Assert.IsTrue(submissionquestion.Count() == 1);
        }
        [TestMethod]
        public void FindByMasterIdTest()
        {
           
            //Act 
            var submissionquestion = _submissionQuestionTestRepository.FindByMasterID("cce0b056-d2a3-485e-a02a-e34c957c4e40");

            //Assert
            Assert.IsNotNull(submissionquestion); // Test if null
            Assert.IsTrue(submissionquestion.Count() == 8);
        }
        [TestMethod]
        public void InsertQuestionBblTest()
        {
            //EntityModelData is added.
            var submissionApplication = new SubmissionApplication
            {
                MasterId = "8dda4efd-8504-4d1a-8370-a18b1667a209"
            };

            var data = new List<ScreeningQuestion>();
            var screeningQuestion = new ScreeningQuestion
            {
                Question = "What is the Business Name?",
                Answer = "DCRA",
                Type = "Textbox"
            };
            data.Add(screeningQuestion);
            submissionApplication.SubQuestion = data;
           
            //Act 
            var submissionquestion = _submissionQuestionTestRepository.InsertQuestionBbl(submissionApplication);

            //Assert
            Assert.IsNotNull(submissionquestion); // Test if null
            Assert.IsTrue(submissionquestion == true);
        }
        [TestMethod]
        public void InsertQuestionBblFalseTest()
        {
            //EntityModelData is added.
            var submissionApplication = new SubmissionApplication {MasterId = "8dda4efd-8504-4d1a-8370-a18b1667a209"};



            //Act 
            var submissionquestion = _submissionQuestionTestRepository.InsertQuestionBbl(submissionApplication);

            //Assert
            Assert.IsNotNull(submissionquestion); // Test if null
            Assert.IsTrue(submissionquestion == false);
        }
        #region Junk
       // private readonly Mock<ISubmissionQuestionRepository> mockRepo = new Mock<ISubmissionQuestionRepository>();

       //private readonly List<SubmissionQuestion> list = new List<SubmissionQuestion>()
       // {
       //     new SubmissionQuestion()
       //     {
       //         SubmQuestionsId = 1,
       //         MasterId = "887fb5ad-114e-4d83-b5c9-ca214d89f117",
       //         Question = "Would you like a two (2) or four (4) year license?",
       //         Answer = "Two (2) year",
       //         OptionType = "RadioButton"
       //     },
       //     new SubmissionQuestion()
       //     {
       //         SubmQuestionsId = 2,
       //         MasterId = "2a5676eb-5b79-45f4-9b13-758d3df378a3",
       //         Question = "Will this business be located in the District of Columbia?",
       //         Answer = "NO",
       //         OptionType = "RadioButton"
       //     }
       // };

       // private readonly SubmissionQuestionModel submissionQuestionModel = new SubmissionQuestionModel
       // {
       //     SubmQuestionsId = 1,
       //     MasterId = "887fb5ad-114e-4d83-b5c9-ca214d89f117"
       // };

       // private readonly SubmissionApplication submissionApplication = new SubmissionApplication()
       // {
       //     MasterId = "2a5676eb-5b79-45f4-9b13-758d3df378a3",
       //     PrimaryID = "CCC5DE09-1273-4998-A135-17265D3AADCC"
       // };

       // [TestMethod()]
       // public void AllSubmissionQuestionsTest()
       // {
       //     mockRepo.Setup(m => m.AllSubmissionQuestions()).Returns(list);
       //     var runtimeOutput = mockRepo.Object.AllSubmissionQuestions();
       //     Assert.IsTrue(runtimeOutput != null);
       //     Assert.IsTrue(runtimeOutput.FirstOrDefault().MasterId == "887fb5ad-114e-4d83-b5c9-ca214d89f117");
       //     Assert.IsTrue(runtimeOutput.Last().MasterId == "2a5676eb-5b79-45f4-9b13-758d3df378a3");
       // }

       // [TestMethod()]
       // public void FindByIDTest()
       // {
       //     mockRepo.Setup(m => m.FindByID(submissionQuestionModel)).Returns(list);
       //     var runtimeOutput = mockRepo.Object.FindByID(submissionQuestionModel);
       //     Assert.IsTrue(runtimeOutput != null);
       //     Assert.IsTrue(runtimeOutput.FirstOrDefault().MasterId == "887fb5ad-114e-4d83-b5c9-ca214d89f117");
       //     Assert.IsTrue(runtimeOutput.Last().MasterId == "2a5676eb-5b79-45f4-9b13-758d3df378a3");
       // }

       // [TestMethod()]
       // public void FindByMasterIDTest()
       // {
       //     string masterId = "887fb5ad-114e-4d83-b5c9-ca214d89f117";
       //     mockRepo.Setup(m => m.FindByMasterID(masterId)).Returns(list);
       //     var runtimeOutput = mockRepo.Object.FindByMasterID(masterId);
       //     Assert.IsTrue(runtimeOutput != null);
       //     Assert.IsTrue(runtimeOutput.FirstOrDefault().MasterId == "887fb5ad-114e-4d83-b5c9-ca214d89f117");
       //     Assert.IsTrue(runtimeOutput.Last().MasterId == "2a5676eb-5b79-45f4-9b13-758d3df378a3");
       // }

       // [TestMethod()]
       // public void InsertQuestionBblTest()
       // {
       //     mockRepo.Setup(m => m.InsertQuestionBbl(submissionApplication)).Returns(true);
       //     var runtimeOutput = mockRepo.Object.InsertQuestionBbl(submissionApplication);
       //     Assert.AreEqual<bool>(true, runtimeOutput);
       // }
#endregion
    }
}
