using System;
using System.Collections.Generic;
using BusinessCenter.Data;

namespace BusinessCenter.Tests.Setup
{
    public class DCBC_ENTITY_BBL_Renewal_InvoiceData
    {
        private readonly List<DCBC_ENTITY_BBL_Renewal_Invoice> _entities;
        public bool IsInitialized;

        public void AddDCBC_ENTITY_BBL_RenewalsEntity(DCBC_ENTITY_BBL_Renewal_Invoice obj)
        {
            _entities.Add(obj);
        }

        public List<DCBC_ENTITY_BBL_Renewal_Invoice> DcbcEntityBblRenewalsList
        {
            get { return _entities; }
        }

        public DCBC_ENTITY_BBL_Renewal_InvoiceData()
        {
            IsInitialized = true;
            _entities = new List<DCBC_ENTITY_BBL_Renewal_Invoice>();

            AddDCBC_ENTITY_BBL_RenewalsEntity(new DCBC_ENTITY_BBL_Renewal_Invoice()
            {

                As_of_Date = Convert.ToDateTime("2015-09-25"),
                SERV_PROV_CODE = "DC",
                B1_PER_ID1 = "11LIC",
                B1_PER_ID2 = "00000",
                B1_PER_ID3 = "J2611",
                B1_PER_GROUP = "Licenses",
                B1_PER_TYPE = "Business License Renewal",
                B1_PER_SUB_TYPE = "NA",
                B1_PER_CATEGORY = "NA",
                B1_APP_TYPE_ALIAS = "Business License Renewal",
                b1_Alt_ID = "LREN11002826",
                GF_DES = "Cigarette Retail",
                GF_FEE = (decimal?) 39.00,
                GF_UNIT = (decimal?)1.00
               
            });

            AddDCBC_ENTITY_BBL_RenewalsEntity(new DCBC_ENTITY_BBL_Renewal_Invoice()
            {

                As_of_Date = Convert.ToDateTime("2015-09-25"),
                SERV_PROV_CODE = "DC",
                B1_PER_ID1 = "11LIC",
                B1_PER_ID2 = "00000",
                B1_PER_ID3 = "J2611",
                B1_PER_GROUP = "Licenses",
                B1_PER_TYPE = "Business License Renewal",
                B1_PER_SUB_TYPE = "NA",
                B1_PER_CATEGORY = "NA",
                B1_APP_TYPE_ALIAS = "Business License Renewal",
                b1_Alt_ID = "LREN11002826",
                GF_DES = "Hotel",
                GF_FEE = (decimal?)25.00,
                GF_UNIT = (decimal?)1.00

            });


            AddDCBC_ENTITY_BBL_RenewalsEntity(new DCBC_ENTITY_BBL_Renewal_Invoice()
            {

                As_of_Date = Convert.ToDateTime("2015-09-25"),
                SERV_PROV_CODE = "DC",
                B1_PER_ID1 = "11LIC",
                B1_PER_ID2 = "00000",
                B1_PER_ID3 = "J2611",
                B1_PER_GROUP = "Licenses",
                B1_PER_TYPE = "Business License Renewal",
                B1_PER_SUB_TYPE = "NA",
                B1_PER_CATEGORY = "NA",
                B1_APP_TYPE_ALIAS = "Business License Renewal",
                b1_Alt_ID = "LREN11002826",
                GF_DES = "Penalty Fee (Expired)",
                GF_FEE = (decimal?)250.00,
                GF_UNIT = (decimal?)1.00

            });

            AddDCBC_ENTITY_BBL_RenewalsEntity(new DCBC_ENTITY_BBL_Renewal_Invoice()
            {

                As_of_Date = Convert.ToDateTime("2015-09-25"),
                SERV_PROV_CODE = "DC",
                B1_PER_ID1 = "11LIC",
                B1_PER_ID2 = "00000",
                B1_PER_ID3 = "J2611",
                B1_PER_GROUP = "Licenses",
                B1_PER_TYPE = "Business License Renewal",
                B1_PER_SUB_TYPE = "NA",
                B1_PER_CATEGORY = "NA",
                B1_APP_TYPE_ALIAS = "Business License Renewal",
                b1_Alt_ID = "LREN11002826",
                GF_DES = "Application Fee",
                GF_FEE = (decimal?)70.00,
                GF_UNIT = (decimal?)1.00

            });

            AddDCBC_ENTITY_BBL_RenewalsEntity(new DCBC_ENTITY_BBL_Renewal_Invoice()
            {

                As_of_Date = Convert.ToDateTime("2015-09-25"),
                SERV_PROV_CODE = "DC",
                B1_PER_ID1 = "11LIC",
                B1_PER_ID2 = "00000",
                B1_PER_ID3 = "J2611",
                B1_PER_GROUP = "Licenses",
                B1_PER_TYPE = "Business License Renewal",
                B1_PER_SUB_TYPE = "NA",
                B1_PER_CATEGORY = "NA",
                B1_APP_TYPE_ALIAS = "Business License Renewal",
                b1_Alt_ID = "LREN11002826",
                GF_DES = "Enhanced Service Fee of 10%",
                GF_FEE = (decimal?)13.40,
                GF_UNIT = (decimal?)1.00

            });

            AddDCBC_ENTITY_BBL_RenewalsEntity(new DCBC_ENTITY_BBL_Renewal_Invoice()
            {

                As_of_Date = Convert.ToDateTime("2015-09-25"),
                SERV_PROV_CODE = "DC",
                B1_PER_ID1 = "11LIC",
                B1_PER_ID2 = "00000",
                B1_PER_ID3 = "J2611",
                B1_PER_GROUP = "Licenses",
                B1_PER_TYPE = "Business License Renewal",
                B1_PER_SUB_TYPE = "NA",
                B1_PER_CATEGORY = "NA",
                B1_APP_TYPE_ALIAS = "Business License Renewal",
                b1_Alt_ID = "LREN11002826",
                GF_DES = "Penalty Fee (Lapse)",
                GF_FEE = (decimal?)250.00,
                GF_UNIT = (decimal?)1.00

            });

            AddDCBC_ENTITY_BBL_RenewalsEntity(new DCBC_ENTITY_BBL_Renewal_Invoice()
            {

                As_of_Date = Convert.ToDateTime("2015-09-25"),
                SERV_PROV_CODE = "DC",
                B1_PER_ID1 = "13LIC",
                B1_PER_ID2 = "00000",
                B1_PER_ID3 = "37599",
                B1_PER_GROUP = "Licenses",
                B1_PER_TYPE = "Business License Renewal",
                B1_PER_SUB_TYPE = "NA",
                B1_PER_CATEGORY = "NA",
                B1_APP_TYPE_ALIAS = "Business License Renewal",
                b1_Alt_ID = "LREN13018589",
                GF_DES = "Application Fee",
                GF_FEE = (decimal?)70.00,
                GF_UNIT = (decimal?)1.00

            });

            AddDCBC_ENTITY_BBL_RenewalsEntity(new DCBC_ENTITY_BBL_Renewal_Invoice()
            {

                As_of_Date = Convert.ToDateTime("2015-09-25"),
                SERV_PROV_CODE = "DC",
                B1_PER_ID1 = "13LIC",
                B1_PER_ID2 = "00000",
                B1_PER_ID3 = "37599",
                B1_PER_GROUP = "Licenses",
                B1_PER_TYPE = "Business License Renewal",
                B1_PER_SUB_TYPE = "NA",
                B1_PER_CATEGORY = "NA",
                B1_APP_TYPE_ALIAS = "Business License Renewal",
                b1_Alt_ID = "LREN13018589",
                GF_DES = "General Business",
                GF_FEE = (decimal?)200.00,
                GF_UNIT = (decimal?)1.00

            });

            AddDCBC_ENTITY_BBL_RenewalsEntity(new DCBC_ENTITY_BBL_Renewal_Invoice()
            {

                As_of_Date = Convert.ToDateTime("2015-09-25"),
                SERV_PROV_CODE = "DC",
                B1_PER_ID1 = "13LIC",
                B1_PER_ID2 = "00000",
                B1_PER_ID3 = "37599",
                B1_PER_GROUP = "Licenses",
                B1_PER_TYPE = "Business License Renewal",
                B1_PER_SUB_TYPE = "NA",
                B1_PER_CATEGORY = "NA",
                B1_APP_TYPE_ALIAS = "Business License Renewal",
                b1_Alt_ID = "LREN13018589",
                GF_DES = "Enhanced Service Fee of 10%",
                GF_FEE = (decimal?)29.50,
                GF_UNIT = (decimal?)1.00

            });

            AddDCBC_ENTITY_BBL_RenewalsEntity(new DCBC_ENTITY_BBL_Renewal_Invoice()
            {

                As_of_Date = Convert.ToDateTime("2015-09-25"),
                SERV_PROV_CODE = "DC",
                B1_PER_ID1 = "13LIC",
                B1_PER_ID2 = "00000",
                B1_PER_ID3 = "37599",
                B1_PER_GROUP = "Licenses",
                B1_PER_TYPE = "Business License Renewal",
                B1_PER_SUB_TYPE = "NA",
                B1_PER_CATEGORY = "NA",
                B1_APP_TYPE_ALIAS = "Business License Renewal",
                b1_Alt_ID = "LREN13018589",
                GF_DES = "Endorsement Fee",
                GF_FEE = (decimal?)25.00,
                GF_UNIT = (decimal?)1.00

            });

            AddDCBC_ENTITY_BBL_RenewalsEntity(new DCBC_ENTITY_BBL_Renewal_Invoice()
            {

                As_of_Date = Convert.ToDateTime("2015-09-25"),
                SERV_PROV_CODE = "DC",
                B1_PER_ID1 = "11LIC",
                B1_PER_ID2 = "00000",
                B1_PER_ID3 = "J2465",
                B1_PER_GROUP = "Licenses",
                B1_PER_TYPE = "Business License Renewal",
                B1_PER_SUB_TYPE = "NA",
                B1_PER_CATEGORY = "NA",
                B1_APP_TYPE_ALIAS = "Business License Renewal",
                b1_Alt_ID = "LREN11002680",
                GF_DES = "General Business",
                GF_FEE = (decimal?)200.00,
                GF_UNIT = (decimal?)1.00

            });
            AddDCBC_ENTITY_BBL_RenewalsEntity(new DCBC_ENTITY_BBL_Renewal_Invoice()
            {

                As_of_Date = Convert.ToDateTime("2015-09-25"),
                SERV_PROV_CODE = "DC",
                B1_PER_ID1 = "11LIC",
                B1_PER_ID2 = "00000",
                B1_PER_ID3 = "J2465",
                B1_PER_GROUP = "Licenses",
                B1_PER_TYPE = "Business License Renewal",
                B1_PER_SUB_TYPE = "NA",
                B1_PER_CATEGORY = "NA",
                B1_APP_TYPE_ALIAS = "Business License Renewal",
                b1_Alt_ID = "LREN11002680",
                GF_DES = "Restaurant",
                GF_FEE = (decimal?)200.00,
                GF_UNIT = (decimal?)1.00

            });

            AddDCBC_ENTITY_BBL_RenewalsEntity(new DCBC_ENTITY_BBL_Renewal_Invoice()
            {

                As_of_Date = Convert.ToDateTime("2015-09-25"),
                SERV_PROV_CODE = "DC",
                B1_PER_ID1 = "11LIC",
                B1_PER_ID2 = "00000",
                B1_PER_ID3 = "J2465",
                B1_PER_GROUP = "Licenses",
                B1_PER_TYPE = "Business License Renewal",
                B1_PER_SUB_TYPE = "NA",
                B1_PER_CATEGORY = "NA",
                B1_APP_TYPE_ALIAS = "Business License Renewal",
                b1_Alt_ID = "LREN11002680",
                GF_DES = "RAO Fee",
                GF_FEE = (decimal?)43.00,
                GF_UNIT = (decimal?)1.00

            });

            AddDCBC_ENTITY_BBL_RenewalsEntity(new DCBC_ENTITY_BBL_Renewal_Invoice()
            {

                As_of_Date = Convert.ToDateTime("2015-09-25"),
                SERV_PROV_CODE = "DC",
                B1_PER_ID1 = "11LIC",
                B1_PER_ID2 = "00000",
                B1_PER_ID3 = "J2465",
                B1_PER_GROUP = "Licenses",
                B1_PER_TYPE = "Business License Renewal",
                B1_PER_SUB_TYPE = "NA",
                B1_PER_CATEGORY = "NA",
                B1_APP_TYPE_ALIAS = "Business License Renewal",
                b1_Alt_ID = "LREN11002680",
                GF_DES = "Penalty Fee (Expired)",
                GF_FEE = (decimal?)250.00,
                GF_UNIT = (decimal?)1.00

            });

            AddDCBC_ENTITY_BBL_RenewalsEntity(new DCBC_ENTITY_BBL_Renewal_Invoice()
            {

                As_of_Date = Convert.ToDateTime("2015-09-25"),
                SERV_PROV_CODE = "DC",
                B1_PER_ID1 = "11LIC",
                B1_PER_ID2 = "00000",
                B1_PER_ID3 = "J2465",
                B1_PER_GROUP = "Licenses",
                B1_PER_TYPE = "Business License Renewal",
                B1_PER_SUB_TYPE = "NA",
                B1_PER_CATEGORY = "NA",
                B1_APP_TYPE_ALIAS = "Business License Renewal",
                b1_Alt_ID = "LREN11002680",
                GF_DES = "Enhanced Service Fee of 10%",
                GF_FEE = (decimal?)29.50,
                GF_UNIT = (decimal?)1.00

            });

            AddDCBC_ENTITY_BBL_RenewalsEntity(new DCBC_ENTITY_BBL_Renewal_Invoice()
            {

                As_of_Date = Convert.ToDateTime("2015-09-25"),
                SERV_PROV_CODE = "DC",
                B1_PER_ID1 = "11LIC",
                B1_PER_ID2 = "00000",
                B1_PER_ID3 = "J2465",
                B1_PER_GROUP = "Licenses",
                B1_PER_TYPE = "Business License Renewal",
                B1_PER_SUB_TYPE = "NA",
                B1_PER_CATEGORY = "NA",
                B1_APP_TYPE_ALIAS = "Business License Renewal",
                b1_Alt_ID = "LREN11002680",
                GF_DES = "Endorsement Fee",
                GF_FEE = (decimal?)25.00,
                GF_UNIT = (decimal?)1.00

            });
            AddDCBC_ENTITY_BBL_RenewalsEntity(new DCBC_ENTITY_BBL_Renewal_Invoice()
            {

                As_of_Date = Convert.ToDateTime("2015-09-25"),
                SERV_PROV_CODE = "DC",
                B1_PER_ID1 = "11LIC",
                B1_PER_ID2 = "00000",
                B1_PER_ID3 = "J2465",
                B1_PER_GROUP = "Licenses",
                B1_PER_TYPE = "Business License Renewal",
                B1_PER_SUB_TYPE = "NA",
                B1_PER_CATEGORY = "NA",
                B1_APP_TYPE_ALIAS = "Business License Renewal",
                b1_Alt_ID = "LREN11002680",
                GF_DES = "Penalty Fee (Lapse)",
                GF_FEE = (decimal?)250.00,
                GF_UNIT = (decimal?)1.00

            });
            AddDCBC_ENTITY_BBL_RenewalsEntity(new DCBC_ENTITY_BBL_Renewal_Invoice()
            {

                As_of_Date = Convert.ToDateTime("2015-09-25"),
                SERV_PROV_CODE = "DC",
                B1_PER_ID1 = "11LIC",
                B1_PER_ID2 = "00000",
                B1_PER_ID3 = "J2465",
                B1_PER_GROUP = "Licenses",
                B1_PER_TYPE = "Business License Renewal",
                B1_PER_SUB_TYPE = "NA",
                B1_PER_CATEGORY = "NA",
                B1_APP_TYPE_ALIAS = "Business License Renewal",
                b1_Alt_ID = "LREN11002680",
                GF_DES = "Application Fee",
                GF_FEE = (decimal?)70.00,
                GF_UNIT = (decimal?)1.00

            });
            //--------------------------------------
            AddDCBC_ENTITY_BBL_RenewalsEntity(new DCBC_ENTITY_BBL_Renewal_Invoice()
            {

                As_of_Date = Convert.ToDateTime("2015-09-25"),
                SERV_PROV_CODE = "DC",
                B1_PER_ID1 = "11LIC",
                B1_PER_ID2 = "00000",
                B1_PER_ID3 = "J2465",
                B1_PER_GROUP = "Licenses",
                B1_PER_TYPE = "Business License Renewal",
                B1_PER_SUB_TYPE = "NA",
                B1_PER_CATEGORY = "NA",
                B1_APP_TYPE_ALIAS = "Business License Renewal",
                b1_Alt_ID = "LREN11002786",
                GF_DES = "Application Fee",
                GF_FEE = (decimal?)70.00,
                GF_UNIT = (decimal?)1.00

            });

            AddDCBC_ENTITY_BBL_RenewalsEntity(new DCBC_ENTITY_BBL_Renewal_Invoice()
            {

                As_of_Date = Convert.ToDateTime("2015-09-25"),
                SERV_PROV_CODE = "DC",
                B1_PER_ID1 = "11LIC",
                B1_PER_ID2 = "00000",
                B1_PER_ID3 = "J2465",
                B1_PER_GROUP = "Licenses",
                B1_PER_TYPE = "Business License Renewal",
                B1_PER_SUB_TYPE = "NA",
                B1_PER_CATEGORY = "NA",
                B1_APP_TYPE_ALIAS = "Business License Renewal",
                b1_Alt_ID = "LREN11002786",
                GF_DES = "Penalty Fee (Expired)",
                GF_FEE = (decimal?)250.00,
                GF_UNIT = (decimal?)1.00

            });
            AddDCBC_ENTITY_BBL_RenewalsEntity(new DCBC_ENTITY_BBL_Renewal_Invoice()
            {

                As_of_Date = Convert.ToDateTime("2015-09-25"),
                SERV_PROV_CODE = "DC",
                B1_PER_ID1 = "11LIC",
                B1_PER_ID2 = "00000",
                B1_PER_ID3 = "J2465",
                B1_PER_GROUP = "Licenses",
                B1_PER_TYPE = "Business License Renewal",
                B1_PER_SUB_TYPE = "NA",
                B1_PER_CATEGORY = "NA",
                B1_APP_TYPE_ALIAS = "Business License Renewal",
                b1_Alt_ID = "LREN11002786",
                GF_DES = "Enhanced Service Fee of 10%",
                GF_FEE = (decimal?)29.50,
                GF_UNIT = (decimal?)1.00

            });

            AddDCBC_ENTITY_BBL_RenewalsEntity(new DCBC_ENTITY_BBL_Renewal_Invoice()
            {

                As_of_Date = Convert.ToDateTime("2015-09-25"),
                SERV_PROV_CODE = "DC",
                B1_PER_ID1 = "11LIC",
                B1_PER_ID2 = "00000",
                B1_PER_ID3 = "J2465",
                B1_PER_GROUP = "Licenses",
                B1_PER_TYPE = "Business License Renewal",
                B1_PER_SUB_TYPE = "NA",
                B1_PER_CATEGORY = "NA",
                B1_APP_TYPE_ALIAS = "Business License Renewal",
                b1_Alt_ID = "LREN11002786",
                GF_DES = "Endorsement Fee",
                GF_FEE = (decimal?)25.00,
                GF_UNIT = (decimal?)1.00

            });
            AddDCBC_ENTITY_BBL_RenewalsEntity(new DCBC_ENTITY_BBL_Renewal_Invoice()
            {

                As_of_Date = Convert.ToDateTime("2015-09-25"),
                SERV_PROV_CODE = "DC",
                B1_PER_ID1 = "11LIC",
                B1_PER_ID2 = "00000",
                B1_PER_ID3 = "J2465",
                B1_PER_GROUP = "Licenses",
                B1_PER_TYPE = "Business License Renewal",
                B1_PER_SUB_TYPE = "NA",
                B1_PER_CATEGORY = "NA",
                B1_APP_TYPE_ALIAS = "Business License Renewal",
                b1_Alt_ID = "LREN11002786",
                GF_DES = "Penalty Fee (Lapse)",
                GF_FEE = (decimal?)250.00,
                GF_UNIT = (decimal?)1.00

            });
            AddDCBC_ENTITY_BBL_RenewalsEntity(new DCBC_ENTITY_BBL_Renewal_Invoice()
            {

                As_of_Date = Convert.ToDateTime("2015-09-25"),
                SERV_PROV_CODE = "DC",
                B1_PER_ID1 = "11LIC",
                B1_PER_ID2 = "00000",
                B1_PER_ID3 = "J2465",
                B1_PER_GROUP = "Licenses",
                B1_PER_TYPE = "Business License Renewal",
                B1_PER_SUB_TYPE = "NA",
                B1_PER_CATEGORY = "NA",
                B1_APP_TYPE_ALIAS = "Business License Renewal",
                b1_Alt_ID = "LREN11002786",
                GF_DES = "One Family Rental",
                GF_FEE = (decimal?)70.00,
                GF_UNIT = (decimal?)1.00

            });
            AddDCBC_ENTITY_BBL_RenewalsEntity(new DCBC_ENTITY_BBL_Renewal_Invoice()
            {

                As_of_Date = Convert.ToDateTime("2015-09-25"),
                SERV_PROV_CODE = "DC",
                B1_PER_ID1 = "11LIC",
                B1_PER_ID2 = "00000",
                B1_PER_ID3 = "J2465",
                B1_PER_GROUP = "Licenses",
                B1_PER_TYPE = "Business License Renewal",
                B1_PER_SUB_TYPE = "NA",
                B1_PER_CATEGORY = "NA",
                B1_APP_TYPE_ALIAS = "Business License Renewal",
                b1_Alt_ID = "LREN15024280",
                GF_DES = "RAO Fee",
                GF_FEE = (decimal?)43.00,
                GF_UNIT = (decimal?)1.00

            });
            AddDCBC_ENTITY_BBL_RenewalsEntity(new DCBC_ENTITY_BBL_Renewal_Invoice()
            {

                As_of_Date = Convert.ToDateTime("2015-09-25"),
                SERV_PROV_CODE = "DC",
                B1_PER_ID1 = "11LIC",
                B1_PER_ID2 = "00000",
                B1_PER_ID3 = "J2465",
                B1_PER_GROUP = "Licenses",
                B1_PER_TYPE = "Business License Renewal",
                B1_PER_SUB_TYPE = "NA",
                B1_PER_CATEGORY = "NA",
                B1_APP_TYPE_ALIAS = "Business License Renewal",
                b1_Alt_ID = "LREN11002826",
                GF_DES = "RAO Fee",
                GF_FEE = (decimal?)43.00,
                GF_UNIT = (decimal?)1.00

            });
        }
         

    }
}