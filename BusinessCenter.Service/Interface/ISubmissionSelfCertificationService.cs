using System.Collections.Generic;
using BusinessCenter.Data;

namespace BusinessCenter.Service.Interface
{
    public interface ISubmissionSelfCertificationService
    {
        IEnumerable<SubmissionSelfCertification> GetSelfCertificationDetails();
        IEnumerable<SubmissionSelfCertification> GetSelfCertificationOnMasterId(
            SubmissionSelfCertification submissionSelfCertification);

        bool InsertUpdateSelfCertification(SubmissionSelfCertification submissionSelfCertification);
        bool DeleteSelfCertification(SubmissionSelfCertification submissionSelfCertification);
        bool SelfCertificationUpdate(string masterId);
    }
}