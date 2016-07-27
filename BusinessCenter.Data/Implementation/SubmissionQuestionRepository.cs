using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessCenter.Data.Implementation
{
    public class SubmissionQuestionRepository : GenericRepository<SubmissionQuestion>, ISubmissionQuestionRepository
    {
        public SubmissionQuestionRepository(IUnitOfWork context) : base(context)
        {
        }
        /// <summary>
        /// This method is used to get particular submission question data based on submission question id
        /// </summary>
        /// <param name="submissionQuestionModel"></param>
        /// <returns>Return submission question</returns>
        public IEnumerable<SubmissionQuestion> FindByID(SubmissionQuestionModel submissionQuestionModel)
        {
            var submissionQuestions =FindBy(x => x.SubmQuestionsId == submissionQuestionModel.SubmQuestionsId).ToList();
            return submissionQuestions;
        }
        /// <summary>
        /// This method is used to get particular submission questions based on master id
        /// </summary>
        /// <param name="masterid"></param>
        /// <returns>Return submission question</returns>
        public IEnumerable<SubmissionQuestion> FindByMasterID(string masterid)
        {
            var submissionQuestions = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == masterid).ToList();
            return submissionQuestions;
        }
        /// <summary>
        /// This method is used to insert submission question based on submission application
        /// </summary>
        /// <param name="submissionApp"></param>
        /// <returns>Return bool value</returns>
        public bool InsertQuestionBbl(SubmissionApplication submissionApp)
        {
            bool result;
            try
            {
                foreach (var itemquestion in submissionApp.SubQuestion)
                { 
                SubmissionQuestion subquestion = new SubmissionQuestion();
                subquestion.MasterId = submissionApp.MasterId;
                subquestion.Question = itemquestion.Question;
                subquestion.Answer = itemquestion.Answer;
                subquestion.OptionType = itemquestion.Type;
                Add(subquestion);
                Save();                
                }
                result = true;
            }
            catch (Exception)
            { result = false; }
            return result;
        }
    }
}
