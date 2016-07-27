using BusinessCenter.Data;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
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
using BusinessCenter.Tests.Setup;

namespace BusinessCenter.Tests.Repository
{
    [TestClass]
    public class SubmissionDocumentRepositoryTest
    {
        //Initialization DBConnection
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Initialization of Repositories
        private SubmissionDocumentRepository _submissionDocumentTestRepository;
        private SubmissionCategoryRepository _submissionCategoryRepository;
        private MasterCategoryDocumentRepository _masterCategoryDocumentRepository;
        private MasterPrimaryCategoryRepository _masterPrimaryCategoryRepository;
        private MasterSecondaryLicenseCategoryRepository _masterSecondaryLicenserRepository;
        private SubmissionMasterRepository _submissionMasterRepository;
        private MasterCategoryPhysicalLocationRepository _masterCategoryPhysicalLocationRepository;
        private SubmissionMasterApplicationChcekListRepository _mockSubmissionMasterApplicationChcekListrepo;
        private MasterCategoryQuestionRepository _masterCategoryQuestionRepository;
        private OSubCategoryFeesRepository _mockSubCategoryFeesRepository;
        private FixFeeRepository _fixedFeeTestRepository;
        private SubmissionQuestionRepository _submissionQuestionRepository;
        private MasterSubCategoryRepository _masterSubCategoryRepository;
        private FeeCodeMapRepository _feeCodeMapRepository;
        private SubmissionMasterRenewalRepository _submissionMasterRenewalRepository;
//private MasterLicenseFEINRenewal _masterLicenseFeinRenewal;
        private DCBC_ENTITY_BBL_Renewal_InvoiceRepository _dcbcEntityBblRenewalInvoiceRepository;
        private BblRepository _bblRepository;
        private UserBBLServiceRepository _userBblServiceRepository;
        private UserRepository _userRepository;
        private UserRoleRepository _userRoleRepository;
        private DCBC_ENTITY_BBL_RenewalsRepository _dcbcEntityBblRenewalsRepository;
        private MasterBusinessActivityRepository _masterBusinessActivityRepository;
        private SubmissionBblAssociationToUsersRepository _submissionBblAssociationToUsersRepository;
        private SubmissionToAccelaRepository _submissionToAccelaRepository;
        private SubmissionIndividualRepository _submissionIndividualRepository;
        private SubmissionDocumentToAccelaRepository _submissionDocumentToAccelaRepository;
        private Lookup_ExistingCategoriesRepository _lookupExistingCategoriesRepository;
       // private MasterLicenseFEINRenewal masterLicenseFeinRenewal;
        private SubmissionLicenseNumberCounterRepository _submissionlicencecounter;
        private MasterRenewalStatusFeeRepository _masterrenewalstatusfee;
        private MasterBblApplicationStatusRepository _masterBblApplicationStatusRepository;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _masterrenewalstatusfee = new MasterRenewalStatusFeeRepository(_testUnitOfWork);
          //  masterLicenseFeinRenewal = new MasterLicenseFEINRenewal(_testUnitOfWork);
            //MasterSecondaryLicenseCategoryRepository Initialization
            _masterSecondaryLicenserRepository = new MasterSecondaryLicenseCategoryRepository(_testUnitOfWork);
            _submissionlicencecounter = new SubmissionLicenseNumberCounterRepository(_testUnitOfWork);
            //MasterCategoryDocumentRepository Initialization
            _masterCategoryDocumentRepository = new MasterCategoryDocumentRepository(_testUnitOfWork);
            _masterBblApplicationStatusRepository=new MasterBblApplicationStatusRepository(_testUnitOfWork);
            //MasterCategoryQuestionRepository Initialization
            _masterCategoryQuestionRepository = new MasterCategoryQuestionRepository(_testUnitOfWork);

            //Lookup_ExistingCategoriesRepository Initialization
            _lookupExistingCategoriesRepository = new Lookup_ExistingCategoriesRepository(_testUnitOfWork);

            //FixFeeRepository Initialization
            _fixedFeeTestRepository = new FixFeeRepository(_testUnitOfWork);


            //SubmissionQuestionRepository Initialization
            _submissionQuestionRepository = new SubmissionQuestionRepository(_testUnitOfWork);
            //  dcbcEntitybblRenewalInvoiceRepository = new DCBC_ENTITY_BBL_Renewal_InvoiceReposiotory(_testUnitOfWork);

            //MasterLicenseFEINRenewal Initialization
          //  _masterLicenseFeinRenewal = new MasterLicenseFEINRenewal(_testUnitOfWork);

            //SubmissionMasterRenewalRepository Initialization
            _submissionMasterRenewalRepository = new SubmissionMasterRenewalRepository(_testUnitOfWork, _masterrenewalstatusfee);
            _dcbcEntityBblRenewalInvoiceRepository = new DCBC_ENTITY_BBL_Renewal_InvoiceRepository(_testUnitOfWork, _lookupExistingCategoriesRepository);

            //BblRepository Initialization
            _bblRepository = new BblRepository(_testUnitOfWork);

            //UserBBLServiceRepository Initialization
            _userBblServiceRepository = new UserBBLServiceRepository(_testUnitOfWork);

            //MasterPrimaryCategoryRepository Initialization
            _masterPrimaryCategoryRepository = new MasterPrimaryCategoryRepository(_testUnitOfWork,
                _masterSecondaryLicenserRepository,
                _masterCategoryDocumentRepository, _masterCategoryQuestionRepository);

            //MasterSubCategoryRepository Initialization
            _masterSubCategoryRepository = new MasterSubCategoryRepository(_testUnitOfWork, _masterPrimaryCategoryRepository, _masterSecondaryLicenserRepository);
            _mockSubCategoryFeesRepository = new OSubCategoryFeesRepository(_testUnitOfWork,
                _masterPrimaryCategoryRepository, _masterSecondaryLicenserRepository);

            //MasterCategoryPhysicalLocationRepository Initialization
            _masterCategoryPhysicalLocationRepository = new MasterCategoryPhysicalLocationRepository(_testUnitOfWork,
                _masterPrimaryCategoryRepository, _masterCategoryQuestionRepository, _mockSubCategoryFeesRepository, _masterSecondaryLicenserRepository);

            //FeeCodeMapRepository Initialization
            _feeCodeMapRepository = new FeeCodeMapRepository(_testUnitOfWork, _mockSubCategoryFeesRepository);

            //SubmissionMasterApplicationChcekListRepository Initialization
            _mockSubmissionMasterApplicationChcekListrepo = new SubmissionMasterApplicationChcekListRepository(_testUnitOfWork);

            //SubmissionCategoryRepository Initialization
            _submissionCategoryRepository = new SubmissionCategoryRepository(_testUnitOfWork, _masterPrimaryCategoryRepository,
                _masterSecondaryLicenserRepository, _mockSubCategoryFeesRepository, _fixedFeeTestRepository, _submissionQuestionRepository,
                _masterSubCategoryRepository,
                _masterCategoryPhysicalLocationRepository, _feeCodeMapRepository,
                _mockSubmissionMasterApplicationChcekListrepo, _submissionMasterRenewalRepository, _dcbcEntityBblRenewalInvoiceRepository, _lookupExistingCategoriesRepository);


            //UserRoleRepository Initialization
            _userRoleRepository = new UserRoleRepository(_testUnitOfWork);

            //UserRepository Initialization
            _userRepository = new UserRepository(_testUnitOfWork, _userRoleRepository);

            //DCBC_ENTITY_BBL_RenewalsRepository Initialization
            _dcbcEntityBblRenewalsRepository = new DCBC_ENTITY_BBL_RenewalsRepository(_testUnitOfWork);

            //MasterBusinessActivityRepository Initialization
            _masterBusinessActivityRepository = new MasterBusinessActivityRepository(_testUnitOfWork, _masterPrimaryCategoryRepository);

            //SubmissionBblAssociationToUsersRepository Initialization
            _submissionBblAssociationToUsersRepository = new SubmissionBblAssociationToUsersRepository(_testUnitOfWork);

            //SubmissionToAccelaRepository Initialization
            _submissionToAccelaRepository = new SubmissionToAccelaRepository(_testUnitOfWork);

            //SubmissionMasterRepository Initialization
            _submissionMasterRepository = new SubmissionMasterRepository(_testUnitOfWork, _submissionCategoryRepository,
                _bblRepository,
                _userBblServiceRepository, _submissionQuestionRepository, _masterCategoryPhysicalLocationRepository,
                _masterPrimaryCategoryRepository, _mockSubmissionMasterApplicationChcekListrepo, _userRepository
                , _dcbcEntityBblRenewalsRepository, _masterBusinessActivityRepository,
                _submissionBblAssociationToUsersRepository
                , _submissionToAccelaRepository, _masterSecondaryLicenserRepository, _lookupExistingCategoriesRepository,
                _submissionlicencecounter, _masterBblApplicationStatusRepository);

            //SubmissionIndividualRepository Initialization
            _submissionIndividualRepository = new SubmissionIndividualRepository(_testUnitOfWork, _submissionMasterRepository);

            //SubmissionDocumentToAccelaRepository Initialization
            _submissionDocumentToAccelaRepository = new SubmissionDocumentToAccelaRepository(_testUnitOfWork);

            //SubmissionDocumentRepository Initialization
            _submissionDocumentTestRepository = new SubmissionDocumentRepository(_testUnitOfWork,
                _submissionCategoryRepository,
                _masterCategoryDocumentRepository,
                _masterPrimaryCategoryRepository,
                _masterSecondaryLicenserRepository, _submissionMasterRepository,
                _masterCategoryPhysicalLocationRepository, _mockSubmissionMasterApplicationChcekListrepo,
                _submissionIndividualRepository, _submissionDocumentToAccelaRepository);

            //---------------------------------------------------------------------------------------------------------------------


            //Setup  SubmissionDocument FackData Initialization
            var testData = new SubmissionDocumentRepositoryData();
            _testDbContext.SubmissionDocument.AddRange(testData.SubmissionDocumentList);
            _testDbContext.SaveChanges();

            //Setup  SubmissionCategory FackData Initialization
            var submissionCategoryData = new SubmissionCategoryRepositoryData();
            _testDbContext.SubmissionCategory.AddRange(submissionCategoryData.SubmissionCategoryEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  MasterCategoryDocument FackData Initialization
            var masterCategoryDocumentData = new MasterCategoryDocumentRepositoryData();
            _testDbContext.MasterCategoryDocument.AddRange(masterCategoryDocumentData.MasterCategoryDocumentList);
            _testDbContext.SaveChanges();

            //Setup  MasterPrimaryCategory FackData Initialization
            var primaryCategoryData = new MasterPrimaryCategoryRepositoryData();
            _testDbContext.MasterPrimaryCategory.AddRange(primaryCategoryData.MasterPrimaryCategoryEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  MasterSecondaryLicenseCategory FackData Initialization
            var secondaryLicenseData = new MasterSecondaryLicenseCategoryRepositoryData();
            _testDbContext.MasterSecondaryLicenseCategory.AddRange(secondaryLicenseData.MasterSeconderyLicenseCategoryList);
            _testDbContext.SaveChanges();

            //Setup  MasterCategoryQuestion FackData Initialization
            var masterCategoryQuestionData = new MasterCategoryQuestionData();
            _testDbContext.MasterCategoryQuestion.AddRange(masterCategoryQuestionData.CategoryQuestionEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  OSub_Category_Fees FackData Initialization
            var testOsubcategoryFeeData = new OSubCategoryFeesData();
            _testDbContext.OSub_Category_Fees.AddRange(testOsubcategoryFeeData.OSubCategoryFeesEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  FixFee FackData Initialization
            var fixedFeeData = new FixFeeRepositoryData();
            _testDbContext.FixFee.AddRange(fixedFeeData.FixFeeEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  SubmissionMaster FackData Initialization
            var submissionData = new SubmissionMasterRepositoryData();
            _testDbContext.SubmissionMaster.AddRange(submissionData.SubmissionMasterEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  MasterCategoryPhysicalLocation FackData Initialization
            var masterCategoryPhysicalLocationData = new MasterCategoryPhysicalLocationData();
            _testDbContext.MasterCategoryPhysicalLocation.AddRange(masterCategoryPhysicalLocationData.MasterCategoryPhysicalLocationList);
            _testDbContext.SaveChanges();

            //Setup  MasterCategoryPhysicalLocation FackData Initialization
            var submissionMasterApplicationChcekListRepositoryData = new SubmissionMasterApplicationChcekListRepositoryData();
            _testDbContext.SubmissionMaster_ApplicationCheckList
                .AddRange(submissionMasterApplicationChcekListRepositoryData.SubmissionMaster_ApplicationCheckListEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  SubmissionQuestion FackData Initialization
            var submissionQuestionsData = new SubmissionQuestionRepositoryData();
            _testDbContext.SubmissionQuestion
                .AddRange(submissionQuestionsData.SubmissionQuestionEntitiesList);
            _testDbContext.SaveChanges();

            //Setup  MasterSubCategory FackData Initialization
            var msaterSubCategoryData = new MasterSubCategoryRepositoryData();
            _testDbContext.MasterSubCategory
                .AddRange(msaterSubCategoryData.MasterSubCategoryList);
            _testDbContext.SaveChanges();

            //Setup  FeeCodeMap FackData Initialization
            var feeCodeData = new FeeCodeMapRepositoryData();
            _testDbContext.FeeCodeMap
                .AddRange(feeCodeData.FeeCodeMapEntitiesList);
            _testDbContext.SaveChanges();

            var userBBlData = new UserBBLServiceRepositoryData();
            _testDbContext.UserBBLService.AddRange(userBBlData.UserBblServiceList);
            _testDbContext.SaveChanges();
        }

        [TestMethod]
        public void FindDocumentList_RecordExist_Test()
        {
            //Act 
            var submissionDocumentsRows =
                _submissionDocumentTestRepository.FindDocumentList("cce0b056-d2a3-485e-a02a-e34c957c4e40");

            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows.Count() == 5);
        }

        [TestMethod]
        public void FindDocumentList_RecordDoesNotExist_Test()
        {
            //Act 
            var submissionDocumentsRows =
                _submissionDocumentTestRepository.FindDocumentList("wew0b056-d2a3-485e-a02a-e34c957c4e40");

            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(!submissionDocumentsRows.Any());
        }

        [TestMethod]
        public void IsUploadStatus_Test()
        {
            //Initial Data is added.
            var bblServiceDocuments = new BblServiceDocuments
            {
                SubmissionCategoryID = 1,
                CategoryID = "1",
                DocRequired = "Department of Health (DOH)",
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Description = "All businesses that manufacture, wholesale, warehouse, distribute, sell or use toxic or flammable materials or provide services to large audiences (theatres, restaurants, and public halls) are subject to inspection to ensure the safety of customers, employees, and property."
            };

            //Act 
            var submissionDocumentsRows = _submissionDocumentTestRepository.IsUploadStatus(bblServiceDocuments).ToList();

            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows.Count() == 0);
        }

        //   public List<BblServiceDocuments> QuestionsList(QuestionsList questionsList)
        [TestMethod]
        public void QuestionsList_Test()
        {
            //Initial Data is added.
            var questionList = new QuestionsList()
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                CategoryName = "Hotel",
                SubmissionLicense = "DAPP15976221"
                //SubmissionCategoryID = 5,
                //CategoryTypeID = "",
                //Endorsement = "",
                //License = "",
                //CategoryCode = "",
                //LicenseName = "",
                //Type = "",
            };
            //Act 
            var submissionDocumentsRows =
                _submissionDocumentTestRepository.DocumentsList(questionList,"");

            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows.Count() == 1);
        }

        //   public bool UpdateSubmissionMaster(BblDocuments bbldoc)
        [TestMethod]
        public void UpdateSubmissionMaster_InPerson_Test()
        {
            //Initial Data is added.
            var bblDocuments = new BblDocuments
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                DocSubType = "IN"
            };
            //Act 
            var submissionDocumentsRows = _submissionDocumentTestRepository.UpdateSubmissionMaster(bblDocuments);
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows);
        }

        [TestMethod]
        public void UpdateSubmissionMaster_Online_Test()
        {
            //Initial Data is added.
            var bblDocuments = new BblDocuments
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                DocSubType = "ON"
            };
            //Act 
            var submissionDocumentsRows = _submissionDocumentTestRepository.UpdateSubmissionMaster(bblDocuments);
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows);
        }

        //     public UploadStatus InsertServiceDocuments(BblServiceDocuments bblServiceDocuments)
        [TestMethod]
        public void InsertServiceDocuments_NotExistingDocument_Test()
        {
            //Initial Data is added.
            var document = new BblServiceDocuments
            {
                SubmissionCategoryID = 8,
                CategoryID = "151",
                DocRequired = "Instructor's License",
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Description = "Applicant must furnish a Driving Instructor(s) license.",
                FileName = "5107_DAPP15986431_FEMS_FPID_FireMarshalInsp.pdf",
                FileLocation = "BBLUpload//"
            };
            //Act 
            var submissionDocumentsRows = _submissionDocumentTestRepository.InsertServiceDocuments(document);
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows.FileName == "5107_DAPP15986431_FEMS_FPID_FireMarshalInsp.pdf");
        }

        [TestMethod]
        public void InsertServiceDocuments_UpdateExistingDocument_Test()
        {
            //Initial Data is added.
            var document = new BblServiceDocuments
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                SubmissionCategoryID = 3,
                DocRequired = "Certified Food Supervisor Certification",
                FileName = "9313_DAPP15986431_DOH_HRLA_FoodSupCert12.pdf",
                FileLocation = "BBLUpload//",
                SubmissionId = "2",
                CategoryID = "304",

                Description = "Caterers must have Certified Food Supervisor(s) present on the premises during the hours the facility is open to the public. The name(s) and certification(s) of the food supervisor employee(s) must be provided to the Department of Health's inspectors.",
            };
            //Act 
            var submissionDocumentsRows = _submissionDocumentTestRepository.InsertServiceDocuments(document);
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows.FileName == "9313_DAPP15986431_DOH_HRLA_FoodSupCert12.pdf");
        }

        //  public bool DeleteDocuments(BblServiceDocuments bbldoc)
        [TestMethod]
        public void DeleteDocuments_Test()
        {
            //Initial Data is added.
            var document = new BblServiceDocuments
            {
                SubmissionCategoryID = 1,
                CategoryID = "151",
                DocRequired = "Fire Marshal Inspection Approval",
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Description = "All businesses that manufacture, wholesale, warehouse, distribute, sell or use toxic or flammable materials or provide services to large audiences (theatres, restaurants, and public halls) are subject to inspection to ensure the safety of customers, employees, and property."

            };
            //Act 
            var submissionDocumentsRows = _submissionDocumentTestRepository.DeleteDocuments(document);
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows);
        }

        // public List<BblServiceDocuments> RenewalDocument(RenewQuestionsList questionsList)
        [TestMethod]
        public void RenewalDocument_Test()
        {
            //Initial Data is added.
            RenewQuestionsList renewQuestionsList = new RenewQuestionsList()
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                CategoryName = "Hotel",
                SubmissionLicense = "",
                SubmissionCategoryID = 1,
                CategoryTypeID = "",
                Endorsement = "",
                License = "",
                CategoryCode = "",
                LicenseName = "",
                Type = "",
                IsCorpRegistration = true,
                IsCleanHands = true
            };
            //Act 
            var submissionDocumentsRows =
                _submissionDocumentTestRepository.RenewalDocument(renewQuestionsList);

            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows.Count() == 1);
        }


        //   public bool CheckDocument(DocumentCheck documentCheck)
        [TestMethod]
        public void CheckDocument_Empty_DocType_Test()
        {
            //Initial Data is added.
            var documentList = new List<BblServiceDocuments>
            {
                new BblServiceDocuments()
                {
                       SubmissionCategoryID = 1,
                CategoryID = "151",
                DocRequired = "Fire Marshal Inspection Approval",
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Description = "All businesses that manufacture, wholesale, warehouse, distribute, sell or use toxic or flammable materials or provide services to large audiences (theatres, restaurants, and public halls) are subject to inspection to ensure the safety of customers, employees, and property."
                },
                  new BblServiceDocuments()
                {
                       SubmissionCategoryID = 2,
                CategoryID = "304",
                DocRequired = "Certified Food Supervisor Certification",
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Description = "Caterers must have Certified Food Supervisor(s) present on the premises during the hours the facility is open to the public. The name(s) and certification(s) of the food supervisor employee(s) must be provided to the Department of Health's inspectors."
                }
            };
            var documentCheck = new DocumentCheck
            {
                DocumentList = documentList,
                DocType = "",
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40"
            };
            //Act 
            var submissionDocumentsRows = _submissionDocumentTestRepository.CheckDocument(documentCheck);
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows);
        }

        [TestMethod]
        public void CheckDocument_OnlineDocType_Test()
        {
            //Initial Data is added.
            var documentList = new List<BblServiceDocuments>
            {
                new BblServiceDocuments()
                {
                       SubmissionCategoryID = 1,
                CategoryID = "151",
                DocRequired = "Fire Marshal Inspection Approval",
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Description = "All businesses that manufacture, wholesale, warehouse, distribute, sell or use toxic or flammable materials or provide services to large audiences (theatres, restaurants, and public halls) are subject to inspection to ensure the safety of customers, employees, and property."
                },
                  new BblServiceDocuments()
                {
                       SubmissionCategoryID = 2,
                CategoryID = "304",
                DocRequired = "Certified Food Supervisor Certification",
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Description = "Caterers must have Certified Food Supervisor(s) present on the premises during the hours the facility is open to the public. The name(s) and certification(s) of the food supervisor employee(s) must be provided to the Department of Health's inspectors."
                }
            };
            var documentCheck = new DocumentCheck
            {
                DocumentList = documentList,
                DocType = "ON",
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40"
            };
            //Act 
            var submissionDocumentsRows = _submissionDocumentTestRepository.CheckDocument(documentCheck);
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows == false);
        }

        [TestMethod]
        public void CheckDocument_InPersonDocType_Test()
        {
            //Initial Data is added.
            var documentList = new List<BblServiceDocuments>
            {
                new BblServiceDocuments()
                {
                       SubmissionCategoryID = 1,
                CategoryID = "151",
                DocRequired = "Fire Marshal Inspection Approval",
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Description = "All businesses that manufacture, wholesale, warehouse, distribute, sell or use toxic or flammable materials or provide services to large audiences (theatres, restaurants, and public halls) are subject to inspection to ensure the safety of customers, employees, and property."
                },
                  new BblServiceDocuments()
                {
                       SubmissionCategoryID = 2,
                CategoryID = "304",
                DocRequired = "Certified Food Supervisor Certification",
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Description = "Caterers must have Certified Food Supervisor(s) present on the premises during the hours the facility is open to the public. The name(s) and certification(s) of the food supervisor employee(s) must be provided to the Department of Health's inspectors."
                }
            };
            var documentCheck = new DocumentCheck
            {
                DocumentList = documentList,
                DocType = "IN",
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40"
            };
            //Act 
            var submissionDocumentsRows = _submissionDocumentTestRepository.CheckDocument(documentCheck);
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows);
        }

        #region categorydocument
        //  public IEnumerable<MasterCategoryDocument> FindByDocID(int documentid)
        [TestMethod]
        public void FindByDocID_Test()
        {
            //Act 
            var submissionDocumentsRows = _submissionDocumentTestRepository.FindByDocID(10);
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows.Count() == 1);
        }

        //  public IEnumerable<MasterCategoryDocument> FindByDocBasedonDocId(int documentid)
        [TestMethod]
        public void FindByDocBasedonDocId_NoDocuments_Test()
        {
            //Act 
            var submissionDocumentsRows = _submissionDocumentTestRepository.FindByDocBasedonDocId(30);
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(!submissionDocumentsRows.Any());
        }

        [TestMethod]
        public void FindByDocBasedonDocId_Test()
        {
            //Act 
            var submissionDocumentsRows = _submissionDocumentTestRepository.FindByDocBasedonDocId(1);
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows.Count() == 1);
        }

        //      public IEnumerable<MasterCategoryDocument> FindByDocName(string categoryname)
        [TestMethod]
        public void FindByDocName_NoRecords_Test()
        {
            //Act 
            var submissionDocumentsRows = _submissionDocumentTestRepository.FindByDocName("Theater (Live)");
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(!submissionDocumentsRows.Any());
        }

        [TestMethod]
        public void FindByDocName_Test()
        {
            //Act 
            var submissionDocumentsRows = _submissionDocumentTestRepository.FindByDocName("Charitable Exempt");
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows.Count() == 4);
        }


        //  public IEnumerable<MasterCategoryDocument> FindByID(string categoryname)
        [TestMethod]
        public void FindByID_NoRecords_Test()
        {
            //Act 
            var submissionDocumentsRows = _submissionDocumentTestRepository.FindByID("Solid Waste Vehicle");
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(!submissionDocumentsRows.Any());
        }

        [TestMethod]
        public void FindByID_Test()
        {
            //Act 
            var submissionDocumentsRows = _submissionDocumentTestRepository.FindByID("Hotel");
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows.Count() == 1);
        }

        //  public IEnumerable<MasterCategoryDocument> FindByRenewID(string categoryname)
        [TestMethod]
        public void FindByRenewID_NoRecords_Test()
        {
            //Act 
            var submissionDocumentsRows = _submissionDocumentTestRepository.FindByRenewID("Used Car Buyer Seller");
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(!submissionDocumentsRows.Any());
        }

        [TestMethod]
        public void FindByRenewID_Test()
        {
            //Act 
            var submissionDocumentsRows = _submissionDocumentTestRepository.FindByRenewID("Hotel");
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows.Count() == 1);
        }

        //   public int InsertUpdateCategoryDocuments(MasterCategoryDocumentModel categoryDocumentModel)
        [TestMethod]
        public void InsertUpdateCategoryDocuments_ToInsertNewRecord_Test()
        {
            MasterCategoryDocumentModel masterCategoryDocumentModel = new MasterCategoryDocumentModel()
            {
                MasterCategoryDocId = 20,
                CategoryName = "Rooming House",
                InitialLicense = "Y",
                PostLicensure = "Y",
                Renewal = "Y",
                Agency = "DCRAS",
                Agency_FullName = "Department of Consumer and Regulatory Affairs (DCRA)",
                Div = "FPID",
                DivisionFullName = "Fire Prevention Inspection Division",
                SupportingDocuments = "Fire Marshal Inspection Approval",
                ShortDocName = "FireMarshalInsp",
                Description = "All businesses that manufacture, wholesale, warehouse, distribute, sell or use toxic or flammable materials or provide services to large audiences (theatres, restaurants, and public halls) are subject to inspection to ensure the safety of customers, employees, and property.",
                Status = true
            };
            //Act 
            var submissionDocumentsRows = _submissionDocumentTestRepository.InsertUpdateCategoryDocuments(masterCategoryDocumentModel);
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows == 1);
        }

        [TestMethod]
        public void InsertUpdateCategoryDocuments_ToUpdateExistingRecord_Test()
        {
            MasterCategoryDocumentModel masterCategoryDocumentModel = new MasterCategoryDocumentModel()
            {
                MasterCategoryDocId = 15,
                CategoryName = "Home Improvement Contractors abc",
                InitialLicense = "Y",
                PostLicensure = "Y",
                Renewal = "Y",
                Agency = "DCRAS",
                Agency_FullName = "Department of Consumer and Regulatory Affairs (DCRA)",
                Div = "FPID",
                DivisionFullName = "Fire Prevention Inspection Division",
                SupportingDocuments = "Fire Marshal Inspection Approval",
                ShortDocName = "FireMarshalInsp",
                Description = "All businesses that manufacture, wholesale, warehouse, distribute, sell or use toxic or flammable materials or provide services to large audiences (theatres, restaurants, and public halls) are subject to inspection to ensure the safety of customers, employees, and property.",
                Status = true
            };
            //Act 
            var submissionDocumentsRows = _submissionDocumentTestRepository.InsertUpdateCategoryDocuments(masterCategoryDocumentModel);
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows == 2);
        }

        // public bool DeleteCategoryDocument(MasterCategoryDocumentModel categoryDocumentModel)
        [TestMethod]
        public void DeleteCategoryDocument_Test()
        {
            MasterCategoryDocumentModel masterCategoryDocumentModel = new MasterCategoryDocumentModel()
            {
                MasterCategoryDocId = 18,
                CategoryName = "Rooming House",
                InitialLicense = "Y",
                PostLicensure = "Y",
                Renewal = "Y",
                Agency = "DCRAS",
                Agency_FullName = "Department of Consumer and Regulatory Affairs (DCRA)",
                Div = "FPID",
                DivisionFullName = "Fire Prevention Inspection Division",
                SupportingDocuments = "Fire Marshal Inspection Approval",
                ShortDocName = "FireMarshalInsp",
                Description = "All businesses that manufacture, wholesale, warehouse, distribute, sell or use toxic or flammable materials or provide services to large audiences (theatres, restaurants, and public halls) are subject to inspection to ensure the safety of customers, employees, and property.",
                Status = true
            };
            //Act 
            var submissionDocumentsRows = _submissionDocumentTestRepository.DeleteCategoryDocument(masterCategoryDocumentModel);
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows);
        }
        #endregion

        

        //  public IEnumerable<BblDocuments> DocumentList(BblDocuments bbldoc)
        [TestMethod]
        public void DocumentList_Test()
        {
            //Initial Data is added.
            var bblDocuments = new BblDocuments
             {
                 MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
                 //       public List<BblServiceDocuments> BblServiceDoc { get; set; }
                 IsIndividual = true,
                 IsFEIN = true,
                 DocSubType = "",
                 IsHop = true,
                 IsCof = true,
                 AppType = "",
                 BusinessStructure = "",
                 TradeName = "",
                 CategoryName = "",
                 IsCorporationDivision = true,
                 ISFEINSSN = true,
                 IsCleanHandsVerify = true,
                 IsCorporateRegistration = true,
                 IsBHAddress = true,
                 IsBPAddress = true,
                 IsMailAddress = true,
                 IsResidentAgent = true,
                 IsDocforCleanHands = true,
                 IsDocforCofo = true,
                 IsDocforHop = true,
                 IsDocforEhop = true,

                 IsSubmissionCofo = true,
                 IsSubmissionHop = true,
                 IsSubmissioneHop = true,
                 CheckedStatus = true,
                 IsSubmissionCorpReg = true,
                 IsSubmissionAgent = true,
                 IsHomeBased = true,
                 IsBusinessMustbeinDC = true,
                 IsMcofo = true,
                 CategoryCode = "",
                 IsIndividualValid = true,
                 IsValidateTextRevenue = true
             };

            //Act 
            var submissionCategoryRows = _submissionDocumentTestRepository.DocumentList(bblDocuments);

            //Assert
            Assert.IsNotNull(submissionCategoryRows); // Test if null
            Assert.IsTrue(submissionCategoryRows.Count() == 1);
        }

        //    public bool DeleteHopcofo(string masterid ,string description)
        [TestMethod]
        public void DeleteHopcofo__MasterIdNotExist_Test()
        {
            string masterid = "";
            string description = "Fire Marshal Inspection Approval";
            //Act 
            var submissionDocumentsRows = _submissionDocumentTestRepository.DeleteHopcofo(masterid, description);
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows);
        }

        [TestMethod]
        public void DeleteHopcofo__DescriptionNotExist_Test()
        {
            string masterid = "cce0b056-d2a3-485e-a02a-e34c957c4e40";
            string description = "";
            //Act 
            var submissionDocumentsRows = _submissionDocumentTestRepository.DeleteHopcofo(masterid, description);
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows);
        }

        [TestMethod]
        public void DeleteHopcofo__MasterId_AndDescription_NotExist_Test()
        {
            string masterid = "";
            string description = "";
            //Act 
            var submissionDocumentsRows = _submissionDocumentTestRepository.DeleteHopcofo(masterid, description);
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows);

        }

        [TestMethod]
        public void DeleteHopcofo__MasterId_AndDescription_Exist_Test()
        {
            string masterid = "cce0b056-d2a3-485e-a02a-e34c957c4e40";
            string description = "Fire Marshal Inspection Approval";
            //Act 
            var submissionDocumentsRows = _submissionDocumentTestRepository.DeleteHopcofo(masterid, description);
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows);
        }

        //      public bool DeleteRenewalDocument(RenewModel renewModel)
        [TestMethod]
        public void DeleteRenewalDocument__MasterIdNotExist_Test()
        {
            var renewModel = new RenewModel { MasterId = "38de17fc-4254-400f-a889-09a665c9adda" };
            //Act 
            var submissionDocumentsRows = _submissionDocumentTestRepository.DeleteRenewalDocument(renewModel);
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows == true);
        }

        [TestMethod]
        public void DeleteRenewalDocument__MasterIdExist_Test()
        {
            var renewModel = new RenewModel { MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40" };
            //Act 
            var submissionDocumentsRows = _submissionDocumentTestRepository.DeleteRenewalDocument(renewModel);
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows);
        }

        [TestMethod]
        public void DeleteRenewalDocument__WithEmpty_MasterId_Test()
        {
            var renewModel = new RenewModel { MasterId = "" };
            //Act 
            var submissionDocumentsRows = _submissionDocumentTestRepository.DeleteRenewalDocument(renewModel);
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows == true);
        }

        //  public IEnumerable<MasterCategoryDocument> FindByDocNameBasedonCategoryName(string categoryname)
        [TestMethod]
        public void FindByDocNameBasedonCategoryName_WithCateogoryNotExist_Test()
        {
            //Act 
            var submissionDocumentsRows =
                _submissionDocumentTestRepository.FindByDocNameBasedonCategoryName("Bed and Breakfast");

            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(!submissionDocumentsRows.Any());
        }

        [TestMethod]
        public void FindByDocNameBasedonCategoryName_WithCateogoryExist_Test()
        {
            //Act 
            var submissionDocumentsRows =
                _submissionDocumentTestRepository.FindByDocNameBasedonCategoryName("Hotel");

            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows.Count() == 2);
        }


        //  public bool DocumentInsertion(string masterid, string licenseNumber)
        [TestMethod]
        public void DocumentInsertion_Test()
        {
            // Act 
            var submissionDocumentsRows =
                _submissionDocumentTestRepository.DocumentInsertion("cce0b056-d2a3-485e-a02a-e34c957c4e40", "DAPP15985360");

            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows);
        }
         [TestMethod]
        public void DocumentInsertionAccelea_Test()
        {
            // Act 
            var submissionDocumentsRows =
                _submissionDocumentTestRepository.DocumentInsertion("4cb3a7f0-8e98-4b8d-86ae-a2d9dbe14959", "DAPP15997187");

            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows);
        }
       

        //  public bool ChecklistStatus(int docCount,string masterId,string docType)

        [TestMethod]
        public void ChecklistStatus_ReturnFalse_Test()
        {
            List<BblServiceDocuments> docCount =new List<BblServiceDocuments>();
            string masterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40";
            string docType = "ON";
            //Act 
            var submissionDocumentsRows = _submissionDocumentTestRepository.ChecklistStatus(docCount, masterId, docType);

            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows == false);
        }

        [TestMethod]
        public void ChecklistStatus_ReturnTrue_Test()
        {
            List<BblServiceDocuments> docCount = new List<BblServiceDocuments>();
            string masterId = "8c6deecc-3b72-485d-8af5-30939af94e97";
            string docType = "ON";
            //Act 
            var submissionDocumentsRows = _submissionDocumentTestRepository.ChecklistStatus(docCount, masterId, docType);

            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(!submissionDocumentsRows);
        }

        [TestMethod]
        public void ChecklistStatus_ToInfoverificationPage_ReturnTrue__Test()
        {
            List<BblServiceDocuments> docCount = new List<BblServiceDocuments>();
            string masterId = "8c6deecc-3b72-485d-8af5-30939af94e97";
            string docType = "ON";
            //Act 
            var submissionDocumentsRows = _submissionDocumentTestRepository.ChecklistStatus(docCount, masterId, docType);

            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(!submissionDocumentsRows);
        }

        [TestMethod]
        public void RenewaldocumentDelete__WithoutDocType_Test()
        {
            var documentList = new List<BblServiceDocuments>
            {
                new BblServiceDocuments()
                {
                       SubmissionCategoryID = 1,
                CategoryID = "151",
                DocRequired = "Fire Marshal Inspection Approval",
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Description = "All businesses that manufacture, wholesale, warehouse, distribute, sell or use toxic or flammable materials or provide services to large audiences (theatres, restaurants, and public halls) are subject to inspection to ensure the safety of customers, employees, and property."
                },
                  new BblServiceDocuments()
                {
                       SubmissionCategoryID = 2,
                CategoryID = "304",
                DocRequired = "Certified Food Supervisor Certification",
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Description = "Caterers must have Certified Food Supervisor(s) present on the premises during the hours the facility is open to the public. The name(s) and certification(s) of the food supervisor employee(s) must be provided to the Department of Health's inspectors."
                }
            };
            var documentCheck = new DocumentCheck
            {
                DocumentList = documentList,
                DocType = "",
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40"
            };
            //Act 
            var submissionDocumentsRows = _submissionDocumentTestRepository.RenewaldocumentDelete(documentCheck);
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows);
        }



        [TestMethod]
        public void RenewaldocumentDelete__WithDocType_Test()
        {
            var documentList = new List<BblServiceDocuments>
            {
                new BblServiceDocuments()
                {
                       SubmissionCategoryID = 1,
                CategoryID = "151",
                DocRequired = "Fire Marshal Inspection Approval",
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Description = "All businesses that manufacture, wholesale, warehouse, distribute, sell or use toxic or flammable materials or provide services to large audiences (theatres, restaurants, and public halls) are subject to inspection to ensure the safety of customers, employees, and property."
                },
                  new BblServiceDocuments()
                {
                       SubmissionCategoryID = 2,
                CategoryID = "304",
                DocRequired = "Certified Food Supervisor Certification",
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Description = "Caterers must have Certified Food Supervisor(s) present on the premises during the hours the facility is open to the public. The name(s) and certification(s) of the food supervisor employee(s) must be provided to the Department of Health's inspectors."
                }
            };
            var documentCheck = new DocumentCheck
            {
                DocumentList = documentList,
                DocType = "IN",
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40"
            };
            //Act 
            var submissionDocumentsRows = _submissionDocumentTestRepository.RenewaldocumentDelete(documentCheck);
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows);
        }

        //    public List<BblServiceDocuments> DocumentByID(string masterid)
        [TestMethod]
        public void GetDocumentByID_WithoutAnyDocumentsExist_Test()
        {
            //Act 
            var submissionDocumentsRows =
                _submissionDocumentTestRepository.DocumentByID("wew0b056-d2a3-485e-a02a-e34c957c4e40");

            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(!submissionDocumentsRows.Any());
        }



        [TestMethod]
        public void GetDocumentByID_WithDocumentsExist_Test()
        {
            //Act 
            var submissionDocumentsRows =
                _submissionDocumentTestRepository.DocumentByID("cce0b056-d2a3-485e-a02a-e34c957c4e40");

            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows.Count() == 2);
        }


        [TestMethod]
        public void DocumentList_MasterId_ExistInSubmissionMaster_Test()
        {
            //Act 
            var submissionDocumentsRows = _submissionDocumentTestRepository.RenewalStatuUpdation("8c6deecc-3b72-485d-8af5-30939af94e97","LiceneseNumber");
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows);
        }



        [TestMethod]
        public void DocumentList_MasterId_DoesNotExistInSubmissionMaster_Test()
        {
            //Act 
            var submissionDocumentsRows = _submissionDocumentTestRepository.RenewalStatuUpdation("8c6deecc-3b72-485d-8af5-30939af94e97", "LiceneseNumber");
            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows);
        }
        [TestMethod]
        public void ChecklistStatus_ToInfoverificationPage_True_Test()
        {
            List<BblServiceDocuments> docCount = new List<BblServiceDocuments>();
            string masterId = "4cb3a7f0-8e98-4b8d-86ae-a2d9dbe14959";
            string docType = "IN";
            //Act 
            var submissionDocumentsRows = _submissionDocumentTestRepository.ChecklistStatus(docCount, masterId, docType);

            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(!submissionDocumentsRows);
        }
        [TestMethod]
        public void ChecklistStatus_ToInfoverificationPage_Indiviudal_Test()
        {
            List<BblServiceDocuments> docCount = new List<BblServiceDocuments>();
            string masterId = "8c6deecc-3b72-485d-8af5-30939af94e97";
            string docType = "ON";
            //Act 
            var submissionDocumentsRows = _submissionDocumentTestRepository.ChecklistStatus(docCount, masterId, docType);

            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(!submissionDocumentsRows);
        }
        [TestMethod]
        public void GetDocumentByID_WithDocumentsExistNodoc_Test()
        {
            //Act 
            var submissionDocumentsRows =
                _submissionDocumentTestRepository.DocumentByID("cce0b056-d2a3-485e-a02a-e34c957c4e40");

            //Assert
            Assert.IsNotNull(submissionDocumentsRows); // Test if null
            Assert.IsTrue(submissionDocumentsRows.Count() == 2);
        }
    }
}
