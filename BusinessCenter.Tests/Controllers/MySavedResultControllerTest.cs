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


namespace BusinessCenter.Tests.Controllers
{
     [TestClass]
    public class MySavedResultControllerTest
    {

        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private MySavedResultController _controller = null;


        #region Declaration of Service
        private AbraRepository _abraRepository;
        private BblRepository _bblRepository;
        private CbeRepository _cbeRepository;
        private OplaRepository _oplaRepository;
        private CorpRespository _corpRepository;

    //    private UserServicesRepositoryData _mockData;
        private UserServicesRepository _serServicesRepository;
        private MyServiceDetails _mockMyServiceDetailsService;
        #endregion


        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);




            _abraRepository = new AbraRepository(_testUnitOfWork);
            _bblRepository = new BblRepository(_testUnitOfWork);
            _cbeRepository = new CbeRepository(_testUnitOfWork);
            _oplaRepository = new OplaRepository(_testUnitOfWork);
            _corpRepository = new CorpRespository(_testUnitOfWork);

            _serServicesRepository = new UserServicesRepository(_testUnitOfWork, _abraRepository, _bblRepository, _cbeRepository, _oplaRepository, _corpRepository);

            _mockMyServiceDetailsService = new MyServiceDetails(_serServicesRepository);
            _controller = new MySavedResultController(_mockMyServiceDetailsService);


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


      



        }
         [TestMethod]
        public async Task AddToMySavedList_Return_Test()
        {
            //Initialize Entites
            var userServiceEntity = new UserServiceModel
            {
                UserId = "E5641464-F7A4-499C-9AF2-E450C8C26796",
                DataSource = "OPLA",
                EntityId = 50004151
            };
            //Act 
            dynamic userServiceRows =  await Task.FromResult(_controller.AddToMySavedList(userServiceEntity));

            var content = userServiceRows.GetType().GetProperty("Content").GetValue(userServiceRows, null);
            var result = content.GetType().GetProperty("status").GetValue(content, null);

            //Assert
            Assert.IsNotNull(userServiceRows); // Test if null
            Assert.AreEqual(result, "Record Already Exits");
            
        
        }
         [TestMethod]
         public async Task MySavedListCount_Return_Test()
         {
              //Initialize Entites
             var dashBoardUserService=new DashBoardUserService
             {
             UserId="E5641464-F7A4-499C-9AF2-E450C8C26796"
             };

             //Act 
             dynamic userServiceRows = await Task.FromResult(_controller.MySavedListCount(dashBoardUserService));
             var content = userServiceRows.GetType().GetProperty("Content").GetValue(userServiceRows, null);
             var result = content.GetType().GetProperty("status").GetValue(content, null);
             //Assert
             Assert.IsNotNull(userServiceRows); // Test if null
             Assert.AreEqual(result, "1");

           
         }
         //[TestMethod]
         //public async Task GetSearchData_Return_Test_OPLA()
         //{ 
         //    //Initialize Entites
         //    var userServiceEntity = new UserServiceModel
         //    {
         //        UserId = "2ED4C269-F244-486B-8323-A2BA96408FE3",
         //        DisplayType = "OPLA",
         //    };

         //    //Act 
         //    var UserServiceRows = await controller.SearchData(userServiceEntity) as JsonResult<SearchDataViewModel>;

         //    //Assert
         //    Assert.IsNotNull(UserServiceRows);
             
         //    //Assert.IsTrue(UserServiceRows.Content.Count()==1);
         //}

         [TestMethod]
         public async Task GetAllData_Return__Test()
         {


             //Initialize Entites
             var userServiceEntity = new UserServiceModel
             {
                 PageIndex = 1,
                 PageSize = 10,
                 DisplayType = "ALL",
                 UserId = "A583A22D-FD47-4816-8A2E-5B17702323D4"
             };
           
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

          
             var abra = abraRepositoryData.AbraEntitiesList.ToList();
             var bbl = bblRepositoryData.BblEntitiesList.ToList();
             var cbe = cbeRepositoryData.CbeEntitiesList.ToList();
             var opla = oplaRepositoryData.OplaEntitiesList.ToList();
             var corp = corpRepositoryData.CorpEntitiesList.ToList();
             var userService = userServiceData.UserServiceEntitiesList.AsQueryable();
             MySaveSearchFilter mssf=new MySaveSearchFilter();
             mssf.GetUserServiceData(userServiceEntity, userService);
             //Act 
             _mockMyServiceDetailsService.GetSearchData(userServiceEntity);
             mssf.GetAllData(userServiceEntity, userService, bbl, opla, corp, cbe, abra);



             var userServiceRows = await _controller.SearchAll(userServiceEntity) as JsonResult<List<CommonData>>;


             //Assert
             Assert.IsNotNull(userServiceRows); // Test if null
             Assert.AreEqual(userServiceRows.Content.Count(), 0);


         }
         [TestMethod]
         public async Task DeleteUserService_Return_Test()
         {
             //Initialize Entites
             var userServiceEntity = new UserServiceModel
             {
                 UserId = "A583A22D-FD47-4816-8A2E-5B17702323D4",
                 DataSource = "CORP",
                 EntityId = 40072773,
             };

             //Act 
             dynamic userserviceRows = await Task.FromResult(_controller.DeleteUserService(userServiceEntity));

             var content = userserviceRows.GetType().GetProperty("Content").GetValue(userserviceRows, null);
             var result = content.GetType().GetProperty("status").GetValue(content, null);
             
             //Assert
             Assert.IsNotNull(userserviceRows);
             Assert.AreEqual(result, "success");

         }
         [TestMethod]
         public async Task DeleteSingleUerService_Test()
         {

             //Initialize Entites
             var singleuserServiceEntity = new DeleteServiceSingle
             {
                 UserId = "A583A22D-FD47-4816-8A2E-5B17702323D4",
                 
             };

             //Act
             dynamic userserviceRows = await Task.FromResult(_controller.DeleteUserService(singleuserServiceEntity));

             var content = userserviceRows.GetType().GetProperty("Content").GetValue(userserviceRows, null);
             var result = content.GetType().GetProperty("status").GetValue(content, null);
             //Assert
             Assert.IsNotNull(userserviceRows);
             Assert.AreEqual(result, "success");
         }
         [TestMethod]
         public async Task DeleteMultipleUserService_Return_Test()
         {
             
             //Intialize Entities
             var userService = new List<UserServiceModel>
             {
                 new UserServiceModel{UserId= "A583A22D-FD47-4816-8A2E-5B17702323D4",DataSource="BBL",EntityId=10045105},
                 new UserServiceModel{UserId="A583A22D-FD47-4816-8A2E-5B17702323D4",DataSource="CORP",EntityId=40072773}
             };

             //Act
             dynamic userserviceRows = await Task.FromResult(_controller.MultipleDeleteUserService(userService));

             var content = userserviceRows.GetType().GetProperty("Content").GetValue(userserviceRows, null);
             var result = content.GetType().GetProperty("status").GetValue(content, null);
             //Assert
             Assert.IsNotNull(userserviceRows);
             Assert.AreEqual(result, "success");

         }
    }
}
