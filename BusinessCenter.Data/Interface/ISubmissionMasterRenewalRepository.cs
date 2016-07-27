using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
   public interface ISubmissionMasterRenewalRepository
   {
       string InsertRenewalDetails(RenewModel Rmodel);
       IEnumerable<SubmissionMasterRenewal> FindByID(string MasterId);
       bool DeleteMasterRenewal(string masterId);
       bool updateIscorpStatus(bool iscorpregistration, string masterid);
   }
}
