using BusinessCenter.Data.Common;
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
   public class SearchEntityDisplayTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
       // private UnitOfWork _testUnitOfWork;
        [TestInitialize]
        public void Initialize()
        {
          //  _testUnitOfWork=new UnitOfWork(_testDbContext);
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            // _MultiColumnFiltersTest = new MultiColumnFilters();
        }
        [TestMethod]
        public void GetBblRecords_Test()
        {
            //Act 
            SearchResultData srd=new SearchResultData();
            SearchEntityDisplay sed=new SearchEntityDisplay();
            var bbldata = new BblRepositoryData();
            _testDbContext.DCBC_ENTITY_BBL.AddRange(bbldata.BblEntitiesList);
            _testDbContext.SaveChanges();
            srd.BblData = bbldata.BblEntitiesList.ToList();
            var bblrecorddata = sed.GetBblRecords(1, 1, true, srd.BblData).ToList();
            //Assert
           Assert.IsNotNull(bblrecorddata); 
            // Test if null
           Assert.IsTrue(bblrecorddata.Count()==1);
        }
        [TestMethod]
        public void GetCorpRecords_Test()
        {
            //Act 
            SearchResultData srd = new SearchResultData();
            SearchEntityDisplay sed = new SearchEntityDisplay();
            var corpdata = new CorpRepositoryData();
            _testDbContext.DCBC_ENTITY_CORP.AddRange(corpdata.CorpEntitiesList);
            _testDbContext.SaveChanges();
            srd.CorpData = corpdata.CorpEntitiesList.ToList();
            var corprecorddata = sed.GetCorpRecords(1, 1, true, srd.CorpData).ToList();
            //Assert
            Assert.IsNotNull(corprecorddata);
            // Test if null
            Assert.IsTrue(corprecorddata.Count() == 1);
        }

        [TestMethod]
        public void GetAbraRecords_Test()
        {
            //Act 
            SearchResultData srd = new SearchResultData();
            SearchEntityDisplay sed = new SearchEntityDisplay();
            var abradata = new AbraRepositoryData();
            _testDbContext.DCBC_ENTITY_ABRA.AddRange(abradata.AbraEntitiesList);
            _testDbContext.SaveChanges();
            srd.AbraData = abradata.AbraEntitiesList.ToList();
            var abrarecorddata = sed.GetAbraRecords(1, 1, true, srd.AbraData).ToList();
            //Assert
            Assert.IsNotNull(abrarecorddata);
            // Test if null
            Assert.IsTrue(abrarecorddata.Count() == 1);
        }

        [TestMethod]
        public void GetOplaRecords_Test()
        {
            //Act 
            SearchResultData srd = new SearchResultData();
            SearchEntityDisplay sed = new SearchEntityDisplay();
            var opladata = new OplaRepositoryData();
            _testDbContext.DCBC_ENTITY_OPLA.AddRange(opladata.OplaEntitiesList);
            _testDbContext.SaveChanges();
            srd.OplaData = opladata.OplaEntitiesList.ToList();
            var oplarecorddata = sed.GetOplaRecords(1, 1, true, srd.OplaData).ToList();
            //Assert
            Assert.IsNotNull(oplarecorddata);
            // Test if null
            Assert.IsTrue(oplarecorddata.Count() == 1);
        }

        [TestMethod]
        public void GetCbeRecords_Test()
        {
            //Act 
            SearchResultData srd = new SearchResultData();
            SearchEntityDisplay sed = new SearchEntityDisplay();
            var cbedata = new CbeRepositoryData();
            _testDbContext.DCBC_ENTITY_CBE.AddRange(cbedata.CbeEntitiesList);
            _testDbContext.SaveChanges();
            srd.CbeData = cbedata.CbeEntitiesList.ToList();
            var cberecorddata = sed.GetCbeRecords(1, 1, true, srd.CbeData).ToList();
            //Assert
            Assert.IsNotNull(cberecorddata);
            // Test if null
            Assert.IsTrue(cberecorddata.Count() == 1);
        }


    }
}
