using System;
using System.Collections.Generic;
using BusinessCenter.Data.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;
using BusinessCenter.Api.Controllers;
using BusinessCenter.Api.Models;
using BusinessCenter.Api.Utility;
using BusinessCenter.Common;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Model;
using BusinessCenter.Service.Implementation;
using BusinessCenter.Tests.Repository;
using BusinessCenter.Tests.Setup;
using BusinessCenter.Data;


namespace BusinessCenter.Tests.Controllers
{
     [TestClass]
    public class RenewControllerTest
    {

        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private RenewController _controller = null;


        #region Declaration RenewalService Reposiotry 
        private RenewRepository _renewTestRepository;
        private BblRepository _bblTestRepository;
        private CorpRespository _corpTestRepository;
        private SubmissionMasterRepository _subMasterTestRepository;
        private MasterBusinessActivityRepository _masterBusinessActivityTestRepository;
        private MasterPrimaryCategoryRepository _masterPrimaryCategoryTestRepository;
        private SubmissionCategoryRepository _submissionCategoryTestRepository;
        private SubmissionDocumentRepository _submissionDocumentTestRepository;
        private SubmissionMasterRenewalRepository _submissionMasterRenewalTestRepository;
        private MasterSecondaryLicenseCategoryRepository _masterSecondaryLicenseCategoryTestRepository;
        private DCBC_ENTITY_BBL_RenewalsRepository _dcbcEntityBblRenewalsTestRepository;
        private UserBBLServiceRepository _userBblServiceTestRepository;
   
        private MasterCategoryPhysicalLocationRepository _masterCategoryPhysicalLocationTestRepository;
        private SubmissionMasterApplicationChcekListRepository _subChcekListAppTestRepository;
        private UserRepository _userTestRepository;
        private SubmissionBblAssociationToUsersRepository _submissionBblAssociationToUsersTestRepository;
        private SubmissionToAccelaRepository _submissionToAccelaTestRepository;
        private OSubCategoryFeesRepository _oSubCategoryFeesTestRepository;
        private FixFeeRepository _fixfeeTestRepository;
        private MasterSubCategoryRepository _masterSubCategoryTestRepository;
        private FeeCodeMapRepository _feeCodeMapTestRepository;
        private Mock<IDCBC_ENTITY_BBL_Renewal_InvoiceRepository> _dCbcEntityBblRenewalInvoiceTestRepository;
        private MasterCategoryDocumentRepository _masterCategoryDocumentTestRepository;
        private MasterCategoryQuestionRepository _masterCategoryQuestionTestRepository;
        private SubmissionIndividualRepository _submissionIndividualTestRepository;
        private SubmissionDocumentToAccelaRepository _submissionDocumentToAccelaTestRepository;
        private DCBC_ENTITY_BBL_Renewal_InvoiceRepository _dcbcEntityBblRenewalInvoiceRepository;
        private Lookup_ExistingCategoriesRepository _lookupExistingCategoriesRepository;
        private SubmissionLicenseNumberCounterRepository _submissionlicencecounter;
        private MasterRenewalStatusFeeRepository _masterrenewalstatusfee;
        private MasterBblApplicationStatusRepository _masterBblApplicationStatusTestRepository;
#endregion
        private SubmissionTaxRevenueRepository _submissionTaxRevenueTestRepository;
        private SubmissionMasterApplicationChcekListRepository _submissionMasterApplicationChcekListRepository;
        private SubmissionCorporationRepository _submissionCorporationRepository;
        private SubmissionCorporationAgentAddressRepository _submissionCorporationAgentAddressRepository;
        private MasterRegisterAgentRepository _masterRegisterAgentRepository;
        private SubmissionQuestionRepository _submissionQuestionRepository;
        private MasterCountryRepository _masterCountryRepository;
        private MasterStateRepository _masterStateRepository;
        private StreetTypesRepository _streetTypesRepository;
        private SubmissionCofoHopeHopRepository _submissionCofoHopeHopRepository;
        private SubmissionCofoHopeHopAddressRepository _submissionCofoHopeHopAddressRepository;

        private SubmissionEHOPEligibilityRepository _submissionEhopEligibilityRepository;
        private MastereHopEligibilityRepository _mastereHopEligibilityRepository;
        private MasterEhopOptionTypeRepository _masterEhopOptionType;
        private SubmissionVerficationRepository _submissionVerficationRepository;
        private PaymentDetailsRepository _paymentDetailsRepository;
        private PaymentCardDetailsRepository _paymentCardDetailsRepository;
        private PaymentAddressDetailsRepository _paymentAddressDetailsRepository;
        private PaymentHistoryDetailsRepository _paymentHistoryDetails;
        private DCBC_ENTITY_Cof_ORepository _dCbcEntityCofoRepository;
        private EtlAddressAndParcelRepository _etlAddressTestRepository;
        private UserRoleRepository _userRoleRepository;
//        #region Declaration of Sumission Master Service Repository
      
      
//        private UserBBLServiceRepository _userBblServiceRepository;
//        private BblRepository _bblRepository;
//        private MasterCategoryPhysicalLocationRepository _masterCategoryPhysicalLocationRepository;
//        private MasterPrimaryCategoryRepository _masterPrimaryCategoryRepository;
//        private UserRepository _userRepository;
//        private MasterBusinessActivityRepository _masterBusinessActivityRepository;
//        private DCBC_ENTITY_BBL_RenewalsRepository _dcbcEntityBblRenewalsRepository;
//        private SubmissionBblAssociationToUsersRepository _submissionBblAssociationToUsersRepository;
//        private SubmissionToAccelaRepository _submissionToAccelaRepository;
//        private MasterSecondaryLicenseCategoryRepository _masterSecondaryLicenseCategoryRepository;
//        private SubmissionQuestionRepository _submissionQuestionRepository;
//        private SubmissionMasterApplicationChcekListRepository _submissionMasterApplicationChcekListRepository;
//        private OSubCategoryFeesRepository _oSubCategoryFeesRepository;
//        private FixFeeRepository _fixedFeeTestRepository;
//        private MasterSubCategoryRepository _masterSubCategoryRepository;
//        private FeeCodeMapRepository _feeCodeMapRepository;
//        private SubmissionMasterRenewalRepository _submissionMasterRenewalRepository;
//        private MasterCategoryQuestionRepository _masterCategoryQuestionRepository;
//        private MasterCategoryDocumentRepository _masterCategoryDocumentRepository;
//        private UserRoleRepository _userRoleRepository;
//        private DCBC_ENTITY_BBL_Renewal_InvoiceRepository dcbcEntitybblRenewalInvoiceRepository;
      
//        private SubmissionMasterService _submissionMasterTestService;
//        private Lookup_ExistingCategoriesRepository lookup_ExistingCategoriesRepository;
      
//        //Repository declaration
//        private SubmissionLicenseNumberCounterRepository _submissionLicenseNumberCounterTestRepository;
     
//#endregion

        #region Declaration of Service
        private RenewalService _renewalServiceTest;
        private SubmissionMasterService _submissionMasterTestService;
        private SubmissionTaxRevenueService _submissionTaxRevenueTestService;
        private BBLAssociateService _bblAssociateService;
        #endregion


        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _submissionMasterApplicationChcekListRepository = new SubmissionMasterApplicationChcekListRepository(_testUnitOfWork);
            _userBblServiceTestRepository = new UserBBLServiceRepository(_testUnitOfWork);

            #region Initialization of Renewal Service
            _etlAddressTestRepository = new EtlAddressAndParcelRepository(_testUnitOfWork);
            _masterBblApplicationStatusTestRepository = new MasterBblApplicationStatusRepository(_testUnitOfWork);
            _submissionDocumentToAccelaTestRepository = new SubmissionDocumentToAccelaRepository(_testUnitOfWork);
            _masterCategoryQuestionTestRepository = new MasterCategoryQuestionRepository(_testUnitOfWork);
            _submissionToAccelaTestRepository = new SubmissionToAccelaRepository(_testUnitOfWork);
            _masterrenewalstatusfee = new MasterRenewalStatusFeeRepository(_testUnitOfWork);
            _submissionlicencecounter = new SubmissionLicenseNumberCounterRepository(_testUnitOfWork);
            _bblTestRepository = new BblRepository(_testUnitOfWork);
            _corpTestRepository = new CorpRespository(_testUnitOfWork);
            _userBblServiceTestRepository = new UserBBLServiceRepository(_testUnitOfWork);
            _lookupExistingCategoriesRepository = new Lookup_ExistingCategoriesRepository(_testUnitOfWork);
            _paymentCardDetailsRepository = new PaymentCardDetailsRepository(_testUnitOfWork);   
            _dcbcEntityBblRenewalInvoiceRepository = new DCBC_ENTITY_BBL_Renewal_InvoiceRepository(_testUnitOfWork, _lookupExistingCategoriesRepository);
            _submissionMasterRenewalTestRepository = new SubmissionMasterRenewalRepository(_testUnitOfWork, _masterrenewalstatusfee);
            _dCbcEntityBblRenewalInvoiceTestRepository = new Mock<IDCBC_ENTITY_BBL_Renewal_InvoiceRepository>();
            _dcbcEntityBblRenewalsTestRepository = new DCBC_ENTITY_BBL_RenewalsRepository(_testUnitOfWork);
            _masterCategoryDocumentTestRepository = new MasterCategoryDocumentRepository(_testUnitOfWork);
            _masterSecondaryLicenseCategoryTestRepository = new MasterSecondaryLicenseCategoryRepository(_testUnitOfWork);
            _mastereHopEligibilityRepository = new MastereHopEligibilityRepository(_testUnitOfWork);
            _masterEhopOptionType = new MasterEhopOptionTypeRepository(_testUnitOfWork);
            _masterBusinessActivityTestRepository = new MasterBusinessActivityRepository(_testUnitOfWork, _masterPrimaryCategoryTestRepository);
            _fixfeeTestRepository = new FixFeeRepository(_testUnitOfWork);
            _feeCodeMapTestRepository = new FeeCodeMapRepository(_testUnitOfWork, _oSubCategoryFeesTestRepository);
            _masterPrimaryCategoryTestRepository = new MasterPrimaryCategoryRepository(_testUnitOfWork, _masterSecondaryLicenseCategoryTestRepository, _masterCategoryDocumentTestRepository, _masterCategoryQuestionTestRepository);
            _masterSubCategoryTestRepository = new MasterSubCategoryRepository(_testUnitOfWork, _masterPrimaryCategoryTestRepository, _masterSecondaryLicenseCategoryTestRepository);
            _subChcekListAppTestRepository = new SubmissionMasterApplicationChcekListRepository(_testUnitOfWork);
            _submissionCategoryTestRepository = new SubmissionCategoryRepository(_testUnitOfWork, _masterPrimaryCategoryTestRepository,
           _masterSecondaryLicenseCategoryTestRepository,
           _oSubCategoryFeesTestRepository, _fixfeeTestRepository,
           _submissionQuestionRepository, _masterSubCategoryTestRepository, _masterCategoryPhysicalLocationTestRepository,
           _feeCodeMapTestRepository, _subChcekListAppTestRepository, _submissionMasterRenewalTestRepository, _dCbcEntityBblRenewalInvoiceTestRepository.Object,
           _lookupExistingCategoriesRepository);
            _subMasterTestRepository = new SubmissionMasterRepository(_testUnitOfWork, _submissionCategoryTestRepository, _bblTestRepository,
           _userBblServiceTestRepository, _submissionQuestionRepository, _masterCategoryPhysicalLocationTestRepository, _masterPrimaryCategoryTestRepository,
           _subChcekListAppTestRepository, _userTestRepository, _dcbcEntityBblRenewalsTestRepository, _masterBusinessActivityTestRepository,
           _submissionBblAssociationToUsersTestRepository, _submissionToAccelaTestRepository, _masterSecondaryLicenseCategoryTestRepository,
           _lookupExistingCategoriesRepository, _submissionlicencecounter, _masterBblApplicationStatusTestRepository);
            _submissionDocumentTestRepository = new SubmissionDocumentRepository(_testUnitOfWork,
            _submissionCategoryTestRepository,
            _masterCategoryDocumentTestRepository,
            _masterPrimaryCategoryTestRepository,
            _masterSecondaryLicenseCategoryTestRepository, _subMasterTestRepository,
            _masterCategoryPhysicalLocationTestRepository, _subChcekListAppTestRepository,
            _submissionIndividualTestRepository, _submissionDocumentToAccelaTestRepository);

            //_renewTestRepository = new RenewRepository(_testUnitOfWork, _bblTestRepository, _corpTestRepository,
            //   _subMasterTestRepository,
            //  _masterBusinessActivityTestRepository,
            // _masterPrimaryCategoryTestRepository,
            // _submissionCategoryTestRepository,
            // _submissionDocumentTestRepository,
            // _submissionMasterRenewalTestRepository,
            // _masterSecondaryLicenseCategoryTestRepository,
            // _dcbcEntityBblRenewalsTestRepository,
            // _Lookup_ExistingCategoriesRepository, _dcbcEntityBblRenewalInvoiceRepository
            // );

#endregion
            _oSubCategoryFeesTestRepository = new OSubCategoryFeesRepository(_testUnitOfWork, _masterPrimaryCategoryTestRepository, _masterSecondaryLicenseCategoryTestRepository);
            _masterCategoryPhysicalLocationTestRepository = new MasterCategoryPhysicalLocationRepository(_testUnitOfWork,
                _masterPrimaryCategoryTestRepository, _masterCategoryQuestionTestRepository, _oSubCategoryFeesTestRepository, _masterSecondaryLicenseCategoryTestRepository);
            _submissionTaxRevenueTestRepository = new SubmissionTaxRevenueRepository(_testUnitOfWork, _submissionMasterApplicationChcekListRepository);

                _submissionCorporationAgentAddressRepository = new SubmissionCorporationAgentAddressRepository(_testUnitOfWork, _submissionMasterApplicationChcekListRepository,
              _subMasterTestRepository);

                _masterRegisterAgentRepository=new MasterRegisterAgentRepository(_testUnitOfWork);
                _submissionQuestionRepository=new SubmissionQuestionRepository(_testUnitOfWork);
            _masterCountryRepository=new MasterCountryRepository(_testUnitOfWork);
            _paymentHistoryDetails = new PaymentHistoryDetailsRepository(_testUnitOfWork);
            _masterStateRepository=new MasterStateRepository(_testUnitOfWork);
            _streetTypesRepository = new StreetTypesRepository(_testUnitOfWork);
            _paymentAddressDetailsRepository = new PaymentAddressDetailsRepository(_testUnitOfWork);
                _submissionCorporationRepository = new SubmissionCorporationRepository(_testUnitOfWork, _submissionCorporationAgentAddressRepository,
                _submissionMasterApplicationChcekListRepository,
                _masterRegisterAgentRepository, _submissionQuestionRepository,
                _corpTestRepository, _subMasterTestRepository, _masterCountryRepository, _masterStateRepository);
                _userRoleRepository = new UserRoleRepository(_testUnitOfWork);

                _userTestRepository = new UserRepository(_testUnitOfWork, _userRoleRepository);
                _submissionIndividualTestRepository = new SubmissionIndividualRepository(_testUnitOfWork, _subMasterTestRepository);

            _submissionMasterTestService = new SubmissionMasterService(_subMasterTestRepository);
            _submissionTaxRevenueTestService = new SubmissionTaxRevenueService(_submissionTaxRevenueTestRepository);
            _submissionCofoHopeHopAddressRepository = new SubmissionCofoHopeHopAddressRepository(_testUnitOfWork, _corpTestRepository, _subMasterTestRepository,
           _streetTypesRepository,
           _masterRegisterAgentRepository, _submissionCorporationRepository,
           _submissionQuestionRepository, _submissionMasterApplicationChcekListRepository, _masterStateRepository, _masterCountryRepository);


            _submissionCofoHopeHopRepository = new SubmissionCofoHopeHopRepository(_testUnitOfWork, _submissionCofoHopeHopAddressRepository,
              _submissionMasterApplicationChcekListRepository,
              _subMasterTestRepository, _streetTypesRepository, _fixfeeTestRepository, _submissionDocumentTestRepository);

            _submissionEhopEligibilityRepository = new SubmissionEHOPEligibilityRepository(_testUnitOfWork, _mastereHopEligibilityRepository,
                         _submissionMasterApplicationChcekListRepository, _userTestRepository,
                         _submissionCofoHopeHopRepository, _submissionDocumentTestRepository, _masterEhopOptionType);

            _paymentDetailsRepository = new PaymentDetailsRepository(_testUnitOfWork, _paymentCardDetailsRepository, _paymentAddressDetailsRepository,
       _subMasterTestRepository, _submissionCategoryTestRepository, _submissionDocumentTestRepository, _submissionMasterApplicationChcekListRepository,
       _fixfeeTestRepository, _submissionMasterRenewalTestRepository, _dcbcEntityBblRenewalInvoiceRepository, _bblTestRepository, _userBblServiceTestRepository,
       _lookupExistingCategoriesRepository, _paymentHistoryDetails);



            _submissionVerficationRepository = new SubmissionVerficationRepository(_testUnitOfWork, _submissionCofoHopeHopRepository, _submissionCorporationRepository,
            _submissionTaxRevenueTestRepository, _submissionCorporationAgentAddressRepository, _submissionCofoHopeHopAddressRepository,
            _submissionCategoryTestRepository, _submissionDocumentTestRepository,
            _submissionMasterApplicationChcekListRepository, _fixfeeTestRepository, _masterCountryRepository, _userBblServiceTestRepository,
            _bblTestRepository, _dcbcEntityBblRenewalsTestRepository, _dcbcEntityBblRenewalInvoiceRepository, _masterStateRepository, _streetTypesRepository,
            _paymentDetailsRepository, _submissionMasterRenewalTestRepository);

            _dCbcEntityCofoRepository = new DCBC_ENTITY_Cof_ORepository(_testUnitOfWork, _submissionCofoHopeHopRepository, _streetTypesRepository,
               _subMasterTestRepository, _submissionMasterApplicationChcekListRepository, _submissionCofoHopeHopAddressRepository, _etlAddressTestRepository);

            _submissionBblAssociationToUsersTestRepository = new SubmissionBblAssociationToUsersRepository(_testUnitOfWork);

               _bblAssociateService = new BBLAssociateService(_bblTestRepository, _userBblServiceTestRepository,
           _submissionDocumentTestRepository, _submissionCorporationRepository,
          _submissionCofoHopeHopRepository, _submissionMasterApplicationChcekListRepository,
          _submissionCorporationAgentAddressRepository, _submissionEhopEligibilityRepository, _submissionVerficationRepository,
          _paymentDetailsRepository, _dcbcEntityBblRenewalsTestRepository,
          _oSubCategoryFeesTestRepository, _dCbcEntityCofoRepository, _masterCategoryQuestionTestRepository,
          _userTestRepository, _submissionBblAssociationToUsersTestRepository, _streetTypesRepository);

               _renewTestRepository = new RenewRepository(_testUnitOfWork, _bblTestRepository, _corpTestRepository,
                _subMasterTestRepository,
               _masterBusinessActivityTestRepository,
              _masterPrimaryCategoryTestRepository,
              _submissionCategoryTestRepository,
              _submissionDocumentTestRepository,
              _submissionMasterRenewalTestRepository,
              _masterSecondaryLicenseCategoryTestRepository,
              _dcbcEntityBblRenewalsTestRepository,
              _lookupExistingCategoriesRepository, _dcbcEntityBblRenewalInvoiceRepository
              );

               _renewalServiceTest = new RenewalService(_renewTestRepository, _bblTestRepository, _submissionMasterRenewalTestRepository);


               _controller = new RenewController(_renewalServiceTest, _submissionMasterTestService, _submissionTaxRevenueTestService, _bblAssociateService);


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

            var submissionIndividualData = new SubmissionIndividualData();
            _testDbContext.SubmissionIndividual.AddRange(submissionIndividualData.SubmissionIndividualDataEntitiesList);
            _testDbContext.SaveChanges();

            var corpRepositoryData = new CorpRepositoryData();
            _testDbContext.DCBC_ENTITY_CORP.AddRange(corpRepositoryData.CorpEntitiesList);
            _testDbContext.SaveChanges();


            var masterSubCategoryRepositoryData = new MasterSubCategoryRepositoryData();
            _testDbContext.MasterSubCategory.AddRange(masterSubCategoryRepositoryData.MasterSubCategoryList);
            _testDbContext.SaveChanges();


            var feeCodeMapRepositoryData = new FeeCodeMapRepositoryData();
            _testDbContext.FeeCodeMap.AddRange(feeCodeMapRepositoryData.FeeCodeMapEntitiesList);
            _testDbContext.SaveChanges();

            //Setup FackData Initialization
            var bblData = new BblRepositoryData();
            _testDbContext.DCBC_ENTITY_BBL.AddRange(bblData.BblEntitiesList);
            _testDbContext.SaveChanges();

            var submissionMasterRenewalData = new SubmissionMasterRenewalData();
            _testDbContext.SubmissionMasterRenewal.AddRange(submissionMasterRenewalData.MasterRenewalEntitiesList);
            _testDbContext.SaveChanges();


        }
         [TestMethod]
        public async Task GetRenewalData_Return_Test()
        { 
              //Initial Model Details
            var renewModel = new RenewModel
            {
              UserBblAssociateId="6",
              UserId = "2AC53E53-87D0-468F-9628-4AEBA1120613"
            };

            //Act 
            dynamic renewalRows = await _controller.GetRenewalData(renewModel);

            var content = renewalRows.GetType().GetProperty("Content").GetValue(renewalRows, null);
            var result = content.GetType().GetProperty("isValidated").GetValue(content, null);

            //Assert
            Assert.IsNotNull(renewalRows);
            Assert.IsTrue(result);
        }
         [TestMethod]
         public async Task RemoveRenewalData_Return_Test()
         {
             //Initial Model Details
             var renewModel = new RenewModel
             {
                 UserBblAssociateId = "6",
                 UserId = "2AC53E53-87D0-468F-9628-4AEBA1120613"
             };

             //Act 
             var renewalRows = await _controller.RemoveRenewalData(renewModel) as JsonResult<bool>;
             //Assert
             if (renewalRows != null) Assert.AreEqual(renewalRows.Content, true);

         }
         [TestMethod]
         public async Task RenewalStatus_Return_Test()
         {
             //Initial Model Details
             var renewModel = new RenewModel
             {
                 UserBblAssociateId = "6",
                 UserId = "2AC53E53-87D0-468F-9628-4AEBA1120613",
                 CategoryName = "APARTMENT|GENERAL BUSINESS LICENSES|",
                 LicenseNumber = "400312000720",
                 EntityId = "10047998",
                 IsCorp=true,
                 SubCategoryName = "Apartment|General Business Licenses|",
                 ActivityName = "Housing: Residential",
                 LicenseDuration=2

             };

             //Act 
             var renewalRows = await _controller.RenewalStatus(renewModel) as JsonResult<IEnumerable<RenewModel>>;
             //Assert
             if (renewalRows != null) Assert.AreEqual(renewalRows.Content.Count(),1);

         }
         [TestMethod]
         public async Task CheckDocument_Return_Test()
         {
             //Initial Model Details
             var servicedocument = new List<BblServiceDocuments>();
             var bblServiceDocuments = new BblServiceDocuments
             {
                 SubmissionCategoryID = 1,
                 CategoryID = "1",
                 DocRequired = "Department of Health (DOH)",
                 MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                 Description = "All businesses that manufacture, wholesale, warehouse, distribute, sell or use toxic or flammable materials or provide services to large audiences (theatres, restaurants, and public halls) are subject to inspection to ensure the safety of customers, employees, and property."
             };
             servicedocument.Add(bblServiceDocuments);
             var documentCheckEntity = new DocumentCheck();
             documentCheckEntity.DocumentList = servicedocument;
             documentCheckEntity.DocType = "IN";
             //Act
             var renewalRows = await _controller.CheckDocument(documentCheckEntity) as JsonResult<bool>;
             //Assert
             if (renewalRows != null) Assert.AreEqual(renewalRows.Content, true);

         }
         [TestMethod]
         public async Task UpdateRenwalDocument_Return_Test()
         {
             //Initialize Entites
             var servicedocument = new List<BblServiceDocuments>();
             var bblServiceDocuments = new BblServiceDocuments
             {
                 SubmissionCategoryID = 1,
                 CategoryID = "1",
                 DocRequired = "Department of Health (DOH)",
                 MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                 Description = "All businesses that manufacture, wholesale, warehouse, distribute, sell or use toxic or flammable materials or provide services to large audiences (theatres, restaurants, and public halls) are subject to inspection to ensure the safety of customers, employees, and property."
             };
             servicedocument.Add(bblServiceDocuments);
             var documentCheckEntity = new DocumentCheck();
             documentCheckEntity.DocumentList = servicedocument;
             documentCheckEntity.DocType = "IN";
             documentCheckEntity.MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40";


             //Act 
             var renewEntityRows = await _controller.UpdateRenwalDocumentType(documentCheckEntity) as JsonResult<bool>;
             //Assert
             if (renewEntityRows != null) Assert.AreEqual(renewEntityRows.Content, true);
         }
         [TestMethod]
         public async Task DeleteRenewalData_Return_Test()
         {
             //Initialize Entites
             var renewModel = new RenewModel { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97" };


             //Act 
             var renewEntityRows = await _controller.DeleteRenewalData(renewModel) as JsonResult<bool>;
             //Assert
             if (renewEntityRows != null) Assert.AreEqual(renewEntityRows.Content, true);
         }
         [TestMethod]
         public async Task GetTaxRevenue_Return_Test()
         {
             //Initialize Entites
             var renewModel = new RenewModel { 
                 UserBblAssociateId = "6",
                 UserId = "2AC53E53-87D0-468F-9628-4AEBA1120613"
             };


             //Act 
             var renewEntityRows = await _controller.GetTaxRevenue(renewModel) as JsonResult<IEnumerable<SubmissionTaxRevenue>>;
             //Assert
             if (renewEntityRows != null) Assert.AreEqual(renewEntityRows.Content.Count(),0);
         }
         [TestMethod]
         public async Task CheckAmount_Return_Test()
         {
             //Initialize Entites
             var renewModel = new RenewModel
             {
                 UserBblAssociateId = "6",
                 UserId = "2AC53E53-87D0-468F-9628-4AEBA1120613"
             };


             //Act 
             var renewEntityRows = await _controller.CheckAmount(renewModel) as JsonResult<RenewModel>;
             //Assert
             Assert.IsNotNull(renewEntityRows);
         }

         [TestMethod]
         public async Task CheckDocuments_Return_Test()
         {
             //Initialize Entites
             var renewModel = new RenewModel
             {
                 UserBblAssociateId = "6",
                 UserId = "2AC53E53-87D0-468F-9628-4AEBA1120613"
             };


             //Act 
             var renewEntityRows = await _controller.CheckDocuments(renewModel) as JsonResult<RenewModel>;
             //Assert
             Assert.IsNotNull(renewEntityRows);
         }
         [TestMethod]
         public async Task ValidateTaxRevenue_Return_Test()
         {
             //InitialModel Details
             var taxRevenue = new SubmissionTaxRevenuEntity
             {
                 UserBblAssociateId = "6",
                 UserId = "2AC53E53-87D0-468F-9628-4AEBA1120613",
                 TaxRevenueFfin = "45-250665",
                 TaxRevenueType = "FEIN/SSN",
                 BusinessOwnerRoles="Owner",
                 FullName="abc",
                 IsIAgree=true
             };

             //Act
             var renewEntityRows = await _controller.TaxValidation(taxRevenue) as JsonResult<RenewModel>;

             //Assert
             Assert.IsNotNull(renewEntityRows);
         }
         [TestMethod]
         public async Task GetFullAddress_Return_Test()
         { 
          //InitialModelDetails
             var taxRevenueData = new TaxRevenueData
             {
                 UserBblAssociateId = "2",
                 UserId = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F",
             };

             //Act
             var renewEntityRows = await _controller.BblAddress(taxRevenueData) as JsonResult<TaxRevenueData>;

             //Assert
             Assert.IsNotNull(renewEntityRows);
             Assert.AreEqual(renewEntityRows.Content.FullAddress, "1215 CONNECTICUT AVE NW");
         }
    }
}
