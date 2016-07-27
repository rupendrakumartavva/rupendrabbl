using BusinessCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Setup
{
    public class RenewalView3RepositoryData
    {
        private readonly List<BblLicenseView3> _entities;
        public bool IsInitialized;

        public void AddBblLicenseView3Entity(BblLicenseView3 obj)
        {
            _entities.Add(obj);
        }

        public List<BblLicenseView3> BblLicenseView3EntitiesList
        {
            get { return _entities; }
        }


        public RenewalView3RepositoryData()
        {
            IsInitialized = true;
            _entities = new List<BblLicenseView3>();

            AddBblLicenseView3Entity(new BblLicenseView3()
            {
                Application_Unique_ID = "2673d7fb-9621-4c14-a896-8c9b273a54cd",
                License_Number = "400312000101",
                Renewal_License_No_ = "LREN13013234",
                Primary_Category = "General Business Licenses",
                License_Renewal_Period = 2,
                ApplicationFee =Convert.ToDecimal(70.00),
                Endorsement_Fee =Convert.ToDecimal( 25.00),
                LicenseFee =Convert.ToDecimal(200.00),
                ESFFee =Convert.ToDecimal(29.50),
                Penalty_Fee__Lapse_ =Convert.ToDecimal(250.00),
                Penalty_Fee__Expired_ =Convert.ToDecimal(250.00),
                TotalAmount =Convert.ToDecimal(1324.50),
                Payment_Transaction_ID = "Test-Transaction",
                Transaction_Payment_Date = System.DateTime.Now,
                Created_Date = System.DateTime.Now,
                UpDated_Date = System.DateTime.Now
            });
        

        }

    }
}
