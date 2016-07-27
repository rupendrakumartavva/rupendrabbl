using BusinessCenter.Data;
using BusinessCenter.Data.Interface;
using BusinessCenter.Email;
using BusinessCenter.Identity.Interfaces;
using BusinessCenter.Service.Interface;
using BussinessCenter.reCaptcha;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data.Common;
using System.Web;
using BusinessCenter.Api.Controllers;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Identity.IdentityModels;
using BusinessCenter.Identity.Implementation;
using BusinessCenter.Service.Implementation;
using BusinessCenter.Tests.Setup;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
namespace BusinessCenter.Api.Test.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {

        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        //Repository declaration
        private UserPasswordTrackingRepository _userPasswordTrackingTestRepository;
        //Service declaration
        private UserPasswordTrackingService _userPasswordTrackingTestService;
   
        private UserManager userManager;
        private AccountController controller = null;
        private EmailTemplate emailTemplate;
        private EmailSending sendingEmail;

        private UserManagerRepository _userManagerTestRepository;
        private UserRoleRepository _userRoleTestRepository;
        private UserRepository _userTestRepository;
        private RoleRepository _roleRepository;

        private UserManagerService _userManagerTestService;

        private  RefreshTokensRepository _refreshTokensRepository;

        private MailTemplateService _MailTemplateService;
        private MailTemplateRepository _MailTemplateRepository;



        [TestInitialize]
        public void Initialize()
        {
           

             //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);

            sendingEmail=new EmailSending();
            emailTemplate = new EmailTemplate(sendingEmail);

          

            //UserPasswordTrackingRepository Initialization
            _userPasswordTrackingTestRepository = new UserPasswordTrackingRepository(_testUnitOfWork);

            //UserPasswordTrackingService Initialization
            _userPasswordTrackingTestService = new UserPasswordTrackingService(_userPasswordTrackingTestRepository);


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
            _refreshTokensRepository=new RefreshTokensRepository(_testUnitOfWork);

            _MailTemplateRepository = new MailTemplateRepository(_testUnitOfWork);
            _MailTemplateService = new MailTemplateService(_MailTemplateRepository);

            //Setup UserPasswordTracking FackData Initialization
            var testData = new UserPasswordTrackingRepositoryData();
            _testDbContext.UserPasswordTracking.AddRange(testData.UserPasswordTrackingEntitiesList);
            _testDbContext.SaveChanges();

            var securityRepositoryData = new SecurityRepositoryData();
            _testDbContext.SecurityQuestion.AddRange(securityRepositoryData.SecurityQuestionEntitiesList);
            _testDbContext.SaveChanges();

            userManager = new UserManager(HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>(),
                   HttpContext.Current.GetOwinContext().Authentication);

            controller = new AccountController(userManager, emailTemplate, _userManagerTestService, _userPasswordTrackingTestService, _refreshTokensRepository, _MailTemplateService);
        }

        //[TestMethod]
        //public void UserAccountsControllerTest_GetSecurityQuestions_ALL()
        //{
        //    // Arrange
        //    var controller = new UserAccountsController(mockUserManager.Object, mockEmailTemplate.Object, _listReCaptcha.Object, mockRepo.Object);
        //    //  controller.Request = new HttpRequestMessage();
        //    //  controller.Configuration = new HttpConfiguration();

        //    // string locationUrl = "http://localhost:40001/";

        //    controller.Request = new HttpRequestMessage
        //    {
        //        RequestUri = new Uri("http://localhost:40001/api/UserAccounts/Questions")
        //    };
        //    controller.Configuration = new HttpConfiguration();
        //    controller.Configuration.Routes.MapHttpRoute(
        //        name: "DefaultApi",
        //        routeTemplate: "api/{controller}/{id}",
        //        defaults: new { id = RouteParameter.Optional });

        //    controller.RequestContext.RouteData = new HttpRouteData(
        //        route: new HttpRoute(),
        //        values: new HttpRouteValueDictionary { { "controller", "products" } });

        //    // Act
        //    var listSecurityQuestion = new List<SecurityQuestion>() {
        //     new SecurityQuestion() { id = 1, Question = "what is your favorite color" },
        //     new SecurityQuestion() { id = 2, Question = "what is your pet name" },
        //     new SecurityQuestion() { id = 3, Question = "what is your primary school name" },
        //      new SecurityQuestion() { id = 4, Question = "what is your phone color" }
        //    };
        //    var response = controller.GetSecurityQuestions();

        //    // Assert
        //    //  Assert.AreEqual("http://localhost:40001/api/UserAccounts/Questions", response.Headers.Location.AbsoluteUri);

        //}
    }
}