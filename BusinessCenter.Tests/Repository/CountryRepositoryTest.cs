using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Interface;
using BusinessCenter.Tests.Setup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BusinessCenter.Tests.Repository
{
    [TestClass]
  public class CountryRepositoryTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private CountryRepository _countryRepository;
        private Mock<ICountryRepository> _mockcountryRepository;
        private CountryRepositoryData _mockData;


        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _countryRepository = new CountryRepository(_testUnitOfWork);

            //Mocking Repository
            _mockcountryRepository = new Mock<ICountryRepository>();
            _mockData = new CountryRepositoryData();

            //Setup Data
            var testData = new CountryRepositoryData();
            _testDbContext.MasterCountry.AddRange(testData.CountryEntitiesList);
            _testDbContext.SaveChanges();
        }

        [TestMethod]
        public void GetAllCountriesTest()
        {

            //Act 
            var countryRows = _countryRepository.GetAllCountries();

            //Assert
            Assert.IsNotNull(countryRows); // Test if null
            Assert.IsTrue(countryRows.Count() == 2);
        }
    }
}
