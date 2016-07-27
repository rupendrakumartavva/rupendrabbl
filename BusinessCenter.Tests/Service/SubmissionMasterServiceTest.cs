using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using BusinessCenter.Service.Implementation;
using BusinessCenter.Tests.Setup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Service
{
     [TestClass]
   public class SubmissionMasterServiceTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        private SubmissionMasterRepository _submissionMasterTestRepository;
        private SubmissionCategoryRepository _submissionCategoryRepository;
        private UserBBLServiceRepository _userBblServiceRepository;
        private BblRepository _bblRepository;
        private MasterCategoryPhysicalLocationRepository _masterCategoryPhysicalLocationRepository;
        private MasterPrimaryCategoryRepository _masterPrimaryCategoryRepository;
        private UserRepository _userRepository;
        private MasterBusinessActivityRepository _masterBusinessActivityRepository;
        private DCBC_ENTITY_BBL_RenewalsRepository _dcbcEntityBblRenewalsRepository;
        private SubmissionBblAssociationToUsersRepository _submissionBblAssociationToUsersRepository;
        private SubmissionToAccelaRepository _submissionToAccelaRepository;
        private MasterSecondaryLicenseCategoryRepository _masterSecondaryLicenseCategoryRepository;
        private SubmissionQuestionRepository _submissionQuestionRepository;
        private SubmissionMasterApplicationChcekListRepository _submissionMasterApplicationChcekListRepository;
        private OSubCategoryFeesRepository _oSubCategoryFeesRepository;
        private FixFeeRepository _fixedFeeTestRepository;
        private MasterSubCategoryRepository _masterSubCategoryRepository;
        private FeeCodeMapRepository _feeCodeMapRepository;
        private SubmissionMasterRenewalRepository _submissionMasterRenewalRepository;
        private MasterCategoryQuestionRepository _masterCategoryQuestionRepository;
        private MasterCategoryDocumentRepository _masterCategoryDocumentRepository;
        private UserRoleRepository _userRoleRepository;
        private DCBC_ENTITY_BBL_Renewal_InvoiceRepository _dcbcEntitybblRenewalInvoiceRepository;
        private Lookup_ExistingCategoriesRepository _lookupExistingCategoriesRepository;
        private SubmissionMasterService _submissionMasterTestService;
       //  private Lookup_ExistingCategoriesRepository lookup_ExistingCategoriesRepository;
         private SubmissionLicenseNumberCounterRepository _submissionlicencecounter;
         //Repository declaration
         //private SubmissionLicenseNumberCounterRepository _submissionLicenseNumberCounterTestRepository;
         private MasterBblApplicationStatusRepository _masterBblApplicationStatusTestRepository;
         private MasterRenewalStatusFeeRepository _masterRenewalStatusFeeRepository;

        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
          
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            //SubmissionLicenseNumberCounterRepository Initialization
            _masterCategoryDocumentRepository=new MasterCategoryDocumentRepository(_testUnitOfWork);
            _submissionlicencecounter = new SubmissionLicenseNumberCounterRepository(_testUnitOfWork);
            _masterCategoryQuestionRepository=new MasterCategoryQuestionRepository(_testUnitOfWork);
            _bblRepository = new BblRepository(_testUnitOfWork);
            _fixedFeeTestRepository = new FixFeeRepository(_testUnitOfWork);
            _lookupExistingCategoriesRepository=new Lookup_ExistingCategoriesRepository(_testUnitOfWork);
            _dcbcEntitybblRenewalInvoiceRepository=new DCBC_ENTITY_BBL_Renewal_InvoiceRepository(_testUnitOfWork,_lookupExistingCategoriesRepository);
            _masterRenewalStatusFeeRepository=new MasterRenewalStatusFeeRepository(_testUnitOfWork);
            _masterBblApplicationStatusTestRepository=new MasterBblApplicationStatusRepository(_testUnitOfWork);
            //_submissionLicenseNumberCounterTestRepository=new SubmissionLicenseNumberCounterRepository(_testUnitOfWork);
            _userRoleRepository=new UserRoleRepository(_testUnitOfWork);
            _submissionMasterRenewalRepository = new SubmissionMasterRenewalRepository(_testUnitOfWork, _masterRenewalStatusFeeRepository);
            _lookupExistingCategoriesRepository = new Lookup_ExistingCategoriesRepository(_testUnitOfWork);
            _masterPrimaryCategoryRepository = new MasterPrimaryCategoryRepository(_testUnitOfWork,
             _masterSecondaryLicenseCategoryRepository,
             _masterCategoryDocumentRepository, _masterCategoryQuestionRepository);
          
            _masterSecondaryLicenseCategoryRepository = new MasterSecondaryLicenseCategoryRepository(_testUnitOfWork);

            _masterBblApplicationStatusTestRepository = new MasterBblApplicationStatusRepository(_testUnitOfWork);
            _oSubCategoryFeesRepository = new OSubCategoryFeesRepository(_testUnitOfWork, _masterPrimaryCategoryRepository, _masterSecondaryLicenseCategoryRepository);
            _feeCodeMapRepository = new FeeCodeMapRepository(_testUnitOfWork, _oSubCategoryFeesRepository);
            _userBblServiceRepository = new UserBBLServiceRepository(_testUnitOfWork);
            _submissionQuestionRepository = new SubmissionQuestionRepository(_testUnitOfWork);
            _lookupExistingCategoriesRepository = new Lookup_ExistingCategoriesRepository(_testUnitOfWork);

            _masterCategoryPhysicalLocationRepository = new MasterCategoryPhysicalLocationRepository(_testUnitOfWork,
               _masterPrimaryCategoryRepository, _masterCategoryQuestionRepository, _oSubCategoryFeesRepository, _masterSecondaryLicenseCategoryRepository);
            _submissionCategoryRepository = new SubmissionCategoryRepository(_testUnitOfWork, _masterPrimaryCategoryRepository,
          _masterSecondaryLicenseCategoryRepository, _oSubCategoryFeesRepository, _fixedFeeTestRepository, _submissionQuestionRepository,
          _masterSubCategoryRepository, _masterCategoryPhysicalLocationRepository, _feeCodeMapRepository,
          _submissionMasterApplicationChcekListRepository, _submissionMasterRenewalRepository, _dcbcEntitybblRenewalInvoiceRepository,
          _lookupExistingCategoriesRepository);

            _submissionMasterApplicationChcekListRepository = new SubmissionMasterApplicationChcekListRepository(_testUnitOfWork);
            _userRepository = new UserRepository(_testUnitOfWork, _userRoleRepository);
            _dcbcEntityBblRenewalsRepository = new DCBC_ENTITY_BBL_RenewalsRepository(_testUnitOfWork);
            _masterBusinessActivityRepository = new MasterBusinessActivityRepository(_testUnitOfWork, _masterPrimaryCategoryRepository);
            _submissionBblAssociationToUsersRepository = new SubmissionBblAssociationToUsersRepository(_testUnitOfWork);
            _submissionToAccelaRepository = new SubmissionToAccelaRepository(_testUnitOfWork);
            _masterSubCategoryRepository = new MasterSubCategoryRepository(_testUnitOfWork, _masterPrimaryCategoryRepository, _masterSecondaryLicenseCategoryRepository);



            _submissionMasterTestRepository = new SubmissionMasterRepository(_testUnitOfWork, _submissionCategoryRepository, _bblRepository,
                _userBblServiceRepository, _submissionQuestionRepository, _masterCategoryPhysicalLocationRepository, _masterPrimaryCategoryRepository,
                _submissionMasterApplicationChcekListRepository, _userRepository, _dcbcEntityBblRenewalsRepository, _masterBusinessActivityRepository,
                _submissionBblAssociationToUsersRepository, _submissionToAccelaRepository, _masterSecondaryLicenseCategoryRepository,
                _lookupExistingCategoriesRepository, _submissionlicencecounter, _masterBblApplicationStatusTestRepository);

            _submissionMasterTestService = new SubmissionMasterService(_submissionMasterTestRepository);

          

            //Setup  SubmissionLicenseNumberCounter FackData Initialization
            var submissionLicenseCounterData = new SubmissionLicenseNumberCounterData();
            _testDbContext.SubmissionLicenseNumberCounter.AddRange(submissionLicenseCounterData.SubmissionLicenseCounterEntitiesList);
            _testDbContext.SaveChanges();

            //Setup Data
            var secondaryLicenceCategoryData = new MasterSecondaryLicenseCategoryRepositoryData();
            _testDbContext.MasterSecondaryLicenseCategory.AddRange(secondaryLicenceCategoryData.MasterSeconderyLicenseCategoryList);
            _testDbContext.SaveChanges();

            var submissionToAccelaData = new SubmissionToAccelaRepositoryData();
            _testDbContext.SubmissiontoAccela.AddRange(submissionToAccelaData.SubmissiontoAccelaEntitiesList);
            _testDbContext.SaveChanges();

            var associatetoUserData = new SubmissionBblAssociationToUsersRepositoryData();
            _testDbContext.SubmissionBblAssociationToUsers.AddRange(associatetoUserData.SubmissionBblAssociationToUsersEntitiesList);
            _testDbContext.SaveChanges();

            var businessActivityData = new MasterBusinessActivityData();
            _testDbContext.MasterBusinessActivity.AddRange(businessActivityData.MasterAcivityEntitiesList);
            _testDbContext.SaveChanges();

            var renewalData = new DCBC_ENTITY_BBL_RenewalsData();
            _testDbContext.DCBC_ENTITY_BBL_Renewals.AddRange(renewalData.DcbcEntityBblRenewalsList);
            _testDbContext.SaveChanges();


            var userData = new UserRepositoryData();
            _testDbContext.User.AddRange(userData.UsersEntitiesList);
            _testDbContext.SaveChanges();

            var submissionMasterApplicationChcekListRepositoryData = new SubmissionMasterApplicationChcekListRepositoryData();
            _testDbContext.SubmissionMaster_ApplicationCheckList
                .AddRange(submissionMasterApplicationChcekListRepositoryData.SubmissionMaster_ApplicationCheckListEntitiesList);
            _testDbContext.SaveChanges();

            var primaryCategoryData = new MasterPrimaryCategoryRepositoryData();
            _testDbContext.MasterPrimaryCategory.AddRange(primaryCategoryData.MasterPrimaryCategoryEntitiesList);
            _testDbContext.SaveChanges();

            var masterCategoryPhysicalLocationData = new MasterCategoryPhysicalLocationData();
            _testDbContext.MasterCategoryPhysicalLocation.AddRange(masterCategoryPhysicalLocationData.MasterCategoryPhysicalLocationList);
            _testDbContext.SaveChanges();

            var testData = new SubmissionMasterRepositoryData();
            _testDbContext.SubmissionMaster.AddRange(testData.SubmissionMasterEntitiesList);
            _testDbContext.SaveChanges();

            var submissionCategoryData = new SubmissionCategoryRepositoryData();
            _testDbContext.SubmissionCategory.AddRange(submissionCategoryData.SubmissionCategoryEntitiesList);
            _testDbContext.SaveChanges();

            var bblData = new BblRepositoryData();
            _testDbContext.DCBC_ENTITY_BBL.AddRange(bblData.BblEntitiesList);
            _testDbContext.SaveChanges();

            var userBBlData = new UserBBLServiceRepositoryData();
            _testDbContext.UserBBLService.AddRange(userBBlData.UserBblServiceList);
            _testDbContext.SaveChanges();

            var submissionQuestionData = new SubmissionQuestionRepositoryData();
            _testDbContext.SubmissionQuestion.AddRange(submissionQuestionData.SubmissionQuestionEntitiesList);
            _testDbContext.SaveChanges();

          
        }
        [TestMethod]
        public void AllSubmissionMasterTest()
        {

            //Act 
            var submissionmaster = _submissionMasterTestService.GetAllSubmissionMaster();

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster.Count() == 5);
        }
        //[TestMethod]
        //public void AllSubmissionsTest()
        //{

        //    //Act 
        //    var submissionmaster = _submissionMasterTestService.AllSubmissions("FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F");

        //    //Assert
        //    Assert.IsNotNull(submissionmaster); // Test if null
        //    Assert.IsTrue(submissionmaster.Count() == 4);
        //}
        //[TestMethod]
        //public void AllSubmissionsUserTest()
        //{

        //    //Act 
        //    var submissionmaster = _submissionMasterTestService.AllSubmissions(string.Empty);

        //    //Assert
        //    Assert.IsNotNull(submissionmaster); // Test if null
        //    Assert.IsTrue(submissionmaster.Count() == 4);
        //}
        [TestMethod]
        public void FindByIDTest()
        {
            var submissionMasterModel = new SubmissionMasterModel { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97" };
            //Act 
            var submissionmaster = _submissionMasterTestService.FindBySubmissionMasterId(submissionMasterModel);

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster.Count() == 1);
        }
        [TestMethod]
        public void FindByEntityIDTest()
        {

            //Act 
            var submissionmaster = _submissionMasterTestService.FindByEntityID("DAPP15985360");

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster.Count() == 1);
        }
        [TestMethod]
        public void GetMasterIdTest()
        {

            //Act 
            var submissionmaster = _submissionMasterTestService.GetmasterId("DAPP15985360", "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F");

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster == "cce0b056-d2a3-485e-a02a-e34c957c4e40");
        }
        [TestMethod]
        public void UpdateSubmissionMasterTest()
        {
            var paymentDetailsModel = new PaymentDetailsModel { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97", PaymentType = "Submission" };
            //Act 
            var submissionmaster = _submissionMasterTestService.UpdateSubmissionMaster(paymentDetailsModel);

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster);
        }
        [TestMethod]
        public void InsertAssociateBblServiceTest()
        {

            var questions = new List<ScreeningQuestion>();
            ScreeningQuestion submissionQuestion = new ScreeningQuestion();
            submissionQuestion.Question = "How many Seats in your Restaurant?";
            submissionQuestion.Answer = "100";
            questions.Add(submissionQuestion);
            ScreeningQuestion submissionQuestion1 = new ScreeningQuestion();
            submissionQuestion1.Question = "Would you like a two (2) or four (4) year license?";
            submissionQuestion1.Answer = "Four (4) year";
            questions.Add(submissionQuestion1);
            ScreeningQuestion submissionQuestion2 = new ScreeningQuestion();
            submissionQuestion2.Question = "Is this business already registered with DCRA’s Corporations Division?";
            submissionQuestion2.Answer = "YES";
            questions.Add(submissionQuestion2);
            ScreeningQuestion submissionQuestion3 = new ScreeningQuestion();
            submissionQuestion3.Question =
                "Which Tax Identification Number is associated with your business: Federal Employer Identification Number (FEIN) or  Social Security Number (SSN)?";
            submissionQuestion3.Answer = "FEIN";
            questions.Add(submissionQuestion3);
            ScreeningQuestion submissionQuestion4 = new ScreeningQuestion();
            submissionQuestion4.Question =
                " Will this business be located in the District of Columbia?";
            submissionQuestion4.Answer = "YES";
            questions.Add(submissionQuestion4);
            var detailedCategory = new List<DetailedCategoryList>();
            DetailedCategoryList detailedCategoryList = new DetailedCategoryList();
            detailedCategoryList.CategoryId = "16FD71A3-7DD7-4B65-A006-777691BCB7FC";
            detailedCategory.Add(detailedCategoryList);
            var submissionApplication = new SubmissionApplication
            {
                SubQuestion = questions,
                PrimaryID = "16FD71A3-7DD7-4B65-A006-777691BCB7FC",//"E037E31E-9DA2-47F0-A2AD-7FFF385EB1B3",
                App_Type = "B",
                DetailedCategoryList = detailedCategory,
                LicenseCategory = "SOLICITOR"
            };
            //Act 
            var submissionmaster = _submissionMasterTestService.InsertAssociateBblService(submissionApplication);

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            //Assert.IsFalse(submissionmaster);
        }
        //[TestMethod]
        //public void BBlServiceListTest()
        //{
        //    var bblAsscoiateService = new BblAsscoiateService { UserID = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F" };
        //    //Act 
        //    var submissionmaster = _submissionMasterTestService.GetBblService(bblAsscoiateService);

        //    //Assert
        //    Assert.IsNotNull(submissionmaster); // Test if null
        //    Assert.IsTrue(submissionmaster.FirstOrDefault().BblServiceList.FirstOrDefault().BusinessName == "IMA PIZZA STORE 13 LLC.");
        //}
        [TestMethod]
        public void UpdateUserSelectTest()
        {
            var generalBusiness = new GeneralBusiness { MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c", UserSelectTpe = "NEWMAIL" };

            //Act 
            var submissionmaster = _submissionMasterTestService.UpdateUserSelect(generalBusiness);

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster);
        }
        [TestMethod]
        public void GetMailTypeTest()
        {
            var generalBusiness = new GeneralBusiness { MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c", UserSelectTpe = "NEWMAIL" };

            //Act 
            var submissionmaster = _submissionMasterTestService.GetMailType(generalBusiness);

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster.UserType == "NEWMAIL");
        }
        [TestMethod]
        public void UpdateEhopTest()
        {
            var cofoHopDetailsModel = new CofoHopDetailsModel { MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c" };
            //Act 
            var submissionmaster = _submissionMasterTestService.UpdateEhop(cofoHopDetailsModel);

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster);
        }
        [TestMethod]
        public void ValidateBblLicenceTest()
        {
            //Act 
            var submissionmaster = _submissionMasterTestService.ValidateBblLicence("931315000136");

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster == "IMA PIZZA STORE 13 LLC.");
        }
        [TestMethod]
        public void UpdateEhopNonSelectTest()
        {
            //Act 
            var submissionmaster = _submissionMasterTestService.UpdateEhopNonSelect("33b635b9-fec2-4b4f-9d07-686b8d6cc60c");

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster);
        }
        [TestMethod]
        public void GetUserAssociateBblListCountTest()
        {
            var bblAsscoiateService = new BblAsscoiateService { UserID = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F" };
            //Act 
            var submissionmaster = _submissionMasterTestService.GetUserAssociateBblCount(bblAsscoiateService);

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster == 0);
        }
        [TestMethod]
        public void MasterStatusTest()
        {

            //Act 
            var submissionmaster = _submissionMasterTestService.MasterStatus("33b635b9-fec2-4b4f-9d07-686b8d6cc60c");

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster.Status == "UnderReview");
        }
        [TestMethod]
        public void UpdateUserAssociateExpiryDateTest()
        {

            //Act 
            var submissionmaster = _submissionMasterTestService.UpdateUserAssociateExpiryDate("8c6deecc-3b72-485d-8af5-30939af94e97");

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster);
        }
        [TestMethod]
        public void TransferSubmissionsTest()
        {
            var submissiontransfer = new Submissiontransfer { MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c" };

            //Act 
            var submissionmaster = _submissionMasterTestService.TransferSubmissions(submissiontransfer);

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster);
        }

        [TestMethod]
        public void GetBblServicesTest()
        {
            //var submissiontransfer = new Submissiontransfer { MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c" };

            //Act 
            var submissionmaster = _submissionMasterTestService.GetBblServices("1");

            //Assert
            Assert.IsNotNull(submissionmaster);
           Assert.IsTrue(submissionmaster.Count()==1);
        }
        [TestMethod]
        public void GetRenewMasterUserAssociateIDTest()
        {
            var renewModel = new RenewModel { UserBblAssociateId = "1" };

            //Act 
            var submissionmaster = _submissionMasterTestService.GetRenewMasterUserAssociateID(renewModel);

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster.Count() == 1);
        }
        [TestMethod]
        public void GetApplicationReviewCounts_Test()
        {

            //Act
            var submissionmaster = _submissionMasterTestService.GetApplicationReviewCounts();

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster.UnderReviewlistCount == 1);
            Assert.IsTrue(submissionmaster.DraftlistCount == 4);
        }
        [TestMethod]
        public void GetAddressDetails_Test()
        {
            //EntityData is Added
            var taxRevenueData = new TaxRevenueData { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97", EntityId = "1", SubmissionLicense = "LREN11002680" };
            //Act
            var submissionmaster = _submissionMasterTestService.GetAddressDetails(taxRevenueData);

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster.BusinessType == "BUSINESS LICENSE");
        }
        [TestMethod]
        public void FindByMasterIDTest()
        {
            //Act
            var submissionmaster = _submissionMasterTestService.FindByMasterID("8c6deecc-3b72-485d-8af5-30939af94e97");

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster.Count() == 1);
        }
        [TestMethod]
        public void UpdateSubmissionOrder_Test()
        {

            //Act
            var submissionmaster = _submissionMasterTestService.UpdateSubmissionOrder("8c6deecc-3b72-485d-8af5-30939af94e97", "", "");

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster);
        }
        [TestMethod]
        public void GetUserDetailsTest()
        {

            //Act
            var submissionmaster = _submissionMasterTestService.GetUserDetails().ToList();

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster.Count() == 5);
        }
        [TestMethod]
        public void UpdateUserSubmissionExpiryDate_Test()
        {

            //Act
            var submissionmaster = _submissionMasterTestService.UpdateUserSubmissionExpiryDate("8c6deecc-3b72-485d-8af5-30939af94e97");

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster);
        }
        [TestMethod]
        public void UserBblDetails_Test()
        {

            //Act
            var submissionmaster = _submissionMasterTestService.UserBblDetails("8c6deecc-3b72-485d-8af5-30939af94e97");

            //Assert
            Assert.IsNotNull(submissionmaster);
           Assert.IsTrue(submissionmaster.Count()==1);
        }
        [TestMethod]
        public void SubmissionDetailsBasedonLicenseandUserIdTest()
        {
            //Act
            var submissionmaster = _submissionMasterTestService.SubmissionDetailsBasedonLicenseandUserId("DAPP15985360", "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F");

            //Assert
            Assert.IsNotNull(submissionmaster); // Test if null
            Assert.IsTrue(submissionmaster.Count() == 1);
        }
          [TestMethod]
        public void UserBblAssociationServiceTest()
        {

            //Act
            var submissionmaster = _submissionMasterTestService.UserBblAssociationService().ToList();

            //Assert
            Assert.IsNotNull(submissionmaster);
            Assert.IsTrue(submissionmaster.Count() == 6);
        }
          [TestMethod]
          public void GetBBlServiceListTest()
          {
              //EntityData is Added
              var bblAsscoiateService = new BblAsscoiateService { UserID = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F" };

              string MultipleLicenseName = string.Empty;
              //Act
              var submissionmaster = _submissionMasterTestService.GetBblService(bblAsscoiateService).ToList();


              foreach (var item in submissionmaster[0].BblServiceList)
              {
                  MultipleLicenseName = item.MultipleLicense;
              }

              //Assert
              Assert.IsNotNull(submissionmaster); // Test if null
              Assert.IsTrue(MultipleLicenseName == null);
          }

   
    }
}
