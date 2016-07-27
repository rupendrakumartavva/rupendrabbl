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
    public class MasterBusinessActivityRepositoryTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private MasterBusinessActivityRepository _masterBusinessActivityTestRepository;
    
        private Mock<IMasterPrimaryCategoryRepository> _mockMasterPrimaryCategoryRepository;
        private MasterBusinessActivityData _mockData;


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

            //Setup Data
            var testData = new MasterBusinessActivityData();
            _testDbContext.MasterBusinessActivity.AddRange(testData.MasterAcivityEntitiesList);
            _testDbContext.SaveChanges();

        }
        [TestMethod]
        public void GetAllBusinessActivitiesEntitiesTest()
        {
            //Act 
            var businessActivitiesRows = _masterBusinessActivityTestRepository.AllBusinessActivities();

            //Assert
            Assert.IsNotNull(businessActivitiesRows); // Test if null
            Assert.IsTrue(businessActivitiesRows.Count() == 5);
        }

        [TestMethod]
        public void GetBusinessActivityTest()
        {

            //Act 
            var businessActivitiesRows = _masterBusinessActivityTestRepository.GetBusinessActivity();

            //Assert
            Assert.IsNotNull(businessActivitiesRows); // Test if null
            Assert.IsTrue(businessActivitiesRows.Count() == 5);
        }

        [TestMethod]
        public void GetFindByIdTest()
        {
            var masterBusinessActivityEntity = new BusinessActivityModel { ActivityId = "DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A" };
            //Act 
            var businessActivitiesRows = _masterBusinessActivityTestRepository.FindByID(masterBusinessActivityEntity);

            //Assert
            Assert.IsNotNull(businessActivitiesRows); // Test if null
            Assert.IsTrue(businessActivitiesRows.Count() == 1);
        }


        [TestMethod]
        public void GetFindByActivityIdTest()
        {
           
            //Act 
            var businessActivitiesRows = _masterBusinessActivityTestRepository.FindByActivityId("DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A");

            //Assert
            Assert.IsNotNull(businessActivitiesRows); // Test if null
            Assert.IsTrue(businessActivitiesRows.Count() == 1);
        }



        [TestMethod]
        public void FindByActivityNameTest()
        {
            var masterBusinessActivityEntity = new RenewModel { ActivityName = "Real Estate & Rentals" };
            //Act 
            var businessActivitiesRows = _masterBusinessActivityTestRepository.FindByActivityName(masterBusinessActivityEntity);

            //Assert
            Assert.IsNotNull(businessActivitiesRows); // Test if null
            Assert.IsTrue(businessActivitiesRows.Count() == 1);
        }
        [TestMethod]
        public void DeleteBusinessActivityTest()
        {
            var masterBusinessActivityEntity = new BusinessActivityEntity { ActivityID = "DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A" };
            //Act 
            var businessActivitiesRows = _masterBusinessActivityTestRepository.DeleteBusinessActivity(masterBusinessActivityEntity);

            //Assert
            Assert.IsNotNull(businessActivitiesRows); // Test if null
            Assert.IsTrue(businessActivitiesRows == true);
        }


        [TestMethod]
        public void FindByIdBasedonActivityIdTest()
        {
            var masterBusinessActivityEntity = new BusinessActivityModel { ActivityId = "DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A" };
            //Act 
            var businessActivitiesRows = _masterBusinessActivityTestRepository.FindByIDBasedonActivityId(masterBusinessActivityEntity);

            //Assert
            Assert.IsNotNull(businessActivitiesRows); // Test if null
            Assert.IsTrue(businessActivitiesRows.Count() == 1);
        }



        //
        [TestMethod]
        public void InsertBusinessActivity_WithNewDetails_Test()
        {
            var masterBusinessActivityEntity = new BusinessActivityEntity { ActivityID = "", ActivityName = "New Entertainment",APP_Type ="1"};
            //Act 
            var businessActivitiesRows = _masterBusinessActivityTestRepository.InsertUpdateBusinessActivity(masterBusinessActivityEntity);

            //Assert
            Assert.IsNotNull(businessActivitiesRows); // Test if null
            Assert.IsTrue(businessActivitiesRows == 1);
        }

         [TestMethod]
        public void InsertBusinessActivity_ActivityName_AlreadyExists_Test()
        {
            var masterBusinessActivityEntity = new BusinessActivityEntity { ActivityID = "", ActivityName = "Real Estate & Rentals", APP_Type = "1" };
            //Act 
            var businessActivitiesRows = _masterBusinessActivityTestRepository.InsertUpdateBusinessActivity(masterBusinessActivityEntity);

            //Assert
            Assert.IsNotNull(businessActivitiesRows); // Test if null
            Assert.IsTrue(businessActivitiesRows == 3);
        }


         [TestMethod]
         public void InsertBusinessActivity_UpdateActivityName_Test()
         {
             var masterBusinessActivityEntity = new BusinessActivityEntity { ActivityID = "DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A", ActivityName = "Pet Care & Retail New", APP_Type = "1" };
             //Act 
             var businessActivitiesRows = _masterBusinessActivityTestRepository.InsertUpdateBusinessActivity(masterBusinessActivityEntity);

             //Assert
             Assert.IsNotNull(businessActivitiesRows); // Test if null
             Assert.IsTrue(businessActivitiesRows == 2);
         }

         [TestMethod]
         public void InsertBusinessActivity_UpdateActivityNameAndStatusWithOutChange_Test()
         {
             var masterBusinessActivityEntity = new BusinessActivityEntity { ActivityID = "DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A", ActivityName = "Real Estate & Rentals", APP_Type = "1" };
             //Act 
             var businessActivitiesRows = _masterBusinessActivityTestRepository.InsertUpdateBusinessActivity(masterBusinessActivityEntity);

             //Assert
             Assert.IsNotNull(businessActivitiesRows); // Test if null
             Assert.IsTrue(businessActivitiesRows == 4);
         }

         [TestMethod]
         public void InsertBusinessActivity_UpdateActivityNameAndStatusChange_Test()
         {
             var masterBusinessActivityEntity = new BusinessActivityEntity { ActivityID = "DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A", ActivityName = "Real Estate & Rentals", APP_Type = "0" };
             //Act 
             var businessActivitiesRows = _masterBusinessActivityTestRepository.InsertUpdateBusinessActivity(masterBusinessActivityEntity);

             //Assert
             Assert.IsNotNull(businessActivitiesRows); // Test if null
             Assert.IsTrue(businessActivitiesRows == 2);
         }

         [TestMethod]
         public void InsertBusinessActivity_Update_OtherActivityNameExistsInTableAndStatusChange_Test()
         {
             var masterBusinessActivityEntity = new BusinessActivityEntity { ActivityID = "DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A", ActivityName = "Charity", APP_Type = "0" };
             //Act 
             var businessActivitiesRows = _masterBusinessActivityTestRepository.InsertUpdateBusinessActivity(masterBusinessActivityEntity);

             //Assert
             Assert.IsNotNull(businessActivitiesRows); // Test if null
             Assert.IsTrue(businessActivitiesRows == 3);
         }
    }
}
