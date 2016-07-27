using BusinessCenter.Common;
using BusinessCenter.Data;
using BusinessCenter.Data.Interface;
using BusinessCenter.Service.Interface;
using System.Collections.Generic;

namespace BusinessCenter.Service.Implementation
{
    public class SubmissionToAccelaService : ISubmissionToAccelaService
    {
        protected ISubmissionToAccelaRepository _repository;

        /// <summary>
        ///
        /// </summary>
        /// <param name="repo"></param>
        public SubmissionToAccelaService(ISubmissionToAccelaRepository repo)
        {
            _repository = repo;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SubmissiontoAccela> SubmissionsAccelaDetails()
        {
            var submissions = _repository.GetSubmissionsToAccela();
            return submissions;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="submissiontoAccelaEntity"></param>
        /// <returns></returns>
        public bool AddSubmissionToAccelaRepository(SubmissiontoAccelaEntity submissiontoAccelaEntity)
        {
            return _repository.AddSubmissionToAccelaRepository(submissiontoAccelaEntity);
        }
    }
}