using BusinessCenter.Data.Model;
using System.Collections.Generic;

namespace BusinessCenter.Data.Interface
{
    public interface IMasterSubCategoryRepository
    {
        IEnumerable<MasterSubCategory> FindByID(string enitityid);
        IEnumerable<MasterSubCategory> GetSuperSubCategory(SubmissionApplication submissionApplication);
        IEnumerable<MasterSubCategory> SubCategories(string primaryName);
        int InsertUpdateSubCategory(SubCategoryEntity subCategoryEntity);
        bool DeleteSubCategory(SubCategoryEntity subCategoryEntity);
        IEnumerable<MasterSubCategory> FindBySubCategoriesBasedonPrimaryName(string primaryName);
        IEnumerable<MasterSubCategory> FindBySubCategoryBasedonSubcatId(string enitityid);
        
    }
}