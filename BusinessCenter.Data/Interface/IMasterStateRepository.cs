using System.Collections.Generic;

namespace BusinessCenter.Data.Interface
{
    public interface IMasterStateRepository
    {
        IEnumerable<MasterState> FindById(string countryCode);
        IEnumerable<MasterState> GetStateName(string stateCode, string countryCode);
		 IEnumerable<MasterState> StateListFindByCountry(string countryCode);
         IEnumerable<MasterState> GetStateCode(string stateName, string countryCode);
    }
}