using System.Collections.Generic;
using BusinessCenter.Common;
using BusinessCenter.Data.Model;

namespace BusinessCenter.Service.Interface
{
    public interface IMastereHopEligibilityService
    {
        IEnumerable<MastereHopEligibilityEntity> GeMastereHop(EhopModel ehopModel);
        int ValidateEhopEligibility(EhopModel ehopModel);
    }
}