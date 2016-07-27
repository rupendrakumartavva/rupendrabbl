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
    public class MasterPrimaryCategoryService : IMasterPrimaryCategoryService
    {
        protected IMasterPrimaryCategoryRepository _repository;

        /// <summary>
        ///
        /// </summary>
        /// <param name="repo"></param>
        public MasterPrimaryCategoryService(IMasterPrimaryCategoryRepository repo)
        {
            _repository = repo;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MasterPrimaryCategory> GetAllPrimaryCategories()
        {
            var commandata = _repository.AllPrimaryCategories();
            return commandata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="submissionApplication"></param>
        /// <returns></returns>
        public IEnumerable<MasterPrimaryCategory> FindPrimaryCategoryById(SubmissionApplication submissionApplication)
        {
            var commondata = _repository.FindByID(submissionApplication).ToList();
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public IEnumerable<MasterPrimaryCategory> FindPrimaryCategoryById(string activityId)
        {
            var commondata = _repository.FindByID(activityId);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public IEnumerable<MasterPrimaryCategory> FindByPrimaryIdbasedonActivity(string activityId)
        {
            var commondata = _repository.FindByPrimaryIdbasedonActivity(activityId);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="primaryPhysicallocation"></param>
        /// <returns></returns>
        public string InsertUpdatePrimaryCategory(PrimaryPhysicallocation primaryPhysicallocation)
        {
            return _repository.InsertUpdatePrimaryCategory(primaryPhysicallocation);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cateogoryid"></param>
        /// <returns></returns>
        public IEnumerable<MasterPrimaryCategory> FindByCategoryID(string cateogoryid)
        {
            var commondata = _repository.FindByCategoryID(cateogoryid);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="primaryCatEntity"></param>
        /// <returns></returns>
        public bool DeletePrimaryCategory(PrimaryCategoryEntity primaryCatEntity)
        {
            return _repository.DeletePrimaryCategory(primaryCatEntity);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="submissionApplication"></param>
        /// <returns></returns>
        public IEnumerable<MasterPrimaryCategory> ActiveFindById(SubmissionApplication submissionApplication)
        {
            return _repository.ActiveFindById(submissionApplication);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public IEnumerable<string> FindByPrimaryCategory(string term)
        {
            return _repository.FindByPrimaryCategory(term);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="primayName"></param>
        /// <returns></returns>
        public IEnumerable<string> FindByPrimaryName(string primayName)
        {
            return _repository.FindByPrimaryName(primayName);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public IEnumerable<MasterPrimaryCategory> SecondaryEndorsement(string category)
        {
            return _repository.SecondaryEndorsement(category);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="primaryPhysicallocation"></param>
        /// <returns></returns>
        public string UpdatePrimaryCategory(PrimaryPhysicallocation primaryPhysicallocation)
        {
            var commondata = _repository.UpdatePrimaryCategory(primaryPhysicallocation);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="primaryId"></param>
        /// <returns></returns>
        public IEnumerable<MasterPrimaryCategory> FindByCategoryIDBasedonPrimaryId(string primaryId)
        {
            var commondata = _repository.FindByCategoryIDBasedonPrimaryId(primaryId).ToList();
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="primaryCatEntity"></param>
        /// <returns></returns>
        public int ActiveSecondary(SlCategoryEntity primaryCatEntity)
        {
            var commondata = _repository.ActiveSecondary(primaryCatEntity);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="primaryId"></param>
        /// <returns></returns>
        public List<SecondaryCategory> SecondaryCategoriesList(string primaryId)
        {
            var commondata = _repository.SecondaryCategoriesList(primaryId).ToList();
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="submissionApplication"></param>
        /// <returns></returns>
        public IEnumerable<MasterSecondaryLicenseCategory> FindSecondaryCategoryById(SubmissionApplication submissionApplication)
        {
            var commondata = _repository.FindBySecondaryPrimaryID(submissionApplication).Where(x => x.SecondaryLicenseCategory.ToUpper().ToString() != "NA");
            return commondata;
        }
    }
}