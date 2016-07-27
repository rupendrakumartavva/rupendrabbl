using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data;

namespace BusinessCenter.Tests.Setup
{
   public class CorpRepositoryData
    {
        private readonly List<DCBC_ENTITY_CORP> _entities;
        public bool IsInitialized;
          public void AddCorpEntity(DCBC_ENTITY_CORP obj)
        {
            _entities.Add(obj);
        }

        public List<DCBC_ENTITY_CORP> CorpEntitiesList
        {
            get { return _entities; }
        }

        public CorpRepositoryData()
        {
            IsInitialized = true;
            _entities = new List<DCBC_ENTITY_CORP>();

            AddCorpEntity(new DCBC_ENTITY_CORP()
            {
                DCBC_ENTITY_ID=40000000,
                REC_STATUS=Convert.ToBoolean("True"),
                LAST_UPDATE = Convert.ToDateTime("2015-07-24 00:00:00.000"),
                DCBC_ENTITY_SOURCE="Corp",
                BusinessName="2118 14TH STREET",
                Suffix=null,
                Locale = "Domestic",
                ModelType = "Corporation (Non-Profit)",
                EntityStatus = "Revoked",
                EntityStatusDate =Convert.ToDateTime("2012-09-04 00:00:00.000"),
                BusniessAddressLine1 = "3501 14TH STREET NW",
                BusniessAddressLine2=null,
                BusniessAddressLine3=null,
                BusniessAddressLine4=null,
                BusinessCity = "WASHINGTON",
                BusinessState = "DC",
                ZipCode = "20010",
                EffectiveDate=Convert.ToDateTime("2001-08-27 00:00:00.000"),
                NextReportYear="2016",
                FileNumber="C212821"
            });
            AddCorpEntity(new DCBC_ENTITY_CORP()
            {
                DCBC_ENTITY_ID = 40000001,
                REC_STATUS = Convert.ToBoolean("True"),
                LAST_UPDATE = Convert.ToDateTime("2015-07-24 00:00:00.000"),
                DCBC_ENTITY_SOURCE = "Corp",
                BusinessName = "A ABLE ACCIDENT ADVOCATE",
                Suffix = null,
                Locale = "Domestic",
                ModelType = "Corporation (Non-Profit)",
                EntityStatus = "Active",
                EntityStatusDate =Convert.ToDateTime("2012-08-20 00:00:00.000"),
                BusniessAddressLine1 = "5225 WISCONSIN AVENUE NW",
                BusniessAddressLine2 = "#504",
                BusniessAddressLine3 = null,
                BusniessAddressLine4 = null,
                BusinessCity = "WASHINGTON",
                BusinessState = "DC",
                ZipCode = "20015",
                EffectiveDate =Convert.ToDateTime("2000-01-24 00:00:00.000"),
                NextReportYear = "2017",
                FileNumber = "C943266"
            });

            AddCorpEntity(new DCBC_ENTITY_CORP()
            {
                DCBC_ENTITY_ID = 40000002,
                REC_STATUS = Convert.ToBoolean("True"),
                LAST_UPDATE = Convert.ToDateTime("2015-07-24 00:00:00.000"),
                DCBC_ENTITY_SOURCE = "Corp",
                BusinessName = "DESO & BUCKLEY,",
                Suffix = null,
                Locale = "Domestic",
                ModelType = "Corporation (Non-Profit)",
                EntityStatus = "Revoked",
                EntityStatusDate =Convert.ToDateTime("2012-08-20 00:00:00.000"),
                BusniessAddressLine1 = "5225 WISCONSIN AVENUE NW",
                BusniessAddressLine2 = "#504",
                BusniessAddressLine3 = null,
                BusniessAddressLine4 = null,
                BusinessCity = "WASHINGTON",
                BusinessState = "DC",
                ZipCode = "20015",
                EffectiveDate = Convert.ToDateTime("2000-01-24 00:00:00.000"),
                NextReportYear = "2017",
                FileNumber = "C880040"
            });
           
        }
    }
}
