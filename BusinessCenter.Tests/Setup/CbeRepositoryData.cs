using System;
using System.Collections.Generic;
using BusinessCenter.Data;

namespace BusinessCenter.Tests.Setup
{
    public class CbeRepositoryData
    {
        private readonly List<DCBC_ENTITY_CBE> _entities;
        public bool IsInitialized;

        public void AddCbeEntity(DCBC_ENTITY_CBE obj)
        {
            _entities.Add(obj);
        }

        public List<DCBC_ENTITY_CBE> CbeEntitiesList
        {
            get { return _entities; }
        }

        public CbeRepositoryData()
        {
            IsInitialized = true;
            _entities = new List<DCBC_ENTITY_CBE>();

            AddCbeEntity(new DCBC_ENTITY_CBE()
            {
                DCBC_ENTITY_ID = 30000001,
                REC_STATUS = true,
                LAST_UPDATE=System.DateTime.Now,
                DCBC_ENTITY_SOURCE = "CBE",
                LsdbeID=4032,
                CompanyID=4174,
                BusinessName = "Jones Electric Company, Inc.",
                BusinessAddress = "2615 30TH STREET NE, WASHINGTON, DC 20018",
                BusinessAddressShort="2615 30TH STREET NE",
                BusinessPhone="2022437320",
                BusinessFax = "2025470159",
                BusinessEmail = "eliu@urbanssc.com",
                GIS_Ward = "4",
                CertificationExpiry = Convert.ToDateTime("2017-01-26 00:00:00.000"),
                NIGPAppID = 22196,
                TradeDivAppID = 22196,
                BAPAppID = 22196,
                BusinessStructure = "Corporation",
                IncorporationDate = Convert.ToDateTime("1986-05-13 00:00:00.000"),
                BusinessDescription = "",
                BusinessContact = "Angel Almaraz",
                LSDBE_Number = "LSD65957012017",
                RefPoints = 7,
                RefPointsDesc = "2 for LBE; 3 for SBE; 2 for DBE",
                BusinessAddress1 = "7706 GEORGIA AVENUE NW",
                BusinessAddress2 = "SUITE 4",
                BusinessCity = "WASHINGTON",
                BusinessState = "DC",
                BusinessZip1 = "2012",
                BusinessZip2 = "",
                LSDBEBusinessOptions = "LBE, SBE, DBE",
                BusinessWebsite = "www.nppcontractors.com",
                Lsdbe_PersonAppID = 22196,
                MailingAddress = "7706 GEORGIA AVENUE NW  SUITE 4 ",
                PublicContact = "Angel Almaraz"
            });

            AddCbeEntity(new DCBC_ENTITY_CBE()
            {
                DCBC_ENTITY_ID = 30000000,
                REC_STATUS = true,
                LAST_UPDATE = System.DateTime.Now,
                DCBC_ENTITY_SOURCE = "CBE",
                LsdbeID = 4031,
                CompanyID = 4173,
                BusinessName = "Kalos Construction Company, Inc.",
                BusinessAddress = "325 VINE STREET NW, WASHINGTON, DC 20012",
                BusinessAddressShort = "325 VINE STREET NW",
                BusinessPhone = "2022918780",
                BusinessFax = "2022918879",
                BusinessEmail = "tbranson@kalosconstruction.com",
                GIS_Ward = "4",
                CertificationExpiry = Convert.ToDateTime("2017-01-26 00:00:00.000"),
                NIGPAppID = 20005,
                TradeDivAppID = 20005,
                BAPAppID = 20005,
                BusinessStructure = "Corporation",
                IncorporationDate = Convert.ToDateTime("1985-10-01 00:00:00.000"),
                BusinessDescription = "",
                BusinessContact = "Taylor Branson",
                LSDBE_Number = "LS15059072015",
                RefPoints = 5,
                RefPointsDesc = "2 for LBE; 3 for SBE",
                BusinessAddress1 = "325 VINE STREET NW",
                BusinessAddress2 = "SUITE 4",
                BusinessCity = "WASHINGTON",
                BusinessState = "DC",
                BusinessZip1 = "2012",
                BusinessZip2 = "",
                LSDBEBusinessOptions = "LBE, SBE",
                BusinessWebsite = "",
                Lsdbe_PersonAppID = 20005,
                MailingAddress = "325 VINE STREET NW  ",
                PublicContact = "Peter T. Branson"
            });
        }
    }
}