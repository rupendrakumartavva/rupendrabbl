using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessCenter.Data.Implementation
{
    public class MasterCategoryQuestionRepository : GenericRepository<MasterCategoryQuestion>, IMasterCategoryQuestionRepository
    {
        public MasterCategoryQuestionRepository(IUnitOfWork context)
            : base(context)
        {
        }
        /// <summary>
        /// This method is used to retrive the Category Questions based on category and unit.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="unit"></param>
        /// <returns> list of questions data</returns>
        public IEnumerable<MasterCategoryQuestion> FindByID(string category, string unit)
        {
            try
            {
                var categoryQuestion = FindBy(x =>x.CategoryName.Replace(System.Environment.NewLine, "").ToString().Trim() == category &&
                     x.Quantity.Replace(System.Environment.NewLine, "").ToString().Trim() == unit);
                return categoryQuestion;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// This method is used to retrive the Category Question based in the Category Name
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns>list of questions data</returns>
        public IEnumerable<MasterCategoryQuestion> FindBySecondaryName(string categoryName)
        {
            try
            {
                var categoryQuestion = FindBy(x => x.CategoryName.Replace(System.Environment.NewLine, "").ToString().Trim() == categoryName.Trim());
                return categoryQuestion;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        
        /// <summary>
        /// This method is used to update the existing category names with new category name based on unit and old category name
        /// </summary>
        /// <param name="categoryQuestionModel"></param>
        /// <returns>return the bool status</returns>
        public bool InsertUpdateCategoryName(PrimaryPhysicallocation categoryQuestionModel)
        {
            bool status = false;
            try
            {
                categoryQuestionModel.OldCategoryName =categoryQuestionModel.OldCategoryName.ToUpper().Trim()=="NA"?"":categoryQuestionModel.OldCategoryName.ToUpper().Trim();
                categoryQuestionModel.OldUnitOne = categoryQuestionModel.OldUnitOne.ToUpper().Trim() == "NA" ? "" : categoryQuestionModel.OldUnitOne.ToUpper().Trim();
                if (categoryQuestionModel.OldUnitOne != "")
                {
                    var categoryQuestionone =
                        FindBy(
                            x =>
                                x.CategoryName.Replace(System.Environment.NewLine, "").ToUpper().Trim() == categoryQuestionModel.OldCategoryName.ToUpper() &&
                                x.Quantity.Replace(System.Environment.NewLine, "").ToUpper().Trim() == categoryQuestionModel.OldUnitOne.ToUpper())
                            .ToList();
                    if (categoryQuestionone.Count == 0)
                    {
                        MasterCategoryQuestion question = new MasterCategoryQuestion();
                        question.CategoryName = categoryQuestionModel.Description.Trim();
                        question.Quantity = categoryQuestionModel.UnitOne.Trim();
                        question.UserQuestion = categoryQuestionModel.UserQuestion1.Trim();
                        Add(question);
                        Save();
                        status = true;
                    }
                    else
                    {
                        var question = categoryQuestionone.SingleOrDefault();
                        question.CategoryName = categoryQuestionModel.Description.Trim();
                        question.Quantity = categoryQuestionModel.UnitOne.Trim();
                        question.UserQuestion = categoryQuestionModel.UserQuestion1.Trim();
                        Save();
                        status = true;
                    }
                }
                categoryQuestionModel.OldUnitTwo = categoryQuestionModel.OldUnitTwo.ToUpper().Trim() == "NA" ? "" : categoryQuestionModel.OldUnitTwo.ToUpper().Trim();
                if (categoryQuestionModel.OldUnitTwo != "")
                    {
                        var categoryQuestiontwo =
                            FindBy(
                                x =>
                                    x.CategoryName.Replace(System.Environment.NewLine, "").ToUpper().Trim() ==
                                     categoryQuestionModel.OldCategoryName.ToUpper() &&
                                    x.Quantity.Replace(System.Environment.NewLine, "").ToUpper().Trim() == categoryQuestionModel.OldUnitTwo.ToUpper())
                                .ToList();
                        if (categoryQuestiontwo.Count == 0)
                        {
                            MasterCategoryQuestion questions = new MasterCategoryQuestion();
                            questions.CategoryName = categoryQuestionModel.Description.Trim();
                            questions.Quantity = categoryQuestionModel.UnitOne.Trim();
                            questions.UserQuestion = categoryQuestionModel.UserQuestion1.Trim();
                            Add(questions);
                            Save();
                            status = true;
                        }
                        else
                        {
                            var question = categoryQuestiontwo.SingleOrDefault();
                            question.CategoryName = categoryQuestionModel.Description.Trim();
                            question.Quantity = categoryQuestionModel.UnitOne.Trim();
                            question.UserQuestion = categoryQuestionModel.UserQuestion1.Trim();
                            Update(question, question.MasterCategoryQuestionId);
                            Save();
                            status = true;
                        }
                    }
                }
            
            catch (Exception)
            {

                status = false;
            }
            
            return
            status;
        }
    }
}
