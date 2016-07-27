using BusinessCenter.Data;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Interface;
using BusinessCenter.Tests.Setup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Repository
{
    [TestClass]
    public class MastereHopEligibilityRepositoryTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private MastereHopEligibilityRepository _mastereHopEligibilityTestRepository;

        private MastereHopEligibilityRepositoryData _mockData;

        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);


            _mastereHopEligibilityTestRepository = new MastereHopEligibilityRepository(_testUnitOfWork);

            //Mocking Repository
            _mockData = new MastereHopEligibilityRepositoryData();

            //Setup Data
            var testData = new MastereHopEligibilityRepositoryData();
            _testDbContext.MastereHOPEligibility.AddRange(testData.MastereHOPEligibilityEntitiesList);
            _testDbContext.SaveChanges();

        }
        [TestMethod]
        public void GetMastereHopEligibilityEntitiesTest()
        {
            //Act 
            var eHopRows = _mastereHopEligibilityTestRepository.GeMastereHopEligibility();

            //Assert
            Assert.IsNotNull(eHopRows); // Test if null
            Assert.IsTrue(eHopRows.Count() == 4);
        }
    }
    
}
