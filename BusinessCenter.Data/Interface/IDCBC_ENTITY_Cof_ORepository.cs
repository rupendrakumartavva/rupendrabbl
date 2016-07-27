using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
  public  interface IDCBC_ENTITY_Cof_ORepository
  {
      List<CofoHopDetailsModel> FindByNumberandDateofIssue(CofoHopDetailsModel cofoHopDetailsModel);
      List<DCBC_ENTITY_Cof_O> FindByNumber(string number, string dateofissue);
      List<StreetDetails> DropDownBind();

      List<CofoHopDetailsModel> GetSubmissionCofoOrHopDetails(string masterId);
     // void CofoServiceStatus(string Masterid);
  }
}
