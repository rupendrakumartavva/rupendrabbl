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
   public class MasterEhopOptionTypeRepositoryTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Repository declaration
        private MasterEhopOptionTypeRepository _masterEhopOptionTypeTestRepository;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);

            //MasterEhopOptionTypeRepository Initialization
            _masterEhopOptionTypeTestRepository = new MasterEhopOptionTypeRepository(_testUnitOfWork);

            //Setup  MasterEhopOptionTypeRepository FackData Initialization
            var testData = new MasterEhopOptionTypeRepositoryData();
            _testDbContext.MasterEhopOptionType.AddRange(testData.MasterEhopOptionTypeEntitiesList);
            _testDbContext.SaveChanges();

        }
        [TestMethod]
        public void FindById_Return_Test()
        {
        

            //Act 
            var ehopRows = _masterEhopOptionTypeTestRepository.FindById(1).ToList();

            //Assert
            Assert.IsNotNull(ehopRows); // Test if null
            Assert.IsTrue(ehopRows.Count() == 1);
        }
    }
}
