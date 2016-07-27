using BusinessCenter.Data;
using BusinessCenter.Data.Interface;
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
    public class AbraRepositoryTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private AbraRepository _abraTestRepository;
        private Mock<IAbraRepository> _mockAbraRepository;
        private AbraRepositoryData _mockData;
       

        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _abraTestRepository = new AbraRepository(_testUnitOfWork);

            //Mocking Repository
            _mockAbraRepository = new Mock<IAbraRepository>();
            _mockData = new AbraRepositoryData();

            //Setup Data
            var testData = new AbraRepositoryData();
            _testDbContext.DCBC_ENTITY_ABRA.AddRange(testData.AbraEntitiesList);
            _testDbContext.SaveChanges();

        }


        [TestMethod]
        public void GetAllAbraEntitiesTest()
        {

            //Act 
            var abraRows = _abraTestRepository.GeArbraLookupAll();

            //Assert
            Assert.IsNotNull(abraRows); // Test if null
            Assert.IsTrue(abraRows.Count() == 2);
        }

        [TestMethod]
        public void GetAllAbraEntitiesMockTest()
        {
            //Setup mock 
            _mockAbraRepository.Setup(x => x.GeArbraLookupAll()).Returns(_mockData.AbraEntitiesList);

            //Act 
            var abraRows = _mockAbraRepository.Object.GeArbraLookupAll();
            //Assert
            Assert.IsNotNull(abraRows); // Test if null
            Assert.IsTrue(abraRows.Count() == 2);
        }
    }
}
