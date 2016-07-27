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
   public class RenewalView3ServiceTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Repository declaration
        private RenewalView3Repository _renewalView3TestRepository;
        //Service declaration
        private RenewalView3Service _renewalView3TestService;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);

            //RenewalView3Repository Initialization
            _renewalView3TestRepository = new RenewalView3Repository(_testUnitOfWork);
            //RenewalView3Service Initialization
            _renewalView3TestService = new RenewalView3Service(_renewalView3TestRepository);

            //Setup  RenewalView3Repository FackData Initialization
            var testData = new RenewalView3RepositoryData();
            _testDbContext.BblLicenseView3.AddRange(testData.BblLicenseView3EntitiesList);
            _testDbContext.SaveChanges();

        }
        [TestMethod]
        public void GetRenewData_Return_Test()
        {

            //Act 
            var renewalRows = _renewalView3TestService.GetRenewData().ToList();

            //Assert
            Assert.IsNotNull(renewalRows); // Test if null
            Assert.IsTrue(renewalRows.Count() == 1);
        }
        [TestMethod]
        public void FindByLicenseNumber_Return_Test()
        {
            //Initial Model Details
            var businessLicense = new RenewalLicense
            {

                MasterId = "2673d7fb-9621-4c14-a896-8c9b273a54cd"
            };
            //Act 
            var renewalRows = _renewalView3TestService.FindByLicenseNumber(businessLicense).ToList();

            //Assert
            Assert.IsNotNull(renewalRows); // Test if null
            Assert.IsTrue(renewalRows.Count() == 1);
        }
    }
}
