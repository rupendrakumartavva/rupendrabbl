using System.Collections.Generic;

namespace BusinessCenter.Data.Interface
{
    public interface IMastereHopEligibilityRepository
    {
        IEnumerable<MastereHOPEligibility> GeMastereHopEligibility();
      //  IEnumerable<MastereHOPEligibility> FindByEligibilityName(string Name);
    }
}