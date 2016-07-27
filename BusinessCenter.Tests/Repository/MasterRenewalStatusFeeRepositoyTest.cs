using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
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
  public  class MasterRenewalStatusFeeRepositoyTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Repository declaration
        private MasterRenewalStatusFeeRepository _masterRenewalStatusFeeTestRepository;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);

            //MasterRenewalStatusFeeRepositoy Initialization
            _masterRenewalStatusFeeTestRepository = new MasterRenewalStatusFeeRepository(_testUnitOfWork);

            //Setup  MasterRenewalStatusFeeRepositoy FackData Initialization
            var testData = new MasterRenewalStatusFeeData();
            _testDbContext.MasterRenewalStatusFee.AddRange(testData.MasterRenewalStatusFeeEntitiesList);
            _testDbContext.SaveChanges();

        }
        [TestMethod]
        public void FindByStatus_Return_Test()
        {

            //Act 
            var renewalStatusFeeRows = _masterRenewalStatusFeeTestRepository.FindByStatus("Lapsed").ToList();

            //Assert
            Assert.IsNotNull(renewalStatusFeeRows); // Test if null
            Assert.IsTrue(renewalStatusFeeRows.Count() == 1);
        }
        [TestMethod]
        public void FindByRange_Return_Test()
        {

            //Act 
            var renewalStatusFeeRows = _masterRenewalStatusFeeTestRepository.FindByRange(50).ToList();

            //Assert
            Assert.IsNotNull(renewalStatusFeeRows); // Test if null
            Assert.IsTrue(renewalStatusFeeRows.Count() == 1);
        }
    }
}
