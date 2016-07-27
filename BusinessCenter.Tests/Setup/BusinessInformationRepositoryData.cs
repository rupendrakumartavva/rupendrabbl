using BusinessCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Setup
{
    public class BusinessInformationRepositoryData
    {
        private readonly List<BblLicenseView> _entities;
        public bool IsInitialized;

        public void AddBblLicenseViewEntity(BblLicenseView obj)
        {
            _entities.Add(obj);
        }

        public List<BblLicenseView> BblLicenseViewEntitiesList
        {
            get { return _entities; }
        }


        public BusinessInformationRepositoryData()
        {
            IsInitialized = true;
            _entities = new List<BblLicenseView>();

            AddBblLicenseViewEntity(new BblLicenseView()
            {

                Application_Unique_ID = "18329841-723b-45cf-855a-ed2b24eace0f",
                Applicant_Name = "TEST",
                Full_Name = "Lavanya Sikhinam",
                Online_App_License_Status = "UnderReview",
                Application_License_No_ = "LAPP16957047",
                Application_Type = "Bed and Breakfast",
                App_Type = "B",
                License_Period = 2,
                ApplicationFee = Convert.ToDecimal(70.00),
                EndorsementFee = Convert.ToDecimal(25.00),
                LicenseFee = Convert.ToDecimal(208.00),
                //RAOFee =
                ESFFee = Convert.ToDecimal(30.30000),
                EHOP_Fees = "$0.0",
                TotalAmount = Convert.ToDecimal(333.30),
                Payment_Transaction_ID = "Test-Transaction",
                Payment_Transaction_Date = "01/20/2016",
                CH_Self_Certificate_Signature = "TEST",
                CH_Self_Certificate_Date = Convert.ToDateTime("2016-01-20"),
                CH_Self_Certificate_Type = "Owner",
                Certificate_of_Occupancy_Number = "AA",
                //CO_Issue_Date_ = Convert.ToDateTime("2015-12-30"),
                EHOP_Attested_By = "",
                First_Name = "FNAME",
                Middle_Name = "MNAME",
                Last_Name = "LNAME",
                Organization_Name = "BNAME",
                Full_Address_Line_1 = "AD1",
                Address_Line_2 = "AD2",
                City = "CITY",
                State = "Georgia",
                Zip_Code = "11111",
                Country_Region = "United States",
                Email = "D@G.IN",
                Business_Organization___ = "GENERAL PARTNERSHIP",
                Trade_Name__If_applicable_ = "",
                Tax_ID_Number = "111-11-1111",
                FEIN_SSN = "SSN",
                Prim_Street_Number = "1100",
                Prim_Street_Name = "13TH",
                StreetType = "Street",
                Quadrant = "NW",
                Unit_Type___ = "BLDG",
                Suite_Unit_Number = "20",
                PCity = "WASHINGTON",
                PState = "District of Columbia",
                PZip = "20005",
                Phone = "0123456789",
                Premise_Address_Verified = "True",
                Parcel_Premise_Suffix = "1/2",
                Parcel_Premise_Ward = "Ward 2",
                Parcel_Premise_ANC = "ANC 2F",
                Billing_Org_Buss__Name = "BNAME",
                Billing_ContactFirstName = "BNAME",
                Billing_ContactMiddleName = "MNAME",
                Billing_ContactLastName = "LNAME",
                Billing_Address_1 = "1100",
                Billing_Address_2_ = "13TH",
                Billing_Address_3 = "Street",
            
                Billing_City = "WASHINGTON",
                Billing_State = "District of Columbia",
                Billing_Country___ = "United States",
                Billing_Zip = "20005",
                Billing_Phone = "",
                Billing_Email = "",
                Agent_Org_Buss__Name = "BUSINESSNAME",
                Contact_First_Name = "FIRSTNAME",
                Contact_Middle_Name = "MIDDLENAME",
                Contact_Last_Name = "LASTNAMW",
                Street_Address = "STADDRESS",
                Address_Line__2_ =  "AD2",
               
               ContactAgent_City = "WASHINGTON",
                ContactAgent_State = "District of Columbia",
                ContactAgent_Country = "United States",
                ContactAgent_ZipCode = "44444",
                Corp_Telephone = "4567891230",
                ContactAgent_Email = "S@G.IN",
                FileNumber = "C262873",
                Created_Date = System.DateTime.Now,
                UpDated_Date = System.DateTime.Now
            });




        }
    }
}
