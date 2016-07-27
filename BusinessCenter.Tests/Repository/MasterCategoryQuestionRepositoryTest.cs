using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using BusinessCenter.Tests.Setup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BusinessCenter.Tests.Repository
{
  public class MasterCategoryQuestionRepositoryTest
    {
        [TestClass]
        public class AbraRepositoryTest
        {
            private DbConnection _connection;
            private BusinessCenter.Tests.Common.TestContext _testDbContext;
            private UnitOfWork _testUnitOfWork;
            private MasterCategoryQuestionRepository _categoryQuestionTestRepository;
            private Mock<IMasterCategoryQuestionRepository> _mockcategoryQuestionRepository;
            private MasterCategoryQuestionData _mockData;


            [TestInitialize]
            public void Initialize()
            {
                //Test Context Repository
                _connection = Effort.DbConnectionFactory.CreateTransient();
                _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
                _testUnitOfWork = new UnitOfWork(_testDbContext);
                _categoryQuestionTestRepository = new MasterCategoryQuestionRepository(_testUnitOfWork);

                //Mocking Repository
                _mockcategoryQuestionRepository = new Mock<IMasterCategoryQuestionRepository>();
                _mockData = new MasterCategoryQuestionData();

                //Setup Data
                var testData = new MasterCategoryQuestionData();
                _testDbContext.MasterCategoryQuestion.AddRange(testData.CategoryQuestionEntitiesList);
                _testDbContext.SaveChanges();

            }


            [TestMethod]
            public void FindByIdTest()
            {

                //Act 
                var masterCategoryQuestionsRows = _categoryQuestionTestRepository.FindByID("Hotel", "Rooms");

                //Assert
                Assert.IsNotNull(masterCategoryQuestionsRows); // Test if null
                Assert.IsTrue(masterCategoryQuestionsRows.Count() == 1);
            }
            
            [TestMethod]
            public void FindBySecondaryNameTest()
            {

                //Act 
                var secondaryNameRows = _categoryQuestionTestRepository.FindBySecondaryName("Hotel");

                //Assert
                Assert.IsNotNull(secondaryNameRows); // Test if null
                Assert.IsTrue(secondaryNameRows.Count() ==2);
            }

            [TestMethod]
            public void InsertCategoryQuestionsBothTest()
            {
                var categoryQuestionModel = new PrimaryPhysicallocation
                {
                    
                    OldCategoryName = "",
                    OldUnitOne = "Quantity1",
                    OldUnitTwo = "Quantity2",
                    Description = "Description",
                    UnitOne = "Quantity1",
                    UnitTwo = "Quantity1",
                    UserQuestion1 = "UserQuestion1",
                    UserQuestion2 = "Quantity1"
                };
                //Act 
                var categoryQuestionRows = _categoryQuestionTestRepository.InsertUpdateCategoryName(categoryQuestionModel);

                //Assert
                Assert.IsNotNull(categoryQuestionRows); // Test if null
                Assert.IsTrue(categoryQuestionRows == true);
            }
            [TestMethod]
            public void UpdateCategoryQuestionsBothTest()
            {
                var categoryQuestionModel = new PrimaryPhysicallocation
                {
                    OldCategoryName = "Hotel",
                    OldUnitOne = "Rooms",
                    OldUnitTwo = "TABLES",
                    Description = "Description",
                    UnitOne = "Quantity1",
                    UnitTwo = "Quantity1",
                    UserQuestion1 = "UserQuestion1",
                    UserQuestion2 = "Quantity1"
                };
                //Act 
                var categoryQuestionRows = _categoryQuestionTestRepository.InsertUpdateCategoryName(categoryQuestionModel);

                //Assert
                Assert.IsNotNull(categoryQuestionRows); // Test if null
                Assert.IsTrue(categoryQuestionRows == true);
            }

            [TestMethod]
            public void InsertCategoryQuantity1Test()
            {
                var categoryQuestionModel = new PrimaryPhysicallocation
                {
                    OldCategoryName = "",
                    OldUnitOne = "Quantity1",
                    OldUnitTwo = "",
                    Description = "Billiard Parlor",
                    UnitOne = "Quantity1",
                    UnitTwo = "",
                    UserQuestion1 = "UserQuestion1",
                    UserQuestion2 = ""
                };
                //Act 
                var categoryQuestionRows = _categoryQuestionTestRepository.InsertUpdateCategoryName(categoryQuestionModel);

                //Assert
                Assert.IsNotNull(categoryQuestionRows); // Test if null
                Assert.IsTrue(categoryQuestionRows == true);
            }

            [TestMethod]
            public void UpdateCategoryQuantity1Test()
            {
                var categoryQuestionModel = new PrimaryPhysicallocation
                {
                    OldCategoryName = "Hotel",
                    OldUnitOne = "Rooms",
                    OldUnitTwo = "",
                    Description = "Hotel Description",
                    UnitOne = "New Rooms",
                    UnitTwo = "",
                    UserQuestion1 = "How many New Rooms in your Hotel?",
                    UserQuestion2 = ""
                };
                //Act 
                var categoryQuestionRows = _categoryQuestionTestRepository.InsertUpdateCategoryName(categoryQuestionModel);

                //Assert
                Assert.IsNotNull(categoryQuestionRows); // Test if null
                Assert.IsTrue(categoryQuestionRows == true);
            }
            [TestMethod]
            public void InsertCategoryQuantity2Test()
            {
                var categoryQuestionModel = new PrimaryPhysicallocation
                {
                    OldCategoryName = "",
                    OldUnitOne = "",
                    OldUnitTwo = "Quantity1",
                    Description = "Hotel Description",
                    UnitOne = "Quantity1",
                    UnitTwo = "",
                    UserQuestion1 = "UserQuestion1",
                    UserQuestion2 = ""
                };
                //Act 
                var categoryQuestionRows = _categoryQuestionTestRepository.InsertUpdateCategoryName(categoryQuestionModel);

                //Assert
                Assert.IsNotNull(categoryQuestionRows); // Test if null
                Assert.IsTrue(categoryQuestionRows == true);
            }

            [TestMethod]
            public void UpdateCategoryQuantity2Test()
            {
                var categoryQuestionModel = new PrimaryPhysicallocation
                {
                    OldCategoryName = "Hotel",
                    OldUnitOne = "",
                    OldUnitTwo = "TABLES",
                    Description = "Billiard Parlor",
                    UnitOne = "Quantity1",
                    UnitTwo = "",
                    UserQuestion1 = "How many Rooms in your Hotel?",
                    UserQuestion2 = ""
                };
                //Act 
                var categoryQuestionRows = _categoryQuestionTestRepository.InsertUpdateCategoryName(categoryQuestionModel);

                //Assert
                Assert.IsNotNull(categoryQuestionRows); // Test if null
                Assert.IsTrue(categoryQuestionRows == true);
            }
        }
    }
}
