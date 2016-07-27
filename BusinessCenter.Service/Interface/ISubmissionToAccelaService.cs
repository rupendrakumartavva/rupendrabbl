using BusinessCenter.Common;
using BusinessCenter.Data;
using System.Collections.Generic;

namespace BusinessCenter.Service.Interface
{
    public interface ISubmissionToAccelaService
    {
        bool AddSubmissionToAccelaRepository(SubmissiontoAccelaEntity submissiontoAccelaEntity);
        IEnumerable<SubmissiontoAccela> SubmissionsAccelaDetails();
    }
}