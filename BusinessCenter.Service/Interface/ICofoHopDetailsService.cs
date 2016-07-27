using BusinessCenter.Data;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Service.Interface
{
    public interface ICofoHopDetailsService
    {
        IEnumerable<CofoHopDetailsModel> FindByNumberandDateofIssue(CofoHopDetailsModel cofoHopDetailsModel);
        List<StreetDetails> DropDownsBind();
      //  bool InsertCofoHopDetails(CofoHopDetailsModel cofoHopDetailsModel);
       // bool UpdateCofoHopDetails(CofoHopDetailsModel cofoHopDetailsModel);
        List<CofoHopDetailsModel> GetSubmissionCofoOrHopDetails(string masterId);
    }
}
