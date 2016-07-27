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
    public class OplaRepositoryTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private OplaRepository _oplaTestRepository;
        private Mock<IOplaRepository> _mockOplaRepository;
        private OplaRepositoryData _mockData;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _oplaTestRepository = new OplaRepository(_testUnitOfWork);

            //Mocking Repository
            _mockOplaRepository = new Mock<IOplaRepository>();
            _mockData = new OplaRepositoryData();

            //Setup Data
            var testData = new OplaRepositoryData();
            _testDbContext.DCBC_ENTITY_OPLA.AddRange(testData.OplaEntitiesList);
            _testDbContext.SaveChanges();

        }
        [TestMethod()]
        public void GetCorpAllTest()
        {
            //Act 
            var bblRows = _oplaTestRepository.GetOplaLookupAll();

            //Assert
            Assert.IsNotNull(bblRows); // Test if null
            Assert.IsTrue(bblRows.Count() == 2);

        }
       
    }
}
#region Junk
//   private readonly List<DCBC_ENTITY_OPLA> list = new List<DCBC_ENTITY_OPLA>()
//   {
//       new DCBC_ENTITY_OPLA()
//       {
//           DCBC_ENTITY_ID =1,
//  REC_STATUS = true,
//  DCBC_ENTITY_SOURCE = "",
//  BOARD_ABBREVIATION = "",
//  LICENSE_PREFIX = "",
// LICENSE_NUMBER = "",
// LICENSE_STATUS_CODE = "",
//  Licensee_Name_1 = "",
//   Licensee_Name_2  = "",
//  Licensee_Name_3  = "",
//  Licensee_Name_4  = "",
//  BUSINESS_ADDRESS = "",
//  BUSINESS_CITY  = "",
//  BUSINESS_STATE  = "",
//  BUSINESS_ZIP  = "",
// BUSINESS_PHONE  = "",
//HOME_ADDRESS  = "",
//    HOME_CITY  = "",
//    HOME_STATE  = "",
//   HOME_ZIP  = "",
//    HOME_PHONE  = "",
//   Preferred_Address_Ind  = "",
//   EMPLOYER_LICENSE_PREFIX1  = "",
//   EMPLOYER_LICENSE_NUMBER  = "",
//   Organization_EMPLOYER_NAME  = "",

//   EMPLOYER_LICENSE_STATUS_CODE = "",
//   EMPLOYER_ADDRESS  = "",
//  EMPLOYER_CITY  = "",
//    EMPLOYER_STATE = "",
//    EMPLOYER_ZIP  = "",
//   EMPLOYER_PHONE = "",
//    FULL_NAME  = "",
//   OPLA_LICENSE_CATEGORY = "",
//   OPLA_SOURCE  = "",

//   License_Board  = "",
//  License_Type  = "",
//       },
//     new DCBC_ENTITY_OPLA()
//       {
//           DCBC_ENTITY_ID =1,
//  REC_STATUS = true,
//  DCBC_ENTITY_SOURCE = "",
//  BOARD_ABBREVIATION = "",
//  LICENSE_PREFIX = "",
// LICENSE_NUMBER = "",
// LICENSE_STATUS_CODE = "",
//  Licensee_Name_1 = "",
//   Licensee_Name_2  = "",
//  Licensee_Name_3  = "",
//  Licensee_Name_4  = "",
//  BUSINESS_ADDRESS = "",
//  BUSINESS_CITY  = "",
//  BUSINESS_STATE  = "",
//  BUSINESS_ZIP  = "",
// BUSINESS_PHONE  = "",
//HOME_ADDRESS  = "",
//    HOME_CITY  = "",
//    HOME_STATE  = "",
//   HOME_ZIP  = "",
//    HOME_PHONE  = "",
//   Preferred_Address_Ind  = "",
//   EMPLOYER_LICENSE_PREFIX1  = "",
//   EMPLOYER_LICENSE_NUMBER  = "",
//   Organization_EMPLOYER_NAME  = "",

//   EMPLOYER_LICENSE_STATUS_CODE = "",
//   EMPLOYER_ADDRESS  = "",
//  EMPLOYER_CITY  = "",
//    EMPLOYER_STATE = "",
//    EMPLOYER_ZIP  = "",
//   EMPLOYER_PHONE = "",
//    FULL_NAME  = "",
//   OPLA_LICENSE_CATEGORY = "",
//   OPLA_SOURCE  = "",

//   License_Board  = "",
//  License_Type  = "",
//       },
//   };

//   // IEnumerable<DCBC_ENTITY_OPLA> GetOplaLookupAll()
#endregion