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
using BusinessCenter.Data;
using Moq;
using BusinessCenter.Data.Interface;

namespace BusinessCenter.Tests.Service
{
     [TestClass]
   public class MastereHopEligibilityServiceTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private MastereHopEligibilityRepository _mastereHopEligibilityTestRepository;
        private SubmissionEHOPEligibilityRepository _submissionEhopEligibilityTestRepository;
        private SubmissionMasterApplicationChcekListRepository _masterApplicationChcekListTestRepository;
        private SubmissionCofoHopeHopRepository _submissionCofoHopeHopTestRepository;
         private UserRoleRepository _userRoleRepository;
        private SubmissionCofoHopeHopAddressRepository _submissionCofoHopeHopAddressTestRepository;
        private SubmissionMasterRepository _submissionMasterTestRepository;
        private FixFeeRepository _fixFeeTestRepository;
        private StreetTypesRepository _streetTypesTestRepository;
        private SubmissionDocumentRepository _submissionDocumentTestRepository;
         private UserRepository _userRepository;
         private CorpRespository _corpRespository;
         private MasterRegisterAgentRepository _masterRegisterAgentRepository;
         private SubmissionCorporationRepository _submissionCorporationRepository;
         private SubmissionQuestionRepository _submissionQuestionRepository;
         private MasterStateRepository _masterStateRepository;
         private MasterCountryRepository _masterCountryRepository;
        private MastereHopEligibilityRepositoryData _mockData;

        private MastereHopEligibilityService _mastereHopEligibilityTestService;
        private MasterEhopOptionTypeRepository _masterEhopOptionType;
        private SubmissionCategoryRepository _submissionCategoryTestRepository;
        private BblRepository _bblTestRepository;
        private UserBBLServiceRepository _userBblServiceTestRepository;
        private MasterCategoryPhysicalLocationRepository _masterCategoryPhysicalLocationTestRepository;
        private MasterPrimaryCategoryRepository _masterPrimaryCategoryTestRepository;
        private DCBC_ENTITY_BBL_RenewalsRepository _dcbcEntityBblRenewalsTestRepository;
        private MasterBusinessActivityRepository _masterBusinessActivityTestRepository;
        private SubmissionBblAssociationToUsersRepository _submissionBblAssociationToUsersTestRepository;
        private SubmissionToAccelaRepository _submissionToAccelaTestRepository;
        private MasterSecondaryLicenseCategoryRepository _masterSecondaryLicenseCategoryTestRepository;
        private Lookup_ExistingCategoriesRepository _lookupExistingCategoriesRepository;
        private SubmissionLicenseNumberCounterRepository _submissionlicencecounter;
        private MasterBblApplicationStatusRepository _masterBblApplicationStatusTestRepository;
        private MasterCategoryDocumentRepository _masterCategoryDocumentTestRepository;
        private SubmissionIndividualRepository _submissionIndividualTestRepository;
        private SubmissionDocumentToAccelaRepository _submissionDocumentToAccelaTestRepository;
        private OSubCategoryFeesRepository _oSubCategoryFeesTestRepository;
        private MasterSubCategoryRepository _masterSubCategoryTestRepository;
        private FeeCodeMapRepository _feeCodeMapTestRepository;
        private SubmissionMasterRenewalRepository _submissionMasterRenewalTestRepository;
        private Mock<IDCBC_ENTITY_BBL_Renewal_InvoiceRepository> _iDcbcEntityBblRenewalInvoiceReposiotory;
        private SubmissionCorporationAgentAddressRepository _submissionCorporationAgentAddressRepository;
        private MasterCategoryQuestionRepository _masterCategoryQuestionRepository;
        private MasterRenewalStatusFeeRepository _masterrenewalstatusfee;

        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
          

           
            _masterApplicationChcekListTestRepository = new SubmissionMasterApplicationChcekListRepository(_testUnitOfWork);
            _iDcbcEntityBblRenewalInvoiceReposiotory = new Mock<IDCBC_ENTITY_BBL_Renewal_InvoiceRepository>();
            _dcbcEntityBblRenewalsTestRepository = new DCBC_ENTITY_BBL_RenewalsRepository(_testUnitOfWork);
            _masterBusinessActivityTestRepository = new MasterBusinessActivityRepository(_testUnitOfWork, _masterPrimaryCategoryTestRepository);
            _masterSubCategoryTestRepository = new MasterSubCategoryRepository(_testUnitOfWork, _masterPrimaryCategoryTestRepository, _masterSecondaryLicenseCategoryTestRepository);
            _submissionBblAssociationToUsersTestRepository = new SubmissionBblAssociationToUsersRepository(_testUnitOfWork);
            _submissionToAccelaTestRepository = new SubmissionToAccelaRepository(_testUnitOfWork);
            _masterSecondaryLicenseCategoryTestRepository = new MasterSecondaryLicenseCategoryRepository(_testUnitOfWork);
            _submissionlicencecounter = new SubmissionLicenseNumberCounterRepository(_testUnitOfWork);
            _masterBblApplicationStatusTestRepository = new MasterBblApplicationStatusRepository(_testUnitOfWork);
            _masterCategoryDocumentTestRepository = new MasterCategoryDocumentRepository(_testUnitOfWork);
            _submissionDocumentToAccelaTestRepository = new SubmissionDocumentToAccelaRepository(_testUnitOfWork);
            _lookupExistingCategoriesRepository = new Lookup_ExistingCategoriesRepository(_testUnitOfWork);
            _masterrenewalstatusfee = new MasterRenewalStatusFeeRepository(_testUnitOfWork);
            _masterCategoryQuestionRepository = new MasterCategoryQuestionRepository(_testUnitOfWork);
            _submissionCorporationAgentAddressRepository = new SubmissionCorporationAgentAddressRepository(_testUnitOfWork, _masterApplicationChcekListTestRepository, _submissionMasterTestRepository);
            _submissionMasterRenewalTestRepository = new SubmissionMasterRenewalRepository(_testUnitOfWork, _masterrenewalstatusfee);
            _fixFeeTestRepository = new FixFeeRepository(_testUnitOfWork);
            _feeCodeMapTestRepository = new FeeCodeMapRepository(_testUnitOfWork, _oSubCategoryFeesTestRepository);
            _bblTestRepository = new BblRepository(_testUnitOfWork);
            _streetTypesTestRepository = new StreetTypesRepository(_testUnitOfWork);
            _corpRespository = new CorpRespository(_testUnitOfWork);
            _masterStateRepository = new MasterStateRepository(_testUnitOfWork);
            _masterCountryRepository = new MasterCountryRepository(_testUnitOfWork);
            _submissionQuestionRepository = new SubmissionQuestionRepository(_testUnitOfWork);
            _submissionCofoHopeHopTestRepository = new SubmissionCofoHopeHopRepository(_testUnitOfWork,
               _submissionCofoHopeHopAddressTestRepository,
               _masterApplicationChcekListTestRepository,
               _submissionMasterTestRepository,
              _streetTypesTestRepository,
             _fixFeeTestRepository,
             _submissionDocumentTestRepository);
            _userRoleRepository=new UserRoleRepository(_testUnitOfWork);
            _userRepository = new UserRepository(_testUnitOfWork, _userRoleRepository);
            _masterEhopOptionType = new MasterEhopOptionTypeRepository(_testUnitOfWork);
            _userRepository = new UserRepository(_testUnitOfWork, _userRoleRepository);
            _userBblServiceTestRepository = new UserBBLServiceRepository(_testUnitOfWork);
            _submissionEhopEligibilityTestRepository = new SubmissionEHOPEligibilityRepository(_testUnitOfWork,
            _mastereHopEligibilityTestRepository,
            _masterApplicationChcekListTestRepository,_userRepository,
           _submissionCofoHopeHopTestRepository, _submissionDocumentTestRepository, _masterEhopOptionType);

            _masterRegisterAgentRepository = new MasterRegisterAgentRepository(_testUnitOfWork);
            _masterPrimaryCategoryTestRepository = new MasterPrimaryCategoryRepository(_testUnitOfWork, _masterSecondaryLicenseCategoryTestRepository, _masterCategoryDocumentTestRepository,
              _masterCategoryQuestionRepository);
            _oSubCategoryFeesTestRepository = new OSubCategoryFeesRepository(_testUnitOfWork, _masterPrimaryCategoryTestRepository, _masterSecondaryLicenseCategoryTestRepository);
            _masterCategoryPhysicalLocationTestRepository = new MasterCategoryPhysicalLocationRepository(_testUnitOfWork,
                _masterPrimaryCategoryTestRepository, _masterCategoryQuestionRepository, _oSubCategoryFeesTestRepository, _masterSecondaryLicenseCategoryTestRepository);


            _submissionCorporationRepository = new SubmissionCorporationRepository(_testUnitOfWork, _submissionCorporationAgentAddressRepository, _masterApplicationChcekListTestRepository,
              _masterRegisterAgentRepository, _submissionQuestionRepository, _corpRespository, _submissionMasterTestRepository, _masterCountryRepository, _masterStateRepository);
           

            _mastereHopEligibilityTestRepository = new MastereHopEligibilityRepository(_testUnitOfWork);
            _submissionCofoHopeHopAddressTestRepository = new SubmissionCofoHopeHopAddressRepository(_testUnitOfWork,
                _corpRespository, _submissionMasterTestRepository,
                _streetTypesTestRepository, _masterRegisterAgentRepository, _submissionCorporationRepository,
                _submissionQuestionRepository, _masterApplicationChcekListTestRepository,
                _masterStateRepository, _masterCountryRepository);

            
              _submissionMasterTestRepository = new SubmissionMasterRepository(_testUnitOfWork, _submissionCategoryTestRepository, _bblTestRepository,
           _userBblServiceTestRepository, _submissionQuestionRepository, _masterCategoryPhysicalLocationTestRepository, _masterPrimaryCategoryTestRepository,
           _masterApplicationChcekListTestRepository, _userRepository, _dcbcEntityBblRenewalsTestRepository, _masterBusinessActivityTestRepository,
           _submissionBblAssociationToUsersTestRepository, _submissionToAccelaTestRepository, _masterSecondaryLicenseCategoryTestRepository,
           _lookupExistingCategoriesRepository, _submissionlicencecounter, _masterBblApplicationStatusTestRepository);

              _submissionIndividualTestRepository = new SubmissionIndividualRepository(_testUnitOfWork, _submissionMasterTestRepository);

              _submissionDocumentTestRepository = new SubmissionDocumentRepository(_testUnitOfWork,
            _submissionCategoryTestRepository,
            _masterCategoryDocumentTestRepository,
            _masterPrimaryCategoryTestRepository,
            _masterSecondaryLicenseCategoryTestRepository, _submissionMasterTestRepository,
            _masterCategoryPhysicalLocationTestRepository, _masterApplicationChcekListTestRepository,
            _submissionIndividualTestRepository, _submissionDocumentToAccelaTestRepository);

              _submissionCategoryTestRepository = new SubmissionCategoryRepository(_testUnitOfWork, _masterPrimaryCategoryTestRepository,
            _masterSecondaryLicenseCategoryTestRepository,
            _oSubCategoryFeesTestRepository, _fixFeeTestRepository,
            _submissionQuestionRepository, _masterSubCategoryTestRepository, _masterCategoryPhysicalLocationTestRepository,
            _feeCodeMapTestRepository, _masterApplicationChcekListTestRepository, _submissionMasterRenewalTestRepository, _iDcbcEntityBblRenewalInvoiceReposiotory.Object,
            _lookupExistingCategoriesRepository);

            //Mocking Repository
            _mockData = new MastereHopEligibilityRepositoryData();

            _mastereHopEligibilityTestService = new MastereHopEligibilityService(_mastereHopEligibilityTestRepository, _submissionEhopEligibilityTestRepository);

          
            //Setup Data
            var testData = new SubmissionEHOPEligibilityRepositoryData();
            _testDbContext.SubmissionEHOPEligibility.AddRange(testData.SubmissionEHOPEligibilityEntitiesList);
            _testDbContext.SaveChanges();

            // 
            var eHopEligibilityRepositoryData = new MastereHopEligibilityRepositoryData();
            _testDbContext.MastereHOPEligibility.AddRange(eHopEligibilityRepositoryData.MastereHOPEligibilityEntitiesList);
            _testDbContext.SaveChanges();

            var chcekListRepositoryData = new SubmissionMasterApplicationChcekListRepositoryData();
            _testDbContext.SubmissionMaster_ApplicationCheckList.AddRange(chcekListRepositoryData.SubmissionMaster_ApplicationCheckListEntitiesList);
            _testDbContext.SaveChanges();


            //Setup Data
            var cofoHopeHopData = new SubmissionCofoHopeHopData();
            _testDbContext.SubmissionCofo_Hop_Ehop.AddRange(cofoHopeHopData.SubmissionCofoHopEhopList);
            _testDbContext.SaveChanges();
        }
        [TestMethod]
        public void GetMastereHopEligibilityEntitiesTest()
        {
            //Initialize Entites
            var ehopModelEntites = new EhopModel { MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40" };
            //Act 
            var eHopRows = _mastereHopEligibilityTestService.GeMastereHop(ehopModelEntites);

            //Assert
            Assert.IsNotNull(eHopRows); // Test if null
            Assert.IsTrue(eHopRows.Count() == 4);
        }
    }
}
