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
    public class RenewRepositoryTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private RenewRepository _renewTestRepository;

        private BblRepository _bblTestRepository;
        private CorpRespository _corpTestRepository;
       // private MasterLicenseFEINRenewal _masterLicenseFEINRenewalTestRepository;
        //private MasterTaxRevenueRepository _masterTaxRevenueTestRepository;
        private SubmissionMasterRepository _subMasterTestRepository;
        private MasterBusinessActivityRepository _masterBusinessActivityTestRepository;
        private MasterPrimaryCategoryRepository _masterPrimaryCategoryTestRepository;
        private SubmissionCategoryRepository _submissionCategoryTestRepository;
        private SubmissionDocumentRepository _submissionDocumentTestRepository;
        private SubmissionMasterRenewalRepository _submissionMasterRenewalTestRepository;
        private MasterSecondaryLicenseCategoryRepository _masterSecondaryLicenseCategoryTestRepository;
        private DCBC_ENTITY_BBL_RenewalsRepository _dcbcEntityBblRenewalsTestRepository;
       // private Lookup_ExistingCategoriesRepository _lookup_ExistingCategoriesTestRepository;

        private UserBBLServiceRepository _userBblServiceTestRepository;
        private SubmissionQuestionRepository _submissionQuestionTestRepository;
        private MasterCategoryPhysicalLocationRepository _masterCategoryPhysicalLocationTestRepository;
        private SubmissionMasterApplicationChcekListRepository _subChcekListAppTestRepository;
        private UserRepository _userTestRepository;
        private SubmissionBblAssociationToUsersRepository _submissionBblAssociationToUsersTestRepository;
        private SubmissionToAccelaRepository _submissionToAccelaTestRepository;
        private OSubCategoryFeesRepository _oSubCategoryFeesTestRepository;
        private FixFeeRepository _fixfeeTestRepository;
        private MasterSubCategoryRepository _masterSubCategoryTestRepository;
        private FeeCodeMapRepository _feeCodeMapTestRepository;
        private Mock<IDCBC_ENTITY_BBL_Renewal_InvoiceRepository> _dcbcEntityBblRenewalInvoiceTestRepository;
        private MasterCategoryDocumentRepository _masterCategoryDocumentTestRepository;
        private MasterCategoryQuestionRepository _masterCategoryQuestionTestRepository;
        private SubmissionIndividualRepository _submissionIndividualTestRepository;
        private SubmissionDocumentToAccelaRepository _submissionDocumentToAccelaTestRepository;
        private DCBC_ENTITY_BBL_Renewal_InvoiceRepository _dcbcEntityBblRenewalInvoiceRepository;
        private Lookup_ExistingCategoriesRepository _lookupExistingCategoriesRepository;
        private SubmissionLicenseNumberCounterRepository _submissionlicencecounter;
        private MasterRenewalStatusFeeRepository _masterrenewalstatusfee;
        private MasterBblApplicationStatusRepository _masterBblApplicationStatusTestRepository;

        private UserRoleRepository _userRoleRepository;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _masterrenewalstatusfee = new MasterRenewalStatusFeeRepository(_testUnitOfWork);
            _masterBblApplicationStatusTestRepository = new MasterBblApplicationStatusRepository(_testUnitOfWork);
            _submissionDocumentToAccelaTestRepository = new SubmissionDocumentToAccelaRepository(_testUnitOfWork);
            _masterCategoryQuestionTestRepository = new MasterCategoryQuestionRepository(_testUnitOfWork);
            _submissionlicencecounter = new SubmissionLicenseNumberCounterRepository(_testUnitOfWork);
            _submissionQuestionTestRepository = new SubmissionQuestionRepository(_testUnitOfWork);
            _bblTestRepository = new BblRepository(_testUnitOfWork);
            _corpTestRepository = new CorpRespository(_testUnitOfWork);
            _userBblServiceTestRepository = new UserBBLServiceRepository(_testUnitOfWork);
            _lookupExistingCategoriesRepository = new Lookup_ExistingCategoriesRepository(_testUnitOfWork);
            _submissionBblAssociationToUsersTestRepository = new SubmissionBblAssociationToUsersRepository(_testUnitOfWork);
            _submissionToAccelaTestRepository = new SubmissionToAccelaRepository(_testUnitOfWork);
            _fixfeeTestRepository = new FixFeeRepository(_testUnitOfWork);
           // _lookup_ExistingCategoriesTestRepository = new Lookup_ExistingCategoriesRepository(_testUnitOfWork);
            _dcbcEntityBblRenewalInvoiceRepository = new DCBC_ENTITY_BBL_Renewal_InvoiceRepository(_testUnitOfWork, _lookupExistingCategoriesRepository);
            _submissionMasterRenewalTestRepository = new SubmissionMasterRenewalRepository(_testUnitOfWork, _masterrenewalstatusfee);
            // _masterLicenseFEINRenewalTestRepository = new MasterLicenseFEINRenewal(_testUnitOfWork);, _masterLicenseFEINRenewalTestRepository
           // _masterTaxRevenueTestRepository = new MasterTaxRevenueRepository(_testUnitOfWork);
            _submissionIndividualTestRepository = new SubmissionIndividualRepository(_testUnitOfWork, _subMasterTestRepository);
            _dcbcEntityBblRenewalInvoiceTestRepository = new Mock<IDCBC_ENTITY_BBL_Renewal_InvoiceRepository>();
            _userRoleRepository = new UserRoleRepository(_testUnitOfWork);
            _userTestRepository = new UserRepository(_testUnitOfWork, _userRoleRepository);
            _dcbcEntityBblRenewalsTestRepository = new DCBC_ENTITY_BBL_RenewalsRepository(_testUnitOfWork);
            _masterCategoryDocumentTestRepository = new MasterCategoryDocumentRepository(_testUnitOfWork);
            _masterSecondaryLicenseCategoryTestRepository = new MasterSecondaryLicenseCategoryRepository(_testUnitOfWork);
            _masterBusinessActivityTestRepository = new MasterBusinessActivityRepository(_testUnitOfWork, _masterPrimaryCategoryTestRepository);
            _masterPrimaryCategoryTestRepository = new MasterPrimaryCategoryRepository(_testUnitOfWork, _masterSecondaryLicenseCategoryTestRepository, _masterCategoryDocumentTestRepository, _masterCategoryQuestionTestRepository);
            _masterSubCategoryTestRepository = new MasterSubCategoryRepository(_testUnitOfWork, _masterPrimaryCategoryTestRepository, _masterSecondaryLicenseCategoryTestRepository);
            _subChcekListAppTestRepository = new SubmissionMasterApplicationChcekListRepository(_testUnitOfWork);
            _oSubCategoryFeesTestRepository = new OSubCategoryFeesRepository(_testUnitOfWork, _masterPrimaryCategoryTestRepository, _masterSecondaryLicenseCategoryTestRepository);
            _feeCodeMapTestRepository = new FeeCodeMapRepository(_testUnitOfWork, _oSubCategoryFeesTestRepository);
            _submissionCategoryTestRepository = new SubmissionCategoryRepository(_testUnitOfWork, _masterPrimaryCategoryTestRepository,
           _masterSecondaryLicenseCategoryTestRepository,
           _oSubCategoryFeesTestRepository, _fixfeeTestRepository,
           _submissionQuestionTestRepository, _masterSubCategoryTestRepository, _masterCategoryPhysicalLocationTestRepository,
           _feeCodeMapTestRepository, _subChcekListAppTestRepository, _submissionMasterRenewalTestRepository, _dcbcEntityBblRenewalInvoiceTestRepository.Object,
           _lookupExistingCategoriesRepository);

            _subMasterTestRepository = new SubmissionMasterRepository(_testUnitOfWork, _submissionCategoryTestRepository, _bblTestRepository,
           _userBblServiceTestRepository, _submissionQuestionTestRepository, _masterCategoryPhysicalLocationTestRepository, _masterPrimaryCategoryTestRepository,
           _subChcekListAppTestRepository, _userTestRepository, _dcbcEntityBblRenewalsTestRepository, _masterBusinessActivityTestRepository,
           _submissionBblAssociationToUsersTestRepository, _submissionToAccelaTestRepository, _masterSecondaryLicenseCategoryTestRepository,
           _lookupExistingCategoriesRepository, _submissionlicencecounter, _masterBblApplicationStatusTestRepository);

            _masterCategoryPhysicalLocationTestRepository = new MasterCategoryPhysicalLocationRepository(_testUnitOfWork,
                _masterPrimaryCategoryTestRepository, _masterCategoryQuestionTestRepository, _oSubCategoryFeesTestRepository, _masterSecondaryLicenseCategoryTestRepository);
          

            _submissionDocumentTestRepository = new SubmissionDocumentRepository(_testUnitOfWork,
          _submissionCategoryTestRepository,
          _masterCategoryDocumentTestRepository,
          _masterPrimaryCategoryTestRepository,
          _masterSecondaryLicenseCategoryTestRepository, _subMasterTestRepository,
          _masterCategoryPhysicalLocationTestRepository, _subChcekListAppTestRepository,
          _submissionIndividualTestRepository, _submissionDocumentToAccelaTestRepository);

            _renewTestRepository = new RenewRepository(_testUnitOfWork, _bblTestRepository, _corpTestRepository, 
               _subMasterTestRepository,
              _masterBusinessActivityTestRepository,
             _masterPrimaryCategoryTestRepository,
             _submissionCategoryTestRepository,
             _submissionDocumentTestRepository,
             _submissionMasterRenewalTestRepository,
             _masterSecondaryLicenseCategoryTestRepository,
             _dcbcEntityBblRenewalsTestRepository,
             _lookupExistingCategoriesRepository, _dcbcEntityBblRenewalInvoiceRepository
             );


            //Setup Data
            var bblRepositoryData = new BblRepositoryData();
            _testDbContext.DCBC_ENTITY_BBL.AddRange(bblRepositoryData.BblEntitiesList);
            _testDbContext.SaveChanges();

            //Setup Data
            var corData = new CorpRepositoryData();
            _testDbContext.DCBC_ENTITY_CORP.AddRange(corData.CorpEntitiesList);
            _testDbContext.SaveChanges();

            //Setup Data
            //var FEINRenewalData = new MasterLicenseFEINRenewalRepositoryData();
            //_testDbContext.masterLicense_Renewal_TaxRevenue.AddRange(FEINRenewalData.MasterLicenseEntitiesList);
            //_testDbContext.SaveChanges();

            //Setup Data
            //var taxRevenueData = new MasterTaxRevenueRepositoryData();
            //_testDbContext.MasterTaxRevenue.AddRange(taxRevenueData.MasterTaxRevenueEntitiesList);
            //_testDbContext.SaveChanges();
            var primaryCategoryData = new MasterPrimaryCategoryRepositoryData();
            _testDbContext.MasterPrimaryCategory.AddRange(primaryCategoryData.MasterPrimaryCategoryEntitiesList);
            _testDbContext.SaveChanges();

            //Setup Data
            var submissionMasterRepositoryData = new SubmissionMasterRepositoryData();
            _testDbContext.SubmissionMaster.AddRange(submissionMasterRepositoryData.SubmissionMasterEntitiesList);
            _testDbContext.SaveChanges();

            //Setup Data
            var businessActivityData = new MasterBusinessActivityData();
            _testDbContext.MasterBusinessActivity.AddRange(businessActivityData.MasterAcivityEntitiesList);
            _testDbContext.SaveChanges();


            var masterDocumentData = new MasterCategoryDocumentRepositoryData();
            _testDbContext.MasterCategoryDocument.AddRange(masterDocumentData.MasterCategoryDocumentList);
            _testDbContext.SaveChanges();

            var secondaryCategoryData = new MasterSecondaryLicenseCategoryRepositoryData();
            _testDbContext.MasterSecondaryLicenseCategory.AddRange(secondaryCategoryData.MasterSeconderyLicenseCategoryList);
            _testDbContext.SaveChanges();

            //Setup Data
            var submissionCategoryRepositoryData = new SubmissionCategoryRepositoryData();
            _testDbContext.SubmissionCategory.AddRange(submissionCategoryRepositoryData.SubmissionCategoryEntitiesList);
            _testDbContext.SaveChanges();

            //Setup Data
            var submissionDocumentRepositoryData = new SubmissionDocumentRepositoryData();
            _testDbContext.SubmissionDocument.AddRange(submissionDocumentRepositoryData.SubmissionDocumentList);
            _testDbContext.SaveChanges();

            var submissionMasterRenewalData = new SubmissionMasterRenewalData();
            _testDbContext.SubmissionMasterRenewal.AddRange(submissionMasterRenewalData.MasterRenewalEntitiesList);
            _testDbContext.SaveChanges();

            var bblrenewaltData = new DCBC_ENTITY_BBL_RenewalsData();
            _testDbContext.DCBC_ENTITY_BBL_Renewals.AddRange(bblrenewaltData.DcbcEntityBblRenewalsList);
            _testDbContext.SaveChanges();


            //Setup Data
            var lookupData = new Lookup_ExistingCategoriesData();
            _testDbContext.Lookup_ExistingCategories.AddRange(lookupData.ExistingCategoriesList);
            _testDbContext.SaveChanges();

          


            //Setup Data
            var checklistData = new SubmissionMasterApplicationChcekListRepositoryData();
            _testDbContext.SubmissionMaster_ApplicationCheckList.AddRange(checklistData.SubmissionMaster_ApplicationCheckListEntitiesList);
            _testDbContext.SaveChanges();
            var userlistData = new UserBBLServiceRepositoryData();
            _testDbContext.UserBBLService.AddRange(userlistData.UserBblServiceList);
            _testDbContext.SaveChanges();
            var renewalinvoice = new DCBC_ENTITY_BBL_Renewal_InvoiceData();
            _testDbContext.DCBC_ENTITY_BBL_Renewal_Invoice.AddRange(renewalinvoice.DcbcEntityBblRenewalsList);
            _testDbContext.SaveChanges();

           
            
        }

        [TestMethod]
        public void CheckDocument_DocType_INPERSON_Test()
        {
            //Initialize Entites
            // var bblServiceDocuments = new BblServiceDocuments { MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40" };
            var servicedocument = new List<BblServiceDocuments>();
            var bblServiceDocuments = new BblServiceDocuments
            {
                SubmissionCategoryID = 1,
                CategoryID = "1",
                DocRequired = "Department of Health (DOH)",
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Description = "All businesses that manufacture, wholesale, warehouse, distribute, sell or use toxic or flammable materials or provide services to large audiences (theatres, restaurants, and public halls) are subject to inspection to ensure the safety of customers, employees, and property."
            };
            servicedocument.Add(bblServiceDocuments);
            var documentCheckEntity = new DocumentCheck();
            documentCheckEntity.DocumentList = servicedocument;
            documentCheckEntity.DocType = "IN";

            //Act 
            var renewEntityRows = _renewTestRepository.CheckDocument(documentCheckEntity);

            //Assert
            Assert.IsNotNull(renewEntityRows); // Test if null
            Assert.IsTrue(renewEntityRows == true);
        }
        [TestMethod]
        public void CheckDocument_Return_NoDocuments_Test()
        {
            //Initialize Entites
            var servicedocument = new List<BblServiceDocuments>();
            var documentCheckEntity = new DocumentCheck();
            documentCheckEntity.DocumentList = servicedocument;
            documentCheckEntity.DocType = "IN";

            //Act 
            var renewEntityRows = _renewTestRepository.CheckDocument(documentCheckEntity);

            //Assert
            Assert.IsNotNull(renewEntityRows); // Test if null
            Assert.IsTrue(renewEntityRows == true);
        }
        [TestMethod]
        public void CheckDocument_DocType_ONLINE_Return_False_Test()
        {
            //Initialize Entites
            var servicedocument = new List<BblServiceDocuments>();
            var bblServiceDocuments = new BblServiceDocuments
            {
                SubmissionCategoryID = 1,
                CategoryID = "1",
                DocRequired = "Department of Health (DOH)",
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Description = "All businesses that manufacture, wholesale, warehouse, distribute, sell or use toxic or flammable materials or provide services to large audiences (theatres, restaurants, and public halls) are subject to inspection to ensure the safety of customers, employees, and property."
            };
            servicedocument.Add(bblServiceDocuments);
            var documentCheckEntity = new DocumentCheck();
            documentCheckEntity.DocumentList = servicedocument;
            documentCheckEntity.DocType = "ON";

            //Act 
            var renewEntityRows = _renewTestRepository.CheckDocument(documentCheckEntity);

            //Assert
            Assert.IsNotNull(renewEntityRows); // Test if null
            Assert.IsTrue(renewEntityRows == false);
        }
        [TestMethod]
        public void CheckDocument_DocType_ONLINE_Return_True_Test()
        {
            //Initialize Entites
            var servicedocument = new List<BblServiceDocuments>
             {
                         new BblServiceDocuments()
                          { 
                            SubmissionCategoryID = 1,
                            CategoryID = "1",
                            DocRequired = "Department of Health (DOH)",
                            MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                            Description = "All businesses that manufacture, wholesale, warehouse, distribute, sell or use toxic or flammable materials or provide services to large audiences (theatres, restaurants, and public halls) are subject to inspection to ensure the safety of customers, employees, and property."
                           },
                           new BblServiceDocuments()
                          { 
                            SubmissionCategoryID = 2,
                            CategoryID = "2",
                            DocRequired = "Certified Food Supervisor Certification",
                            MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                            Description = "Caterers must have Certified Food Supervisor(s) present on the premises during the hours the facility is open to the public. The name(s) and certification(s) of the food supervisor employee(s) must be provided to the Department of Health's inspectors."
                           },
                            new BblServiceDocuments()
                          { 
                            SubmissionCategoryID = 3,
                            CategoryID = "3",
                            DocRequired = "Health Inspection Approval",
                            MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                            Description = "Must be inspected by and/or receive approval to operate from the Department of Health."
                           },
                             new BblServiceDocuments()
                          { 
                            SubmissionCategoryID = 4,
                            CategoryID = "4",
                            DocRequired = "Health Inspection Approval",
                            MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                            Description = "An approved health inspection report for review of embalming and tissue removal procedures from the Department of Health"
                           },
                              new BblServiceDocuments()
                          { 
                            SubmissionCategoryID = 5,
                            CategoryID = "5",
                            DocRequired = "Certificate of Occupancy (COfO) Document",
                            MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                            Description = "DCRA"
                           }
    
    
               };

            var documentCheckEntity = new DocumentCheck();
            documentCheckEntity.DocumentList = servicedocument;
            documentCheckEntity.DocType = "ON";

            //Act 
            var renewEntityRows = _renewTestRepository.CheckDocument(documentCheckEntity);

            //Assert
            Assert.IsNotNull(renewEntityRows); // Test if null
            Assert.IsTrue(renewEntityRows == true);
        }
        [TestMethod]
        public void UpdateRenwalDocumentType_Return_Test()
        {
            //Initialize Entites
            var servicedocument = new List<BblServiceDocuments>();
            var bblServiceDocuments = new BblServiceDocuments
            {
                SubmissionCategoryID = 1,
                CategoryID = "1",
                DocRequired = "Department of Health (DOH)",
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Description = "All businesses that manufacture, wholesale, warehouse, distribute, sell or use toxic or flammable materials or provide services to large audiences (theatres, restaurants, and public halls) are subject to inspection to ensure the safety of customers, employees, and property."
            };
            servicedocument.Add(bblServiceDocuments);
            var documentCheckEntity = new DocumentCheck();
            documentCheckEntity.DocumentList = servicedocument;
            documentCheckEntity.DocType = "IN";
            documentCheckEntity.MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40";


            //Act 
            var renewEntityRows = _renewTestRepository.UpdateRenwalDocumentType(documentCheckEntity);

            //Assert
            Assert.IsNotNull(renewEntityRows); // Test if null
            Assert.IsTrue(renewEntityRows == true);
        }
        [TestMethod]
        public void DeleteRenewal_Return_Test()
        {
            //Initialize Entites
            var renewModel = new RenewModel { MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40" };


            //Act 
            var renewEntityRows = _renewTestRepository.DeleteRenewal(renewModel);

            //Assert
            Assert.IsNotNull(renewEntityRows); // Test if null
            Assert.IsTrue(renewEntityRows == true);
        }
        [TestMethod]
        public void DocumentList_Return_Test()
        {
            //Initialize Entites
            var renewModel = new RenewModel { MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40", CorpStatus="Active"};


            //Act 
            var renewEntityRows = _renewTestRepository.DocumentList(renewModel);

            //Assert
            Assert.IsNotNull(renewEntityRows); // Test if null
           Assert.IsTrue(renewEntityRows.DocumentList.Count()==1);
        }
        [TestMethod]
        public void CheckCategoryStatus_Test()
        {
            //Initialize Entites
          //  var renewModel = new RenewModel { MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40", CorpStatus = "Active" };


            //Act 
            var renewEntityRows = _renewTestRepository.CheckCategoryStatus("1", "931315000136", "LREN11002680");

            //Assert
            Assert.IsNotNull(renewEntityRows); // Test if null
            Assert.IsTrue(renewEntityRows == "NoCategory");
        }
        [TestMethod]
        public void CheckRenewal_Test()
        {
            //Initialize Entites
            var renewModel = new RenewModel { MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97", CategoryName = "Auctioneer", LrenNumber = "LREN11002680", LicenseNumber = "931315000136", CorpNumber = "C212821", EntityId = "1" };


            //Act 
            var renewEntityRows = _renewTestRepository.CheckRenewal(renewModel);

            //Assert
            Assert.IsNotNull(renewEntityRows); // Test if null
            Assert.IsTrue(renewEntityRows.LrenNumber == "LREN11002680");
        }
        [TestMethod]
        public void GetCorpNumber_Test()
        {
            //Initialize Entites

            //Act 
            var renewEntityRows = _renewTestRepository.GetCorpNumber("8c6deecc-3b72-485d-8af5-30939af94e97");

            //Assert
            Assert.IsNotNull(renewEntityRows); // Test if null
            Assert.IsTrue(renewEntityRows == "900175");
        }
        
        [TestMethod]
        public void CheckRenewalTest()
        {
            //Initialize Entites
            var renewModel = new RenewModel { CategoryName = "Auctioneer", SubmissionLicense = "LREN11002680", LicenseNumber = "931315000136", IsCorp=false, EntityId = "1", MasterId = string.Empty };


            //Act 
            var renewEntityRows = _renewTestRepository.CheckRenewal(renewModel);

            //Assert
            Assert.IsNotNull(renewEntityRows); // Test if null
            Assert.IsTrue(renewEntityRows.LrenNumber == "LREN11002680");
        }

        [TestMethod]
        public void CheckAmount_Test()
        {
            //Initialize Entites
            var renewModel = new RenewModel { Extradays="Lapsed", UserBblAssociateId = "2", MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97", CategoryName = "Auctioneer", LrenNumber = "LREN11002680", LicenseNumber = "931315000136", CorpNumber = "C212821", EntityId = "1" };


            //Act 
            var renewEntityRows = _renewTestRepository.CheckAmount(renewModel);

            //Assert
            Assert.IsNotNull(renewEntityRows); // Test if null
            Assert.IsTrue(renewEntityRows.LrenNumber == "LREN11002680");
        }
         [TestMethod]
        public void CheckDocumentRenewal_Test()
        {
            //Initialize Entites
            var renewModel = new RenewModel { Extradays = "Lapsed", UserBblAssociateId = "2", MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97", CategoryName = "Charitable Exempt", LrenNumber = "LREN11002680", LicenseNumber = "931315000136", CorpNumber = "C212821", EntityId = "1" };


            //Act 
            var renewEntityRows = _renewTestRepository.CheckDocument(renewModel);

            //Assert
            Assert.IsNotNull(renewEntityRows); // Test if null
            Assert.IsTrue(renewEntityRows.LrenNumber == "LREN11002680");
        }
        
        //[TestMethod]
        //public void CheckRenewalCategoryTest()
        //{
        //    //Initialize Entites
        //    var renewModel = new RenewModel { CategoryName = "Auctionee", LrenNumber = "LREN1100282", LicenseNumber = "93131500013", CorpNumber = "C943266", EntityId = "1" };


        //    //Act 
        //    var renewEntityRows = _renewTestRepository.CheckRenewal(renewModel);

        //    //Assert
        //    Assert.IsNotNull(renewEntityRows); // Test if null
        //    Assert.IsTrue(renewEntityRows.RenewStatus == "NoData");
        //}

        //[TestMethod]
        //public void CheckRenewalNocorpTest()
        //{
        //    //Initialize Entites
        //    var renewModel = new RenewModel { CategoryName = "Auctioneer", LrenNumber = "LREN1100282", LicenseNumber = "93131500013", CorpNumber = "C94366",
        //                                      EntityId = "1",
        //                                      IsCorp = true,
        //                                      ActivityName = "Real Estate & Rentals",
        //                                      ActivityId = "DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A",
        //                                      SubCategoryName = "Food Vending Machine,Food Products",
        //                                      SubCategoryID = "1DE53FC4-F444-44EA-99A4-5DA3F107D5DD,42D8D51F-6B22-4E7B-BC7F-CE0EA2E2814B"
        //    };


        //    //Act 
        //    var renewEntityRows = _renewTestRepository.CheckRenewal(renewModel);

        //    //Assert
        //    Assert.IsNotNull(renewEntityRows); // Test if null
        //    Assert.IsTrue(renewEntityRows.DocumentList.Count() == 0);
        //}
    }
}
