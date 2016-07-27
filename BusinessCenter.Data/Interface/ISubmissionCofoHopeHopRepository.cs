using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Common;

namespace BusinessCenter.Data.Interface
{
   public interface ISubmissionCofoHopeHopRepository
    {
       bool InsertSubmissionLocation(CofoHopDetailsModel generalbusiness);
      // IEnumerable<SubmissionLocationandStruc> FindByID(GeneralBusiness generalBusiness);
       GeneralBusiness GetPrimisessAddress(GeneralBusiness DetailsModel);
       IEnumerable<SubmissionCofo_Hop_Ehop> FindByID(string masterId);
       bool DeleteCofo(CofoHopDetailsModel cofoModel);
       bool DeleteHOP(string MasterID);
       bool DeleteHopPrimsesAddrssOnly(CofoHopDetailsModel cofoModel);
       IEnumerable<SubmissionCofo_Hop_Ehop> EhopNumberWithMasterId(string masterId);
       IEnumerable<SubmissionCofo_Hop_Ehop_Address> GetPrimisessAddress(int customTypeid);
       TaxAndReneueInitailDisplay DisplayTaxAndRevenuWithPrimisessDetails(string masterId);

       string BusinessOwnerName(string masterId);
    }
}
