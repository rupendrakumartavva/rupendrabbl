using System;
using System.Collections.Generic;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;

namespace BusinessCenter.Data.Implementation
{
    public class MasterStateRepository : GenericRepository<MasterState>, IMasterStateRepository
    {
        public MasterStateRepository(IUnitOfWork context)
            : base(context)
        {

        }
        /// <summary>
        /// This method is used get specific master state data based on state code
        /// </summary>
        /// <param name="stateCode"></param>
        /// <returns>Return master state data</returns>
        public IEnumerable<MasterState> FindById(string stateCode)
        {
            var masterState = FindBy(x => x.StateCode.ToUpper().Trim() == stateCode.ToUpper().Trim());
            return masterState;
        }
        /// <summary>
        /// This method is used to get specific master state data based on state code and country code
        /// </summary>
        /// <param name="stateCode"></param>
        /// <param name="countryCode"></param>
        /// <returns>Return master state data</returns>
        public IEnumerable<MasterState> GetStateName(string stateCode,string countryCode)
        {
            var masterState = FindBy(x => x.StateCode.ToUpper().Trim() == stateCode.ToUpper().Trim() && x.CountryCode.ToUpper().Trim() == countryCode.ToUpper().Trim());
            return masterState;
        }
        /// <summary>
        /// This method is used to get specific master state data based on country code.
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns>Return master state data</returns>
		 public IEnumerable<MasterState> StateListFindByCountry(string countryCode)
        {
            var masterState = FindBy(x => x.CountryCode.ToUpper().Trim() == countryCode.ToUpper().Trim());
            return masterState;
        }
        /// <summary>
        /// This method is used to get specific state data based on state name and country code
        /// </summary>
        /// <param name="stateName"></param>
        /// <param name="countryCode"></param>
         /// <returns>Return master state data</returns>
         public IEnumerable<MasterState> GetStateCode(string stateName, string countryCode)
         {
             var masterState = FindBy(x => x.StateName.ToUpper().Trim() == stateName.ToUpper().Trim() && x.CountryCode.ToUpper().Trim() == countryCode.ToUpper().Trim());
             return masterState;
         }
    }
}