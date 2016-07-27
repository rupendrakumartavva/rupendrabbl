using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
    public interface ISubmissionEHOPEligibilityRepository
    {
        bool InsertEHopEligibility(EligibilityModel eligibilityModel);
        EhopModel MasterHopEligibility(EhopModel ehopModel);
        int ValidateEhopEligibility(EhopModel ehopModel);
        EhopData EhopData(EhopData ehopData);
    }
}
