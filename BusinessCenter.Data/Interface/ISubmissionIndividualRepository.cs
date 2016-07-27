using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Common;

namespace BusinessCenter.Data.Interface
{
  public  interface ISubmissionIndividualRepository
    {
      IEnumerable<SubmissionIndividual> FindByID(int enitityid);
      int InsertUpdateSubmissionIndividual(SubmissionIndividualEntity individualEntity);
      bool ValidateSubmission(string masterId);
      IEnumerable<SubmissionIndividualEntity> GetSubmissionIndividualData(ChecklistModel MasterId);
      bool SubmissionIndividualDelete(string masterId);
    }
}
