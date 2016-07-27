using BusinessCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Setup
{
    public class DCBC_ENTITY_Cof_ORepositoryData
    {
        private readonly List<DCBC_ENTITY_Cof_O> _entities;
        public bool IsInitialized;

        public void AddDCBCCofoEntity(DCBC_ENTITY_Cof_O obj)
        {
            _entities.Add(obj);
        }

        public List<DCBC_ENTITY_Cof_O> DCBCCofoEntitiesList
        {
            get { return _entities; }
        }


        public DCBC_ENTITY_Cof_ORepositoryData()
        {
            IsInitialized = true;
            _entities = new List<DCBC_ENTITY_Cof_O>();

            AddDCBCCofoEntity(new DCBC_ENTITY_Cof_O()
            {
                As_of_Date =Convert.ToDateTime("2015-09-25") ,
                B1_APP_TYPE_ALIAS = "Certificate of Occupancy Permit",
                b1_Alt_ID = "CO1501622",
                B1_APPL_STATUS = "Open",

                DCBC_Status = "Valid",
                B1_APPL_STATUS_DATE = DateTime.Now,
                B1_HSE_NBR_START = 600,
                B1_HSE_NBR_END = null,
                B1_HSE_FRAC_NBR_START = null,
                B1_UNIT_START = null,
                B1_STR_NAME = "MARYLAND",
                B1_STR_SUFFIX = "AVE",
                B1_STR_SUFFIX_DIR = "SW",
                B1_SITUS_CITY = "WASHINGTON",
                B1_SITUS_STATE = "DC",
                B1_SITUS_ZIP = "20002-4816",
                B1_SITUS_NBRHD = null,
                Zoning_Use = null,
                Medical_Marijuana_Dispensary =null,
                SD_APP_DES = null,
                Status_DATE = DateTime.Now
            });
            AddDCBCCofoEntity(new DCBC_ENTITY_Cof_O()
            {
                As_of_Date = Convert.ToDateTime("2015-09-25"),
                B1_APP_TYPE_ALIAS = "Home Occupation Permit",
                b1_Alt_ID = "HO0800065",
                B1_APPL_STATUS = "Open",

                DCBC_Status = "Valid",
                B1_APPL_STATUS_DATE = DateTime.Now,
                B1_HSE_NBR_START = 600,
                B1_HSE_NBR_END = null,
                B1_HSE_FRAC_NBR_START = null,
                B1_UNIT_START = null,
                B1_STR_NAME = "MARYLAND",
                B1_STR_SUFFIX = "AL",
                B1_STR_SUFFIX_DIR = "SW",
                B1_SITUS_CITY = "WASHINGTON",
                B1_SITUS_STATE = "DC",
                B1_SITUS_ZIP = "20002-4816",
                B1_SITUS_NBRHD = null,
                Zoning_Use = null,
                Medical_Marijuana_Dispensary = null,
                SD_APP_DES = null,
                Status_DATE = DateTime.Now
            });
            //AddDCBCCofoEntity(new DCBC_ENTITY_Cof_O()
            //{
            //    As_of_Date = Convert.ToDateTime("2015-09-25"),
            //    B1_APP_TYPE_ALIAS = "Home Occupation Permit",
            //    b1_Alt_ID = "HO0800066",
            //    B1_APPL_STATUS = "Open",

            //    DCBC_Status = "Valid",
            //    B1_APPL_STATUS_DATE = DateTime.Now,
            //    B1_HSE_NBR_START = 600,
            //    B1_HSE_NBR_END = null,
            //    B1_HSE_FRAC_NBR_START = null,
            //    B1_UNIT_START = null,
            //    B1_STR_NAME = "MARYLAND",
            //    B1_STR_SUFFIX = "AL",
            //    B1_STR_SUFFIX_DIR = "SW",
            //    B1_SITUS_CITY = "WASHINGTON",
            //    B1_SITUS_STATE = "DC",
            //    B1_SITUS_ZIP = "20002-4816",
            //    B1_SITUS_NBRHD = null,
            //    Zoning_Use = null,
            //    Medical_Marijuana_Dispensary = null,
            //    SD_APP_DES = null,
            //    Status_DATE = DateTime.Now
            //});

          
        }
    }
}
