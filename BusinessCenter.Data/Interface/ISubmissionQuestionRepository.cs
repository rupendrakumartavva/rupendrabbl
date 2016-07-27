using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
    public interface ISubmissionQuestionRepository
    {
       // IEnumerable<SubmissionQuestion> AllSubmissionQuestions();
        IEnumerable<SubmissionQuestion> FindByID(SubmissionQuestionModel submissionQuestionModel);
        bool InsertQuestionBbl(SubmissionApplication submissionApp);
        IEnumerable<SubmissionQuestion> FindByMasterID(string masterid);
    }
}
