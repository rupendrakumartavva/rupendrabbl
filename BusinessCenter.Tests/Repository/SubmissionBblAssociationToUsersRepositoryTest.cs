using BusinessCenter.Data;
using BusinessCenter.Data.Interface;
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
using BusinessCenter.Data.Model;


namespace BusinessCenter.Tests.Repository
{
    [TestClass]
    public class SubmissionBblAssociationToUsersRepositoryTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private SubmissionBblAssociationToUsersRepository _submissionBblAssociationToUsersTestRepository;
        private Mock<ISubmissionBblAssociationToUsersRepository> _mockSubmissionBblAssociationToUsersRepository;
        private SubmissionBblAssociationToUsersRepositoryData _mockData;

        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _submissionBblAssociationToUsersTestRepository = new SubmissionBblAssociationToUsersRepository(_testUnitOfWork);

            //Mocking Repository
            _mockSubmissionBblAssociationToUsersRepository = new Mock<ISubmissionBblAssociationToUsersRepository>();
            _mockData = new SubmissionBblAssociationToUsersRepositoryData();

            //Setup Data
            var testData = new SubmissionBblAssociationToUsersRepositoryData();
            _testDbContext.SubmissionBblAssociationToUsers.AddRange(testData.SubmissionBblAssociationToUsersEntitiesList);
            _testDbContext.SaveChanges();
        }

        [TestMethod]
        public void InsertTransferLicense_WithNewDetails_Test()
        {
            var submissionBblAssociationToUsersEntity = new Submissiontransfer
            {
               // SubmissionBblAssociationOtherUid = "",
                MasterId = "887fb5ad-114e-4d83-b5c9-ca214d89f117",
                FromUserId = "FF6E6EC1-2976-4C34-B92D-22E4EB00A897",
                ToUserId = "44AF8B96-A586-4930-A211-42EB0B72C97311111",
              //  DateOfTransfer = null,
                CreatedBy = "2AC53E53-87D0-468F-9628-4AEBA1120613",
                ReasonForTransfer = "",
            };
            //Act 
            var submissionBblAssociationToUsersRows = _submissionBblAssociationToUsersTestRepository.InsertTransferLicense(submissionBblAssociationToUsersEntity);
            //Assert
            Assert.IsNotNull(submissionBblAssociationToUsersRows); // Test if null
            Assert.IsTrue(submissionBblAssociationToUsersRows);
        }

        [TestMethod]
        public void InsertTransferLicense_WithExistingData_Test()
        {
            var submissionBblAssociationToUsersEntity = new Submissiontransfer
            {
              //  SubmissionBblAssociationOtherUid = "",
                MasterId = "887fb5ad-114e-4d83-b5c9-ca214d89f117",
                FromUserId = "FF6E6EC1-2976-4C34-B92D-22E4EB00A897",
                ToUserId = "8EB70E26-725E-4E52-9109-CF9C37F3B980",
                LicenseNumber = "LAPP16900002",
              //  DateOfTransfer = null,
                CreatedBy = "2AC53E53-87D0-468F-9628-4AEBA1120613",
                ReasonForTransfer = "",
            };
            //Act 
            var submissionBblAssociationToUsersRows = _submissionBblAssociationToUsersTestRepository.InsertTransferLicense(submissionBblAssociationToUsersEntity);
            //Assert
            Assert.IsNotNull(submissionBblAssociationToUsersRows); // Test if null
            Assert.IsTrue(submissionBblAssociationToUsersRows == true);
        }
    }
}
