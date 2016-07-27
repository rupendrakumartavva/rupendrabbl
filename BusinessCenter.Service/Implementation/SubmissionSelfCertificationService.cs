using BusinessCenter.Data;
using BusinessCenter.Data.Interface;
using BusinessCenter.Service.Interface;
using System.Collections.Generic;

namespace BusinessCenter.Service.Implementation
{
    public class SubmissionSelfCertificationService : ISubmissionSelfCertificationService
    {
        public readonly ISubmissionSelfCertificationRepository _SubmissionSelfCertificationRepository;

        /// <summary>
        ///
        /// </summary>
        /// <param name="submissionSelfCertificationRepository"></param>
        public SubmissionSelfCertificationService(ISubmissionSelfCertificationRepository submissionSelfCertificationRepository)
        {
            _SubmissionSelfCertificationRepository = submissionSelfCertificationRepository;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SubmissionSelfCertification> GetSelfCertificationDetails()
        {
            return _SubmissionSelfCertificationRepository.GetSelfCertificationDetails();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="submissionSelfCertification"></param>
        /// <returns></returns>
        public IEnumerable<SubmissionSelfCertification> GetSelfCertificationOnMasterId(SubmissionSelfCertification submissionSelfCertification)
        {
            return _SubmissionSelfCertificationRepository.GetSelfCertificationOnMasterId(submissionSelfCertification);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="submissionSelfCertification"></param>
        /// <returns></returns>
        public bool InsertUpdateSelfCertification(SubmissionSelfCertification submissionSelfCertification)
        {
            return _SubmissionSelfCertificationRepository.InsertUpdateSelfCertification(submissionSelfCertification);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="submissionSelfCertification"></param>
        /// <returns></returns>
        public bool DeleteSelfCertification(SubmissionSelfCertification submissionSelfCertification)
        {
            return _SubmissionSelfCertificationRepository.DeleteSelfCertification(submissionSelfCertification);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
        public bool SelfCertificationUpdate(string masterId)
        {
            return _SubmissionSelfCertificationRepository.UpdateSelfCertification(masterId);
        }
    }
}