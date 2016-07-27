using System.Data.Common;
using System.Linq;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Interface;
using BusinessCenter.Tests.Setup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BusinessCenter.Tests.Repository
{
    [TestClass]
    public class CbeRepositoryTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Repository declaration
        private CbeRepository _cbeTestRepository;
        private Mock<ICbeRepository> _mockCbeRepository;
        private CbeRepositoryData _mockData;

        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _cbeTestRepository = new CbeRepository(_testUnitOfWork);

            //Mocking Repository
            _mockCbeRepository = new Mock<ICbeRepository>();
            _mockData = new CbeRepositoryData();

            //Setup FackData Initialization
            var testData = new CbeRepositoryData();
            _testDbContext.DCBC_ENTITY_CBE.AddRange(testData.CbeEntitiesList);
            _testDbContext.SaveChanges();

        }

        [TestMethod]
        public void GetAllCbeTest()
        {

            //Act 
            var cbeRows = _cbeTestRepository.GetCbeLookupAll();

            //Assert
            Assert.IsNotNull(cbeRows); 
            Assert.IsTrue(cbeRows.Count() == 2);
        }
    }
}