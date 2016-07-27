using BusinessCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Setup
{
    public class EtlAddressAndParcelRepositoryData
    {
        private readonly List<TBL_ETL_Address_And_Parcel> _entities;
        public bool IsInitialized;

        public void AddETLAddessEntity(TBL_ETL_Address_And_Parcel obj)
        {
            _entities.Add(obj);
        }

        public List<TBL_ETL_Address_And_Parcel> ETLAddessEntitiesList
        {
            get { return _entities; }
        }


        public EtlAddressAndParcelRepositoryData()
        {
            IsInitialized = true;
            _entities = new List<TBL_ETL_Address_And_Parcel>();

            AddETLAddessEntity(new TBL_ETL_Address_And_Parcel()
            {
                L1_PARCEL_NBR = "5724W   0057",
                L1_HSE_NBR_START = 600,

                L1_STR_NAME = "MARYLAND",

                L1_STR_SUFFIX = "AVE",
                L1_STR_SUFFIX_DIR = "SW",
                L1_INSP_DISTRICT_PREFIX = "8",
                L1_INSP_DISTRICT = "38",
                L1_SITUS_NBRHD_PREFIX = "C",
                L1_SITUS_NBRHD = "Randle Heights",
                L1_SITUS_CITY = "WASHINGTON",

                L1_SITUS_STATE = "DC",
                L1_SITUS_ZIP = "20020",
                L1_ADDR_STATUS = "A",

                L1_UDF1 = "8",
                L1_UDF2 = "R-2",
                L1_UDF3 = "8B",
                L1_UDF4 = "8B06",
                L1_FULL_ADDRESS = "3411 25TH ST SE"
            });


        }
    }
}
