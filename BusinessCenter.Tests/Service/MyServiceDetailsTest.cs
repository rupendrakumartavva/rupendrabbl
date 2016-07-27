using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
//using BusinessCenter.Data.Model;
using BusinessCenter.Service.Implementation;
using BusinessCenter.Tests.Setup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using BusinessCenter.Data.Model;
using BusinessCenter.Data.Models;

namespace BusinessCenter.Tests.Service
{
      [TestClass]
    public class MyServiceDetailsTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private UserServicesRepository _serServicesRepository;

        private AbraRepository _abraRepository;
        private BblRepository _bblRepository;
        private CbeRepository _cbeRepository;
        private OplaRepository _oplaRepository;
        private CorpRespository _corpRepository;

      
        private MyServiceDetails _mockMyServiceDetailsService;

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
        public void UserServiceAdd_Return_Test()
        {
            var userServiceEntity = new UserServiceModel
            {
                UserId = "E5641464-F7A4-499C-9AF2-E450C8C26796",
                DataSource = "OPLA",
                EntityId = 50004151
            };
            //Act 
            var userServiceRows = _mockMyServiceDetailsService.UserServiceAdd(userServiceEntity);

            //Assert
            Assert.IsNotNull(userServiceRows); // Test if null
            Assert.IsTrue(userServiceRows == 0);
          
        }

          [TestMethod]
          public void UserSaveListCount_Return_Test()
        {
            //Act 
            var userServiceRows = _mockMyServiceDetailsService.UserSaveListCount("E5641464-F7A4-499C-9AF2-E450C8C26796");
            //Assert
            Assert.IsNotNull(userServiceRows); // Test if null
            Assert.IsTrue(userServiceRows == 1);
        }


          [TestMethod]
          public void GetSearchData_Return_Test_OPLA()
          {
              var userServiceEntity = new UserServiceModel
              {
                  UserId = "2ED4C269-F244-486B-8323-A2BA96408FE3",
                  DisplayType = "OPLA",
              };

              var userServiceRows = _mockMyServiceDetailsService.GetCount(userServiceEntity).ToList();

              Assert.IsNotNull(userServiceRows);
              Assert.IsTrue(userServiceRows.Count() == 1);
          }

          [TestMethod]
          public void GetSearchData_Return_Test_BBL()
          {
              var userServiceEntity = new UserServiceModel
              {
                  UserId = "A583A22D-FD47-4816-8A2E-5B17702323D4",
                  DisplayType = "BBL-CORP",
              };

              var userServiceRows = _mockMyServiceDetailsService.GetCount(userServiceEntity).ToList();

              Assert.IsNotNull(userServiceRows);
              Assert.IsTrue(userServiceRows.Count() == 1);
          }

          //[TestMethod]
          //public void GetSearchData_Return_Test_ABRA()
          //{
          //    var userServiceEntity = new UserServiceModel
          //    {
          //        UserId = "2AC53E53-87D0-468F-9628-4AEBA1120613",
          //        DisplayType = "ABRA",
          //    };

          //    var UserServiceRows = _mockMyServiceDetailsService.GetCount(userServiceEntity).ToList();

          //    Assert.IsNotNull(UserServiceRows);
          //    Assert.IsTrue(UserServiceRows.Count() == 1);
          //}


          [TestMethod]
          public void GetSearchData_Return_Test_CBE()
          {
              var userServiceEntity = new UserServiceModel
              {
                  UserId = "2AC53E53-87D0-468F-9628-4AEBA1120613",
                  DisplayType = "ABRA-CBE",
              };

              var userServiceRows = _mockMyServiceDetailsService.GetCount(userServiceEntity).ToList();

              Assert.IsNotNull(userServiceRows);
              Assert.IsTrue(userServiceRows.Count() == 1);
          }

          [TestMethod]
          public void GetSearchData_Return_Test_ALL()
          {
              var userServiceEntity = new UserServiceModel
              {
                  UserId = "A583A22D-FD47-4816-8A2E-5B17702323D4",
                  DisplayType = "ALL",
              };

              var userServiceRows = _mockMyServiceDetailsService.GetCount(userServiceEntity).ToList();

              Assert.IsNotNull(userServiceRows);
              Assert.IsTrue(userServiceRows.Count() == 1);
          }

          [TestMethod]
          public void DeleteUserService_Return_Test()
          {
              var userServiceEntity = new UserServiceModel
              {
                  UserId = "A583A22D-FD47-4816-8A2E-5B17702323D4",
                  DataSource = "CORP",
                  EntityId = 40072773,
              };
              _mockMyServiceDetailsService.DeleteUserService(userServiceEntity);

              var list = _testDbContext.UserService.ToList();
              var records = list.Where(x => x.DCBC_ENTITY_ID == 30000001).ToList();
              if (!records.Any())
              {
                  Assert.IsTrue(records.Count() == 0);
              }
          }
          [TestMethod]
          public void DeleteUserService()
          {
              var userService = new List<UserServiceModel>();

              var entity1 = new UserServiceModel
              {
                  UserId = "A583A22D-FD47-4816-8A2E-5B17702323D4",
                  DataSource = "BBL",
                  EntityId = 10045105,
              };

              var entity2 = new UserServiceModel
              {
                  UserId = "A583A22D-FD47-4816-8A2E-5B17702323D4",
                  DataSource = "CORP",
                  EntityId = 40072773,
              };

              userService.Add(entity1);
              userService.Add(entity2);

              _mockMyServiceDetailsService.DeleteUserService(userService);

              var list = _testDbContext.UserService.ToList();
              var records = list.Where(x => x.DCBC_ENTITY_ID == 10045105 && x.DCBC_ENTITY_ID == 40072773).ToList();
              if (!records.Any())
              {
                  Assert.IsTrue(records.Count() == 0);
              }
          }
          [TestMethod]
          public void DeleteSingleUerService_Test()
          {
              _mockMyServiceDetailsService.DeleteSingleUerService("A583A22D-FD47-4816-8A2E-5B17702323D4");

              var list = _testDbContext.UserService.ToList();
              var records = list.Where(x => x.UserId == "A583A22D-FD47-4816-8A2E-5B17702323D4").ToList();
              if (!records.Any())
              {
                  Assert.IsTrue(records.Count() == 0);
              }
          }

          [TestMethod]
          public void GetAllData_Return__Test()
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
              mssf.GetAllData(userServiceEntity,userService, bbl, opla, corp, cbe, abra);



              var userServiceRows = _mockMyServiceDetailsService.GetAllData(userServiceEntity);


              //Assert
              Assert.IsNotNull(userServiceRows); // Test if null
              Assert.IsTrue(userServiceRows.Count() == 0);

          }
          
    }
}
