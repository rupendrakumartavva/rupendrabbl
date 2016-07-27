using BusinessCenter.Data;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using BusinessCenter.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Service.Implementation
{
    public class SubmissionQuestionService : ISubmissionQuestionService
    {
        protected ISubmissionQuestionRepository _repository;

        /// <summary>
        ///
        /// </summary>
        /// <param name="repo"></param>
        public SubmissionQuestionService(ISubmissionQuestionRepository repo)
        {
            _repository = repo;
        }

        //public IEnumerable<SubmissionQuestion> GetAllSubmissionQuestions()
        //{
        //    var commandata = _repository.AllSubmissionQuestions();
        //    return commandata;
        //}
        /// <summary>
        ///
        /// </summary>
        /// <param name="submissionQuestionModel"></param>
        /// <returns></returns>
        public IEnumerable<SubmissionQuestion> FindSubmissionQuestionId(SubmissionQuestionModel submissionQuestionModel)
        {
            var commondata = _repository.FindByID(submissionQuestionModel);
            return commondata;
        }
    }
}