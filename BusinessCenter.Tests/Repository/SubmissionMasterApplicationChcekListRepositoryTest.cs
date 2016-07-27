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
    public class SubmissionMasterApplicationChcekListRepositoryTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Repository declaration
        private SubmissionMasterApplicationChcekListRepository _checkListTestRepository;
      

        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);

            //SubmissionMasterApplicationChcekListRepository Initialization
            _checkListTestRepository = new SubmissionMasterApplicationChcekListRepository(_testUnitOfWork);



            //Setup  SubmissionMaster_ApplicationCheckList FackData Initialization
            var testData = new SubmissionMasterApplicationChcekListRepositoryData();
            _testDbContext.SubmissionMaster_ApplicationCheckList.AddRange(testData.SubmissionMaster_ApplicationCheckListEntitiesList);
            _testDbContext.SaveChanges();

        }

        [TestMethod()]
        public void FindByMasterIdTest()
        {
            //Act 
            var serviceChecklist = _checkListTestRepository.FindByMasterId("cce0b056-d2a3-485e-a02a-e34c957c4e40");

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist.Count() == 1);
        }
        [TestMethod()]
        public void UpdateAllCheckListConditionsTrueTest()
        {
            //Initial Model Details
            var cofoHopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97"
            };

            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateAllCheckListConditions(cofoHopDetailsModel);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist == true);
        }
        [TestMethod()]
        public void UpdateAllCheckListConditionTest()
        {
            //Initial Model Details
            var cofoHopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40"
            };
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateAllCheckListConditions(cofoHopDetailsModel);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsFalse(serviceChecklist);
        }
        [TestMethod()]
        public void UpdateAllCheckListConditionsFalseTest()
        {
            //Initial Model Details
            var cofoHopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40"
            };

            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateAllCheckListConditions(cofoHopDetailsModel);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist == false);
        }
        [TestMethod()]
        public void UpdateAllCheckListConditionsTest()
        {
            //Initial Model Details
            var cofoHopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "8dda4efd-8504-4d"
            };

            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateAllCheckListConditions(cofoHopDetailsModel);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist == false);
        }
        [TestMethod()]
        public void UpdateIsCofoTest()
        {
            //Initial Model data Details
            var cofoHopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Type = "COFO"
            };
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateIsCofo(cofoHopDetailsModel);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist == true);
        }
        [TestMethod()]
        public void UpdateIsHopTest()
        {
            //Initial Model data Details
            var cofoHopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
                Type = "HOP"
            };
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateIsCofo(cofoHopDetailsModel);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist == true);
        }
        [TestMethod()]
        public void UpdateIsEHOPTest()
        {
            //Initial Model data Details
            var cofoHopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Type = "EHOP"
            };
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateIsCofo(cofoHopDetailsModel);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist == true);
        }

        [TestMethod()]
        public void UpdateIsTest()
        {
            //Initial Model data Details
            var cofoHopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Type = null
            };
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateIsCofo(cofoHopDetailsModel);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist == false);
        }
        [TestMethod()]
        public void InsertSubmissionChecklistTest()
        {
            //Initial Model data Details
            var submissionApplication = new SubmissionApplication
            {
                MasterId = "8dda4efd-8504-4d1a-8370-a18b1667a210"
               
            };
            //Act 
            var serviceChecklist = _checkListTestRepository.InsertSubmissionChecklist(submissionApplication);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist == true);
        }

        [TestMethod()]
        public void UpdateCheckListAppTest()
        {
            //Initial Model data Details
            var submissionTaxRevenue = new SubmissionTaxRevenue
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40"

            };
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateCheckListApp(submissionTaxRevenue, true);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
           // Assert.IsTrue(serviceChecklist == submissionMasterApplicationCheck);
        }
        [TestMethod()]
        public void UpdateCheckListAppDataTest()
        {
            //Initial Model data Details
            var submissionTaxRevenue = new SubmissionTaxRevenue
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                TaxRevenueType="DATA"

            };
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateCheckListApp(submissionTaxRevenue, true);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            // Assert.IsTrue(serviceChecklist == submissionMasterApplicationCheck);
        }
        [TestMethod()]
        public void UpdateCheckListAppNoDataTest()
        {
            //Initial Model data Details
            var submissionTaxRevenue = new SubmissionTaxRevenue
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                TaxRevenueType = "NODATA"

            };
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateCheckListApp(submissionTaxRevenue, true);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            // Assert.IsTrue(serviceChecklist == submissionMasterApplicationCheck);
        }
        
        public void UpdateDetailsCofoTest()
        {
            //Initial Model data Details
            var cofoHopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Type ="COFO"
            };
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateDetails(cofoHopDetailsModel);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
             Assert.IsTrue(serviceChecklist == true);
        }


        [TestMethod()]
        public void UpdateDetailsCofoIsdocTest()
        {
            //Initial Model data Details
            var cofoHopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Type = "COFO",
                IsUploadSupportDoc=true,
                IsValid  =true
            };
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateDetails(cofoHopDetailsModel);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist == true);
        }

        [TestMethod()]
        public void UpdateDetailsHopTest()
        {
            //Initial Model data Details
            var cofoHopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Type = "HOP"
            };
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateDetails(cofoHopDetailsModel);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist == true);
        }
        [TestMethod()]
        public void UpdateDetailsHopIsdocTest()
        {
            //Initial Model data Details
            var cofoHopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Type = "HOP",
                IsUploadSupportDoc = true,
                IsValid = true
            };
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateDetails(cofoHopDetailsModel);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist == true);
        }
        [TestMethod()]
        public void UpdateDetailsEhopTest()
        {
            //Initial Model data Details
            var cofoHopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
                Type = "EHOP"
            };
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateDetails(cofoHopDetailsModel);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist == true);
        }
        [TestMethod()]
        public void UpdateDetailsEhopIsdocTest()
        {
            //Initial Model data Details
            var cofoHopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
                Type = "EHOP",
                IsUploadSupportDoc = true,
                IsValid = true
            };
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateDetails(cofoHopDetailsModel);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist == true);
        }

        [TestMethod()]
        public void UpdateDetailsEehopTest()
        {
            //Initial Model data Details
            var cofoHopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
                Type = "EEHOP"
            };
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateDetails(cofoHopDetailsModel);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist == true);
        }
        [TestMethod()]
        public void UpdateDetailsNopoTest()
        {
            //Initial Model data Details
            var cofoHopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
                Type = "NOPO"
            };
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateDetails(cofoHopDetailsModel);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist == true);
        }
        [TestMethod()]
        public void UpdateDetailsTest()
        {
            //Initial Model data Details
            var cofoHopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Type = " NOPO"
            };
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateDetails(cofoHopDetailsModel);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist == true);
        }
        [TestMethod()]
        public void UpdateDetailsFalseTest()
        {
            //Initial Model data Details
            var cofoHopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "8dda4efd-8504-4d1a-8370-a18b1667a210"
            };
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateDetails(cofoHopDetailsModel);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist == false);
        }
        [TestMethod()]
        public void ChcekListUpdateStatusFalseTest()
        {
            //Act 
            var serviceChecklist = _checkListTestRepository.ChcekListUpdateStatus("8dda4efd-8504-4d1a-8370-a18b1667a210", "Y-CORPREG", true);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist == false);
        }

        [TestMethod()]
        public void ChcekListUpdateStatusYcorpAgentTest()
        {
            //Act 
            var serviceChecklist = _checkListTestRepository.ChcekListUpdateStatus("cce0b056-d2a3-485e-a02a-e34c957c4e40", "Y-CORPAGENT", true);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist);
        }
        [TestMethod()]
        public void ChcekListUpdateStatusYcorpTest()
        {
            //Act 
            var serviceChecklist = _checkListTestRepository.ChcekListUpdateStatus("cce0b056-d2a3-485e-a02a-e34c957c4e40", "Y-CORPREG", true);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist);
        }
        [TestMethod()]
        public void ChcekListUpdateStatusNcorpAgntTest()
        {
            //Act 
            var serviceChecklist = _checkListTestRepository.ChcekListUpdateStatus("cce0b056-d2a3-485e-a02a-e34c957c4e40", "N-CORPAGNT", true);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist);
        }

        [TestMethod()]
        public void ChcekListUpdateStatusNcorpAgentTest()
        {
            //Act 
            var serviceChecklist = _checkListTestRepository.ChcekListUpdateStatus("cce0b056-d2a3-485e-a02a-e34c957c4e40", "N-CORPAGENT", true);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist);
        }

        [TestMethod()]
        public void ChcekListUpdateStatusNRegTest()
        {
            //Act 
            var serviceChecklist = _checkListTestRepository.ChcekListUpdateStatus("cce0b056-d2a3-485e-a02a-e34c957c4e40", "N-CORPREG", true);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist);
        }

        [TestMethod()]
        public void ChcekListUpdateStatusSearchCorpTest()
        {
            //Act 
            var serviceChecklist = _checkListTestRepository.ChcekListUpdateStatus("cce0b056-d2a3-485e-a02a-e34c957c4e40", "SEARCHCORP", true);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist);
        }

        [TestMethod()]
        public void ChcekListUpdateStatusNewmailTest()
        {
            //Act 
            var serviceChecklist = _checkListTestRepository.ChcekListUpdateStatus("cce0b056-d2a3-485e-a02a-e34c957c4e40", "NEWMAIL", true);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist);
        }
        [TestMethod()]
        public void ChcekListUpdateStatusTest()
        {
            //Act 
            var serviceChecklist = _checkListTestRepository.ChcekListUpdateStatus("cce0b056-d2a3-485e-a02a-e34c957c4e40", " NEWMAIL", true);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist);
        }
        [TestMethod()]
        public void UpdateIsCorporationYcorpAgentTest()
        {
            //Initial Model data Details
            var generalBusiness = new GeneralBusiness
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                UserType = "Y-CORPAGENT"
            };
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateIsCorporation(generalBusiness);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist);
        }

        [TestMethod()]
        public void UpdateIsCorporationNcorpRegTest()
        {
            //Initial Model data Details
            var generalBusiness = new GeneralBusiness
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                UserType = "N-CORPREG"
            };
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateIsCorporation(generalBusiness);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist);
        }
        [TestMethod()]
        public void UpdateIsCorporationNcorpAgentTest()
        {
            //Initial Model data Details
            var generalBusiness = new GeneralBusiness
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                UserType = "N-CORPAGENT"
            };
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateIsCorporation(generalBusiness);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist);
        }
        [TestMethod()]
        public void UpdateIsCorporationFalseTest()
        {
            //Initial Model data Details
            var generalBusiness = new GeneralBusiness
            {
                MasterId = "8dda4efd-8504-4d1a-8370-a18b1667a210",
                UserType = "N-CORPAGENT"
            };


            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateIsCorporation(generalBusiness);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsFalse(serviceChecklist);
        }
        [TestMethod()]
        public void UpdateIsCorporationAgentFalseTest()
        {
            //Initial Model data Details
            var generalBusiness = new GeneralBusiness
            {
                MasterId = "8dda4efd-8504-4d1a-8370-a18b1667a210"
              
            };
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateIsCorporationAgent(generalBusiness);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsFalse(serviceChecklist);
        }
        [TestMethod()]
        public void UpdateIsCorporationAgentTest()
        {
            //Initial Model data Details
            var generalBusiness = new GeneralBusiness
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40"

            };
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateIsCorporationAgent(generalBusiness);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist);
        }
        [TestMethod()]
        public void UpdateIsMailAddressTest()
        {
            //Initial Model data Details
            var generalBusiness = new GeneralBusiness
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40"

            };
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateIsMailAddress(generalBusiness);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist);
        }
        [TestMethod()]
        public void UpdateIsMailAddressFalseTest()
        {
            //Initial Model data Details
            var generalBusiness = new GeneralBusiness
            {
                MasterId = "8dda4efd-8504-4d1a-8370-a18b1667a210"

            };
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateIsMailAddress(generalBusiness);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsFalse(serviceChecklist);
        }

        [TestMethod()]
        public void UpdateCofodetailsTest()
        {
            //Initial Model data Details
            var cofoHopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40"

            };
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateCofodetails(cofoHopDetailsModel);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist);
        }
        [TestMethod()]
        public void UpdateCofodetailsFalseTest()
        {
            //Initial Model data Details
            var cofoHopDetailsModel = new CofoHopDetailsModel
            {
                MasterId = "8dda4efd-8504-4d1a-8370-a18b1667a210"

            };
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateCofodetails(cofoHopDetailsModel);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsFalse(serviceChecklist);
        }

        [TestMethod()]
        public void InsertRenewChecklistTest()
        {
            //Initial Model data Details
            var renewModel = new RenewModel
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40"

            };
            //Act 
            var serviceChecklist = _checkListTestRepository.InsertRenewChecklist(renewModel);

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist);
        }

        [TestMethod()]
        public void UpdateCorpCheckYcorpTest()
        {
          
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateCorpCheckStatus("cce0b056-d2a3-485e-a02a-e34c957c4e40", "Y-CORPREG", "HQ ");

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist);
          
        }
        [TestMethod()]
        public void UpdateCorpCheckStatusYcorpAgentTest()
        {
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateCorpCheckStatus("cce0b056-d2a3-485e-a02a-e34c957c4e40", "Y-CORPAGENT", "HQ ");

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist);
        }
        [TestMethod()]
        public void UpdateCorpCheckStatusNcorpAgentEmptyTest()
        {
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateCorpCheckStatus("cce0b056-d2a3-485e-a02a-e34c957c4e40", "N-CORPAGENTEMPTY", "HQ ");

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist);
        }
        [TestMethod()]
        public void UpdateCorpCheckStatusEhopAddressTest()
        {
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateCorpCheckStatus("cce0b056-d2a3-485e-a02a-e34c957c4e40", "EHOPADDRESS", "HQ ");

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist);
        }
        [TestMethod()]
        public void UpdateCorpCheckStatusNopoTest()
        {
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateCorpCheckStatus("cce0b056-d2a3-485e-a02a-e34c957c4e40", "NOPO", "HQ ");

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist);
        }
        [TestMethod()]
        public void UpdateCorpCheckStatusNcorpemptyTest()
        {
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateCorpCheckStatus("cce0b056-d2a3-485e-a02a-e34c957c4e40", "N-CORPREG-EMPTY", "HQ ");

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist);
        }
        [TestMethod()]
        public void UpdateCorpCheckStatusNewEmailTest()
        {
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateCorpCheckStatus("cce0b056-d2a3-485e-a02a-e34c957c4e40", "NEWMAIL", "HQ ");

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist);
        }
        [TestMethod()]
        public void UpdateCorpCheckStatusNcorpRegTest()
        {
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateCorpCheckStatus("cce0b056-d2a3-485e-a02a-e34c957c4e40", "N-CORPREG", "HQ ");

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist);
        }
        [TestMethod()]
        public void UpdateCorpCheckStatusNcorpagentTest()
        {
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateCorpCheckStatus("cce0b056-d2a3-485e-a02a-e34c957c4e40", "N-CORPAGENT", "HQ ");

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist);
        }
        [TestMethod()]
        public void UpdateCorpCheckStatusDefaultTest()
        {
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateCorpCheckStatus("cce0b056-d2a3-485e-a02a-e34c957c4e40", " N-CORPAGENT", "HQ ADDRESS");

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist);
        }
        [TestMethod()]
        public void UpdateCorpCheckStatusFalseTest()
        {
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateCorpCheckStatus("8dda4efd-8504-4d1a-8370-a18b1667a210", " N-CORPAGENT", "HQ ADDRESS");

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsFalse(serviceChecklist);
        }
         [TestMethod()]
        public void UpdateMailStatusTest()
        {
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateMailStatus("cce0b056-d2a3-485e-a02a-e34c957c4e40");

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsTrue(serviceChecklist);
        }
         [TestMethod()]
        public void UpdateMailStatusFalseTest()
        {
            //Act 
            var serviceChecklist = _checkListTestRepository.UpdateMailStatus("8dda4efd-8504-4d1a-8370-a18b1667a210");

            //Assert
            Assert.IsNotNull(serviceChecklist); // Test if null
            Assert.IsFalse(serviceChecklist);
        }
         [TestMethod()]
         public void UpdateMailTrueStatusTest()
         {//Act 
             var serviceChecklist = _checkListTestRepository.UpdateMailTrueStatus("cce0b056-d2a3-485e-a02a-e34c957c4e40");

             //Assert
             Assert.IsNotNull(serviceChecklist); // Test if null
             Assert.IsTrue(serviceChecklist);
         }
         [TestMethod()]
         public void UpdateMailTrueStatusFalseTest()
         {
             //Act 
             var serviceChecklist = _checkListTestRepository.UpdateMailTrueStatus("8dda4efd-8504-4d1a-8370-a18b1667a210");

             //Assert
             Assert.IsNotNull(serviceChecklist); // Test if null
             Assert.IsFalse(serviceChecklist);
         }
         [TestMethod()]
         public void CofoServiceStatusTest()
         {//Act 
             _checkListTestRepository.CofoServiceStatus("cce0b056-d2a3-485e-a02a-e34c957c4e40", "PRIMSES ADDRESS");

             //Assert
             //Assert.IsNotNull(serviceChecklist); // Test if null
             //Assert.IsTrue(serviceChecklist);
         }
         [TestMethod()]
         public void TaxServiceStatusTest()
         {
             //Act 
             _checkListTestRepository.TaxServiceStatus("cce0b056-d2a3-485e-a02a-e34c957c4e40", true);

             //Assert
             //Assert.IsNotNull(serviceChecklist); // Test if null
             //Assert.IsFalse(serviceChecklist);
         }
         [TestMethod()]
         public void TaxServiceStatusFalseTest()
         {
             //Act 
             _checkListTestRepository.TaxServiceStatus("cce0b056-d2a3-485e-a02a-e34c957c4e40", false);

             //Assert
             //Assert.IsNotNull(serviceChecklist); // Test if null
             //Assert.IsFalse(serviceChecklist);
         }
         [TestMethod()]
         public void UpdateCorpSearchStatusTest()
         {//Act 
             var serviceChecklist = _checkListTestRepository.UpdateCorpSearchStatus("cce0b056-d2a3-485e-a02a-e34c957c4e40");

             //Assert
             Assert.IsNotNull(serviceChecklist); // Test if null
             Assert.IsTrue(serviceChecklist);
         }
         [TestMethod()]
         public void UpdateCorpSearchStatusFalseTest()
         {
             //Act 
             var serviceChecklist = _checkListTestRepository.UpdateCorpSearchStatus("8dda4efd-8504-4d1a-8370-a18b1667a210");

             //Assert
             Assert.IsNotNull(serviceChecklist); // Test if null
             Assert.IsFalse(serviceChecklist);
         }
        #region Junk
        //private readonly List<SubmissionMaster_ApplicationCheckList> list = new List
        //   <SubmissionMaster_ApplicationCheckList>()
        //{
        //    new SubmissionMaster_ApplicationCheckList()
        //    {
        //     SubMaster_ApplicationCheckListId = 25,
        //MasterId = "2a5676eb-5b79-45f4-9b13-758d3df378a3",
        //FEIN_SSN = false,
        //IsSubmissionCofo = false,
        //IsSubmissionHop = false,
        //IsSubmissioneHop =false,
        //IsCleanHandsVerify = false,
        //IsCorporateRegistration =false,
        //IsBHAddress =false,
        //IsBPAddress =false,
        //IsMailAddress =false,
        //IsResidentAgent =false,
        //IsDocForCleanHands =false,
        //IsDocForCofo =false,
        //IsDocForHop =false,
        //IsDocForeHop =false,
        //    },
        //     new SubmissionMaster_ApplicationCheckList()
        //    {
        //     SubMaster_ApplicationCheckListId = 25,
        //MasterId = "C49EACE2-2180-430E-BA2F-8B891D659421",
        //FEIN_SSN = false,
        //IsSubmissionCofo = false,
        //IsSubmissionHop = false,
        //IsSubmissioneHop =false,
        //IsCleanHandsVerify = false,
        //IsCorporateRegistration =false,
        //IsBHAddress =false,
        //IsBPAddress =false,
        //IsMailAddress =false,
        //IsResidentAgent =false,
        //IsDocForCleanHands =false,
        //IsDocForCofo =false,
        //IsDocForHop =false,
        //IsDocForeHop =false,
        //    }
        //};

        //private readonly SubmissionMaster_ApplicationCheckList submissionMaster_ApplicationCheckList = new SubmissionMaster_ApplicationCheckList
        //{
        //    SubMaster_ApplicationCheckListId = 25,
        //    MasterId = "2a5676eb-5b79-45f4-9b13-758d3df378a3",
        //    FEIN_SSN = false,
        //    IsSubmissionCofo = false,
        //    IsSubmissionHop = false,
        //    IsSubmissioneHop = false,
        //    IsCleanHandsVerify = false,
        //    IsCorporateRegistration = false,
        //    IsBHAddress = false,
        //    IsBPAddress = false,
        //    IsMailAddress = false,
        //    IsResidentAgent = false,
        //    IsDocForCleanHands = false,
        //    IsDocForCofo = false,
        //    IsDocForHop = false,
        //    IsDocForeHop = false,
        //};

        //private readonly Mock<ISubmissionMasterApplicationChcekListRepository> mockRepo = new Mock<ISubmissionMasterApplicationChcekListRepository>();

        //private readonly CofoHopDetailsModel cofoHopDetailsModel = new CofoHopDetailsModel()
        //{
        //    MasterId = "2a5676eb-5b79-45f4-9b13-758d3df378a3",
        //    Type = "Cofo",
        //    DateofIssue = "2014-06-12 00:00:00.000",
        //    Street = "Lane 10",
        //    StreetName = "Wilson Building",
        //    Quadrant = "NW",
        //    UnitType = "UNIT",
        //    Unit = "Washington",
        //    City = "Phoenix",
        //    State = "Arizona",
        //    Zip = "20004",
        //    Telephone = "(236) 952-6398"
        //};

        //private readonly SubmissionApplication submissionApplication = new SubmissionApplication()
        //{
        //    MasterId = "2a5676eb-5b79-45f4-9b13-758d3df378a3",
        //    PrimaryID = "CCC5DE09-1273-4998-A135-17265D3AADCC"
        //};
        //private readonly SubmissionTaxRevenue submissionTaxRevenue = new SubmissionTaxRevenue();

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

        //private readonly RenewModel renewModel = new RenewModel()
        //{
        //    SubCategoryName = "Inn and Motel",
        //    CategoryName = "General Contractor/Construction Manager",
        //    ActivityName = "Parking Facility",
        //    Endoresement = "General Service and Repair",
        //    CategoryId = "36ac8c39-fe45-46b5-87ec-ba7cfd14675d",
        //    ActivityId = "D96ABBB9-7437-4476-BBA6-72EF6A94F327",
        //    SubCategoryID = "FF0C5A9C-86B4-4378-BA8F-C1CE165B814E",
        //    LicenseAmount = 65,
        //    EndorsementFee = 25,
        //    ApplicationFee = 25,
        //    TechFee = 25,
        //    RAOFee = 25,
        //};

        //public void UpdateMailTrueStatusTest()
        //{
        //    string masterId = "2a5676eb-5b79-45f4-9b13-758d3df378a3";
        //    mockRepo.Setup(m => m.UpdateMailTrueStatusTest(masterId)).Returns(true);
        //    var runtimeOutput = mockRepo.Object.UpdateMailTrueStatusTest(masterId);
        //    Assert.AreEqual<bool>(true, runtimeOutput);
        //}
          
        // This is a method which returns void, so need to be rewritten
        //public void CofoServiceStatusTest()
        //{
        //    string masterId = "2a5676eb-5b79-45f4-9b13-758d3df378a3";
        //    string mailtype = "";
        //    mockRepo.Setup(m => m.CofoServiceStatus(masterId, mailtype));
        //    var runtimeOutput = mockRepo.Object.CofoServiceStatus(masterId, mailtype);
        //    Assert.AreEqual<bool>(true, runtimeOutput);
        //}

        // This is a method which returns void, so need to be rewritten
        //public void TaxServiceStatusTest()
        //{
        //    string masterId = "2a5676eb-5b79-45f4-9b13-758d3df378a3";
        //    bool UpdateStatus = true;
        //    mockRepo.Setup(m => m.TaxServiceStatus(masterId, mailtype));
        //    var runtimeOutput = mockRepo.Object.TaxServiceStatus(masterId, mailtype);
        //    Assert.AreEqual<bool>(true, runtimeOutput);
        //}
#endregion
    }
}
