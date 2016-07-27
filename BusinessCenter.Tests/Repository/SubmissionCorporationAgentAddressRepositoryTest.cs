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
    public class SubmissionCorporationAgentAddressRepositoryTest
    {
        //Initialization DBConnection
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Declaration  of Repositories
        private SubmissionCorporationAgentAddressRepository _submissionCorporationAgentAddressRepository;
        private SubmissionMasterApplicationChcekListRepository _submissionMasterApplicationChcekListRepository;
        private SubmissionMasterRepository _submissionMasterRepository;
        private SubmissionCategoryRepository _submissionCategoryRepository;
        private UserBBLServiceRepository _userBblServiceRepository;
        private SubmissionQuestionRepository _submissionQuestionRepository;
        private BblRepository _bblRepository;
        private MasterCategoryPhysicalLocationRepository _masterCategoryPhysicalLocationRepository;
        private MasterPrimaryCategoryRepository _masterPrimaryCategoryRepository;
        private UserRepository _userRepository;
        private MasterBusinessActivityRepository _masterBusinessActivityRepository;
        private DCBC_ENTITY_BBL_RenewalsRepository _dcbcEntityBblRenewalsRepository;
        private SubmissionBblAssociationToUsersRepository _submissionBblAssociationToUsersRepository;
        private SubmissionToAccelaRepository _submissionToAccelaRepository;
        private MasterSecondaryLicenseCategoryRepository _masterSecondaryLicenseCategoryRepository;
        private Lookup_ExistingCategoriesRepository _lookupExistingCategoriesRepository;
    //    private MasterLicenseFEINRenewal masterLicenseFeinRenewal;
        private SubmissionLicenseNumberCounterRepository _submissionLicenseNumberCounterRepository;
        private MasterBblApplicationStatusRepository _masterBblApplicationStatusRepository;
        private OSubCategoryFeesRepository _oSubCategoryFeesRepository;
        private FixFeeRepository _fixFeeRepository;
        private MasterSubCategoryRepository _masterSubCategoryRepository;
        private FeeCodeMapRepository _feeCodeMapRepository;
        private SubmissionMasterRenewalRepository _submissionMasterRenewalRepository;
        private DCBC_ENTITY_BBL_Renewal_InvoiceRepository _dcbcEntityBblRenewalInvoiceRepository;
        private MasterCategoryQuestionRepository _masterCategoryQuestionRepository;
        private MasterCategoryDocumentRepository _masterCategoryDocumentRepository;
        private UserRoleRepository _userRoleRepository;
        private MasterRenewalStatusFeeRepository _masterRenewalStatusFeeRepository;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            //   masterLicenseFeinRenewal=new MasterLicenseFEINRenewal(_testUnitOfWork); masterLicenseFeinRenewal
            //SubmissionMasterApplicationChcekListRepository Initialization
            _submissionMasterApplicationChcekListRepository = new SubmissionMasterApplicationChcekListRepository(_testUnitOfWork);
            _submissionLicenseNumberCounterRepository = new SubmissionLicenseNumberCounterRepository(_testUnitOfWork);
            _userBblServiceRepository=new UserBBLServiceRepository(_testUnitOfWork);
            _userRoleRepository = new UserRoleRepository(_testUnitOfWork);
            _fixFeeRepository = new FixFeeRepository(_testUnitOfWork);
            _masterCategoryDocumentRepository = new MasterCategoryDocumentRepository(_testUnitOfWork);
            _masterCategoryQuestionRepository = new MasterCategoryQuestionRepository(_testUnitOfWork);
            _submissionQuestionRepository = new SubmissionQuestionRepository(_testUnitOfWork);
            _bblRepository = new BblRepository(_testUnitOfWork);
            _masterBblApplicationStatusRepository = new MasterBblApplicationStatusRepository(_testUnitOfWork);
            _masterSecondaryLicenseCategoryRepository = new MasterSecondaryLicenseCategoryRepository(_testUnitOfWork);
            _masterRenewalStatusFeeRepository = new MasterRenewalStatusFeeRepository(_testUnitOfWork);
            //Lookup_ExistingCategoriesRepository Initialization
            _lookupExistingCategoriesRepository = new Lookup_ExistingCategoriesRepository(_testUnitOfWork);
            _dcbcEntityBblRenewalInvoiceRepository = new DCBC_ENTITY_BBL_Renewal_InvoiceRepository(_testUnitOfWork, _lookupExistingCategoriesRepository);
            _dcbcEntityBblRenewalsRepository = new DCBC_ENTITY_BBL_RenewalsRepository(_testUnitOfWork);
            _submissionBblAssociationToUsersRepository = new SubmissionBblAssociationToUsersRepository(_testUnitOfWork);
            _submissionToAccelaRepository = new SubmissionToAccelaRepository(_testUnitOfWork);
            _userRepository = new UserRepository(_testUnitOfWork, _userRoleRepository);
            _masterBusinessActivityRepository = new MasterBusinessActivityRepository(_testUnitOfWork, _masterPrimaryCategoryRepository);
            _submissionCategoryRepository=new SubmissionCategoryRepository(_testUnitOfWork,_masterPrimaryCategoryRepository,_masterSecondaryLicenseCategoryRepository,
                _oSubCategoryFeesRepository, _fixFeeRepository, _submissionQuestionRepository, _masterSubCategoryRepository,
                _masterCategoryPhysicalLocationRepository, _feeCodeMapRepository, _submissionMasterApplicationChcekListRepository, _submissionMasterRenewalRepository,
                _dcbcEntityBblRenewalInvoiceRepository,_lookupExistingCategoriesRepository);
            _feeCodeMapRepository = new FeeCodeMapRepository(_testUnitOfWork, _oSubCategoryFeesRepository);
            _submissionMasterRenewalRepository = new SubmissionMasterRenewalRepository(_testUnitOfWork, _masterRenewalStatusFeeRepository);
            _masterSubCategoryRepository = new MasterSubCategoryRepository(_testUnitOfWork, _masterPrimaryCategoryRepository, _masterSecondaryLicenseCategoryRepository);
            _oSubCategoryFeesRepository = new OSubCategoryFeesRepository(_testUnitOfWork, _masterPrimaryCategoryRepository, _masterSecondaryLicenseCategoryRepository);
            _masterCategoryPhysicalLocationRepository = new MasterCategoryPhysicalLocationRepository(_testUnitOfWork,
            _masterPrimaryCategoryRepository, _masterCategoryQuestionRepository, _oSubCategoryFeesRepository, _masterSecondaryLicenseCategoryRepository);
            //SubmissionMasterRepository Initialization
            _submissionMasterRepository = new SubmissionMasterRepository(_testUnitOfWork,
               _submissionCategoryRepository,
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
             _masterSecondaryLicenseCategoryRepository, _lookupExistingCategoriesRepository,  _submissionLicenseNumberCounterRepository,
               _masterBblApplicationStatusRepository);
            _masterPrimaryCategoryRepository = new MasterPrimaryCategoryRepository(_testUnitOfWork, _masterSecondaryLicenseCategoryRepository, _masterCategoryDocumentRepository, _masterCategoryQuestionRepository);
            //SubmissionCorporationAgentAddressRepository Initialization
            _submissionCorporationAgentAddressRepository = new SubmissionCorporationAgentAddressRepository(_testUnitOfWork,
           _submissionMasterApplicationChcekListRepository,
           _submissionMasterRepository
           );





            //Setup  SubmissionCorporation_Agent_Address FackData Initialization
            var corpAgentData = new SubmissionCorporationAgentAddressRepositoryData();
            _testDbContext.SubmissionCorporation_Agent_Address.AddRange(corpAgentData.SubmissionCorporationAgentAddressEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  SubmissionMaster_ApplicationCheckList FackData Initialization
            var appChecklistData = new SubmissionMasterApplicationChcekListRepositoryData();
            _testDbContext.SubmissionMaster_ApplicationCheckList.AddRange(appChecklistData.SubmissionMaster_ApplicationCheckListEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  SubmissionMaster FackData Initialization
            var submissionMasterData = new SubmissionMasterRepositoryData();
            _testDbContext.SubmissionMaster.AddRange(submissionMasterData.SubmissionMasterEntitiesList);
            _testDbContext.SaveChanges();
           
        }
        [TestMethod]
        public void FindById_Returns_Test()
        {
            //Initialize Entites
            var subCorpAgentModel = new SubmissionCorpAgentModel { SCAId = 1 };

            //Act 
            var submissionCorpAgentRows = _submissionCorporationAgentAddressRepository.FindById(subCorpAgentModel);

            //Assert
            Assert.IsNotNull(submissionCorpAgentRows); // Test if null
            Assert.IsTrue(submissionCorpAgentRows.Count() == 4);
        }
        [TestMethod]
        public void FindByTypewithMasterId_Returns_Test()
        {
         

            //Act 
            var submissionCorpAgentRows = _submissionCorporationAgentAddressRepository.FindByTypewithMasterId(1, "Y-CORPREG");

            //Assert
            Assert.IsNotNull(submissionCorpAgentRows); // Test if null
            Assert.IsTrue(submissionCorpAgentRows.Count() == 1);
        }
        [TestMethod]
        public void GetHeadQuarterAddress_Returns_Test()
        {
         

            //Act 
            var submissionCorpAgentRows = _submissionCorporationAgentAddressRepository.GetHeadQuarterAddress(1,"Y-CORPAGENT","C943266");

            //Assert
            Assert.IsNotNull(submissionCorpAgentRows); // Test if null
            Assert.IsTrue(submissionCorpAgentRows.Count() == 1);
        }
        [TestMethod]
        public void GetHeadQuarterAddress_Addresstype_NewMail_Return_Test()
        {


            //Act 
            var submissionCorpAgentRows = _submissionCorporationAgentAddressRepository.GetHeadQuarterAddress(1, "NEWMAIL", "C943266");

            //Assert
            Assert.IsNotNull(submissionCorpAgentRows); // Test if null
            Assert.IsTrue(submissionCorpAgentRows.Any());
        }
        [TestMethod]
        public void FindBySubID_Returns_Test()
        {
            

            //Act 
            var submissionCorpAgentRows = _submissionCorporationAgentAddressRepository.FindBySubID(1);

            //Assert
            Assert.IsNotNull(submissionCorpAgentRows); // Test if null
            Assert.IsTrue(submissionCorpAgentRows.Count() == 4);
        }
        [TestMethod]
        public void DeleteHqAddress_Returns_True_Test()
        {


            //Act 
            var submissionCorpAgentRows = _submissionCorporationAgentAddressRepository.DeleteHqAddress(1, "Y-CORPREG");

            //Assert
            Assert.IsNotNull(submissionCorpAgentRows); // Test if null
            Assert.IsTrue(submissionCorpAgentRows==true);
        }
        [TestMethod]
        public void DeleteHqAddress_Returns_False_Test()
        {


            //Act 
            var submissionCorpAgentRows = _submissionCorporationAgentAddressRepository.DeleteHqAddress(2, "Y-CORPREG");

            //Assert
            Assert.IsNotNull(submissionCorpAgentRows); // Test if null
            Assert.IsTrue(submissionCorpAgentRows == false);
        }
        [TestMethod]
        public void InsertCorporationDetails_With_NewDetails_Test()
        {
            //Initialize Entites
            var detailsModel = new GeneralBusiness
            {
                SubCorporationRegId=2, UserType = "Y-CORPREG",



                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                FileNumber = "C943266",
                BusinessName = "APROSERVE CORPORATION",
                FirstName = null,
                MiddleName = null,
                LastName = null,
                BusinessAddressLine1 = "12100 Wilshire Boulevard, Suite 1400",
                BusinessAddressLine2 = null,
                BusinessAddressLine3 = null,
                BusinessCity = "Los Angeles",
                BusinessState = "CA",
                BusinessCountry = null,
                Telphone = null,
                ZipCode = "90025",
                Email = null,
                HQStatus = "True",
                Quardrant = "",
                Unit = null,
                AddressNumber = null
            };

            //Act 
            var submissionCorpAgentRows = _submissionCorporationAgentAddressRepository.InsertCorporationDetails(2,detailsModel);

            //Assert
            Assert.IsNotNull(submissionCorpAgentRows); // Test if null
           Assert.IsTrue(submissionCorpAgentRows==true);
        }
        [TestMethod]
        public void UpdateCorporationDetails_FileNumber_NotNull_Return_Test()
        {
            //Initialize Entites
            var detailsModel = new GeneralBusiness
            {
                SubCorporationRegId = 1,
                UserType = "Y-CORPAGENT",



                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                FileNumber = "C943266",
                BusinessName = "APROSERVE CORPORATION",
                //FirstName = null,
                //MiddleName = null,
                //LastName = null,
                //BusinessAddressLine1 = "12100 Wilshire Boulevard, Suite 1400",
                //BusinessAddressLine2 = null,
                //BusinessAddressLine3 = null,
                //BusinessCity = "Los Angeles",
                //BusinessState = "CA",
                //BusinessCountry = null,
                //Telphone = null,
                //ZipCode = "90025",
                //Email = null,
                HQStatus = "False",
                CorpStatus="False",
                Quardrant = "",
                Unit = null,
                AddressNumber = null
            };

            //Act 
            var submissionCorpAgentRows = _submissionCorporationAgentAddressRepository.InsertCorporationDetails(1, detailsModel);

            //Assert
            Assert.IsNotNull(submissionCorpAgentRows); // Test if null
            Assert.IsTrue(submissionCorpAgentRows == true);
        }
        [TestMethod]
        public void UpdateCorporationDetails_FileNumber_as_Empty_Return_Test()
        {
            //Initialize Entites
            var detailsModel = new GeneralBusiness
            {
                SubCorporationRegId = 1,
                UserType = "Y-CORPREG",



                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                FileNumber = "",
                BusinessName = "APROSERVE CORPORATION",
                //FirstName = null,
                //MiddleName = null,
                //LastName = null,
                //BusinessAddressLine1 = "12100 Wilshire Boulevard, Suite 1400",
                //BusinessAddressLine2 = null,
                //BusinessAddressLine3 = null,
                //BusinessCity = "Los Angeles",
                //BusinessState = "CA",
                //BusinessCountry = null,
                //Telphone = null,
                //ZipCode = "90025",
                //Email = null,
                HQStatus = "False",
                CorpStatus = "False",
                Quardrant = "",
                Unit = null,
                AddressNumber = null
            };

            //Act 
            var submissionCorpAgentRows = _submissionCorporationAgentAddressRepository.InsertCorporationDetails(1, detailsModel);

            //Assert
            Assert.IsNotNull(submissionCorpAgentRows); // Test if null
            Assert.IsTrue(submissionCorpAgentRows == true);
        }
        [TestMethod]
        public void UpdateCorporationDetails_UserType_as_NEWMAIL_Return_Test()
        {
            //Initialize Entites
            var detailsModel = new GeneralBusiness
            {
                SubCorporationRegId = 1,
                UserType = "NEWMAIL",



                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                FileNumber = "",
                BusinessName = "APROSERVE CORPORATION",
                //FirstName = null,
                //MiddleName = null,
                //LastName = null,
                //BusinessAddressLine1 = "12100 Wilshire Boulevard, Suite 1400",
                //BusinessAddressLine2 = null,
                //BusinessAddressLine3 = null,
                //BusinessCity = "Los Angeles",
                //BusinessState = "CA",
                //BusinessCountry = null,
                //Telphone = null,
                //ZipCode = "90025",
                //Email = null,
                HQStatus = "False",
                CorpStatus = "False",
                Quardrant = "",
                Unit = null,
                AddressNumber = null
            };

            //Act 
            var submissionCorpAgentRows = _submissionCorporationAgentAddressRepository.InsertCorporationDetails(1, detailsModel);

            //Assert
            Assert.IsNotNull(submissionCorpAgentRows); // Test if null
            Assert.IsTrue(submissionCorpAgentRows == true);
        }
        [TestMethod]
        public void UpdateCorporationDetails_AddressType_as_CORPAGENT_Return_Test()
        {
            //Initialize Entites
            var detailsModel = new GeneralBusiness
            {
                SubCorporationRegId = 1,
                UserType = "CORPREG",



                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                FileNumber = "C943266",
                BusinessName = "APROSERVE CORPORATION",
                //FirstName = null,
                //MiddleName = null,
                //LastName = null,
                //BusinessAddressLine1 = "12100 Wilshire Boulevard, Suite 1400",
                //BusinessAddressLine2 = null,
                //BusinessAddressLine3 = null,
                //BusinessCity = "Los Angeles",
                //BusinessState = "CA",
                //BusinessCountry = null,
                //Telphone = null,
                //ZipCode = "90025",
                //Email = null,
                HQStatus = "False",
                CorpStatus = "False",
                Quardrant = "",
                Unit = null,
                AddressNumber = null
            };

            //Act 
            var submissionCorpAgentRows = _submissionCorporationAgentAddressRepository.InsertCorporationDetails(1, detailsModel);

            //Assert
            Assert.IsNotNull(submissionCorpAgentRows); // Test if null
            Assert.IsTrue(submissionCorpAgentRows == true);
        }

        [TestMethod]
        public void InsertCorporationDetails_With_NewDetailsnull_Test()
        {
            //Initialize Entites
            var detailsModel = new GeneralBusiness
            {
                SubCorporationRegId = 2,
                UserType = "Y-CORPREG",



                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                FileNumber = "C943266",
                //BusinessName = "APROSERVE CORPORATION",
                //FirstName = null,
                //MiddleName = null,
                //LastName = null,
                //BusinessAddressLine1 = "12100 Wilshire Boulevard, Suite 1400",
                //BusinessAddressLine2 = null,
                //BusinessAddressLine3 = null,
                //BusinessCity = "Los Angeles",
                //BusinessState = "CA",
                //BusinessCountry = null,
                //Telphone = null,
                //ZipCode = "90025",
                //Email = null,
                //HQStatus = "True",
                //Quardrant = "",
                //Unit = null,
                //AddressNumber = null
            };

            //Act 
            var submissionCorpAgentRows = _submissionCorporationAgentAddressRepository.InsertCorporationDetails(2, detailsModel);

            //Assert
            Assert.IsNotNull(submissionCorpAgentRows); // Test if null
            Assert.IsTrue(submissionCorpAgentRows == true);
        }

        [TestMethod]
        public void InsertCorporationDetails_With_NewDetails_Notset_Test()
        {
            //Initialize Entites
            var detailsModel = new GeneralBusiness
            {
                SubCorporationRegId = 2,
                UserType = "Y-CORPREG",



                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                FileNumber = "C943266",
                //BusinessName = "APROSERVE CORPORATION",
                //FirstName = null,
                //MiddleName = null,
                //LastName = null,
                //BusinessAddressLine1 = "12100 Wilshire Boulevard, Suite 1400",
                //BusinessAddressLine2 = null,
                //BusinessAddressLine3 = null,
                //BusinessCity = "Los Angeles",
                BusinessState = "NOTSET",
                BusinessCountry = "",
                //Telphone = null,
                //ZipCode = "90025",
                //Email = null,
                //HQStatus = "True",
                //Quardrant = "",
                //Unit = null,
                //AddressNumber = null
            };

            //Act 
            var submissionCorpAgentRows = _submissionCorporationAgentAddressRepository.InsertCorporationDetails(2, detailsModel);

            //Assert
            Assert.IsNotNull(submissionCorpAgentRows); // Test if null
            Assert.IsTrue(submissionCorpAgentRows == true);
        }

        [TestMethod]
        public void UpdateCorporationDetails_UserType_as_CORPREG_Return_Test()
        {
            //Initialize Entites
            var detailsModel = new GeneralBusiness
            {
                SubCorporationRegId = 1,
                UserType = "Y-CORPREG",



                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                FileNumber = "C943266",
                BusinessName = "APROSERVE CORPORATION",
                //FirstName = null,
                //MiddleName = null,
                //LastName = null,
                //BusinessAddressLine1 = "12100 Wilshire Boulevard, Suite 1400",
                //BusinessAddressLine2 = null,
                //BusinessAddressLine3 = null,
                //BusinessCity = "Los Angeles",
                //BusinessState = "CA",
                //BusinessCountry = null,
                //Telphone = null,
                //ZipCode = "90025",
                //Email = null,
                HQStatus = "False",
                CorpStatus = "False",
                Quardrant = "",
                Unit = null,
                AddressNumber = null
            };

            //Act 
            var submissionCorpAgentRows = _submissionCorporationAgentAddressRepository.InsertCorporationDetails(1, detailsModel);

            //Assert
            Assert.IsNotNull(submissionCorpAgentRows); // Test if null
            Assert.IsTrue(submissionCorpAgentRows == true);
        }
    }

}
