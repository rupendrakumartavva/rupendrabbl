//using BusinessCenter.Data;
//using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using System;
//using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web.UI.WebControls;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Tests.Setup;

namespace BusinessCenter.Tests.Repository
{
    [TestClass]
    public class SubmissionCofoHopeHopAddressRepositoryTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Repository declaration
        private SubmissionCofoHopeHopAddressRepository _cofoAddressRepository;
      //  private Mock<ISubmissionCofoHopeHopAddressRepository> _mockcofoAddressRepository;
       // private SubmissionCofoHopeHopAddressData _submissionCofoHopeHopAddress;
        private CorpRespository _corpRespository;
        private SubmissionMasterRepository _submissionMasterRepository;
        private StreetTypesRepository _streetTypesRepository;
        private MasterRegisterAgentRepository _masterRegisterAgentRepository;
        private SubmissionCorporationRepository _submissionCorporationRepository;
        private SubmissionQuestionRepository _submissionQuestionRepository;
        private SubmissionMasterApplicationChcekListRepository _submissionMasterApplicationChcekListRepository;
        private SubmissionCategoryRepository _submissionCategoryRepository;
        private OSubCategoryFeesRepository _oSubCategoryFeesRepository;
        private FixFeeRepository _fixedFeeTestRepository;
        private FeeCodeMapRepository _feeCodeMapRepository;
        private SubmissionMasterRenewalRepository _submissionMasterRenewalRepository;
        private MasterSubCategoryRepository _masterSubCategoryRepository;
        private UserBBLServiceRepository _userBblServiceRepository;
        private BblRepository _bblRepository;
        private MasterCategoryPhysicalLocationRepository _masterCategoryPhysicalLocationRepository;
        private MasterPrimaryCategoryRepository _masterPrimaryCategory;
        private UserRepository _userRepository;
        private MasterBusinessActivityRepository _masterBusinessActivityRepository;
        private DCBC_ENTITY_BBL_RenewalsRepository _dcbcEntityBblRenewalsRepository;
        private DCBC_ENTITY_BBL_Renewal_InvoiceRepository _dcbcEntitybblRenewalInvoiceRepository;
        private SubmissionBblAssociationToUsersRepository _submissionBblAssociationToUsersRepository;
        private SubmissionToAccelaRepository _submissionToAccelaRepository;
        private MasterSecondaryLicenseCategoryRepository _masterSecondaryLicenseCategoryRepository;
        private SubmissionCorporationAgentAddressRepository _submissionCorporationAgentAddressRepository;
        private Lookup_ExistingCategoriesRepository _lookupExistingCategoriesRepository;
        private MasterRenewalStatusFeeRepository _masterRenewalStatusFeeRepository;
        private MasterCategoryQuestionRepository _masterCategoryQuestionRepository;
        private MasterCategoryDocumentRepository _masterCategoryDocumentRepository;
        private UserRoleRepository _userRoleRepository;
      //  private MasterLicenseFEINRenewal masterLicenseFeinRenewal;
      //  private MasterPrimaryCategoryRepository _masterPrimaryCategoryRepository;
        private MasterCountryRepository _masterCountryRepository;
        private MasterStateRepository _masterStateRepository;
        private SubmissionLicenseNumberCounterRepository _submissionlicencecounter;
        private MasterBblApplicationStatusRepository _masterBblApplicationStatusRepository;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
         //   masterLicenseFeinRenewal = new MasterLicenseFEINRenewal(_testUnitOfWork);
            _dcbcEntityBblRenewalsRepository = new DCBC_ENTITY_BBL_RenewalsRepository(_testUnitOfWork);
            _masterCountryRepository = new MasterCountryRepository(_testUnitOfWork);
            _masterStateRepository = new MasterStateRepository(_testUnitOfWork);
            _userRoleRepository = new UserRoleRepository(_testUnitOfWork);
            _masterCategoryDocumentRepository = new MasterCategoryDocumentRepository(_testUnitOfWork);
            _submissionToAccelaRepository=new SubmissionToAccelaRepository(_testUnitOfWork);
            _masterCategoryQuestionRepository = new MasterCategoryQuestionRepository(_testUnitOfWork);
            _masterBblApplicationStatusRepository = new MasterBblApplicationStatusRepository(_testUnitOfWork);
            _masterSecondaryLicenseCategoryRepository = new MasterSecondaryLicenseCategoryRepository(_testUnitOfWork);
            _submissionBblAssociationToUsersRepository = new SubmissionBblAssociationToUsersRepository(_testUnitOfWork);
            _submissionlicencecounter = new SubmissionLicenseNumberCounterRepository(_testUnitOfWork);
            //Lookup_ExistingCategoriesRepository Initialization
            _lookupExistingCategoriesRepository = new Lookup_ExistingCategoriesRepository(_testUnitOfWork);
            _masterRenewalStatusFeeRepository = new MasterRenewalStatusFeeRepository(_testUnitOfWork);
            //CorpRespository Initialization
            _corpRespository=new CorpRespository(_testUnitOfWork);

            //StreetTypesRepository Initialization
            _streetTypesRepository=new StreetTypesRepository(_testUnitOfWork);

            //MasterRegisterAgentRepository Initialization
            _masterRegisterAgentRepository = new MasterRegisterAgentRepository(_testUnitOfWork);

            //SubmissionQuestionRepository Initialization
            _submissionQuestionRepository=new SubmissionQuestionRepository(_testUnitOfWork);

            //FixFeeRepository Initialization
            _fixedFeeTestRepository = new FixFeeRepository(_testUnitOfWork);

            //UserBBLServiceRepository Initialization
            _userBblServiceRepository = new UserBBLServiceRepository(_testUnitOfWork);

            //BblRepository Initialization
            _bblRepository = new BblRepository(_testUnitOfWork);
            //UserRepository Initialization
            _userRepository = new UserRepository(_testUnitOfWork, _userRoleRepository);
            _submissionCorporationAgentAddressRepository = new SubmissionCorporationAgentAddressRepository(_testUnitOfWork, _submissionMasterApplicationChcekListRepository, _submissionMasterRepository);
            _dcbcEntitybblRenewalInvoiceRepository = new DCBC_ENTITY_BBL_Renewal_InvoiceRepository(_testUnitOfWork, _lookupExistingCategoriesRepository);
            _masterBusinessActivityRepository = new MasterBusinessActivityRepository(_testUnitOfWork, _masterPrimaryCategory);
            _masterPrimaryCategory = new MasterPrimaryCategoryRepository(_testUnitOfWork, _masterSecondaryLicenseCategoryRepository,
               _masterCategoryDocumentRepository, _masterCategoryQuestionRepository);

            _masterSubCategoryRepository = new MasterSubCategoryRepository(_testUnitOfWork, _masterPrimaryCategory, _masterSecondaryLicenseCategoryRepository);
            _masterCategoryPhysicalLocationRepository = new MasterCategoryPhysicalLocationRepository(_testUnitOfWork, _masterPrimaryCategory, _masterCategoryQuestionRepository,
              _oSubCategoryFeesRepository, _masterSecondaryLicenseCategoryRepository);

            //SubmissionMasterApplicationChcekListRepository Initialization
            _submissionMasterApplicationChcekListRepository=new SubmissionMasterApplicationChcekListRepository(_testUnitOfWork);

            //SubmissionMasterRepository Initialization
            _submissionMasterRepository = new SubmissionMasterRepository(_testUnitOfWork, _submissionCategoryRepository, _bblRepository,
            _userBblServiceRepository, _submissionQuestionRepository, _masterCategoryPhysicalLocationRepository, _masterPrimaryCategory,
            _submissionMasterApplicationChcekListRepository, _userRepository, _dcbcEntityBblRenewalsRepository, _masterBusinessActivityRepository,
            _submissionBblAssociationToUsersRepository, _submissionToAccelaRepository, _masterSecondaryLicenseCategoryRepository,
            _lookupExistingCategoriesRepository, _submissionlicencecounter, _masterBblApplicationStatusRepository);

            //SubmissionCorporationRepository Initialization
            _submissionCorporationRepository=new SubmissionCorporationRepository(_testUnitOfWork,_submissionCorporationAgentAddressRepository,
            _submissionMasterApplicationChcekListRepository,_masterRegisterAgentRepository,
            _submissionQuestionRepository, _corpRespository, _submissionMasterRepository, _masterCountryRepository, _masterStateRepository);

            //SubmissionCofoHopeHopAddressRepository Initialization
            _cofoAddressRepository = new SubmissionCofoHopeHopAddressRepository(_testUnitOfWork,_corpRespository,_submissionMasterRepository,
                _streetTypesRepository,_masterRegisterAgentRepository,_submissionCorporationRepository,_submissionQuestionRepository,
                _submissionMasterApplicationChcekListRepository, _masterStateRepository, _masterCountryRepository);

            //SubmissionCategoryRepository InitialiZation

            _submissionCategoryRepository = new SubmissionCategoryRepository(_testUnitOfWork, _masterPrimaryCategory,
                _masterSecondaryLicenseCategoryRepository, _oSubCategoryFeesRepository, _fixedFeeTestRepository, _submissionQuestionRepository,
                _masterSubCategoryRepository, _masterCategoryPhysicalLocationRepository, _feeCodeMapRepository, _submissionMasterApplicationChcekListRepository,
                _submissionMasterRenewalRepository, _dcbcEntitybblRenewalInvoiceRepository, _lookupExistingCategoriesRepository
                );

            _oSubCategoryFeesRepository = new OSubCategoryFeesRepository(_testUnitOfWork, _masterPrimaryCategory, _masterSecondaryLicenseCategoryRepository);
            _feeCodeMapRepository = new FeeCodeMapRepository(_testUnitOfWork, _oSubCategoryFeesRepository);
            _submissionMasterRenewalRepository = new SubmissionMasterRenewalRepository(_testUnitOfWork, _masterRenewalStatusFeeRepository);
             
            //SubmissionCofoHopeHopAddressRepository Initialization
           // _mockcofoAddressRepository = new Mock<ISubmissionCofoHopeHopAddressRepository>();
            //_mockData = new SubmissionCofoHopeHopAddressData();

            //Setup  SubmissionCofo_Hop_Ehop_Address FackData
            var testData = new SubmissionCofoHopeHopAddressData();
            _testDbContext.SubmissionCofo_Hop_Ehop_Address.AddRange(testData.SubmissionCofoHopEhopAddressList);
            _testDbContext.SaveChanges();

            //Setup  DCBC_ENTITY_CORP FackData
            var corpData = new CorpRepositoryData();
            _testDbContext.DCBC_ENTITY_CORP.AddRange(corpData.CorpEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  SubmissionMaster FackData
            var masterData = new SubmissionMasterRepositoryData();
            _testDbContext.SubmissionMaster.AddRange(masterData.SubmissionMasterEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  StreetTypes FackData
            var streetTypeData = new StreetTypesRepositoryData();
            _testDbContext.StreetTypes.AddRange(streetTypeData.ListAll);
            _testDbContext.SaveChanges();

            //Setup  MasterRegisteredAgent FackData
            //var masterRegisterAgentData = new MasterRegisterAgentData();
            //_testDbContext.MasterRegisteredAgent.AddRange(masterRegisterAgentData.MasterRegisteredAgentEntitiesList);
            //_testDbContext.SaveChanges();

            //Setup  SubmissionCorporation_Agent FackData
            var submissionCorporationData = new SubmissionCorporationRepositoryData();
            _testDbContext.SubmissionCorporation_Agent.AddRange(submissionCorporationData.SubmissionCorporationEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  SubmissionQuestion FackData
            var submissionQuestionData = new SubmissionQuestionRepositoryData();
            _testDbContext.SubmissionQuestion.AddRange(submissionQuestionData.SubmissionQuestionEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  SubmissionMaster_ApplicationCheckList FackData
            var submissionChecklistData = new SubmissionMasterApplicationChcekListRepositoryData();
            _testDbContext.SubmissionMaster_ApplicationCheckList.AddRange(
                submissionChecklistData.SubmissionMaster_ApplicationCheckListEntitiesList);
            _testDbContext.SaveChanges();

            var countryData = new MasterCountryData();
            _testDbContext.MasterCountry.AddRange(countryData.MasterCountryEntitiesList);
            _testDbContext.SaveChanges();

            var stateData = new MasterStateRepositoryData();
            _testDbContext.MasterState.AddRange(stateData.MasterStateEntitiesList);
            _testDbContext.SaveChanges();
            
        }
         [TestMethod]
        public void FindByIdTest()
        {
             //Act 
            var cofoAddress = _cofoAddressRepository.GetPrimisessAddress(1).ToList();

            //Assert
            Assert.IsNotNull(cofoAddress); // Test if null
            Assert.IsTrue(cofoAddress.Count() == 1);
        }
         [TestMethod]
         public void GetPrimisessAddressTest()
         {
             //Act 
             var cofoAddress = _cofoAddressRepository.GetPrimisessAddress(1,"hop").ToList();

             //Assert
             Assert.IsNotNull(cofoAddress); // Test if null
             Assert.IsTrue(cofoAddress.Count() == 1);
         }

         [TestMethod]
         public void GetCorpBusinessDataNoDataTest()
         {
             //Initial Model Details
             var generalBusiness = new GeneralBusiness { MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40", FileNumber="" };
             //Act 
             var cofoAddress = _cofoAddressRepository.GetCorpBusinessData(generalBusiness).ToList();

             //Assert
             Assert.IsNotNull(cofoAddress); // Test if null
             // ReSharper disable once PossibleNullReferenceException
             Assert.IsTrue(cofoAddress.FirstOrDefault().EntityStatus == "NODATA");
         }

         [TestMethod]
         public void GetCorpBusinessDataTest()
         {
             //Initial Model Details
             var generalBusiness = new GeneralBusiness { MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40", FileNumber = "C212821" };
             //Act 
             var cofoAddress = _cofoAddressRepository.GetCorpBusinessData(generalBusiness).ToList();

             //Assert
             Assert.IsNotNull(cofoAddress); // Test if null
             // ReSharper disable once PossibleNullReferenceException
             Assert.IsTrue(cofoAddress.FirstOrDefault().FileNumber == "C212821");
         }
         [TestMethod]
         public void GetCorpBusinessDatacofo_Test()
         {
             //Initial Model Details
             var generalBusiness = new GeneralBusiness { MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c", FileNumber = "C943266" };
             //Act 
             var cofoAddress = _cofoAddressRepository.GetCorpBusinessData(generalBusiness).ToList();

             //Assert
             Assert.IsNotNull(cofoAddress); // Test if null
             // ReSharper disable once PossibleNullReferenceException
             Assert.IsTrue(cofoAddress.FirstOrDefault().FileNumber == "C943266");
         }
         [TestMethod]
         public void UpdateSubmissionLocationTest()
         {
             //Initial Model Details
             var cofoHopDetailsModel = new CofoHopDetailsModel { MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40", StreetTypeId = 2, Number = "CO1501622" };
             //Act 
             var cofoAddress = _cofoAddressRepository.InsertSubmissionLocation(1,cofoHopDetailsModel);

             //Assert
             Assert.IsNotNull(cofoAddress); // Test if null
             Assert.IsTrue(cofoAddress);
         }
         [TestMethod]
         public void InsertSubmissionLocationTest()
         {
             //Initial Model Details
             var cofoHopDetailsModel = new CofoHopDetailsModel { MasterId = "4cb3a7f0-8e98-4b8d-86ae-a2d9dbe14959", StreetTypeId = 2, Number = "CO1501622" };
             //Act 
             var cofoAddress = _cofoAddressRepository.InsertSubmissionLocation(4, cofoHopDetailsModel);

             //Assert
             Assert.IsNotNull(cofoAddress); // Test if null
             Assert.IsTrue(cofoAddress);
         }
         [TestMethod]
         public void RemoveSubmissionLocationTest()
         {
             //Initial Model Details
             var cofoHopDetailsModel = new CofoHopDetailsModel { MasterId = "4cb3a7f0-8e98-4b8d-86ae-a2d9dbe14959", StreetTypeId = 2, Number = "" };
             //Act 
             var cofoAddress = _cofoAddressRepository.InsertSubmissionLocation(2, cofoHopDetailsModel);

             //Assert
             Assert.IsNotNull(cofoAddress); // Test if null
             Assert.IsTrue(cofoAddress);
         }
         [TestMethod]
         public void SubmissionLocationFalseTest()
         {
             //Initial Model Details
             var cofoHopDetailsModel = new CofoHopDetailsModel { MasterId = "4cb3a7f0-8e98-4b8d-86ae-a2d9dbe14959", StreetTypeId = 10, Number = "CO1501622" };
             //Act 
             var cofoAddress = _cofoAddressRepository.InsertSubmissionLocation(4, cofoHopDetailsModel);

             //Assert
             Assert.IsNotNull(cofoAddress); // Test if null
             Assert.IsFalse(cofoAddress);
         }

         [TestMethod]
         public void DeleteBusinessAddressFalseTest()
         {
             //Initial Model Details
             var cofoHopDetailsModel = new CofoHopDetailsModel { MasterId = "4cb3a7f0-8e98-4b8d-86ae-a2d9dbe14959", StreetTypeId = 10, Number = "CO1501622" };
             //Act 
             var cofoAddress = _cofoAddressRepository.DeleteBusinessAddress(cofoHopDetailsModel);

             //Assert
             Assert.IsNotNull(cofoAddress); // Test if null
             Assert.IsFalse(cofoAddress);
         }
         [TestMethod]
         public void DeleteBusinessAddressTest()
         {
             //Initial Model Details
             var cofoHopDetailsModel = new CofoHopDetailsModel { CofoHopId=2 };
             //Act 
             var cofoAddress = _cofoAddressRepository.DeleteBusinessAddress(cofoHopDetailsModel);

             //Assert
             Assert.IsNotNull(cofoAddress); // Test if null
             Assert.IsTrue(cofoAddress);
         }

       
         [TestMethod]
         public void DeleteHopTest()
         {
           //Act 
             var cofoAddress = _cofoAddressRepository.DeleteHOP(1);

             //Assert
             Assert.IsNotNull(cofoAddress); // Test if null
             Assert.IsTrue(cofoAddress);
         }

         [TestMethod]
         public void GetStateFullName_Test()
         {
           //Act 
             var stateFullName = _cofoAddressRepository.GetStateFullName("AK", "US");

             //Assert
             Assert.IsNotNull(stateFullName); // Test if null
             Assert.IsTrue(stateFullName == "Alaska");
         }
         [TestMethod]
         public void GetStateFullNameWithoutCountry_Test()
         {
             //Act 
             var stateFullName = _cofoAddressRepository.GetStateFullName("AK", "");

             //Assert
             Assert.IsNotNull(stateFullName); // Test if null
             Assert.IsTrue(stateFullName == "AK");
         }
         [TestMethod]
         public void GetStateFullNameWithwrongstate_Test()
         {
             //Act 
             var stateFullName = _cofoAddressRepository.GetStateFullName("Test", "US");

             //Assert
             Assert.IsNotNull(stateFullName); // Test if null
             Assert.IsTrue(stateFullName == "Test");
         }

         [TestMethod]
         public void GetStateCode_Test()
         {
             //Act 
             var stateFullName = _cofoAddressRepository.GetStateCode("Alaska", "US");

             //Assert
             Assert.IsNotNull(stateFullName); // Test if null
             Assert.IsTrue(stateFullName == "AK");
         }
         [TestMethod]
         public void GetStateCodeWithoutCountry_Test()
         {
             //Act 
             var stateFullName = _cofoAddressRepository.GetStateCode("AK", "");

             //Assert
             Assert.IsNotNull(stateFullName); // Test if null
             Assert.IsTrue(stateFullName == "AK");
         }
         [TestMethod]
         public void GetStateCodeWithwrongstate_Test()
         {
             //Act 
             var stateFullName = _cofoAddressRepository.GetStateCode("Test", "US");

             //Assert
             Assert.IsNotNull(stateFullName); // Test if null
             Assert.IsTrue(stateFullName == "Test");
         }
         [TestMethod]
         public void GetCountryFullName_Test()
         {
             //Act 
             var countryFullName = _cofoAddressRepository.GetCountryFullName("US");

             //Assert
             Assert.IsNotNull(countryFullName); // Test if null
             Assert.IsTrue(countryFullName == "United States");
         }
         [TestMethod]
         public void GetCountryFullNameWithwrongcountry_Test()
         {
             //Act 
             var countryFullName = _cofoAddressRepository.GetCountryFullName("Test");

             //Assert
             Assert.IsNotNull(countryFullName); // Test if null
             Assert.IsTrue(countryFullName == "Test");
         }

         [TestMethod]
         public void GetStreetFullName_Test()
         {
             //Act 
             var streetFullName = _cofoAddressRepository.GetStreetFullName("AL");

             //Assert
             Assert.IsNotNull(streetFullName); // Test if null
             Assert.IsTrue(streetFullName == "Alley");
         }
         [TestMethod]
         public void GetStreetFullNameWithwrongcountry_Test()
         {
             //Act 
             var streetFullName = _cofoAddressRepository.GetStreetFullName("Test");

             //Assert
             Assert.IsNotNull(streetFullName); // Test if null
             Assert.IsTrue(streetFullName == "Test");
         }
        //4cb3a7f0-8e98-4b8d-86ae-a2d9dbe14959
        #region Junk

        //Mock<ISubmissionCofoHopeHopAddressRepository> mockRepo = new Mock<ISubmissionCofoHopeHopAddressRepository>();

        //List<SubmissionCofo_Hop_Ehop_Address> list = new List<SubmissionCofo_Hop_Ehop_Address>()
        //{
        //    new SubmissionCofo_Hop_Ehop_Address()
        //    {
        //        SubmissionCofo_Hop_Ehop_AddressId = 1,
        //        CustomTypeId = 1,
        //        CustomType = "",
        //        Name = "",
        //        Street =  "",
        //        StreetName = "",
        //        StreetType = "",
        //        Quadrant = "",
        //        UnitType = "",
        //        Unit = "",
        //        City = "WASHINGTON",
        //        State = "DC",
        //        Telephone = "",
        //        Zip = "20016-4602",
        //        IsValid = false,
        //        Country = ""
        //    },
        //     new SubmissionCofo_Hop_Ehop_Address()
        //    {
        //        SubmissionCofo_Hop_Ehop_AddressId = 2,
        //        CustomTypeId = 2,
        //        CustomType = "",
        //      Name = "",
        //    Street = "Lane 4",
        //    StreetName = "SMALLSYS INC",
        //    Quadrant = "Nw",
        //    UnitType = "APT",
        //    Unit = "Washington",
        //    City = "Chicago",
        //    State = "Illinois",
        //    Telephone = "88896558",
        //    Zip = "20016-4602",
        //    IsValid = false,
        //    Country = "US",
        //    }
        //};

        //private readonly CofoHopDetailsModel cofoHopDetailsModel = new CofoHopDetailsModel()
        //{
        //    Type = "cofo",
        //    Name = "",
        //    Street = "Lane 4",
        //    StreetName = "SMALLSYS INC",
        //    Quadrant = "Nw",
        //    UnitType = "APT",
        //    Unit = "Washington",
        //    City = "Chicago",
        //    State = "Illinois",
        //    Telephone = "88896558",
        //    Zip = "20016-4602",
        //    IsValid = false,
        //    Country = "US",
        //};

        //private readonly GeneralBusiness generalBusiness = new GeneralBusiness()
        //{
        //    UserType = "User",
        //    FileNumber = "FA588558",
        //    MasterId = "9F92454C-ED4C-4062-B557-4125813B2BDD",
        //    CBusinessName = "",
        //    TradeName = "",
        //    BusinessStructure = "",
        //    FirstName = "",
        //    MiddleName = "",
        //    LastName = "",
        //    BusinessName = "",
        //    BusinessAddressLine1 = "",
        //    BusinessAddressLine2 = "",
        //    BusinessAddressLine3 = "",
        //    BusinessAddressLine4 = "",
        //    BusinessCity = "",
        //    BusinessState = "",
        //    BusinessCountry = "",
        //    ZipCode = "",
        //    Email = "",
        //    EntityStatus = "",
        //    UserSelectTpe = "",
        //    Quardrant = "",
        //    UnitType = "",
        //    Unit = "",
        //    Telphone = "",
        //    OccupancyAddssValidate = "",
        //    CorpStatus = "",
        //    HQStatus = ""
        //};

        //private readonly List<GeneralBusiness> generalBusinessList = new List<GeneralBusiness>()
        //{
        //    new GeneralBusiness()
        //    {
        //    UserType = "User",
        //    FileNumber = "FA588558",
        //    MasterId = "9F92454C-ED4C-4062-B557-4125813B2BDD",
        //    CBusinessName = "",
        //    TradeName = "",
        //    BusinessStructure = "",
        //    FirstName = "",
        //    MiddleName = "",
        //    LastName = "",
        //    BusinessName = "",
        //    BusinessAddressLine1 = "",
        //    BusinessAddressLine2 = "",
        //    BusinessAddressLine3 = "",
        //    BusinessAddressLine4 = "",
        //    BusinessCity = "",
        //    BusinessState = "",
        //    BusinessCountry = "",
        //    ZipCode = "",
        //    Email = "",
        //    EntityStatus = "",
        //    UserSelectTpe = "",
        //    Quardrant = "",
        //    UnitType = "",
        //    Unit = "",
        //    Telphone = "",
        //    OccupancyAddssValidate = "",
        //    CorpStatus = "",
        //    HQStatus = ""
        //    },
        //      new GeneralBusiness()
        //    {
        //    UserType = "User",
        //    FileNumber = "FA588558",
        //    MasterId = "B0059EA0-AC78-4F89-BF6F-AD5447C48544",
        //    CBusinessName = "",
        //    TradeName = "",
        //    BusinessStructure = "",
        //    FirstName = "",
        //    MiddleName = "",
        //    LastName = "",
        //    BusinessName = "",
        //    BusinessAddressLine1 = "",
        //    BusinessAddressLine2 = "",
        //    BusinessAddressLine3 = "",
        //    BusinessAddressLine4 = "",
        //    BusinessCity = "",
        //    BusinessState = "",
        //    BusinessCountry = "",
        //    ZipCode = "",
        //    Email = "",
        //    EntityStatus = "",
        //    UserSelectTpe = "",
        //    Quardrant = "",
        //    UnitType = "",
        //    Unit = "",
        //    Telphone = "",
        //    OccupancyAddssValidate = "",
        //    CorpStatus = "",
        //    HQStatus = ""
        //    }
        //};

        //[TestMethod()]
        //public void FindByIDTest()
        //{
        //    mockRepo.Setup(m => m.FindByID(generalBusiness)).Returns(list);
        //    var runtimeOutput = mockRepo.Object.FindByID(generalBusiness);
        //    Assert.IsTrue(runtimeOutput != null);
        //    Assert.IsTrue(runtimeOutput.FirstOrDefault().CustomTypeId == 1);
        //    Assert.IsTrue(runtimeOutput.Last().CustomTypeId == 2);
        //}

        //[TestMethod()]
        //public void GetPrimisessAddressTest()
        //{
        //    int customTypeid = 1;
        //    mockRepo.Setup(m => m.GetPrimisessAddress(customTypeid)).Returns(list);
        //    var runtimeOutput = mockRepo.Object.GetPrimisessAddress(customTypeid);
        //    Assert.IsTrue(runtimeOutput != null);
        //    Assert.IsTrue(runtimeOutput.FirstOrDefault().CustomTypeId == 1);
        //    Assert.IsTrue(runtimeOutput.Last().CustomTypeId == 2);
        //}

        //[TestMethod()]
        //public void GetPrimisessAddress2Test()
        //{
        //    int customTypeid = 1;
        //    string customType = "cofo";
        //    mockRepo.Setup(m => m.GetPrimisessAddress(customTypeid, customType)).Returns(list);
        //    var runtimeOutput = mockRepo.Object.GetPrimisessAddress(customTypeid, customType);
        //    Assert.IsTrue(runtimeOutput != null);
        //    Assert.IsTrue(runtimeOutput.FirstOrDefault().CustomTypeId == 1);
        //    Assert.IsTrue(runtimeOutput.Last().CustomTypeId == 2);
        //}

        ////[TestMethod()]
        ////public void GetEnumDescriptionTest()
        ////{
        ////    mockRepo.Setup(m => m.GetEnumDescription(generalBusiness)).Returns(list);
        ////    var runtimeOutput = mockRepo.Object.GetEnumDescription(generalBusiness);
        ////    Assert.IsTrue(runtimeOutput != null);
        ////    Assert.IsTrue(runtimeOutput.FirstOrDefault().CustomTypeId == 1);
        ////    Assert.IsTrue(runtimeOutput.Last().CustomTypeId == 2);
        ////}

        //[TestMethod()]
        //public void GetCorpBusinessDataTest()
        //{
        //    mockRepo.Setup(m => m.GetCorpBusinessData(generalBusiness)).Returns(generalBusinessList);
        //    var runtimeOutput = mockRepo.Object.GetCorpBusinessData(generalBusiness);
        //    Assert.IsTrue(runtimeOutput != null);
        //    Assert.IsTrue(runtimeOutput.FirstOrDefault().MasterId == "9F92454C-ED4C-4062-B557-4125813B2BDD");
        //    Assert.IsTrue(runtimeOutput.Last().MasterId == "B0059EA0-AC78-4F89-BF6F-AD5447C48544");
        //}

        //[TestMethod()]
        //public void InsertSubmissionLocationTest()
        //{
        //    int customtype = 10;
        //    mockRepo.Setup(m => m.InsertSubmissionLocation(customtype, cofoHopDetailsModel)).Returns(true);
        //    var runtimeOutput = mockRepo.Object.InsertSubmissionLocation(customtype, cofoHopDetailsModel);
        //    Assert.AreEqual<bool>(true, runtimeOutput);
        //}

        //[TestMethod()]
        //public void DeleteBusinessAddressTest()
        //{
        //    mockRepo.Setup(m => m.DeleteBusinessAddress(cofoHopDetailsModel)).Returns(true);
        //    var runtimeOutput = mockRepo.Object.DeleteBusinessAddress(cofoHopDetailsModel);
        //    Assert.AreEqual<bool>(true, runtimeOutput);
        //}

        //[TestMethod()]
        //public void DeleteHOPTest()
        //{
        //    int CustomTypeId = 10;
        //    mockRepo.Setup(m => m.DeleteHOP(CustomTypeId)).Returns(true);
        //    var runtimeOutput = mockRepo.Object.DeleteHOP(CustomTypeId);
        //    Assert.AreEqual<bool>(true, runtimeOutput);
        //}
#endregion
    }
}
