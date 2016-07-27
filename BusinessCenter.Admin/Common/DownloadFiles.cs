using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using BusinessCenter.Admin.Models;

namespace BusinessCenter.Admin.Common
{
    public class DownloadFiles
    {
        public List<DownLoadFileInformation> GetFiles()
        {
            List<DownLoadFileInformation> lstFiles = new List<DownLoadFileInformation>();
            string drive = ConfigurationManager.AppSettings["UploadDrive"].ToString();
              string uploadFolder = ConfigurationManager.AppSettings["BBLUpload"].ToString();
              DirectoryInfo dirInfo = new DirectoryInfo(drive + uploadFolder);

            int i = 0;
            foreach (var item in dirInfo.GetFiles())
            {
                lstFiles.Add(new DownLoadFileInformation()
                {

                    FileId = i + 1,
                    FileName = item.Name,
                    FilePath = dirInfo.FullName + @"\" + item.Name
                });
                i = i + 1;
            }
            return lstFiles;
        }  
    }
}