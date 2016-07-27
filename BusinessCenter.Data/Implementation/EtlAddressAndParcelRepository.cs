using System;
using System.Collections.Generic;
using System.Linq;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;

namespace BusinessCenter.Data.Implementation
{
    public class EtlAddressAndParcelRepository : GenericRepository<TBL_ETL_Address_And_Parcel>, IEtlAddressAndParcelRepository
    {
        public EtlAddressAndParcelRepository(IUnitOfWork context)
            : base(context)
        {
        }
        /// <summary>
        /// This method is used to retrive TBL_ETL_Address_And_Parcel data based on L1_HSE_NBR_START
        /// </summary>
        /// <param name="cofohopdetails"></param>
        /// <returns>Return TBL_ETL_Address_And_Parcel Data</returns>
        public IEnumerable<TBL_ETL_Address_And_Parcel> FindByStreetNumber(CofoHopDetailsModel cofohopdetails)
        {
            try
            {
                return FindBy(x => x.L1_HSE_NBR_START.ToString().ToUpper() == cofohopdetails.AddressNumber.ToString().ToUpper()).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// This method is used to retrive TBL_ETL_Address_And_Parcel data based on L1_HSE_NBR_START and L1_STR_NAME.
        /// </summary>
        /// <param name="cofohopdetails"></param>
        /// <returns>Return TBL_ETL_Address_And_Parcel Data</returns>
        public IEnumerable<TBL_ETL_Address_And_Parcel> FindByStreetName( CofoHopDetailsModel cofohopdetails)
        {
            try
            {
                return  FindBy(x => x.L1_HSE_NBR_START.ToString().ToUpper() == cofohopdetails.AddressNumber.ToString().ToUpper() &&
                    x.L1_STR_NAME.ToString().ToUpper() == cofohopdetails.StreetName.ToString().ToUpper()).ToList();
                // return query;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// This method is used to retrive TBL_ETL_Address_And_Parcel data based on L1_HSE_NBR_START ,L1_STR_NAME and L1_STR_SUFFIX.
        /// </summary>
        /// <param name="cofohopdetails"></param>
        /// <param name="StreetType"></param>
        /// <returns>Return TBL_ETL_Address_And_Parcel Data</returns>
        public IEnumerable<TBL_ETL_Address_And_Parcel> FindByStreetType(CofoHopDetailsModel cofohopdetails, string StreetType)
        {
            try
            {
                return  FindBy(x => x.L1_HSE_NBR_START.ToString().ToUpper() == cofohopdetails.AddressNumber.ToString().ToUpper() &&
                    x.L1_STR_NAME.ToString().ToUpper() == cofohopdetails.StreetName.ToString().ToUpper() &&
                    x.L1_STR_SUFFIX.ToString().ToUpper() == StreetType.ToString().ToUpper()).ToList();
                // return query;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// This method is used to retrive TBL_ETL_Address_And_Parcel data based on L1_HSE_NBR_START ,L1_STR_NAME , L1_STR_SUFFIX and L1_STR_SUFFIX_DIR.
        /// </summary>
        /// <param name="cofohopdetails"></param>
        /// <param name="StreetType"></param>
        /// <returns>Return TBL_ETL_Address_And_Parcel Data</returns>
        public IEnumerable<TBL_ETL_Address_And_Parcel> FindByQuadrant(CofoHopDetailsModel cofohopdetails, string StreetType)
        {
            try
            {
                return  FindBy(x => x.L1_HSE_NBR_START.ToString().ToUpper() == cofohopdetails.AddressNumber.ToString().ToUpper() &&
                    x.L1_STR_NAME.ToString().ToUpper() == cofohopdetails.StreetName.ToString().ToUpper() &&
                    x.L1_STR_SUFFIX.ToString().ToUpper() == StreetType.ToString().ToUpper() &&
                    x.L1_STR_SUFFIX_DIR.ToString().ToUpper() == cofohopdetails.Quadrant.ToString().ToUpper()).ToList();
                // return query;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// This method is used to retrive TBL_ETL_Address_And_Parcel data based on L1_HSE_NBR_START ,L1_STR_NAME , L1_STR_SUFFIX and L1_STR_SUFFIX_DIR.
        /// </summary>
        /// <param name="cofohopdetails"></param>
        /// <param name="StreetType"></param>
        /// <returns>Return TBL_ETL_Address_And_Parcel Data</returns>
        public IEnumerable<TBL_ETL_Address_And_Parcel> FindByDetails(CofoHopDetailsModel cofohopdetails,string StreetType)
        {
            try
            {
                return FindBy(x => x.L1_STR_SUFFIX_DIR.ToString().ToUpper() == cofohopdetails.Quadrant.ToString().ToUpper()
                    && x.L1_STR_SUFFIX.ToString().ToUpper() == StreetType.ToString().ToUpper()
                    && x.L1_STR_NAME.ToString().ToUpper() == cofohopdetails.StreetName.ToString().ToUpper()
                && x.L1_HSE_NBR_START.ToString().ToUpper() == cofohopdetails.AddressNumber.ToString().ToUpper()).ToList();
                // return query;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// This method is used to retrive AddressDetails based on L1_FULL_ADDRESS starts. 
        /// </summary>
        /// <param name="enterAddress"></param>
        /// <returns></returns>
        public IList<AddressDetails> ListEtlAddressDetails(string enterAddress)
        {

            var cofoData = FindBySingle(x => x.L1_FULL_ADDRESS.StartsWith(enterAddress)).OrderBy(x=>x.L1_FULL_ADDRESS).ToList();
            return cofoData.Select(cofdetailslist => new AddressDetails
            {
                FullAddress = (cofdetailslist.L1_FULL_ADDRESS ?? "").Trim(),
                AddressNumber = (cofdetailslist.L1_HSE_NBR_START).ToString(),
                AddressNumberSufix = (cofdetailslist.L1_HSE_FRAC_NBR_START ?? "").Trim(),
                StreetName = (cofdetailslist.L1_STR_NAME ?? "").Trim(),
                StreetType = (cofdetailslist.L1_STR_SUFFIX ?? "").Trim(),
                Quadrant = (cofdetailslist.L1_STR_SUFFIX_DIR ?? "").Trim(),
                City = (cofdetailslist.L1_SITUS_CITY ?? "").Trim(),
                State = (cofdetailslist.L1_SITUS_STATE ?? "").Trim(),
                ZipCode = (cofdetailslist.L1_SITUS_ZIP ?? "").Trim(),
                UnitType = (cofdetailslist.L1_UNIT_TYPE ?? "").Trim(),
                Xcoord = string.Empty,
                Ycoord =string.Empty,
                Anc = (cofdetailslist.L1_UDF3 ?? "").Trim(),
                Ward = (cofdetailslist.L1_UDF1 ?? "").Trim(),
                Cluster = string.Empty,
                Latitude = string.Empty,
                Longitude = string.Empty,
                Vote_Prcnct = string.Empty,
                UnitNumber = (cofdetailslist.L1_UNIT_START??"").Trim(),
                Phone = string.Empty,
                Email = string.Empty,
                Zone = (cofdetailslist.L1_UDF2 ?? "").Trim(),
                SMD = (cofdetailslist.L1_UDF4 ?? "").Trim(),
                SSL = (cofdetailslist.L1_PARCEL_NBR ?? "").Trim()
            }).ToList(); 
           
        }
    }
}