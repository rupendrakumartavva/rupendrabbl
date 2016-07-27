using BusinessCenter.Data;
using BusinessCenter.Data.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data.Models;
using System.Data.Common;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Tests.Setup;

namespace BusinessCenter.Tests.Repository
{
    [TestClass]
   public class SearchRepositoryTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private SearchRepository _searchTestRepository;

        private AbraRepository _abraTestRepository;
        private BblRepository _bblTestRepository;
        private CbeRepository _cbeTestRepository;
        private OplaRepository _oplaTestRepository;
        private CorpRespository _corpTestRepository;
        private SearchKeywordRepository _keywordTestRepository;
        private KeywordDetailsRepository _keywordDetailsTestRepository;
        private UserServicesRepository _userServicesTestRepository;
        private UserRoleRepository _userRoleRepository;
        private UserRepository _userTestRepository;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);


            _abraTestRepository = new AbraRepository(_testUnitOfWork);
            _bblTestRepository = new BblRepository(_testUnitOfWork);
            _cbeTestRepository = new CbeRepository(_testUnitOfWork);
            _oplaTestRepository = new OplaRepository(_testUnitOfWork);
            _corpTestRepository = new CorpRespository(_testUnitOfWork);
            _userRoleRepository=new UserRoleRepository(_testUnitOfWork);
            _userTestRepository = new UserRepository(_testUnitOfWork, _userRoleRepository);
            _keywordTestRepository = new SearchKeywordRepository(_testUnitOfWork, _keywordDetailsTestRepository, _userTestRepository, _userServicesTestRepository);
            _keywordDetailsTestRepository = new KeywordDetailsRepository(_testUnitOfWork);
            _userServicesTestRepository = new UserServicesRepository(_testUnitOfWork, _abraTestRepository, _bblTestRepository,
                _cbeTestRepository, _oplaTestRepository, _corpTestRepository);

            _searchTestRepository = new SearchRepository(_testUnitOfWork,
                _abraTestRepository,
                _bblTestRepository,
                _cbeTestRepository,
                _oplaTestRepository,
                _corpTestRepository,
                _keywordTestRepository,
                _keywordDetailsTestRepository,
                _userServicesTestRepository);

          

            //Setup Data
            var testData = new SearchMultiColumnLookUpIndexData();
            _testDbContext.DCBC_ENTITY_MultiColumn_LOOKUP_INDEX.AddRange(testData.MultiColumnLookupIndexEntitiesList);
            _testDbContext.SaveChanges();

            // 
            var abraRepositoryData = new AbraRepositoryData();
            _testDbContext.DCBC_ENTITY_ABRA.AddRange(abraRepositoryData.AbraEntitiesList);
            _testDbContext.SaveChanges();

            // 
            var bblRepositoryData = new BblRepositoryData();
            _testDbContext.DCBC_ENTITY_BBL.AddRange(bblRepositoryData.BblEntitiesList);
            _testDbContext.SaveChanges();

            // 
            var cbeRepositoryData = new CbeRepositoryData();
            _testDbContext.DCBC_ENTITY_CBE.AddRange(cbeRepositoryData.CbeEntitiesList);
            _testDbContext.SaveChanges();

            // 
            var oplaRepositoryData = new OplaRepositoryData();
            _testDbContext.DCBC_ENTITY_OPLA.AddRange(oplaRepositoryData.OplaEntitiesList);
            _testDbContext.SaveChanges();

            // 
            var corpRepositoryData = new CorpRepositoryData();
            _testDbContext.DCBC_ENTITY_CORP.AddRange(corpRepositoryData.CorpEntitiesList);
            _testDbContext.SaveChanges();

            var userServiceData = new UserServicesRepositoryData();
            _testDbContext.UserService.AddRange(userServiceData.UserServiceEntitiesList);
            _testDbContext.SaveChanges();

            var userData = new UserRepositoryData();
            _testDbContext.User.AddRange(userData.UsersEntitiesList);
            _testDbContext.SaveChanges();


            //Setup Data
            var keywordDetailsData = new SearchKeywordRepositoryData();
            _testDbContext.KeywordMaster.AddRange(keywordDetailsData.KeyWordEntitiesList);
            _testDbContext.SaveChanges();

            //Setup Data
            var keywordData = new KeywordDetailsRepositoryData();
            _testDbContext.KeywordDetails.AddRange(keywordData.KeyDetailsEntitiesList);
            _testDbContext.SaveChanges();
        }
        //[TestMethod]
        //public void GetMultiColumnLookupAll_Return_Test()
        //{

        //    //Act 

        //    var searchRows = _searchTestRepository.GetMultiColumnLookupAll().ToList();

        //    //Assert
        //    Assert.IsNotNull(searchRows); // Test if null

        //    Assert.IsTrue(searchRows.Count() == 10);

        //}

     
        [TestMethod]
        public void GetLicenceNumber_Returns_Test()
        {
            //Initialize Entites
            var inputSearchKeyWord = new AutoFillKeyWord { SearchKeyWord = "ABRA" };

            //Act 
            var searchRows = _searchTestRepository.GetLicenceNumber(inputSearchKeyWord).ToList();


            //Assert
            Assert.IsNotNull(searchRows); // Test if null
            Assert.IsTrue(searchRows.Count()==2);
        }
        [TestMethod]
        public async Task GetCompanyName_Returns_Test()
        {


            //Initialize Entites
            var inputSearchKeyWord = new AutoFillKeyWord { SearchKeyWord = "AABLEACCIDENTADVOCATE" };

            //Act 
            var searchRows = await _searchTestRepository.GetCompanyName(inputSearchKeyWord);


            //Assert
            Assert.IsNotNull(searchRows); // Test if null
            Assert.IsTrue(searchRows.Count() == 1);

        }
        [TestMethod]
        public async Task GetFirstName_Returns_Test()
        {


            //Initialize Entites
            var inputSearchKeyWord = new AutoFillKeyWord { SearchKeyWord = "TAYLOR" };

            //Act 
            var searchRows = await _searchTestRepository.GetFirstName(inputSearchKeyWord);


            //Assert
            Assert.IsNotNull(searchRows); // Test if null
            Assert.IsTrue(searchRows.Count() == 1);

        }
        [TestMethod]
        public async Task GetLastName_Returns_Test()
        {


            //Initialize Entites
            var inputSearchKeyWord = new AutoFillKeyWord { SearchKeyWord = "MICHAEL" };

            //Act 
            var searchRows = await _searchTestRepository.GetLastName(inputSearchKeyWord);


            //Assert
            Assert.IsNotNull(searchRows); // Test if null
            Assert.IsTrue(searchRows.Count() == 1);

        }
        [TestMethod]
        public async Task GetSearchData_Returns_Test()
        {


            //Initialize Entites
            var searchinput = new SearchServiceInputs { PageIndex = 2, DisplayType = "BBL-CORP-OPLA-CBE-ABRA-ALL", IsChanged =false, SearchType = "BBL-CORP-OPLA-CBE-ABRA-ALL", Userid = "A583A22D-FD47-4816-8A2E-5B17702323D4",KeyType="SEARCH",
                                                        CompanyName = "MICHAEL D BALDERAS",
                                                        LicenseName = "ABRA-099577",
                                                        FirstName = "MICHAEL",
                                                        LastName = "MICHAEL"
            };

            //Act 
            var searchRows = await _searchTestRepository.GetSearchData(searchinput);


            //Assert
            Assert.IsNotNull(searchRows); // Test if null
            Assert.IsTrue(searchRows.Count() == 1);

        }
        [TestMethod]
        public async Task GetSearchData__Test()
        {


            //Initialize Entites
            var searchinput = new SearchServiceInputs
            {
                PageIndex = 2,
                DisplayType = "BBL-CORP-OPLA-CBE-ABRA-ALL",
                IsChanged = false,
                SearchType = "BBL-CORP-OPLA-CBE-ABRA-ALL",
                Userid = "A583A22D-FD47-4816-8A2E-5B17702323D4",
                KeyType = "SEARCH",
                CompanyName = "ABC",
                LicenseName = "ABRA-099577",
                FirstName = "MICHAEL",
                LastName = "MICHAEL"
            };

            //Act 
            var searchRows = await _searchTestRepository.GetSearchData(searchinput);


            //Assert
            Assert.IsNotNull(searchRows); // Test if null
            Assert.IsTrue(searchRows.Count() == 1);

        }
        //[TestMethod]
        //public async Task GetAllData_Returns_Test()
        //{


        //    //Initialize Entites
        //    var searchinput = new SearchServiceInputs { PageIndex = 2, DisplayType = "BBL-CORP-OPLA-CBE-ABRA-ALL", IsChanged = true, SearchType = "BBL-CORP-OPLA-CBE-ABRA-ALL", Userid = "A583A22D-FD47-4816-8A2E-5B17702323D4",
        //                                                CompanyName = "MICHAEL D BALDERAS",
        //                                                LicenseName = "ABRA-099577",
        //                                                FirstName = "MICHAEL",
        //                                                LastName = "MICHAEL"
        //    };

        //    //Act 
        //    var searchRows = await _searchTestRepository.GetAllData(searchinput);


        //    //Assert
        //    Assert.IsNotNull(searchRows); // Test if null
        //    //Assert.IsTrue(searchRows.Count() == 3);

        //}

        //[TestMethod]
        //public async Task GetSearchPdfData_Test()
        //{


        //    //Initialize Entites
        //    var searchinput = new SearchServiceInputs
        //    {
        //        PageIndex = 2,
        //        DisplayType = "BBL-CORP-OPLA-CBE-ABRA-ALL",
        //        IsChanged = true,
        //        SearchType = "BBL-CORP-OPLA-CBE-ABRA-ALL",
        //        Userid = "A583A22D-FD47-4816-8A2E-5B17702323D4",
        //        CompanyName = "MICHAEL D BALDERAS",
        //        LicenseName = "ABRA-099577",
        //        FirstName = "MICHAEL",
        //        LastName = "MICHAEL"
        //    };

        //    //Act 
        //    var searchRows = await _searchTestRepository.GetSearchPdfData(searchinput);


        //    //Assert
        //    Assert.IsNotNull(searchRows); // Test if null
        //    //Assert.IsTrue(searchRows.Count() == 3);

        //}
    
    }
}
