using System;
using System.Collections.Generic;
using BusinessCenter.Data.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;
using BusinessCenter.Api.Controllers;
using BusinessCenter.Api.Models;
using BusinessCenter.Api.Utility;
using BusinessCenter.Common;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Model;
using BusinessCenter.Data.Models;
using BusinessCenter.Service.Implementation;
using BusinessCenter.Tests.Repository;
using BusinessCenter.Tests.Setup;
using BusinessCenter.Service.Common;


namespace BusinessCenter.Tests.Controllers
{
     [TestClass]
    public class SearchApiControllerTest
    {

        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private SearchApiController controller = null;


        #region Declaration of Service
        private SearchRepository _searchTestRepository;
        private AbraRepository _abraTestRepository;
        private BblRepository _bblTestRepository;
        private CbeRepository _cbeTestRepository;
        private OplaRepository _oplaTestRepository;
        private CorpRespository _corpTestRepository;
        private SearchKeywordRepository _keywordTestRepository;
        private KeywordDetailsRepository _keywordDetailsTestRepository;
        private UserServicesRepository _userServicesTestRepository;
        private UserRepository _userTestRepository;
         private UserRoleRepository _userRoleRepository;
        private SearchService _searchTestService;
        private SearchServiceInputs _searchServiceInput;
        #endregion

     

        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _searchServiceInput=new SearchServiceInputs();
            _userRoleRepository=new UserRoleRepository(_testUnitOfWork);
            _userTestRepository = new UserRepository(_testUnitOfWork, _userRoleRepository);
            _abraTestRepository = new AbraRepository(_testUnitOfWork);
            _bblTestRepository = new BblRepository(_testUnitOfWork);
            _cbeTestRepository = new CbeRepository(_testUnitOfWork);
            _oplaTestRepository = new OplaRepository(_testUnitOfWork);
            _corpTestRepository = new CorpRespository(_testUnitOfWork);
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

            _searchTestService = new SearchService(_searchTestRepository, _searchServiceInput);

            controller = new SearchApiController(_searchTestService);

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





         

            //_searchTestService = new SearchService(_searchTestRepository, _searchServiceInput);


            ////Setup Data
            //var testData = new SearchMultiColumnLookUpIndexData();
            //_testDbContext.DCBC_ENTITY_MultiColumn_LOOKUP_INDEX.AddRange(testData.MultiColumnLookupIndexEntitiesList);
            //_testDbContext.SaveChanges();

            //// 
            //var abraRepositoryData = new AbraRepositoryData();
            //_testDbContext.DCBC_ENTITY_ABRA.AddRange(abraRepositoryData.AbraEntitiesList);
            //_testDbContext.SaveChanges();

            //// 
            //var bblRepositoryData = new BblRepositoryData();
            //_testDbContext.DCBC_ENTITY_BBL.AddRange(bblRepositoryData.BblEntitiesList);
            //_testDbContext.SaveChanges();

            //// 
            //var cbeRepositoryData = new CbeRepositoryData();
            //_testDbContext.DCBC_ENTITY_CBE.AddRange(cbeRepositoryData.CbeEntitiesList);
            //_testDbContext.SaveChanges();

            //// 
            //var oplaRepositoryData = new OplaRepositoryData();
            //_testDbContext.DCBC_ENTITY_OPLA.AddRange(oplaRepositoryData.OplaEntitiesList);
            //_testDbContext.SaveChanges();

            //// 
            //var corpRepositoryData = new CorpRepositoryData();
            //_testDbContext.DCBC_ENTITY_CORP.AddRange(corpRepositoryData.CorpEntitiesList);
            //_testDbContext.SaveChanges();

            //var userServiceData = new UserServicesRepositoryData();
            //_testDbContext.UserService.AddRange(userServiceData.UserServiceEntitiesList);
            //_testDbContext.SaveChanges();

            //var userData = new UserRepositoryData();
            //_testDbContext.User.AddRange(userData.UsersEntitiesList);
            //_testDbContext.SaveChanges();


            ////Setup Data
            //var keywordDetailsData = new SearchKeywordRepositoryData();
            //_testDbContext.KeywordMaster.AddRange(keywordDetailsData.KeyWordEntitiesList);
            //_testDbContext.SaveChanges();

            ////Setup Data
            //var keywordData = new KeywordDetailsRepositoryData();
            //_testDbContext.KeywordDetails.AddRange(keywordData.KeyDetailsEntitiesList);
            //_testDbContext.SaveChanges();

        }

        [TestMethod]
         public async Task SearchData_Return_Test()
        {
            var searchinput = new SearchServiceInput
            {
                PageIndex = 2,
                DisplayType = "BBL-CORP-OPLA-CBE-ABRA-ALL",
                IsChanged = false,
                SearchType = "BBL-CORP-OPLA-CBE-ABRA-ALL",
                Userid = "A583A22D-FD47-4816-8A2E-5B17702323D4",
                KeyType = "SEARCH",
                CompanyName = "MICHAEL D BALDERAS",
                LicenseName = "ABRA-099577",
                FirstName = "MICHAEL",
                LastName = "MICHAEL"
            };

            var searchRows = await controller.SearchData(searchinput) as JsonResult<IEnumerable<SearchData>>;

            Assert.IsNotNull(searchRows); // Test if null
            var searchRowsFirst = searchRows.Content.FirstOrDefault();
            Assert.IsNotNull(searchRowsFirst.SearchCount);
            //Assert.IsTrue(searchRows.Count() == 1);
        }

        // [TestMethod]
        // public async Task SearchAll_Return_Test()
        //{
        //     // arrange
        //    var searchinput = new SearchInput
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

        //     // act 
        //    var searchRows = await controller.SearchAll(searchinput) as JsonResult<List<CommonData>>;

        //     // assert
        //    Assert.IsNotNull(searchRows);
        //}

         [TestMethod]
         public async Task CompanyNames_Return_Test()
        {
            var inputSearchKeyWord = new AutoFillKeyWord { SearchKeyWord = "A ABLE ACCIDENT ADVOCATE" };

             var searchRows = await controller.CompanyNames(inputSearchKeyWord) as JsonResult<List<string>>;

             Assert.IsNotNull(searchRows);
             Assert.IsTrue(searchRows.Content.FirstOrDefault() == "a able accident advocate");

        }

         [TestMethod]
         public async Task FirstNames_Return_Test()
         {
             var inputSearchKeyWord = new AutoFillKeyWord { SearchKeyWord = "A ABLE ACCIDENT ADVOCATE" };

             var searchRows = await controller.FirstNames(inputSearchKeyWord) as JsonResult<List<string>>;

             Assert.IsNotNull(searchRows);
          //   Assert.IsTrue(searchRows.Content.FirstOrDefault() == "a able accident advocate");

         }

         [TestMethod]
         public async Task LastNames_Return_Test()
         {
             var inputSearchKeyWord = new AutoFillKeyWord { SearchKeyWord = "A ABLE ACCIDENT ADVOCATE" };

             var searchRows = await controller.LastNames(inputSearchKeyWord) as JsonResult<List<string>>;

             Assert.IsNotNull(searchRows);
          //   Assert.IsTrue(searchRows.Content.FirstOrDefault() == "a able accident advocate");

         }

         [TestMethod]
         public void LicenceNumbers_Return_Test()
         {
             var inputSearchKeyWord = new AutoFillKeyWord { SearchKeyWord = "A ABLE ACCIDENT ADVOCATE" };

             var searchRows = controller.LicenceNumbers(inputSearchKeyWord) as JsonResult<List<string>>;

             Assert.IsNotNull(searchRows);
           //  Assert.IsTrue(searchRows.Content.FirstOrDefault() == "a able accident advocate");

         }
    }
}
