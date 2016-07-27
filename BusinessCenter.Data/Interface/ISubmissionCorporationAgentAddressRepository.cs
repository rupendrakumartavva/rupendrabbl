using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
    public interface ISubmissionCorporationAgentAddressRepository
    {
        IEnumerable<SubmissionCorporation_Agent_Address> FindById(SubmissionCorpAgentModel subCorpAgentModel);
        bool InsertCorporationDetails(int regId, GeneralBusiness detailsModel);
        IEnumerable<SubmissionCorporation_Agent_Address> GetHeadQuarterAddress(int submissionid, string addresstype, string fileNumber);
        IEnumerable<SubmissionCorporation_Agent_Address> FindByTypewithMasterId(int masterId, string type);
        IEnumerable<SubmissionCorporation_Agent_Address> FindBySubID(int subId);
        bool DeleteHqAddress(int SubID, string typeName);
    }
}
