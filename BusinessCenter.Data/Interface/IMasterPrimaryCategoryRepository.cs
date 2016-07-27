using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
    public interface IMasterPrimaryCategoryRepository
    {
        IEnumerable<MasterPrimaryCategory> AllPrimaryCategories();

        IEnumerable<MasterPrimaryCategory> FindByCategoryID(string cateogoryid);

        IEnumerable<MasterPrimaryCategory> FindByID(SubmissionApplication submissionApplication);
        IEnumerable<MasterPrimaryCategory> FindByID(string activityId);
        IEnumerable<MasterPrimaryCategory> FindByprimaryID(SubmissionApplication submissionApplication);
        IEnumerable<MasterPrimaryCategory> SecondaryEndorsement(string category);
        string InsertUpdatePrimaryCategory(PrimaryPhysicallocation primaryPhysicallocation);
        bool DeletePrimaryCategory(PrimaryCategoryEntity primaryCatEntity);
      //  bool DeletePrimaryByActivity(string businessActivityId);
        IEnumerable<MasterPrimaryCategory> ActiveFindById(SubmissionApplication submissionApplication);
        IEnumerable<string> FindByPrimaryCategory(string term);
        IEnumerable<string> FindByPrimaryName(string primayName);
        //bool InsertUpdateFee(PrimaryPhysicallocation primaryPhysicallocation);
        string UpdatePrimaryCategory(PrimaryPhysicallocation primaryPhysicallocation);
        IEnumerable<MasterPrimaryCategory> FindByPrimaryIdbasedonActivity(string activityId);
        IEnumerable<MasterPrimaryCategory> FindByCategoryIDBasedonPrimaryId(string primaryId);
        int ActiveSecondary(SlCategoryEntity primaryCatEntity);
        List<SecondaryCategory> SecondaryCategoriesList(string primaryId);
        IEnumerable<MasterSecondaryLicenseCategory> FindBySecondaryPrimaryID(SubmissionApplication submissionApplication);

    }
}
