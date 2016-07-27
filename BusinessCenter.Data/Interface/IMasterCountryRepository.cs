using System.Collections.Generic;

namespace BusinessCenter.Data.Interface
{
    public interface IMasterCountryRepository
    {
        IEnumerable<MasterCountry> GetCountryList();
        IEnumerable<MasterCountry> FindCountryBasedOnCode(string countryCode);
        IEnumerable<MasterCountry> FindCountryBasedOnName(string countryName);
    }
}