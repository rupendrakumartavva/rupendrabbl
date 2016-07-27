using BusinessCenter.Data;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Common;

namespace BusinessCenter.Service.Interface
{
    public interface ISubmissionCofoHopeHopAddressService
    {
       // IEnumerable<SubmissionCofo_Hop_Ehop_Address> FindByID(GeneralBusiness generalBusiness);
        IEnumerable<GeneralBusiness> GetCorpBusinessData(GeneralBusiness generalBusiness);
      //  TaxAndReneueInitailDisplay DisplayTaxAndRevenuWithPrimisessDetails(string masterId);
    }
}
