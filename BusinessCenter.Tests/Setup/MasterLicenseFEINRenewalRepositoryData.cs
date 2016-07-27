using BusinessCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Setup
{
   public class MasterLicenseFEINRenewalRepositoryData
    {
         private readonly List<MasterLicense_Renewal_TaxRevenue> _entities;
        public bool IsInitialized;

        public void AddMasterLicenseEntity(MasterLicense_Renewal_TaxRevenue obj)
        {
            _entities.Add(obj);
        }

        public List<MasterLicense_Renewal_TaxRevenue>MasterLicenseEntitiesList
        {
            get { return _entities; }
        }


        public MasterLicenseFEINRenewalRepositoryData()
        {
            IsInitialized = true;
            _entities = new List<MasterLicense_Renewal_TaxRevenue>();

            AddMasterLicenseEntity(new MasterLicense_Renewal_TaxRevenue()
            {
                MasterLicenseId = 1,
                TextFEINNumber = "11-1111111",
                LicenseNumber = "100112000001"
            });
            AddMasterLicenseEntity(new MasterLicense_Renewal_TaxRevenue()
            {
                MasterLicenseId = 2,
                TextFEINNumber = "22-1234567",
                LicenseNumber = "931315000136"
            });
        }
    }
}
