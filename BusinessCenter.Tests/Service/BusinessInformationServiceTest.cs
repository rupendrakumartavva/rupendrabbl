using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Model;
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
 public class BusinessInformationServiceTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Repository declaration
        private BusinessInformationRepository _businessInformationTestRepository;
        //Service declaration
        private BusinessInformationService _businessInformationTestService;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);

            //BusinessInformationRepository Initialization
            _businessInformationTestRepository = new BusinessInformationRepository(_testUnitOfWork);

            //BusinessInformationService Initialization
            _businessInformationTestService = new BusinessInformationService(_businessInformationTestRepository);

            //Setup  BusinessInformationRepository FackData Initialization
            var testData = new BusinessInformationRepositoryData();
            _testDbContext.BblLicenseView.AddRange(testData.BblLicenseViewEntitiesList);
            _testDbContext.SaveChanges();

        }
        [TestMethod]
        public void GetSubmissionData_Return_Test()
        {

            //Act 
            var documentRows = _businessInformationTestService.GetSubmissionData().ToList();

            //Assert
            Assert.IsNotNull(documentRows); // Test if null
            Assert.IsTrue(documentRows.Count() == 1);
        }
        [TestMethod]
        public void FindByLicenseNumber_Return_Test()
        {
            //Initial Model Details
            var businessLicense = new BusinessLicense { MasterId = "18329841-723b-45cf-855a-ed2b24eace0f" };

            //Act 
            var documentRows = _businessInformationTestService.FindByLicenseNumber(businessLicense).ToList();

            //Assert
            Assert.IsNotNull(documentRows); // Test if null
            Assert.IsTrue(documentRows.Count() == 1);
        }
    }
}
