using System.Collections.Generic;

namespace BusinessCenter.Data.Interface
{
    public interface ISubmissionSelfCertificationRepository
    {
        IEnumerable<SubmissionSelfCertification> GetSelfCertificationDetails();
        bool InsertUpdateSelfCertification(SubmissionSelfCertification submissionSelfCertification);
       bool DeleteSelfCertification(SubmissionSelfCertification submissionSelfCertification);

        IEnumerable<SubmissionSelfCertification> GetSelfCertificationOnMasterId(
            SubmissionSelfCertification submissionSelfCertification);
        bool UpdateSelfCertification(string masterId);
    }
}