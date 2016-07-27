using System.Collections.Generic;
using System.Linq;
using BusinessCenter.Data;
using BusinessCenter.Data.Model;

namespace BusinessCenter.Service.Interface
{
    public interface IEtlAddressAndParcelService
    {
        IList<AddressDetails> ListEtlAddressDetails(string enterAddress);
    }
}