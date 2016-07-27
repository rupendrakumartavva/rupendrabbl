using BusinessCenter.Api.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Controllers
{
     [TestClass]
   public class GenaralConvertionTest
    {
         [TestMethod]
         public void CheckDateExpire_False_Test()
         {
             string fromdate=(DateTime.Now.AddDays(-30)).ToString("MM/dd/yyyy");
             string todate = (DateTime.Now).ToString("MM/dd/yyyy");
             //Act 
             var checkdate = GenaralConvertion.CheckDateExpire(fromdate, todate);

             //Assert
             Assert.IsNotNull(checkdate); // Test if null
             Assert.IsFalse(checkdate);
         }
         [TestMethod]
         public void CheckDateExpire_True_Test()
         {
             string fromdate = (DateTime.Now.AddDays(-1)).ToString("MM/dd/yyyy");
             string todate = (DateTime.Now).ToString("MM/dd/yyyy");
             //Act 
             var checkdate = GenaralConvertion.CheckDateExpire(fromdate, todate);

             //Assert
             Assert.IsNotNull(checkdate); // Test if null
             Assert.IsTrue(checkdate);
         }
          [TestMethod]
         public void ValidateGivenTime_Test()
         {
            
             //Act 
             var checkdate = GenaralConvertion.ValidateGivenTime("2016-04-04");

             //Assert
             Assert.IsNotNull(checkdate); // Test if null
           Assert.IsTrue(checkdate.Day==4);
         }
          [TestMethod]
          public void CheckExpireDate_False_Test()
          {
              
              string fromdate = (DateTime.Now.AddHours(2)).ToString("MM/dd/yyyy");
              string todate = (DateTime.Now).ToString("MM/dd/yyyy");
              //Act 
              var checkdate = GenaralConvertion.CheckExpireDate(fromdate, todate);

              //Assert
              Assert.IsNotNull(checkdate); // Test if null
              Assert.IsFalse(checkdate);
          }
          [TestMethod]
          public void CheckExpireDate_True_Test()
          {
              string fromdate = (DateTime.Now.AddDays(-30)).ToString("MM/dd/yyyy");
              string todate = (DateTime.Now).ToString("MM/dd/yyyy");
              //Act 
              var checkdate = GenaralConvertion.CheckExpireDate(fromdate, todate);

              //Assert
              Assert.IsNotNull(checkdate); // Test if null
              Assert.IsTrue(checkdate);
          }

          [TestMethod]
          public void CheckDateExpireForLockout_False_Test()
          {

              string fromdate = (DateTime.Now.AddHours(2)).ToString("MM/dd/yyyy");
              string todate = (DateTime.Now).ToString("MM/dd/yyyy");
              //Act 
              var checkdate = GenaralConvertion.CheckDateExpireForLockout(fromdate, todate);

              //Assert
              Assert.IsNotNull(checkdate); // Test if null
              Assert.IsFalse(checkdate);
          }
          [TestMethod]
          public void UploadFiles_False_Test()
          {
              string fromdate = (DateTime.Now.AddDays(-1)).ToString("MM/dd/yyyy");
              string todate = (DateTime.Now).ToString("MM/dd/yyyy");
              //Act 
              var checkdate = GenaralConvertion.UploadFiles(fromdate, todate);

              //Assert
              Assert.IsNotNull(checkdate); // Test if null
              Assert.IsFalse(checkdate);
          }

          //[TestMethod]
          //public void fileupload_Test()
          //{
            
          //    //Act 
          //    var checkdate = GenaralConvertion.fileupload();

          //    //Assert
          //    Assert.IsNotNull(checkdate); // Test if null
          //  //  Assert.IsTrue(checkdate);
          //}
    }
}
