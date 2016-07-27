using System;
using System.Collections.Generic;
using BusinessCenter.Data;

namespace BusinessCenter.Tests.Setup
{
    public class SearchMultiColumnLookUpIndexData
    {
        private readonly List<DCBC_ENTITY_MultiColumn_LOOKUP_INDEX> _entities;
        public bool IsInitialized;

        public void AddMultiColumnLookupIndexEntity(DCBC_ENTITY_MultiColumn_LOOKUP_INDEX obj)
        {
            _entities.Add(obj);
        }

        public List<DCBC_ENTITY_MultiColumn_LOOKUP_INDEX> MultiColumnLookupIndexEntitiesList
        {
            get { return _entities; }
        }

        public SearchMultiColumnLookUpIndexData()
        {
            IsInitialized = true;
            _entities = new List<DCBC_ENTITY_MultiColumn_LOOKUP_INDEX>();

            AddMultiColumnLookupIndexEntity(new DCBC_ENTITY_MultiColumn_LOOKUP_INDEX()
            {
                DATA_SOURCE = "ABRA",
                DCBC_ENTITY_ID = 20007555,
                LicenseNumberOrig = "ABRA-099577",
                LicenseNumberLookup = "ABRA099577",
                CompanyNameOrig = "MICHAEL D. BALDERAS",
                CompanyNameLookup = "MICHAEL D BALDERAS",
                FirstNameOrig = "MICHAEL",
                FirstNameLookup = "MICHAEL",
                LastNameOrig = "BALDERAS",
                LastNameLookup = "MICHAEL"
            });
            AddMultiColumnLookupIndexEntity(new DCBC_ENTITY_MultiColumn_LOOKUP_INDEX()
            {
                DATA_SOURCE = "ABRA",
                DCBC_ENTITY_ID = 20006725,
                LicenseNumberOrig = "ABRA-098068",
                LicenseNumberLookup = "ABRA098068",
                CompanyNameOrig = "NELZAR A. GALLO",
                CompanyNameLookup = "NELZAR A GALLO",
                FirstNameOrig = "NELZAR",
                FirstNameLookup = "NELZAR",
                LastNameOrig = "GALLO",
                LastNameLookup = "NELZAR"
            });

            AddMultiColumnLookupIndexEntity(new DCBC_ENTITY_MultiColumn_LOOKUP_INDEX()
            {
                DATA_SOURCE = "BBL",
                DCBC_ENTITY_ID = 10067731,
                LicenseNumberOrig = "931315000135",
                LicenseNumberLookup = "931315000135",
                CompanyNameOrig = "IMA PIZZA STORE 13 LLC.",
                CompanyNameLookup = "IMA PIZZA STORE 13 LLC",
                FirstNameOrig = "",
                FirstNameLookup = "",
                LastNameOrig = "",
                LastNameLookup = ""
            });

            AddMultiColumnLookupIndexEntity(new DCBC_ENTITY_MultiColumn_LOOKUP_INDEX()
            {
                DATA_SOURCE = "BBL",
                DCBC_ENTITY_ID = 10067732,
                LicenseNumberOrig = "931315000136",
                LicenseNumberLookup = "931315000136",
                CompanyNameOrig = "1723 DUPONT L.L.C.",
                CompanyNameLookup = "1723 DUPONT LLC",
                FirstNameOrig = "",
                FirstNameLookup = "",
                LastNameOrig = "",
                LastNameLookup = ""
            });

            AddMultiColumnLookupIndexEntity(new DCBC_ENTITY_MultiColumn_LOOKUP_INDEX()
            {
                DATA_SOURCE = "CBE",
                DCBC_ENTITY_ID = 30000001,
                LicenseNumberOrig = "LSZXR56734082015",
                LicenseNumberLookup = "LSZXR56734082015",
                CompanyNameOrig = "JONES ELECTRIC COMPANY, INC.",
                CompanyNameLookup = "JONES ELECTRIC COMPANY INC",
                FirstNameOrig = "FRITZ JONES",
                FirstNameLookup = "FRITZ JONES",
                LastNameOrig = "FRITZ JONES",
                LastNameLookup = "FRITZ JONES"
            });

            AddMultiColumnLookupIndexEntity(new DCBC_ENTITY_MultiColumn_LOOKUP_INDEX()
            {
                DATA_SOURCE = "CBE",
                DCBC_ENTITY_ID = 30000000,
                LicenseNumberOrig = "LS15059072015",
                LicenseNumberLookup = "LS15059072015",
                CompanyNameOrig = "KALOS CONSTRUCTION COMPANY, INC.",
                CompanyNameLookup = "KALOS CONSTRUCTION COMPANY INC",
                FirstNameOrig = "TAYLOR",
                FirstNameLookup = "TAYLOR",
                LastNameOrig = "BRANSON",
                LastNameLookup = "TAYLOR"
            });

            AddMultiColumnLookupIndexEntity(new DCBC_ENTITY_MultiColumn_LOOKUP_INDEX()
            {
                DATA_SOURCE = "Corp",
                DCBC_ENTITY_ID = 40000000,
                LicenseNumberOrig = "C212821",
                LicenseNumberLookup = "C212821",
                CompanyNameOrig = "2118 14TH STREET",
                CompanyNameLookup = "2118 14TH STREET",
                FirstNameOrig = "",
                FirstNameLookup = "",
                LastNameOrig = "",
                LastNameLookup = ""
            });

            AddMultiColumnLookupIndexEntity(new DCBC_ENTITY_MultiColumn_LOOKUP_INDEX()
            {
                DATA_SOURCE = "Corp",
                DCBC_ENTITY_ID = 40000001,
                LicenseNumberOrig = "C200260",
                LicenseNumberLookup = "C200260",
                CompanyNameOrig = "A ABLE ACCIDENT ADVOCATE",
                CompanyNameLookup = "AABLEACCIDENTADVOCATE",
                FirstNameOrig = "",
                FirstNameLookup = "",
                LastNameOrig = "",
                LastNameLookup = ""
            });

            AddMultiColumnLookupIndexEntity(new DCBC_ENTITY_MultiColumn_LOOKUP_INDEX()
            {
                DATA_SOURCE = "OPLA",
                DCBC_ENTITY_ID = 50000000,
                LicenseNumberOrig = "AA10",
                LicenseNumberLookup = "AA10",
                CompanyNameOrig = "",
                CompanyNameLookup = "",
                FirstNameOrig = "DAVID",
                FirstNameLookup = "DAVID",
                LastNameOrig = "FALK",
                LastNameLookup = "DAVID"
            });

            AddMultiColumnLookupIndexEntity(new DCBC_ENTITY_MultiColumn_LOOKUP_INDEX()
            {
                DATA_SOURCE = "OPLA",
                DCBC_ENTITY_ID = 50000009,
                LicenseNumberOrig = "CPA1174",
                LicenseNumberLookup = "CPA1174",
                CompanyNameOrig = "",
                CompanyNameLookup = "",
                FirstNameOrig = "JERRY",
                FirstNameLookup = "JERRY",
                LastNameOrig = "BURKE",
                LastNameLookup = "JERRY"
            });
        }
    }
}