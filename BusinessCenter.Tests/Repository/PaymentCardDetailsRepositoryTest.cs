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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Repository
{
    [TestClass]
    public class PaymentCardDetailsRepositoryTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private PaymentCardDetailsRepository _paymentCardDetailsTestRepository;


        private PaymentCardDetailsData _mockData;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);


            _paymentCardDetailsTestRepository = new PaymentCardDetailsRepository(_testUnitOfWork);

            //Mocking Repository
            _mockData = new PaymentCardDetailsData();

            //Setup Data
            var testData = new PaymentCardDetailsData();
            _testDbContext.PaymentCardDetails.AddRange(testData.PaymentDetailsList);
            _testDbContext.SaveChanges();

        }
        [TestMethod]
        public void GetAll_Return_Test()
        {

            //Act 
            var paymentCardRows = _paymentCardDetailsTestRepository.GetPaymentDetails();

            //Assert
            Assert.IsNotNull(paymentCardRows); // Test if null
            Assert.IsTrue(paymentCardRows.Count() == 1);
        }
        [TestMethod]
        public void GetFindByIdTest()
        {
            var receiptModelEntity = new ReceiptModel { PaymentID = "34cd9f7d-988c-4cd9-81c9-d740255bbfc6" };
            //Act 
            var paymentCardRows = _paymentCardDetailsTestRepository.FindByID(receiptModelEntity);

            //Assert
            Assert.IsNotNull(paymentCardRows); // Test if null
            Assert.IsTrue(paymentCardRows.Count() == 1);
        }
        [TestMethod]
        public void InsertPaymentDetails_WithNewDetails_Test()
        {
            var paymentDetailsEntity = new PaymentDetailsModel
            {
               
                PaymentId = "34cd9f7d-988c-4cd9-81c9-d740255bbfc6",
                CardType = "3",
                CardNumber = "2222-2222-22222-2222",
                CardName = "CODE-IT",
                Email = "Test123@gmail.com",
                CvvNumber = "Test",
                FullName = ""
            };
            //Act 
            var paymentCardRows = _paymentCardDetailsTestRepository.InsertPaymentCardDetails(paymentDetailsEntity);

            //Assert
            Assert.IsNotNull(paymentCardRows); // Test if null
            Assert.IsTrue(paymentCardRows == true);
        }
        [TestMethod]
        public void InsertPaymentDetails_WithNullValues_Test()
        {
            var paymentDetailsEntity = new PaymentDetailsModel { };
           
            //Act 
            var paymentCardRows = _paymentCardDetailsTestRepository.InsertPaymentCardDetails(paymentDetailsEntity);

            //Assert
            Assert.IsNotNull(paymentCardRows); // Test if null
            Assert.IsTrue(paymentCardRows == true);
        }
    }
}
