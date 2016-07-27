using BusinessCenter.Data;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Service.Interface
{
    public interface ISubmissionCategoryService
    {
        IEnumerable<SubmissionCategory> GetAllSubmissionCategories();
        IEnumerable<SubmissionCategory> FindBySubmissionCategoryId(SubmissionCategoryModel submissionCategoryModel);
        SubmissionApplication GetTotalFees(SubmissionApplication submissionApp);
        ServiceChecklist ServiceCheckList(ServiceChecklist servicechecklist);
        bool Checkunits(string quantity);
    }
}
