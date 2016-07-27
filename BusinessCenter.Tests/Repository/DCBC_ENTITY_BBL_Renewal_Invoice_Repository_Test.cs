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
    public class DCBC_ENTITY_BBL_Renewal_Invoice_Repository_Test
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private DCBC_ENTITY_BBL_Renewal_InvoiceRepository _BBL_Renewal_InvoiceReposiotory;
        private  Lookup_ExistingCategoriesRepository _lookupExistingCategoriesRepository;

        //  private DCBC_ENTITY_BBL_Renewal_InvoiceData _mockData;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);

            _lookupExistingCategoriesRepository = new Lookup_ExistingCategoriesRepository(_testUnitOfWork);
            _BBL_Renewal_InvoiceReposiotory = new DCBC_ENTITY_BBL_Renewal_InvoiceRepository(_testUnitOfWork, _lookupExistingCategoriesRepository);

            //Mocking Repository
            //  _mockData = new DCBC_ENTITY_BBL_Renewal_InvoiceData();

            //Setup Data
            var testData = new DCBC_ENTITY_BBL_Renewal_InvoiceData();
            _testDbContext.DCBC_ENTITY_BBL_Renewal_Invoice.AddRange(testData.DcbcEntityBblRenewalsList);
            _testDbContext.SaveChanges();

        }
        [TestMethod]
        public void GetRenewalDetailsWithLrenNumber()
        {

            //Act 
            var renewalListRows = _BBL_Renewal_InvoiceReposiotory.FindAmountByLicense("LREN11002680");

            //Assert
            Assert.IsNotNull(renewalListRows); // Test if null
            Assert.IsTrue(renewalListRows.Count() == 8);
        }

        [TestMethod]
        public void RenewalCalculation_ToGetGrandTotal_WithExpired_ReturnTest()
        {
            //initilize
            var renewmodel = new RenewModel { LrenNumber = "LREN11002680", CategoryName = "Cigarette Retail", Extradays = "Expired" };


            //Act 
            var renewalListRows = _BBL_Renewal_InvoiceReposiotory.RenewalCalculation(renewmodel);

            //Assert
            Assert.IsNotNull(renewalListRows); // Test if null
            Assert.IsTrue(renewalListRows == 1067.5M);
        }
        [TestMethod]
        public void RenewalCalculation_ToGetGrandTotal_WithLapsed_ReturnTest()
        {
            //initilize
            var renewmodel = new RenewModel { LrenNumber = "LREN11002680", CategoryName = "Cigarette Retail", Extradays = "Lapsed" };


            //Act 
            var renewalListRows = _BBL_Renewal_InvoiceReposiotory.RenewalCalculation(renewmodel);

            //Assert
            Assert.IsNotNull(renewalListRows); // Test if null
            Assert.IsTrue(renewalListRows == 817.5M);
        }
        [TestMethod]
        public void RenewalCalculation_ToGetGrandTotal_WithLapsed_NoLrenNumber_ReturnTest()
        {
            //initilize
            var renewmodel = new RenewModel { LrenNumber = "LREN11002826123", CategoryName = "Cigarette Retail", Extradays = "Lapsed" };


            //Act 
            var renewalListRows = _BBL_Renewal_InvoiceReposiotory.RenewalCalculation(renewmodel);

            //Assert
            Assert.IsNotNull(renewalListRows); // Test if null
            Assert.IsTrue(renewalListRows == 0);
        }

        [TestMethod]
        public void RenewalCalculation_ToGetGrandTotal_WithLapsed_NoApplicationFee_ReturnTest()
        {
            //initilize
            var renewmodel = new RenewModel { LrenNumber = "LREN11002786", CategoryName = "One Family Rental", Extradays = "Lapsed" };


            //Act 
            var renewalListRows = _BBL_Renewal_InvoiceReposiotory.RenewalCalculation(renewmodel);

            //Assert
            Assert.IsNotNull(renewalListRows); // Test if null
            Assert.IsTrue(renewalListRows == 444.5M);
        }
        //
    }
}
