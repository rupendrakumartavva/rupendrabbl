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
    public class DCBC_ENTITY_Cof_ORepositoryTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        private DCBC_ENTITY_Cof_ORepository _mockCofoTestRepository;
        private SubmissionCofoHopeHopRepository _mocksubmissionCofoHopeHopTestReposiotry;
        private StreetTypesRepository _mockstreetTestRepository;
        private SubmissionMasterRepository _mockSubmissionMasterTestRepository;
        private SubmissionMasterApplicationChcekListRepository _mockApplicationChcekListTestRepository;

        private SubmissionCofoHopeHopAddressRepository _submissionCofoHopeHopAddressRepository;
        private SubmissionMasterRepository _submissionMasterRepository;
        private FixFeeRepository _fixFeeRepository;
        private SubmissionDocumentRepository _submissionDocumentRepository;

        private SubmissionCategoryRepository _submissionCategoryTestRepository;
        private BblRepository _bblTestRepository;
        private UserBBLServiceRepository _userBblServiceTestRepository;
        private SubmissionQuestionRepository _submissionQuestionTestRepository;
        private MasterCategoryPhysicalLocationRepository _masterCategoryPhysicalLocationTestRepository;
        private MasterPrimaryCategoryRepository _masterPrimaryCategoryTestRepository;
        private UserRepository _userTestRepository;
        private DCBC_ENTITY_BBL_RenewalsRepository _dcbcEntityBblRenewalsTestRepository;
        private MasterBusinessActivityRepository _masterBusinessActivityTestRepository;
        private SubmissionBblAssociationToUsersRepository _submissionBblAssociationToUsersTestRepository;
        private SubmissionToAccelaRepository _submissionToAccelaTestRepository;
        private MasterSecondaryLicenseCategoryRepository _masterSecondaryLicenseCategoryTestRepository;
        private EtlAddressAndParcelRepository _etlAddressTestRepository;

      
        private Lookup_ExistingCategoriesRepository _lookupExistingCategoriesRepository;
      //  private MasterLicenseFEINRenewal masterLicenseFeinRenewal;
        private SubmissionLicenseNumberCounterRepository _submissionlicencecounter;
        private MasterBblApplicationStatusRepository _masterBblApplicationStatusTestRepository;
        private CorpRespository _corpRespository;
        private MasterRegisterAgentRepository _masterRegisterAgentRepository;
        private SubmissionCorporationRepository _submissionCorporationRepository;
        private SubmissionQuestionRepository _submissionQuestionRepository;
        private MasterStateRepository _masterStateRepository;
        private MasterCountryRepository _masterCountryRepository;
        private MasterCategoryDocumentRepository _masterCategoryDocumentRepository;
        private SubmissionIndividualRepository _submissionIndividualRepository;
        private SubmissionDocumentToAccelaRepository _submissionDocumentToAccelaRepository;
        private OSubCategoryFeesRepository _osubcategoryFeesTestRepository;
        private MasterSubCategoryRepository _masterSubCategoryRepository;
        private FeeCodeMapRepository _feeCodeMapRepository;
        private SubmissionMasterRenewalRepository _submissionMasterRenewalRepository;
        private DCBC_ENTITY_BBL_Renewal_InvoiceRepository _dcbcEntityBblRenewalInvoiceRepository;
        private MasterCategoryQuestionRepository _masterCategoryQuestionRepository;
        private UserRoleRepository _userRoleRepository;
        private SubmissionCorporationAgentAddressRepository _submissionCorporationAgentAddressRepository;
        private MasterRenewalStatusFeeRepository _masterRenewalStatusFeeRepository;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
          //  masterLicenseFeinRenewal = new MasterLicenseFEINRenewal(_testUnitOfWork);
            _submissionlicencecounter = new SubmissionLicenseNumberCounterRepository(_testUnitOfWork);

            _lookupExistingCategoriesRepository = new Lookup_ExistingCategoriesRepository(_testUnitOfWork);

              _mockstreetTestRepository = new StreetTypesRepository(_testUnitOfWork);

              _etlAddressTestRepository = new EtlAddressAndParcelRepository(_testUnitOfWork);
              _fixFeeRepository = new FixFeeRepository(_testUnitOfWork);
              _bblTestRepository = new BblRepository(_testUnitOfWork);
              _userBblServiceTestRepository = new UserBBLServiceRepository(_testUnitOfWork);
              _userTestRepository = new UserRepository(_testUnitOfWork, _userRoleRepository);
              _submissionQuestionTestRepository = new SubmissionQuestionRepository(_testUnitOfWork);
              _dcbcEntityBblRenewalsTestRepository = new DCBC_ENTITY_BBL_RenewalsRepository(_testUnitOfWork);
              _masterBusinessActivityTestRepository = new MasterBusinessActivityRepository(_testUnitOfWork, _masterPrimaryCategoryTestRepository);
              _submissionBblAssociationToUsersTestRepository = new SubmissionBblAssociationToUsersRepository(_testUnitOfWork);
              _submissionToAccelaTestRepository = new SubmissionToAccelaRepository(_testUnitOfWork);
              _masterSecondaryLicenseCategoryTestRepository = new MasterSecondaryLicenseCategoryRepository(_testUnitOfWork);
              _lookupExistingCategoriesRepository = new Lookup_ExistingCategoriesRepository(_testUnitOfWork);
              _masterBblApplicationStatusTestRepository = new MasterBblApplicationStatusRepository(_testUnitOfWork);
              _corpRespository = new CorpRespository(_testUnitOfWork);
              _userRoleRepository = new UserRoleRepository(_testUnitOfWork);
              _submissionQuestionRepository = new SubmissionQuestionRepository(_testUnitOfWork);
              _masterStateRepository = new MasterStateRepository(_testUnitOfWork);
              _masterCountryRepository = new MasterCountryRepository(_testUnitOfWork);
              _masterCategoryDocumentRepository = new MasterCategoryDocumentRepository(_testUnitOfWork);
              _submissionDocumentToAccelaRepository = new SubmissionDocumentToAccelaRepository(_testUnitOfWork);
              _masterCategoryQuestionRepository = new MasterCategoryQuestionRepository(_testUnitOfWork);
              _masterRenewalStatusFeeRepository = new MasterRenewalStatusFeeRepository(_testUnitOfWork);
              _submissionCorporationAgentAddressRepository = new SubmissionCorporationAgentAddressRepository(_testUnitOfWork, _mockApplicationChcekListTestRepository, _mockSubmissionMasterTestRepository);
              _dcbcEntityBblRenewalInvoiceRepository = new DCBC_ENTITY_BBL_Renewal_InvoiceRepository(_testUnitOfWork, _lookupExistingCategoriesRepository);
              _submissionIndividualRepository = new SubmissionIndividualRepository(_testUnitOfWork, _submissionMasterRepository);
              _masterPrimaryCategoryTestRepository = new MasterPrimaryCategoryRepository(_testUnitOfWork, _masterSecondaryLicenseCategoryTestRepository, _masterCategoryDocumentRepository,
                 _masterCategoryQuestionRepository);
              _masterSubCategoryRepository = new MasterSubCategoryRepository(_testUnitOfWork, _masterPrimaryCategoryTestRepository, _masterSecondaryLicenseCategoryTestRepository);
              _masterCategoryPhysicalLocationTestRepository = new MasterCategoryPhysicalLocationRepository(_testUnitOfWork,
                 _masterPrimaryCategoryTestRepository, _masterCategoryQuestionRepository, _osubcategoryFeesTestRepository, _masterSecondaryLicenseCategoryTestRepository);
              _osubcategoryFeesTestRepository = new OSubCategoryFeesRepository(_testUnitOfWork, _masterPrimaryCategoryTestRepository, _masterSecondaryLicenseCategoryTestRepository);
              _feeCodeMapRepository = new FeeCodeMapRepository(_testUnitOfWork, _osubcategoryFeesTestRepository);
            _mockSubmissionMasterTestRepository = new SubmissionMasterRepository(_testUnitOfWork, _submissionCategoryTestRepository, _bblTestRepository,
             _userBblServiceTestRepository, _submissionQuestionTestRepository, _masterCategoryPhysicalLocationTestRepository, _masterPrimaryCategoryTestRepository,
             _mockApplicationChcekListTestRepository, _userTestRepository, _dcbcEntityBblRenewalsTestRepository, _masterBusinessActivityTestRepository,
             _submissionBblAssociationToUsersTestRepository, _submissionToAccelaTestRepository, _masterSecondaryLicenseCategoryTestRepository,
             _lookupExistingCategoriesRepository, _submissionlicencecounter, _masterBblApplicationStatusTestRepository);

            _masterRegisterAgentRepository=new MasterRegisterAgentRepository(_testUnitOfWork);

            _submissionCofoHopeHopAddressRepository = new SubmissionCofoHopeHopAddressRepository(_testUnitOfWork, _corpRespository, _mockSubmissionMasterTestRepository,
              _mockstreetTestRepository, _masterRegisterAgentRepository, _submissionCorporationRepository, _submissionQuestionRepository, _mockApplicationChcekListTestRepository,
              _masterStateRepository, _masterCountryRepository);

            _mocksubmissionCofoHopeHopTestReposiotry = new SubmissionCofoHopeHopRepository(_testUnitOfWork, _submissionCofoHopeHopAddressRepository,
                 _mockApplicationChcekListTestRepository, _submissionMasterRepository, _mockstreetTestRepository, _fixFeeRepository, _submissionDocumentRepository);


            _mockApplicationChcekListTestRepository = new SubmissionMasterApplicationChcekListRepository(_testUnitOfWork);
            _mockCofoTestRepository = new DCBC_ENTITY_Cof_ORepository(_testUnitOfWork, _mocksubmissionCofoHopeHopTestReposiotry,
               _mockstreetTestRepository,
               _mockSubmissionMasterTestRepository,
              _mockApplicationChcekListTestRepository,_submissionCofoHopeHopAddressRepository,
              _etlAddressTestRepository);

            _submissionMasterRepository = new SubmissionMasterRepository(_testUnitOfWork, _submissionCategoryTestRepository, _bblTestRepository,
           _userBblServiceTestRepository, _submissionQuestionTestRepository, _masterCategoryPhysicalLocationTestRepository, _masterPrimaryCategoryTestRepository,
           _mockApplicationChcekListTestRepository, _userTestRepository, _dcbcEntityBblRenewalsTestRepository, _masterBusinessActivityTestRepository,
           _submissionBblAssociationToUsersTestRepository, _submissionToAccelaTestRepository, _masterSecondaryLicenseCategoryTestRepository,
           _lookupExistingCategoriesRepository, _submissionlicencecounter, _masterBblApplicationStatusTestRepository);

            _submissionDocumentRepository = new SubmissionDocumentRepository(_testUnitOfWork, _submissionCategoryTestRepository,_masterCategoryDocumentRepository,
            _masterPrimaryCategoryTestRepository,_masterSecondaryLicenseCategoryTestRepository,_submissionMasterRepository,_masterCategoryPhysicalLocationTestRepository,
            _mockApplicationChcekListTestRepository, _submissionIndividualRepository, _submissionDocumentToAccelaRepository);

            _submissionCategoryTestRepository = new SubmissionCategoryRepository(_testUnitOfWork, _masterPrimaryCategoryTestRepository, _masterSecondaryLicenseCategoryTestRepository,
                _osubcategoryFeesTestRepository, _fixFeeRepository, _submissionQuestionRepository, _masterSubCategoryRepository, _masterCategoryPhysicalLocationTestRepository,
                _feeCodeMapRepository, _mockApplicationChcekListTestRepository, _submissionMasterRenewalRepository, _dcbcEntityBblRenewalInvoiceRepository, _lookupExistingCategoriesRepository
                );
            _submissionCorporationRepository = new SubmissionCorporationRepository(_testUnitOfWork, _submissionCorporationAgentAddressRepository, _mockApplicationChcekListTestRepository,
              _masterRegisterAgentRepository, _submissionQuestionRepository, _corpRespository, _submissionMasterRepository, _masterCountryRepository, _masterStateRepository);
            _submissionMasterRenewalRepository = new SubmissionMasterRenewalRepository(_testUnitOfWork, _masterRenewalStatusFeeRepository);

            //Setup Data
            var cofoData = new DCBC_ENTITY_Cof_ORepositoryData();
            _testDbContext.DCBC_ENTITY_Cof_O.AddRange(cofoData.DCBCCofoEntitiesList);
            _testDbContext.SaveChanges();

            //Setup Data
            var submissionCofoHopeHopData = new SubmissionCofoHopeHopData();
            _testDbContext.SubmissionCofo_Hop_Ehop.AddRange(submissionCofoHopeHopData.SubmissionCofoHopEhopList);
            _testDbContext.SaveChanges();

            //Setup Data
            var streetTypesData = new StreetTypesRepositoryData();
            _testDbContext.StreetTypes.AddRange(streetTypesData.ListAll);
            _testDbContext.SaveChanges();

            //Setup Data
            var submissionMasterData = new SubmissionMasterRepositoryData();
            _testDbContext.SubmissionMaster.AddRange(submissionMasterData.SubmissionMasterEntitiesList);
            _testDbContext.SaveChanges();

            //Setup Data
            var  checklistData= new SubmissionMasterApplicationChcekListRepositoryData();
            _testDbContext.SubmissionMaster_ApplicationCheckList.AddRange(checklistData.SubmissionMaster_ApplicationCheckListEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  EtlAddressAndParcelRepository FackData Initialization
            var testData = new EtlAddressAndParcelRepositoryData();
            _testDbContext.TBL_ETL_Address_And_Parcel.AddRange(testData.ETLAddessEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  SubmissionCofo_Hop_Ehop_Address FackData
            var cofoHopeHopAddressData = new SubmissionCofoHopeHopAddressData();
            _testDbContext.SubmissionCofo_Hop_Ehop_Address.AddRange(cofoHopeHopAddressData.SubmissionCofoHopEhopAddressList);
            _testDbContext.SaveChanges();

        }
        [TestMethod]
        public void FindByNumber_Returns_Test()
        {
           
            //Act 
            string date = DateTime.Now.ToString();
            var cofoRows = _mockCofoTestRepository.FindByNumber("CO1501622", date);

            //Assert
            Assert.IsNotNull(cofoRows); // Test if null
            Assert.IsTrue(cofoRows.Count() == 1);
        }
        [TestMethod]
        public void FindByNumberandDateofIssue_COFO_Number_Returns_Test()
        {
            //Initialize Entites
            var cofoHopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Number = "CO1501622",
                DateofIssue = DateTime.Now.ToString(),
                Type = "COFO"

            };
            //Act 

            var cofoRows = _mockCofoTestRepository.FindByNumberandDateofIssue(cofoHopDetailsModel).ToList();

            //Assert
            Assert.IsNotNull(cofoRows); // Test if null

            Assert.IsTrue(cofoRows.Count() == 1);

        }
        [TestMethod]
        public void FindByNumberandDateofIssue_HOP_Number_Returns_Test()
        {
            //Initialize Entites
            var cofoHopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
                Number = "HO0800065",
                DateofIssue = "2015-09-25",
                Type = "HOP"

            };
            //Act 

            var cofoRows = _mockCofoTestRepository.FindByNumberandDateofIssue(cofoHopDetailsModel).ToList();

            //Assert
            Assert.IsNotNull(cofoRows); // Test if null

            Assert.IsTrue(cofoRows.Count() == 1);

        }
        [TestMethod]
        public void FindByNumberandDateofIssue_Returns_NODATA_Test()
        {
            //Initialize Entites
            var cofoHopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
                Number = "HO0800066",
                DateofIssue = "2015-09-25",
                Type = "HOP"

            };
            //Act 

            var cofoRows = _mockCofoTestRepository.FindByNumberandDateofIssue(cofoHopDetailsModel).ToList();

            //Assert
            Assert.IsNotNull(cofoRows); // Test if null

            Assert.IsTrue(cofoRows.Count() == 1);

        }
        [TestMethod]
        public void FindByNumberandDateofIssue_Returns_NoSubmissions_Test()
        {
            //Initialize Entites
            var cofoHopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "84c28889-48f2-4066-9956-c214c5b4c2e3",
                Number = "HO0800065",
                DateofIssue = "2015-09-25",
                Type = "HOP"

            };
            //Act 

            var cofoRows = _mockCofoTestRepository.FindByNumberandDateofIssue(cofoHopDetailsModel).ToList();

            //Assert
            Assert.IsNotNull(cofoRows); // Test if null

            Assert.IsTrue(cofoRows.Count() == 1);

        }
        [TestMethod]
        public void GetSubmissionCofoOrHopDetails_Test()
        {
            
            //Act 

            var cofoRows = _mockCofoTestRepository.GetSubmissionCofoOrHopDetails("8c6deecc-3b72-485d-8af5-30939af94e97").ToList();

            //Assert
            Assert.IsNotNull(cofoRows); // Test if null

            Assert.IsTrue(cofoRows.Count() == 1);

        }
        [TestMethod]
        public void DropDownBind_Test()
        {

            //Act 

            var cofoRows = _mockCofoTestRepository.DropDownBind().ToList();

            //Assert
            Assert.IsNotNull(cofoRows); // Test if null

            Assert.IsTrue(cofoRows.Count() == 2);

        }
    }


}
