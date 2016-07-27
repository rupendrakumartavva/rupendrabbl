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
    public class StreetTypesRepositoryTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private StreetTypesRepository _streetTypesRepository;



        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _streetTypesRepository = new StreetTypesRepository(_testUnitOfWork);

            //Data Setup
            var streetTypesData = new StreetTypesRepositoryData();
            _testDbContext.StreetTypes.AddRange(streetTypesData.ListAll);
            _testDbContext.SaveChanges();
        }


        [TestMethod]
        public void GetAllStreetTypesTest()
        {
            //Act 
            var streetTypesRows = _streetTypesRepository.AllStreetTypes();

            //Assert
            Assert.IsNotNull(streetTypesRows); // Test if null
            Assert.IsTrue(streetTypesRows.Count() == 2);
        }

        [TestMethod]
        public void FindByStreetTypeIdTest()
        {
            //Act 
            var streetTypesRows = _streetTypesRepository.FindByStreetTypeId(1);

            //Assert
            Assert.IsNotNull(streetTypesRows); // Test if null
            Assert.IsTrue(streetTypesRows.Count() == 1);
        }
         [TestMethod]
        public void FindStreetIdbyTypeTest()
        {
            //Act 
            var streetTypesRows = _streetTypesRepository.FindStreetIdbyType("Alley");

            //Assert
            Assert.IsNotNull(streetTypesRows); // Test if null
            Assert.IsTrue(streetTypesRows.Count() == 1);
        }

         [TestMethod]
         public void FindStreetIdbyCodeTest()
         {
             //Act 
             var streetTypesRows = _streetTypesRepository.FindStreetIdbyCode("AL");

             //Assert
             Assert.IsNotNull(streetTypesRows); // Test if null
             Assert.IsTrue(streetTypesRows.Count() == 1);
         }
    }
}
