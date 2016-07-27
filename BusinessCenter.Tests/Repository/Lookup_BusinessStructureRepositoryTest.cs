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
using BusinessCenter.Data.Model;

namespace BusinessCenter.Tests.Repository
{
    [TestClass]
   public class Lookup_BusinessStructureRepositoryTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private Lookup_BusinessStructureRepository _businessStructureRepository;
        private Mock<ILookup_BusinessStructureRepository> _mockbusinessStructureRepository;
        private Lookup_BusinessStructureRepositoryData _mockData;

        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _businessStructureRepository = new Lookup_BusinessStructureRepository(_testUnitOfWork);

            //Mocking Repository
            _mockbusinessStructureRepository = new Mock<ILookup_BusinessStructureRepository>();
            _mockData = new Lookup_BusinessStructureRepositoryData();

            //Setup Data
            var testData = new Lookup_BusinessStructureRepositoryData();
            _testDbContext.Lookup_BusinessStructure.AddRange(testData.BusienssStructureEntitiesList);
            _testDbContext.SaveChanges();

        }

        [TestMethod]
        public void GetBusinessStructureAllTest()
        {
            //Act 
            var CategoriesRows = _businessStructureRepository.GetBusinessStructureAll();

            //Assert
            Assert.IsNotNull(CategoriesRows); // Test if null
            Assert.IsTrue(CategoriesRows.Count() == 3);
        }
        [TestMethod]
        public void FindByStructureTest()
        {
            //Act 
            var CategoriesRows = _businessStructureRepository.FindByStructure("Corporation (For Profit)");

            //Assert
            Assert.IsNotNull(CategoriesRows); // Test if null
            Assert.IsTrue(CategoriesRows.Count() == 1);
        }

        [TestMethod]
        public void InsertCategoyLookUpTest()
        {
            var businessStructureLook = new BusinessStructureLookUp { BusinessStructure = "Limited Partnership (LP)", IsManualAddress = false };
            //Act 
            var businessRows = _businessStructureRepository.InsertUpdateBusienssStrucutureLookUp(businessStructureLook);

            //Assert
            Assert.IsNotNull(businessRows); // Test if null
            Assert.IsTrue(businessRows);
        }
        [TestMethod]
        public void UpdateCategoyLookUpTest()
        {
            var businessStructureLook = new BusinessStructureLookUp { LookUpBusinessStructureId = 1, BusinessStructure = "Corporation (For Profit)", IsManualAddress = true };
            //Act 
            var businessRows = _businessStructureRepository.InsertUpdateBusienssStrucutureLookUp(businessStructureLook);

            //Assert
            Assert.IsNotNull(businessRows); // Test if null
            Assert.IsTrue(businessRows);
        }
        [TestMethod]
        public void InsertCategoyLookUpNullTest()
        {
            var businessStructureLook = new BusinessStructureLookUp { BusinessStructure = null, IsManualAddress = false };
            //Act 
            var businessRows = _businessStructureRepository.InsertUpdateBusienssStrucutureLookUp(businessStructureLook);

            //Assert
            Assert.IsNotNull(businessRows); // Test if null
            Assert.IsTrue(businessRows);
        }
        [TestMethod]
        public void UpdateCategoyLookUpNullTest()
        {
            var businessStructureLook = new BusinessStructureLookUp { LookUpBusinessStructureId = 1, BusinessStructure = null, IsManualAddress = true };
            //Act 
            var businessRows = _businessStructureRepository.InsertUpdateBusienssStrucutureLookUp(businessStructureLook);

            //Assert
            Assert.IsNotNull(businessRows); // Test if null
            Assert.IsTrue(businessRows);
        }
    }
}
