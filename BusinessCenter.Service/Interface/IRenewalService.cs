using BusinessCenter.Data;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Service.Interface
{
   public interface IRenewalService
   {
       IEnumerable<DCBC_ENTITY_BBL> GetRenewData(RenewModel renewModel);
       RenewModel CheckRenewal(RenewModel renewModel);
       bool CheckDocument(DocumentCheck documentCheck);
       bool UpdateRenwalDocumentType(DocumentCheck documentCheck);
       bool DeleteRenewal(RenewModel renewModel);
       IEnumerable<SubmissionMasterRenewal> FindByID(string masterId);
       RenewModel CheckDocument(RenewModel renewModel);
       RenewModel CheckAmount(RenewModel renewModel);
       string CheckCategoryStatus(string entityID, string renewallicenseNumber, string renewallrenNumber);
   }
}
