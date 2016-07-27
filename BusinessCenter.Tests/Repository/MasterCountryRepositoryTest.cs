using System.Data.Common;
using System.Linq;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Interface;
using BusinessCenter.Tests.Setup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BusinessCenter.Tests.Repository
{
     [TestClass]
    public class MasterCountryRepositoryTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Repository declaration
        private MasterCountryRepository _masterCountryRepository;
     

    

        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _masterCountryRepository = new MasterCountryRepository(_testUnitOfWork);



            //Setup FackData Initialization
            var testData = new MasterCountryData();
            _testDbContext.MasterCountry.AddRange(testData.MasterCountryEntitiesList);
            _testDbContext.SaveChanges();

        }

        [TestMethod]
        public void GetAllCountryEntitiesTest()
        {

            //Act 
            var abraRows = _masterCountryRepository.GetCountryList();

            //Assert
            Assert.IsNotNull(abraRows); // Test if null
            Assert.IsTrue(abraRows.Count() == 3);
        }

        [TestMethod]
        public void FindCountryEntitiesTest()
        {

            //Act 
            var abraRows = _masterCountryRepository.FindCountryBasedOnCode("IN");

            //Assert
            Assert.IsNotNull(abraRows); // Test if null
            Assert.IsTrue(abraRows.Count() == 1);
        }
    }
}