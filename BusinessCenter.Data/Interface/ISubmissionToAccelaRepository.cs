using BusinessCenter.Common;
using System.Collections.Generic;

namespace BusinessCenter.Data.Interface
{
    public interface ISubmissionToAccelaRepository
    {
        bool AddSubmissionToAccelaRepository(SubmissiontoAccelaEntity submissiontoAccelaEntity);
        IEnumerable<SubmissiontoAccela> GetSubmissionsToAccela();
    }
}