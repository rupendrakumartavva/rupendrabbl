using System;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Linq;
using BusinessCenter.Data.Model;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BusinessCenter.Api.Utility
{
    public static class GenaralConvertion
    {
        public static bool CheckDateExpire(string fromDate, string toDate)
        {
            DateTime expireDate = DateTime.Parse(toDate);

            DateTime currentDate = DateTime.Parse(fromDate);

            TimeSpan remainingTimeSpan = expireDate - currentDate;




            if (remainingTimeSpan.TotalSeconds <= 86400 && remainingTimeSpan.TotalSeconds > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static DateTime ValidateGivenTime(string inputTime)
        {
            DateTime? lockoutEndDateUtc = String.IsNullOrEmpty(inputTime.ToString())
                                ? Convert.ToDateTime("01/01/1700")
                                : (DateTime?)Convert.ToDateTime(inputTime.ToString());

            return Convert.ToDateTime(lockoutEndDateUtc);
        }

        public static bool CheckExpireDate(string fromDate, string toDate)
        {




            DateTime expireDate = DateTime.Parse(toDate);

            DateTime currentDate = DateTime.Parse(fromDate);

            TimeSpan remainingTimeSpan = expireDate - currentDate;

            if (remainingTimeSpan.Days != 0)
            { return true; }


            if (remainingTimeSpan.TotalSeconds <= 86400 && remainingTimeSpan.TotalSeconds > 0)

                return true;

            return false;


        }

        public static bool CheckDateExpireForLockout(string fromDate, string toDate)
        {
            var expireDate = DateTime.Parse(toDate);
            var currentDate = DateTime.Parse(fromDate);
            var remainingTimeSpan = expireDate - currentDate;
            var g = remainingTimeSpan.TotalSeconds.ToString();
            if (g.Contains("-"))
            {
                return false;
            }
            else
            {
                if (remainingTimeSpan.TotalSeconds <= 300 && remainingTimeSpan.TotalSeconds > 0)
                    return true;


                return false;
            }



        }

        public static bool UploadFiles(string sourcePath, string destinationPath)
        {
            bool uploadstatus = false;
            try
            {
                FileInfo fi = new FileInfo(@sourcePath);
                if (!System.IO.Directory.Exists(destinationPath))
                {
                    System.IO.Directory.CreateDirectory(destinationPath);
                }
                string filename = fi.Name;
                fi.CopyTo(@destinationPath + "\\" + filename, true);
                uploadstatus = true;
            }
            catch (Exception )
            { uploadstatus = false; }
            return uploadstatus;
        }

        public class BblUploadData
        {
            public Guid MasterId { get; set; }
            public List<BblServiceDocuments> BblServiceDoc { get; set; }
        }

        public static BblServiceDocuments fileupload()
        {
            try
            {
                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    var httpPostedFile = HttpContext.Current.Request.Files["file_data"];
                    String data = HttpContext.Current.Request.Form["uploadData"];
                    int sno = Convert.ToInt32(HttpContext.Current.Request.Form["key"].ToString());
                    string UploadDrive = System.Web.Configuration.WebConfigurationManager.AppSettings["UploadDrive"].ToString();
                    string UploadFolder = System.Web.Configuration.WebConfigurationManager.AppSettings["UploadFolder"].ToString();
                    string destinationPath = UploadDrive + UploadFolder;
                    BblServiceDocuments uploadedData = JsonConvert.DeserializeObject<BblServiceDocuments>(data);
                    string fileName = "";
                    DateTime date=DateTime.Now;
                    Random rnd = new Random();
                    string categoryid = false ? "" : uploadedData.CategoryCode.ToString().Trim();
                    if (categoryid != "")
                    {

                        fileName = categoryid + "_" + uploadedData.LicenseName;
                       // fileName = categoryid + "_" + finaldata[0].BblServiceDoc[sno].SubmissionId;
                     
                    }
                    string Agency = uploadedData.Agency == null ? "" : uploadedData.Agency.ToString().Trim();
                    if (Agency != "")
                    {
                        fileName = fileName + "_" + Agency; 
                      
                    }
                //    string Division = finaldata[0].BblServiceDoc[sno].Division == null ? "" : finaldata[0].BblServiceDoc[sno].Division.ToString().Trim();
                    string div = uploadedData.Div == null ? "" : uploadedData.Div.ToString().Trim();
                    if (div != "")
                    {
                        fileName = fileName + "_" + div;

                    }
                    string shortname = uploadedData.ShortName == null ? "" : uploadedData.ShortName.ToString().Trim();
                    if (shortname != "")
                    {
                        fileName = fileName + "_" + shortname;
                    }
                    fileName = fileName +  ".pdf";
                    if (httpPostedFile != null)
                    {
                        if (!System.IO.Directory.Exists(destinationPath))
                        {
                            System.IO.Directory.CreateDirectory(destinationPath);
                        }
                        var fileSavePath = Path.Combine(destinationPath, fileName);
                        httpPostedFile.SaveAs(fileSavePath);
                        uploadedData.FileName = fileName;
                        uploadedData.FileLocation = UploadFolder;

                        return uploadedData;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

    }
}