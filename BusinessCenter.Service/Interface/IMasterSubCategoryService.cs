using System.Collections.Generic;
using BusinessCenter.Data;
using BusinessCenter.Data.Model;

namespace BusinessCenter.Service.Interface
{
    public interface IMasterSubCategoryService
    {
        IEnumerable<MasterSubCategory> GetSuperSubCategory(SubmissionApplication submissionApplication);
        IEnumerable<MasterSubCategory> FindBySubCategoryID(string enitityid);
        IEnumerable<MasterSubCategory> SubCategories(string primaryName);
        int InsertUpdateSubCategory(SubCategoryEntity subCategoryEntity);
        bool DeleteSubCategory(SubCategoryEntity subCategoryEntity);
        IEnumerable<MasterSubCategory> FindBySubCategoriesBasedonPrimaryName(string primaryName);
        IEnumerable<MasterSubCategory> FindBySubCategoryBasedonSubcatId(string primaryName);
    }
}