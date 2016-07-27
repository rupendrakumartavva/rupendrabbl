using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data;

namespace BusinessCenter.Tests.Setup
{
    public class AbraRepositoryData
    {
        private readonly List<DCBC_ENTITY_ABRA> _entities;
        public bool IsInitialized;

        public void AddAbraEntity(DCBC_ENTITY_ABRA obj)
        {
            _entities.Add(obj);
        }

        public List<DCBC_ENTITY_ABRA> AbraEntitiesList
        {
            get { return _entities; }
        }

        public AbraRepositoryData()
        {
            IsInitialized = true;
            _entities = new List<DCBC_ENTITY_ABRA>();

            AddAbraEntity(new DCBC_ENTITY_ABRA()
            {
                DCBC_ENTITY_ID = 20007555,
                REC_STATUS=true,
                LAST_UPDATE = DateTime.Now,
                DCBC_ENTITY_SOURCE = "ABRA",
                Serv_Prov_Code = "ABRA",
                B1_PER_GROUP = "Licenses",
                B1_PER_TYPE = "Alcohol License",
                B1_PER_CATEGORY="NA",
                B1_APP_TYPE_ALIAS = "Licenses/Alcohol License/Manager/NA",
                B1_SPECIAL_TEXT = "Purple Patch",
                License_Number = "ABRA-098066",
                B1_TRACKING_NBR = 62474957669,
                License_Status = "Active",
                B1_CHECKBOX_TYPE = "LICENSE INFORMATION",
                License_Expiration_Date = "3/31/2016",
                Valid_Thru_Date = "03/31/2014",
                Posting_Date = "4/5/2013",
                Class_Type = "C",
                Establishment_Type = "Restaurant",
                Applicant_FNAME = "Meagan",
                Applicant_MNAME = "A",
                Applicant_LNAME = "Mann",
                Applicant_FULL_NAME = null,
                Applicant_RELATION = null,
                Applicant_BUSINESS_NAME = null,
                Solicitor_FNAME = null,
                Solicitor_MNAME = null,
                Solicitor_LNAME = null,
                Solicitor_FULL_NAME = null,
                Solicitor_RELATION = null,
                Solicitor_BUSINESS_NAME = null,
                Solicitor_ADDR1 = null,
                Solicitor_ADDR2 = null,
                Solicitor_ADDR3 = null,
                Solicitor_CITY = null,
                Solicitor_STATE = null,
                Solicitor_ZIP = null,
                Manager_FNAME = null,
                Manager_MNAME = null,
                Manager_LNAME = null,
                Manager_FULL_NAME = null,
                Manager_RELATION = null,
                Manager_BUSINESS_NAME = null,
                Manager_ADDR1 = null,
                Manager_ADDR2 = null,
                Manager_ADDR3 = null,
                Manager_CITY = null,
                Manager_STATE = null,
                Manager_ZIP = null,
                B1_PRIMARY_ADDR_FLG = null,
                B1_HSE_NBR_START = null,
                B1_HSE_NBR_END = null,
                B1_HSE_FRAC_NBR_START = null,
                B1_HSE_FRAC_NBR_END = null,
                B1_UNIT_START = null,
                B1_STR_NAME = null,
                B1_STR_SUFFIX = null,
                B1_STR_SUFFIX_DIR = null,
                B1_SITUS_CITY = null,
                B1_SITUS_STATE = null,
                B1_SITUS_ZIP = null
            });
            AddAbraEntity(new DCBC_ENTITY_ABRA()
            {
                DCBC_ENTITY_ID = 20006725,
                REC_STATUS = true,
                LAST_UPDATE = DateTime.Now,
                DCBC_ENTITY_SOURCE = "ABRA",
                Serv_Prov_Code = "ABRA",
                B1_PER_GROUP = "Licenses",
                B1_PER_TYPE = "Alcohol License",
                B1_PER_CATEGORY = "NA",
                B1_APP_TYPE_ALIAS = "Licenses/Alcohol License/Manager/NA",
                B1_SPECIAL_TEXT = "Purple Patch",
                License_Number = "ABRA-098066",
                B1_TRACKING_NBR = 62474957669,
                License_Status = "Active",
                B1_CHECKBOX_TYPE = "LICENSE INFORMATION",
                License_Expiration_Date = "3/31/2016",
                Valid_Thru_Date = "03/31/2014",
                Posting_Date = "4/5/2013",
                Class_Type = "C",
                Establishment_Type = "Restaurant",
                Applicant_FNAME = "Meagan",
                Applicant_MNAME = "A",
                Applicant_LNAME = "Mann",
                Applicant_FULL_NAME = null,
                Applicant_RELATION = null,
                Applicant_BUSINESS_NAME = null,
                Solicitor_FNAME = null,
                Solicitor_MNAME = null,
                Solicitor_LNAME = null,
                Solicitor_FULL_NAME = null,
                Solicitor_RELATION = null,
                Solicitor_BUSINESS_NAME = null,
                Solicitor_ADDR1 = null,
                Solicitor_ADDR2 = null,
                Solicitor_ADDR3 = null,
                Solicitor_CITY = null,
                Solicitor_STATE = null,
                Solicitor_ZIP = null,
                Manager_FNAME = null,
                Manager_MNAME = null,
                Manager_LNAME = null,
                Manager_FULL_NAME = null,
                Manager_RELATION = null,
                Manager_BUSINESS_NAME = null,
                Manager_ADDR1 = null,
                Manager_ADDR2 = null,
                Manager_ADDR3 = null,
                Manager_CITY = null,
                Manager_STATE = null,
                Manager_ZIP = null,
                B1_PRIMARY_ADDR_FLG = null,
                B1_HSE_NBR_START = null,
                B1_HSE_NBR_END = null,
                B1_HSE_FRAC_NBR_START = null,
                B1_HSE_FRAC_NBR_END = null,
                B1_UNIT_START = null,
                B1_STR_NAME = null,
                B1_STR_SUFFIX = null,
                B1_STR_SUFFIX_DIR = null,
                B1_SITUS_CITY = null,
                B1_SITUS_STATE = null,
                B1_SITUS_ZIP = null
            });
        }
    }
}
