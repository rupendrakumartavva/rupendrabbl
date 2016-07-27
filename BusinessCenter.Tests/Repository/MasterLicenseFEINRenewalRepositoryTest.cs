using BusinessCenter.Data;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
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
    public class MasterLicenseFEINRenewalRepositoryTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private MasterLicenseFEINRenewal _masterLicenseFEINRenewalTestRepository;

        private MasterLicenseFEINRenewalRepositoryData _mockData;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);


            _masterLicenseFEINRenewalTestRepository = new MasterLicenseFEINRenewal(_testUnitOfWork);

            //Mocking Repository
            _mockData = new MasterLicenseFEINRenewalRepositoryData();

            //Setup Data
            var testData = new MasterLicenseFEINRenewalRepositoryData();
            _testDbContext.masterLicense_Renewal_TaxRevenue.AddRange(testData.MasterLicenseEntitiesList);
            _testDbContext.SaveChanges();

        }
        [TestMethod]
        public void FindByLicenseTest()
        {
            var renewModel = new RenewModel { LicenseNumber = "100112000001" };
            //Act 
            var licenserows = _masterLicenseFEINRenewalTestRepository.FindByLicense(renewModel);

            //Assert
            Assert.IsNotNull(licenserows); // Test if null
            Assert.IsTrue(licenserows.Count() == 1);
        }
        [TestMethod]
        public void FindByLicenseTaxTest()
        {
       
            //Act 
            var license = _masterLicenseFEINRenewalTestRepository.FindByLicenseTax("100112000001", "11-1111111");

            //Assert
            Assert.IsNotNull(license); // Test if null
            Assert.IsTrue(license == true);
        }
    }
}
