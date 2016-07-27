using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
   public interface IRenewRepository
   {
       RenewModel CheckRenewal(RenewModel renewModel);
       bool CheckDocument(DocumentCheck documentCheck);
       bool UpdateRenwalDocumentType(DocumentCheck documentCheck);
       bool DeleteRenewal(RenewModel renewModel);
       RenewModel CheckDocument(RenewModel renewModel);
       RenewModel CheckAmount(RenewModel renewModel);
       string CheckCategoryStatus(string entityID, string renewallicenseNumber, string renewallrenNumber);
     // string GetCorpNumber(string masterid);
   }
}
