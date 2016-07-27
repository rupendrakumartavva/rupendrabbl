using BusinessCenter.Data;
using BusinessCenter.Data.Common;
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
using BusinessCenter.Data.Implementation;
using BusinessCenter.Tests.Setup;

namespace BusinessCenter.Tests.Repository
{
     [TestClass]
    public class MasterCategoryDocumentRepositoryTest
     {
         private DbConnection _connection;
         private BusinessCenter.Tests.Common.TestContext _testDbContext;
         private UnitOfWork _testUnitOfWork;
         private MasterCategoryDocumentRepository _masterCategoryDocumentTestRepository;

         private MasterCategoryDocumentRepositoryData _mockData;

         [TestInitialize]
         public void Initialize()
         {
             //Test Context Repository
             _connection = Effort.DbConnectionFactory.CreateTransient();
             _testDbContext = new BusinessCenter.Tests.Common.TestContext(_connection);
             _testUnitOfWork = new UnitOfWork(_testDbContext);


             _masterCategoryDocumentTestRepository = new MasterCategoryDocumentRepository(_testUnitOfWork);

             //Mocking Repository
             _mockData = new MasterCategoryDocumentRepositoryData();

             //Setup Data
             var testData = new MasterCategoryDocumentRepositoryData();
             _testDbContext.MasterCategoryDocument.AddRange(testData.MasterCategoryDocumentList);
             _testDbContext.SaveChanges();

         }

      
         [TestMethod]
         public void FindByDocNameTest()
         {
          
             //Act 
             var categoryNameRows = _masterCategoryDocumentTestRepository.FindByDocName("Hotel");

             //Assert
             Assert.IsNotNull(categoryNameRows); // Test if null
             Assert.IsTrue(categoryNameRows.Count() == 2);
         }
         [TestMethod]
         public void FindByIDTest()
         {

             //Act 
             var categoryNameRows = _masterCategoryDocumentTestRepository.FindByID("Hotel","");

             //Assert
             Assert.IsNotNull(categoryNameRows); // Test if null
             Assert.IsTrue(categoryNameRows.Count() == 1);
         }
         [TestMethod]
         public void FindByRenewIDTest()
         {

             //Act 
             var categoryNameRows = _masterCategoryDocumentTestRepository.FindByRenewID("Hotel","");

             //Assert
             Assert.IsNotNull(categoryNameRows); // Test if null
             Assert.IsTrue(categoryNameRows.Count() == 1);
         }
         [TestMethod]
         public void FindByDocNameBasedonCategoryNameTest()
         {

             //Act 
             var categoryNameRows = _masterCategoryDocumentTestRepository.FindByDocNameBasedonCategoryName("Hotel");

             //Assert
             Assert.IsNotNull(categoryNameRows); // Test if null
             Assert.IsTrue(categoryNameRows.Count() == 2);
         }
      
         [TestMethod]
         public void UpdateCategoryNameTest()
         {

             //Act 
             var categoryNameRows = _masterCategoryDocumentTestRepository.UpdateCategoryName("Hotel", "Hotels");

             //Assert
             Assert.IsNotNull(categoryNameRows); // Test if null
             Assert.IsTrue(categoryNameRows == true);
         }
         [TestMethod]
         public void InsertCategoryDocuments_Test()
         {
             var masterCategoryDocumentEntity = new MasterCategoryDocumentModel
             {
                 MasterCategoryDocId = 0, CategoryName = "Hotel", InitialLicense = "N",
                 PostLicensure = "Y",
                 Renewal = "N",
                 Agency = "DOH",
                 Agency_FullName = "Department of Health (DOH)",
                 Div = "EHA",
                 DivisionFullName = "Environmental Health Administration -- Bureau of Food, Drug, and Radiation Protection (Food Protection Division)",
                 SupportingDocuments = "Health Inspection Approval",
                 ShortDocName = "HealthInsp",
                 Description = "Must be inspected by and/or receive approval to operate from the Department of Health.",
                 Status = true
             
             };
             //Act 
             var categoryDocumentRows = _masterCategoryDocumentTestRepository.InsertUpdateCategoryDocuments(masterCategoryDocumentEntity);

             //Assert
             Assert.IsNotNull(categoryDocumentRows); // Test if null
             Assert.IsTrue(categoryDocumentRows == 1);
         }
         [TestMethod]
         public void UpdateCategoryDocuments_Test()
         {
             var masterCategoryDocumentEntity = new MasterCategoryDocumentModel
             {
                 MasterCategoryDocId = 1,
                 CategoryName = "Hotel",
                 InitialLicense = "N",
                 PostLicensure = "Y",
                 Renewal = "N",
                 Agency = "DOH",
                 Agency_FullName = "Department of Health (DOH)",
                 Div = "EHA",
                 DivisionFullName = "Environmental Health Administration -- Bureau of Food, Drug, and Radiation Protection (Food Protection Division)",
                 SupportingDocuments = "Health Inspection Approval",
                 ShortDocName = "HealthInsp",
                 Description = "Must be inspected by and/or receive approval to operate from the Department of Health.",
                 Status = false

             };
             //Act 
             var categoryDocs = _masterCategoryDocumentTestRepository.InsertUpdateCategoryDocuments(masterCategoryDocumentEntity);

             //Assert
             Assert.IsNotNull(categoryDocs); // Test if null
             Assert.IsTrue(categoryDocs == 2);
         }
         [TestMethod]
         public void DeleteBusinessActivityTest()
         {
             var categoryDocumentModel = new MasterCategoryDocumentModel { MasterCategoryDocId = 1 };
             //Act 
             var categoryDocs = _masterCategoryDocumentTestRepository.DeleteCategoryDocument(categoryDocumentModel);

             //Assert
             Assert.IsNotNull(categoryDocs); // Test if null
             Assert.IsTrue(categoryDocs == true);
         }
         [TestMethod]
         public void FindByDocIDTest()
         {

             //Act 
             var categoryDocs = _masterCategoryDocumentTestRepository.FindByDocID(1).ToList();

             //Assert
             Assert.IsNotNull(categoryDocs); // Test if null
             Assert.IsTrue(categoryDocs.Count() == 1);
         }
         [TestMethod]
         public void FindByDocBasedonDocIdTest()
         {

             //Act 
             var categoryDocs = _masterCategoryDocumentTestRepository.FindByDocBasedonDocId(1).ToList();

             //Assert
             Assert.IsNotNull(categoryDocs); // Test if null
             Assert.IsTrue(categoryDocs.Count() == 1);
         }
         [TestMethod]
         public void FindByRenewIDSeondary_Test()
         {

             //Act 
             var categoryNameRows = _masterCategoryDocumentTestRepository.FindByRenewID("Hotel", "SECONDARYCATEGORY");

             //Assert
             Assert.IsNotNull(categoryNameRows); // Test if null
             Assert.IsTrue(categoryNameRows.Count() == 1);
         }
    }
}
