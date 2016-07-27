using System;
using BusinessCenter.Data;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Tests.Setup;

namespace BusinessCenter.Tests.Repository
{
    [TestClass]
    public class BblRepositoryTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Repository declaration
        private BblRepository _bblTestRepository;
        private Mock<IBblRepository> _mockBblRepository;
        private BblRepositoryData _mockData;


        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _bblTestRepository = new BblRepository(_testUnitOfWork);

            //Mocking Repository
            _mockBblRepository = new Mock<IBblRepository>();
            _mockData = new BblRepositoryData();

            //Setup FackData Initialization
            var testData = new BblRepositoryData();
            _testDbContext.DCBC_ENTITY_BBL.AddRange(testData.BblEntitiesList);
            _testDbContext.SaveChanges();

        }

        [TestMethod()]
        public void GetBblLookupAllTest()
        {
            //Act 
            var bblRows = _bblTestRepository.GeBblLookupAll();

            //Assert
            Assert.IsNotNull(bblRows); // Test if null
            Assert.IsTrue(bblRows.Count() == 6);
           
        }


        [TestMethod()]
        public void FindByIdBblTest()
        {
            long  dcbc_EntityID = 1;
            //Act 
            var bblRows = _bblTestRepository.FindByID(Convert.ToInt32(dcbc_EntityID)).ToList();

            //Assert
            Assert.IsNotNull(bblRows); // Test if null
            Assert.IsTrue(bblRows.Count() ==1);

        }

        [TestMethod()]
        public void FindByIdBbl_NotExist_Test()
        {
            long dcbc_EntityID = 10;
            //Act 
            var bblRows = _bblTestRepository.FindByID(Convert.ToInt32(dcbc_EntityID)).ToList();

            //Assert
            Assert.IsNotNull(bblRows); // Test if null
            Assert.IsTrue(!bblRows.Any());
        }

        [TestMethod()]
        public void FindByLicenseTest()
        {
            //Act 
            var bblRows = _bblTestRepository.FindByLicense("931315000136");

            //Assert
            Assert.IsNotNull(bblRows); // Test if null
            Assert.IsTrue(bblRows.Count() == 1);

        }

        [TestMethod()]
        public void FindByLicense_NotExist_Test()
        {
            //Act 
            var bblRows = _bblTestRepository.FindByLicense("258315000135");

            //Assert
            Assert.IsNotNull(bblRows); // Test if null
            Assert.IsTrue(!bblRows.Any());

        }

        [TestMethod()]
        public void FindByExpressionTest()
        {
            //Act 
            var bblRows = _bblTestRepository.FindBy(x => x.B1_ALT_ID == "931315000136");
            //    FindByLicense("931315000135");

            //Assert
            Assert.IsNotNull(bblRows); // Test if null
            Assert.IsTrue(bblRows.Count() == 1);
        }

        [TestMethod()]
        public void FindByExpression_NotExist_Test()
        {
            //Act 
            var bblRows = _bblTestRepository.FindBy(x => x.B1_ALT_ID == "825315000135");
            //    FindByLicense("931315000135");

            //Assert
            Assert.IsNotNull(bblRows); // Test if null
            Assert.IsTrue(!bblRows.Any());
        }

        [TestMethod()]
        public void ValidateBblLicenceTest()
        {
            //Act 
            var bblRows = _bblTestRepository.ValidateBblLicence("931315000136");

            //Assert
            Assert.IsNotNull(bblRows); // Test if null
            Assert.IsTrue(bblRows == "IMA PIZZA STORE 13 LLC.");

        }
        [TestMethod()]
        public void ValidateBblLicenceExipredTest()
        {
            //Act 
            var bblRows = _bblTestRepository.ValidateBblLicence("100113000002");

            //Assert
            Assert.IsNotNull(bblRows); // Test if null
            Assert.IsTrue(bblRows == "1723 DUPONT L.L.C.");

        }
        [TestMethod()]
        public void ValidateBblLicenceNoDataTest()
        {
            //Act 
            var bblRows = _bblTestRepository.ValidateBblLicence("100113000000");

            //Assert
            Assert.IsNotNull(bblRows); // Test if null
            Assert.IsTrue(bblRows == "NoData");

        }

        [TestMethod()]
        public void ValidateBblLicenceInActiveTest()
        {
            //Act 
            var bblRows = _bblTestRepository.ValidateBblLicence("100113000003");

            //Assert
            Assert.IsNotNull(bblRows); // Test if null
            Assert.IsTrue(bblRows == "InActive");

        }

        [TestMethod()]
        public void ValidateBblLicenceLapsedTest()
        {
            //Act 
            var bblRows = _bblTestRepository.ValidateBblLicence("931313000266");

            //Assert
            Assert.IsNotNull(bblRows); // Test if null
            Assert.IsTrue(bblRows == "RETROSPECT COFFE AND TEA LLC");

        }

        [TestMethod()]
        public void ValidateBblLicencenullTest()
        {
            //Act 
            var bblRows = _bblTestRepository.ValidateBblLicence("100113000005");

            //Assert
            Assert.IsNotNull(bblRows); // Test if null
            Assert.IsTrue(bblRows == "");

        }

        [TestMethod()]
        public void GetRenewDataTest()
        {
            var getRenuwalData = new RenewModel {EntityId = "1", LicenseNumber = "931315000135"};

            //Act 
            var bblRows = _bblTestRepository.GetRenewData(getRenuwalData).ToList();

            //Assert
            Assert.IsNotNull(bblRows); // Test if null
            Assert.IsTrue(bblRows.Count()==1);
        }

        [TestMethod]
        public void GetRenewData_With_NullObject_Test()
        {
            var getRenuwalData = new RenewModel { EntityId = "10", LicenseNumber = "931315000135" };
            //Act 
            var bblRows = _bblTestRepository.GetRenewData(getRenuwalData).ToList();

            //Assert
            Assert.IsNotNull(bblRows); // Test if null
            Assert.IsTrue(!bblRows.Any());
        }
        [TestMethod()]
        public void FindByLicenseTax_Test()
        {
            var bblassociatepin = new BblAsscoiatePin {
                LicenseNumber = "931313000266",
                CleanHandsType = "FEIN",
                TaxNumber = "217643170"
            };

            //Act 
            var bblRows = _bblTestRepository.FindByLicenseTax(bblassociatepin);

            //Assert
            Assert.IsNotNull(bblRows); // Test if null
            Assert.IsTrue(bblRows);
        }


        [TestMethod()]
        public void DailyMailAlarmToBBlLicenseUsers_Test()
        {
            //Act 
            var bblRows = _bblTestRepository.DailyMailAlarmToBBlLicenseUsers();

            //Assert
            Assert.IsNotNull(bblRows); // Test if null
            Assert.IsTrue(bblRows.Count() == 1);
        }
        
      
    }
}