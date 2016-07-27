using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Model;
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
  public  class EtlAddressAndParcelRepositoryTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Repository declaration
        private EtlAddressAndParcelRepository _EtlAddressAndParcelTestRepository;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);

            //EtlAddressAndParcelRepository Initialization
            _EtlAddressAndParcelTestRepository = new EtlAddressAndParcelRepository(_testUnitOfWork);

            //Setup  EtlAddressAndParcelRepository FackData Initialization
            var testData = new EtlAddressAndParcelRepositoryData();
            _testDbContext.TBL_ETL_Address_And_Parcel.AddRange(testData.ETLAddessEntitiesList);
            _testDbContext.SaveChanges();

        }
         [TestMethod]
        public void ListEtlAddressDetails_Return_Test()
        {

            //Act 
            var EtlAddressrows = _EtlAddressAndParcelTestRepository.ListEtlAddressDetails("3411").ToList();

            //Assert
            Assert.IsNotNull(EtlAddressrows); // Test if null
            Assert.IsTrue(EtlAddressrows.Count() == 1);
        }
         [TestMethod]
         public void FindByStreetNumber_Return_Test()
         {

             //Initial Model Details
             var cofohopdetails = new CofoHopDetailsModel { AddressNumber = "600" };
             //Act 
             var EtlAddressrows = _EtlAddressAndParcelTestRepository.FindByStreetNumber(cofohopdetails).ToList();

             //Assert
             Assert.IsNotNull(EtlAddressrows); // Test if null
             Assert.IsTrue(EtlAddressrows.Count() == 1);
         }
         [TestMethod]
         public void FindByStreetName_Return_Test()
         {

             //Initial Model Details
             var cofohopdetails = new CofoHopDetailsModel { AddressNumber = "600", StreetName = "MARYLAND" };
             //Act 
             var EtlAddressrows = _EtlAddressAndParcelTestRepository.FindByStreetName(cofohopdetails).ToList();

             //Assert
             Assert.IsNotNull(EtlAddressrows); // Test if null
             Assert.IsTrue(EtlAddressrows.Count() == 1);
         }
         [TestMethod]
         public void FindByStreetType_Return_Test()
         {

             //Initial Model Details
             var cofohopdetails = new CofoHopDetailsModel { AddressNumber = "600", StreetName = "MARYLAND" };
             //Act 
             var EtlAddressrows = _EtlAddressAndParcelTestRepository.FindByStreetType(cofohopdetails, "AVE").ToList();

             //Assert
             Assert.IsNotNull(EtlAddressrows); // Test if null
             Assert.IsTrue(EtlAddressrows.Count() == 1);
         }
         [TestMethod]
         public void FindByQuadrant_Return_Test()
         {

             //Initial Model Details
             var cofohopdetails = new CofoHopDetailsModel { AddressNumber = "600", StreetName = "MARYLAND", Quadrant = "SW" };
             //Act 
             var EtlAddressrows = _EtlAddressAndParcelTestRepository.FindByQuadrant(cofohopdetails, "AVE").ToList();

             //Assert
             Assert.IsNotNull(EtlAddressrows); // Test if null
             Assert.IsTrue(EtlAddressrows.Count() == 1);
         }
         [TestMethod]
         public void FindByDetails_Return_Test()
         {

             //Initial Model Details
             var cofohopdetails = new CofoHopDetailsModel { AddressNumber = "600", StreetName = "MARYLAND", Quadrant = "SW" };
             //Act 
             var EtlAddressrows = _EtlAddressAndParcelTestRepository.FindByDetails(cofohopdetails, "AVE").ToList();

             //Assert
             Assert.IsNotNull(EtlAddressrows); // Test if null
             Assert.IsTrue(EtlAddressrows.Count() == 1);
         }
    }
}
