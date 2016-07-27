using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Model;
using BusinessCenter.Service.Implementation;
using BusinessCenter.Tests.Setup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Service
{
      [TestClass]
   public class SubmissionCofoHopeHopAddressServiceTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        private SubmissionCofoHopeHopAddressRepository _cofoAddressRepository;
        private CorpRespository _corpRespository;
        private SubmissionMasterRepository _submissionMasterRepository;
        private StreetTypesRepository _streetTypesRepository;
        private MasterRegisterAgentRepository _masterRegisterAgentRepository;
        private SubmissionCorporationRepository _submissionCorporationRepository;
        private SubmissionQuestionRepository _submissionQuestionRepository;
        private SubmissionMasterApplicationChcekListRepository _submissionMasterApplicationChcekListRepository;
        private SubmissionCorporationAgentAddressRepository _submissionCorporationAgentAddressRepository;

        private SubmissionCategoryRepository _submissionCategoryRepository;
        private UserBBLServiceRepository _userBblServiceRepository;
        private BblRepository _bblRepository;
        private MasterCategoryPhysicalLocationRepository _masterCategoryPhysicalLocationRepository;
        private MasterPrimaryCategoryRepository _masterPrimaryCategory;
        private UserRepository _userRepository;
        private MasterBusinessActivityRepository _masterBusinessActivityRepository;
        private DCBC_ENTITY_BBL_RenewalsRepository _dcbcEntityBblRenewalsRepository;
        private SubmissionBblAssociationToUsersRepository _submissionBblAssociationToUsersRepository;
        private SubmissionToAccelaRepository _submissionToAccelaRepository;
        private MasterSecondaryLicenseCategoryRepository _masterSecondaryLicenseCategoryRepository;
        private OSubCategoryFeesRepository _mockSubCategoryFeesRepository;
        private FixFeeRepository _fixedFeeTestRepository;
        private MasterSubCategoryRepository _masterSubCategoryTestRepository;
       private MasterCategoryPhysicalLocationRepository _masterCategoryPhysicalLocationTestRepository;
        private FeeCodeMapRepository _feeCodeMapTestRepository;
        private SubmissionMasterRenewalRepository _submissionMasterRenewalRepository;
        private DCBC_ENTITY_BBL_Renewal_InvoiceRepository  _dcbcEntityBblRenewalInvoiceRepository;
        private MasterCategoryQuestionRepository _mockMasterCategoryQuestionRepository;
        private MasterCategoryDocumentRepository _masterCategoryDocument;
         private UserRoleRepository _userRoleRepository;
          private MasterRenewalStatusFeeRepository _masterRenewalStatusFeeRepository;

        private SubmissionCofoHopeHopAddressData _mockData;
       // private MasterLicenseFEINRenewal masterLicenseFeinRenewal;
        private SubmissionCofoHopeHopAddressService _submissionCofoHopeHopAddressTestService;
        private Lookup_ExistingCategoriesRepository _lookupExistingCategoriesRepository;
        private SubmissionLicenseNumberCounterRepository _submissionlicencecounter;

        private MasterCountryRepository _masterCountryRepository;
        private MasterStateRepository _masterStateRepository;
        private MasterBblApplicationStatusRepository _masterBblApplicationStatusTestRepository;


        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _streetTypesRepository = new StreetTypesRepository(_testUnitOfWork);
           // masterLicenseFeinRenewal = new MasterLicenseFEINRenewal(_testUnitOfWork);
            _corpRespository = new CorpRespository(_testUnitOfWork);
            _lookupExistingCategoriesRepository = new Lookup_ExistingCategoriesRepository(_testUnitOfWork);
            _submissionlicencecounter = new SubmissionLicenseNumberCounterRepository(_testUnitOfWork);
            _masterCountryRepository = new MasterCountryRepository(_testUnitOfWork);
            _masterStateRepository = new MasterStateRepository(_testUnitOfWork);
            _submissionBblAssociationToUsersRepository = new SubmissionBblAssociationToUsersRepository(_testUnitOfWork);
            _submissionToAccelaRepository = new SubmissionToAccelaRepository(_testUnitOfWork);
            _submissionMasterApplicationChcekListRepository = new SubmissionMasterApplicationChcekListRepository(_testUnitOfWork);
            _submissionCorporationRepository = new SubmissionCorporationRepository(_testUnitOfWork, _submissionCorporationAgentAddressRepository,
             _submissionMasterApplicationChcekListRepository, _masterRegisterAgentRepository,
             _submissionQuestionRepository, _corpRespository, _submissionMasterRepository, _masterCountryRepository, _masterStateRepository);
            _masterBusinessActivityRepository = new MasterBusinessActivityRepository(_testUnitOfWork, _masterPrimaryCategory);
            _masterSecondaryLicenseCategoryRepository = new MasterSecondaryLicenseCategoryRepository(_testUnitOfWork);
            _masterPrimaryCategory = new MasterPrimaryCategoryRepository(_testUnitOfWork, _masterSecondaryLicenseCategoryRepository, _masterCategoryDocument, _mockMasterCategoryQuestionRepository);
            _masterSubCategoryTestRepository = new MasterSubCategoryRepository(_testUnitOfWork, _masterPrimaryCategory, _masterSecondaryLicenseCategoryRepository);
            _mockSubCategoryFeesRepository = new OSubCategoryFeesRepository(_testUnitOfWork, _masterPrimaryCategory, _masterSecondaryLicenseCategoryRepository);
            _dcbcEntityBblRenewalsRepository = new DCBC_ENTITY_BBL_RenewalsRepository(_testUnitOfWork);
            _fixedFeeTestRepository = new FixFeeRepository(_testUnitOfWork);
            _masterBblApplicationStatusTestRepository = new MasterBblApplicationStatusRepository(_testUnitOfWork);
            _masterRenewalStatusFeeRepository = new MasterRenewalStatusFeeRepository(_testUnitOfWork);
            _userRoleRepository = new UserRoleRepository(_testUnitOfWork);
            _masterCategoryDocument = new MasterCategoryDocumentRepository(_testUnitOfWork);
            _mockMasterCategoryQuestionRepository = new MasterCategoryQuestionRepository(_testUnitOfWork);
            _feeCodeMapTestRepository = new FeeCodeMapRepository(_testUnitOfWork, _mockSubCategoryFeesRepository);
           _submissionMasterRenewalRepository = new SubmissionMasterRenewalRepository(_testUnitOfWork,_masterRenewalStatusFeeRepository);
           _dcbcEntityBblRenewalInvoiceRepository = new DCBC_ENTITY_BBL_Renewal_InvoiceRepository(_testUnitOfWork, _lookupExistingCategoriesRepository);
            _masterCategoryPhysicalLocationRepository = new MasterCategoryPhysicalLocationRepository(_testUnitOfWork, _masterPrimaryCategory, _mockMasterCategoryQuestionRepository,
              _mockSubCategoryFeesRepository, _masterSecondaryLicenseCategoryRepository);
           

            _submissionCategoryRepository = new SubmissionCategoryRepository(_testUnitOfWork, _masterPrimaryCategory, _masterSecondaryLicenseCategoryRepository,
               _mockSubCategoryFeesRepository, _fixedFeeTestRepository, _submissionQuestionRepository, _masterSubCategoryTestRepository, _masterCategoryPhysicalLocationTestRepository,
              _feeCodeMapTestRepository, _submissionMasterApplicationChcekListRepository, _submissionMasterRenewalRepository, _dcbcEntityBblRenewalInvoiceRepository,
              _lookupExistingCategoriesRepository);

            _submissionMasterRepository = new SubmissionMasterRepository(_testUnitOfWork, _submissionCategoryRepository, _bblRepository,
               _userBblServiceRepository, _submissionQuestionRepository, _masterCategoryPhysicalLocationRepository, _masterPrimaryCategory,
               _submissionMasterApplicationChcekListRepository, _userRepository, _dcbcEntityBblRenewalsRepository, _masterBusinessActivityRepository,
               _submissionBblAssociationToUsersRepository, _submissionToAccelaRepository, _masterSecondaryLicenseCategoryRepository,
               _lookupExistingCategoriesRepository, _submissionlicencecounter, _masterBblApplicationStatusTestRepository);

            _submissionQuestionRepository = new SubmissionQuestionRepository(_testUnitOfWork);

            _cofoAddressRepository = new SubmissionCofoHopeHopAddressRepository(_testUnitOfWork, _corpRespository, _submissionMasterRepository,
              _streetTypesRepository, _masterRegisterAgentRepository, _submissionCorporationRepository, _submissionQuestionRepository,
              _submissionMasterApplicationChcekListRepository, _masterStateRepository, _masterCountryRepository);

            _masterRegisterAgentRepository = new MasterRegisterAgentRepository(_testUnitOfWork);
            _userRepository = new UserRepository(_testUnitOfWork, _userRoleRepository);
            _masterCategoryPhysicalLocationTestRepository = new MasterCategoryPhysicalLocationRepository(_testUnitOfWork, _masterPrimaryCategory, _mockMasterCategoryQuestionRepository,
             _mockSubCategoryFeesRepository, _masterSecondaryLicenseCategoryRepository);

            _submissionCorporationAgentAddressRepository = new SubmissionCorporationAgentAddressRepository(_testUnitOfWork, _submissionMasterApplicationChcekListRepository, _submissionMasterRepository);
            _userBblServiceRepository = new UserBBLServiceRepository(_testUnitOfWork);
            _bblRepository = new BblRepository(_testUnitOfWork);

            var submissionChecklistData = new SubmissionMasterApplicationChcekListRepositoryData();
            _testDbContext.SubmissionMaster_ApplicationCheckList.AddRange(
                submissionChecklistData.SubmissionMaster_ApplicationCheckListEntitiesList);
            _testDbContext.SaveChanges();

            _mockData = new SubmissionCofoHopeHopAddressData();

            _submissionCofoHopeHopAddressTestService = new SubmissionCofoHopeHopAddressService(_cofoAddressRepository);

            //Setup Data
            var testData = new SubmissionCofoHopeHopAddressData();
            _testDbContext.SubmissionCofo_Hop_Ehop_Address.AddRange(testData.SubmissionCofoHopEhopAddressList);
            _testDbContext.SaveChanges();

            var corpData = new CorpRepositoryData();
            _testDbContext.DCBC_ENTITY_CORP.AddRange(corpData.CorpEntitiesList);
            _testDbContext.SaveChanges();

            var masterData = new SubmissionMasterRepositoryData();
            _testDbContext.SubmissionMaster.AddRange(masterData.SubmissionMasterEntitiesList);
            _testDbContext.SaveChanges();

            var streetTypeData = new StreetTypesRepositoryData();
            _testDbContext.StreetTypes.AddRange(streetTypeData.ListAll);
            _testDbContext.SaveChanges();

            //var masterRegisterAgentData = new MasterRegisterAgentData();
            //_testDbContext.MasterRegisteredAgent.AddRange(masterRegisterAgentData.MasterRegisteredAgentEntitiesList);
            //_testDbContext.SaveChanges();

            var submissionCorporationData = new SubmissionCorporationRepositoryData();
            _testDbContext.SubmissionCorporation_Agent.AddRange(submissionCorporationData.SubmissionCorporationEntitiesList);
            _testDbContext.SaveChanges();

            var submissionQuestionData = new SubmissionQuestionRepositoryData();
            _testDbContext.SubmissionQuestion.AddRange(submissionQuestionData.SubmissionQuestionEntitiesList);
            _testDbContext.SaveChanges();

        
        }
        [TestMethod]
        public void GetCorpBusinessDataNoDataTest()
        {
            var generalBusiness = new GeneralBusiness { MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40", FileNumber = "" };
            //Act 
            var cofoAddress = _submissionCofoHopeHopAddressTestService.GetCorpBusinessData(generalBusiness).ToList();

            //Assert
            Assert.IsNotNull(cofoAddress); // Test if null
            Assert.IsTrue(cofoAddress.FirstOrDefault().EntityStatus == "NODATA");
        }
        [TestMethod]
        public void GetCorpBusinessDataTest()
        {
            var generalBusiness = new GeneralBusiness { MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40", FileNumber = "C212821" };
            //Act 
            var cofoAddress = _submissionCofoHopeHopAddressTestService.GetCorpBusinessData(generalBusiness).ToList();

            //Assert
            Assert.IsNotNull(cofoAddress); // Test if null
            Assert.IsTrue(cofoAddress.FirstOrDefault().FileNumber == "C212821");
        }
    }

}
