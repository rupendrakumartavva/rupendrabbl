using BusinessCenter.Data;
using BusinessCenter.Data.Interface;
using BusinessCenter.Service.Interface;
using System.Collections.Generic;

namespace BusinessCenter.Service.Implementation
{
    public class MasterCountryService : IMasterCountryService
    {
        protected IMasterCountryRepository MasterCountryRepository;

        /// <summary>
        ///
        /// </summary>
        /// <param name="msaterCountryRepository"></param>
        public MasterCountryService(IMasterCountryRepository msaterCountryRepository)
        {
            MasterCountryRepository = msaterCountryRepository;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MasterCountry> GetCountryList()
        {
            return MasterCountryRepository.GetCountryList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public IEnumerable<MasterCountry> FindCountryBasedOnCode(string countryCode)
        {
            return MasterCountryRepository.FindCountryBasedOnCode(countryCode);
        }
    }
}