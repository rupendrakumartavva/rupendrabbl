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
    public class MasterCountryServiceTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private MasterCountryRepository _masterCountryRepositoryTestRepository;

        private Mock<IMasterPrimaryCategoryRepository> _mockMasterPrimaryCategoryRepository;
      

        private MasterCountryService _mockMasterCountryTestService;

        [TestInitialize]
        public void Initialize()
        {

            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _mockMasterPrimaryCategoryRepository = new Mock<IMasterPrimaryCategoryRepository>();

            _masterCountryRepositoryTestRepository = new MasterCountryRepository(_testUnitOfWork);

           


            //_mockBusinessActivityTestService=new Mock<IMasterBusinessActivityService>();
            _mockMasterCountryTestService = new MasterCountryService(_masterCountryRepositoryTestRepository);

            //Setup Data
            var testData = new MasterCountryData();
            _testDbContext.MasterCountry.AddRange(testData.MasterCountryEntitiesList);
            _testDbContext.SaveChanges();



        }

        // GetCountryList
        [TestMethod]
        public void GetCountryList_Test()
        {

            //Act 
            var masterCountryRows = _mockMasterCountryTestService.GetCountryList();

            //Assert
            Assert.IsNotNull(masterCountryRows); // Test if null
            Assert.IsTrue(masterCountryRows.Count() == 3);
        }

        [TestMethod]
        public void FindCountryBasedOnCode_NotExist_Test()
        {
            //Act 
            var masterCountryRows = _mockMasterCountryTestService.FindCountryBasedOnCode("AL");

            //Assert
            Assert.IsNotNull(masterCountryRows); // Test if null
            Assert.IsTrue(!masterCountryRows.Any());
        }

        [TestMethod]
        public void FindCountryBasedOnCode_Test()
        {
            //Act 
            var masterCountryRows = _mockMasterCountryTestService.FindCountryBasedOnCode("AD");

            //Assert
            Assert.IsNotNull(masterCountryRows); // Test if null
            Assert.IsTrue(masterCountryRows.Count() == 1);
        }
    }
}
