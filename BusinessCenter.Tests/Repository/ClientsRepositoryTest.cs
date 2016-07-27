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
   public class ClientsRepositoryTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private ClientsRepository _clientsRepository;
        private Mock<IClientsRepository> _mockclientRepository;
        private ClientsRepositoryData _mockData;


        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _clientsRepository = new ClientsRepository(_testUnitOfWork);

            //Mocking Repository
            _mockclientRepository = new Mock<IClientsRepository>();
            _mockData = new ClientsRepositoryData();

            //Setup Data
            var testData = new ClientsRepositoryData();
            _testDbContext.Clients.AddRange(testData.ClientEntitiesList);
            _testDbContext.SaveChanges();

        }
        [TestMethod]
        public void FindClientTest()
        {

            //Act 
            var clientRows = _clientsRepository.FindClient("352549070fb44ce793a5343a5f846dcc");

            //Assert
            Assert.IsNotNull(clientRows); // Test if null
            Assert.IsTrue(clientRows.Count() == 1);
        }
    }
}
