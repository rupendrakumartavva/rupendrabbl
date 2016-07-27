using BusinessCenter.Data;
using BusinessCenter.Data.Interface;
using BusinessCenter.Service.Interface;
using System.Collections.Generic;
using System.Linq;

namespace BusinessCenter.Service.Implementation
{
    public class SecurityQuestionService : ISecurityQuestionsService
    {
        protected ISecurityRepository _repository;

        /// <summary>
        /// Initializes a new Instance of SecurityQuestionService
        /// </summary>
        /// <param name="repo"></param>
        public SecurityQuestionService(ISecurityRepository repo)
        {
            _repository = repo;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SecurityQuestion> GetAll()
        {
            //   var f = _repository.GetQuestions();
            return _repository.GetQuestions();
        }
    }
}