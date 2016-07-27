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
    public class SubmissionEHOPEligibilityRepositoryTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;


        //Repository declaration
        private SubmissionEHOPEligibilityRepository _submissionEhopEligibilityRepository;
        private MastereHopEligibilityRepository _mastereHopEligibilityRepository;
        private SubmissionMasterApplicationChcekListRepository _submissionMasterApplicationChcekListRepository;
        private SubmissionCofoHopeHopRepository _submissionCofoHopeHopRepository;
        private SubmissionCofoHopeHopAddressRepository _submissionCofoHopeHopAddressRepository;
        private SubmissionMasterRepository _submissionMasterRepository;
        private FixFeeRepository _fixFeeRepository;
        private StreetTypesRepository _streetTypesRepository;
        private SubmissionDocumentRepository _submissionDocumentRepository;
        private CorpRespository _corpRespository;
       
        private MasterRegisterAgentRepository _masterRegisterAgentRepository;
        private SubmissionCorporationRepository _submissionCorporationRepository;
        private SubmissionQuestionRepository _submissionQuestionRepository;
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
     //   private MasterLicenseFEINRenewal masterLicenseFeinRenewal;
        private MasterCategoryDocumentRepository _masterCategoryDocumentRepository;
        private SubmissionIndividualRepository _submissionIndividualRepository;
        private SubmissionDocumentToAccelaRepository _submissionDocumentToAccelaRepository;
        //private SubmissionEHOPEligibilityRepositoryData _mockEHOPEligibilityData;
        //private MastereHopEligibilityRepositoryData _mockMastereHopEligibilityData;
        //private SubmissionMasterApplicationChcekListRepositoryData _mockApplicationChcekListData;
        //private SubmissionCofoHopeHopData _mocksubmissionCofoHopeHopData;
        //private SubmissionCofoHopeHopAddressData _mocksubmissionCofoHopeHopAddressData;
        //private SubmissionMasterRepositoryData _mocksubmissionMasterData;
        private UserRoleRepository _userRoleRepository;
     //   private UserRepository _userRepository;
      

        private Lookup_ExistingCategoriesRepository _lookupExistingCategoriesRepository;
        private MasterEhopOptionTypeRepository _masterEhopOptionType;
        private OSubCategoryFeesRepository _oSubCategoryFeesRepository;
        private MasterCountryRepository _masterCountryRepository;
        private MasterStateRepository _masterStateRepository;
      private  SubmissionLicenseNumberCounterRepository _submissionLicenseNumberCounterRepository;
      private MasterBblApplicationStatusRepository _masterBblApplicationStatusRepository;
        private SubmissionCorporationAgentAddressRepository _submissionCorporationAgentAddressRepository;
        private MasterSubCategoryRepository _masterSubCategoryRepository;
        private FeeCodeMapRepository _feeCodeMapRepository;
        private SubmissionMasterRenewalRepository _submissionMasterRenewalRepository;
        private DCBC_ENTITY_BBL_Renewal_InvoiceRepository _dcbcEntityBblRenewalInvoiceRepository;
        private MasterCategoryQuestionRepository _masterCategoryQuestionRepository;
        private MasterRenewalStatusFeeRepository _masterRenewalStatusFeeRepository;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _masterCategoryQuestionRepository=new MasterCategoryQuestionRepository(_testUnitOfWork);
            _masterRenewalStatusFeeRepository=new MasterRenewalStatusFeeRepository(_testUnitOfWork);
          //  masterLicenseFeinRenewal = new MasterLicenseFEINRenewal(_testUnitOfWork);
            _submissionLicenseNumberCounterRepository = new SubmissionLicenseNumberCounterRepository(_testUnitOfWork);
            //StreetTypesRepository Initialization
            _streetTypesRepository = new StreetTypesRepository(_testUnitOfWork);
            _userBblServiceRepository=new UserBBLServiceRepository(_testUnitOfWork);
            _masterRegisterAgentRepository=new MasterRegisterAgentRepository(_testUnitOfWork);
            _corpRespository=new CorpRespository(_testUnitOfWork);
            _fixFeeRepository=new FixFeeRepository(_testUnitOfWork);
            _submissionMasterRenewalRepository = new SubmissionMasterRenewalRepository(_testUnitOfWork, _masterRenewalStatusFeeRepository);
            _submissionDocumentToAccelaRepository=new SubmissionDocumentToAccelaRepository(_testUnitOfWork);
            _submissionIndividualRepository=new SubmissionIndividualRepository(_testUnitOfWork,_submissionMasterRepository);
            _dcbcEntityBblRenewalsRepository=new DCBC_ENTITY_BBL_RenewalsRepository(_testUnitOfWork);
            _submissionCorporationAgentAddressRepository=new SubmissionCorporationAgentAddressRepository(_testUnitOfWork,_submissionMasterApplicationChcekListRepository,_submissionMasterRepository);
            //MastereHopEligibilityRepository Initialization
            _mastereHopEligibilityRepository = new MastereHopEligibilityRepository(_testUnitOfWork);
            _submissionCategoryRepository=new SubmissionCategoryRepository(_testUnitOfWork,_masterPrimaryCategoryRepository,_masterSecondaryLicenseCategoryRepository,
                _oSubCategoryFeesRepository, _fixFeeRepository, _submissionQuestionRepository, _masterSubCategoryRepository,
                _masterCategoryPhysicalLocationRepository, _feeCodeMapRepository,_submissionMasterApplicationChcekListRepository,
                _submissionMasterRenewalRepository, _dcbcEntityBblRenewalInvoiceRepository,_lookupExistingCategoriesRepository);
            _masterCategoryDocumentRepository=new MasterCategoryDocumentRepository(_testUnitOfWork);
            _masterBblApplicationStatusRepository=new MasterBblApplicationStatusRepository(_testUnitOfWork);
            //Lookup_ExistingCategoriesRepository Initialization
            _lookupExistingCategoriesRepository = new Lookup_ExistingCategoriesRepository(_testUnitOfWork);
            _dcbcEntityBblRenewalInvoiceRepository = new DCBC_ENTITY_BBL_Renewal_InvoiceRepository(_testUnitOfWork, _lookupExistingCategoriesRepository);
            _masterCategoryPhysicalLocationRepository = new MasterCategoryPhysicalLocationRepository(_testUnitOfWork, _masterPrimaryCategoryRepository, _masterCategoryQuestionRepository,
              _oSubCategoryFeesRepository, _masterSecondaryLicenseCategoryRepository);
            _feeCodeMapRepository = new FeeCodeMapRepository(_testUnitOfWork, _oSubCategoryFeesRepository);
            _masterSecondaryLicenseCategoryRepository=new MasterSecondaryLicenseCategoryRepository(_testUnitOfWork);
            _submissionBblAssociationToUsersRepository=new SubmissionBblAssociationToUsersRepository(_testUnitOfWork);
            //SubmissionMasterApplicationChcekListRepository Initialization
            _submissionMasterApplicationChcekListRepository = new SubmissionMasterApplicationChcekListRepository(_testUnitOfWork);
            _oSubCategoryFeesRepository = new OSubCategoryFeesRepository(_testUnitOfWork, _masterPrimaryCategoryRepository, _masterSecondaryLicenseCategoryRepository);
            _masterSubCategoryRepository = new MasterSubCategoryRepository(_testUnitOfWork, _masterPrimaryCategoryRepository, _masterSecondaryLicenseCategoryRepository);
            //MasterEhopOptionTypeRepository Initialization
            _masterEhopOptionType = new MasterEhopOptionTypeRepository(_testUnitOfWork);
            _submissionToAccelaRepository=new SubmissionToAccelaRepository(_testUnitOfWork);
            //MasterCountryRepository Initialization
            _masterCountryRepository = new MasterCountryRepository(_testUnitOfWork);
            _submissionQuestionRepository=new SubmissionQuestionRepository(_testUnitOfWork);
            //MasterStateRepository Initialization
            _masterStateRepository = new MasterStateRepository(_testUnitOfWork);
            _bblRepository=new BblRepository(_testUnitOfWork);
            _masterPrimaryCategoryRepository=new MasterPrimaryCategoryRepository(_testUnitOfWork,_masterSecondaryLicenseCategoryRepository,_masterCategoryDocumentRepository,
                _masterCategoryQuestionRepository);
            _masterBusinessActivityRepository=new MasterBusinessActivityRepository(_testUnitOfWork,_masterPrimaryCategoryRepository);
            _submissionCorporationRepository = new SubmissionCorporationRepository(_testUnitOfWork, _submissionCorporationAgentAddressRepository,
                _submissionMasterApplicationChcekListRepository,_masterRegisterAgentRepository,_submissionQuestionRepository,_corpRespository,
                _submissionMasterRepository,_masterCountryRepository,_masterStateRepository);

            //SubmissionDocumentRepository Initialization
            _submissionDocumentRepository = new SubmissionDocumentRepository(_testUnitOfWork,
                _submissionCategoryRepository,
                _masterCategoryDocumentRepository,
                _masterPrimaryCategoryRepository,
                _masterSecondaryLicenseCategoryRepository, _submissionMasterRepository,
                _masterCategoryPhysicalLocationRepository, _submissionMasterApplicationChcekListRepository,
                _submissionIndividualRepository, _submissionDocumentToAccelaRepository);

            //SubmissionMasterRepository Initialization
            _submissionMasterRepository = new SubmissionMasterRepository(_testUnitOfWork, _submissionCategoryRepository,
            _bblRepository,
            _userBblServiceRepository,
            _submissionQuestionRepository,
            _masterCategoryPhysicalLocationRepository,
            _masterPrimaryCategoryRepository,
            _submissionMasterApplicationChcekListRepository,
            _userRepository,
            _dcbcEntityBblRenewalsRepository,
            _masterBusinessActivityRepository,
            _submissionBblAssociationToUsersRepository,
            _submissionToAccelaRepository,
            _masterSecondaryLicenseCategoryRepository, _lookupExistingCategoriesRepository, _submissionLicenseNumberCounterRepository, _masterBblApplicationStatusRepository);

            //SubmissionCofoHopeHopAddressRepository Initialization
            _submissionCofoHopeHopAddressRepository = new SubmissionCofoHopeHopAddressRepository(_testUnitOfWork, _corpRespository,
            _submissionMasterRepository,
            _streetTypesRepository,
            _masterRegisterAgentRepository,
            _submissionCorporationRepository,
            _submissionQuestionRepository,
            _submissionMasterApplicationChcekListRepository, _masterStateRepository, _masterCountryRepository);

            //SubmissionCofoHopeHopRepository Initialization
            _submissionCofoHopeHopRepository = new SubmissionCofoHopeHopRepository(_testUnitOfWork,
            _submissionCofoHopeHopAddressRepository,
            _submissionMasterApplicationChcekListRepository,
            _submissionMasterRepository,
            _streetTypesRepository,
            _fixFeeRepository,
            _submissionDocumentRepository);

           
            _userRoleRepository = new UserRoleRepository(_testUnitOfWork);
            _userRepository = new UserRepository(_testUnitOfWork, _userRoleRepository);
            //SubmissionEHOPEligibilityRepository Initialization
            _submissionEhopEligibilityRepository = new SubmissionEHOPEligibilityRepository(_testUnitOfWork,
                _mastereHopEligibilityRepository,
                _submissionMasterApplicationChcekListRepository,_userRepository,
               _submissionCofoHopeHopRepository, _submissionDocumentRepository, _masterEhopOptionType);

            //_mockEHOPEligibilityData = new SubmissionEHOPEligibilityRepositoryData();
            //_mockMastereHopEligibilityData = new MastereHopEligibilityRepositoryData();
            //_mockApplicationChcekListData = new SubmissionMasterApplicationChcekListRepositoryData();
            //_mocksubmissionCofoHopeHopData = new SubmissionCofoHopeHopData();
            //_mocksubmissionCofoHopeHopAddressData = new SubmissionCofoHopeHopAddressData();
            //_mocksubmissionMasterData = new SubmissionMasterRepositoryData();
            var userdata = new UserRepositoryData();
            _testDbContext.User.AddRange(userdata.UsersEntitiesList);
            _testDbContext.SaveChanges();
            //Setup  MastereHOPEligibility FackData Initialization
            var eHopoptiontypeRepositoryData = new MasterEhopOptionTypeRepositoryData();
            _testDbContext.MasterEhopOptionType.AddRange(eHopoptiontypeRepositoryData.MasterEhopOptionTypeEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  MastereHOPEligibility FackData Initialization
            var eHopEligibilityRepositoryData = new MastereHopEligibilityRepositoryData();
            _testDbContext.MastereHOPEligibility.AddRange(eHopEligibilityRepositoryData.MastereHOPEligibilityEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  SubmissionEHOPEligibility FackData Initialization
            var testData = new SubmissionEHOPEligibilityRepositoryData();
            _testDbContext.SubmissionEHOPEligibility.AddRange(testData.SubmissionEHOPEligibilityEntitiesList);
            _testDbContext.SaveChanges();


            //Setup  SubmissionMaster_ApplicationCheckList FackData Initialization
            var chcekListRepositoryData = new SubmissionMasterApplicationChcekListRepositoryData();
            _testDbContext.SubmissionMaster_ApplicationCheckList.AddRange(chcekListRepositoryData.SubmissionMaster_ApplicationCheckListEntitiesList);
            _testDbContext.SaveChanges();


            //Setup  SubmissionCofo_Hop_Ehop FackData Initialization
            var cofoHopeHopData = new SubmissionCofoHopeHopData();
            _testDbContext.SubmissionCofo_Hop_Ehop.AddRange(cofoHopeHopData.SubmissionCofoHopEhopList);
            _testDbContext.SaveChanges();

            //Setup  SubmissionCofo_Hop_Ehop_Address FackData Initialization
            var cofoHopeHopAddressData = new SubmissionCofoHopeHopAddressData();
            _testDbContext.SubmissionCofo_Hop_Ehop_Address.AddRange(cofoHopeHopAddressData.SubmissionCofoHopEhopAddressList);
            _testDbContext.SaveChanges();

            //Setup  SubmissionMaster FackData Initialization
            var submissionMasterData = new SubmissionMasterRepositoryData();
            _testDbContext.SubmissionMaster.AddRange(submissionMasterData.SubmissionMasterEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  SubmissionDocument FackData Initialization
            var submissiondocumentdata = new SubmissionDocumentRepositoryData();
            _testDbContext.SubmissionDocument.AddRange(submissiondocumentdata.SubmissionDocumentList);
            _testDbContext.SaveChanges();

            //Setup  StreetTypes FackData Initialization
            var streetTypesData = new StreetTypesRepositoryData();
            _testDbContext.StreetTypes.AddRange(streetTypesData.ListAll);
            _testDbContext.SaveChanges();
        }
        [TestMethod]
        public void MasterHopEligibility_Returns_True_Test()
        {
            //Initialize Entites
            var ehopModelEntites = new EhopModel { MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40" };

            //Act 
            var ehopRows = _submissionEhopEligibilityRepository.MasterHopEligibility(ehopModelEntites);

            //Assert
            Assert.IsNotNull(ehopRows); // Test if null
            Assert.IsTrue(ehopRows.CheckList.Count() == 4);
            Assert.IsTrue(ehopRows.IsChecked == true);
        }
        [TestMethod]
        public void MasterHopEligibility_Returns_False_Test()
        {
            //Initialize Entites
            var ehopModelEntites = new EhopModel { MasterId = "dde0b056-d2a3-485e-a02a-e34c957c4e40" };

            //Act 
            var ehopRows = _submissionEhopEligibilityRepository.MasterHopEligibility(ehopModelEntites);

            //Assert
            Assert.IsNotNull(ehopRows); // Test if null
            Assert.IsTrue(ehopRows.CheckList.Count() == 4);
            Assert.IsTrue(ehopRows.IsChecked == false);
        }
        [TestMethod]
        public void ValidateEhopEligibility_Returns_Test()
        {
            //Initialize Entites
            var ehopModelEntites = new EhopModel { MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40" };

            //Act 
            var ehopRows = _submissionEhopEligibilityRepository.ValidateEhopEligibility(ehopModelEntites);

            //Assert
            Assert.IsNotNull(ehopRows); // Test if null
            Assert.IsTrue(ehopRows == 1);
        }
        [TestMethod]
        public void InsertEHopEligibility_Returns_Test()
        {
            //Initialize Entites
            var eligibilityModelEntites = new EligibilityModel { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97", EhopIds = "1,2,3,4,5,6,7,8,9,10,11,12", TypeId = 1 };

            //Act 
            var ehopRows = _submissionEhopEligibilityRepository.InsertEHopEligibility(eligibilityModelEntites);

            //Assert
            Assert.IsNotNull(ehopRows); // Test if null
            Assert.IsTrue(ehopRows == true);
        }
        [TestMethod]
        public void UpdateEHopEligibility_Returns_Test()
        {
            //Initialize Entites
            var eligibilityModelEntites = new EligibilityModel { MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40", EhopIds = "1,2,3,4,5,6,7,8,9,10,11,12", TypeId = 1 };

            //Act 
            var ehopRows = _submissionEhopEligibilityRepository.InsertEHopEligibility(eligibilityModelEntites);

            //Assert
            Assert.IsNotNull(ehopRows); // Test if null
            Assert.IsTrue(ehopRows == true);
        }
        [TestMethod]
        public void EhopData_Returns_Test()
        {
            //Initialize Entites
            var ehopDataEntity = new EhopData { MasterID = "cce0b056-d2a3-485e-a02a-e34c957c4e40"};

            //Act 
            var ehopRows = _submissionEhopEligibilityRepository.EhopData(ehopDataEntity);

            //Assert
            Assert.IsNotNull(ehopRows); // Test if null
        
        }
    }
}
