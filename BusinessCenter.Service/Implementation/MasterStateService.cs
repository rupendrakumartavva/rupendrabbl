using BusinessCenter.Data;
using BusinessCenter.Data.Interface;
using BusinessCenter.Service.Interface;
using System;
using System.Collections.Generic;

namespace BusinessCenter.Service.Implementation
{
    public class MasterStateService : IMasterStateService
    {
        protected IMasterStateRepository MasterStateRepository;

        /// <summary>
        ///
        /// </summary>
        /// <param name="masterStateRepository"></param>
        public MasterStateService(IMasterStateRepository masterStateRepository)
        {
            MasterStateRepository = masterStateRepository;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public IEnumerable<MasterState> FindById(string countryCode)
        {
            var masterState = MasterStateRepository.FindById(countryCode);
            return masterState;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public IEnumerable<MasterState> StatesFindByCountry(string countryCode)
        {
            var masterState = MasterStateRepository.StateListFindByCountry(countryCode);
            return masterState;
        }
    }
}