using BusinessCenter.Data;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using BusinessCenter.Tests.Setup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Repository
{
      [TestClass]
    public class KeywordDetailsRepositoryTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private KeywordDetailsRepository _keywordDetailsTestRepository;
        private Mock<IKeywordDetailsRepository> _mockkeywordDetailsRepository;
        private KeywordDetailsRepositoryData _mockData;

        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _keywordDetailsTestRepository = new KeywordDetailsRepository(_testUnitOfWork);

            //Mocking Repository
            _mockkeywordDetailsRepository = new Mock<IKeywordDetailsRepository>();
            _mockData = new KeywordDetailsRepositoryData();

            //Setup Data
            var testData = new KeywordDetailsRepositoryData();
            _testDbContext.KeywordDetails.AddRange(testData.KeyDetailsEntitiesList);
            _testDbContext.SaveChanges();

        }


        [TestMethod]
        public void KeyDetailsGetAllTest()
        {

            //Act 
            var keyworddetailsRows = _keywordDetailsTestRepository.KeyDetailsGetAll();

            //Assert
            Assert.IsNotNull(keyworddetailsRows); // Test if null
            Assert.IsTrue(keyworddetailsRows.Count() == 2);
        }
        [TestMethod]
        public void FindByTest()
        {

            //Act 
            var keyworddetailsRows = _keywordDetailsTestRepository.FindBy(x=>x.KeyId==1);

            //Assert
            Assert.IsNotNull(keyworddetailsRows); // Test if null
            Assert.IsTrue(keyworddetailsRows.Count() == 1);
        }
        [TestMethod]
        public void AddKeyWordTest()
        {
            var keywordDetails = new KeywordDetails { KeyId = 2, KeyCount = 30, CreatedDate=DateTime.Now};
            //Act 
           _keywordDetailsTestRepository.AddKeyWord(keywordDetails);

            //Assert
            //Assert.IsNotNull(keyworddetailsRows); // Test if null
            //Assert.IsTrue(keyworddetailsRows.Count() == 1);
        }
        [TestMethod]
        public void SaveChangesTest()
        {
          //  var keywordDetails = new KeywordDetails { KeyId = 2, KeyCount = 30, CreatedDate = DateTime.Now };
            //Act 
            _keywordDetailsTestRepository.SaveChanges();

            //Assert
            //Assert.IsNotNull(keyworddetailsRows); // Test if null
            //Assert.IsTrue(keyworddetailsRows.Count() == 1);
        }
        #region Junk
        //private readonly Mock<KeywordDetailsRepository> mockRepo = new Mock<KeywordDetailsRepository>();

        //  private readonly List<KeywordDetails> list = new List<KeywordDetails>()
        //  {
        //      new KeywordDetails()
        //      {
        //          KeyId = 1,
        //          KeyCount = 1,
        //          KeywordDid = 8,
        //      },
        //      new KeywordDetails()
        //      {
        //          KeyId = 2,
        //          KeyCount = 5,
        //          KeywordDid =91,
        //      }
        //  };

        //  private readonly KeywordDetails keywordDetails = new KeywordDetails()
        //  {
        //      KeyId = 1,
        //      KeyCount = 1,
        //      KeywordDid = 1,
        //  };

        //  //[TestMethod()]
        //  //public void FindByTest()
        //  //{
        //  //    mockRepo.Setup(m => m.FindBy()).Returns(list);
        //  //    var runtimeOutput = mockRepo.Object.FindBy();
        //  //    Assert.IsTrue(runtimeOutput != null);
        //  //    Assert.IsTrue(runtimeOutput.FirstOrDefault().KeyId == 1);
        //  //    Assert.IsTrue(runtimeOutput.Last().KeyId ==2);
        //  //}

        ////  KeywordDetails AddKeyWord(KeywordDetails entity)
        //  [TestMethod()]
        //  public void AddKeyWordTest()
        //  {
        //      mockRepo.Setup(m => m.AddKeyWord(keywordDetails)).Returns(keywordDetails);
        //      var runtimeOutput = mockRepo.Object.AddKeyWord(keywordDetails);
        //      Assert.IsTrue(runtimeOutput != null);
        //      Assert.IsTrue(runtimeOutput.KeyId == 1);
        //  }
        #endregion
      }
}
