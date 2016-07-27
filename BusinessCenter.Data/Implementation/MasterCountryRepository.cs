using System.Collections.Generic;
using System.Linq;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;

namespace BusinessCenter.Data.Implementation
{
    public class MasterCountryRepository : GenericRepository<MasterCountry>, IMasterCountryRepository
    {
        public MasterCountryRepository(IUnitOfWork context)
            : base(context)
        {
           

        }
        /// <summary>
        /// This method is used to get all country list .
        /// </summary>
        /// <returns>Return master country data</returns>
        public IEnumerable<MasterCountry> GetCountryList()
        {
            return GetAll().AsQueryable().OrderBy(x=>x.CountryName);
        }
        /// <summary>
        /// This method is used to get specific country data based on country code
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns>Return master country data</returns>
        public IEnumerable<MasterCountry> FindCountryBasedOnCode(string countryCode)
        {
            return FindBy(x => x.CountryCode == countryCode);
        }
        /// <summary>
        /// This method is used to get specific country data based on country code
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns>Return master country data</returns>
        public IEnumerable<MasterCountry> FindCountryBasedOnName(string countryName)
        {
            return FindBy(x => x.CountryName == countryName);
        }
    }
}