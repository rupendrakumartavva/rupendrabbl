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
  public  class EtlAddressAndParcelServiceTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Repository declaration
        private EtlAddressAndParcelRepository _EtlAddressAndParcelTestRepository;
          //Service declaration
        private EtlAddressAndParcelService _EtlAddressAndParcelTestService;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);

            //EtlAddressAndParcelRepository Initialization
            _EtlAddressAndParcelTestRepository = new EtlAddressAndParcelRepository(_testUnitOfWork);

            //EtlAddressAndParcelService Initialization
            _EtlAddressAndParcelTestService = new EtlAddressAndParcelService(_EtlAddressAndParcelTestRepository);

            //Setup  EtlAddressAndParcelRepository FackData Initialization
            var testData = new EtlAddressAndParcelRepositoryData();
            _testDbContext.TBL_ETL_Address_And_Parcel.AddRange(testData.ETLAddessEntitiesList);
            _testDbContext.SaveChanges();

        }
        [TestMethod]
        public void ListEtlAddressDetails_Return_Test()
        {

            //Act 
            var EtlAddressrows = _EtlAddressAndParcelTestService.ListEtlAddressDetails("3411").ToList();

            //Assert
            Assert.IsNotNull(EtlAddressrows); // Test if null
            Assert.IsTrue(EtlAddressrows.Count() == 1);
        }
    }
}
