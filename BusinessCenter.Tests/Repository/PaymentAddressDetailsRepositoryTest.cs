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
    public class PaymentAddressDetailsRepositoryTest
    {

        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private PaymentAddressDetailsRepository _paymentAddressDetailsTestRepository;


        private PaymentAddressDetailsData _mockData;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);


            _paymentAddressDetailsTestRepository = new PaymentAddressDetailsRepository(_testUnitOfWork);

            //Mocking Repository
            _mockData = new PaymentAddressDetailsData();

            //Setup Data
            var testData = new PaymentAddressDetailsData();
            _testDbContext.PaymentAddressDetails.AddRange(testData.PaymentAddressDetailsList);
            _testDbContext.SaveChanges();

        }
        [TestMethod]
        public void GetAll_Return_Test()
        {
            //Act 
            var paymentAddressEntities = _paymentAddressDetailsTestRepository.GetPaymentAddressDetails();

            //Assert
            Assert.IsNotNull(paymentAddressEntities); // Test if null
            Assert.IsTrue(paymentAddressEntities.Count() == 1);
        }
        [TestMethod]
        public void InsertPaymentAddressDetails_WithNewDetails_Test()
        {
            var paymentDetailsEntity = new PaymentDetailsModel
            {
               
                PaymentId = "34cd9f7d-988c-4cd9-81c9-d740255bbfc6",
                FullAddress = "12100 Wilshire Boulevard, Suite 1400",
                BusinessName = "APROSERVE CORPORATION",
                City = "Los Angeles",
                State = "USA",
                Country = "",
                ContactNumber1 = "",
                ContactNumber2 = "",
                Email = "Test123@gmail.com",
                Zip = "90025",
                StreetNumber = "12100 Wilshire Boulevard, Suite 1400",
                StreetName = "",
                StreetType = "",
                Quadrant = "",
                UnitNumber = "",
                ContactFirstName = "",
                ContactMiddleName = "",
                ContactLastName = ""
            };
            //Act 
            var paymentEntityRows = _paymentAddressDetailsTestRepository.InsertPaymentAddressDetails(paymentDetailsEntity);

            //Assert
            Assert.IsNotNull(paymentEntityRows); // Test if null
            Assert.IsTrue(paymentEntityRows == true);
        }
        [TestMethod]
        public void InsertPaymentAddressDetails_WithNullValues_Test()
        {
            var paymentDetailsEntity = new PaymentDetailsModel { };
            //Act 
            var paymentEntityRows = _paymentAddressDetailsTestRepository.InsertPaymentAddressDetails(paymentDetailsEntity);

            //Assert
            Assert.IsNotNull(paymentEntityRows); // Test if null
            Assert.IsTrue(paymentEntityRows == true);
        }
        [TestMethod]
        public void FindByPaymentId_Test()
        {
         
            //Act 
            var paymentEntityRows = _paymentAddressDetailsTestRepository.FindByPaymentId("34cd9f7d-988c-4cd9-81c9-d740255bbfc6").ToList();

            //Assert
            Assert.IsNotNull(paymentEntityRows); // Test if null
            Assert.IsTrue(paymentEntityRows.Count()==1);
        }
    }

}
