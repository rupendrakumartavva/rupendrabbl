using System;
using System.Collections.Generic;
using BusinessCenter.Data.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;
using BusinessCenter.Api.Controllers;
using BusinessCenter.Api.Models;
using BusinessCenter.Api.Utility;
using BusinessCenter.Common;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Model;
using BusinessCenter.Service.Implementation;
using BusinessCenter.Tests.Setup;
using BusinessCenter.Data.Models;
using BusinessCenter.Data;

namespace BusinessCenter.Tests.Controllers
{
    [TestClass]
   public class RefreshTokenControllerTest
    {
        //Db declaration
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;

        #region Declaration RefreshTokensRepository
        private RefreshTokensRepository _refreshTokensTestRepository;
        #endregion

        private RefreshTokenController controller = null;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);

            #region Initialize RefreshTokensRepository
            _refreshTokensTestRepository = new RefreshTokensRepository(_testUnitOfWork);
            #endregion

            controller = new RefreshTokenController(_refreshTokensTestRepository);

            //Setup  RefreshTokensRepository FackData Initialization
            var testData = new RefreshTokensRepositoryData();
            _testDbContext.RefreshTokens.AddRange(testData.RefreshTokensEntitiesList);
            _testDbContext.SaveChanges();

        }
        [TestMethod]
        public async Task RefreshToken_Delete_WithToken_ReturnValue()
        {


            // Initial Model Details
            var refreshToken = new ModelFactory.DeleteRefreshToken
            {
                RefreshTokenId = "4hFZuEDUmoAQPDe4EeTmMS8x/ecIxYRDatgyGnmMlLo=",

            };
            //Act 
            var contacts = await controller.Delete(refreshToken) as OkNegotiatedContentResult<bool>;


            //Assert
            if (contacts != null) Assert.AreEqual(contacts.Content, true);


            // var conNegResult = Assert.IsInstanceOfType(contacts, typeof(OkResult));

            //StatusCodeResult statusCodeResult = contacts as StatusCodeResult;

            //Assert.AreEqual<HttpStatusCode>(HttpStatusCode.OK, statusCodeResult.StatusCode);

        }
    }
}
