using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
    public interface IMasterCategoryPhysicalLocationRepository
    {
        IEnumerable<MasterCategoryPhysicalLocation> AllCategoryPhysicallocations();
        IEnumerable<MasterCategoryPhysicalLocation> FindByID(SubmissionApplication submissionApplication);
        IEnumerable<SubmissionApplication> AllScreeningQuestions(SubmissionApplication submissionApplication);
        IEnumerable<MasterCategoryPhysicalLocation> FindCategoryID(string Categoryid);
        string InsertUpdatePhysicalLocation(PrimaryPhysicallocation primaryPhysicallocation);
    }
}
