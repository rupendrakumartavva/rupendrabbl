using BusinessCenter.Data;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Models;
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
    public class MultiColumnFiltersTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
       // private UnitOfWork _testUnitOfWork;

        [TestInitialize]
        public void Initialize()
        {
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            // _MultiColumnFiltersTest = new MultiColumnFilters();
        }

        [TestMethod]
        public void MySavedListTest()
        {
            //  MultiColumnFilters multicolu;
            //Act
            var getRenuwalData = new UserService { DATA_SOURCE = "10", DCBC_ENTITY_ID = 1 };
            List<UserService> userlist = new List<UserService>();
            userlist.Add(getRenuwalData);
            var savedlist = MultiColumnFilters.MySavedList(1, userlist);

            //Assert
            Assert.IsNotNull(savedlist); // Test if null
            Assert.IsTrue(savedlist);
        }

        [TestMethod]
        public void GetFilterDataTest()
        {
            //  MultiColumnFilters multicolu;
            //Act
            SearchResultData srd=new SearchResultData();
            var searchinput = new SearchServiceInputs { CompanyName = "L", LicenseName = "", FirstName = "", LastName = "", SearchType = "All" };
            List<SearchServiceInputs> searchinputs = new List<SearchServiceInputs>();
            searchinputs.Add(searchinput);
            var userlist = new UserService { DCBC_ENTITY_ID = 20007555 };
            List<UserService> userServiceData = new List<UserService>();
            userServiceData.Add(userlist);
            var multicolumndata = new DCBC_ENTITY_MultiColumn_LOOKUP_INDEXData();
            _testDbContext.DCBC_ENTITY_MultiColumn_LOOKUP_INDEX.AddRange(multicolumndata.LookupEntitiesList);
            _testDbContext.SaveChanges();
            List<CommonData> commndata = new List<CommonData>();
            var commondata1 = new CommonData { id = 11, EntityID = 1, Source = "ABRA", CompanyName = "Purple Patch", WishList = false };
            commndata.Add(commondata1);
            var commondata2 = new CommonData { id = 12, EntityID = 2, Source = "ABRA", CompanyName = "Purple Patch", WishList = false };
            commndata.Add(commondata2);
            var commondata3 = new CommonData { id = 7, EntityID = 1, Source = "BBL", CompanyName = "IMA PIZZA STORE 13 LLC.", WishList = false };
            commndata.Add(commondata3);
            var commondata4 = new CommonData { id = 1, EntityID = 2, Source = "BBL", CompanyName = "IMA PIZZA  13 LLC.", WishList = false };
            commndata.Add(commondata4);
            var commondata5 = new CommonData { id = 2, EntityID = 3, Source = "BBL", CompanyName = "PIZZA STORE 13 LLC.", WishList = false };
            commndata.Add(commondata5);
            var commondata6 = new CommonData { id = 3, EntityID = 4, Source = "BBL", CompanyName = "IMA STORE 13 LLC.", WishList = false };
            commndata.Add(commondata6);
            var commondata7 = new CommonData { id = 4, EntityID = 5, Source = "BBL", CompanyName = "STORE 13 LLC.", WishList = false };
            commndata.Add(commondata7);
            var commondata8 = new CommonData { id = 13, EntityID = 6, Source = "BBL", CompanyName = "IMA 13 LLC.", WishList = false };
            commndata.Add(commondata8);
            var commondata9 = new CommonData { id = 8, EntityID = 1, Source = "CBE", CompanyName = "Jones Electric Company, Inc.", WishList = false };
            commndata.Add(commondata9);
            var commondata10 = new CommonData { id = 14, EntityID = 2, Source = "CBE", CompanyName = "RETROSPECT COFFE AND TEA LLC", WishList = false };
            commndata.Add(commondata10);
            var commondata11 = new CommonData { id = 15, EntityID = 1, Source = "CORP", CompanyName = "2118 14TH STREET", WishList = false };
            commndata.Add(commondata11);
            var commondata12 = new CommonData { id = 5, EntityID = 2, Source = "CORP", CompanyName = "A ABLE ACCIDENT ADVOCATE", WishList = false };
            commndata.Add(commondata12);
            var commondata13 = new CommonData { id = 6, EntityID = 3, Source = "CORP", CompanyName = "DESO & BUCKLEY", WishList = false };
            commndata.Add(commondata13);
            var commondata14 = new CommonData { id = 10, EntityID = 1, Source = "OPLA", CompanyName = "Patch", WishList = false };
            commndata.Add(commondata14);
            var commondata15 = new CommonData { id = 9, EntityID = 2, Source = "OPLA", CompanyName = "Purple", WishList = false };
            commndata.Add(commondata15);

            var abradata = new AbraRepositoryData();
            _testDbContext.DCBC_ENTITY_ABRA.AddRange(abradata.AbraEntitiesList);
            _testDbContext.SaveChanges();
            srd.AbraData = abradata.AbraEntitiesList.ToList();

            var bbldata = new BblRepositoryData();
            _testDbContext.DCBC_ENTITY_BBL.AddRange(bbldata.BblEntitiesList);
            _testDbContext.SaveChanges();
            srd.BblData = bbldata.BblEntitiesList.ToList();

            var cbedata = new CbeRepositoryData();
            _testDbContext.DCBC_ENTITY_CBE.AddRange(cbedata.CbeEntitiesList);
            _testDbContext.SaveChanges();
            srd.CbeData = cbedata.CbeEntitiesList.ToList();

            var corpdata = new CorpRepositoryData();
            _testDbContext.DCBC_ENTITY_CORP.AddRange(corpdata.CorpEntitiesList);
            _testDbContext.SaveChanges();
            srd.CorpData = corpdata.CorpEntitiesList.ToList();

            var opladata = new OplaRepositoryData();
            _testDbContext.DCBC_ENTITY_OPLA.AddRange(opladata.OplaEntitiesList);
            _testDbContext.SaveChanges();
            srd.OplaData = opladata.OplaEntitiesList.ToList();

            //   var multi = multicolumndata.LookupEntitiesList.AsQueryable();

            //  IQueryable<DCBC_ENTITY_MultiColumn_LOOKUP_INDEX> multicolumn = multicolumndata.AsQueryable();
            //IAbraRepository abrarep;
            //IBblRepository bblrep;
            //ICbeRepository cberep;
            //ICorpRespository corprep;
            //IOplaRepository oplarep;

            //    MultiColumnFilters.GetFilterData(searchinput, multicolumndata.LookupEntitiesList.AsQueryable(), userServiceData);

            //Assert
            // Assert.IsNotNull(savedlist); // Test if null
            //  Assert.IsTrue(savedlist);
        }

        [TestMethod]
        public void PdfDownloadDataTest()
        {
            //  MultiColumnFilters multicolu;
            //Act
            SearchResultData srd=new SearchResultData();
            var searchinput = new SearchServiceInputs { CompanyName = "L", LicenseName = "", FirstName = "", LastName = "", SearchType = "All" };
            List<SearchServiceInputs> searchinputs = new List<SearchServiceInputs>();
            searchinputs.Add(searchinput);
            var userlist = new UserService { DCBC_ENTITY_ID = 20007555 };
            List<UserService> userServiceData = new List<UserService>();
            userServiceData.Add(userlist);
            var multicolumndata = new DCBC_ENTITY_MultiColumn_LOOKUP_INDEXData();
            _testDbContext.DCBC_ENTITY_MultiColumn_LOOKUP_INDEX.AddRange(multicolumndata.LookupEntitiesList);
            _testDbContext.SaveChanges();
            List<CommonData> commndata = new List<CommonData>();
            var commondata1 = new CommonData { id = 11, EntityID = 20007555, Source = "ABRA", CompanyName = "Purple Patch", WishList = false };
            commndata.Add(commondata1);
            var commondata2 = new CommonData { id = 12, EntityID = 20006725, Source = "ABRA", CompanyName = "Purple Patch", WishList = false };
            commndata.Add(commondata2);
            var commondata3 = new CommonData { id = 7, EntityID = 1, Source = "BBL", CompanyName = "IMA PIZZA STORE 13 LLC.", WishList = false };
            commndata.Add(commondata3);
            var commondata4 = new CommonData { id = 1, EntityID = 2, Source = "BBL", CompanyName = "IMA PIZZA  13 LLC.", WishList = false };
            commndata.Add(commondata4);
            var commondata5 = new CommonData { id = 2, EntityID = 3, Source = "BBL", CompanyName = "PIZZA STORE 13 LLC.", WishList = false };
            commndata.Add(commondata5);
            var commondata6 = new CommonData { id = 3, EntityID = 4, Source = "BBL", CompanyName = "IMA STORE 13 LLC.", WishList = false };
            commndata.Add(commondata6);
            var commondata7 = new CommonData { id = 4, EntityID = 5, Source = "BBL", CompanyName = "STORE 13 LLC.", WishList = false };
            commndata.Add(commondata7);
            var commondata8 = new CommonData { id = 13, EntityID = 6, Source = "BBL", CompanyName = "IMA 13 LLC.", WishList = false };
            commndata.Add(commondata8);
            var commondata9 = new CommonData { id = 8, EntityID = 30000001, Source = "CBE", CompanyName = "Jones Electric Company, Inc.", WishList = false };
            commndata.Add(commondata9);
            var commondata10 = new CommonData { id = 14, EntityID = 30000000, Source = "CBE", CompanyName = "RETROSPECT COFFE AND TEA LLC", WishList = false };
            commndata.Add(commondata10);
            var commondata11 = new CommonData { id = 15, EntityID = 40000000, Source = "CORP", CompanyName = "2118 14TH STREET", WishList = false };
            commndata.Add(commondata11);
            var commondata12 = new CommonData { id = 5, EntityID = 40000001, Source = "CORP", CompanyName = "A ABLE ACCIDENT ADVOCATE", WishList = false };
            commndata.Add(commondata12);
            var commondata13 = new CommonData { id = 6, EntityID = 40000002, Source = "CORP", CompanyName = "DESO & BUCKLEY", WishList = false };
            commndata.Add(commondata13);
            var commondata14 = new CommonData { id = 10, EntityID = 50000000, Source = "OPLA", CompanyName = "Patch", WishList = false };
            commndata.Add(commondata14);
            var commondata15 = new CommonData { id = 9, EntityID = 50000009, Source = "OPLA", CompanyName = "Purple", WishList = false };
            commndata.Add(commondata15);

            var abradata = new AbraRepositoryData();
            _testDbContext.DCBC_ENTITY_ABRA.AddRange(abradata.AbraEntitiesList);
            _testDbContext.SaveChanges();
            srd.AbraData = abradata.AbraEntitiesList.ToList();

            var bbldata = new BblRepositoryData();
            _testDbContext.DCBC_ENTITY_BBL.AddRange(bbldata.BblEntitiesList);
            _testDbContext.SaveChanges();
            srd.BblData = bbldata.BblEntitiesList.ToList();

            var cbedata = new CbeRepositoryData();
            _testDbContext.DCBC_ENTITY_CBE.AddRange(cbedata.CbeEntitiesList);
            _testDbContext.SaveChanges();
            srd.CbeData = cbedata.CbeEntitiesList.ToList();

            var corpdata = new CorpRepositoryData();
            _testDbContext.DCBC_ENTITY_CORP.AddRange(corpdata.CorpEntitiesList);
            _testDbContext.SaveChanges();
            srd.CorpData = corpdata.CorpEntitiesList.ToList();

            var opladata = new OplaRepositoryData();
            _testDbContext.DCBC_ENTITY_OPLA.AddRange(opladata.OplaEntitiesList);
            _testDbContext.SaveChanges();
            srd.OplaData = opladata.OplaEntitiesList.ToList();

            //   var multi = multicolumndata.LookupEntitiesList.AsQueryable();

            //  IQueryable<DCBC_ENTITY_MultiColumn_LOOKUP_INDEX> multicolumn = multicolumndata.AsQueryable();
            //IAbraRepository abrarep;
            //IBblRepository bblrep;
            //ICbeRepository cberep;
            //ICorpRespository corprep;
            //IOplaRepository oplarep;

            // MultiColumnFilters.PdfDownloadData(searchinput, multicolumndata.LookupEntitiesList.AsQueryable(), userServiceData);

            //Assert
            // Assert.IsNotNull(savedlist); // Test if null
            //  Assert.IsTrue(savedlist);
        }

        //
        //[TestMethod]
        //public void WithDataSourceTest()
        //{
        //    var multicolumndata = new DCBC_ENTITY_MultiColumn_LOOKUP_INDEXData();
        //    _testDbContext.DCBC_ENTITY_MultiColumn_LOOKUP_INDEX.AddRange(multicolumndata.LookupEntitiesList);
        //    _testDbContext.SaveChanges();
        //    //Act
        //    var datasource = MultiColumnFilters.WithDataSource("l");

        //    //Assert
        //    Assert.IsNotNull(datasource); // Test if null
        //    // Assert.IsTrue(datasource);
        //}

        [TestMethod]
        public void GetFilterData_null_Test()
        {
            //  MultiColumnFilters multicolu;
            //Act

            var searchinput = new SearchServiceInputs { CompanyName = "", LicenseName = "", FirstName = "", LastName = "", SearchType = "All" };
            List<SearchServiceInputs> searchinputs = new List<SearchServiceInputs>();
            searchinputs.Add(searchinput);
            var userlist = new UserService { DCBC_ENTITY_ID = 20007555 };
            List<UserService> userServiceData = new List<UserService>();
            userServiceData.Add(userlist);
            var multicolumndata = new DCBC_ENTITY_MultiColumn_LOOKUP_INDEXData();
            _testDbContext.DCBC_ENTITY_MultiColumn_LOOKUP_INDEX.AddRange(multicolumndata.LookupEntitiesList);
            _testDbContext.SaveChanges();
            List<CommonData> commndata = new List<CommonData>();
            var commondata1 = new CommonData { id = 1, EntityID = 20007555, Source = "ABRA", CompanyName = "Purple Patch", WishList = false };
            commndata.Add(commondata1);
            var commondata2 = new CommonData { id = 2, EntityID = 20006725, Source = "ABRA", CompanyName = "Purple Patch", WishList = false };
            commndata.Add(commondata2);
            var commondata3 = new CommonData { id = 3, EntityID = 1, Source = "BBL", CompanyName = "IMA PIZZA STORE 13 LLC.", WishList = false };
            commndata.Add(commondata3);
            var commondata4 = new CommonData { id = 4, EntityID = 2, Source = "BBL", CompanyName = "IMA PIZZA  13 LLC.", WishList = false };
            commndata.Add(commondata4);
            var commondata5 = new CommonData { id = 5, EntityID = 3, Source = "BBL", CompanyName = "PIZZA STORE 13 LLC.", WishList = false };
            commndata.Add(commondata5);
            var commondata6 = new CommonData { id = 6, EntityID = 4, Source = "BBL", CompanyName = "IMA STORE 13 LLC.", WishList = false };
            commndata.Add(commondata6);
            var commondata7 = new CommonData { id = 7, EntityID = 5, Source = "BBL", CompanyName = "STORE 13 LLC.", WishList = false };
            commndata.Add(commondata7);
            var commondata8 = new CommonData { id = 8, EntityID = 6, Source = "BBL", CompanyName = "IMA 13 LLC.", WishList = false };
            commndata.Add(commondata8);
            var commondata9 = new CommonData { id = 9, EntityID = 30000001, Source = "CBE", CompanyName = "Jones Electric Company, Inc.", WishList = false };
            commndata.Add(commondata9);
            var commondata10 = new CommonData { id = 10, EntityID = 30000000, Source = "CBE", CompanyName = "RETROSPECT COFFE AND TEA LLC", WishList = false };
            commndata.Add(commondata10);
            var commondata11 = new CommonData { id = 11, EntityID = 40000000, Source = "CORP", CompanyName = "2118 14TH STREET", WishList = false };
            commndata.Add(commondata11);
            var commondata12 = new CommonData { id = 12, EntityID = 40000001, Source = "CORP", CompanyName = "A ABLE ACCIDENT ADVOCATE", WishList = false };
            commndata.Add(commondata12);
            var commondata13 = new CommonData { id = 13, EntityID = 40000002, Source = "CORP", CompanyName = "DESO & BUCKLEY", WishList = false };
            commndata.Add(commondata13);
            var commondata14 = new CommonData { id = 14, EntityID = 50000000, Source = "OPLA", CompanyName = "Patch", WishList = false };
            commndata.Add(commondata14);
            var commondata15 = new CommonData { id = 15, EntityID = 50000009, Source = "OPLA", CompanyName = "Purple", WishList = false };
            commndata.Add(commondata15);
            SearchResultData srd=new SearchResultData();
            var abradata = new AbraRepositoryData();
            _testDbContext.DCBC_ENTITY_ABRA.AddRange(abradata.AbraEntitiesList);
            _testDbContext.SaveChanges();
            srd.AbraData = abradata.AbraEntitiesList.ToList();

            var bbldata = new BblRepositoryData();
            _testDbContext.DCBC_ENTITY_BBL.AddRange(bbldata.BblEntitiesList);
            _testDbContext.SaveChanges();
            srd.BblData = bbldata.BblEntitiesList.ToList();

            var cbedata = new CbeRepositoryData();
            _testDbContext.DCBC_ENTITY_CBE.AddRange(cbedata.CbeEntitiesList);
            _testDbContext.SaveChanges();
            srd.CbeData = cbedata.CbeEntitiesList.ToList();

            var corpdata = new CorpRepositoryData();
            _testDbContext.DCBC_ENTITY_CORP.AddRange(corpdata.CorpEntitiesList);
            _testDbContext.SaveChanges();
            srd.CorpData = corpdata.CorpEntitiesList.ToList();

            var opladata = new OplaRepositoryData();
            _testDbContext.DCBC_ENTITY_OPLA.AddRange(opladata.OplaEntitiesList);
            _testDbContext.SaveChanges();
            srd.OplaData = opladata.OplaEntitiesList.ToList();

            //   var multi = multicolumndata.LookupEntitiesList.AsQueryable();

            //  IQueryable<DCBC_ENTITY_MultiColumn_LOOKUP_INDEX> multicolumn = multicolumndata.AsQueryable();
            //IAbraRepository abrarep;
            //IBblRepository bblrep;
            //ICbeRepository cberep;
            //ICorpRespository corprep;
            //IOplaRepository oplarep;

            //   MultiColumnFilters.GetFilterData(searchinput, multicolumndata.LookupEntitiesList.AsQueryable(), userServiceData);

            //Assert
            // Assert.IsNotNull(savedlist); // Test if null
            //  Assert.IsTrue(savedlist);
        }

        [TestMethod]
        public void WithCompanyName_Start_Test()
        {
            var multicolumndata = new DCBC_ENTITY_MultiColumn_LOOKUP_INDEXData();
            _testDbContext.DCBC_ENTITY_MultiColumn_LOOKUP_INDEX.AddRange(multicolumndata.LookupEntitiesList);
            _testDbContext.SaveChanges();
            var withcompany = FilterMultiColumnData1.WithCompanyName(multicolumndata.LookupEntitiesList.AsQueryable(), "*l");
            //Assert
            Assert.IsNotNull(withcompany); // Test if null
            Assert.IsTrue(withcompany.Count() == 5);
        }

        [TestMethod]
        public void WithCompanyName_Start_end_Test()
        {
            var multicolumndata = new DCBC_ENTITY_MultiColumn_LOOKUP_INDEXData();
            _testDbContext.DCBC_ENTITY_MultiColumn_LOOKUP_INDEX.AddRange(multicolumndata.LookupEntitiesList);
            _testDbContext.SaveChanges();
            var withcompany = FilterMultiColumnData1.WithCompanyName(multicolumndata.LookupEntitiesList.AsQueryable(), "*l*");
            //Assert
            Assert.IsNotNull(withcompany); // Test if null
            Assert.IsTrue(withcompany.Count() == 15);
        }

        [TestMethod]
        public void WithCompanyName_End_Test()
        {
            var multicolumndata = new DCBC_ENTITY_MultiColumn_LOOKUP_INDEXData();
            _testDbContext.DCBC_ENTITY_MultiColumn_LOOKUP_INDEX.AddRange(multicolumndata.LookupEntitiesList);
            _testDbContext.SaveChanges();
            var withcompany = FilterMultiColumnData1.WithCompanyName(multicolumndata.LookupEntitiesList.AsQueryable(), "l*");
            //Assert
            Assert.IsNotNull(withcompany); // Test if null
            Assert.IsTrue(withcompany.Count() == 2);
        }

        [TestMethod]
        public void WithCompanyName_Test()
        {
            var multicolumndata = new DCBC_ENTITY_MultiColumn_LOOKUP_INDEXData();
            _testDbContext.DCBC_ENTITY_MultiColumn_LOOKUP_INDEX.AddRange(multicolumndata.LookupEntitiesList);
            _testDbContext.SaveChanges();
            var withcompany = FilterMultiColumnData1.WithCompanyName(multicolumndata.LookupEntitiesList.AsQueryable(), "PurplePatch");
            //Assert
            Assert.IsNotNull(withcompany); // Test if null
            Assert.IsTrue(withcompany.Count() == 2);
        }

        [TestMethod]
        public void WithCompanyName_middle_Test()
        {
            var multicolumndata = new DCBC_ENTITY_MultiColumn_LOOKUP_INDEXData();
            _testDbContext.DCBC_ENTITY_MultiColumn_LOOKUP_INDEX.AddRange(multicolumndata.LookupEntitiesList);
            _testDbContext.SaveChanges();
            var withcompany = FilterMultiColumnData1.WithCompanyName(multicolumndata.LookupEntitiesList.AsQueryable(), "l*l");
            //Assert
            Assert.IsNotNull(withcompany); // Test if null
            //Assert.IsTrue(withcompany.Count() == 15);
        }

        [TestMethod]
        public void WithLicenseName()
        {
            var multicolumndata = new DCBC_ENTITY_MultiColumn_LOOKUP_INDEXData();
            _testDbContext.DCBC_ENTITY_MultiColumn_LOOKUP_INDEX.AddRange(multicolumndata.LookupEntitiesList);
            _testDbContext.SaveChanges();
            var withcompany = FilterMultiColumnData1.WithLicenseName(multicolumndata.LookupEntitiesList.AsQueryable(), "C943266");
            //Assert
            Assert.IsNotNull(withcompany); // Test if null
            Assert.IsTrue(withcompany.Count() == 1);
        }

        [TestMethod]
        public void WithFirstName_Start_Test()
        {
            var multicolumndata = new DCBC_ENTITY_MultiColumn_LOOKUP_INDEXData();
            _testDbContext.DCBC_ENTITY_MultiColumn_LOOKUP_INDEX.AddRange(multicolumndata.LookupEntitiesList);
            _testDbContext.SaveChanges();
            var withcompany = FilterMultiColumnData1.WithFirstName(multicolumndata.LookupEntitiesList.AsQueryable(), "*D");
            //Assert
            Assert.IsNotNull(withcompany); // Test if null
            Assert.IsTrue(withcompany.Count() == 1);
        }

        [TestMethod]
        public void WithFirstName_Start_end_Test()
        {
            var multicolumndata = new DCBC_ENTITY_MultiColumn_LOOKUP_INDEXData();
            _testDbContext.DCBC_ENTITY_MultiColumn_LOOKUP_INDEX.AddRange(multicolumndata.LookupEntitiesList);
            _testDbContext.SaveChanges();
            var withcompany = FilterMultiColumnData1.WithFirstName(multicolumndata.LookupEntitiesList.AsQueryable(), "*D*");
            //Assert
            Assert.IsNotNull(withcompany); // Test if null
            Assert.IsTrue(withcompany.Count() == 1);
        }

        [TestMethod]
        public void WithFirstName_End_Test()
        {
            var multicolumndata = new DCBC_ENTITY_MultiColumn_LOOKUP_INDEXData();
            _testDbContext.DCBC_ENTITY_MultiColumn_LOOKUP_INDEX.AddRange(multicolumndata.LookupEntitiesList);
            _testDbContext.SaveChanges();
            var withcompany = FilterMultiColumnData1.WithFirstName(multicolumndata.LookupEntitiesList.AsQueryable(), "D*");
            //Assert
            Assert.IsNotNull(withcompany); // Test if null
            Assert.IsTrue(withcompany.Count() == 1);
        }

        [TestMethod]
        public void WithFirstName_Test()
        {
            var multicolumndata = new DCBC_ENTITY_MultiColumn_LOOKUP_INDEXData();
            _testDbContext.DCBC_ENTITY_MultiColumn_LOOKUP_INDEX.AddRange(multicolumndata.LookupEntitiesList);
            _testDbContext.SaveChanges();
            var withcompany = FilterMultiColumnData1.WithFirstName(multicolumndata.LookupEntitiesList.AsQueryable(), "Purple");
            //Assert
            Assert.IsNotNull(withcompany); // Test if null
            Assert.IsTrue(withcompany.Count() == 2);
        }

        [TestMethod]
        public void WithFirstName_middle_Test()
        {
            var multicolumndata = new DCBC_ENTITY_MultiColumn_LOOKUP_INDEXData();
            _testDbContext.DCBC_ENTITY_MultiColumn_LOOKUP_INDEX.AddRange(multicolumndata.LookupEntitiesList);
            _testDbContext.SaveChanges();
            var withcompany = FilterMultiColumnData1.WithFirstName(multicolumndata.LookupEntitiesList.AsQueryable(), "D*D");
            //Assert
            Assert.IsNotNull(withcompany); // Test if null
            //Assert.IsTrue(withcompany.Count() == 15);
        }

        [TestMethod]
        public void WithLastName_Start_Test()
        {
            var multicolumndata = new DCBC_ENTITY_MultiColumn_LOOKUP_INDEXData();
            _testDbContext.DCBC_ENTITY_MultiColumn_LOOKUP_INDEX.AddRange(multicolumndata.LookupEntitiesList);
            _testDbContext.SaveChanges();
            var withcompany = FilterMultiColumnData1.WithLastName(multicolumndata.LookupEntitiesList.AsQueryable(), "*K");
            //Assert
            Assert.IsNotNull(withcompany); // Test if null
            Assert.IsTrue(withcompany.Count() == 1);
        }

        [TestMethod]
        public void WithLastName_Start_end_Test()
        {
            var multicolumndata = new DCBC_ENTITY_MultiColumn_LOOKUP_INDEXData();
            _testDbContext.DCBC_ENTITY_MultiColumn_LOOKUP_INDEX.AddRange(multicolumndata.LookupEntitiesList);
            _testDbContext.SaveChanges();
            var withcompany = FilterMultiColumnData1.WithLastName(multicolumndata.LookupEntitiesList.AsQueryable(), "*F*");
            //Assert
            Assert.IsNotNull(withcompany); // Test if null
            Assert.IsTrue(withcompany.Count() == 1);
        }

        [TestMethod]
        public void WithLastName_End_Test()
        {
            var multicolumndata = new DCBC_ENTITY_MultiColumn_LOOKUP_INDEXData();
            _testDbContext.DCBC_ENTITY_MultiColumn_LOOKUP_INDEX.AddRange(multicolumndata.LookupEntitiesList);
            _testDbContext.SaveChanges();
            var withcompany = FilterMultiColumnData1.WithLastName(multicolumndata.LookupEntitiesList.AsQueryable(), "F*");
            //Assert
            Assert.IsNotNull(withcompany); // Test if null
            Assert.IsTrue(withcompany.Count() == 1);
        }

        [TestMethod]
        public void WithLastName_Test()
        {
            var multicolumndata = new DCBC_ENTITY_MultiColumn_LOOKUP_INDEXData();
            _testDbContext.DCBC_ENTITY_MultiColumn_LOOKUP_INDEX.AddRange(multicolumndata.LookupEntitiesList);
            _testDbContext.SaveChanges();
            var withcompany = FilterMultiColumnData1.WithLastName(multicolumndata.LookupEntitiesList.AsQueryable(), "Patch");
            //Assert
            Assert.IsNotNull(withcompany); // Test if null
            Assert.IsTrue(withcompany.Count() == 2);
        }

        [TestMethod]
        public void WithLastName_middle_Test()
        {
            var multicolumndata = new DCBC_ENTITY_MultiColumn_LOOKUP_INDEXData();
            _testDbContext.DCBC_ENTITY_MultiColumn_LOOKUP_INDEX.AddRange(multicolumndata.LookupEntitiesList);
            _testDbContext.SaveChanges();
            var withcompany = FilterMultiColumnData1.WithLastName(multicolumndata.LookupEntitiesList.AsQueryable(), "F*k");
            //Assert
            Assert.IsNotNull(withcompany); // Test if null
            //Assert.IsTrue(withcompany.Count() == 15);
        }
    }
}