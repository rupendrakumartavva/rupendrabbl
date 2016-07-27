using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using System.Collections.Generic;
using System.Linq;

namespace BusinessCenter.Data.Implementation
{
    public class CountryRepository : GenericRepository<MasterCountry>, ICountryRepository
    {
        public CountryRepository(IUnitOfWork context)
            : base(context)
        {
        }

        /// <summary>
        /// This method is used to retrive all the Countries records
        /// </summary>
        /// <returns>All masterCount list Records</returns>
        public IEnumerable<MasterCountry> GetAllCountries()
        {
            return GetAll().AsQueryable();
        }
    }
}
