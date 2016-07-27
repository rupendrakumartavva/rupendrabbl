using System.Data.Common;
using System.Linq;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using BusinessCenter.Service.Implementation;
using BusinessCenter.Service.Interface;
using BusinessCenter.Tests.Setup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BusinessCenter.Tests.Service
{
     [TestClass]
    public class MasterBusinessActivityServiceTest
    {
      

        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private MasterBusinessActivityRepository _masterBusinessActivityTestRepository;

        private Mock<IMasterPrimaryCategoryRepository> _mockMasterPrimaryCategoryRepository;
        private MasterBusinessActivityData _mockData;

        // private Mock<IMasterBusinessActivityService> _mockBusinessActivityTestService;



         private MasterBusinessActivityService _mockBusinessActivityTestService;

        [TestInitialize]
        public void Initialize()
        {

            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _mockMasterPrimaryCategoryRepository = new Mock<IMasterPrimaryCategoryRepository>();

            _masterBusinessActivityTestRepository = new MasterBusinessActivityRepository(_testUnitOfWork, _mockMasterPrimaryCategoryRepository.Object);

            //Mocking Repository
            _mockData = new MasterBusinessActivityData();

         

//_mockBusinessActivityTestService=new Mock<IMasterBusinessActivityService>();
            _mockBusinessActivityTestService = new MasterBusinessActivityService(_masterBusinessActivityTestRepository);

            //Setup Data
            var testData = new MasterBusinessActivityData();
            _testDbContext.MasterBusinessActivity.AddRange(testData.MasterAcivityEntitiesList);
            _testDbContext.SaveChanges();


           
        }


        [TestMethod]
        public void FindBusinessActivity_With_Activity_ID_Return_Test()
        {
            var masterBusinessActivityEntity = new BusinessActivityModel { ActivityId = "DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A" };

         
            //Act 
            var businessActivitiesRows = _mockBusinessActivityTestService.FindByIDBasedonActivityId(masterBusinessActivityEntity);

            //Assert
            Assert.IsNotNull(businessActivitiesRows); // Test if null
            Assert.IsTrue(businessActivitiesRows.Count() == 1);
        }














        [TestMethod]
        public void GetAllBusinessActivitiesEntities_Service_Return_Test()
        {
          
            //Act 
            var businessActivitiesRows = _mockBusinessActivityTestService.GetAllBusinessActivities();

            //Assert
            Assert.IsNotNull(businessActivitiesRows); // Test if null
            Assert.IsTrue(businessActivitiesRows.Count() == 5);
        }


        [TestMethod]
        public void GetAllBusinessActivitiesEntities_OrderBy_Service_Return_Test()
        {
           //Act 
            var businessActivitiesRows = _mockBusinessActivityTestService.GetBusinessActivity();

            //Assert
            Assert.IsNotNull(businessActivitiesRows); // Test if null
            Assert.IsTrue(businessActivitiesRows.Count() == 5);
        }
        //

        [TestMethod]
        public void FindBusinessActivity_With_Activity_Return_Test()
        {
            var masterBusinessActivityEntity = new BusinessActivityModel { ActivityId = "DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A" };


         




            //Act 
            var businessActivitiesRows = _mockBusinessActivityTestService.FindBusinessActivitiesById(masterBusinessActivityEntity);

            //Assert
            Assert.IsNotNull(businessActivitiesRows); // Test if null
            Assert.IsTrue(businessActivitiesRows.Count() == 1);
        }


        [TestMethod]
        public void DeleteBusinessActivityTest()
        {
            var masterBusinessActivityEntity = new BusinessActivityEntity { ActivityID = "DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A" };
            //Act 
            var businessActivitiesRows = _mockBusinessActivityTestService.DeleteBusinessActivity(masterBusinessActivityEntity);

            //Assert
            Assert.IsNotNull(businessActivitiesRows); // Test if null
            Assert.IsTrue(businessActivitiesRows == true);
        }





        //
        [TestMethod]
        public void InsertBusinessActivity_WithNewDetails_Test()
        {
            var masterBusinessActivityEntity = new BusinessActivityEntity { ActivityID = "", ActivityName = "New Entertainment", APP_Type = "1" };
            //Act 
            var businessActivitiesRows = _mockBusinessActivityTestService.InsertUpdateBusinessActivity(masterBusinessActivityEntity);

            //Assert
            Assert.IsNotNull(businessActivitiesRows); // Test if null
            Assert.IsTrue(businessActivitiesRows == 1);
        }

        [TestMethod]
        public void InsertBusinessActivity_ActivityName_AlreadyExists_Test()
        {
            var masterBusinessActivityEntity = new BusinessActivityEntity { ActivityID = "", ActivityName = "Real Estate & Rentals", APP_Type = "1" };
            //Act 
            var businessActivitiesRows = _mockBusinessActivityTestService.InsertUpdateBusinessActivity(masterBusinessActivityEntity);

            //Assert
            Assert.IsNotNull(businessActivitiesRows); // Test if null
            Assert.IsTrue(businessActivitiesRows == 3);
        }


        [TestMethod]
        public void InsertBusinessActivity_UpdateActivityName_Test()
        {
            var masterBusinessActivityEntity = new BusinessActivityEntity { ActivityID = "DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A", ActivityName = "Pet Care & Retail New", APP_Type = "1" };
            //Act 
            var businessActivitiesRows = _mockBusinessActivityTestService.InsertUpdateBusinessActivity(masterBusinessActivityEntity);

            //Assert
            Assert.IsNotNull(businessActivitiesRows); // Test if null
            Assert.IsTrue(businessActivitiesRows == 2);
        }

        [TestMethod]
        public void InsertBusinessActivity_UpdateActivityNameAndStatusWithOutChange_Test()
        {
            var masterBusinessActivityEntity = new BusinessActivityEntity { ActivityID = "DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A", ActivityName = "Real Estate & Rentals", APP_Type = "1" };
            //Act 
            var businessActivitiesRows = _mockBusinessActivityTestService.InsertUpdateBusinessActivity(masterBusinessActivityEntity);

            //Assert
            Assert.IsNotNull(businessActivitiesRows); // Test if null
            Assert.IsTrue(businessActivitiesRows == 4);
        }

        [TestMethod]
        public void InsertBusinessActivity_UpdateActivityNameAndStatusChange_Test()
        {
            var masterBusinessActivityEntity = new BusinessActivityEntity { ActivityID = "DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A", ActivityName = "Real Estate & Rentals", APP_Type = "0" };
            //Act 
            var businessActivitiesRows = _mockBusinessActivityTestService.InsertUpdateBusinessActivity(masterBusinessActivityEntity);

            //Assert
            Assert.IsNotNull(businessActivitiesRows); // Test if null
            Assert.IsTrue(businessActivitiesRows == 2);
        }

        [TestMethod]
        public void InsertBusinessActivity_Update_OtherActivityNameExistsInTableAndStatusChange_Test()
        {
            var masterBusinessActivityEntity = new BusinessActivityEntity { ActivityID = "DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A", ActivityName = "Charity", APP_Type = "0" };
            //Act 
            var businessActivitiesRows = _mockBusinessActivityTestService.InsertUpdateBusinessActivity(masterBusinessActivityEntity);

            //Assert
            Assert.IsNotNull(businessActivitiesRows); // Test if null
            Assert.IsTrue(businessActivitiesRows == 3);
        }

    }
}