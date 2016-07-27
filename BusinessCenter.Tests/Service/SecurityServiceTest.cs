using System.Collections.Generic;
using System.Linq;
using BusinessCenter.Data;
using BusinessCenter.Data.Interface;
using BusinessCenter.Service.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BusinessCenter.Tests.Service
{
    [TestClass]
    public class SecurityServiceTest
    {
        private Mock<ISecurityRepository> _listSecurityQuestionRepo;

        [TestInitialize]
        public void Initialize()
        {
            _listSecurityQuestionRepo = new Mock<ISecurityRepository>();

            var listSecurityQuestion = new List<SecurityQuestion>() {
             new SecurityQuestion() { id = 1, Question = "what is your favorite color" },
             new SecurityQuestion() { id = 2, Question = "what is your pet name" },
             new SecurityQuestion() { id = 3, Question = "what is your primary school name" },
              new SecurityQuestion() { id = 4, Question = "what is your phone color" }
            };

            _listSecurityQuestionRepo.Setup(x => x.GetQuestions()).Returns(listSecurityQuestion);
        }

        [TestMethod]
        public void SecurityQuestion_Service_Get_ALL()
        {
            Mock<ISecurityQuestionsService> mockRepo = new Mock<ISecurityQuestionsService>();

            //Act
            mockRepo.Setup(x => x.GetAll()).Returns(_listSecurityQuestionRepo.Object.GetQuestions);
            var v = mockRepo.Object.GetAll();
            //Assert

            Assert.IsNotNull(v);
            var securityQuestion = v as SecurityQuestion[] ?? v.ToArray();
            Assert.AreEqual(4, securityQuestion.Count());
            Assert.AreEqual("what is your favorite color", securityQuestion.ToList()[0].Question);
            Assert.AreEqual("what is your pet name", securityQuestion.ToList()[1].Question);
            Assert.AreEqual("what is your primary school name", securityQuestion.ToList()[2].Question);
            Assert.AreEqual("what is your phone color", securityQuestion.ToList()[3].Question);
        }
    }
}