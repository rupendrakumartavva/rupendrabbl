using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Model;
using BusinessCenter.Tests.Setup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Repository
{
    [TestClass]
    public class CofoHopDetailsRepositoryTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;


        //Repository declaration
      //  private CofoHopDetailsRepository _mockCofoHopDetailsTestRepository;
        private StreetTypesRepository _mockStreetTypesTestRepository;
        private SubmissionCofoHopeHopRepository _mockSubmissionCofoHopeHopTestRepository;
        private SubmissionCofoHopeHopAddressRepository _mockSubmissionCofoHopeHopAddressTestRepository;
        private SubmissionMasterApplicationChcekListRepository _mocksubcheckTestRepository;
        private FixFeeRepository _mockfixfeeTestRepository;
        private SubmissionDocumentRepository _mocksubmissionDocumentTestRepository;
        private CorpRespository _mockcorptestRepository;
        private MasterRegisterAgentRepository _mockRegisterAgentTestRepository;
        private SubmissionCorporationRepository _mockCorporationTestRepository;
        private SubmissionQuestionRepository _mockSubmissionQuestionTestRepository;
        private UserRepository _userRepository;
        private UserRoleRepository _userRoleRepository;
        private UserBBLServiceRepository _userBblServiceRepository;
        private SubmissionToAccelaRepository _submissionToAccelaRepository;
        private SubmissionQuestionRepository _submissionQuestionRepository;
        private SubmissionMasterRepository _submissionMasterTestRepository;
        private SubmissionCategoryRepository _submissionCategoryRepository;
        private SubmissionBblAssociationToUsersRepository _submissionBblAssociationToUsersRepository;
        private MasterSecondaryLicenseCategoryRepository _masterSecondaryLicenseCategoryRepository;
        private DCBC_ENTITY_BBL_Renewal_InvoiceRepository dcbcEntitybblRenewalInvoiceRepository;
        private SubmissionMasterRenewalRepository _submissionMasterRenewalRepository;
        private OSubCategoryFeesRepository _oSubCategoryFeesRepository;
        private MasterSubCategoryRepository _masterSubCategoryRepository;
        private MasterPrimaryCategoryRepository _masterPrimaryCategoryRepository;
        private MasterCategoryPhysicalLocationRepository _masterCategoryPhysicalLocationRepository;
        private MasterBusinessActivityRepository _masterBusinessActivityRepository;
        private FixFeeRepository _fixedFeeTestRepository;
        private FeeCodeMapRepository _feeCodeMapRepository;
        private DCBC_ENTITY_BBL_RenewalsRepository _dcbcEntityBblRenewalsRepository;
        private Lookup_ExistingCategoriesRepository _Lookup_ExistingCategoriesRepository;
        private BblRepository _bblRepository;
      //  private MasterLicenseFEINRenewal masterLicenseFeinRenewal;

        protected MasterCountryRepository _MasterCountryRepository;
        protected MasterStateRepository _masterStateRepository;
        private SubmissionLicenseNumberCounterRepository _submissionlicencecounter;

        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
         //   masterLicenseFeinRenewal = new MasterLicenseFEINRenewal(_testUnitOfWork);

            _submissionlicencecounter = new SubmissionLicenseNumberCounterRepository(_testUnitOfWork);
            _MasterCountryRepository = new MasterCountryRepository(_testUnitOfWork);
            _masterStateRepository = new MasterStateRepository(_testUnitOfWork);

            _masterSecondaryLicenseCategoryRepository = new MasterSecondaryLicenseCategoryRepository(_testUnitOfWork);
            _submissionBblAssociationToUsersRepository = new SubmissionBblAssociationToUsersRepository(_testUnitOfWork);
            _mockStreetTypesTestRepository = new StreetTypesRepository(_testUnitOfWork);
            _submissionQuestionRepository = new SubmissionQuestionRepository(_testUnitOfWork);
            _Lookup_ExistingCategoriesRepository = new Lookup_ExistingCategoriesRepository(_testUnitOfWork);


            _mockSubmissionCofoHopeHopTestRepository = new SubmissionCofoHopeHopRepository(_testUnitOfWork, _mockSubmissionCofoHopeHopAddressTestRepository,
            _mocksubcheckTestRepository,
            _submissionMasterTestRepository,
              _mockStreetTypesTestRepository,
              _mockfixfeeTestRepository,
             _mocksubmissionDocumentTestRepository);
            _mockSubmissionCofoHopeHopAddressTestRepository = new SubmissionCofoHopeHopAddressRepository(_testUnitOfWork, _mockcorptestRepository,
               _submissionMasterTestRepository,
              _mockStreetTypesTestRepository,
             _mockRegisterAgentTestRepository,
             _mockCorporationTestRepository,
            _mockSubmissionQuestionTestRepository,
           _mocksubcheckTestRepository, _masterStateRepository, _MasterCountryRepository);
            _userBblServiceRepository = new UserBBLServiceRepository(_testUnitOfWork);
            _userRepository = new UserRepository(_testUnitOfWork, _userRoleRepository);
            _submissionToAccelaRepository = new SubmissionToAccelaRepository(_testUnitOfWork);
            _submissionCategoryRepository = new SubmissionCategoryRepository(_testUnitOfWork, _masterPrimaryCategoryRepository,
            _masterSecondaryLicenseCategoryRepository, _oSubCategoryFeesRepository, _fixedFeeTestRepository, _submissionQuestionRepository,
            _masterSubCategoryRepository, _masterCategoryPhysicalLocationRepository, _feeCodeMapRepository,
            _mocksubcheckTestRepository, _submissionMasterRenewalRepository, dcbcEntitybblRenewalInvoiceRepository, _Lookup_ExistingCategoriesRepository);
            _submissionMasterTestRepository = new SubmissionMasterRepository(_testUnitOfWork, _submissionCategoryRepository, _bblRepository,
            _userBblServiceRepository, _submissionQuestionRepository, _masterCategoryPhysicalLocationRepository, _masterPrimaryCategoryRepository,
            _mocksubcheckTestRepository, _userRepository, _dcbcEntityBblRenewalsRepository, _masterBusinessActivityRepository,
            _submissionBblAssociationToUsersRepository, _submissionToAccelaRepository, _masterSecondaryLicenseCategoryRepository,
            _Lookup_ExistingCategoriesRepository,  _submissionlicencecounter);
            //_mockCofoHopDetailsTestRepository = new CofoHopDetailsRepository(_testUnitOfWork, _mockStreetTypesTestRepository,
            //_mockSubmissionCofoHopeHopTestRepository,
            //_mockSubmissionCofoHopeHopAddressTestRepository, _submissionMasterTestRepository);

       

            //Setup Data
            var streetTypesData = new StreetTypesRepositoryData();
            _testDbContext.StreetTypes.AddRange(streetTypesData.ListAll);
            _testDbContext.SaveChanges();

         
            var cofoHopeHopData = new SubmissionCofoHopeHopData();
            _testDbContext.SubmissionCofo_Hop_Ehop.AddRange(cofoHopeHopData.SubmissionCofoHopEhopList);
            _testDbContext.SaveChanges();

            var cofoHopeHopAddressData = new SubmissionCofoHopeHopAddressData();
            _testDbContext.SubmissionCofo_Hop_Ehop_Address.AddRange(cofoHopeHopAddressData.SubmissionCofoHopEhopAddressList);
            _testDbContext.SaveChanges();

            var masterdata = new SubmissionMasterRepositoryData();
            _testDbContext.SubmissionMaster.AddRange(masterdata.SubmissionMasterEntitiesList);
            _testDbContext.SaveChanges();


        }
        [TestMethod]
        public void GetSubmissionCofoOrHopDetails_Returns_Test()
        {
           

            //Act 
            var cofoHopRows = _mockCofoHopDetailsTestRepository.GetSubmissionCofoOrHopDetails("cce0b056-d2a3-485e-a02a-e34c957c4e40");

            //Assert
            Assert.IsNotNull(cofoHopRows); // Test if null
            Assert.IsTrue(cofoHopRows.Count() == 1);
        }
        [TestMethod]
        public void DropDownBind_Returns_Test()
        {


            //Act 
            var cofoHopRows = _mockCofoHopDetailsTestRepository.DropDownBind();

            //Assert
            Assert.IsNotNull(cofoHopRows); // Test if null
            Assert.IsTrue(cofoHopRows.Count() == 2);
        }
       
    }
}
