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
   public class Lookup_ExistingCategoriesTest
    {
        private DbConnection _connection;
        private BusinessCenter.Tests.Common.TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private Lookup_ExistingCategoriesRepository _existingCategoriesRepository;
        private Mock<ILookup_ExistingCategoriesRepository> _mockexistingCategoriesRepository;
        private Lookup_ExistingCategoriesData _mockData;
        [TestInitialize]
        public void Initialize()
        {
            //Test Context Repository
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _existingCategoriesRepository = new Lookup_ExistingCategoriesRepository(_testUnitOfWork);

            //Mocking Repository
            _mockexistingCategoriesRepository = new Mock<ILookup_ExistingCategoriesRepository>();
            _mockData = new Lookup_ExistingCategoriesData();

            //Setup Data
            var testData = new Lookup_ExistingCategoriesData();
            _testDbContext.Lookup_ExistingCategories.AddRange(testData.ExistingCategoriesList);
            _testDbContext.SaveChanges();

        }

        [TestMethod]
        public void FindByTest()
        {
            //Act 
            var CategoriesRows = _existingCategoriesRepository.FindBy("Hotel");

            //Assert
            Assert.IsNotNull(CategoriesRows); // Test if null
            Assert.IsTrue(CategoriesRows.Count() == 1);
        }

        [TestMethod]
        public void InsertCategoyLookUpTest()
        {
            var categoryLookup = new CategoryLookup { NewCategoryName = "Asbestos Business", ExistingCategory = "Asbestos Business" };
            //Act 
            var CategoriesRows = _existingCategoriesRepository.InsertUpdateCategoryLookUp(categoryLookup);

            //Assert
            Assert.IsNotNull(CategoriesRows); // Test if null
            Assert.IsTrue(CategoriesRows);
        }
        [TestMethod]
        public void UpdateCategoyLookUpTest()
        {
            var categoryLookup = new CategoryLookup { LookUpId=1,NewCategoryName = "Athletic Exhibition", ExistingCategory = "Athletic Exhibition" };
            //Act 
            var CategoriesRows = _existingCategoriesRepository.InsertUpdateCategoryLookUp(categoryLookup);

            //Assert
            Assert.IsNotNull(CategoriesRows); // Test if null
            Assert.IsTrue(CategoriesRows);
        }
        [TestMethod]
        public void InsertCategoyLookUpNullTest()
        {
            var categoryLookup = new CategoryLookup { NewCategoryName = null, ExistingCategory = null };
            //Act 
            var CategoriesRows = _existingCategoriesRepository.InsertUpdateCategoryLookUp(categoryLookup);

            //Assert
            Assert.IsNotNull(CategoriesRows); // Test if null
            Assert.IsTrue(CategoriesRows);
        }
        [TestMethod]
        public void UpdateCategoyLookUpNullTest()
        {
            var categoryLookup = new CategoryLookup { LookUpId = 1, NewCategoryName = null, ExistingCategory = null };
            //Act 
            var CategoriesRows = _existingCategoriesRepository.InsertUpdateCategoryLookUp(categoryLookup);

            //Assert
            Assert.IsNotNull(CategoriesRows); // Test if null
            Assert.IsTrue(CategoriesRows);
        }
        [TestMethod]
        public void NewCategoryFindBy()
        {
            //Act 
            var CategoriesRows = _existingCategoriesRepository.NewCategoryFindBy("Hotel");

            //Assert
            Assert.IsNotNull(CategoriesRows); // Test if null
            Assert.IsTrue(CategoriesRows.Count() == 1);
        }
    }
}
