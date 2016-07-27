using BusinessCenter.Data;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Tests.Setup;


namespace BusinessCenter.Tests.Repository
{
     [TestClass]
    public class MasterRegisterAgentRepositoryTest
     {

         private DbConnection _connection;
         private BusinessCenter.Tests.Common.TestContext _testDbContext;
         private UnitOfWork _testUnitOfWork;
         private MasterRegisterAgentRepository _masterRegisterTestRepository;

        // private MasterRegisterAgentData _mockData;

         [TestInitialize]
         public void Initialize()
         {
             //Test Context Repository
             _connection = Effort.DbConnectionFactory.CreateTransient();
             _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
             _testUnitOfWork = new UnitOfWork(_testDbContext);
             _masterRegisterTestRepository = new MasterRegisterAgentRepository(_testUnitOfWork);



             //Setup Data
             var testData = new CorpRepositoryData();
             _testDbContext.DCBC_ENTITY_CORP.AddRange(testData.CorpEntitiesList);
             _testDbContext.SaveChanges();

         }

         [TestMethod]
         public void GetFindByIDTest()
         {
             var generaldata = new GeneralBusiness { FileNumber = "C943266" };
             //Act 
             var corpdata = _masterRegisterTestRepository.FindByID(generaldata);

             //Assert
             Assert.IsNotNull(corpdata); // Test if null
             Assert.IsTrue(corpdata.Count() == 1);
         }

         //[TestMethod]
         //public void GetFindByID_NumberException_Test()
         //{
         //    var h = new GeneralBusiness { FileNumber = null };
         //    //Act 
         //    var fixedFeeRows = _masterRegisterTestRepository.FindByID(h);

         //    //Assert
         //    Assert.IsNotNull(fixedFeeRows); // Test if null
         //    Assert.IsTrue(!fixedFeeRows.Any());
         //}
         //   public IEnumerable<MasterRegisteredAgent> FindByID(GeneralBusiness generalModel)

         //[TestMethod()]
         //public void FindByIDTest()
         //{
         //    mockRepo.Setup(m => m.FindByID(generalBusiness)).Returns(list);
         //    var runtimeOutput = mockRepo.Object.FindByID(generalBusiness);
         //    Assert.IsTrue(runtimeOutput != null);
         //    Assert.IsTrue(runtimeOutput.FirstOrDefault().RegisterID == 45);
         //    Assert.IsTrue(runtimeOutput.Last().RegisterID == 52);
         //}
     }
}
