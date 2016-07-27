using BusinessCenter.Data;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using BusinessCenter.Service.Interface;
using System.Collections.Generic;

namespace BusinessCenter.Service.Implementation
{
    public class MasterSubCategoryService : IMasterSubCategoryService
    {
        protected IMasterSubCategoryRepository MasterSubCategoryrepo;

        /// <summary>
        ///
        /// </summary>
        /// <param name="masterSubCategoryrepo"></param>
        public MasterSubCategoryService(IMasterSubCategoryRepository masterSubCategoryrepo)
        {
            MasterSubCategoryrepo = masterSubCategoryrepo;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="submissionApplication"></param>
        /// <returns></returns>
        public IEnumerable<MasterSubCategory> GetSuperSubCategory(SubmissionApplication submissionApplication)
        {
            return MasterSubCategoryrepo.GetSuperSubCategory(submissionApplication);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="enitityid"></param>
        /// <returns></returns>
        public IEnumerable<MasterSubCategory> FindBySubCategoryID(string enitityid)
        {
            return MasterSubCategoryrepo.FindByID(enitityid);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="primaryName"></param>
        /// <returns></returns>
        public IEnumerable<MasterSubCategory> SubCategories(string primaryName)
        {
            return MasterSubCategoryrepo.SubCategories(primaryName);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="subCategoryEntity"></param>
        /// <returns></returns>
        public int InsertUpdateSubCategory(SubCategoryEntity subCategoryEntity)
        {
            return MasterSubCategoryrepo.InsertUpdateSubCategory(subCategoryEntity);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="subCategoryEntity"></param>
        /// <returns></returns>
        public bool DeleteSubCategory(SubCategoryEntity subCategoryEntity)
        {
            return MasterSubCategoryrepo.DeleteSubCategory(subCategoryEntity);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="primaryName"></param>
        /// <returns></returns>
        public IEnumerable<MasterSubCategory> FindBySubCategoriesBasedonPrimaryName(string primaryName)
        {
            return MasterSubCategoryrepo.FindBySubCategoriesBasedonPrimaryName(primaryName);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="primaryName"></param>
        /// <returns></returns>
        public IEnumerable<MasterSubCategory> FindBySubCategoryBasedonSubcatId(string primaryName)
        {
            return MasterSubCategoryrepo.FindBySubCategoryBasedonSubcatId(primaryName);
        }
    }
}