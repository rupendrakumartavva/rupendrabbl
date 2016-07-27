using BusinessCenter.Data;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Tests.Setup;

namespace BusinessCenter.Tests.Repository
{
    [TestClass]
    public class SubmissionCategoryRepositoryTest
    {
        //Initialization DBConnection
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Repository declaration
        private SubmissionCategoryRepository _submissionCategoryTestRepository;
        private MasterPrimaryCategoryRepository _mockMasterPrimaryCategoryRepository;
        private MasterSecondaryLicenseCategoryRepository _mockMasterSecondaryLicenseCategoryRepository;
        private MasterCategoryDocumentRepository _masterCategoryDocument;
        private MasterCategoryQuestionRepository _mockMasterCategoryQuestionRepository;
        private OSubCategoryFeesRepository _mockSubCategoryFeesRepository;
        private FixFeeRepository _fixedFeeTestRepository;
        private SubmissionQuestionRepository _submissionQuestionTestRepository;
        private MasterSubCategoryRepository _MasterSubCategoryTestRepository;
        private MasterCategoryPhysicalLocationRepository _masterCategoryPhysicalLocationTestRepository;
        private FeeCodeMapRepository _feeCodeMapTestRepository;
        private SubmissionMasterApplicationChcekListRepository _checkListTestRepository;
        private SubmissionMasterRenewalRepository _submissionMasterRenewalRepository;
        private DCBC_ENTITY_BBL_Renewal_InvoiceRepository _dCBC_ENTITY_BBL_Renewal_InvoiceTestRepository;
       // private Mock<IDCBC_ENTITY_BBL_Renewal_InvoiceRepository> _iDCBC_ENTITY_BBL_Renewal_InvoiceReposiotory;
        //private SubmissionCategoryRepositoryData _submissionCategoryRepositoryData;
        //private MasterPrimaryCategoryRepositoryData _masterPrimaryCategoryRepositoryData;
        //private MasterSecondaryLicenseCategoryRepositoryData _masterSecondaryLicenseCategoryRepositoryData;
        //private MasterCategoryDocumentRepositoryData _masterCategoryDocumentRepositoryData;
        //private MasterCategoryQuestionData _masterCategoryQuestionData;
        //private OSubCategoryFeesData _oSubCategoryFeesData;
        //private FixFeeRepositoryData _fixFeeRepositoryData;
        //private SubmissionQuestionRepositoryData _submissionQuestionRepositoryData;
        //private MasterSubCategoryRepositoryData _masterSubCategoryRepositoryData;
        //private MasterCategoryPhysicalLocationData _masterCategoryPhysicalLocationRepositoryData;
        //private FeeCodeMapRepositoryData _feeCodeMapRepositoryData;
        //private SubmissionMasterApplicationChcekListRepositoryData _submissionMasterApplicationChcekListRepositoryData;
        private MasterRenewalStatusFeeRepository _masterrenewalstatusfee;
        //private SubmissionMasterRenewalData
        private Lookup_ExistingCategoriesRepository _Lookup_ExistingCategoriesRepository;

        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _Lookup_ExistingCategoriesRepository = new Lookup_ExistingCategoriesRepository(_testUnitOfWork);
            //MasterSecondaryLicenseCategoryRepository Initialization
            _mockMasterSecondaryLicenseCategoryRepository = new MasterSecondaryLicenseCategoryRepository(_testUnitOfWork);

            //MasterCategoryDocumentRepository Initialization
            _masterCategoryDocument = new MasterCategoryDocumentRepository(_testUnitOfWork);

            //MasterCategoryQuestionRepository Initialization
            _mockMasterCategoryQuestionRepository = new MasterCategoryQuestionRepository(_testUnitOfWork);

            //FixFeeRepository Initialization
            _fixedFeeTestRepository = new FixFeeRepository(_testUnitOfWork);

            //SubmissionQuestionRepository Initialization
            _submissionQuestionTestRepository = new SubmissionQuestionRepository(_testUnitOfWork);

            //SubmissionMasterApplicationChcekListRepository Initialization
            _checkListTestRepository = new SubmissionMasterApplicationChcekListRepository(_testUnitOfWork);
            _masterrenewalstatusfee = new MasterRenewalStatusFeeRepository(_testUnitOfWork);
            //SubmissionMasterRenewalRepository Initialization
            _submissionMasterRenewalRepository = new SubmissionMasterRenewalRepository(_testUnitOfWork, _masterrenewalstatusfee);

            //IDCBC_ENTITY_BBL_Renewal_InvoiceRepository Initialization
        //    _iDCBC_ENTITY_BBL_Renewal_InvoiceReposiotory = new Mock<IDCBC_ENTITY_BBL_Renewal_InvoiceRepository>();
            _dCBC_ENTITY_BBL_Renewal_InvoiceTestRepository = new DCBC_ENTITY_BBL_Renewal_InvoiceRepository(_testUnitOfWork,
           _Lookup_ExistingCategoriesRepository);
            //MasterPrimaryCategoryRepository Initialization
            _mockMasterPrimaryCategoryRepository = new MasterPrimaryCategoryRepository(_testUnitOfWork,
                             _mockMasterSecondaryLicenseCategoryRepository,
                             _masterCategoryDocument, _mockMasterCategoryQuestionRepository);

            //OSubCategoryFeesRepository Initialization
            _mockSubCategoryFeesRepository = new OSubCategoryFeesRepository(_testUnitOfWork, _mockMasterPrimaryCategoryRepository,
              _mockMasterSecondaryLicenseCategoryRepository);

            //MasterSubCategoryRepository Initialization
            _MasterSubCategoryTestRepository = new MasterSubCategoryRepository(_testUnitOfWork,
                                                                               _mockMasterPrimaryCategoryRepository,
                                                                               _mockMasterSecondaryLicenseCategoryRepository);

            //MasterCategoryPhysicalLocationRepository Initialization
            _masterCategoryPhysicalLocationTestRepository = new MasterCategoryPhysicalLocationRepository(_testUnitOfWork,
                                                            _mockMasterPrimaryCategoryRepository,
                                                            _mockMasterCategoryQuestionRepository
                                                            , _mockSubCategoryFeesRepository
                                                            , _mockMasterSecondaryLicenseCategoryRepository);

            //FeeCodeMapRepository Initialization
            _feeCodeMapTestRepository = new FeeCodeMapRepository(_testUnitOfWork, _mockSubCategoryFeesRepository);

            //SubmissionCategoryRepository Initialization
            _submissionCategoryTestRepository = new SubmissionCategoryRepository(_testUnitOfWork, _mockMasterPrimaryCategoryRepository,
                _mockMasterSecondaryLicenseCategoryRepository,
                _mockSubCategoryFeesRepository, _fixedFeeTestRepository,
                _submissionQuestionTestRepository, _MasterSubCategoryTestRepository, _masterCategoryPhysicalLocationTestRepository,
                _feeCodeMapTestRepository, _checkListTestRepository, _submissionMasterRenewalRepository, _dCBC_ENTITY_BBL_Renewal_InvoiceTestRepository,
               _Lookup_ExistingCategoriesRepository);

            var feecode = new FeeCodeMapRepositoryData();
            _testDbContext.FeeCodeMap.AddRange(feecode.FeeCodeMapEntitiesList);
            _testDbContext.SaveChanges();


            var renewinvoicedata = new DCBC_ENTITY_BBL_Renewal_InvoiceData();
            _testDbContext.DCBC_ENTITY_BBL_Renewal_Invoice.AddRange(renewinvoicedata.DcbcEntityBblRenewalsList);
            _testDbContext.SaveChanges();

            //Setup  SubmissionCategory FackData Initialization
            var testData = new SubmissionCategoryRepositoryData();
            _testDbContext.SubmissionCategory.AddRange(testData.SubmissionCategoryEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  MasterPrimaryCategory FackData Initialization
            var masterPriamryCategoryData = new MasterPrimaryCategoryRepositoryData();
            _testDbContext.MasterPrimaryCategory.AddRange(masterPriamryCategoryData.MasterPrimaryCategoryEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  MasterSecondaryLicenseCategory FackData Initialization
            var masterSecondaryCategoryData = new MasterSecondaryLicenseCategoryRepositoryData();
            _testDbContext.MasterSecondaryLicenseCategory.AddRange(masterSecondaryCategoryData.MasterSeconderyLicenseCategoryList);
            _testDbContext.SaveChanges();

            //Setup  MasterCategoryDocument FackData Initialization
            var masterCategoryDocumentRepositoryData = new MasterCategoryDocumentRepositoryData();
            _testDbContext.MasterCategoryDocument.AddRange(masterCategoryDocumentRepositoryData.MasterCategoryDocumentList);
            _testDbContext.SaveChanges();


            //Setup  MasterCategoryQuestion FackData Initialization
            var masterCategoryQuestionData = new MasterCategoryQuestionData();
            _testDbContext.MasterCategoryQuestion.AddRange(masterCategoryQuestionData.CategoryQuestionEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  OSub_Category_Fees FackData Initialization
            var oSubCategoryFeesData = new OSubCategoryFeesData();
            _testDbContext.OSub_Category_Fees.AddRange(oSubCategoryFeesData.OSubCategoryFeesEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  FixFee FackData Initialization
            var fixFeeRepositoryData = new FixFeeRepositoryData();
            _testDbContext.FixFee.AddRange(fixFeeRepositoryData.FixFeeEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  SubmissionQuestion FackData Initialization
            var submissionQuestionRepositoryData = new SubmissionQuestionRepositoryData();
            _testDbContext.SubmissionQuestion.AddRange(submissionQuestionRepositoryData.SubmissionQuestionEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  MasterSubCategory FackData Initialization
            var masterSubCategoryRepositoryData = new MasterSubCategoryRepositoryData();
            _testDbContext.MasterSubCategory.AddRange(masterSubCategoryRepositoryData.MasterSubCategoryList);
            _testDbContext.SaveChanges();

            //Setup  MasterCategoryPhysicalLocation FackData Initialization
            var masterCategoryPhysicalLocationData = new MasterCategoryPhysicalLocationData();
            _testDbContext.MasterCategoryPhysicalLocation.AddRange(masterCategoryPhysicalLocationData.MasterCategoryPhysicalLocationList);
            _testDbContext.SaveChanges();

            //Setup  FeeCodeMap FackData Initialization
            var feeCodeMapRepositoryData = new FeeCodeMapRepositoryData();
            _testDbContext.FeeCodeMap.AddRange(feeCodeMapRepositoryData.FeeCodeMapEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  SubmissionMaster_ApplicationCheckList FackData Initialization
            var submissionMasterApplicationChcekListRepositoryData = new SubmissionMasterApplicationChcekListRepositoryData();
            _testDbContext.SubmissionMaster_ApplicationCheckList.AddRange(submissionMasterApplicationChcekListRepositoryData.SubmissionMaster_ApplicationCheckListEntitiesList);
            _testDbContext.SaveChanges();

            var lookupdata = new Lookup_ExistingCategoriesData();
            _testDbContext.Lookup_ExistingCategories.AddRange(lookupdata.ExistingCategoriesList);
            _testDbContext.SaveChanges();
            
        }

        [TestMethod]
        public void GetAll_SubmissionCategories_Return_Test()
        {
            //Act 
            var primaryCategory = _submissionCategoryTestRepository.AllSubmissionCategories();

            //Assert
            Assert.IsNotNull(primaryCategory); // Test if null
            Assert.IsTrue(primaryCategory.Count() == 8);
        }

        [TestMethod]
        public void GetFindById_RecordsExist_Test()
        {
            //Initialize Entites
            var submissionCategoryModel = new SubmissionCategoryModel { SubmissionCategoryId = 1 };

            //Act 
            var submissionCategoryRows = _submissionCategoryTestRepository.FindByID(submissionCategoryModel);

            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
            Assert.IsTrue(submissionCategoryRows.Count() == 1);
        }

        [TestMethod]
        public void GetFindById_RecordsDoesnotExist_Test()
        {
            //Initialize Entites
            var submissionCategoryModel = new SubmissionCategoryModel { SubmissionCategoryId = 8 };

            //Act 
            var submissionCategoryRows = _submissionCategoryTestRepository.FindByID(submissionCategoryModel);

            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
           Assert.IsTrue(submissionCategoryRows.Count()==1);
        }

        [TestMethod]
        public void Get_SubmissionCategoryListWithStatus_Test()
        {
            //Initialize Entites
            var submissionCategoryList = new SubmissionCategoryList
            {
                Status = false,
                CategoryName = ""
            };
           
            //Act 
            var submissionCategoryRows = _submissionCategoryTestRepository.SubmissionCategoryListWithStatus(submissionCategoryList, "cce0b056-d2a3-485e-a02a-e34c957c4e40");

            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
            Assert.IsTrue(submissionCategoryRows.CategoryName == "Hotel, APARTMENT, Restaurant");
        }

        // 38de17fc-4254-400f-a889-09a665c9adda
        [TestMethod]
        public void Get_SubmissionCategoryListWithStatus_SubCategory_Test()
        {
            //Initialize Entites
            var submissionCategoryList = new SubmissionCategoryList
            {
                Status = false,
                CategoryName = ""
            };

            //Act 
            var submissionCategoryRows = _submissionCategoryTestRepository.SubmissionCategoryListWithStatus(submissionCategoryList, "38de17fc-4254-400f-a889-09a665c9adda");

            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
            Assert.IsTrue(submissionCategoryRows.SubCategory == "Shoe Cleaning/Repair");
        }

        [TestMethod]
        public void InsertPrimaryBbl_Test()
        {
            //Initialize Entites
            var detailedCategoryList = new List<DetailedCategoryList>()
            {
                new DetailedCategoryList()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14ED4",
                    EndorsementFee = Convert.ToDecimal(25.0000),
                    Endorsement = "General Sales"
                },
                 new DetailedCategoryList()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14EER",
                     EndorsementFee = Convert.ToDecimal(55.0000),
                    Endorsement = "Inspected Sales and Services"
                }
            };

            var screenQuestionList = new List<ScreeningQuestion>()
            {
                new ScreeningQuestion()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14ED4",
                    
                },
                  new ScreeningQuestion()
                {
                     CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14EER"
                }
            };

            var subQuestion = new SubmissionQuestion();

            var submissionApplication = new SubmissionApplication
            {
                // Primary Category
                PrimaryID = "792734C3-4002-4C8D-8704-E3FDB2E14ED4",
                MasterId = "38de17fc-4254-400f-a889-09a665c9adda",
                DetailedCategoryList = detailedCategoryList,
                SubQuestion = screenQuestionList
            };

            // Act 
            var submissionCategoryRows =
                _submissionCategoryTestRepository.InsertPrimaryBbl(submissionApplication);

            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
            Assert.IsTrue(submissionCategoryRows);
        }

        [TestMethod]
        public void InsertPrimaryBblWith_PrimaryCategoryalreadyexists_Test()
        {
            //Initialize Entites
            var detailedCategoryList = new List<DetailedCategoryList>()
            {
                new DetailedCategoryList()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14ED4",
                    EndorsementFee = Convert.ToDecimal(25.0000),
                    Endorsement = "General Sales"
                },
                 new DetailedCategoryList()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14EER",
                     EndorsementFee = Convert.ToDecimal(55.0000),
                    Endorsement = "Inspected Sales and Services"
                }
            };

            var screenQuestionList = new List<ScreeningQuestion>()
            {
                new ScreeningQuestion()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14ED4",
                    
                },
                  new ScreeningQuestion()
                {
                     CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14EER"
                }
            };

            var subQuestion = new SubmissionQuestion();

            var submissionApplication = new SubmissionApplication
            {
                // Primary Category
                PrimaryID = "792734C3-4002-4C8D-8704-E3FDB2E14ED4",
                MasterId = "38de17fc-4254-400f-a889-09a665c9adda",
                DetailedCategoryList = detailedCategoryList,
                SubQuestion = screenQuestionList
            };

            // Act 
            var submissionCategoryRows =
                _submissionCategoryTestRepository.InsertPrimaryBbl(submissionApplication);

            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
            Assert.IsTrue(submissionCategoryRows);
        }

        //  public bool InsertSecondaryBbl(SubmissionApplication submissionApp)
        [TestMethod]
        public void InsertSecondaryBbl_Test()
        {
            //Initialize Entites
            var detailedCategoryList = new List<DetailedCategoryList>()
            {
                new DetailedCategoryList()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14ED4",
                    EndorsementFee = Convert.ToDecimal(25.0000),
                    Endorsement = "General Sales"
                },
                 new DetailedCategoryList()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14EER",
                     EndorsementFee = Convert.ToDecimal(55.0000),
                    Endorsement = "Inspected Sales and Services"
                }
            };

            var screenQuestionList = new List<ScreeningQuestion>()
            {
                new ScreeningQuestion()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14ED4",
                    
                },
                  new ScreeningQuestion()
                {
                     CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14EER"
                }
            };

            var submissionApplication = new SubmissionApplication
            {
                // Primary Category
                PrimaryID = "38de17fc-4254-400f-a889-09a665c9adda",
                MasterId = "9F92454C-ED4C-4062-B557-4125813B2BDD",
                Secondary = "792734C3-4002-4C8D-8704-E3FDB2E14ED4,792734C3-4002-4C8D-8704-E3FDB2E14EER",
                DetailedCategoryList = detailedCategoryList,
                SubQuestion = screenQuestionList
            };


            // Act 
            var submissionCategoryRows =
                _submissionCategoryTestRepository.InsertSecondaryBbl(submissionApplication);

            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
            Assert.IsTrue(submissionCategoryRows);
        }

        [TestMethod]
        public void InsertSecondaryBbl_PrimaryCategoryalreadyexists_Test()
        {
            //Initialize Entites
            var detailedCategoryList = new List<DetailedCategoryList>()
            {
                new DetailedCategoryList()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14ED4",
                    EndorsementFee = Convert.ToDecimal(25.0000),
                    Endorsement = "General Sales"
                },
                 new DetailedCategoryList()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14EER",
                     EndorsementFee = Convert.ToDecimal(55.0000),
                    Endorsement = "Inspected Sales and Services"
                }
            };

            var screenQuestionList = new List<ScreeningQuestion>()
            {
                new ScreeningQuestion()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14ED4",
                    
                },
                  new ScreeningQuestion()
                {
                     CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14EER"
                }
            };

            var submissionApplication = new SubmissionApplication
            {
                // Primary Category
                PrimaryID = "38de17fc-4254-400f-a889-09a665c9adda",
                MasterId = "9F92454C-ED4C-4062-B557-4125813B2BDD",
                Secondary = "Second Hand Dealers (B),Auction Sales Annual",
                DetailedCategoryList = detailedCategoryList,
                SubQuestion = screenQuestionList
            };

            // Act 
            var submissionCategoryRows =
                _submissionCategoryTestRepository.InsertSecondaryBbl(submissionApplication);

            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
            Assert.IsTrue(submissionCategoryRows);
        }

        [TestMethod]
        public void InsertSubSubCagteogryBbl_Test()
        {
            //Initialize Entites
            var detailedCategoryList = new List<DetailedCategoryList>()
            {
                new DetailedCategoryList()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14ED4",
                    EndorsementFee = Convert.ToDecimal(25.0000),
                    Endorsement = "General Sales"
                },
                 new DetailedCategoryList()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14EER",
                     EndorsementFee = Convert.ToDecimal(55.0000),
                    Endorsement = "Inspected Sales and Services"
                }
            };

            var screenQuestionList = new List<ScreeningQuestion>()
            {
                new ScreeningQuestion()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14ED4",
                    
                },
                  new ScreeningQuestion()
                {
                     CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14EER"
                }
            };

            var submissionApplication = new SubmissionApplication
            {
                // Primary Category
                PrimaryID = "38de17fc-4254-400f-a889-09a665c9adda",
                MasterId = "9F92454C-ED4C-4062-B557-4125813B2BDD",
                Secondary = "Second Hand Dealers (B),Auction Sales Annual",
                DetailedCategoryList = detailedCategoryList,
                SubQuestion = screenQuestionList
            };


            // Act 
            var submissionCategoryRows =
                _submissionCategoryTestRepository.InsertSecondaryBbl(submissionApplication);

            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
            Assert.IsTrue(submissionCategoryRows);
        }

        //   public bool InsertSubSubCagteogryBbl(SubmissionApplication submissionApp)
        [TestMethod]
        public void InsertSubSubCagteogryBbl_PrimaryCategoryalreadyexists_Test()
        {
            //Initialize Entites
            var detailedCategoryList = new List<DetailedCategoryList>()
            {
                new DetailedCategoryList()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14ED4",
                    EndorsementFee = Convert.ToDecimal(25.0000),
                    Endorsement = "General Sales"
                },
                 new DetailedCategoryList()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14EER",
                     EndorsementFee = Convert.ToDecimal(55.0000),
                    Endorsement = "Inspected Sales and Services"
                }
            };

            var screenQuestionList = new List<ScreeningQuestion>()
            {
                new ScreeningQuestion()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14ED4",
                    
                },
                  new ScreeningQuestion()
                {
                     CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14EER"
                }
            };

            var submissionApplication = new SubmissionApplication
            {
                // Primary Category
                PrimaryID = "38de17fc-4254-400f-a889-09a665c9adda",
                MasterId = "9F92454C-ED4C-4062-B557-4125813B2BDD",
                Secondary = "General Business Licenses",
                SubSubCategory = "891F943E-D4D9-4660-872C-26366BB1C197",
                DetailedCategoryList = detailedCategoryList,
                SubQuestion = screenQuestionList
            };


            // Act 
            var submissionCategoryRows =
                _submissionCategoryTestRepository.InsertSubSubCagteogryBbl(submissionApplication);

            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
            Assert.IsTrue(submissionCategoryRows);
        }

        //   public string GetLicenseDuration(SubmissionApplication submissionApp)
        [TestMethod]
        public void GetLicenseDuration_PrimaryCategoryalreadyexists_Test()
        {
            //Initialize Entites
            var detailedCategoryList = new List<DetailedCategoryList>()
            {
                new DetailedCategoryList()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14ED4",
                    EndorsementFee = Convert.ToDecimal(25.0000),
                    Endorsement = "General Sales"
                },
                 new DetailedCategoryList()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14EER",
                     EndorsementFee = Convert.ToDecimal(55.0000),
                    Endorsement = "Inspected Sales and Services"
                }
            };

            var screenQuestionList = new List<ScreeningQuestion>()
            {
                new ScreeningQuestion()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14ED4",
                    Question = "Would you like a two (2) or four (4) year license?",
                    Answer = "2"
                    
                },
                  new ScreeningQuestion()
                {
                     CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14EER",
                     Question = "Will this business be located in the District of Columbia?",
                     Answer = "YES"
                }
            };

            var submissionApplication = new SubmissionApplication
            {
                // Primary Category
                PrimaryID = "38de17fc-4254-400f-a889-09a665c9adda",
                MasterId = "9F92454C-ED4C-4062-B557-4125813B2BDD",
                Secondary = "Second Hand Dealers (B),Auction Sales Annual",
                DetailedCategoryList = detailedCategoryList,
                SubQuestion = screenQuestionList
            };
            // Act 
            var submissionCategoryRows =
                _submissionCategoryTestRepository.GetLicenseDuration(submissionApplication);

            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
            Assert.IsTrue(submissionCategoryRows == "2");
        }

        //   public string ItemQuanity(SubmissionApplication submissionApp,string submissionCategoryId )
        [TestMethod]
        public void ItemQuanity_Test()
        {
            //Initialize Entites
            var detailedCategoryList = new List<DetailedCategoryList>()
            {
                new DetailedCategoryList()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14ED4",
                    EndorsementFee = Convert.ToDecimal(25.0000),
                    Endorsement = "General Sales"
                },
                 new DetailedCategoryList()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14EER",
                     EndorsementFee = Convert.ToDecimal(55.0000),
                    Endorsement = "Inspected Sales and Services"
                }
            };

            var screenQuestionList = new List<ScreeningQuestion>()
            {
                new ScreeningQuestion()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14ED4",
                    Question = "Would you like a two (2) or four (4) year license?",
                    Answer = "2"
                    
                },
                  new ScreeningQuestion()
                {
                     CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14EER",
                     Question = "Will this business be located in the District of Columbia?",
                     Answer = "YES"
                }
            };

            var submissionApplication = new SubmissionApplication
            {
                // Primary Category
                PrimaryID = "38de17fc-4254-400f-a889-09a665c9adda",
                MasterId = "9F92454C-ED4C-4062-B557-4125813B2BDD",
                Secondary = "Second Hand Dealers (B),Auction Sales Annual",
                DetailedCategoryList = detailedCategoryList,
                SubQuestion = screenQuestionList
            };

            // Act 
            var submissionCategoryRows =
                _submissionCategoryTestRepository.ItemQuanity(submissionApplication, "792734C3-4002-4C8D-8704-E3FDB2E14ED4");

            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
            Assert.IsTrue(submissionCategoryRows == "2");
        }

        //   public SubmissionApplication GetTotalFees(SubmissionApplication submissionApp)
        [TestMethod]
        public void GetTotalFees_ForFeeExempt_Test()
        {
            //Initialize Entites
            var detailedCategoryList = new List<DetailedCategoryList>()
            {
                new DetailedCategoryList()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14ED4",
                    EndorsementFee = Convert.ToDecimal(25.0000),
                    Endorsement = "General Sales"
                },
                 new DetailedCategoryList()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14EER",
                     EndorsementFee = Convert.ToDecimal(55.0000),
                    Endorsement = "Inspected Sales and Services"
                }
            };

            var screenQuestionList = new List<ScreeningQuestion>()
            {
                new ScreeningQuestion()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14ED4",
                    Question = "Would you like a two (2) or four (4) year license?",
                    Answer = "TWO (2) YEAR"
                    
                },
                  new ScreeningQuestion()
                {
                     CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14EER",
                     Question = "Will this business be located in the District of Columbia?",
                     Answer = "YES"
                }
            };

            var submissionApplication = new SubmissionApplication
            {
                // Primary Category
                PrimaryID = "BDABCA0B-8B80-4CC6-8836-6D5A751B6C54",
                MasterId = "9F92454C-ED4C-4062-B557-4125813B2BDD",
                Secondary = "1B69B449-ECAB-41AE-89A9-E2F76329A52B",
                DetailedCategoryList = detailedCategoryList,
                SubQuestion = screenQuestionList
            };

            // Act 
            var submissionCategoryRows =
                _submissionCategoryTestRepository.GetTotalFees(submissionApplication);

            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
            Assert.IsTrue(submissionCategoryRows.TotalFee == 0);
        }

        [TestMethod]
        public void GetTotalFees_Test()
        {
            //Initialize Entites
            var detailedCategoryList = new List<DetailedCategoryList>()
            {
                new DetailedCategoryList()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14ED4",
                    EndorsementFee = Convert.ToDecimal(25.0000),
                    Endorsement = "General Sales"
                },
                 new DetailedCategoryList()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14EER",
                     EndorsementFee = Convert.ToDecimal(55.0000),
                    Endorsement = "Inspected Sales and Services"
                }
            };

            var screenQuestionList = new List<ScreeningQuestion>()
            {
                new ScreeningQuestion()
                {
                    CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14ED4",
                    Question = "Would you like a two (2) or four (4) year license?",
                    Answer = "TWO (2) YEAR"
                    
                },
                  new ScreeningQuestion()
                {
                     CategoryId = "792734C3-4002-4C8D-8704-E3FDB2E14EER",
                     Question = "Will this business be located in the District of Columbia?",
                     Answer = "YES"
                }
            };

            var submissionApplication = new SubmissionApplication
            {
                // Primary Category
                PrimaryID = "6C548EBE-59ED-40B8-BEBE-EDF79149295D",
                MasterId = "9F92454C-ED4C-4062-B557-4125813B2BDD",
                Secondary = "1DE53FC4-F444-44EA-99A4-5DA3F107D5DD",
                DetailedCategoryList = detailedCategoryList,
                SubQuestion = screenQuestionList
            };

            // Act 
            var submissionCategoryRows =
                _submissionCategoryTestRepository.GetTotalFees(submissionApplication);

            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
            Assert.IsTrue(submissionCategoryRows.TotalFee == 132);
            Assert.IsTrue(submissionCategoryRows.LicenseCategory == "Hotel");
        }

        //  public IEnumerable<SubmissionCategory> FindbyMaster(string masterid)
        [TestMethod]
        public void GetFindbyMaster_Test()
        {
            //Act 
            var submissionCategoryRows = _submissionCategoryTestRepository.FindbyMaster("8c6deecc-3b72-485d-8af5-30939af94e97");

            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
            Assert.IsTrue(submissionCategoryRows.Count() == 2);
        }

        //  public ServiceChecklist ServiceCheckList(ServiceChecklist servicechecklist)
        [TestMethod]
        public void ServiceCheckList_Test()
        {
            //Initialize Entites
            ServiceChecklist servicechecklist = new ServiceChecklist()
            {
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
              
            };
            //Act 
            var submissionCategoryRows = _submissionCategoryTestRepository.ServiceCheckList(servicechecklist);

            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
            Assert.IsTrue(submissionCategoryRows.TotalFee == Convert.ToDecimal(72.6));
        }


        //   public decimal InsertRenewalData(RenewModel renewModel)
        [TestMethod]
        public void InsertRenewalData_Test()
        {
            //Initialize Entites
            var renewModel = new RenewModel
            {
                MasterId = "38de17fc-4254-400f-a889-09a665c9adda",
                CategoryId = "6C548EBE-59ED-40B8-BEBE-EDF79149295D",
                LrenNumber = "LREN11002680",
                Endoresement = "Housing: Transient",
                CategoryName = "General Business|Restaurant",
                ActivityName = "Real Estate & Rentals",

              
            };

            // Act 
            var submissionCategoryRows =
                _submissionCategoryTestRepository.InsertRenewalData(renewModel);

            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
          Assert.IsTrue(submissionCategoryRows==0);
        }

        [TestMethod]
        public void InsertRenewalData_New_Test()
        {
            //Initialize Entites
            var renewModel = new RenewModel
            {
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
                CategoryId = "BDABCA0B-8B80-4CC6-8836-6D5A751B6C54",
                LrenNumber = "LREN13018589",
                
            };

            // Act 
            var submissionCategoryRows =
                _submissionCategoryTestRepository.InsertRenewalData(renewModel);

            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
            //  Assert.IsTrue(submissionCategoryRows);
        }

        //  public IEnumerable<SubmissionCategory> FindByMasterId(string masterid, string endorsment)
        [TestMethod]
        public void FindByMasterIdTest()
        {
            //Initialize Entites
            string masterid = "cce0b056-d2a3-485e-a02a-e34c957c4e40";
            string endorsment = "Housing: Transient";

            //Act 
            var submissionCategoryRows = _submissionCategoryTestRepository.FindByMasterId(masterid, endorsment);

            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
            Assert.IsTrue(submissionCategoryRows.Count() == 1);
        }

        //     public decimal CalculationAmount(int quantity, string categoryname, string unittype, string itemquantity)
        [TestMethod]
        public void CalculationAmount_Test()
        {
            //Initialize Entites
            int quantity = 4;
            string categoryname = "Restaurant";
            string unittype = "Rooms";
            string itemquantity = "4";

            // Act 
            var submissionCategoryRows =
                _submissionCategoryTestRepository.CalculationAmount(quantity, categoryname, unittype, itemquantity);

            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
             Assert.IsTrue(submissionCategoryRows==450);
        }

        [TestMethod]
        public void CalculationAmount_With_T_Test()
        {
            //Initialize Entites
            int quantity = 4;
            string categoryname = "Restaurant";
            string unittype = "Rooms";
            string itemquantity = "4";

            // Act 
            var submissionCategoryRows =
                _submissionCategoryTestRepository.CalculationAmount(quantity, categoryname, unittype, itemquantity);

            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
            Assert.IsTrue(submissionCategoryRows == 450);
        }
        
        [TestMethod]
        public void CalculationAmount_With_S_Test()
        {
            //Initialize Entites
            int quantity = 4;
            string categoryname = "General Business Licenses";
            string unittype = "";
            string itemquantity = "4";

            // Act 
            var submissionCategoryRows =
                _submissionCategoryTestRepository.CalculationAmount(quantity, categoryname, unittype, itemquantity);

            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
            Assert.IsTrue(submissionCategoryRows == 400);
        }

        //[TestMethod]
        //public void CalculationAmount_With_C_Test()
        //{
        //    int quantity = 4;
        //    string categoryname = "Hotel";
        //    string unittype = "Rooms";
        //    string itemquantity = "4";

        //    // Act 
        //    var submissionCategoryRows =
        //        _submissionCategoryTestRepository.CalculationAmount(quantity, categoryname, unittype, itemquantity);

        //    //Assert
        //    Assert.IsNotNull(submissionCategoryRows); // Test if null
        //    Assert.IsTrue(submissionCategoryRows == 84);
        //}

        [TestMethod]
        public void CalculationAmount_With_TA_Test()
        {
            //Initialize Entites
            int quantity = 4;
            string categoryname = "Food Vending Machine";
            string unittype = "Machines";
            string itemquantity = "4";

            // Act 
            var submissionCategoryRows =
                _submissionCategoryTestRepository.CalculationAmount(quantity, categoryname, unittype, itemquantity);

            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
            Assert.IsTrue(submissionCategoryRows == 21);
        }

        // public decimal LicenseAmount(string primarycategoryId, string itemquantity)
        [TestMethod]
        public void LicenseAmount_Test()
        {
            //Initialize Entites
            string primarycategoryId = "6C548EBE-59ED-40B8-BEBE-EDF79149295D";
            string itemquantity = "51,5";

            // Act 
            var submissionCategoryRows =
                _submissionCategoryTestRepository.LicenseAmount(primarycategoryId, itemquantity);

            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
            Assert.IsTrue(submissionCategoryRows == 0);
        }

        //    public decimal SecondLicenseAmount(string secondaryId, string itemquantity)
        [TestMethod]
        public void SecondLicenseAmount_Test()
        {
            string secondaryId = "";
            string itemquantity = "";

            // Act 
            var submissionCategoryRows =
                _submissionCategoryTestRepository.SecondLicenseAmount(secondaryId, itemquantity);

            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
            //  Assert.IsTrue(submissionCategoryRows);
        }

        [TestMethod]
        public void SecondLicenseAmountWithout_SecondaryNameExist_InMasterPrimary_Test()
        {
            string secondaryId = "42D8D51F-6B22-4E7B-BC7F-CE0EA2E2814B";
            string itemquantity = "4,8";

            // Act 
            var submissionCategoryRows =
                _submissionCategoryTestRepository.SecondLicenseAmount(secondaryId, itemquantity);

            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
            Assert.IsTrue(submissionCategoryRows == 0);
        }

        [TestMethod]
        public void SecondLicenseAmount_SecondaryNameExist_EmptyItemQuantity_Test()
        {
            string secondaryId = "42D8D51F-6B22-4E7B-BC7F-CE0EA2E2814B";
            string itemquantity = "";

            // Act 
            var submissionCategoryRows =
                _submissionCategoryTestRepository.SecondLicenseAmount(secondaryId, itemquantity);

            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
            Assert.IsTrue(submissionCategoryRows == 0);
        }

        [TestMethod]
        public void SecondLicenseAmount_SecondaryNameExist_ItemQuantityExist_Test()
        {
            string secondaryId = "891F943E-D4D9-4660-872C-26366BB1C197";
            string itemquantity = "100";

            // Act 
            var submissionCategoryRows =
                _submissionCategoryTestRepository.SecondLicenseAmount(secondaryId, itemquantity);

            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
          //  Assert.IsTrue(submissionCategoryRows == 0);
        }

        [TestMethod]
        public void CheckunitsTrueTest()
        {
            //Act 
            var submissionCategoryRows = _submissionCategoryTestRepository.Checkunits("ROOMS");
            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
            Assert.IsTrue(submissionCategoryRows);
        }

        [TestMethod]
        public void CheckunitsFalseTest()
        {
            //Act 
            var submissionCategoryRows = _submissionCategoryTestRepository.Checkunits("Room");
            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
            Assert.IsTrue(submissionCategoryRows == false);
        }

          [TestMethod]
        public void GetSubCategory_Test()
        {
            //Act 
            var submissionCategoryRows = _submissionCategoryTestRepository.GetSubCategory("0495A756-2E3C-4444-8B6F-993445502401");
            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
            Assert.IsTrue(submissionCategoryRows == "Shoe Cleaning/Repair");
        }
         [TestMethod]
          public void GetSuperSubCategoryName_Test()
        {

            var submissionapp = new SubmissionApplication
            {
                SubSubCategory = "0495A756-2E3C-4444-8B6F-993445502401"
            };
            //Act 
            var submissionCategoryRows = _submissionCategoryTestRepository.GetSuperSubCategoryName(submissionapp);
            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
            Assert.IsTrue(submissionCategoryRows == "Shoe Cleaning/Repair");
        }
         [TestMethod]
         public void CalculationAmount_With_C_Test()
         {
             //Initialize Entites
             int quantity = 4;
             string categoryname = "Hotel";
             string unittype = "Seats";
             string itemquantity = "4";

             // Act 
             var submissionCategoryRows =
                 _submissionCategoryTestRepository.CalculationAmount(quantity, categoryname, unittype, itemquantity);

             //Assert
             Assert.IsNotNull(submissionCategoryRows); // Test if null
             Assert.IsTrue(submissionCategoryRows == 84);
         }
         [TestMethod]
         public void CalculationAmount_With_C_APARTMENT_Test()
         {
             //Initialize Entites
             int quantity = 4;
             string categoryname = "APARTMENT";
             string unittype = "Flats";
             string itemquantity = "4";

             // Act 
             var submissionCategoryRows =
                 _submissionCategoryTestRepository.CalculationAmount(quantity, categoryname, unittype, itemquantity);

             //Assert
             Assert.IsNotNull(submissionCategoryRows); // Test if null
             Assert.IsTrue(submissionCategoryRows == 1816);
         }
         [TestMethod]
         public void CalculationAmount_With_C_APARTMENT_Null_Test()
         {
             //Initialize Entites
             int quantity = 4;
             string categoryname = "APARTMENT";
             string unittype = "Flats";
             string itemquantity =string.Empty;

             // Act 
             var submissionCategoryRows =
                 _submissionCategoryTestRepository.CalculationAmount(quantity, categoryname, unittype, itemquantity);

             //Assert
             Assert.IsNotNull(submissionCategoryRows); // Test if null
             Assert.IsTrue(submissionCategoryRows == 1687);
         }

         [TestMethod]
         public void CalculationAmount_WithTA_Test()
         {
             //Initialize Entites
             int quantity = 102;
             string categoryname = "Restaurant";
             string unittype = "Rooms";
             string itemquantity = "102";

             // Act 
             var submissionCategoryRows =
                 _submissionCategoryTestRepository.CalculationAmount(quantity, categoryname, unittype, itemquantity);

             //Assert
             Assert.IsNotNull(submissionCategoryRows); // Test if null
             Assert.IsTrue(submissionCategoryRows == 2243);
         }

         [TestMethod]
         public void CalculationAmount_With_NoUnits_Test()
         {
             //Initialize Entites
             int quantity = 102;
             string categoryname = "Restaurant";
             string unittype = "Kitchens";
             string itemquantity = "102";

             // Act 
             var submissionCategoryRows =
                 _submissionCategoryTestRepository.CalculationAmount(quantity, categoryname, unittype, itemquantity);

             //Assert
             Assert.IsNotNull(submissionCategoryRows); // Test if null
             Assert.IsTrue(submissionCategoryRows == 56762);
         }
         [TestMethod]
         public void CalculationAmount_With_NoTire_Test()
         {
             //Initialize Entites
             int quantity = 10;
             string categoryname = "ONE FAMILY RENTAL";
             string unittype = "";
             string itemquantity = "10";

             // Act 
             var submissionCategoryRows =
                 _submissionCategoryTestRepository.CalculationAmount(quantity, categoryname, unittype, itemquantity);

             //Assert
             Assert.IsNotNull(submissionCategoryRows); // Test if null
             Assert.IsTrue(submissionCategoryRows == 992);
         }
         [TestMethod]
         public void CalculationAmount_With_NoTire_one_family_rental_Test()
         {
             //Initialize Entites
             int quantity = 10;
             string categoryname = "ONE FAMILY RENTAL";
             string unittype = "";
             string itemquantity = "";

             // Act 
             var submissionCategoryRows =
                 _submissionCategoryTestRepository.CalculationAmount(quantity, categoryname, unittype, itemquantity);

             //Assert
             Assert.IsNotNull(submissionCategoryRows); // Test if null
             Assert.IsTrue(submissionCategoryRows == 605);
         }

         [TestMethod]
         public void CalculationAmount_With_Machines_TA_Test()
         {
             //Initialize Entites
             int quantity = 103;
             string categoryname = "Food Vending Machine";
             string unittype = "Machines";
             string itemquantity = "103";

             // Act 
             var submissionCategoryRows =
                 _submissionCategoryTestRepository.CalculationAmount(quantity, categoryname, unittype, itemquantity);

             //Assert
             Assert.IsNotNull(submissionCategoryRows); // Test if null
             Assert.IsTrue(submissionCategoryRows == 63);
         }
         [TestMethod]
         public void CalculationAmount_With_Machines_Solicitor_Test()
         {
             //Initialize Entites
             int quantity = 10;
             string categoryname = "Solicitor";
             string unittype = "";
             string itemquantity = "10";

             // Act 
             var submissionCategoryRows =
                 _submissionCategoryTestRepository.CalculationAmount(quantity, categoryname, unittype, itemquantity);

             //Assert
             Assert.IsNotNull(submissionCategoryRows); // Test if null
             Assert.IsTrue(submissionCategoryRows == 2055);
         }
    }
}
