using BusinessCenter.Data;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Tests.Setup;
using Moq.Language.Flow;


namespace BusinessCenter.Tests.Repository
{
    [TestClass]
    public class CorpRepositoryTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private CorpRespository _corpTestRepository;
        private Mock<ICorpRespository> _mockCorpRepository;
        private CorpRepositoryData _mockData;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _corpTestRepository = new CorpRespository(_testUnitOfWork);

            //Mocking Repository
            _mockCorpRepository = new Mock<ICorpRespository>();
            _mockData = new CorpRepositoryData();

            //Setup Data
            var testData = new CorpRepositoryData();
            _testDbContext.DCBC_ENTITY_CORP.AddRange(testData.CorpEntitiesList);
            _testDbContext.SaveChanges();

        }
        [TestMethod()]
        public void GetCorpAllTest()
        {
            //Act 
            var bblRows = _corpTestRepository.GeCorpLookupAll();
            
            //Assert
            Assert.IsNotNull(bblRows); // Test if null
            Assert.IsTrue(bblRows.Count() == 3);

        }
        [TestMethod()]
        public void FindByLicenseTest()
        {
            //Act 
            var corpRows = _corpTestRepository.FindByFileNumber("C212821");

            //Assert
            Assert.IsNotNull(corpRows); // Test if null
            Assert.IsTrue(corpRows.Count() == 1);
        }
         [TestMethod()]
        public void FindByTest()
        {
            //Act 
            var corpRows = _corpTestRepository.FindBy(x => x.FileNumber == "C212821");

            //Assert
            Assert.IsNotNull(corpRows); // Test if null
            Assert.IsTrue(corpRows.Count() == 1);
        }
         [TestMethod]
         public void CorpOnlineSearch_Revoked_Test()
         {
             //Initial Model Details
             var corpStatus = new CorporationDetails { FileNumber = "C212821" };

             //Act 
             var corpRows = _corpTestRepository.CorpOnlineSearch(corpStatus);

             //Assert
             Assert.IsNotNull(corpRows); // Test if null
             Assert.IsTrue(corpRows == "Revoked");
         }
         [TestMethod]
         public void CorpOnlineSearch_NoData_Test()
         {
             //Initial Model Details
             var corpStatus = new CorporationDetails { FileNumber = "C123821" };

             //Act 
             var corpRows = _corpTestRepository.CorpOnlineSearch(corpStatus);

             //Assert
             Assert.IsNotNull(corpRows); // Test if null
             Assert.IsTrue(corpRows == "NoData");
         }

    }
}

#region Junk
//    private readonly List<DCBC_ENTITY_CORP> list = new List<DCBC_ENTITY_CORP>()
//    {
//        new DCBC_ENTITY_CORP()
//        {
//DCBC_ENTITY_SOURCE = "Corp",
//BusinessName ="CHRISTOPHER W. NUTTER",
//Suffix = "L.L.C.",
//Locale = "Domestic",
//ModelType = "SP",
//EntityStatus = "Active",
//BusniessAddressLine1 = "460 N Street NW",
//BusniessAddressLine2 = "#7",
//BusinessCity = "Washington",
//BusinessState = "DC",
//ZipCode = "20001",
//NextReportYear="2017",
//FileNumber ="P00005237981"
//        },
//         new DCBC_ENTITY_CORP()
//        {
//DCBC_ENTITY_SOURCE = "Corp",
//BusinessName ="DELLTONJIA  ADAMS",
//Suffix = "L.L.C.",
//Locale = "Domestic",
//ModelType = "SP",
//EntityStatus = "Active",
//BusniessAddressLine1 = "2806 14TH  ST. NW",
//BusniessAddressLine2 = "#101",
//BusinessCity = "Washington",
//BusinessState = "DC",
//ZipCode = "20009",
//NextReportYear="2016",
//FileNumber ="N00005237983"
//        }
//    };
//    private readonly Mock<ICorpRespositry> mockRepo=new Mock<ICorpRespositry>();


//    [TestMethod()]
//    public void GeCorpLookupAllTest()
//    {
//        mockRepo.Setup(m => m.GeCorpLookupAll()).Returns(list);
//        var runtimeOutput = mockRepo.Object.GeCorpLookupAll();
//        Assert.IsTrue(runtimeOutput != null);
//        Assert.IsTrue(runtimeOutput.FirstOrDefault().FileNumber == "P00005237981");
//        Assert.IsTrue(runtimeOutput.Last().FileNumber == "N00005237983");
//    }

//    //[TestMethod()]
//    //public void FindByTest()
//    //{
//    //    System.Linq.Expressions.Expression<Func<DCBC_ENTITY_CORP, bool>> predicate;
//    //    mockRepo.Setup(m => m.FindBy(predicate)).Returns(list);
//    //    var runtimeOutput = mockRepo.Object.FindBy();
//    //    Assert.IsTrue(runtimeOutput != null);
//    //    Assert.IsTrue(runtimeOutput.FirstOrDefault().FileNumber == "P00005237981");
//    //    Assert.IsTrue(runtimeOutput.Last().FileNumber == "N00005237983");
//    //}

//    [TestMethod()]
//    public void FindByFileNumberTest()
//    {
//        string fileNumber = "P00005237981";
//        mockRepo.Setup(m => m.FindByFileNumber(fileNumber)).Returns(list);
//        var runtimeOutput = mockRepo.Object.FindByFileNumber(fileNumber);
//        Assert.IsTrue(runtimeOutput != null);
//        Assert.IsTrue(runtimeOutput.FirstOrDefault().FileNumber == "P00005237981");
//        Assert.IsTrue(runtimeOutput.Last().FileNumber == "N00005237983");
//    }
#endregion