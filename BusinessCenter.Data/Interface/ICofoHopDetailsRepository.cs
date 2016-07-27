using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
    public interface ICofoHopDetailsRepository
    {
      //  List<CofoHopDetailsModel> FindByNumberandDateofIssue(CofoHopDetailsModel cofoHopDetailsModel);
        List<StreetDetails> DropDownBind();
       // bool InsertCofoHopDetails(CofoHopDetailsModel cofoHopDetailsModel);
        //bool UpdateCofoHopDetails(CofoHopDetailsModel cofoHopDetailsModel);
        List<CofoHopDetailsModel> GetSubmissionCofoOrHopDetails(string masterId);
    }
}
