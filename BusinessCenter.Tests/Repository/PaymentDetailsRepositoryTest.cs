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
    public class PaymentDetailsRepositoryTest
    {

        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private PaymentDetailsRepository _paymentDetailsTestRepository;

        private PaymentCardDetailsRepository _payCardDetailsTestRepository;
        private PaymentAddressDetailsRepository _payAddressTestRepository;  
        private SubmissionMasterRepository _subMasterTestRepository;           
        private SubmissionCategoryRepository _submissionCategoryTestRepository;     
        private SubmissionDocumentRepository _submissionDocumentTestRepository;        
        private SubmissionMasterApplicationChcekListRepository _subChcekListAppTestRepository;
        private FixFeeRepository _fixfeeTestRepository;
        private SubmissionMasterRenewalRepository _submissionMasterRenewalTestRepository;
        private DCBC_ENTITY_BBL_Renewal_InvoiceRepository _dCbcEntityBblRenewalInvoiceTestRepository;
        private BblRepository _bblTestRepository;
        private UserBBLServiceRepository _userBblServiceTestRepository;
       
        private Mock<IUserRoleRepository> _mockUserRoleRepository;

        private SubmissionQuestionRepository _submissionQuestionTestRepository;
        private MasterCategoryPhysicalLocationRepository _masterCategoryPhysicalLocationTestRepository;
        private MasterPrimaryCategoryRepository _masterPrimaryCategoryTestRepository;
        private UserRepository _userTestRepository;
        private DCBC_ENTITY_BBL_RenewalsRepository _dcbcEntityBblRenewalsTestRepository;
        private MasterBusinessActivityRepository _masterBusinessActivityTestRepository;
        private SubmissionBblAssociationToUsersRepository _submissionBblAssociationToUsersTestRepository;
        private SubmissionToAccelaRepository _submissionToAccelaTestRepository;
        private MasterSecondaryLicenseCategoryRepository _masterSecondaryLicenseCategoryTestRepository;
        private OSubCategoryFeesRepository _oSubCategoryFeesTestRepository;
        private MasterSubCategoryRepository _masterSubCategoryTestRepository;
        private FeeCodeMapRepository _feeCodeMapTestRepository;
        private MasterCategoryDocumentRepository _masterCategoryDocumentTestRepository;
        private SubmissionIndividualRepository _submissionIndividualTestRepository;
        private SubmissionDocumentToAccelaRepository _submissionDocumentToAccelaTestRepository;
        private MasterCategoryQuestionRepository _masterCategoryQuestionTestRepository;

       

      
  
        private Lookup_ExistingCategoriesRepository _lookupExistingCategoriesRepository;
      //  private MasterLicenseFEINRenewal masterLicenseFeinRenewal;
        private SubmissionLicenseNumberCounterRepository _submissionlicencecounter;
        private PaymentHistoryDetailsRepository _paymentHistorydetails;
        private MasterRenewalStatusFeeRepository _masterrenewalstatusfee;
        private MasterBblApplicationStatusRepository _masterBblApplicationStatusTestRepository;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
          //  masterLicenseFeinRenewal = new MasterLicenseFEINRenewal(_testUnitOfWork);
            _submissionlicencecounter = new SubmissionLicenseNumberCounterRepository(_testUnitOfWork);
            _payCardDetailsTestRepository = new PaymentCardDetailsRepository(_testUnitOfWork);
            _payAddressTestRepository = new PaymentAddressDetailsRepository(_testUnitOfWork);
            _subChcekListAppTestRepository = new SubmissionMasterApplicationChcekListRepository(_testUnitOfWork);
            _fixfeeTestRepository = new FixFeeRepository(_testUnitOfWork);
            _masterrenewalstatusfee = new MasterRenewalStatusFeeRepository(_testUnitOfWork);
            _submissionMasterRenewalTestRepository = new SubmissionMasterRenewalRepository(_testUnitOfWork, _masterrenewalstatusfee);
            _lookupExistingCategoriesRepository = new Lookup_ExistingCategoriesRepository(_testUnitOfWork);
            _dcbcEntityBblRenewalsTestRepository = new DCBC_ENTITY_BBL_RenewalsRepository(_testUnitOfWork);
            _masterBusinessActivityTestRepository = new MasterBusinessActivityRepository(_testUnitOfWork, _masterPrimaryCategoryTestRepository);
            _submissionBblAssociationToUsersTestRepository = new SubmissionBblAssociationToUsersRepository(_testUnitOfWork);
            _submissionToAccelaTestRepository = new SubmissionToAccelaRepository(_testUnitOfWork);
            _submissionDocumentToAccelaTestRepository = new SubmissionDocumentToAccelaRepository(_testUnitOfWork);
            _masterBblApplicationStatusTestRepository = new MasterBblApplicationStatusRepository(_testUnitOfWork);
           // _dCBC_ENTITY_BBL_Renewal_InvoiceData = new Mock<IDCBC_ENTITY_BBL_Renewal_InvoiceRepository>();
            _bblTestRepository = new BblRepository(_testUnitOfWork);
            _userBblServiceTestRepository = new UserBBLServiceRepository(_testUnitOfWork);
            _masterCategoryQuestionTestRepository = new MasterCategoryQuestionRepository(_testUnitOfWork);
            _submissionQuestionTestRepository = new SubmissionQuestionRepository(_testUnitOfWork);
            _masterSecondaryLicenseCategoryTestRepository = new MasterSecondaryLicenseCategoryRepository(_testUnitOfWork);
            _masterCategoryDocumentTestRepository = new MasterCategoryDocumentRepository(_testUnitOfWork);
            _dCbcEntityBblRenewalInvoiceTestRepository = new DCBC_ENTITY_BBL_Renewal_InvoiceRepository(_testUnitOfWork,
               _lookupExistingCategoriesRepository);
            _masterPrimaryCategoryTestRepository = new MasterPrimaryCategoryRepository(_testUnitOfWork, _masterSecondaryLicenseCategoryTestRepository, _masterCategoryDocumentTestRepository, _masterCategoryQuestionTestRepository);
            _masterCategoryPhysicalLocationTestRepository = new MasterCategoryPhysicalLocationRepository(_testUnitOfWork, _masterPrimaryCategoryTestRepository,
                _masterCategoryQuestionTestRepository,
                _oSubCategoryFeesTestRepository,
                _masterSecondaryLicenseCategoryTestRepository);
            _masterSubCategoryTestRepository = new MasterSubCategoryRepository(_testUnitOfWork, _masterPrimaryCategoryTestRepository, _masterSecondaryLicenseCategoryTestRepository);
            _oSubCategoryFeesTestRepository = new OSubCategoryFeesRepository(_testUnitOfWork, _masterPrimaryCategoryTestRepository, _masterSecondaryLicenseCategoryTestRepository);
            _feeCodeMapTestRepository = new FeeCodeMapRepository(_testUnitOfWork, _oSubCategoryFeesTestRepository);
            _mockUserRoleRepository = new Mock<IUserRoleRepository>();

            _userTestRepository = new UserRepository(_testUnitOfWork, _mockUserRoleRepository.Object);
         

            _subMasterTestRepository = new SubmissionMasterRepository(_testUnitOfWork, _submissionCategoryTestRepository, _bblTestRepository,
               _userBblServiceTestRepository, _submissionQuestionTestRepository, _masterCategoryPhysicalLocationTestRepository, _masterPrimaryCategoryTestRepository,
               _subChcekListAppTestRepository, _userTestRepository, _dcbcEntityBblRenewalsTestRepository, _masterBusinessActivityTestRepository,
               _submissionBblAssociationToUsersTestRepository, _submissionToAccelaTestRepository, _masterSecondaryLicenseCategoryTestRepository,
               _lookupExistingCategoriesRepository, _submissionlicencecounter, _masterBblApplicationStatusTestRepository);

          
            _submissionCategoryTestRepository = new SubmissionCategoryRepository(_testUnitOfWork, _masterPrimaryCategoryTestRepository,
               _masterSecondaryLicenseCategoryTestRepository,
               _oSubCategoryFeesTestRepository, _fixfeeTestRepository,
               _submissionQuestionTestRepository, _masterSubCategoryTestRepository, _masterCategoryPhysicalLocationTestRepository,
               _feeCodeMapTestRepository, _subChcekListAppTestRepository, _submissionMasterRenewalTestRepository, _dCbcEntityBblRenewalInvoiceTestRepository,
               _lookupExistingCategoriesRepository);

            _submissionIndividualTestRepository = new SubmissionIndividualRepository(_testUnitOfWork, _subMasterTestRepository);


            _submissionDocumentTestRepository = new SubmissionDocumentRepository(_testUnitOfWork,
             _submissionCategoryTestRepository,
             _masterCategoryDocumentTestRepository,
             _masterPrimaryCategoryTestRepository,
             _masterSecondaryLicenseCategoryTestRepository, _subMasterTestRepository,
             _masterCategoryPhysicalLocationTestRepository, _subChcekListAppTestRepository,
             _submissionIndividualTestRepository, _submissionDocumentToAccelaTestRepository);

          _paymentHistorydetails=new PaymentHistoryDetailsRepository(_testUnitOfWork);

            _paymentDetailsTestRepository = new PaymentDetailsRepository(_testUnitOfWork,
              _payCardDetailsTestRepository,
              _payAddressTestRepository,
              _subMasterTestRepository,
             _submissionCategoryTestRepository,
             _submissionDocumentTestRepository,
             _subChcekListAppTestRepository,
            _fixfeeTestRepository,
            _submissionMasterRenewalTestRepository,
            _dCbcEntityBblRenewalInvoiceTestRepository,
           _bblTestRepository,
          _userBblServiceTestRepository,
          _lookupExistingCategoriesRepository,_paymentHistorydetails);

            //_mockPrimaryData = new MasterPrimaryCategoryRepositoryData();
            //_mockPrimaryData1 = new MasterSecondaryLicenseCategoryRepositoryData();

            //Setup Data
            var testData = new PaymentDetailsData();
            _testDbContext.PaymentDetails.AddRange(testData.PaymentDetailsList);
            _testDbContext.SaveChanges();

            // 
            var paymentCardData = new PaymentCardDetailsData();
            _testDbContext.PaymentCardDetails.AddRange(paymentCardData.PaymentDetailsList);
            _testDbContext.SaveChanges();

            var paymentAddressDetailsData = new PaymentAddressDetailsData();
            _testDbContext.PaymentAddressDetails.AddRange(paymentAddressDetailsData.PaymentAddressDetailsList);
            _testDbContext.SaveChanges();


            //Setup Data
            var submissionMasterRepositoryData = new SubmissionMasterRepositoryData();
            _testDbContext.SubmissionMaster.AddRange(submissionMasterRepositoryData.SubmissionMasterEntitiesList);
            _testDbContext.SaveChanges();

            //Setup Data
            var masterCategoryQuestionData = new MasterCategoryQuestionData();
            _testDbContext.MasterCategoryQuestion.AddRange(masterCategoryQuestionData.CategoryQuestionEntitiesList);
            _testDbContext.SaveChanges();
           
            //Setup Data
            var submissionCategoryRepositoryData = new SubmissionCategoryRepositoryData();
            _testDbContext.SubmissionCategory.AddRange(submissionCategoryRepositoryData.SubmissionCategoryEntitiesList);
            _testDbContext.SaveChanges();

            //Setup Data
            var submissionDocumentRepositoryData = new SubmissionDocumentRepositoryData();
            _testDbContext.SubmissionDocument.AddRange(submissionDocumentRepositoryData.SubmissionDocumentList);
            _testDbContext.SaveChanges();

            //Setup Data
            var checklistData = new SubmissionMasterApplicationChcekListRepositoryData();
            _testDbContext.SubmissionMaster_ApplicationCheckList.AddRange(checklistData.SubmissionMaster_ApplicationCheckListEntitiesList);
            _testDbContext.SaveChanges();
            
            //Setup Data
            var fixFeeRepositoryData = new FixFeeRepositoryData();
            _testDbContext.FixFee.AddRange(fixFeeRepositoryData.FixFeeEntitiesList);
            _testDbContext.SaveChanges();

            var submissionMasterRenewalData = new SubmissionMasterRenewalData();
            _testDbContext.SubmissionMasterRenewal.AddRange(submissionMasterRenewalData.MasterRenewalEntitiesList);
            _testDbContext.SaveChanges();

            var bblRenewalInvoiceData = new DCBC_ENTITY_BBL_Renewal_InvoiceData();
            _testDbContext.DCBC_ENTITY_BBL_Renewal_Invoice.AddRange(bblRenewalInvoiceData.DcbcEntityBblRenewalsList);
            _testDbContext.SaveChanges();

            var bblRepositoryData = new BblRepositoryData();
            _testDbContext.DCBC_ENTITY_BBL.AddRange(bblRepositoryData.BblEntitiesList);
            _testDbContext.SaveChanges();

            var userBblServiceRepositoryData = new UserBBLServiceRepositoryData();
            _testDbContext.UserBBLService.AddRange(userBblServiceRepositoryData.UserBblServiceList);
            _testDbContext.SaveChanges();

            var submissionQuestionData = new SubmissionQuestionRepositoryData();
            _testDbContext.SubmissionQuestion.AddRange(submissionQuestionData.SubmissionQuestionEntitiesList);
            _testDbContext.SaveChanges();

            var categoryPhysicalLocationData = new MasterCategoryPhysicalLocationData();
            _testDbContext.MasterCategoryPhysicalLocation.AddRange(categoryPhysicalLocationData.MasterCategoryPhysicalLocationList);
            _testDbContext.SaveChanges();


            var primaryCategoryData = new MasterPrimaryCategoryRepositoryData();
            _testDbContext.MasterPrimaryCategory.AddRange(primaryCategoryData.MasterPrimaryCategoryEntitiesList);
            _testDbContext.SaveChanges();


            var secondaryCategoryData = new MasterSecondaryLicenseCategoryRepositoryData();
            _testDbContext.MasterSecondaryLicenseCategory.AddRange(secondaryCategoryData.MasterSeconderyLicenseCategoryList);
            _testDbContext.SaveChanges();

            var masterDocumentData = new MasterCategoryDocumentRepositoryData();
            _testDbContext.MasterCategoryDocument.AddRange(masterDocumentData.MasterCategoryDocumentList);
            _testDbContext.SaveChanges();

            //Setup Data
            var lookupData = new Lookup_ExistingCategoriesData();
            _testDbContext.Lookup_ExistingCategories.AddRange(lookupData.ExistingCategoriesList);
            _testDbContext.SaveChanges();
        }
        [TestMethod]
        public void GetFindByIdTest()
        {
            //Initialize Entites
            var paymentDetails = new PaymentDetails { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97" };

            //Act 
            var paymentEntityRows = _paymentDetailsTestRepository.FindByPaymentID(paymentDetails).ToList();

            //Assert
            Assert.IsNotNull(paymentEntityRows); // Test if null
            Assert.IsTrue(paymentEntityRows.Count() == 1);
        }
        [TestMethod]
        public void InsertPaymentDetails_Returns_Test()
        {
            var paymentDetailsEntity = new PaymentDetailsModel
            {
             
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                OrderNumber = "DCRABBLORDER15953919",
                PaymentType = "submission",
                PaymentMailAddress = "Test123@gmail.com",
                Signature = "Codeit",
                IsAggree = true,
                TranscationId = "Test-Transaction",
                PaymentStatus = "Test Approval",
                PaymentDate = Convert.ToDateTime("2015-12-15 07:52:14.063"),
                ApproveBy = "",
                Description = ""
            };

            // Act 
            var paymentEntityRows =
                _paymentDetailsTestRepository.InsertPaymentDetails(paymentDetailsEntity);

            //Assert
            Assert.IsNotNull(paymentEntityRows); // Test if null
           
        }
        [TestMethod]
        public void InsertPaymentDetails_CheckNulls_Test()
        {
            var paymentDetailsEntity = new PaymentDetailsModel
            {
                PaymentType = "submission",
                IsAggree = true,
                PaymentDate = Convert.ToDateTime("2015-12-15 07:52:14.063"),
            };

            // Act 
            var paymentEntityRows =
                _paymentDetailsTestRepository.InsertPaymentDetails(paymentDetailsEntity);

            //Assert
            Assert.IsNotNull(paymentEntityRows); // Test if null

        }
        [TestMethod]
        public void UpdatePaymentDetails_Returns_Test()
        {
            //Initialize Entites
            var paymentDetailsEntity = new PaymentDetailsModel
            {
                PaymentId = "34cd9f7d-988c-4cd9-81c9-d740255bbfc6",
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                OrderNumber = "DCRABBLORDER15953919",
                PaymentType = "submission",
                PaymentMailAddress = "Test123@gmail.com",
                Signature = "Codeit",
                IsAggree = true,
                TranscationId = "Test-Transaction",
                PaymentStatus = "Test Approval",
                PaymentDate = Convert.ToDateTime("2015-12-15 07:52:14.063"),
                ApproveBy = "",
                Description = ""
            };

            // Act 
            var paymentEntityRows =
                _paymentDetailsTestRepository.UpdatePaymentDetails(paymentDetailsEntity);

            //Assert
            Assert.IsNotNull(paymentEntityRows); // Test if null
            Assert.IsTrue(paymentEntityRows == false);
        }
        [TestMethod]
        public void UpdatePaymentDetailsTrue_Returns_Test()
        {
            //Initialize Entites
            var paymentDetailsEntity = new PaymentDetailsModel
            {
                PaymentId = "34cd9f7d-988c-4cd9-81c9-d740255bbfc6",
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
                OrderNumber = "DCRABBLORDER15953919",
                PaymentType = "submission",
                PaymentMailAddress = "Test123@gmail.com",
                Signature = "Codeit",
                IsAggree = true,
                TranscationId = "Test-Transaction",
                PaymentStatus = "Test Approval",
                PaymentDate = Convert.ToDateTime("2015-12-15 07:52:14.063"),
                ApproveBy = "",
                Description = ""
            };

            // Act 
            var paymentEntityRows =
                _paymentDetailsTestRepository.UpdatePaymentDetails(paymentDetailsEntity);

            //Assert
            Assert.IsNotNull(paymentEntityRows); // Test if null
            Assert.IsTrue(paymentEntityRows == true);
        }
        [TestMethod]
        public void GetReceiptData_With_NoRenewData_Test()
        {
            //Initialize Entites
            var receiptModelEntity = new ReceiptModel
            {
                PaymentID = "34cd9f7d-988c-4cd9-81c9-d740255bbfc6",
                MasterID = "cce0b056-d2a3-485e-a02a-e34c957c4e40"    };

            // Act 
            var paymentEntityRows =
                _paymentDetailsTestRepository.GetReceiptData(receiptModelEntity);

            //Assert
            Assert.IsNotNull(paymentEntityRows); // Test if null
            Assert.IsTrue(paymentEntityRows.DocumentList.Count()!=0);
        }
        [TestMethod]
        public void GetReceiptData_ASSOCIATE_Return_Test()
        {
            //Initialize Entites
            var receiptModelEntity = new ReceiptModel
            {
                PaymentID = "34cd9f7d-988c-4cd9-81c9-d740255bbfc6",
                MasterID = "8c6deecc-3b72-485d-8af5-30939af94e97"
            };

            // Act 
            var paymentEntityRows =
                _paymentDetailsTestRepository.GetReceiptData(receiptModelEntity);

            //Assert
            Assert.IsNotNull(paymentEntityRows); // Test if null
            Assert.IsTrue(paymentEntityRows.DocType == "ON");
         
        }
        [TestMethod]
        public void GetReceiptData_NoRenewData_Return_Test()
        {
            //Initialize Entites
            var receiptModelEntity = new ReceiptModel
            {
                PaymentID = "34cd9f7d-988c-4cd9-81c9-d740255bbfc6",
                MasterID = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c"
            };

            // Act 
            var paymentEntityRows =
                _paymentDetailsTestRepository.GetReceiptData(receiptModelEntity);

            //Assert
            Assert.IsNotNull(paymentEntityRows); // Test if null
            Assert.IsTrue(paymentEntityRows.DocType == "IN");

        }
         
        [TestMethod]
        public void GetReceiptData_WithRenewData_Return_Test()
        {
            //Initialize Entites
            var receiptModelEntity = new ReceiptModel
            {
                PaymentID = "34cd9f7d-988c-4cd9-81c9-d740255bbfc6",
                MasterID = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c"
            };

            // Act 
            var paymentEntityRows =
                _paymentDetailsTestRepository.GetReceiptData(receiptModelEntity);

            //Assert
            Assert.IsNotNull(paymentEntityRows); // Test if null
            Assert.IsTrue(paymentEntityRows.DocType == "IN");

        }

        [TestMethod]
        public void FindAddressByPaymentId_Return_Test()
        {
            

            // Act 
            var paymentEntityRows =
                _paymentDetailsTestRepository.FindAddressByPaymentId("8c6deecc-3b72-485d-8af5-30939af94e97");

            //Assert
            Assert.IsNotNull(paymentEntityRows); // Test if null
            Assert.IsTrue(paymentEntityRows.PaymentAddressDetails.Count() == 1);
        }
        [TestMethod]
        public void InsertUpdatePaymentDetails_Returns_Test()
        {
            var paymentDetailsEntity = new PaymentDetailsModel
            {

                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
                OrderNumber = "DCRABBLORDER15953919",
                PaymentType = "submission",
                PaymentMailAddress = "Test123@gmail.com",
                Signature = "Codeit",
                IsAggree = true,
                TranscationId = "Test-Transaction",
                PaymentStatus = "Test Approval",
                PaymentDate = Convert.ToDateTime("2015-12-15 07:52:14.063"),
                ApproveBy = "",
                Description = ""
            };

            // Act 
            var paymentEntityRows =
                _paymentDetailsTestRepository.InsertPaymentDetails(paymentDetailsEntity);

            //Assert
            Assert.IsNotNull(paymentEntityRows); // Test if null

        }
    }
}
