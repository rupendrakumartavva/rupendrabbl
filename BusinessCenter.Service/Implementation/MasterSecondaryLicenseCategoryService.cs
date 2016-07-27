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
    public class MasterSecondaryLicenseCategoryService : IMasterSecondaryLicenseCategoryService
    {
        protected IMasterSecondaryLicenseCategoryRepository _repository;

        /// <summary>
        ///
        /// </summary>
        /// <param name="repo"></param>
        public MasterSecondaryLicenseCategoryService(IMasterSecondaryLicenseCategoryRepository repo)
        {
            _repository = repo;
        }

        //public IEnumerable<MasterSecondaryLicenseCategory> GetAllSecondaryCategories()
        //{
        //    var commondata = _repository.AllSecondaryLicenseCategories();
        //    return commondata;
        //}
        /// <summary>
        ///
        /// </summary>
        /// <param name="submissionApplication"></param>
        /// <returns></returns>
        public IEnumerable<MasterSecondaryLicenseCategory> FindSecondaryCategoryById(SubmissionApplication submissionApplication)
        {
            var commondata = _repository.FindByID(submissionApplication).Where(x => x.SecondaryLicenseCategory.ToUpper().ToString() != "NA");
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="primaryId"></param>
        /// <returns></returns>
        public IEnumerable<MasterSecondaryLicenseCategory> FindSecondaryCategoryById(string primaryId)
        {
            var commondata = _repository.FindByPrimaryId(primaryId);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="primaryCatEntity"></param>
        /// <returns></returns>
        public int InsertUpdateSlCategory(SlCategoryEntity primaryCatEntity)
        {
            return _repository.InsertUpdateSlCategory(primaryCatEntity);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="slCategoryEntity"></param>
        /// <returns></returns>
        public bool DeleteSecondaryCategory(SlCategoryEntity slCategoryEntity)
        {
            return _repository.DeleteSecondaryCategory(slCategoryEntity);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="primaryId"></param>
        /// <returns></returns>
        public IEnumerable<MasterSecondaryLicenseCategory> FindSecondaryId(string primaryId)
        {
            var commondata = _repository.FindBySecondaryID(primaryId);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="primaryId"></param>
        /// <returns></returns>
        public IEnumerable<MasterSecondaryLicenseCategory> FindBySecondaryBasedonPrimaryId(string primaryId)
        {
            var commondata = _repository.FindBySecondaryBasedonPrimaryId(primaryId);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="secondaryID"></param>
        /// <returns></returns>
        public IEnumerable<MasterSecondaryLicenseCategory> FindBySecondaryBasedonSecondaryId(string secondaryID)
        {
            var commondata = _repository.FindBySecondaryBasedonSecondaryId(secondaryID);
            return commondata;
        }
    }
}