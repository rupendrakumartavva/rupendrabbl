using BusinessCenter.Data;
using BusinessCenter.Data.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data.Model;
using BusinessCenter.Data.Models;
using System.Data.Common;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Tests.Setup;

namespace BusinessCenter.Tests.Repository
{
    [TestClass]
    public class UserServicesRepositoryTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

  

        //Repository declaration
        private UserServicesRepository _UserServicesTestRepository;
        private AbraRepository _abraTestRepository;
        private BblRepository _bblTestRepository;
        private CbeRepository _cbeTestRepository;
        private OplaRepository _oplaTestRepository;
        private CorpRespository _corpTestRespository;

        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);

            _abraTestRepository = new AbraRepository(_testUnitOfWork);
            _bblTestRepository = new BblRepository(_testUnitOfWork);
            _cbeTestRepository = new CbeRepository(_testUnitOfWork);
            _oplaTestRepository = new OplaRepository(_testUnitOfWork);
            _corpTestRespository = new CorpRespository(_testUnitOfWork);

            _UserServicesTestRepository = new UserServicesRepository(_testUnitOfWork,
              _abraTestRepository, _bblTestRepository,
             _cbeTestRepository, _oplaTestRepository,
             _corpTestRespository);

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

        }

        [TestMethod]
        public void GetUserServiceAll_Return_Test()
        {
            //Act 
            var UserServiceRows = _UserServicesTestRepository.GetUserServiceAll().ToList();

            //Assert
            Assert.IsNotNull(UserServiceRows); // Test if null
            Assert.IsTrue(UserServiceRows.Count() == 6);
        }

        [TestMethod]
        public void UserServiceAdd_ExistingDetails_Return_Test()
        {
            var userServiceEntity = new UserServiceModel
            {
                UserId = "E5641464-F7A4-499C-9AF2-E450C8C26796",
                DataSource = "OPLA",
                EntityId = 50004151
            };
            //Act 
            var UserServiceRows = _UserServicesTestRepository.UserServiceAdd(userServiceEntity);

            //Assert
            Assert.IsNotNull(UserServiceRows); // Test if null
            Assert.IsTrue(UserServiceRows == 0);
        }

        [TestMethod]
        public void UserServiceAdd_NewDetails_Return_Test()
        {
            var userServiceEntity = new UserServiceModel
            {
                UserId = "2ED4C269-F244-486B-8323-A2BA96408FE3",
                DataSource = "OPLA",
                EntityId = 50004151
            };

            //Act 

            var UserServiceRows = _UserServicesTestRepository.UserServiceAdd(userServiceEntity);

            //Assert
            Assert.IsNotNull(UserServiceRows); // Test if null

            Assert.IsTrue(UserServiceRows == 1);

        }


        [TestMethod]
        public void FindBy_Return_Test()
        {


            //Act 

            var UserServiceRows = _UserServicesTestRepository.FindBy(x => x.UserId == "E5641464-F7A4-499C-9AF2-E450C8C26796").ToList();

            //Assert
            Assert.IsNotNull(UserServiceRows); // Test if null

            Assert.IsTrue(UserServiceRows.Count() == 1);

        }
        [TestMethod]
        public void UserSaveListCount_Return_Test()
        {
            //Act 
            var UserServiceRows = _UserServicesTestRepository.UserSaveListCount("E5641464-F7A4-499C-9AF2-E450C8C26796");
            //Assert
            Assert.IsNotNull(UserServiceRows); // Test if null
            Assert.IsTrue(UserServiceRows == 1);
        }

     



        [TestMethod]
        public void GetSearchData_Return_Test_OPLA()
        {
            var userServiceEntity = new UserServiceModel
          {
              UserId = "2ED4C269-F244-486B-8323-A2BA96408FE3",
              DisplayType = "OPLA",
          };

            var UserServiceRows = _UserServicesTestRepository.GetCount(userServiceEntity).ToList();

            Assert.IsNotNull(UserServiceRows);
            Assert.IsTrue(UserServiceRows.Count() == 1);
        }

        [TestMethod]
        public void GetSearchData_Return_Test_BBL()
        {
            var userServiceEntity = new UserServiceModel
            {
                UserId = "A583A22D-FD47-4816-8A2E-5B17702323D4",
                DisplayType = "BBL-CORP",
            };

            var UserServiceRows = _UserServicesTestRepository.GetCount(userServiceEntity).ToList();

            Assert.IsNotNull(UserServiceRows);
            Assert.IsTrue(UserServiceRows.Count() == 1);
        }

        [TestMethod]
        public void GetSearchData_Return_Test_ABRA()
        {
            var userServiceEntity = new UserServiceModel
            {
                UserId = "2AC53E53-87D0-468F-9628-4AEBA1120613",
                DisplayType = "ABRA",
            };

            var UserServiceRows = _UserServicesTestRepository.GetCount(userServiceEntity).ToList();

            Assert.IsNotNull(UserServiceRows);
            Assert.IsTrue(UserServiceRows.Count() == 1);
        }


        [TestMethod]
        public void GetSearchData_Return_Test_CBE()
        {
            var userServiceEntity = new UserServiceModel
            {
                UserId = "2AC53E53-87D0-468F-9628-4AEBA1120613",
                DisplayType = "ABRA-CBE",
            };

            var UserServiceRows = _UserServicesTestRepository.GetCount(userServiceEntity).ToList();

            Assert.IsNotNull(UserServiceRows);
            Assert.IsTrue(UserServiceRows.Count() == 1);
        }

        [TestMethod]
        public void GetSearchData_Return_Test_ALL()
        {
            var userServiceEntity = new UserServiceModel
            {
                UserId = "A583A22D-FD47-4816-8A2E-5B17702323D4",
                DisplayType = "ALL",
            };

            var UserServiceRows = _UserServicesTestRepository.GetCount(userServiceEntity).ToList();

            Assert.IsNotNull(UserServiceRows);
            Assert.IsTrue(UserServiceRows.Count() == 1);
        }

    

        //[TestMethod]
        //public void GetAllData_Return_Test()
        //{
        //    var userServiceEntity = new UserServiceModel
        //    {
        //        UserId = "2ED4C269-F244-486B-8323-A2BA96408FE3",
        //        DisplayType = "OPLA",
        //    };

        //    var UserServiceRows = _UserServicesTestRepository.GetAllData(userServiceEntity).ToList();

        //    Assert.IsNotNull(UserServiceRows);
        //    Assert.IsTrue(UserServiceRows.Count() == 1);
        //}

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
            _UserServicesTestRepository.GetSearchData(userServiceEntity);
            mssf.GetAllData(userServiceEntity, userService, bbl, opla, corp, cbe, abra);

       

            var UserServiceRows = _UserServicesTestRepository.GetAllData(userServiceEntity);


            //Assert
            Assert.IsNotNull(UserServiceRows); // Test if null
            Assert.IsTrue(UserServiceRows.Count() == 0);

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
            _UserServicesTestRepository.DeleteUserService(userServiceEntity);

            var list = _testDbContext.UserService.ToList();
            var records = list.Where(x => x.DCBC_ENTITY_ID == 30000001).ToList();
            if (!records.Any())
            {
                Assert.IsTrue(records.Count() == 0);
            }
        }

        [TestMethod]
         public  void DeleteUserService()
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

            _UserServicesTestRepository.DeleteUserService(userService);

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
            _UserServicesTestRepository.DeleteSingleUerService("A583A22D-FD47-4816-8A2E-5B17702323D4");

            var list = _testDbContext.UserService.ToList();
            var records = list.Where(x => x.UserId == "A583A22D-FD47-4816-8A2E-5B17702323D4").ToList();
            if (!records.Any())
            {
                Assert.IsTrue(records.Count() == 0);
            }
        }
    }
}
