using System.Collections.Generic;
using BusinessCenter.Data;

namespace BusinessCenter.Service.Interface
{
    public interface IMasterStateService
    {
        IEnumerable<MasterState> FindById(string countryCode);
		 IEnumerable<MasterState> StatesFindByCountry(string countryCode);
    }
}