using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Model;
using BusinessCenter.Tests.Setup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Repository
{
    [TestClass]
  public class MailTemplateRepositoryTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Repository declaration
        private MailTemplateRepository _mailTemplateTestRepository;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);

            //MailTemplateRepository Initialization
            _mailTemplateTestRepository = new MailTemplateRepository(_testUnitOfWork);

            //Setup  MailTemplateRepository FackData Initialization
            var testData = new MailTemplateRepositoryData();
            _testDbContext.MailTemplate.AddRange(testData.MailTemplateEntitiesList);
            _testDbContext.SaveChanges();

        }
        [TestMethod]
        public void FindByID_Return_Test()
        {
            //Initial Model Details
            var mailTemplateModel = new MailTemplateModel { EmailTemplateId = "6eaf458c-1918-4c80-8820-6f63be9d6c3c" };

            //Act 
            var mailTemplateRows = _mailTemplateTestRepository.FindByID(mailTemplateModel).ToList();

            //Assert
            Assert.IsNotNull(mailTemplateRows); // Test if null
            Assert.IsTrue(mailTemplateRows.Count() == 1);
        }
        [TestMethod]
        public void FindByStatus_Return_Test()
        {
            //Initial Model Details
            var mailTemplateModel = new MailTemplateModel { Subject = "MailAlert", UserId = "B068CA9E-BF68-4E09-816E-C1135083880E", CustomApplicationId = "LREN14005140" };

            //Act 
            var mailTemplateRows = _mailTemplateTestRepository.FindByStatus(mailTemplateModel);

            //Assert
            Assert.IsNotNull(mailTemplateRows); // Test if null
            Assert.IsTrue(mailTemplateRows==true);
        }
        [TestMethod]
        public void FindByMailStatusCheck_Return_Test()
        {
            //Initial Model Details
            var mailTemplateModel = new MailTemplateModel { Subject = "MailAlert", UserId = "B068CA9E-BF68-4E09-816E-C1135083880E", CustomApplicationId = "LREN14005140" };

            //Act 
            var mailTemplateRows = _mailTemplateTestRepository.FindByMailStatusCheck(mailTemplateModel).ToList();

            //Assert
            Assert.IsNotNull(mailTemplateRows); // Test if null
            Assert.IsTrue(mailTemplateRows.Count() == 1);
        }
        [TestMethod]
        public void InsertUpdateMailTemplate_Return_Test()
        {
            //Initial Model Details
            var mailTemplateModel = new MailTemplateModel
            {
                Type = "DailyMailAlert",
                Subject = "MailAlert",
                IsMailSent = true,
                UserId = "119ffabe-7f2b-41ab-ae0e-1c05d1a38eeb", 
                CustomApplicationId = "LREN14005140",
            };

            //Act 
            var mailTemplateRows = _mailTemplateTestRepository.InsertUpdateMailTemplate(mailTemplateModel);

            //Assert
            Assert.IsNotNull(mailTemplateRows); // Test if null
            Assert.IsTrue(mailTemplateRows == 1);
        }
        [TestMethod]
        public void UpdateMailTemplate_Return_Test()
        {
            //Initial Model Details
            var mailTemplateModel = new MailTemplateModel
            {
                Type = "DailyMailAlert",
                Subject = "MailAlert",
                IsMailSent = true,
                 UserId = "B068CA9E-BF68-4E09-816E-C1135083880E",
                CustomApplicationId = "LREN14005140",
            };

            //Act 
            var mailTemplateRows = _mailTemplateTestRepository.InsertUpdateMailTemplate(mailTemplateModel);

            //Assert
            Assert.IsNotNull(mailTemplateRows); // Test if null
            Assert.IsTrue(mailTemplateRows ==2 );
        }
    }
}
