using BusinessCenter.Data;
using BusinessCenter.Data.Implementation;
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
using BusinessCenter.Tests.Setup;

namespace BusinessCenter.Tests.Repository
{
    [TestClass]
    public class DCBC_ENTITY_BBL_RenewalsRepositoryTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private DCBC_ENTITY_BBL_RenewalsRepository _bblRenewalsRepositoryTestRepository;
        private Mock<IDCBC_ENTITY_BBL_RenewalsRepository> _mockbblRenewalsRepository;
        //private MasterLicenseFEINRenewal _masterLicenseFeinRenewalRepository;
        private DCBC_ENTITY_BBL_RenewalsData _mockData;

        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            //_masterLicenseFeinRenewalRepository = new MasterLicenseFEINRenewal(_testUnitOfWork);, _masterLicenseFeinRenewalRepository
            _bblRenewalsRepositoryTestRepository = new DCBC_ENTITY_BBL_RenewalsRepository(_testUnitOfWork);

            //Mocking Repository
            _mockbblRenewalsRepository = new Mock<IDCBC_ENTITY_BBL_RenewalsRepository>();
            _mockData = new DCBC_ENTITY_BBL_RenewalsData();

            //Setup Data
            var testData = new DCBC_ENTITY_BBL_RenewalsData();
            _testDbContext.DCBC_ENTITY_BBL_Renewals.AddRange(testData.DcbcEntityBblRenewalsList);
            _testDbContext.SaveChanges();


            //var testOsubcategoryFeeData = new MasterLicenseFEINRenewalRepositoryData();
            //_testDbContext.masterLicense_Renewal_TaxRevenue.AddRange(testOsubcategoryFeeData.MasterLicenseEntitiesList);
            //_testDbContext.SaveChanges();

        }
        [TestMethod]
        public void FindByLicenseTest()
        {

            //Act 
            var renewalRepositoryRows = _bblRenewalsRepositoryTestRepository.FindByLicense("400312000307");

            //Assert
            Assert.IsNotNull(renewalRepositoryRows); // Test if null
            Assert.IsTrue(renewalRepositoryRows.Count() == 1);
        }
        [TestMethod]
        public void FindByPinTest()
        {
            var bblAsscoiatePin = new BblAsscoiatePin
            {
                LicenseNumber = "931315000136",
                PinNumber = "184952"
            };

            //Act 
            var renewalRepositoryRows = _bblRenewalsRepositoryTestRepository.FindByPin(bblAsscoiatePin);

            //Assert
            Assert.IsNotNull(renewalRepositoryRows); // Test if null
            Assert.IsTrue(renewalRepositoryRows.Count() == 0);
        }

        [TestMethod]
        public void CheckAssociateTest()
        {
            var bblAsscoiatePin = new BblAsscoiatePin
            {
                LicenseNumber = "100112000001",
                TaxNumber = "11-1111111"
            };

            //Act 
            var renewalRepositoryRows = _bblRenewalsRepositoryTestRepository.CheckAssociate(bblAsscoiatePin);

            //Assert
            Assert.IsNotNull(renewalRepositoryRows); // Test if null
            Assert.IsTrue(renewalRepositoryRows ==true);
        }

        [TestMethod]
        public void FindByLicensenumber_Test()
        {
          

            //Act 
            var renewalRepositoryRows = _bblRenewalsRepositoryTestRepository.FindByLicensenumber("LREN11002680");

            //Assert
            Assert.IsNotNull(renewalRepositoryRows); // Test if null
            Assert.IsTrue(renewalRepositoryRows.Count()== 1);
        }

        [TestMethod]
        public void FindByPinDrca_Test()
        {
            var bblAsscoiatePin = new BblAsscoiatePin
            {
                LicenseNumber = "400312000307",
                PinNumber = "184952"
            };

            //Act 
            var renewalRepositoryRows = _bblRenewalsRepositoryTestRepository.FindByPin(bblAsscoiatePin);

            //Assert
            Assert.IsNotNull(renewalRepositoryRows); // Test if null
            Assert.IsTrue(renewalRepositoryRows.Count() == 0);
        }

        #region junk
        //    private readonly List<DCBC_ENTITY_BBL_Renewals> list = new List<DCBC_ENTITY_BBL_Renewals>()
    //    {
    //        new DCBC_ENTITY_BBL_Renewals()
    //        {
    //            As_of_Date = Convert.ToDateTime("2015-09-25"),
    //            SERV_PROV_CODE = "DC",
    //            B1_PER_ID1 = "11LIC",
    //            B1_PER_ID2 = "00000",
    //            B1_PER_ID3 = "J1013",
    //            B1_PER_GROUP = "Licenses",
    //            B1_PER_TYPE = "Business License Renewal",
    //            B1_PER_SUB_TYPE = "NA",
    //            B1_PER_CATEGORY = "NA",
    //            B1_APP_TYPE_ALIAS = "Business License Renewal",
    //            b1_Alt_ID = "LREN11001229",
    //            License_Being_Renewed = "69004944",
    //            B1_APPL_STATUS = "Billed",
    //            Renewal_Status = "Ready to Renew",

    //            B1_HSE_NBR_START = 805,
    //            B1_HSE_NBR_END = null,
    //            B1_HSE_FRAC_NBR_START = "",
    //            B1_UNIT_START = "",
    //            B1_STR_NAME = "C",
    //            B1_STR_SUFFIX = "",
    //            B1_STR_SUFFIX_DIR = "",
    //            B1_SITUS_CITY = "",
    //            B1_SITUS_STATE = "",
    //            B1_SITUS_ZIP = "",
    //            License_Period = "",
    //            OSR_Pin = "",
    //            CofO_Number = "",
    //            Review_Necessary = "",
    //            CofO_IssueDate = "",
    //            Contact_Business_Name = "",
    //            Contact_FirstName = "",
    //            Contact_MiddleName = "",
    //            Contact_LastName = "",
    //            Billing_Address1 = "",
    //            Billing_Address2 = "",
    //            Billing_Address3 = "",
    //            Billing_CITY = "",
    //            Billing_STATE = "",
    //            Billing_ZIP = "",
    //            OwnrApplicant_BUSINESS_NAME = "",
    //            OwnrApplicant_FNAME = "",
    //            OwnrApplicant_MNAME = "",
    //            OwnrApplicant_LNAME = "",
    //            OwnrApplicant_Address1 = "",
    //            OwnrApplicant_Address2 = "",
    //            OwnrApplicant_Address3 = "",
    //            OwnrApplicant_CITY = "",
    //            OwnrApplicant_STATE = "",
    //            OwnrApplicant_ZIP = "",
    //            RegAgent_BUSINESS_NAME = "",
    //            RegAgent_FNAME = "",
    //            RegAgent_MNAME = "",
    //            RegAgent_LNAME = "",
    //            RegAgent_Address1 = "",
    //            RegAgent_Address2 = "",
    //            RegAgent_Address3 = "",
    //            RegAgent_STATE = "",
    //            RegAgent_ZIP = "",
    //            Attr_TRADE_NAME = "",
    //            Business_Org = "",

    //        },
    //        new DCBC_ENTITY_BBL_Renewals()
    //        {
    //            As_of_Date = Convert.ToDateTime("2015-09-25"),
    //            SERV_PROV_CODE = "DC",
    //            B1_PER_ID1 = "11LIC",
    //            B1_PER_ID2 = "00000",
    //            B1_PER_ID3 = "J2528",
    //            B1_PER_GROUP = "Licenses",
    //            B1_PER_TYPE = "Business License Renewal",
    //            B1_PER_SUB_TYPE = "NA",
    //            B1_PER_CATEGORY = "NA",
    //            B1_APP_TYPE_ALIAS = "Business License Renewal",
    //            b1_Alt_ID = "LREN11002743",
    //            License_Being_Renewed = "67003543",
    //            B1_APPL_STATUS = "Billed",
    //            Renewal_Status = "Ready to Renew",

    //            B1_HSE_NBR_START = 805,
    //            B1_HSE_NBR_END = null,
    //            B1_HSE_FRAC_NBR_START = "",
    //            B1_UNIT_START = "",
    //            B1_STR_NAME = "C",
    //            B1_STR_SUFFIX = "",
    //            B1_STR_SUFFIX_DIR = "",
    //            B1_SITUS_CITY = "",
    //            B1_SITUS_STATE = "",
    //            B1_SITUS_ZIP = "",
    //            License_Period = "",
    //            OSR_Pin = "",
    //            CofO_Number = "",
    //            Review_Necessary = "",
    //            CofO_IssueDate = "",
    //            Contact_Business_Name = "",
    //            Contact_FirstName = "",
    //            Contact_MiddleName = "",
    //            Contact_LastName = "",
    //            Billing_Address1 = "",
    //            Billing_Address2 = "",
    //            Billing_Address3 = "",
    //            Billing_CITY = "",
    //            Billing_STATE = "",
    //            Billing_ZIP = "",
    //            OwnrApplicant_BUSINESS_NAME = "",
    //            OwnrApplicant_FNAME = "",
    //            OwnrApplicant_MNAME = "",
    //            OwnrApplicant_LNAME = "",
    //            OwnrApplicant_Address1 = "",
    //            OwnrApplicant_Address2 = "",
    //            OwnrApplicant_Address3 = "",
    //            OwnrApplicant_CITY = "",
    //            OwnrApplicant_STATE = "",
    //            OwnrApplicant_ZIP = "",
    //            RegAgent_BUSINESS_NAME = "",
    //            RegAgent_FNAME = "",
    //            RegAgent_MNAME = "",
    //            RegAgent_LNAME = "",
    //            RegAgent_Address1 = "",
    //            RegAgent_Address2 = "",
    //            RegAgent_Address3 = "",
    //            RegAgent_STATE = "",
    //            RegAgent_ZIP = "",
    //            Attr_TRADE_NAME = "",
    //            Business_Org = "",

    //        }
    //    };

    //    Mock<DCBC_ENTITY_BBL_RenewalsRepository> mockRepo = new Mock<DCBC_ENTITY_BBL_RenewalsRepository>();

    //    private readonly BblAsscoiatePin bblAsscoiatePin = new BblAsscoiatePin()
    //    {
    //        EntityId = "10000003",
    //        PinNumber = "aa1014",
    //    //LicenseNumber = "",
    //    //TaxNumber = "",
    //    //UserId  = ""
    //};

    //// IEnumerable<DCBC_ENTITY_BBL_Renewals> FindByLicense(string  LicenseNumber)
    //    [TestMethod()]
    //    public void GetSubmissionCofoOrHopDetailsTest()
    //    {
    //        string LicenseNumber = "LREN11002743";
    //        mockRepo.Setup(m => m.FindByLicense(LicenseNumber)).Returns(list);
    //        var runtimeOutput = mockRepo.Object.FindByLicense(LicenseNumber);
    //        Assert.IsTrue(runtimeOutput != null);
    //        Assert.IsTrue(runtimeOutput.FirstOrDefault().License_Being_Renewed == "69004944");
    //        Assert.IsTrue(runtimeOutput.Last().License_Being_Renewed == "67003543");
    //    }

    //    // IEnumerable<DCBC_ENTITY_BBL_Renewals> FindByPin(BblAsscoiatePin bblassociate)
    //    [TestMethod()]
    //    public void FindByPinTest()
    //    {
    //        mockRepo.Setup(m => m.FindByPin(bblAsscoiatePin)).Returns(list);
    //        var runtimeOutput = mockRepo.Object.FindByPin(bblAsscoiatePin);
    //        Assert.IsTrue(runtimeOutput != null);
    //        Assert.IsTrue(runtimeOutput.FirstOrDefault().License_Being_Renewed == "69004944");
    //        Assert.IsTrue(runtimeOutput.Last().License_Being_Renewed == "67003543");
    //    }

    //    //  bool CheckAssociate(BblAsscoiatePin bblassociate)
    //    [TestMethod()]
    //    public void CheckAssociateTest()
    //    {
    //        mockRepo.Setup(m => m.CheckAssociate(bblAsscoiatePin)).Returns(true);
    //        var runtimeOutput = mockRepo.Object.CheckAssociate(bblAsscoiatePin);
    //        Assert.AreEqual<bool>(true, runtimeOutput);
    //    }
#endregion
    }
}
