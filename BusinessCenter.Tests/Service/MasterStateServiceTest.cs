using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
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
  public  class MasterStateServiceTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Repository declaration
        private MasterStateRepository _masterStateTestRepository;

        //Service declaration
        private MasterStateService _masterStateTestService;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);

            //MasterStateRepository Initialization
            _masterStateTestRepository = new MasterStateRepository(_testUnitOfWork);

            _masterStateTestService = new MasterStateService(_masterStateTestRepository);

            //Setup  MasterState FackData Initialization
            var testData = new MasterStateRepositoryData();
            _testDbContext.MasterState.AddRange(testData.MasterStateEntitiesList);
            _testDbContext.SaveChanges();

        }
        [TestMethod]
        public void GetFindByIdTest()
        {

            //Act 
            var stateRows = _masterStateTestService.FindById("AK");

            //Assert
            Assert.IsNotNull(stateRows); // Test if null
            Assert.IsTrue(stateRows.Count() == 1);
        }
        [TestMethod]
        public void StateListFindByCountryTest()
        {

            //Act 
            var stateRows = _masterStateTestService.StatesFindByCountry("US").ToList();

            //Assert
            Assert.IsNotNull(stateRows); // Test if null
            Assert.IsTrue(stateRows.Count() == 5);
        }
    }
}
