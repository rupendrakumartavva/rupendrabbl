using System.Collections.Generic;
using System.Linq;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Model;

namespace BusinessCenter.Data.Interface
{
    public interface IEtlAddressAndParcelRepository
    {
        IEnumerable<TBL_ETL_Address_And_Parcel> FindByStreetNumber(CofoHopDetailsModel cofohopdetails);
        IEnumerable<TBL_ETL_Address_And_Parcel> FindByStreetName(CofoHopDetailsModel cofohopdetails);
        IEnumerable<TBL_ETL_Address_And_Parcel> FindByStreetType(CofoHopDetailsModel cofohopdetails,string StreetType);
        IEnumerable<TBL_ETL_Address_And_Parcel> FindByQuadrant(CofoHopDetailsModel cofohopdetails, string StreetType);
        IEnumerable<TBL_ETL_Address_And_Parcel> FindByDetails(CofoHopDetailsModel cofohopdetails, string StreetType);
        IList<AddressDetails> ListEtlAddressDetails(string enterAddress);
    }
}