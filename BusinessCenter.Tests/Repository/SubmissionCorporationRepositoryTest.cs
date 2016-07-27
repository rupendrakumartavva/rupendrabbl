using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Model;
using BusinessCenter.Tests.Setup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Common;
using System.Linq;

namespace BusinessCenter.Tests.Repository
{
    [TestClass]
    public class SubmissionCorporationRepositoryTest
    {
        //Initialization DBConnection
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Declaration  of Repositories
        private SubmissionCorporationRepository _submissionCorporationRepository;
        private SubmissionCorporationAgentAddressRepository _submissionCorporationAgentAddressRepository;
        private SubmissionMasterApplicationChcekListRepository _submissionMasterApplicationChcekListRepository;
        private MasterRegisterAgentRepository _masterRegisterAgentRepository;
        private SubmissionQuestionRepository _submissionQuestionRepository;
        private CorpRespository _corpRespository;
        private SubmissionMasterRepository _submissionMasterRepository;
        private SubmissionCategoryRepository _submissionCategoryRepository;
        private UserBBLServiceRepository _userBblServiceRepository;
        private BblRepository _bblRepository;
        private MasterCategoryPhysicalLocationRepository _masterCategoryPhysicalLocationRepository;
        private MasterPrimaryCategoryRepository _masterPrimaryCategoryRepository;
     //   private SubmissionMasterApplicationChcekListRepository _submissionMasterApplicationChcekListRepository;
        private UserRepository _userRepository;
        private MasterBusinessActivityRepository _masterBusinessActivityRepository;
        private DCBC_ENTITY_BBL_RenewalsRepository _dcbcEntityBblRenewalsRepository;
        private SubmissionBblAssociationToUsersRepository _submissionBblAssociationToUsersRepository;
        private SubmissionToAccelaRepository _submissionToAccelaRepository;
        private MasterSecondaryLicenseCategoryRepository _masterSecondaryLicenseCategoryRepository;
       // private SubmissionCorporationRepositoryData _submissionCorporationRepository;
      //  private SubmissionCorporationAgentAddressRepositoryData _submissionCorporationAgentAddressRepository;
     //   private SubmissionMasterApplicationChcekListRepositoryData _submissionMasterApplicationChcekListRepository;
//private MasterRegisterAgentData _mockMasterRegisterAgentData;
        //private SubmissionQuestionRepositoryData _mockSubmissionQuestiondata;
        //private CorpRepositoryData _mockCorpRepositoryData;
        //private SubmissionMasterRepositoryData _mockSubmissionMasterRepositoryData;
        private Lookup_ExistingCategoriesRepository _lookupExistingCategoriesRepository;
      //  private MasterLicenseFEINRenewal masterLicenseFeinRenewal;
        private SubmissionLicenseNumberCounterRepository _submissionLicenseNumberCounterRepository;
        private MasterCountryRepository _masterCountryRepository;
        private MasterStateRepository _masterStateRepository;
        private MasterBblApplicationStatusRepository _masterBblApplicationStatusRepository;
        private OSubCategoryFeesRepository _oSubCategoryFeesRepository;
        private FixFeeRepository _fixFeeRepository;
        private MasterSubCategoryRepository _masterSubCategoryRepository;
        private FeeCodeMapRepository _feeCodeMapRepository;
        private SubmissionMasterRenewalRepository _submissionMasterRenewalRepository;
        private DCBC_ENTITY_BBL_Renewal_InvoiceRepository _dcbcEntityBblRenewalInvoiceRepository;
        private MasterCategoryQuestionRepository _masterCategoryQuestionRepository;
        private MasterCategoryDocumentRepository _masterCategoryDocumentRepository;
        private MasterRenewalStatusFeeRepository _masterRenewalStatusFeeRepository;
        private UserRoleRepository _userRoleRepository;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _submissionLicenseNumberCounterRepository = new SubmissionLicenseNumberCounterRepository(_testUnitOfWork);
          //  masterLicenseFeinRenewal = new MasterLicenseFEINRenewal(_testUnitOfWork);
            //Lookup_ExistingCategoriesRepository Initialization
            _lookupExistingCategoriesRepository = new Lookup_ExistingCategoriesRepository(_testUnitOfWork);
            _masterCategoryQuestionRepository=new MasterCategoryQuestionRepository(_testUnitOfWork);
            _dcbcEntityBblRenewalInvoiceRepository=new DCBC_ENTITY_BBL_Renewal_InvoiceRepository(_testUnitOfWork,_lookupExistingCategoriesRepository);
            _masterBusinessActivityRepository=new MasterBusinessActivityRepository(_testUnitOfWork,_masterPrimaryCategoryRepository);
            _masterCountryRepository = new MasterCountryRepository(_testUnitOfWork);
            _masterStateRepository = new MasterStateRepository(_testUnitOfWork);
            _submissionToAccelaRepository=new SubmissionToAccelaRepository(_testUnitOfWork);
            _bblRepository=new BblRepository(_testUnitOfWork);
            _userRoleRepository=new UserRoleRepository(_testUnitOfWork);
            _masterRenewalStatusFeeRepository=new MasterRenewalStatusFeeRepository(_testUnitOfWork);
            _masterCategoryDocumentRepository=new MasterCategoryDocumentRepository(_testUnitOfWork);
            _submissionMasterRenewalRepository = new SubmissionMasterRenewalRepository(_testUnitOfWork, _masterRenewalStatusFeeRepository);
            _feeCodeMapRepository=new FeeCodeMapRepository(_testUnitOfWork,_oSubCategoryFeesRepository);
            _masterSecondaryLicenseCategoryRepository=new MasterSecondaryLicenseCategoryRepository(_testUnitOfWork);
            _dcbcEntityBblRenewalsRepository=new DCBC_ENTITY_BBL_RenewalsRepository(_testUnitOfWork);
            _submissionBblAssociationToUsersRepository=new SubmissionBblAssociationToUsersRepository(_testUnitOfWork);
            //SubmissionMasterApplicationChcekListRepository Initialization
            _submissionMasterApplicationChcekListRepository = new SubmissionMasterApplicationChcekListRepository(_testUnitOfWork);
            _userRepository = new UserRepository(_testUnitOfWork, _userRoleRepository);
            _masterPrimaryCategoryRepository = new MasterPrimaryCategoryRepository(_testUnitOfWork, _masterSecondaryLicenseCategoryRepository, _masterCategoryDocumentRepository,
                _masterCategoryQuestionRepository);
            //SubmissionCorporationAgentAddressRepository Initialization
            _submissionCorporationAgentAddressRepository = new SubmissionCorporationAgentAddressRepository(_testUnitOfWork,
                _submissionMasterApplicationChcekListRepository,
               _submissionMasterRepository);
            _masterSubCategoryRepository=new MasterSubCategoryRepository(_testUnitOfWork,_masterPrimaryCategoryRepository,_masterSecondaryLicenseCategoryRepository);
            _userBblServiceRepository=new UserBBLServiceRepository(_testUnitOfWork);
            //MasterRegisterAgentRepository Initialization
            _masterRegisterAgentRepository = new MasterRegisterAgentRepository(_testUnitOfWork);

            //SubmissionQuestionRepository Initialization
            _submissionQuestionRepository = new SubmissionQuestionRepository(_testUnitOfWork);
            _fixFeeRepository=new FixFeeRepository(_testUnitOfWork);
            //CorpRespository Initialization
            _corpRespository = new CorpRespository(_testUnitOfWork);
            _oSubCategoryFeesRepository=new OSubCategoryFeesRepository(_testUnitOfWork,_masterPrimaryCategoryRepository,_masterSecondaryLicenseCategoryRepository);
            _masterCategoryPhysicalLocationRepository = new MasterCategoryPhysicalLocationRepository(_testUnitOfWork, _masterPrimaryCategoryRepository, _masterCategoryQuestionRepository,
                _oSubCategoryFeesRepository,_masterSecondaryLicenseCategoryRepository);
            _masterBblApplicationStatusRepository=new MasterBblApplicationStatusRepository(_testUnitOfWork);
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

            //SubmissionCorporationRepository Initialization
            _submissionCorporationRepository = new SubmissionCorporationRepository(_testUnitOfWork,
                _submissionCorporationAgentAddressRepository,
                _submissionMasterApplicationChcekListRepository,
                _masterRegisterAgentRepository,
                _submissionQuestionRepository,
                _corpRespository,
                _submissionMasterRepository, _masterCountryRepository, _masterStateRepository
                );
            _submissionCategoryRepository=new SubmissionCategoryRepository(_testUnitOfWork,_masterPrimaryCategoryRepository,_masterSecondaryLicenseCategoryRepository,
                _oSubCategoryFeesRepository, _fixFeeRepository, _submissionQuestionRepository, _masterSubCategoryRepository,
                _masterCategoryPhysicalLocationRepository, _feeCodeMapRepository,_submissionMasterApplicationChcekListRepository,
                _submissionMasterRenewalRepository, _dcbcEntityBblRenewalInvoiceRepository,_lookupExistingCategoriesRepository);
            //Setup  SubmissionCorporation_Agent FackData Initialization
            var testData = new SubmissionCorporationRepositoryData();
            _testDbContext.SubmissionCorporation_Agent.AddRange(testData.SubmissionCorporationEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  SubmissionCorporation_Agent_Address FackData Initialization
            var CorporationAgentAddressData = new SubmissionCorporationAgentAddressRepositoryData();
            _testDbContext.SubmissionCorporation_Agent_Address.AddRange(CorporationAgentAddressData.SubmissionCorporationAgentAddressEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  SubmissionMaster_ApplicationCheckList FackData Initialization
            var ApplicationChcekListRepositoryData = new SubmissionMasterApplicationChcekListRepositoryData();
            _testDbContext.SubmissionMaster_ApplicationCheckList.AddRange(ApplicationChcekListRepositoryData.SubmissionMaster_ApplicationCheckListEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  MasterRegisteredAgent FackData Initialization
            //var RegisterAgentData = new MasterRegisterAgentData();
            //_testDbContext.MasterRegisteredAgent.AddRange(RegisterAgentData.MasterRegisteredAgentEntitiesList);
            //_testDbContext.SaveChanges();

            var SubmissionQuestionData = new SubmissionQuestionRepositoryData();
            _testDbContext.SubmissionQuestion.AddRange(SubmissionQuestionData.SubmissionQuestionEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  DCBC_ENTITY_CORP FackData Initialization
            var corpData = new CorpRepositoryData();
            _testDbContext.DCBC_ENTITY_CORP.AddRange(corpData.CorpEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  SubmissionMaster FackData Initialization
            var SubmissionMasterData = new SubmissionMasterRepositoryData();
            _testDbContext.SubmissionMaster.AddRange(SubmissionMasterData.SubmissionMasterEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  MasterCountry FackData Initialization
            var masterCountryData = new MasterCountryData();
            _testDbContext.MasterCountry.AddRange(masterCountryData.MasterCountryEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  MasterState FackData Initialization
            var masterStateData = new MasterStateRepositoryData();
            _testDbContext.MasterState.AddRange(masterStateData.MasterStateEntitiesList);
            _testDbContext.SaveChanges();
        }

        [TestMethod]
        public void GetAllCorporations_Return_Test()
        {
            //Act

            var corporationRows = _submissionCorporationRepository.GetAllCorporations().ToList();

            //Assert
            Assert.IsNotNull(corporationRows); // Test if null

            Assert.IsTrue(corporationRows.Count() == 3);
        }

        [TestMethod]
        public void FindByMasterId_Test()
        {
            //Initialize Entites
            var generalBusiness = new GeneralBusiness { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97" };

            //Act
            var corporationRows = _submissionCorporationRepository.FindByMasterId(generalBusiness);

            //Assert
            Assert.IsNotNull(corporationRows); // Test if null
            Assert.IsTrue(corporationRows.Count() == 1);
        }

        [TestMethod]
        public void FindById_Test()
        {
            //Act
            var corporationRows = _submissionCorporationRepository.FindById("8c6deecc-3b72-485d-8af5-30939af94e97");

            //Assert
            Assert.IsNotNull(corporationRows); // Test if null
            Assert.IsTrue(corporationRows.Count() == 1);
        }

        [TestMethod]
        public void GetCorpBusinessData_Returns_corpAddressDetails_Test()
        {
            //Initialize Entites
            var generalBusiness = new GeneralBusiness { MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40", FileNumber = "C943266" };

            //Act
            var corporationRows = _submissionCorporationRepository.GetCorpBusinessData(generalBusiness);

            //Assert
            Assert.IsNotNull(corporationRows); // Test if null
            Assert.IsTrue(corporationRows.Count() == 1);
        }
        [TestMethod]
        public void GetCorpBusinessData_Returns_corpAddressDetailsnulls_Test()
        {
            //Initialize Entites
            var generalBusiness = new GeneralBusiness { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97", FileNumber = "C943266", UserType = "CORPAGENT" };

            //Act
            var corporationRows = _submissionCorporationRepository.GetCorpBusinessData(generalBusiness);

            //Assert
            Assert.IsNotNull(corporationRows); // Test if null
            Assert.IsTrue(corporationRows.Count() == 1);
        }

        [TestMethod]
        public void GetCorpBusinessData_WithAllTheAddressDetails_Returns_corpAddressDetails_Test()
        {
            //Initialize Entites
            var generalBusiness = new GeneralBusiness
            {
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
                FileNumber = "C943266",
                UserType = "Y-CORPAGENT"
               
            };

            //Act
            var corporationRows = _submissionCorporationRepository.GetCorpBusinessData(generalBusiness);

            //Assert
            Assert.IsNotNull(corporationRows); // Test if null
            Assert.IsTrue(corporationRows.Count() == 1);
        }






        //[TestMethod]
        //public void GetCorpBusinessData_Returns_Test()
        //{
        //    //Initialize Entites
        //    var generalBusiness = new GeneralBusiness { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97", FileNumber = "C943266" };

        //    //Act
        //    var corporationRows = _submissionCorporationTestRepository.GetCorpBusinessData(generalBusiness);

        //    //Assert
        //    Assert.IsNotNull(corporationRows); // Test if null
        //    Assert.IsTrue(corporationRows.Count() == 1);
        //}
        [TestMethod]
        public void GetCorpBusinessData_Returns_Test()
        {
            //Initialize Entites
            var generalBusiness = new GeneralBusiness { MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c", FileNumber = "C943266" };

            //Act
            var corporationRows = _submissionCorporationRepository.GetCorpBusinessData(generalBusiness);

            //Assert
            Assert.IsNotNull(corporationRows); // Test if null
            Assert.IsTrue(corporationRows.Count() == 1);
        }

        [TestMethod]
        public void GetHQAddess_Returns__Test()
        {
            //Initialize Entites
            var generalBusiness = new GeneralBusiness { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97", UserType = "Y-CORPAGENT", FileNumber = "C943266" };

            //Act
            var corporationRows = _submissionCorporationRepository.GetHQAddess(generalBusiness);

            //Assert
            Assert.IsNotNull(corporationRows); // Test if null
            Assert.IsTrue(corporationRows.BusinessName.Trim() == "Auckland Park");
        }
        [TestMethod]
        public void GetHQAddess_Returns_Agent_Test()
        {
            //Initialize Entites
            var generalBusiness = new GeneralBusiness { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",  FileNumber = "C943266" };

            //Act
            var corporationRows = _submissionCorporationRepository.GetHQAddess(generalBusiness);

            //Assert
            Assert.IsNotNull(corporationRows); // Test if null
            Assert.IsTrue(corporationRows.FileNumber.Trim() == "C943266");
        }
        [TestMethod]
        public void FindByMasterwithFile_Test()
        {
            //Initialize Entites
            var generalBusiness = new GeneralBusiness { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97", FileNumber = "C943266" };

            //Act
            var corporationRows = _submissionCorporationRepository.FindByMasterwithFile(generalBusiness);

            //Assert
            Assert.IsNotNull(corporationRows); // Test if null
            Assert.IsTrue(corporationRows.Count() == 1);
        }

        [TestMethod]
        public void DeleteSubmissionCorp_Test()
        {
            //Act
            var corporationRows = _submissionCorporationRepository.DeleteSubmissionCorp("8c6deecc-3b72-485d-8af5-30939af94e97");

            //Assert
            Assert.IsNotNull(corporationRows); // Test if null
            Assert.IsTrue(corporationRows == true);
        }

        [TestMethod]
        public void GetCorpAgent_Returns_SubmissionCorporationAgentDetails_Test()
        {
            //Initialize Entites
            var generalBusiness = new GeneralBusiness { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97", FileNumber = "C943266" };

            //Act
            var corporationRows = _submissionCorporationRepository.GetCorpAgent(generalBusiness);

            //Assert
            Assert.IsNotNull(corporationRows); // Test if null
            Assert.IsTrue(corporationRows.Count() == 2);
        }

        [TestMethod]
        public void GetCorpAgent_Returns_MasterRegisterAgentDetails_Test()
        {
            //Initialize Entites

            var generalBusiness = new GeneralBusiness { MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c", FileNumber = "C880040" };

            //Act
            var corporationRows = _submissionCorporationRepository.GetCorpAgent(generalBusiness);

            //Assert
            Assert.IsNotNull(corporationRows); // Test if null
            Assert.IsTrue(corporationRows.Count() == 1);
        }

        [TestMethod]
        public void GetCorpAgent_Returns_MasterRegisterAgentDetailsAgent_Test()
        {
            //Initialize Entites

            var generalBusiness = new GeneralBusiness { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97", FileNumber = "C880040" };

            //Act
            var corporationRows = _submissionCorporationRepository.GetCorpAgent(generalBusiness);

            //Assert
            Assert.IsNotNull(corporationRows); // Test if null
            Assert.IsTrue(corporationRows.Count() == 2);
        }
        [TestMethod]
        public void CorporationStatus_PassingMaterId_returnStatusValue_Test()
        {
           
            //Act
            var corporationRows = _submissionCorporationRepository.CorpServiceStatus("SDE0CC3C-95EB-42AC-93B8-C7DD78B9399A");

            //Assert
            Assert.IsNotNull(corporationRows); // Test if null
            Assert.IsTrue(corporationRows.OriginalCorpStatus.ToUpper() == "ACTIVE");
        }

        [TestMethod]
        public void CorporationStatus_PassingMaterIdWith_CorpNumberREVOKED_returnStatusValue_Test()
        {

            //Act
            var corporationRows = _submissionCorporationRepository.CorpServiceStatus("33b635b9-fec2-4b4f-9d07-686b8d6cc60c");

            //Assert
            Assert.IsNotNull(corporationRows); // Test if null
            Assert.IsTrue(corporationRows.OriginalCorpStatus == "REVOKED");
        }
        [TestMethod]
        public void CorporationStatus_PassingMaterIdWith_CorpNumberActive_returnStatus_Test()
        {

            //Act
            var corporationRows = _submissionCorporationRepository.CorpServiceStatus("8c6deecc-3b72-485d-8af5-30939af94e97");

            //Assert
            Assert.IsNotNull(corporationRows); // Test if null
            Assert.IsTrue(corporationRows.OriginalCorpStatus == "Active");
        }

        [TestMethod]
        public void CorporationStatus_PassingMaterIdWith_CorpNumberREVOKED_returnStatus_Test()
        {

            //Act
            var corporationRows = _submissionCorporationRepository.CorpServiceStatus("cce0b056-d2a3-485e-a02a-e34c957c4e40");

            //Assert
            Assert.IsNotNull(corporationRows); // Test if null
            Assert.IsTrue(corporationRows.OriginalCorpStatus == "REVOKED");
        }
        //[TestMethod]
        //public void CorporationStatus_PassingMaterIdWith_CorpNumberREVOKED_returnCorpStatusChangedValue_Test()
        //{

        //    //Act
        //    var corporationRows = _submissionCorporationTestRepository.CorpServiceStatus("cce0b056-d2a3-485e-a02a-e34c957c4e40");

        //    //Assert
        //    Assert.IsNotNull(corporationRows); // Test if null
        //    Assert.IsTrue(corporationRows.OriginalCorpStatus == "REVOKED");//cce0b056-d2a3-485e-a02a-e34c957c4e40
        //}

          [TestMethod]
        public void DeleteSubmissionCorpEmpty_PassingMaterIdWith_CorpNumberREVOKED_returnStatusValue_Test()
        {

            //Act
            var corporationRows = _submissionCorporationRepository.DeleteSubmissionCorpEmpty("8c6deecc-3b72-485d-8af5-30939af94e97", "Y-CORPREG");

            //Assert
            Assert.IsNotNull(corporationRows); // Test if null
            Assert.IsFalse(corporationRows);
        }

          [TestMethod]
          public void DeleteSubmissionCorp_WithAgent_PassingMaterIdWith_CorpNumberREVOKED_returnStatusValue_Test()
          {

              //Act
              var corporationRows = _submissionCorporationRepository.DeleteSubmissionCorpEmpty("8c6deecc-3b72-485d-8af5-30939af94e97", "N-CORPAGENT");

              //Assert
              Assert.IsNotNull(corporationRows); // Test if null
              Assert.IsFalse(corporationRows);
          }
        //HQ ADDRESS

          [TestMethod]
          public void DeleteSubmissionCorp_WithHqAddress_PassingMaterIdWith_CorpNumberREVOKED_returnStatusValue_Test()
          {

              //Act
              var corporationRows = _submissionCorporationRepository.DeleteSubmissionCorpEmpty("8c6deecc-3b72-485d-8af5-30939af94e97", "HQ ADDRESS");

              //Assert
              Assert.IsNotNull(corporationRows); // Test if null
              Assert.IsTrue(corporationRows==false);
          }
          [TestMethod]
          public void DeleteSubmissionCorp_WithNCORPREG_PassingMaterIdWith_CorpNumberREVOKED_returnStatusValue_Test()
          {

              //Act
              var corporationRows = _submissionCorporationRepository.DeleteSubmissionCorpEmpty("8c6deecc-3b72-485d-8af5-30939af94e97", "N-CORPREG");

              //Assert
              Assert.IsNotNull(corporationRows); // Test if null
              Assert.IsTrue(corporationRows == false);
          }
          [TestMethod]
          public void InsertCorporationDetails_WithHqAddress_returnStatusValue_Test()
          {

              var insertCorpDetails = new GeneralBusiness
              {
                 // SubCorporationRegId = 0,
                  MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
                  FileNumber = "C880040",
                  BusinessName = "APROSERVE CORPORATION",
                  TradeName = "",
                  BusinessStructure = "Corporation (Non-Profit)",
                  UserType  = "Y-CORPAGENT",
              
                  FirstName = "Auckland Park",
                  MiddleName = "Peter",
                  LastName = "Thiel",
                  BusinessAddressLine1 = "Auckland Park",
                  BusinessAddressLine2 = "NEW YORK",
                  BusinessAddressLine3 = "AVENUE",
                  BusinessCity = "San Jose",
                  BusinessState = "California",
                  BusinessCountry = "United States",
                  Telphone = "123456789",
                  ZipCode = "20008",
                  Email = "",
                  CorpStatus = "True",
                  Quardrant = "SW",
                  Unit = "1",
                  AddressNumber = "5225",
                  HQStatus = "True"
              };


              //Act
              var corporationRows = _submissionCorporationRepository.InsertCorporationDetails(insertCorpDetails);

              //Assert
              Assert.IsNotNull(corporationRows); // Test if null
              Assert.IsTrue(corporationRows);
          }

          [TestMethod]
          public void InsertCorporationDetails_returnStatusValue_Test()
          {

              var insertCorpDetails = new GeneralBusiness
              {
                  // SubCorporationRegId = 0,
                  MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
                 FileNumber = "C880040",
                  BusinessName = "APROSERVE CORPORATION",
                  TradeName = "",
                  BusinessStructure = "Corporation (Non-Profit)",
                  UserType = "Y-CORPAGENT",

                  FirstName = "Auckland Park",
                  MiddleName = "Peter",
                  LastName = "Thiel",
                  BusinessAddressLine1 = "Auckland Park",
                  BusinessAddressLine2 = "NEW YORK",
                  BusinessAddressLine3 = "AVENUE",
                  BusinessCity = "San Jose",
                  BusinessState = "California",
                  BusinessCountry = "United States",
                  Telphone = "123456789",
                  ZipCode = "20008",
                  Email = "",
                  CorpStatus = "True",
                  Quardrant = "SW",
                  Unit = "1",
                  AddressNumber = "5225",
                  HQStatus = "True"
              };


              //Act
              var corporationRows = _submissionCorporationRepository.InsertCorporationDetails(insertCorpDetails);

              //Assert
              Assert.IsNotNull(corporationRows); // Test if null
              Assert.IsTrue(corporationRows);
          }
          [TestMethod]
          public void UpdateCorporationDetails_WithHqAddress_returnStatusValue_Test()
          {

              var insertCorpDetails = new GeneralBusiness
              {
                  // SubCorporationRegId = 0,
                  MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
                  FileNumber = "C943266",
                  BusinessName = "APROSERVE CORPORATION",
                  TradeName = "",
                  BusinessStructure = "Corporation (Non-Profit)",
                  UserType = "Y-CORPAGENT",

                  FirstName = "Auckland Park",
                  MiddleName = "Peter",
                  LastName = "Thiel",
                  BusinessAddressLine1 = "Auckland Park",
                  BusinessAddressLine2 = "NEW YORK",
                  BusinessAddressLine3 = "AVENUE",
                  BusinessCity = "San Jose",
                  BusinessState = "California",
                  BusinessCountry = "United States",
                  Telphone = "123456789",
                  ZipCode = "20008",
                  Email = "",
                  CorpStatus = "True",
                  Quardrant = "SW",
                  Unit = "1",
                  AddressNumber = "5225",
                  HQStatus = "True"
              };


              //Act
              var corporationRows = _submissionCorporationRepository.InsertCorporationDetails(insertCorpDetails);

              //Assert
              Assert.IsNotNull(corporationRows); // Test if null
              Assert.IsTrue(corporationRows);
          }
          [TestMethod]
          public void UpdateCorportationData_Test()
          {

              var generalBusiness = new GeneralBusiness
              {
                  // SubCorporationRegId = 0,
                  MasterId = "4cb3a7f0-8e98-4b8d-86ae-a2d9dbe14959",
                  FileNumber = "C880040",
                  BusinessName = "APROSERVE CORPORATION",
                  TradeName = "",
                  BusinessStructure = "Corporation (Non-Profit)",
                  UserType = "Y-CORPAGENT",

                  FirstName = "Auckland Park",
                  MiddleName = "Peter",
                  LastName = "Thiel",
                  BusinessAddressLine1 = "Auckland Park",
                  BusinessAddressLine2 = "NEW YORK",
                  BusinessAddressLine3 = "AVENUE",
                  BusinessCity = "San Jose",
                  BusinessState = "California",
                  BusinessCountry = "United States",
                  Telphone = "123456789",
                  ZipCode = "20008",
                  Email = "",
                  CorpStatus = "True",
                  Quardrant = "SW",
                  Unit = "1",
                  AddressNumber = "5225",
                  HQStatus = "True"
              };


              //Act
              var corporationRows = _submissionCorporationRepository.UpdateCorportationData(generalBusiness);

              //Assert
              Assert.IsNotNull(corporationRows); // Test if null
              Assert.IsTrue(corporationRows==false);
          }
          [TestMethod]
          public void CorpOnlineSearch_Test()
          {
              //Initialize Entites
              var corporationdetails = new CorporationDetails { FileNumber = "C880040" };

              //Act
              var corporationRows = _submissionCorporationRepository.CorpOnlineSearch(corporationdetails);

              //Assert
              Assert.IsNotNull(corporationRows); // Test if null
              Assert.IsTrue(corporationRows.ToUpper() == "REVOKED");
          }
          [TestMethod]
          public void GetStateFullName_Test()
          {
            

              //Act
              var corporationRows = _submissionCorporationRepository.GetStateFullName("WA", "US");

              //Assert
              Assert.IsNotNull(corporationRows); // Test if null
              Assert.IsTrue(corporationRows.ToUpper() == "WASHINGTON");
          }
          [TestMethod]
          public void GetStateCode_Test()
          {


              //Act
              var corporationRows = _submissionCorporationRepository.GetStateCode("Washington", "US");

              //Assert
              Assert.IsNotNull(corporationRows); // Test if null
              Assert.IsTrue(corporationRows.ToUpper() == "WA");
          }

          [TestMethod]
          public void InsertCorporationDetails_returnStatus_Test()
          {

              var insertCorpDetails = new GeneralBusiness
              {
                  // SubCorporationRegId = 0,
                  MasterId = "4cb3a7f0-8e98-4b8d-86ae-a2d9dbe14959",
                  FileNumber = "C880040",
                  BusinessName = "APROSERVE CORPORATION",
                  TradeName = "",
                  BusinessStructure = "Corporation (Non-Profit)",
                  UserType = "NEWMAIL",

                  FirstName = "Auckland Park",
                  MiddleName = "Peter",
                  LastName = "Thiel",
                  BusinessAddressLine1 = "Auckland Park",
                  BusinessAddressLine2 = "NEW YORK",
                  BusinessAddressLine3 = "AVENUE",
                  BusinessCity = "San Jose",
                  BusinessState = "California",
                  BusinessCountry = "United States",
                  Telphone = "123456789",
                  ZipCode = "20008",
                  Email = "",
                  CorpStatus = "True",
                  Quardrant = "SW",
                  Unit = "1",
                  AddressNumber = "5225",
                  HQStatus = "True"
              };


              //Act
              var corporationRows = _submissionCorporationRepository.InsertCorporationDetails(insertCorpDetails);

              //Assert
              Assert.IsNotNull(corporationRows); // Test if null
              Assert.IsTrue(!corporationRows);
          }


          [TestMethod]
          public void UpdateCorporationDetails_WithHqAddress_NewMail_Test()
          {

              var insertCorpDetails = new GeneralBusiness
              {
                  // SubCorporationRegId = 0,
                  MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
                  FileNumber = "C943266",
                  BusinessName = "APROSERVE CORPORATION",
                  TradeName = "",
                  BusinessStructure = "Corporation (Non-Profit)",
                  UserType = "NEWMAIL",
                  UserSelectTpe = "NEWMAIL",
                  FirstName = "Auckland Park",
                  MiddleName = "Peter",
                  LastName = "Thiel",
                  BusinessAddressLine1 = "Auckland Park",
                  BusinessAddressLine2 = "NEW YORK",
                  BusinessAddressLine3 = "AVENUE",
                  BusinessCity = "San Jose",
                  BusinessState = "California",
                  BusinessCountry = "United States",
                  Telphone = "123456789",
                  ZipCode = "20008",
                  Email = "",
                  CorpStatus = "True",
                  Quardrant = "SW",
                  Unit = "1",
                  AddressNumber = "5225",
                  HQStatus = "True"
              };


              //Act
              var corporationRows = _submissionCorporationRepository.InsertCorporationDetails(insertCorpDetails);

              //Assert
              Assert.IsNotNull(corporationRows); // Test if null
              Assert.IsTrue(!corporationRows);
          }
        [TestMethod]
          public void ChcekListUpdateStatus_Test()
          {


              //Act
               _submissionCorporationRepository.ChcekListUpdateStatus("cce0b056-d2a3-485e-a02a-e34c957c4e40", "Y-CORPREG",true);

              //Assert
           //   Assert.IsNotNull(); // Test if null
              //Assert.IsTrue(corporationRows.ToUpper() == "WA");
          }
        [TestMethod]
        public void GetCorpAgent_Returns_SubmissionCorporationAgent_null_Test()
        {
            //Initialize Entites
            var generalBusiness = new GeneralBusiness { MasterId = "4cb3a7f0-8e98-4b8d-86ae-a2d9dbe14959", FileNumber = "C943266" };

            //Act
            var corporationRows = _submissionCorporationRepository.GetCorpAgent(generalBusiness);

            //Assert
            Assert.IsNull(corporationRows); // Test if null
            Assert.IsTrue(corporationRows==null);
        }

        [TestMethod]
        public void GetCountryFullName_Test()
        {
            //Act 
            var countryFullName = _submissionCorporationRepository.GetCountryFullName("US");

            //Assert
            Assert.IsNotNull(countryFullName); // Test if null
            Assert.IsTrue(countryFullName == "United States");
        }
        [TestMethod]
        public void GetCountryFullNameNon_Test()
        {
            //Act 
            var countryFullName = _submissionCorporationRepository.GetCountryFullName("DC");

            //Assert
            Assert.IsNotNull(countryFullName); // Test if null
            Assert.IsTrue(countryFullName == "DC");
        }
       
      
        [TestMethod]
        public void GetStateCodeWithoutCountry_Test()
        {
            //Act 
            var stateFullName = _submissionCorporationRepository.GetStateCode("AK", "");

            //Assert
            Assert.IsNotNull(stateFullName); // Test if null
            Assert.IsTrue(stateFullName == "AK");
        }
        [TestMethod]
        public void GetStateCodeWithwrongstate_Test()
        {
            //Act 
            var stateFullName = _submissionCorporationRepository.GetStateCode("Test", "US");

            //Assert
            Assert.IsNotNull(stateFullName); // Test if null
            Assert.IsTrue(stateFullName == "Test");
        }
       
        [TestMethod]
        public void GetStateFullNameWithoutCountry_Test()
        {
            //Act 
            var stateFullName = _submissionCorporationRepository.GetStateFullName("AK", "");

            //Assert
            Assert.IsNotNull(stateFullName); // Test if null
            Assert.IsTrue(stateFullName == "AK");
        }
        [TestMethod]
        public void GetStateFullNameWithwrongstate_Test()
        {
            //Act 
            var stateFullName = _submissionCorporationRepository.GetStateFullName("Test", "US");

            //Assert
            Assert.IsNotNull(stateFullName); // Test if null
            Assert.IsTrue(stateFullName == "Test");
        }
           [TestMethod]
        public void UpdateCorpDisplayStatus_Test()
        {
            var generalBusiness = new GeneralBusiness { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97", FileNumber = "C943266" };
            //Act 
            _submissionCorporationRepository.UpdateCorpDisplayStatus(generalBusiness);

            ////Assert
            //Assert.IsNotNull(stateFullName); // Test if null
            //Assert.IsTrue(stateFullName == "Test");
        }
           [TestMethod]
           public void InsertCorporationDetails_WithHqAddress_returnStatusValue_No_Test()
           {

               var insertCorpDetails = new GeneralBusiness
               {
                   // SubCorporationRegId = 0,
                   MasterId = "4cb3a7f0-8e98-4b8d-86ae-a2d9dbe14959",
                   FileNumber = "C880040",
                   BusinessName = "APROSERVE CORPORATION",
                   TradeName = "",
                   BusinessStructure = "Corporation (Non-Profit)",
                   UserType = "Y-CORPREG",

                   FirstName = "Auckland Park",
                   MiddleName = "Peter",
                   LastName = "Thiel",
                   BusinessAddressLine1 = "Auckland Park",
                   BusinessAddressLine2 = "NEW YORK",
                   BusinessAddressLine3 = "AVENUE",
                   BusinessCity = "San Jose",
                   BusinessState = "California",
                   BusinessCountry = "United States",
                   Telphone = "123456789",
                   ZipCode = "20008",
                   Email = "",
                   CorpStatus = "True",
                   Quardrant = "SW",
                   Unit = "1",
                   AddressNumber = "5225",
                   HQStatus = "True"
               };


               //Act
               var corporationRows = _submissionCorporationRepository.InsertCorporationDetails(insertCorpDetails);

               //Assert
               Assert.IsNotNull(corporationRows); // Test if null
               Assert.IsTrue(corporationRows);
           }

           [TestMethod]
           public void UpdateCorporationDetails_WithHqAddress_returnStatusValueTest()
           {

               var insertCorpDetails = new GeneralBusiness
               {
                   // SubCorporationRegId = 0,
                   MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                   FileNumber = "C880040",
                   BusinessName = "APROSERVE CORPORATION",
                   TradeName = "",
                   BusinessStructure = "Corporation (Non-Profit)",
                   UserType = "Y-CORPAGENT",

                   FirstName = "Auckland Park",
                   MiddleName = "Peter",
                   LastName = "Thiel",
                   BusinessAddressLine1 = "Auckland Park",
                   BusinessAddressLine2 = "NEW YORK",
                   BusinessAddressLine3 = "AVENUE",
                   BusinessCity = "San Jose",
                   BusinessState = "California",
                   BusinessCountry = "United States",
                   Telphone = "123456789",
                   ZipCode = "20008",
                   Email = "",
                   CorpStatus = "True",
                   Quardrant = "SW",
                   Unit = "1",
                   AddressNumber = "5225",
                   HQStatus = "false"
               };


               //Act
               var corporationRows = _submissionCorporationRepository.InsertCorporationDetails(insertCorpDetails);

               //Assert
               Assert.IsNotNull(corporationRows); // Test if null
               Assert.IsTrue(!corporationRows);
           }

           [TestMethod]
           public void InsertCorporationDetails_WithHqAddress_NofileNumber_Test()
           {

               var insertCorpDetails = new GeneralBusiness
               {
                   // SubCorporationRegId = 0,
                   MasterId = "4cb3a7f0-8e98-4b8d-86ae-a2d9dbe14959",
                   FileNumber = "",
                   BusinessName = "APROSERVE CORPORATION",
                   TradeName = "",
                   BusinessStructure = "Corporation (Non-Profit)",
                   UserType = "Y-CORPREG",

                   FirstName = "Auckland Park",
                   MiddleName = "Peter",
                   LastName = "Thiel",
                   BusinessAddressLine1 = "Auckland Park",
                   BusinessAddressLine2 = "NEW YORK",
                   BusinessAddressLine3 = "AVENUE",
                   BusinessCity = "San Jose",
                   BusinessState = "California",
                   BusinessCountry = "United States",
                   Telphone = "123456789",
                   ZipCode = "20008",
                   Email = "",
                   CorpStatus = "True",
                   Quardrant = "SW",
                   Unit = "1",
                   AddressNumber = "5225",
                   HQStatus = "True"
               };


               //Act
               var corporationRows = _submissionCorporationRepository.InsertCorporationDetails(insertCorpDetails);

               //Assert
               Assert.IsNotNull(corporationRows); // Test if null
               Assert.IsTrue(corporationRows);
           }

           [TestMethod]
           public void updateCorporationDetails_WithHqAddress_NofileNumber_Test()
           {

               var insertCorpDetails = new GeneralBusiness
               {
                   // SubCorporationRegId = 0,
                   MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                   FileNumber = "",
                   BusinessName = "APROSERVE CORPORATION",
                   TradeName = "",
                   BusinessStructure = "Corporation (Non-Profit)",
                   UserType = "Y-CORPREG",

                   FirstName = "Auckland Park",
                   MiddleName = "Peter",
                   LastName = "Thiel",
                   BusinessAddressLine1 = "Auckland Park",
                   BusinessAddressLine2 = "NEW YORK",
                   BusinessAddressLine3 = "AVENUE",
                   BusinessCity = "San Jose",
                   BusinessState = "California",
                   BusinessCountry = "United States",
                   Telphone = "123456789",
                   ZipCode = "20008",
                   Email = "",
                   CorpStatus = "True",
                   Quardrant = "SW",
                   Unit = "1",
                   AddressNumber = "5225",
                   HQStatus = "True"
               };


               //Act
               var corporationRows = _submissionCorporationRepository.InsertCorporationDetails(insertCorpDetails);

               //Assert
               Assert.IsNotNull(corporationRows); // Test if null
               Assert.IsTrue(!corporationRows);
           }
    }
}