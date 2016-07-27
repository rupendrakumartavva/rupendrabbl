using BusinessCenter.Data;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Interface;
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
    public class SearchKeywordRepositoryTest
    {
        //Initialization DBConnection
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Declaration  of Repositories
        private SearchKeywordRepository _searchKeywordTestRepository;
        private KeywordDetailsRepository _keyDetailsTestRepository;
        private UserRepository _userTestRepository;
        private UserServicesRepository _userServiceTestRepository;
       private UserRoleRepository _userRoleRepositoryTestRepository;
       private AbraRepository _abraTestRepository;
       private BblRepository _bblTestRepository;
       private CbeRepository _cbeTestRepository;
       private OplaRepository _oplaTestRepository;
       private CorpRespository _corpTestRepository;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);

            //UserRoleRepository Initialization
            _userRoleRepositoryTestRepository = new UserRoleRepository(_testUnitOfWork);

            //AbraRepository Initialization
            _abraTestRepository = new AbraRepository(_testUnitOfWork);

            //CbeRepository Initialization
            _cbeTestRepository = new CbeRepository(_testUnitOfWork);

            //OplaRepository Initialization
            _oplaTestRepository = new OplaRepository(_testUnitOfWork);

            //CorpRespository Initialization
            _corpTestRepository = new CorpRespository(_testUnitOfWork);

            //BblRepository Initialization
            _bblTestRepository = new BblRepository(_testUnitOfWork);

            //KeywordDetailsRepository Initialization
            _keyDetailsTestRepository = new KeywordDetailsRepository(_testUnitOfWork);

            //UserRepository Initialization
            _userTestRepository = new UserRepository(_testUnitOfWork,
                _userRoleRepositoryTestRepository);

          
            //UserServicesRepository Initialization
            _userServiceTestRepository = new UserServicesRepository(_testUnitOfWork, _abraTestRepository, _bblTestRepository,
                  _cbeTestRepository, _oplaTestRepository, _corpTestRepository);

            //SearchKeywordRepository Initialization
            _searchKeywordTestRepository = new SearchKeywordRepository(_testUnitOfWork,
               _keyDetailsTestRepository,
              _userTestRepository,
             _userServiceTestRepository);

            //Setup Data
            var userRoleData = new UserRoleRepositoryData();
            _testDbContext.UserRole.AddRange(userRoleData.UserRolesEntitiesList);
            _testDbContext.SaveChanges();

            var userServiceData = new UserServicesRepositoryData();
            _testDbContext.UserService.AddRange(userServiceData.UserServiceEntitiesList);
            _testDbContext.SaveChanges();

            //Setup Data
            var userData = new UserRepositoryData();
            _testDbContext.User.AddRange(userData.UsersEntitiesList);
            _testDbContext.SaveChanges();

            //Setup Data
            var keywordData = new KeywordDetailsRepositoryData();
            _testDbContext.KeywordDetails.AddRange(keywordData.KeyDetailsEntitiesList);
            _testDbContext.SaveChanges();

            //Setup Data
            var keywordDetailsData = new SearchKeywordRepositoryData();
            _testDbContext.KeywordMaster.AddRange(keywordDetailsData.KeyWordEntitiesList);
            _testDbContext.SaveChanges();
        }
        [TestMethod]
        public void GetKeyMaster_Return_Test()
        {

            //Act 

            var searchRows = _searchKeywordTestRepository.GetKeyMaster().ToList();

            //Assert
            Assert.IsNotNull(searchRows); // Test if null

            Assert.IsTrue(searchRows.Count() == 5);

        }
        [TestMethod]
        public void FindBy_Test()
        {
            //Initialize Entites
            var keywordMasterEntity = new KeywordMaster { KeyId = 1 };

            //Act
            var searchRows = _searchKeywordTestRepository.FindBy(x => x.KeyId == keywordMasterEntity.KeyId).ToList();

            //Assert
            Assert.IsNotNull(searchRows); // Test if null
            Assert.IsTrue(searchRows.Count() == 1);
        }
        [TestMethod]
        public async Task GetKeywordSearchCount_Return_All_Test()
        {
            //Act
            var searchRows = await _searchKeywordTestRepository.GetKeywordSearchCount(DateTime.Now, DateTime.Now, "ALL");

            //Assert
            Assert.IsNotNull(searchRows); // Test if null
           Assert.IsTrue(searchRows.Count() == 2);
        }
        [TestMethod]
        public async Task GetKeywordSearchCount_Return_BusinessName_Test()
        {
            //Act
            var searchRows = await _searchKeywordTestRepository.GetKeywordSearchCount(DateTime.Now, DateTime.Now, "BUSINESSNAME");

            //Assert
            Assert.IsNotNull(searchRows); // Test if null
            Assert.IsTrue(searchRows.Count() == 2);
        }
        [TestMethod]
        public async Task AdminKeywordSearchCount_Return_All_Test()
        {
            //Act
            var searchRows = await _searchKeywordTestRepository.AdminKeywordSearchCount(DateTime.Now, DateTime.Now, "ALL");

            //Assert
            Assert.IsNotNull(searchRows); // Test if null
            Assert.IsTrue(searchRows.Count() == 2);
        }
        [TestMethod]
        public async Task AdminKeywordSearchCount_Return_BusinessName_Test()
        {
            //Act
            var searchRows = await _searchKeywordTestRepository.AdminKeywordSearchCount(DateTime.Now, DateTime.Now, "BUSINESSNAME");

            //Assert
            Assert.IsNotNull(searchRows); // Test if null
            Assert.IsTrue(searchRows.Count() == 2);
        }
        [TestMethod]
        public async Task GetDashBoardInvdvidualcountsCount_Return_Test()
        {
            //Act
            var searchRows = await _searchKeywordTestRepository.GetDashBoardInvdvidualcountsCount();

            //Assert
            Assert.IsNotNull(searchRows); // Test if null
            Assert.IsTrue(searchRows.First().Businesscount=="2");
        }
        [TestMethod]
        public async Task GetDashBoardRegulatorCount_Return_Test()
        {
            //Act
            var searchRows = await _searchKeywordTestRepository.GetDashBoardRegulatorCount();

            //Assert
            Assert.IsNotNull(searchRows); // Test if null
            Assert.IsTrue(searchRows.Count()==5);
        }
    }
}
