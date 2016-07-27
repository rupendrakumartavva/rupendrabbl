using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using BusinessCenter.Tests.Setup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BusinessCenter.Tests.Repository
{
    [TestClass]
   public class UserBBLServiceRepositoryTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Repository declaration
        private UserBBLServiceRepository _userBblServiceTestRepository;
      


        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);

            //UserBBLServiceRepository Initialization
            _userBblServiceTestRepository = new UserBBLServiceRepository(_testUnitOfWork);



            //Setup  UserBBLService FackData
            var testData = new UserBBLServiceRepositoryData();
            _testDbContext.UserBBLService.AddRange(testData.UserBblServiceList);
            _testDbContext.SaveChanges();

        }
        [TestMethod]
        public void InsertAssociateBblTest()
        {
            //Initial Model Details
            var bblAsscoiateService = new BblAsscoiateService
            {
                SubmissionLicense = "112233445566",
                UserID = "abc123-def456-ghi789",
                LicenseExpirationDate = DateTime.Now
            };

            //Act 
            var userbblService = _userBblServiceTestRepository.InsertAssociateBbl(bblAsscoiateService);

            //Assert
            Assert.IsNotNull(userbblService); // Test if null
            Assert.IsTrue(userbblService != string.Empty);
        }
        [TestMethod]
        public void UpdateAssociateBblTest()
        {
            //Initial Model Details
            var bblAsscoiateService = new BblAsscoiateService
            {
                SubmissionLicense = "123456789",
                UserID = "abc123-def456-ghi789",
                LicenseExpirationDate = DateTime.Now
            };
            //Act 
            var userbblService = _userBblServiceTestRepository.InsertAssociateBbl(bblAsscoiateService);

            //Assert
            Assert.IsNotNull(userbblService); // Test if null
            Assert.IsTrue(userbblService !=string.Empty);
        }
        [TestMethod]
        public void InsertSubmissionBblTest()
        {
            //Initial Model Details
            var bblAsscoiateService = new SubmissionApplication
            {
                SubmissionLicense = "DAPP15985360",
                UserID = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F"
            };
            //Act 
            var userbblService = _userBblServiceTestRepository.InsertSubmissionBbl(bblAsscoiateService);

            //Assert
            Assert.IsNotNull(userbblService); // Test if null
            Assert.IsTrue(userbblService == 7);
        }
        [TestMethod]
        public void InsertSubmissionBblFalseTest()
        {
            //Initial Model Details
            var bblAsscoiateService = new SubmissionApplication
            {
                SubmissionLicense = null,
                UserID = "abc123-def456-ghi789"
            };
            //Act 
            var userbblService = _userBblServiceTestRepository.InsertSubmissionBbl(bblAsscoiateService);

            //Assert
            Assert.IsNotNull(userbblService); // Test if null
            //Assert.IsTrue(userbblService == 0);
        }
        [TestMethod]
        public void UpdateUserBBLTest()
        {
            //Initial Model Details
            var paymentDetailsModel = new PaymentDetailsModel
            {
                SubmissionLicense = "112233445566"

            };
            //Act 
            var userbblService = _userBblServiceTestRepository.UpdateUserBBL("LREN11002680", paymentDetailsModel, "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F");

            //Assert
            Assert.IsNotNull(userbblService); // Test if null
            Assert.IsTrue(userbblService == true);
        }
        [TestMethod]
        public void UpdateUserBBLFalseTest()
        {
            //Initial Model Details
            var paymentDetailsModel = new PaymentDetailsModel
            {
                SubmissionLicense = "112233445566"

            };
            //Act 
            var userbblService = _userBblServiceTestRepository.UpdateUserBBL(null, paymentDetailsModel,"FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F");

            //Assert
            Assert.IsNotNull(userbblService); // Test if null
            Assert.IsTrue(userbblService == true);
        }
        [TestMethod]
        public void UpdateUserAssociateExpiryDateTest()
        {
          
            //Act 
            var userbblService = _userBblServiceTestRepository.UpdateUserAssociateExpiryDate(1, DateTime.Now);

            //Assert
            Assert.IsNotNull(userbblService); // Test if null
            Assert.IsTrue(userbblService == true);
        }
        [TestMethod]
        public void UpdateUserAssociateExpiryDateFalseTest()
        {
           
            //Act 
            var userbblService = _userBblServiceTestRepository.UpdateUserAssociateExpiryDate(0, DateTime.Now);

            //Assert
            Assert.IsNotNull(userbblService); // Test if null
            Assert.IsTrue(userbblService == false);
        }
        [TestMethod]
        public void FindByUserIDTest()
        {

            //Act 
            var userbblService = _userBblServiceTestRepository.FindByUserID("FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F");

            //Assert
            Assert.IsNotNull(userbblService); // Test if null
            Assert.IsTrue(userbblService.Count() == 5);
        }
        [TestMethod]
        public void FindByIDTest()
        {

            //Act 
            var userbblService = _userBblServiceTestRepository.FindByID(1);

            //Assert
            Assert.IsNotNull(userbblService); // Test if null
            Assert.IsTrue(userbblService.Count() == 1);
        }
         [TestMethod]
        public void FindByUserIdEntityIdTest()
        {
            //Initial Model Details
            var bblAsscoiateService = new BblAsscoiateService
            {
                UserID = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F",
                SubmissionLicense = "DAPP15985360",
                DCBC_ENTITY_ID = "DAPP15985360",
                B1_ALT_ID = "DAPP15985360"
            };
            //Act 
            var userbblService = _userBblServiceTestRepository.FindByUserIdEntityId(bblAsscoiateService);

            //Assert
            Assert.IsNotNull(userbblService); // Test if null
            Assert.IsTrue(userbblService.Count() == 1);
        }
         [TestMethod]
         public void DeleteUserServiceTest()
         {
             //Initial Model Details
             var bblAsscoiateService = new BblAsscoiateService
             {
                 UserID = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F",
                 SubmissionLicense = "DAPP15985360",
                 DCBC_ENTITY_ID="1",
             };
             //Act 
             var userbblService = _userBblServiceTestRepository.DeleteUserService(bblAsscoiateService);

             //Assert
             Assert.IsNotNull(userbblService); // Test if null
             Assert.IsTrue(userbblService == true);
         }
         [TestMethod]
         public void DeleteUserServiceFalseTest()
         {
             //Initial Model Details
             var bblAsscoiateService = new BblAsscoiateService
             {
                 UserID = "abc123-def456-ghi78900",
                 SubmissionLicense = "123456789"
             };
             //Act 
             var userbblService = _userBblServiceTestRepository.DeleteUserService(bblAsscoiateService);

             //Assert
             Assert.IsNotNull(userbblService); // Test if null
             Assert.IsTrue(userbblService == false);
         }
         [TestMethod]
         public void CheckUserBBLTest()
         {
            //Act 
             var userbblService = _userBblServiceTestRepository.CheckUserBBL("DAPP15985360", "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F");

             //Assert
             Assert.IsNotNull(userbblService); // Test if null
             Assert.IsTrue(userbblService.Count() == 1);
         }
         [TestMethod]
         public void FindByUserStatusIDTest()
         {
             //Act 
             var userbblService = _userBblServiceTestRepository.FindByUserStatusID("FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F");

             //Assert
             Assert.IsNotNull(userbblService); // Test if null
             Assert.IsTrue(userbblService.Count() == 5);
         }

         [TestMethod]
         public void TransferSubmissionsTest()
         {
             //Initial Model Details
             var submissiontransfer = new Submissiontransfer
             {
                 ToUserId = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F"
             };
             //Act 
             var userbblService = _userBblServiceTestRepository.TransferSubmissions(submissiontransfer, "LREN11002680");

             //Assert
             Assert.IsNotNull(userbblService); // Test if null
             Assert.IsTrue(userbblService == true);
         }
    }
}
