using System.Collections.Generic;
using BusinessCenter.Data;

namespace BusinessCenter.Tests.Setup
{
    public class MasterCategoryDocumentRepositoryData
    {
        private readonly List<MasterCategoryDocument> _entities;
        public bool IsInitialized;

        public void AddMasterCategoryDocumentEntity(MasterCategoryDocument obj)
        {
            _entities.Add(obj);
        }

        public IEnumerable<MasterCategoryDocument> MasterCategoryDocumentList
        {
            get { return _entities; }
        }

        public MasterCategoryDocumentRepositoryData()
        {
            IsInitialized = true;
            _entities = new List<MasterCategoryDocument>();

            AddMasterCategoryDocumentEntity(new MasterCategoryDocument()
            {
                MasterCategoryDocId=1,
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
                Status = true,
                IsPrimaryCategoryDoc=true,
                IsSecondaryLicenseDoc=true
            });
            AddMasterCategoryDocumentEntity(new MasterCategoryDocument()
            {
                MasterCategoryDocId = 2,
                CategoryName = "Hotel",
                InitialLicense = "Y",
                PostLicensure = "Y",
                Renewal = "Y",
                Agency = "FEMS",
                Agency_FullName = "Fire and Emergency Medical Services (FEMS) Department",
                Div = "FPID",
                DivisionFullName = "Fire Prevention Inspection Division",
                SupportingDocuments = "Fire Marshal Inspection Approval",
                ShortDocName = "FireMarshalInsp",
                Description = "All businesses that manufacture, wholesale, warehouse, distribute, sell or use toxic or flammable materials or provide services to large audiences (theatres, restaurants, and public halls) are subject to inspection to ensure the safety of customers, employees, and property.",
                Status = true,
                IsPrimaryCategoryDoc = true,
                IsSecondaryLicenseDoc = true
            });

            AddMasterCategoryDocumentEntity(new MasterCategoryDocument()
            {
                MasterCategoryDocId = 3,
                CategoryName = "Charitable Exempt",
                InitialLicense = "Y",
                PostLicensure = "Y",
                Renewal = "Y",
                Agency = "APPLICANT",
                Agency_FullName = "Applicant",
                Div = "NA",
                DivisionFullName = "NA",
                SupportingDocuments = "Affidavit",
                ShortDocName = "Affidavit",
                Description = "A written and sworn version of declaration/letter stating that exemption status is/was in place on the date of the submission of application",
                Status = true,
                IsPrimaryCategoryDoc = true,
                IsSecondaryLicenseDoc = true
            });


            AddMasterCategoryDocumentEntity(new MasterCategoryDocument()
              {
                  MasterCategoryDocId = 4,
                  CategoryName = "Charitable Exempt",
                  InitialLicense = "N",
                  PostLicensure = "N",
                  Renewal = "N",
                  Agency = "DCRA",
                  Agency_FullName = "Department of Consumer and Regulatory Affairs (DCRA)",
                  Div = "BLD",
                  DivisionFullName = "Business Licensing Division (BLD)",
                  SupportingDocuments = "Solicitor information Card(Optional)",
                  ShortDocName = "EmployeeList",
                  Description = "A list of the names and addresses of the persons who will be soliciting on behalf of the organization must be submitted in triplicate (3 copies).",
                  Status =true,
                  IsPrimaryCategoryDoc = true,
                  IsSecondaryLicenseDoc = true
              });
            AddMasterCategoryDocumentEntity(new MasterCategoryDocument()
            {
                MasterCategoryDocId = 5,
                CategoryName = "Charitable Exempt",
                InitialLicense = "Y",
                PostLicensure = "Y",
                Renewal = "Y",
                Agency = "IRS",
                Agency_FullName = "Internal Revenue Services (IRS)",
                Div = "FED",
                DivisionFullName = "Federal",
                SupportingDocuments = "IRS Determination Letter",
                ShortDocName = "IRSDetermLtr",
                Description = "An official ruling letter issued by the Internal Revenue Service that allows the organization to claim/have exempt status.",
                Status = true,
                IsPrimaryCategoryDoc = true,
                IsSecondaryLicenseDoc = true
            });
            AddMasterCategoryDocumentEntity(new MasterCategoryDocument()
            {
                MasterCategoryDocId = 6,
                CategoryName = "Charitable Exempt",
                InitialLicense = "N",
                PostLicensure = "Y",
                Renewal = "N",
                Agency = "OTR",
                Agency_FullName = "Office of Tax and Revenue (OTR)",
                Div = "BTSC",
                DivisionFullName = "Business Tax Service Center",
                SupportingDocuments = "D.C. Determination Letter",
                ShortDocName = "OTRDetermLtr",
                Description = "An official ruling letter issued by the D.C. Office of Tax and Revenue that allows the organization to claim/have exempt status.",
                Status = true,
                IsPrimaryCategoryDoc = true,
                IsSecondaryLicenseDoc = true
            });
            AddMasterCategoryDocumentEntity(new MasterCategoryDocument()
            {
                MasterCategoryDocId = 7,
                CategoryName = "Auctioneer",
                InitialLicense = "Y",
                PostLicensure = "Y",
                Renewal = "Y",
                Agency = "APPLICANT",
                Agency_FullName = "Applicant",
                Div = "NA",
                DivisionFullName = "NA",
                SupportingDocuments = "Employer Certification Letter",
                ShortDocName = "EmploymentCert",
                Description = "Certifying that this employee is employed with their company",
                Status = true,
                IsPrimaryCategoryDoc = true,
                IsSecondaryLicenseDoc = true
            });
            AddMasterCategoryDocumentEntity(new MasterCategoryDocument()
            {
                MasterCategoryDocId = 8,
                CategoryName = "Auctioneer",
                InitialLicense = "Y",
                PostLicensure = "Y",
                Renewal = "Y",
                Agency = "APPLICANT",
                Agency_FullName = "Applicant",
                Div = "NA",
                DivisionFullName = "NA",
                SupportingDocuments = "Police Criminal History Report (Non-DC Residents)",
                ShortDocName = "CrimHistory",
                Description = "A certified copy of a Police Criminal History Report from the applicant's jurisdiction of residence dated within 30 days of submission.",
                Status = true,
                IsPrimaryCategoryDoc = true,
                IsSecondaryLicenseDoc = true
            });
            AddMasterCategoryDocumentEntity(new MasterCategoryDocument()
            {
                MasterCategoryDocId = 9,
                CategoryName = "Auctioneer",
                InitialLicense = "Y",
                PostLicensure = "Y",
                Renewal = "N",
                Agency = "APPLICANT",
                Agency_FullName = "Applicant",
                Div = "NA",
                DivisionFullName = "NA",
                SupportingDocuments = "Proof of Residency and Age",
                ShortDocName = "",
                Description = "Copy of current driver's or non-driver's license (with photo)",
                Status = true,
                IsPrimaryCategoryDoc = true,
                IsSecondaryLicenseDoc = true
            });
            AddMasterCategoryDocumentEntity(new MasterCategoryDocument()
            {
                MasterCategoryDocId = 10,
                CategoryName = "Auctioneer",
                InitialLicense = "Y",
                PostLicensure = "Y",
                Renewal = "Y",
                Agency = "APPLICANT",
                Agency_FullName = "Applicant",
                Div = "NA",
                DivisionFullName = "NA",
                SupportingDocuments = "Three (3) Passport-sized Photos",
                ShortDocName = "PassportPhoto3",
                Description = "",
                Status = true,
                IsPrimaryCategoryDoc = true,
                IsSecondaryLicenseDoc = true
            });
            AddMasterCategoryDocumentEntity(new MasterCategoryDocument()
            {
                MasterCategoryDocId = 11,
                CategoryName = "Auctioneer",
                InitialLicense = "Y",
                PostLicensure = "Y",
                Renewal = "N",
                Agency = "MPD",
                Agency_FullName = "Metropolitan Police Department (MPD)",
                Div = "PAWN",
                DivisionFullName = "Pawn Unit(BLD)",
                SupportingDocuments = "Metropolitan Police Department Interview",
                ShortDocName = "MPDInterview",
                Description = "Must submit a signed Statement of Approval in order to conduct an Auction in Washington, DC.",
                Status = true,
                IsPrimaryCategoryDoc = true,
                IsSecondaryLicenseDoc = true
            });
            AddMasterCategoryDocumentEntity(new MasterCategoryDocument()
            {
                MasterCategoryDocId = 12,
                CategoryName = "Auctioneer",
                InitialLicense = "Y",
                PostLicensure = "Y",
                Renewal = "Y",
                Agency = "MPD",
                Agency_FullName = "Metropolitan Police Department (MPD)",
                Div = "PCH",
                DivisionFullName = "Police Criminal History Report Division",
                SupportingDocuments = "Police Criminal History Report (Form PD 70) DC Residents Only",
                ShortDocName = "MPDCrimHist",
                Description = "A certified copy of applicant's Police Criminal History Report.",
                Status = true,
                IsPrimaryCategoryDoc = true,
                IsSecondaryLicenseDoc = true
            });
            AddMasterCategoryDocumentEntity(new MasterCategoryDocument()
            {
                MasterCategoryDocId = 13,
                CategoryName = "Home Improvement Contractor",
                InitialLicense = "Y",
                PostLicensure = "Y",
                Renewal = "Y",
                Agency = "APPLICANT",
                Agency_FullName = "Applicant",
                Div = "NA",
                DivisionFullName = "NA",
                SupportingDocuments = "Certificate of Liability Insurance",
                ShortDocName = "Insurace",
                Description = "Applicant shall furnish a certificate of insurance for the license period; therefore, each applicant must secure commercial general liability insuranceCertificate of Insurance: Certificate Holder: DCRA with current Address ;  Insured:  Business name, premise address , city, state and zip code.  Description of Operations:  Gen Contr/Construction Mngr in Washington, DC .   Current one year Certificate.Certificate of Liability Insurance in the amount of - Property Damage ($50,000), Public Liability ($100,000)",
                Status = true,
                IsPrimaryCategoryDoc = true,
                IsSecondaryLicenseDoc = true
            });
            AddMasterCategoryDocumentEntity(new MasterCategoryDocument()
            {
                MasterCategoryDocId = 14,
                CategoryName = "Home Improvement Contractor",
                InitialLicense = "Y",
                PostLicensure = "Y",
                Renewal = "Y",
                Agency = "APPLICANT",
                Agency_FullName = "Applicant",
                Div = "NA",
                DivisionFullName = "NA",
                SupportingDocuments = "Police Criminal History Report (Non-DC Residents)",
                ShortDocName = "CrimHistory",
                Description = "A certified copy of a Police Criminal History Report from the applicant's jurisdiction of residence dated within 30 days of submission.",
                Status = true,
                IsPrimaryCategoryDoc = true,
                IsSecondaryLicenseDoc = true
            });
            AddMasterCategoryDocumentEntity(new MasterCategoryDocument()
            {
                MasterCategoryDocId = 15,
                CategoryName = "Home Improvement Contractor",
                InitialLicense = "Y",
                PostLicensure = "Y",
                Renewal = "Y",
                Agency = "DCRA",
                Agency_FullName = "Department of Consumer and Regulatory Affairs (DCRA)",
                Div = "BLD",
                DivisionFullName = "Business Licensing Division (BLD)",
                SupportingDocuments = "Designation Letter",
                ShortDocName = "EmployeeList",
                Description = "A form document that lists salespersons employed with Home Improvement Contactor's business (HIC).",
                Status = true,
                IsPrimaryCategoryDoc = true,
                IsSecondaryLicenseDoc = true
            });
            AddMasterCategoryDocumentEntity(new MasterCategoryDocument()
            {
                MasterCategoryDocId = 16,
                CategoryName = "Home Improvement Contractor",
                InitialLicense = "Y",
                PostLicensure = "Y",
                Renewal = "Y",
                Agency = "DCRA",
                Agency_FullName = "Department of Consumer and Regulatory Affairs (DCRA)",
                Div = "BLD",
                DivisionFullName = "Business Licensing Division (BLD)",
                SupportingDocuments = "Surety Bond - Home Improvement Surety Bond",
                ShortDocName = "Bond25k",
                Description = "A bond obtained by applicant from an insurance company, surety company or post the full amount with DCRA, if/of their choosing to cover the full duration of the license period (two years) in the amount of $25,000.",
                Status = true,
                IsPrimaryCategoryDoc = true,
                IsSecondaryLicenseDoc = true
            });
            AddMasterCategoryDocumentEntity(new MasterCategoryDocument()
            {
                MasterCategoryDocId = 17,
                CategoryName = "Home Improvement Contractor",
                InitialLicense = "Y",
                PostLicensure = "Y",
                Renewal = "Y",
                Agency = "DCRA",
                Agency_FullName = "Department of Consumer and Regulatory Affairs (DCRA)",
                Div = "BLD",
                DivisionFullName = "Business Licensing Division (BLD)",
                SupportingDocuments = "Three (3) Contract Samples",
                ShortDocName = "SampleContract3",
                Description = "This is a typeset document that the applicant provides and is used in the operation of business",
                Status = true,
                IsPrimaryCategoryDoc = true,
                IsSecondaryLicenseDoc = true
            });
            AddMasterCategoryDocumentEntity(new MasterCategoryDocument()
            {
                MasterCategoryDocId = 18,
                CategoryName = "Home Improvement Contractor",
                InitialLicense = "Y",
                PostLicensure = "Y",
                Renewal = "Y",
                Agency = "MPD",
                Agency_FullName = "Metropolitan Police Department (MPD)",
                Div = "PCH",
                DivisionFullName = "Police Criminal History Report Division",
                SupportingDocuments = "Police Criminal History Report (Form PD 70) DC Residents Only",
                ShortDocName = "MPDCrimHist",
                Description = "A certified copy of applicant's Police Criminal History Report",
                Status = true,
                IsPrimaryCategoryDoc = true,
                IsSecondaryLicenseDoc = true
            });
        }
    }
}