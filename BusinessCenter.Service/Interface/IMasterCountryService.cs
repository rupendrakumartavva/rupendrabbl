using System.Collections.Generic;
using BusinessCenter.Data;

namespace BusinessCenter.Service.Interface
{
    public interface IMasterCountryService
    {
        IEnumerable<MasterCountry> GetCountryList();
        IEnumerable<MasterCountry> FindCountryBasedOnCode(string countryCode);
    }
}