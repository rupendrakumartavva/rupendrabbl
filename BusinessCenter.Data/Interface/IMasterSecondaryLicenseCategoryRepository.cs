using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
    public interface IMasterSecondaryLicenseCategoryRepository
    {
        //IEnumerable<MasterSecondaryLicenseCategory> AllSecondaryLicenseCategories();
        IEnumerable<MasterSecondaryLicenseCategory> FindByID(SubmissionApplication submissionApplication);
        IEnumerable<MasterSecondaryLicenseCategory> FindByID(string primaryId);
        IEnumerable<MasterSecondaryLicenseCategory> FindByPrimaryId(string primaryId);
        IEnumerable<MasterSecondaryLicenseCategory> FindBySecondaryID(string secondaryID);
        int InsertUpdateSlCategory(SlCategoryEntity primaryCatEntity);
        bool DeleteSecondaryCategory(SlCategoryEntity slCategoryEntity);
        bool DeleteSecondaryByPrimary(string primaryId);
        IEnumerable<MasterSecondaryLicenseCategory> FindBySecondaryName(string secondaryName);
         bool  UpdateCategoryName(string oldname, string newName);
        IEnumerable<MasterSecondaryLicenseCategory> FindBySecondaryBasedonPrimaryId(string primaryId);
        IEnumerable<MasterSecondaryLicenseCategory> FindBySecondaryBasedonSecondaryId(string secondaryId);
        IEnumerable<MasterSecondaryLicenseCategory> FindByRenewSecondaryBasedonPrimary(string categoryName,
            string primaryId);
    }
}
