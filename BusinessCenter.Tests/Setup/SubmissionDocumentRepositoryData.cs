using BusinessCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessCenter.Tests.Setup
{
    public class SubmissionDocumentRepositoryData
    {
         private readonly List<SubmissionDocument> _entities;
        public bool IsInitialized;

        public void AddSubmissionDocumentEntity(SubmissionDocument obj)
        {
            _entities.Add(obj);
        }

        public List<SubmissionDocument> SubmissionDocumentList
        {
            get { return _entities; }
        }


        public SubmissionDocumentRepositoryData()
        {
            IsInitialized = true;
            _entities = new List<SubmissionDocument>();

            AddSubmissionDocumentEntity(new SubmissionDocument()
            {
                SubmDocId = 1,
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                SubmissionCategoryID = 1,
                MasterCategoryDocId = "2",
                SubmittedDocumentType = "Document",
                ApprovedBy = "",
                DocRequired = "Fire Marshal Inspection Approval",
                FileName = "5107_DAPP15986431_FEMS_FPID_FireMarshalInsp.pdf",
                FileLocation = "BBLUpload//",
                DocStatus = "Open",
                Description = "All businesses that manufacture, wholesale, warehouse, distribute, sell or use toxic or flammable materials or provide services to large audiences (theatres, restaurants, and public halls) are subject to inspection to ensure the safety of customers, employees, and property.",
                CreatedDate = null,
                UpdatedDate = null
            });
            AddSubmissionDocumentEntity(new SubmissionDocument()
            {
                SubmDocId = 2,
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                SubmissionCategoryID = 3,
                MasterCategoryDocId = "304",
                SubmittedDocumentType = "Document",
                ApprovedBy = "",
                DocRequired = "Certified Food Supervisor Certification",
                FileName = "9313_DAPP15986431_DOH_HRLA_FoodSupCert.pdf",
                FileLocation = "BBLUpload//",
                DocStatus = "Open",
                Description = "Caterers must have Certified Food Supervisor(s) present on the premises during the hours the facility is open to the public. The name(s) and certification(s) of the food supervisor employee(s) must be provided to the Department of Health's inspectors.",
                CreatedDate = null,
                UpdatedDate = null,
                
            });
            AddSubmissionDocumentEntity(new SubmissionDocument()
            {
                SubmDocId = 3,
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                SubmissionCategoryID = 3,
                MasterCategoryDocId = "3",
                SubmittedDocumentType = "Document",
                ApprovedBy = "",
                DocRequired = "Health Inspection Approval",
                FileName = "9313_DAPP15986431_DOH_EHA_HealthInsp.pdf",
                FileLocation = "BBLUpload//",
                DocStatus = "Open",
                Description = "Must be inspected by and/or receive approval to operate from the Department of Health.",
                CreatedDate = null,
                UpdatedDate = null
            });
            AddSubmissionDocumentEntity(new SubmissionDocument()
            {
                SubmDocId = 4,
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                SubmissionCategoryID = 2,
                MasterCategoryDocId = "286",
                SubmittedDocumentType = "Document",
                ApprovedBy = "",
                DocRequired = "Health Inspection Approval",
                FileName = "9306_DAPP15986431_DOH_EHA_HealthInsp.pdf",
                FileLocation = "BBLUpload//",
                DocStatus = "Open",
                Description = "An approved health inspection report for review of embalming and tissue removal procedures from the Department of Health",
                CreatedDate = null,
                UpdatedDate = null
            });
            AddSubmissionDocumentEntity(new SubmissionDocument()
            {
                SubmDocId = 5,
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                SubmissionCategoryID = 999999,
                MasterCategoryDocId = "1306",
                SubmittedDocumentType = "Document",
                ApprovedBy = "",
                DocRequired = "Certificate of Occupancy (COfO) Document",
                FileName = "5107_DAPP15986431_DCRA_Zoning_COFO.pdf",
                FileLocation = "BBLUpload//",
                DocStatus = "Open",
                Description = "DCRA",
                CreatedDate = null,
                UpdatedDate = null
            });
        }
    }
}
