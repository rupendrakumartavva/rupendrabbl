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
    public class SubmissionCategoryService : ISubmissionCategoryService
    {
        protected ISubmissionCategoryRepository _repository;

        /// <summary>
        ///
        /// </summary>
        /// <param name="repo"></param>
        public SubmissionCategoryService(ISubmissionCategoryRepository repo)
        {
            _repository = repo;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SubmissionCategory> GetAllSubmissionCategories()
        {
            var commondata = _repository.AllSubmissionCategories();
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="submissionCategoryModel"></param>
        /// <returns></returns>
        public IEnumerable<SubmissionCategory> FindBySubmissionCategoryId(SubmissionCategoryModel submissionCategoryModel)
        {
            var commondata = _repository.FindByID(submissionCategoryModel);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="submissionApp"></param>
        /// <returns></returns>
        public SubmissionApplication GetTotalFees(SubmissionApplication submissionApp)
        {
            var commondata = _repository.GetTotalFees(submissionApp);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="servicechecklist"></param>
        /// <returns></returns>
        public ServiceChecklist ServiceCheckList(ServiceChecklist servicechecklist)
        {
            var commondata = _repository.ServiceCheckList(servicechecklist);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public bool Checkunits(string quantity)
        {
            var commondata = _repository.Checkunits(quantity);
            return commondata;
        }
    }
}