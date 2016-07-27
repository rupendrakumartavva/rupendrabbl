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
    public class FixFeeRepositoryTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private FixFeeRepository _fixedFeeTestRepository;
       

        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _fixedFeeTestRepository = new FixFeeRepository(_testUnitOfWork);

         

            //Setup Data
            var testData1 = new FixFeeRepositoryData();
            _testDbContext.FixFee.AddRange(testData1.FixFeeEntitiesList);
            _testDbContext.SaveChanges();

        }

        [TestMethod]
        public void GetAllFixFeeEntitiesTest()
        {

            //Act 
            var fixedFeeRows = _fixedFeeTestRepository.AllFixFees();

            //Assert
            Assert.IsNotNull(fixedFeeRows); // Test if null
            Assert.IsTrue(fixedFeeRows.Count() == 1);
        }

       
    }
}
