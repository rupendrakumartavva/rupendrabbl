using BusinessCenter.Data;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
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
   public class RefreshTokensRepositoryTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        //Repository declaration
        private RefreshTokensRepository _refreshTokensTestRepository;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);

            //RefreshTokensRepository Initialization
            _refreshTokensTestRepository = new RefreshTokensRepository(_testUnitOfWork);

            //Setup  RefreshTokensRepository FackData Initialization
            var testData = new RefreshTokensRepositoryData();
            _testDbContext.RefreshTokens.AddRange(testData.RefreshTokensEntitiesList);
            _testDbContext.SaveChanges();

        }
        [TestMethod]
        public void FindRefreshToken_Return_Test()
        {
            

            //Act 
            var refreshTokenRows = _refreshTokensTestRepository.FindRefreshToken("4hFZuEDUmoAQPDe4EeTmMS8x/ecIxYRDatgyGnmMlLo=").Result.ToList();

            //Assert
            Assert.IsNotNull(refreshTokenRows); // Test if null
           Assert.IsTrue(refreshTokenRows.Count()==1);
        }
        [TestMethod]
        public void GetAllRefreshToken_Return_Test()
        {
           
            //Act 
            var refreshTokenRows = _refreshTokensTestRepository.GetAllRefreshToken().ToList();

            //Assert
            Assert.IsNotNull(refreshTokenRows); // Test if null
            Assert.IsTrue(refreshTokenRows.Count() == 1);
        }
        [TestMethod]
        public void RemoveRefreshToken_Return_Test()
        {
          
            //Act 
            var refreshTokenRows = _refreshTokensTestRepository.RemoveRefreshToken("4hFZuEDUmoAQPDe4EeTmMS8x/ecIxYRDatgyGnmMlLo=").Result;

            //Assert
            Assert.IsNotNull(refreshTokenRows); // Test if null
            Assert.IsTrue(refreshTokenRows);
        }
        [TestMethod]
        public void UpdateRefreshTokenTime_Test()
        {
            var refreshToken = new RefreshTokens
            {
                Id = "lpbfnXRLNAcahdk4NtpqLUvOiBulKmVflxhld0B4gAw=",
                Subject = "UserName1234",
                ClientId = "352549070fb44ce793a5343a5f846dcc",
               
            };
            
            //Act 
            var refreshTokenRows = _refreshTokensTestRepository.UpdateRefreshTokenTime(refreshToken, "dcra").Result;

            //Assert
            Assert.IsNotNull(refreshTokenRows); // Test if null
            Assert.IsFalse(refreshTokenRows);
        }
        [TestMethod]
        public void AddRefreshToken_Return_Test()
        {
            //Initial Model Details
            var refreshToken = new RefreshTokens
            {
                Id = "lpbfnXRLNAcahdk4NtpqLUvOiBulKmVflxhld0B4gAw=",
                Subject = "UserName1234",
                ClientId = "352549070fb44ce793a5343a5f846dcc",
               
            };
            //Act 
            var refreshTokenRows = _refreshTokensTestRepository.AddRefreshToken(refreshToken).Result;

            //Assert
            Assert.IsNotNull(refreshTokenRows); // Test if null
            Assert.IsTrue(refreshTokenRows);
        }
        [TestMethod]
        public void UpdateRefreshToken_Return_Test()
        {
            //Initial Model Details
            var refreshToken = new RefreshTokens
            {
                Id = "4hFZuEDUmoAQPDe4EeTmMS8x/ecIxYRDatgyGnmMlLo=",
                Subject = "bharathpbk6699",
                ClientId = "352549070fb44ce793a5343a5f846dcc",

            };
            //Act 
            var refreshTokenRows = _refreshTokensTestRepository.AddRefreshToken(refreshToken).Result;

            //Assert
            Assert.IsNotNull(refreshTokenRows); // Test if null
            Assert.IsTrue(refreshTokenRows);
        }
        [TestMethod]
        public void RemoveRefreshToken_True_Test()
        {
           // Initial Model Details
            var refreshToken = new RefreshTokens
            { 
                Id = "4hFZuEDUmoAQPDe4EeTmMS8x/ecIxYRDatgyGnmMlLo=",
              
            };
            //Act 
            var refreshTokenRows = _refreshTokensTestRepository.RemoveRefreshToken(refreshToken).Result;

            //Assert
            Assert.IsNotNull(refreshTokenRows); // Test if null
            Assert.IsTrue(refreshTokenRows);
        }
        
    }
}
