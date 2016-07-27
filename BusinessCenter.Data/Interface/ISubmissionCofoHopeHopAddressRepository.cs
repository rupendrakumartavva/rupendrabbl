using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
    public interface ISubmissionCofoHopeHopAddressRepository
    {
       // IEnumerable<SubmissionCofo_Hop_Ehop_Address> FindByID(GeneralBusiness generalBusiness);
        IEnumerable<GeneralBusiness> GetCorpBusinessData(GeneralBusiness generalBusiness);
        bool InsertSubmissionLocation(int customtype, CofoHopDetailsModel generalbusiness);
        IEnumerable<SubmissionCofo_Hop_Ehop_Address> GetPrimisessAddress(int customTypeid);
        IEnumerable<SubmissionCofo_Hop_Ehop_Address> GetPrimisessAddress(int customTypeId, string customType);
        bool DeleteBusinessAddress(CofoHopDetailsModel cofoModel);
        bool DeleteHOP(int CustomTypeId);
        string GetBusinessOwnerFullName(string masterId);
        string GetTradeNameWithSubmissionQuestions(string masterId);
        
    }
}
