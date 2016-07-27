using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
    public interface ISubmissionCategoryRepository
    {
        IEnumerable<SubmissionCategory> AllSubmissionCategories();

        IEnumerable<SubmissionCategory> FindByID(SubmissionCategoryModel submissionCategoryModel);

        bool InsertPrimaryBbl(SubmissionApplication submissionApp);

        bool InsertSecondaryBbl(SubmissionApplication submissionApp);

        SubmissionApplication GetTotalFees(SubmissionApplication submissionApp);

        IEnumerable<SubmissionCategory> FindbyMaster(string masterid);

        ServiceChecklist ServiceCheckList(ServiceChecklist servicechecklist);

        bool InsertSubSubCagteogryBbl(SubmissionApplication submissionApp);

        SubmissionCategoryList SubmissionCategoryListWithStatus(SubmissionCategoryList categorylist, string masterid);

        decimal InsertRenewalData(RenewModel renewModel);

        bool Checkunits(string quantity);

        bool DeleteSubmissionCategory(string masterId);

        int Unitcount(string masterId);

        string CategoryCode(string categoryName);

        bool CheckPrimaryCategoryShowPdf(string categoryName);
    }
}