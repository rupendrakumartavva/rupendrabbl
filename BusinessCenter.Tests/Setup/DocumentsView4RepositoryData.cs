using BusinessCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Setup
{
    public class DocumentsView4RepositoryData
    {
        private readonly List<BblLicenseView4> _entities;
        public bool IsInitialized;

        public void AddBblLicenseView4Entity(BblLicenseView4 obj)
        {
            _entities.Add(obj);
        }

        public List<BblLicenseView4> BblLicenseView4EntitiesList
        {
            get { return _entities; }
        }


        public DocumentsView4RepositoryData()
        {
            IsInitialized = true;
            _entities = new List<BblLicenseView4>();

            AddBblLicenseView4Entity(new BblLicenseView4()
            {
                APPID=1,
                Application_License_No_ = "10006762",
                FileName = "LREN13013234.pdf",
                SubmissionFileLocation = "BBLUpload//",
                DocumentSubmissionType = "Online",
                CategoryName = "General Business Licenses",
                MasterCategoryDocId = -99992,
                SubmissionType = "Renewal",
                Agency = "DCRA",
                Agency_FullName = "Department of Consumer and Regulatory Affairs (DCRA)",
                Div = "BLD",
                DivisionFullName = "Business Licensing Division (BLD)",
                SupportingDocuments = "Licence",
                ShortDocName = "Licence",
                Description = "A License pdf issued as part of the instantaneous Online approval for certain category types that require no supporting documents",
                Created_Date = System.DateTime.Now,
                UpDated_Date = System.DateTime.Now
            });
            AddBblLicenseView4Entity(new BblLicenseView4()
            {
                APPID = 2,
                Application_License_No_ = "400316957915",
                FileName = "EHOP16943939.pdf",
                SubmissionFileLocation = "BBLUpload//",
                DocumentSubmissionType = "Online",
                CategoryName = "General Business Licenses",
                MasterCategoryDocId = -99991,
                SubmissionType = "Submission",
                Agency = "DCRA",
                Agency_FullName = "Department of Consumer and Regulatory Affairs (DCRA)",
                Div = "BLD",
                DivisionFullName = "Business Licensing Division (BLD)",
                SupportingDocuments = "Expedited Home Occupation Permit",
                ShortDocName = "EHOP",
                Description = "An Expedited Home Occupation Permit that is issued Online for certain category types based they meet the criteria of all qualified conditions",
                Created_Date = System.DateTime.Now,
                UpDated_Date = System.DateTime.Now
            });
          


        }
    }
}
