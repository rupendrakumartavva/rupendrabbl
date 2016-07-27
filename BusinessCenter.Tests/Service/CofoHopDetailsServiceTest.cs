using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Model;
using BusinessCenter.Service.Implementation;
using BusinessCenter.Tests.Setup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace BusinessCenter.Tests.Service
{
    [TestClass]
    public class CofoHopDetailsServiceTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

      //  private CofoHopDetailsRepository _mockCofoHopDetailsTestRepository;
        private StreetTypesRepository _streetTypesRepository;
        private SubmissionCofoHopeHopRepository _submissionCofoHopeHopRepository;
        private SubmissionCofoHopeHopAddressRepository _submissionCofoHopeHopAddressRepository;

       
        private SubmissionMasterApplicationChcekListRepository _submissionMasterApplicationChcekListRepository;
        private SubmissionMasterRepository _submissionMasterRepository;
        private FixFeeRepository _fixFeeRepository;
        private SubmissionDocumentRepository _submissionDocumentRepository;

        private CorpRespository _corpRespository;
        private MasterRegisterAgentRepository _masterRegisterAgentRepository;
        private SubmissionCorporationRepository _submissionCorporationRepository;
        private SubmissionQuestionRepository _submissionQuestionRepository;

        private DCBC_ENTITY_Cof_ORepository _dcbcEntityCofORepository;

        //private StreetTypesRepositoryData _mockStreetTypesData;
        //private SubmissionCofoHopeHopData _mockCofoHopeHopData;
        //private SubmissionCofoHopeHopAddressData _mockcCofoHopeHopAddressData;
       // private SubmissionMasterRepository _submissionMasterRepository;
        private SubmissionCategoryRepository _submissionCategoryRepository;
        private BblRepository _bblRepository;
        private UserBBLServiceRepository _userBblServiceRepository;
        private MasterCategoryPhysicalLocationRepository _masterCategoryPhysicalLocationRepository;
        private MasterPrimaryCategoryRepository _masterPrimaryCategoryRepository;
        private UserRepository _userRepository;
        private DCBC_ENTITY_BBL_RenewalsRepository _dcbcEntityBblRenewalsRepository;
        private MasterBusinessActivityRepository _masterBusinessActivityRepository;
        private SubmissionBblAssociationToUsersRepository _submissionBblAssociationToUsersRepository;
        private SubmissionToAccelaRepository _submissionToAccelaRepository;
        private MasterSecondaryLicenseCategoryRepository _masterSecondaryLicenseCategoryRepository;
        private CofoHopDetailsService _cofoHopDetailsTestService;
        private Lookup_ExistingCategoriesRepository _lookupExistingCategoriesRepository;
     //   private MasterLicenseFEINRenewal masterLicenseFeinRenewal;
        private MasterCategoryDocumentRepository _masterCategoryDocumentRepository;
        private MasterCountryRepository _masterCountryRepository;
        private MasterStateRepository _masterStateRepository;
        private SubmissionLicenseNumberCounterRepository _submissionlicencecounter;
        private EtlAddressAndParcelRepository _etlAddressTestRepository;
          private MasterBblApplicationStatusRepository _masterBblApplicationStatusRepository;
          private SubmissionIndividualRepository _submissionIndividualRepository;
          private SubmissionDocumentToAccelaRepository _submissionDocumentToAccelaRepository;
          private SubmissionCorporationAgentAddressRepository _submissionCorporationAgentAddressRepository;
          private OSubCategoryFeesRepository _oSubCategoryFeesRepository;
          private FeeCodeMapRepository _feeCodeMapRepository;
          private MasterSubCategoryRepository _masterSubCategoryRepository;
          private SubmissionMasterRenewalRepository _submissionMasterRenewalRepository;
          private DCBC_ENTITY_BBL_Renewal_InvoiceRepository _dcbcEntityBblRenewalInvoiceRepository;
          private MasterCategoryQuestionRepository _masterCategoryQuestionRepository;
          private UserRoleRepository _userRoleRepository;
          private MasterRenewalStatusFeeRepository _masterRenewalStatusFeeRepository;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
         //   masterLicenseFeinRenewal = new MasterLicenseFEINRenewal(_testUnitOfWork);
            _bblRepository = new BblRepository(_testUnitOfWork);
            _submissionlicencecounter = new SubmissionLicenseNumberCounterRepository(_testUnitOfWork);
            _fixFeeRepository = new FixFeeRepository(_testUnitOfWork);
            _userRoleRepository = new UserRoleRepository(_testUnitOfWork);
            _masterRenewalStatusFeeRepository = new MasterRenewalStatusFeeRepository(_testUnitOfWork);
            _etlAddressTestRepository=new EtlAddressAndParcelRepository(_testUnitOfWork);
            _masterCountryRepository = new MasterCountryRepository(_testUnitOfWork);
            _masterStateRepository = new MasterStateRepository(_testUnitOfWork);
            _masterCategoryDocumentRepository = new MasterCategoryDocumentRepository(_testUnitOfWork);
            _masterCategoryQuestionRepository = new MasterCategoryQuestionRepository(_testUnitOfWork);
            _masterSecondaryLicenseCategoryRepository = new MasterSecondaryLicenseCategoryRepository(_testUnitOfWork);
            _submissionToAccelaRepository = new SubmissionToAccelaRepository(_testUnitOfWork);
            _submissionDocumentToAccelaRepository = new SubmissionDocumentToAccelaRepository(_testUnitOfWork);
            _submissionBblAssociationToUsersRepository = new SubmissionBblAssociationToUsersRepository(_testUnitOfWork);
            _submissionQuestionRepository = new SubmissionQuestionRepository(_testUnitOfWork);
            _corpRespository = new CorpRespository(_testUnitOfWork);
            _masterRegisterAgentRepository = new MasterRegisterAgentRepository(_testUnitOfWork);
            _userBblServiceRepository=new UserBBLServiceRepository(_testUnitOfWork);
            _dcbcEntityBblRenewalsRepository = new DCBC_ENTITY_BBL_RenewalsRepository(_testUnitOfWork);
            _submissionMasterApplicationChcekListRepository = new SubmissionMasterApplicationChcekListRepository(_testUnitOfWork);
            _masterBblApplicationStatusRepository = new MasterBblApplicationStatusRepository(_testUnitOfWork);
            _streetTypesRepository = new StreetTypesRepository(_testUnitOfWork);
            _lookupExistingCategoriesRepository = new Lookup_ExistingCategoriesRepository(_testUnitOfWork);
            _dcbcEntityBblRenewalInvoiceRepository = new DCBC_ENTITY_BBL_Renewal_InvoiceRepository(_testUnitOfWork, _lookupExistingCategoriesRepository);
            _submissionIndividualRepository = new SubmissionIndividualRepository(_testUnitOfWork, _submissionMasterRepository);
            _submissionMasterRenewalRepository = new SubmissionMasterRenewalRepository(_testUnitOfWork, _masterRenewalStatusFeeRepository);
            _userRepository = new UserRepository(_testUnitOfWork, _userRoleRepository);
            _masterSubCategoryRepository = new MasterSubCategoryRepository(_testUnitOfWork, _masterPrimaryCategoryRepository, _masterSecondaryLicenseCategoryRepository);
            _oSubCategoryFeesRepository = new OSubCategoryFeesRepository(_testUnitOfWork, _masterPrimaryCategoryRepository, _masterSecondaryLicenseCategoryRepository);
            _feeCodeMapRepository = new FeeCodeMapRepository(_testUnitOfWork, _oSubCategoryFeesRepository);
            _masterBusinessActivityRepository = new MasterBusinessActivityRepository(_testUnitOfWork, _masterPrimaryCategoryRepository);
            _submissionCorporationAgentAddressRepository = new SubmissionCorporationAgentAddressRepository(_testUnitOfWork, _submissionMasterApplicationChcekListRepository, _submissionMasterRepository);
            _masterPrimaryCategoryRepository = new MasterPrimaryCategoryRepository(_testUnitOfWork, _masterSecondaryLicenseCategoryRepository, _masterCategoryDocumentRepository, _masterCategoryQuestionRepository);
            _masterCategoryPhysicalLocationRepository = new MasterCategoryPhysicalLocationRepository(_testUnitOfWork,
              _masterPrimaryCategoryRepository, _masterCategoryQuestionRepository, _oSubCategoryFeesRepository, _masterSecondaryLicenseCategoryRepository);
            _submissionMasterRepository = new SubmissionMasterRepository(_testUnitOfWork, _submissionCategoryRepository, _bblRepository,
         _userBblServiceRepository, _submissionQuestionRepository, _masterCategoryPhysicalLocationRepository, _masterPrimaryCategoryRepository,
         _submissionMasterApplicationChcekListRepository, _userRepository, _dcbcEntityBblRenewalsRepository, _masterBusinessActivityRepository,
         _submissionBblAssociationToUsersRepository, _submissionToAccelaRepository, _masterSecondaryLicenseCategoryRepository, _lookupExistingCategoriesRepository,
          _submissionlicencecounter, _masterBblApplicationStatusRepository);
            //_mockCofoHopDetailsTestRepository = new CofoHopDetailsRepository(_testUnitOfWork, _mockStreetTypesTestRepository,
            //   _mockSubmissionCofoHopeHopTestRepository,
            //  _mockSubmissionCofoHopeHopAddressTestRepository, _submissionMasterTestRepository);

            _submissionCofoHopeHopRepository = new SubmissionCofoHopeHopRepository(_testUnitOfWork, _submissionCofoHopeHopAddressRepository,
            _submissionMasterApplicationChcekListRepository,
            _submissionMasterRepository,
              _streetTypesRepository,
              _fixFeeRepository,
             _submissionDocumentRepository);
            _submissionCategoryRepository = new SubmissionCategoryRepository(_testUnitOfWork, _masterPrimaryCategoryRepository,
       _masterSecondaryLicenseCategoryRepository, _oSubCategoryFeesRepository, _fixFeeRepository, _submissionQuestionRepository,
       _masterSubCategoryRepository, _masterCategoryPhysicalLocationRepository, _feeCodeMapRepository,
       _submissionMasterApplicationChcekListRepository, _submissionMasterRenewalRepository, _dcbcEntityBblRenewalInvoiceRepository,
        _lookupExistingCategoriesRepository);
            _submissionDocumentRepository = new SubmissionDocumentRepository(_testUnitOfWork, _submissionCategoryRepository, _masterCategoryDocumentRepository,
              _masterPrimaryCategoryRepository, _masterSecondaryLicenseCategoryRepository, _submissionMasterRepository, _masterCategoryPhysicalLocationRepository,
              _submissionMasterApplicationChcekListRepository, _submissionIndividualRepository, _submissionDocumentToAccelaRepository);

            _submissionCofoHopeHopAddressRepository = new SubmissionCofoHopeHopAddressRepository(_testUnitOfWork,_corpRespository,
               _submissionMasterRepository,
              _streetTypesRepository,
             _masterRegisterAgentRepository,
             _submissionCorporationRepository,
            _submissionQuestionRepository,
           _submissionMasterApplicationChcekListRepository, _masterStateRepository, _masterCountryRepository);
            _submissionCorporationRepository = new SubmissionCorporationRepository(_testUnitOfWork,
            _submissionCorporationAgentAddressRepository,
            _submissionMasterApplicationChcekListRepository,
            _masterRegisterAgentRepository,
            _submissionQuestionRepository,
            _corpRespository,
            _submissionMasterRepository, _masterCountryRepository, _masterStateRepository
            );
            _dcbcEntityCofORepository = new DCBC_ENTITY_Cof_ORepository(_testUnitOfWork, _submissionCofoHopeHopRepository,
               _streetTypesRepository,
               _submissionMasterRepository,
              _submissionMasterApplicationChcekListRepository, _submissionCofoHopeHopAddressRepository,
              _etlAddressTestRepository);

            //_mockCofoHopDetailsTestRepository = new CofoHopDetailsRepository(_testUnitOfWork, _mockStreetTypesTestRepository,
            //   _mockSubmissionCofoHopeHopTestRepository,
            //  _mockSubmissionCofoHopeHopAddressTestRepository, _submissionMasterTestRepository);


            _cofoHopDetailsTestService
                = new CofoHopDetailsService( _dcbcEntityCofORepository);

            //_mockStreetTypesData = new StreetTypesRepositoryData();
            //_mockCofoHopeHopData = new SubmissionCofoHopeHopData();
            //_mockcCofoHopeHopAddressData = new SubmissionCofoHopeHopAddressData();

            //Setup Data
            var streetTypesData = new StreetTypesRepositoryData();
            _testDbContext.StreetTypes.AddRange(streetTypesData.ListAll);
            _testDbContext.SaveChanges();

            // 
            var cofoHopeHopData = new SubmissionCofoHopeHopData();
            _testDbContext.SubmissionCofo_Hop_Ehop.AddRange(cofoHopeHopData.SubmissionCofoHopEhopList);
            _testDbContext.SaveChanges();

            var cofoHopeHopAddressData = new SubmissionCofoHopeHopAddressData();
            _testDbContext.SubmissionCofo_Hop_Ehop_Address.AddRange(cofoHopeHopAddressData.SubmissionCofoHopEhopAddressList);
            _testDbContext.SaveChanges();
            var masterdata = new SubmissionMasterRepositoryData();
            _testDbContext.SubmissionMaster.AddRange(masterdata.SubmissionMasterEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  EtlAddressAndParcelRepository FackData Initialization
            var testData = new EtlAddressAndParcelRepositoryData();
            _testDbContext.TBL_ETL_Address_And_Parcel.AddRange(testData.ETLAddessEntitiesList);
            _testDbContext.SaveChanges();

        }
        [TestMethod]
        public void GetSubmissionCofoOrHopDetails_Returns_Test()
        {


            //Act 
            var cofoHopRows = _cofoHopDetailsTestService.GetSubmissionCofoOrHopDetails("cce0b056-d2a3-485e-a02a-e34c957c4e40");

            //Assert
            Assert.IsNotNull(cofoHopRows); // Test if null
            Assert.IsTrue(cofoHopRows.Count() == 1);
        }
        [TestMethod]
        public void DropDownBind_Returns_Test()
        {


            //Act 
            var cofoHopRows = _cofoHopDetailsTestService.DropDownsBind();

            //Assert
            Assert.IsNotNull(cofoHopRows); // Test if null
            Assert.IsTrue(cofoHopRows.Count() == 2);
        }
        [TestMethod]
        public void FindByNumberandDateofIssue_COFO_Number_Returns_Test()
        {
            //Initialize Entites
            var cofoHopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Number = "CO1501622",
                DateofIssue = "2015-09-25",
                Type = "COFO"

            };
            //Act 

            var cofoRows = _cofoHopDetailsTestService.FindByNumberandDateofIssue(cofoHopDetailsModel).ToList();

            //Assert
            Assert.IsNotNull(cofoRows); // Test if null

            Assert.IsTrue(cofoRows.Count() == 1);

        }

    }
}
