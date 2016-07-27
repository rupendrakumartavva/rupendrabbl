using BusinessCenter.Data;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Service.Interface
{
    public interface IMasterSecondaryLicenseCategoryService
    {
   //     IEnumerable<MasterSecondaryLicenseCategory> GetAllSecondaryCategories();
        IEnumerable<MasterSecondaryLicenseCategory> FindSecondaryCategoryById(SubmissionApplication submissionApplication);
        IEnumerable<MasterSecondaryLicenseCategory> FindSecondaryCategoryById(string primaryId);
        int InsertUpdateSlCategory(SlCategoryEntity primaryCatEntity);
        bool DeleteSecondaryCategory(SlCategoryEntity slCategoryEntity);
        IEnumerable<MasterSecondaryLicenseCategory> FindSecondaryId(string primaryId);
        IEnumerable<MasterSecondaryLicenseCategory> FindBySecondaryBasedonPrimaryId(string primaryId);
        IEnumerable<MasterSecondaryLicenseCategory> FindBySecondaryBasedonSecondaryId(string secondaryID);
    }
}
