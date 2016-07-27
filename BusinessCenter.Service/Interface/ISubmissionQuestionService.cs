using BusinessCenter.Data;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Service.Interface
{
    public interface ISubmissionQuestionService
    {
       // IEnumerable<SubmissionQuestion> GetAllSubmissionQuestions();
        IEnumerable<SubmissionQuestion> FindSubmissionQuestionId(SubmissionQuestionModel submissionQuestionModel);
    }
}
