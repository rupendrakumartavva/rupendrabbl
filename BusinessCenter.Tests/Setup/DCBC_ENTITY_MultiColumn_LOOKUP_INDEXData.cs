using BusinessCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Setup
{
   public class DCBC_ENTITY_MultiColumn_LOOKUP_INDEXData
    {

       private readonly List<DCBC_ENTITY_MultiColumn_LOOKUP_INDEX> _entities;
        public bool IsInitialized;

        public void AddLookUpEntity(DCBC_ENTITY_MultiColumn_LOOKUP_INDEX obj)
        {
            _entities.Add(obj);
        }

        public List<DCBC_ENTITY_MultiColumn_LOOKUP_INDEX> LookupEntitiesList
        {
            get { return _entities; }
        }

        public DCBC_ENTITY_MultiColumn_LOOKUP_INDEXData()
        {
            IsInitialized = true;
            _entities = new List<DCBC_ENTITY_MultiColumn_LOOKUP_INDEX>();

            AddLookUpEntity(new DCBC_ENTITY_MultiColumn_LOOKUP_INDEX()
            {
                DATA_SOURCE = "ABRA",
                DCBC_ENTITY_ID = 1,
                LicenseNumberOrig="ABRA-098066",
                LicenseNumberLookup="ABRA-098066",
                CompanyNameOrig="Purple Patch",
                CompanyNameLookup="PurplePatch",
                FirstNameOrig="Purple",
                FirstNameLookup="Purple",
                LastNameOrig="Patch",
                LastNameLookup="Patch"
            });
            AddLookUpEntity(new DCBC_ENTITY_MultiColumn_LOOKUP_INDEX()
            {
               DATA_SOURCE = "ABRA",
                DCBC_ENTITY_ID = 2,
                LicenseNumberOrig="ABRA-098066",
                LicenseNumberLookup="ABRA098066",
                CompanyNameOrig="Purple Patch",
                CompanyNameLookup="PurplePatch",
                FirstNameOrig="Purple",
                FirstNameLookup="Purple",
                LastNameOrig="Patch",
                LastNameLookup="Patch"
            });
            AddLookUpEntity(new DCBC_ENTITY_MultiColumn_LOOKUP_INDEX()
            {
                 DATA_SOURCE = "BBL",
                DCBC_ENTITY_ID = 1,
                LicenseNumberOrig="931315000136",
                LicenseNumberLookup="931315000136",
                CompanyNameOrig="IMA PIZZA STORE 13 LLC.",
                CompanyNameLookup="IMAPIZZASTORE13LLC",
                FirstNameOrig="NA",
                FirstNameLookup="NA",
                LastNameOrig="NA",
                LastNameLookup="NA"
            });
            AddLookUpEntity(new DCBC_ENTITY_MultiColumn_LOOKUP_INDEX()
            {
                 DATA_SOURCE = "BBL",
                DCBC_ENTITY_ID = 2,
                LicenseNumberOrig="931315000139",
                LicenseNumberLookup="931315000139",
                CompanyNameOrig="1723 DUPONT L.L.C.",
                CompanyNameLookup="1723DUPONTLLC.",
                FirstNameOrig="NA",
                FirstNameLookup="NA",
                LastNameOrig="NA",
                LastNameLookup="NA"
            });
            AddLookUpEntity(new DCBC_ENTITY_MultiColumn_LOOKUP_INDEX()
            {
                 DATA_SOURCE = "BBL",
                DCBC_ENTITY_ID = 3,
                LicenseNumberOrig="100113000002",
                LicenseNumberLookup="100113000002",
                CompanyNameOrig="1723 DUPONT L.L.C.",
                CompanyNameLookup="1723DUPONTLLC.",
                FirstNameOrig="NA",
                FirstNameLookup="NA",
                LastNameOrig="NA",
                LastNameLookup="NA"
            });
             AddLookUpEntity(new DCBC_ENTITY_MultiColumn_LOOKUP_INDEX()
            {
                 DATA_SOURCE = "BBL",
                DCBC_ENTITY_ID = 4,
                LicenseNumberOrig="100113000003",
                LicenseNumberLookup="100113000003",
                CompanyNameOrig="1723 DUPONT L.L.C.",
                CompanyNameLookup="1723DUPONTLLC.",
                FirstNameOrig="NA",
                FirstNameLookup="NA",
                LastNameOrig="NA",
                LastNameLookup="NA"
            });
            AddLookUpEntity(new DCBC_ENTITY_MultiColumn_LOOKUP_INDEX()
            {
                 DATA_SOURCE = "BBL",
                DCBC_ENTITY_ID = 5,
                LicenseNumberOrig="100113000005",
                LicenseNumberLookup="100113000005",
                CompanyNameOrig="1723 DUPONT L.L.C.",
                CompanyNameLookup="1723DUPONTLLC.",
                FirstNameOrig="NA",
                FirstNameLookup="NA",
                LastNameOrig="NA",
                LastNameLookup="NA"
            });
            AddLookUpEntity(new DCBC_ENTITY_MultiColumn_LOOKUP_INDEX()
            {
                 DATA_SOURCE = "BBL",
                DCBC_ENTITY_ID = 6,
                LicenseNumberOrig="931313000266",
                LicenseNumberLookup="100113000003",
                CompanyNameOrig="RETROSPECT COFFE AND TEA LLC",
                CompanyNameLookup="RETROSPECTCOFFEANDTEALLC",
                FirstNameOrig="NA",
                FirstNameLookup="NA",
                LastNameOrig="NA",
                LastNameLookup="NA"
            });

            AddLookUpEntity(new DCBC_ENTITY_MultiColumn_LOOKUP_INDEX()
            {
                 DATA_SOURCE = "CBE",
                DCBC_ENTITY_ID = 1,
                LicenseNumberOrig="LSD65957012017",
                LicenseNumberLookup="LSD65957012017",
                CompanyNameOrig="Jones Electric Company, Inc.",
                CompanyNameLookup="JONES ELECTRIC COMPANY, INC..",
                FirstNameOrig="Angel Almaraz",
                FirstNameLookup="AngelAlmaraz",
                LastNameOrig="Angel Almaraz",
                LastNameLookup="AngelAlmaraz"
            });
            AddLookUpEntity(new DCBC_ENTITY_MultiColumn_LOOKUP_INDEX()
            {
                 DATA_SOURCE = "CBE",
                DCBC_ENTITY_ID = 2,
                LicenseNumberOrig="LS15059072015",
                LicenseNumberLookup="LS15059072015",
                CompanyNameOrig="RETROSPECT COFFE AND TEA LLC",
                CompanyNameLookup="RETROSPECTCOFFEANDTEALLC",
                FirstNameOrig="Taylor Branson",
                FirstNameLookup="TaylorBranson",
                LastNameOrig="Taylor Branson",
                LastNameLookup="TaylorBranson"
            });

             AddLookUpEntity(new DCBC_ENTITY_MultiColumn_LOOKUP_INDEX()
            {
                 DATA_SOURCE = "CORP",
                DCBC_ENTITY_ID = 1,
                LicenseNumberOrig="C212821",
                LicenseNumberLookup="C212821",
                CompanyNameOrig="2118 14TH STREET l",
                CompanyNameLookup="2118 14TH STREET l",
                FirstNameOrig="",
                FirstNameLookup="",
                LastNameOrig="",
                LastNameLookup=""
            });
            AddLookUpEntity(new DCBC_ENTITY_MultiColumn_LOOKUP_INDEX()
            {
                 DATA_SOURCE = "CORP",
                DCBC_ENTITY_ID = 2,
                LicenseNumberOrig="C943266",
                LicenseNumberLookup="C943266",
                CompanyNameOrig="A ABLE ACCIDENT ADVOCATE l ",
                CompanyNameLookup="A ABLE ACCIDENT ADVOCATE l",
                FirstNameOrig="Taylor Branson",
                FirstNameLookup="TaylorBranson",
                LastNameOrig="Taylor Branson",
                LastNameLookup="TaylorBranson"
            });
              AddLookUpEntity(new DCBC_ENTITY_MultiColumn_LOOKUP_INDEX()
            {
                 DATA_SOURCE = "CORP",
                DCBC_ENTITY_ID = 3,
                LicenseNumberOrig="C880040",
                LicenseNumberLookup="C880040",
                CompanyNameOrig="DESO & BUCKLEY l,",
                CompanyNameLookup="DESO  BUCKLEY l",
                FirstNameOrig="Taylor Branson",
                FirstNameLookup="TaylorBranson",
                LastNameOrig="Taylor Branson",
                LastNameLookup="TaylorBranson"
            });

             AddLookUpEntity(new DCBC_ENTITY_MultiColumn_LOOKUP_INDEX()
            {
                 DATA_SOURCE = "OPLA",
                DCBC_ENTITY_ID = 1,
                LicenseNumberOrig="AA10",
                LicenseNumberLookup="AA10",
                CompanyNameOrig="l",
                CompanyNameLookup="l",
                FirstNameOrig="DAVID",
                FirstNameLookup="DAVID",
                LastNameOrig="FALK",
                LastNameLookup="FALK"
            });
              AddLookUpEntity(new DCBC_ENTITY_MultiColumn_LOOKUP_INDEX()
            {
                 DATA_SOURCE = "OPLA",
                DCBC_ENTITY_ID = 2,
                LicenseNumberOrig="CPA1174",
                LicenseNumberLookup="CPA1174",
                CompanyNameOrig="l",
                CompanyNameLookup="l",
                FirstNameOrig="JERRY",
                FirstNameLookup="JERRY",
                LastNameOrig="BURKE",
                LastNameLookup="BURKE"
            });
        }
    }
}
