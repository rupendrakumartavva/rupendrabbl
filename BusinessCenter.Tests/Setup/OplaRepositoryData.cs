using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data;

namespace BusinessCenter.Tests.Setup
{
   public class OplaRepositoryData
    {
        private readonly List<DCBC_ENTITY_OPLA> _entities;
        public bool IsInitialized;
        public void AddCorpEntity(DCBC_ENTITY_OPLA obj)
        {
            _entities.Add(obj);
        }
         public List<DCBC_ENTITY_OPLA> OplaEntitiesList
        {
            get { return _entities; }
        }

         public OplaRepositoryData()
        {
            IsInitialized = true;
            _entities = new List<DCBC_ENTITY_OPLA>();

            AddCorpEntity(new DCBC_ENTITY_OPLA()
            {
                DCBC_ENTITY_ID = 50000000,
                REC_STATUS = Convert.ToBoolean("True"),
                LAST_UPDATE = Convert.ToDateTime("2015-07-24 00:00:00.000"),
                DCBC_ENTITY_SOURCE = "OPLA",
                BOARD_ABBREVIATION = "AA",
                LICENSE_PREFIX = "AA",
                LICENSE_NUMBER = "10",
                LICENSE_ISSUE_DATE = Convert.ToDateTime("2014-06-01"),
                LICENSE_EXPIRATION_DATE = Convert.ToDateTime("2016-05-31"),
                LICENSE_STATUS_CODE = "A",
                Licensee_Name_1 = "DAVID",
                Licensee_Name_2 = "BENJAMIN",
                Licensee_Name_3 = "FALK",
                Licensee_Name_4="",
                BUSINESS_ADDRESS = "FAME, LLC,5335 WISCONSIN AVENUE,SUITE 960",
                BUSINESS_CITY = "WASHINGTON",
                BUSINESS_STATE = "DC",
                BUSINESS_ZIP = "20015",
                BUSINESS_PHONE = "202-686-3263",
                HOME_ADDRESS="",
                HOME_CITY="",
                HOME_STATE="",
                HOME_ZIP="",
                HOME_PHONE="",
                Preferred_Address_Ind = "B",
                EMPLOYER_LICENSE_PREFIX1="",
                EMPLOYER_LICENSE_NUMBER="",
                Organization_EMPLOYER_NAME="",
                EMPLOYER_LICENSE_ISSUE_DATE = null,
                EMPLOYER_LICENSE_EXPIRATION_DATE=null,
                EMPLOYER_LICENSE_STATUS_CODE="",
                EMPLOYER_ADDRESS="",
                EMPLOYER_CITY="",
                EMPLOYER_STATE="",
                EMPLOYER_ZIP="",
                EMPLOYER_PHONE="",
                FULL_NAME = "DAVID BENJAMIN FALK",
                OPLA_LICENSE_CATEGORY = "Individual_Employer",
                OPLA_SOURCE = "ARM",
                REC_DATE = Convert.ToDateTime("2015-07-24 23:15:35.057"),
                License_Board = "ATHLETE AGENT",
                License_Type = "Athlete Agent"

            });
            AddCorpEntity(new DCBC_ENTITY_OPLA()
            {
                DCBC_ENTITY_ID = 50000009   ,
                REC_STATUS = Convert.ToBoolean("True"),
                LAST_UPDATE = Convert.ToDateTime("2015-07-24 00:00:00.000"),
                DCBC_ENTITY_SOURCE = "OPLA",
                BOARD_ABBREVIATION = "AC",
                LICENSE_PREFIX = "CPA",
                LICENSE_NUMBER = "1174",
                LICENSE_ISSUE_DATE = Convert.ToDateTime("2015-01-01"),
                LICENSE_EXPIRATION_DATE = Convert.ToDateTime("2016-12-31"),
                LICENSE_STATUS_CODE = "A",
                Licensee_Name_1 = "JERRY",
                Licensee_Name_2 = "",
                Licensee_Name_3 = "BURKE",
                Licensee_Name_4 = "",
                BUSINESS_ADDRESS = "APT. 505,3005 S LEISURE WORLD BLVD",
                BUSINESS_CITY = "SILVER SPRING",
                BUSINESS_STATE = "MD",
                BUSINESS_ZIP = "20906",
                BUSINESS_PHONE = "(301)984-7000",
                HOME_ADDRESS = "APT. 505,3005 S LEISURE WORLD BLVD",
                HOME_CITY = "SILVER SPRING",
                HOME_STATE = "MD",
                HOME_ZIP = "20906",
                HOME_PHONE = "(301)984-7000",
                Preferred_Address_Ind = "H",
                EMPLOYER_LICENSE_PREFIX1 = "",
                EMPLOYER_LICENSE_NUMBER = "",
                Organization_EMPLOYER_NAME = "",
                EMPLOYER_LICENSE_ISSUE_DATE = null,
                EMPLOYER_LICENSE_EXPIRATION_DATE = null,
                EMPLOYER_LICENSE_STATUS_CODE = "",
                EMPLOYER_ADDRESS = "",
                EMPLOYER_CITY = "",
                EMPLOYER_STATE = "",
                EMPLOYER_ZIP = "",
                EMPLOYER_PHONE = "",
                FULL_NAME = "JERRY BURKE ",
                OPLA_LICENSE_CATEGORY = "Individual_Employer",
                OPLA_SOURCE = "PULSE",
                REC_DATE = Convert.ToDateTime("2015-07-24 23:15:35.057"),
                License_Board = "ACCOUNTANCY",
                License_Type = "Certified Public Accountant"
            });
        }
    }
}
