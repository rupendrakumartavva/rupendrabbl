using System;
using BusinessCenter.Data;
using BusinessCenter.Data.Interface;
using BusinessCenter.Email;
using BusinessCenter.Identity.Interfaces;
using BusinessCenter.Service.Interface;
using BussinessCenter.reCaptcha;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using BusinessCenter.Api.Controllers;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Model;
using BusinessCenter.Service.Implementation;
using BusinessCenter.Tests.Setup;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BusinessCenter.Api.Test.Controllers
{
    [TestClass]
    public class BblCategoryControllerTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

 
        private MasterPrimaryCategoryRepository _masterPrimaryCategoryTestRepository;
        private MasterBusinessActivityService _mockBusinessActivityTestService;
     
        private MasterBusinessActivityRepository _masterBusinessActivityTestRepository;


        private MasterSecondaryLicenseCategoryRepository _mockMasterSecondaryLicenseCategoryRepository;
        private MasterCategoryDocumentRepository _mockMasterCategoryDocumentRepository;
        private MasterCategoryQuestionRepository _mockMasterCategoryQuestionRepository;


        private  MasterPrimaryCategoryService _bblPrimaryCategories;
        private MasterSecondaryLicenseCategoryService _masterSecondaryLicenseCategoryService;

 

        #region SubmissionMasterRepo
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
        private SubmissionLicenseNumberCounterRepository _submissionlicencecounter;
        //Repository declaration
        private SubmissionLicenseNumberCounterRepository _submissionLicenseNumberCounterTestRepository;
        private MasterBblApplicationStatusRepository _masterBblApplicationStatusTestRepository;
        #endregion

        #region Declaration of MasterCategoryPhysicalLocation Service
        private MasterCategoryPhysicalLocationRepository _masterCategoryPhysicalLocationTestRepository;
        private MasterPrimaryCategoryRepository _mockMasterPrimaryCategoryRepository;
        private OSubCategoryFeesRepository _mockSubCategoryFeesRepository;
      //  private MasterCategoryPhysicalLocationData _mockData;
        private MasterCategoryDocumentRepository _masterCategoryDocument;
        private MasterCategoryPhysicalLocationService _masterCategoryPhysicalLocationTestService;
        #endregion


        #region Declaration of SubmissionCategoryService
        private SubmissionCategoryRepository _submissionCategoryTestRepository;
     
        private SubmissionQuestionRepository _submissionQuestionTestRepository;
        private MasterSubCategoryRepository _masterSubCategoryTestRepository;
      
        private FeeCodeMapRepository _feeCodeMapTestRepository;
        private SubmissionMasterApplicationChcekListRepository _checkListTestRepository;
        private Mock<IDCBC_ENTITY_BBL_Renewal_InvoiceRepository> _iDcbcEntityBblRenewalInvoiceReposiotory;
        private MasterRenewalStatusFeeRepository _masterrenewalstatusfee;
        private SubmissionCategoryService _submissionCategoryTestService;
#endregion

        private BblCategoryController _controller = null;

        #region Declaration Of MasterSubCategoryService
      private MasterSubCategoryService _mockMasterSubCategoryTestService;

       

        #endregion


        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);


            _mockMasterSecondaryLicenseCategoryRepository = new MasterSecondaryLicenseCategoryRepository(_testUnitOfWork);
            _mockMasterCategoryDocumentRepository = new MasterCategoryDocumentRepository(_testUnitOfWork);
            _mockMasterCategoryQuestionRepository = new MasterCategoryQuestionRepository(_testUnitOfWork);

            _masterPrimaryCategoryTestRepository = new MasterPrimaryCategoryRepository(_testUnitOfWork,
                _mockMasterSecondaryLicenseCategoryRepository,
                _mockMasterCategoryDocumentRepository,
                _mockMasterCategoryQuestionRepository);

            _masterBusinessActivityTestRepository = new MasterBusinessActivityRepository(_testUnitOfWork, _masterPrimaryCategoryTestRepository);


            _mockBusinessActivityTestService = new MasterBusinessActivityService(_masterBusinessActivityTestRepository);
            _bblPrimaryCategories = new MasterPrimaryCategoryService(_masterPrimaryCategoryTestRepository);

            _masterSecondaryLicenseCategoryService = new MasterSecondaryLicenseCategoryService(_mockMasterSecondaryLicenseCategoryRepository);



            #region SubmissionMasterRepoInitialization

            _submissionlicencecounter = new SubmissionLicenseNumberCounterRepository(_testUnitOfWork);

            _bblRepository = new BblRepository(_testUnitOfWork);
            //   masterLicenseFeinRenewal = new MasterLicenseFEINRenewal(_testUnitOfWork);
            _lookupExistingCategoriesRepository = new Lookup_ExistingCategoriesRepository(_testUnitOfWork);
            _masterPrimaryCategoryRepository = new MasterPrimaryCategoryRepository(_testUnitOfWork,
             _masterSecondaryLicenseCategoryRepository,
             _masterCategoryDocumentRepository, _masterCategoryQuestionRepository);

            _masterSecondaryLicenseCategoryRepository = new MasterSecondaryLicenseCategoryRepository(_testUnitOfWork);



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
            _oSubCategoryFeesRepository = new OSubCategoryFeesRepository(_testUnitOfWork, _masterPrimaryCategoryRepository, _masterSecondaryLicenseCategoryRepository);
            _masterSubCategoryRepository = new MasterSubCategoryRepository(_testUnitOfWork, _masterPrimaryCategoryRepository, _masterSecondaryLicenseCategoryRepository);
            _feeCodeMapRepository = new FeeCodeMapRepository(_testUnitOfWork, _oSubCategoryFeesRepository);
            _masterCategoryQuestionRepository = new MasterCategoryQuestionRepository(_testUnitOfWork);
            _masterCategoryDocumentRepository = new MasterCategoryDocumentRepository(_testUnitOfWork);
            _userRoleRepository = new UserRoleRepository(_testUnitOfWork);
            _dcbcEntitybblRenewalInvoiceRepository = new DCBC_ENTITY_BBL_Renewal_InvoiceRepository(_testUnitOfWork, _lookupExistingCategoriesRepository);
            _submissionLicenseNumberCounterTestRepository = new SubmissionLicenseNumberCounterRepository(_testUnitOfWork);
            _masterBblApplicationStatusTestRepository = new MasterBblApplicationStatusRepository(_testUnitOfWork);

            _submissionMasterTestRepository = new SubmissionMasterRepository(_testUnitOfWork, _submissionCategoryRepository, _bblRepository,
                _userBblServiceRepository, _submissionQuestionRepository, _masterCategoryPhysicalLocationRepository, _masterPrimaryCategoryRepository,
                _submissionMasterApplicationChcekListRepository, _userRepository, _dcbcEntityBblRenewalsRepository, _masterBusinessActivityRepository,
                _submissionBblAssociationToUsersRepository, _submissionToAccelaRepository, _masterSecondaryLicenseCategoryRepository,
                _lookupExistingCategoriesRepository, _submissionlicencecounter, _masterBblApplicationStatusTestRepository);

            _submissionMasterTestService = new SubmissionMasterService(_submissionMasterTestRepository);

            #endregion

            #region Initialization Of MasterCategoryService
            _mockMasterSecondaryLicenseCategoryRepository = new MasterSecondaryLicenseCategoryRepository(_testUnitOfWork);



            _masterCategoryDocument = new MasterCategoryDocumentRepository(_testUnitOfWork);
            _mockMasterCategoryQuestionRepository = new MasterCategoryQuestionRepository(_testUnitOfWork);
            _mockMasterPrimaryCategoryRepository = new MasterPrimaryCategoryRepository(_testUnitOfWork,
                                _mockMasterSecondaryLicenseCategoryRepository,
                                _masterCategoryDocument, _mockMasterCategoryQuestionRepository);





            _mockSubCategoryFeesRepository = new OSubCategoryFeesRepository(_testUnitOfWork, _mockMasterPrimaryCategoryRepository,
              _mockMasterSecondaryLicenseCategoryRepository);





            _masterCategoryPhysicalLocationTestRepository = new MasterCategoryPhysicalLocationRepository(_testUnitOfWork,
                                                            _mockMasterPrimaryCategoryRepository,
                                                            _mockMasterCategoryQuestionRepository
                                                            , _mockSubCategoryFeesRepository
                                                            , _mockMasterSecondaryLicenseCategoryRepository);


            _masterCategoryPhysicalLocationTestService = new MasterCategoryPhysicalLocationService(_masterCategoryPhysicalLocationTestRepository);
            #endregion

            #region Initilazation of SubmissionCategoryService
            _masterrenewalstatusfee = new MasterRenewalStatusFeeRepository(_testUnitOfWork);
            _lookupExistingCategoriesRepository = new Lookup_ExistingCategoriesRepository(_testUnitOfWork);
            _mockMasterSecondaryLicenseCategoryRepository = new MasterSecondaryLicenseCategoryRepository(_testUnitOfWork);
            _masterCategoryDocument = new MasterCategoryDocumentRepository(_testUnitOfWork);
            _mockMasterCategoryQuestionRepository = new MasterCategoryQuestionRepository(_testUnitOfWork);
            _fixedFeeTestRepository = new FixFeeRepository(_testUnitOfWork);
            _submissionQuestionTestRepository = new SubmissionQuestionRepository(_testUnitOfWork);
            _checkListTestRepository = new SubmissionMasterApplicationChcekListRepository(_testUnitOfWork);
            _submissionMasterRenewalRepository = new SubmissionMasterRenewalRepository(_testUnitOfWork, _masterrenewalstatusfee);

            _iDcbcEntityBblRenewalInvoiceReposiotory = new Mock<IDCBC_ENTITY_BBL_Renewal_InvoiceRepository>();

            _mockMasterPrimaryCategoryRepository = new MasterPrimaryCategoryRepository(_testUnitOfWork,
                             _mockMasterSecondaryLicenseCategoryRepository,
                             _masterCategoryDocument, _mockMasterCategoryQuestionRepository);


            _mockSubCategoryFeesRepository = new OSubCategoryFeesRepository(_testUnitOfWork, _mockMasterPrimaryCategoryRepository,
              _mockMasterSecondaryLicenseCategoryRepository);


            _masterSubCategoryTestRepository = new MasterSubCategoryRepository(_testUnitOfWork,
                                                                               _mockMasterPrimaryCategoryRepository,
                                                                               _mockMasterSecondaryLicenseCategoryRepository);
            

            _masterCategoryPhysicalLocationTestRepository = new MasterCategoryPhysicalLocationRepository(_testUnitOfWork,
                                                            _mockMasterPrimaryCategoryRepository,
                                                            _mockMasterCategoryQuestionRepository
                                                            , _mockSubCategoryFeesRepository
                                                            , _mockMasterSecondaryLicenseCategoryRepository);
            _feeCodeMapTestRepository = new FeeCodeMapRepository(_testUnitOfWork, _mockSubCategoryFeesRepository);

            _submissionCategoryTestRepository = new SubmissionCategoryRepository(_testUnitOfWork, _mockMasterPrimaryCategoryRepository,
                _mockMasterSecondaryLicenseCategoryRepository,
                _mockSubCategoryFeesRepository, _fixedFeeTestRepository,
                _submissionQuestionTestRepository, _masterSubCategoryTestRepository, _masterCategoryPhysicalLocationTestRepository,
                _feeCodeMapTestRepository, _checkListTestRepository, _submissionMasterRenewalRepository, _iDcbcEntityBblRenewalInvoiceReposiotory.Object,
               _lookupExistingCategoriesRepository);

            _submissionCategoryTestService = new SubmissionCategoryService(_submissionCategoryTestRepository);
            #endregion

            _mockMasterSubCategoryTestService = new MasterSubCategoryService(_masterSubCategoryTestRepository);


            var masterBusinessActivityData = new MasterBusinessActivityData();
            _testDbContext.MasterBusinessActivity.AddRange(masterBusinessActivityData.MasterAcivityEntitiesList);
            _testDbContext.SaveChanges();

           // //Setup Data
            var masterPrimaryCategoryRepositoryDataData = new MasterPrimaryCategoryRepositoryData();
            _testDbContext.MasterPrimaryCategory.AddRange(masterPrimaryCategoryRepositoryDataData.MasterPrimaryCategoryEntitiesList);
            _testDbContext.SaveChanges();

           // // 
            var secondaryLicenseData = new MasterSecondaryLicenseCategoryRepositoryData();
            _testDbContext.MasterSecondaryLicenseCategory.AddRange(secondaryLicenseData.MasterSeconderyLicenseCategoryList);
            _testDbContext.SaveChanges();

            var masterCategoryDocumentRepositoryData = new MasterCategoryDocumentRepositoryData();
            _testDbContext.MasterCategoryDocument.AddRange(masterCategoryDocumentRepositoryData.MasterCategoryDocumentList);
            _testDbContext.SaveChanges();



            var masterCategoryQuestionData = new MasterCategoryQuestionData();
            _testDbContext.MasterCategoryQuestion.AddRange(masterCategoryQuestionData.CategoryQuestionEntitiesList);
            _testDbContext.SaveChanges();




            var submissionToAccelaData = new SubmissionToAccelaRepositoryData();
            _testDbContext.SubmissiontoAccela.AddRange(submissionToAccelaData.SubmissiontoAccelaEntitiesList);
            _testDbContext.SaveChanges();

            var associatetoUserData = new SubmissionBblAssociationToUsersRepositoryData();
            _testDbContext.SubmissionBblAssociationToUsers.AddRange(associatetoUserData.SubmissionBblAssociationToUsersEntitiesList);
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



            var masterCategoryPhysicalLocationData = new MasterCategoryPhysicalLocationData();
            _testDbContext.MasterCategoryPhysicalLocation.AddRange(masterCategoryPhysicalLocationData.MasterCategoryPhysicalLocationList);
            _testDbContext.SaveChanges();

            var submissionMasterRepositoryData = new SubmissionMasterRepositoryData();
            _testDbContext.SubmissionMaster.AddRange(submissionMasterRepositoryData.SubmissionMasterEntitiesList);
            _testDbContext.SaveChanges();

            var submissionCategoryData = new SubmissionCategoryRepositoryData();
            _testDbContext.SubmissionCategory.AddRange(submissionCategoryData.SubmissionCategoryEntitiesList);
            _testDbContext.SaveChanges();

        

            var userBBlData = new UserBBLServiceRepositoryData();
            _testDbContext.UserBBLService.AddRange(userBBlData.UserBblServiceList);
            _testDbContext.SaveChanges();

            var submissionQuestionData = new SubmissionQuestionRepositoryData();
            _testDbContext.SubmissionQuestion.AddRange(submissionQuestionData.SubmissionQuestionEntitiesList);
            _testDbContext.SaveChanges();



            var testOsubcategoryFeeData = new OSubCategoryFeesData();
            _testDbContext.OSub_Category_Fees.AddRange(testOsubcategoryFeeData.OSubCategoryFeesEntitiesList);
            _testDbContext.SaveChanges();


        

           ////------------------






            var fixFeeRepositoryData = new FixFeeRepositoryData();
            _testDbContext.FixFee.AddRange(fixFeeRepositoryData.FixFeeEntitiesList);
            _testDbContext.SaveChanges();


            var masterSubCategoryRepositoryData = new MasterSubCategoryRepositoryData();
            _testDbContext.MasterSubCategory.AddRange(masterSubCategoryRepositoryData.MasterSubCategoryList);
            _testDbContext.SaveChanges();


            var feeCodeMapRepositoryData = new FeeCodeMapRepositoryData();
            _testDbContext.FeeCodeMap.AddRange(feeCodeMapRepositoryData.FeeCodeMapEntitiesList);
            _testDbContext.SaveChanges();

            _controller = new BblCategoryController(_mockBusinessActivityTestService,
                      _bblPrimaryCategories,
                       _submissionMasterTestService, _masterCategoryPhysicalLocationTestService, _submissionCategoryTestService, _mockMasterSubCategoryTestService);
       



        }

        [TestMethod]
        public async Task GetBusinessActivities_Return_Test()
        {

          
            var contacts = await _controller.GetBusinessActivities() as JsonResult<IEnumerable<MasterBusinessActivity>>;
            if (contacts != null) Assert.AreEqual(contacts.Content.ToList().Count(), 5);
        }

        [TestMethod]
        public async Task FindPrimaryCategoryById_Return_Test()
        {
            var submissionApplication = new SubmissionApplication { ActivityID = "FF0C5A9C-86B4-4378-BA8F-C1CE165B814E" };

            var contacts = await _controller.FindPrimaryCategoryById(submissionApplication) as JsonResult<IEnumerable<MasterPrimaryCategory>>;
            if (contacts != null) Assert.AreEqual(contacts.Content.ToList().Count(), 4);
        }
   
        [TestMethod]
        public async Task FindSecondaryCategoryById_Return_Test()
        {
            //Initial Model Details
            var submissionApplication = new SubmissionApplication { PrimaryID = "640C7E4B-8F90-4E59-9BA5-D1EA71E7B6FB" };
            //Act 
            var contacts = await _controller.FindSecondaryCategoryById(submissionApplication) as JsonResult<IEnumerable<MasterSecondaryLicenseCategory>>;
            //Assert
            if (contacts != null) Assert.AreEqual(contacts.Content.ToList().Count(), 1);
        }


        [TestMethod]
        public async Task GetSuperSubcategory_Return_Test()
        {
            //Initial Model Details
            var submissionApplication = new SubmissionApplication { ActivityID = "BE29F663-A3FA-4B64-8697-C9C4FF91B69F", PrimaryID = "BDABCA0B-8B80-4CC6-8836-6D5A751B6C54", Secondary = "1B69B449-ECAB-41AE-89A9-E2F76329A52B" };
            //Act 
            var contacts = await _controller.GetSuperSubcategory(submissionApplication) as JsonResult<IEnumerable<MasterSubCategory>>;

            //Assert
            if (contacts != null) Assert.AreEqual(contacts.Content.ToList().Count(), 4);
        }
        [TestMethod]
        public async Task GetSubmissionMaster_Return_Test()
        {
            //Initial Model Details
            //  var submissionApplication = new SubmissionApplication { ActivityID = "BE29F663-A3FA-4B64-8697-C9C4FF91B69F", PrimaryID = "BDABCA0B-8B80-4CC6-8836-6D5A751B6C54", Secondary = "1B69B449-ECAB-41AE-89A9-E2F76329A52B" };
            //Act 
            var contacts = await _controller.GetSubmissionMaster() as JsonResult<IEnumerable<SubmissionMaster>>;

            //Assert
            if (contacts != null) Assert.AreEqual(contacts.Content.ToList().Count(), 5);
        }
        


        [TestMethod]
        public async Task InsertAssociateBblService_Return_Test()
        {
            //Initial Model Details
              var submissionApplication = new SubmissionApplication
              {
                  
                  IsRaoFeeApplied=false,
                  MasterId=null,
                  SubmissionLicense=null,
                  UserID="2bfe7e43-d343-4fd5-9997-5bda0b7ce25e",
                  RAOFee=0,IseHOP=true,eHOP=0,TotalAmount=0,Status=null,
                  ExpirationDate=Convert.ToDateTime("1900-01-01"),ApprovedBy=null,Description=null,App_Type="B",DocSubmType=null,
                  FEIN=null,IsFEIN=true,
                  ActivityID = "BE29F663-A3FA-4B64-8697-C9C4FF91B69F", PrimaryID = "BDABCA0B-8B80-4CC6-8836-6D5A751B6C54", Secondary = "1B69B449-ECAB-41AE-89A9-E2F76329A52B",
                  SubSubCategory = "14DDF60B-85C1-4AE3-AC6D-386F715E3361",
                  ItemQty = "",
                  Question = null,
                  Answer = null,
                  Applicationfee=0,
                  Licensefee=0,
                  Endorsmentfee=0,
                  Techfee=0,
                  SubQuestion = new List<ScreeningQuestion> { new ScreeningQuestion { Question = "Is this business already registered with DCRA’s Corporations Division", Answer = "YES", Type="RadioButton",
                      StartRange=0,EndRange = 0,QuestionFor=null,CategoryId=null,keyIdentifying=null,
                      Option = new List<BusinessStructure>() { new BusinessStructure { BusinessStructureOption="YES"} , new BusinessStructure {BusinessStructureOption="NO"} } },
                  
                   new ScreeningQuestion { Question = "What is your Business Structure ?", Answer = "Corporation (For Profit)", Type="Dropdown",
                      StartRange=0,EndRange = 0,QuestionFor=null,CategoryId=null,keyIdentifying=null,
                      Option = new List<BusinessStructure>() { new BusinessStructure { BusinessStructureOption="Select One"} ,
                          new BusinessStructure {BusinessStructureOption="Corporation (For Profit)"}
                      , new BusinessStructure {BusinessStructureOption="Corporation (Non-Profit)"}
                      , new BusinessStructure {BusinessStructureOption="Limited Liability Company (LLC)"}
                      , new BusinessStructure {BusinessStructureOption="Limited Liability Partnership (LLP)"}
                      , new BusinessStructure {BusinessStructureOption="General Cooperative Association"}
                      , new BusinessStructure {BusinessStructureOption="Limited Cooperative Association"}
                      , new BusinessStructure {BusinessStructureOption="Statutory Trust"}
                      , new BusinessStructure {BusinessStructureOption="Sole Proprietorship"}
                      , new BusinessStructure {BusinessStructureOption="General Partnership"}
                      , new BusinessStructure {BusinessStructureOption="Joint Venture"}} },

                       new ScreeningQuestion { Question = "What is the Trade Name?", Answer = "", Type="Textbox",
                      StartRange=0,EndRange = 0,QuestionFor=null,CategoryId=null,keyIdentifying=null,
                      Option = new List<BusinessStructure>() { new BusinessStructure { BusinessStructureOption="YES"} , new BusinessStructure {BusinessStructureOption="NO"} } },

                        new ScreeningQuestion { Question = "Would you like a two (2) or four (4) year license?", Answer = "Two (2) year", Type="RadioButton",
                      StartRange=0,EndRange = 0,QuestionFor=null,CategoryId=null,keyIdentifying=null,
                      Option = new List<BusinessStructure>() { new BusinessStructure { BusinessStructureOption="Two (2) year"} , new BusinessStructure {BusinessStructureOption="Four (4) year"} } },
                  
                        new ScreeningQuestion { Question = "Will this business be located in the District of Columbia?", Answer = "YES", Type="RadioButton",
                      StartRange=0,EndRange = 0,QuestionFor=null,CategoryId=null,keyIdentifying=null,
                      Option = new List<BusinessStructure>() { new BusinessStructure { BusinessStructureOption="YES"} , new BusinessStructure {BusinessStructureOption="NO"} } },
                  
                        new ScreeningQuestion { Question = "Will this Business be Home based?", Answer = "YES", Type="RadioButton",
                      StartRange=0,EndRange = 0,QuestionFor=null,CategoryId=null,keyIdentifying=null,
                      Option = new List<BusinessStructure>() { new BusinessStructure { BusinessStructureOption="YES"} , new BusinessStructure {BusinessStructureOption="NO"} } },
                   
                      new ScreeningQuestion { Question = "Do you have a Home Occupancy Permit (HOP) from Office of Zoning?", Answer = "YES", Type="RadioButton",
                      StartRange=0,EndRange = 0,QuestionFor=null,CategoryId=null,keyIdentifying=null,
                      Option = new List<BusinessStructure>() { new BusinessStructure { BusinessStructureOption="YES"} , new BusinessStructure {BusinessStructureOption="NO"} } },
                  
                        new ScreeningQuestion { Question = "Which Tax Identification Number is associated with your business: Federal Employer Identification Number (FEIN) or  Social Security Number (SSN)?", Answer = "FEIN", Type="RadioButton",
                      StartRange=0,EndRange = 0,QuestionFor=null,CategoryId=null,keyIdentifying=null,
                      Option = new List<BusinessStructure>() { new BusinessStructure { BusinessStructureOption="FEIN"} , new BusinessStructure {BusinessStructureOption="SSN"} } },
                  
                    }, 
                  LicenseCategory="Charitable Exempt",
                  Endorsement="General Business",
                  DetailedCategoryList = new List<DetailedCategoryList>
                  {
                      new DetailedCategoryList{Endorsement="General Business",
                          CategoryId="BDABCA0B-8B80-4CC6-8836-6D5A751B6C54",LicenseCategory="Charitable Exempt",Units="NA",
                          ApplicationFee=0,CategoryLicenseFee=0,EndorsementFee=0,SubTotal=0,TechFee=0,TotalFee=0,CategoryCode="4001",
                          IsRaoFeeApplied=false,LicenseDuration="TWO (2) YEAR",
                          IsSubCategory=false,SubCategoryName=null,ExpiryFee=0,LapsedFee=0},

                           new DetailedCategoryList{Endorsement="General Business",
                          CategoryId="1B69B449-ECAB-41AE-89A9-E2F76329A52B",LicenseCategory="General Business Licenses",Units="NA",
                          ApplicationFee=0,CategoryLicenseFee=0,EndorsementFee=0,SubTotal=0,TechFee=0,TotalFee=0,CategoryCode="4003",
                          IsRaoFeeApplied=false,LicenseDuration="TWO (2) YEAR",
                          IsSubCategory=false,SubCategoryName="Collection Agencies",ExpiryFee=0,LapsedFee=0}
                  },
                  Validation = null,
                  TotalFee=0,
                  IsBusinessMustbeinDC=false,
                  IsHomeBased=false,
                  IsCofo=false,
                  IsPhysicalLocationVerify=false,
                  GrandTotal=0,
                  IsCorporationDivision = false,
                  BusinessStructure=null,
                  TradeName=null
              };
            //Act 
            // var contacts = await controller.InsertAssociateBblService(submissionApplication) as JsonResult<List<string>>;

           // var contacts = await controller.InsertAssociateBblService(submissionApplication) as JsonResult;

          //  dynamic data=contacts.datal
            //Assert
            //  if (contacts != null) Assert.AreEqual(contacts.Content, "BDABCA0B-8B80-4CC6-8836-6D5A751B6C54345");

            //var actionResult = await controller.InsertAssociateBblService(submissionApplication);
            //var p = actionResult.GetType().GetProperty("Result").GetValue(actionResult, null);

              dynamic actionResult = await _controller.InsertAssociateBblService(submissionApplication);
              var Content = actionResult.GetType().GetProperty("Content").GetValue(actionResult, null);
              var Result = Content.GetType().GetProperty("Result").GetValue(Content, null);

              Assert.IsNotNull(Result);

            //var contentResult = actionResult as OkNegotiatedContentResult<object>;

            // Assert
            //Assert.IsNull(contentResult);

        }

        

         [TestMethod]
         public async Task GetTotalFees_Return_Test()
         {
             //Initial Model Details
             var detailedCategoryList = new List<DetailedCategoryList>()
            {
                new DetailedCategoryList()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14ED4",
                    EndorsementFee = Convert.ToDecimal(25.0000),
                    Endorsement = "General Sales"
                },
                 new DetailedCategoryList()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14EER",
                     EndorsementFee = Convert.ToDecimal(55.0000),
                    Endorsement = "Inspected Sales and Services"
                }
            };

             var screenQuestionList = new List<ScreeningQuestion>()
            {
                new ScreeningQuestion()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14ED4",
                    Question = "Would you like a two (2) or four (4) year license?",
                    Answer = "TWO (2) YEAR"
                    
                },
                  new ScreeningQuestion()
                {
                     CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14EER",
                     Question = "Will this business be located in the District of Columbia?",
                     Answer = "YES"
                }
            };

             var submissionApplication = new SubmissionApplication
             {
                 // Primary Category
                 PrimaryID = "6C548EBE-59ED-40B8-BEBE-EDF79149295D",
                 MasterId = "9F92454C-ED4C-4062-B557-4125813B2BDD",
                 Secondary = "1DE53FC4-F444-44EA-99A4-5DA3F107D5DD",
                 DetailedCategoryList = detailedCategoryList,
                 SubQuestion = screenQuestionList
             };
             //Act 
             var contacts = await _controller.GetTotalFees(submissionApplication) as JsonResult<SubmissionApplication>;

             //Assert
             if (contacts != null) Assert.AreEqual(contacts.Content.TotalFee,132);
         }

         [TestMethod]
         public async Task GetScreeningQuestions_Return_Test()
         {
             //Initial Model Details
             var submissionApplicationEntity = new SubmissionApplication { ActivityID = "1ED33685-B54E-4D7A-BBCE-1C93BD762CB1", PrimaryID = "26E9FC09-2763-4751-B880-EC4E805DF281" };
             //Act 
             var contacts = await _controller.GetScreeningQuestions(submissionApplicationEntity) as JsonResult<IEnumerable<SubmissionApplication>>;

             //Assert
             if (contacts != null) Assert.AreEqual(contacts.Content.ToList().Count(), 1);
         }
         [TestMethod]
         public async Task FindBySubmissionCategoryId_Return_Test()
         {
             //Initialize Entites
             var submissionCategoryModel = new SubmissionCategoryModel { SubmissionCategoryId = 1 };

             //Act 
             var submissionCategoryRows = await _controller.FindSubmissionCategoryById(submissionCategoryModel) as JsonResult<IEnumerable<SubmissionCategory>>;
             //Assert
             Assert.IsNotNull(submissionCategoryRows); // Test if null
             Assert.IsTrue(submissionCategoryRows.Content.Count()== 1);
         }

    }
}