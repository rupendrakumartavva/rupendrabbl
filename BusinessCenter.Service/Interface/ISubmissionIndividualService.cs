using System.Collections.Generic;
using BusinessCenter.Common;

namespace BusinessCenter.Service.Interface
{
    public interface ISubmissionIndividualService
    {
        int InsertUpdateSubmissionIndividual(SubmissionIndividualEntity individualEntity);
        bool ValidateSubmission(string masterId);

        IEnumerable<SubmissionIndividualEntity> GetSubmissionIndividualData(ChecklistModel MasterId);
        bool SubmissionIndividualDelete(string masterId);
    }
}