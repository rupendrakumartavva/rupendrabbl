using BusinessCenter.Data;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Service.Interface
{
    public interface IMasterPrimaryCategoryService
    {
        IEnumerable<MasterPrimaryCategory> GetAllPrimaryCategories();
        IEnumerable<MasterPrimaryCategory> FindPrimaryCategoryById(SubmissionApplication submissionApplication);
        IEnumerable<MasterPrimaryCategory> FindPrimaryCategoryById(string activityId);
        string InsertUpdatePrimaryCategory(PrimaryPhysicallocation primaryPhysicallocation);
        IEnumerable<MasterPrimaryCategory> FindByCategoryID(string cateogoryid);
        bool DeletePrimaryCategory(PrimaryCategoryEntity primaryCatEntity);
        IEnumerable<MasterPrimaryCategory> ActiveFindById(SubmissionApplication submissionApplication);
        IEnumerable<string> FindByPrimaryCategory(string term);
        IEnumerable<MasterPrimaryCategory> SecondaryEndorsement(string category);
        IEnumerable<string> FindByPrimaryName(string primayName);
        string UpdatePrimaryCategory(PrimaryPhysicallocation primaryPhysicallocation);
        IEnumerable<MasterPrimaryCategory> FindByPrimaryIdbasedonActivity(string activityId);
        IEnumerable<MasterPrimaryCategory> FindByCategoryIDBasedonPrimaryId(string primaryId);

        int ActiveSecondary(SlCategoryEntity primaryCatEntity);

        List<SecondaryCategory> SecondaryCategoriesList(string primaryId);

        IEnumerable<MasterSecondaryLicenseCategory> FindSecondaryCategoryById(SubmissionApplication submissionApplication);

    }
}
