using BusinessCenter.Data;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Service.Interface
{
    public interface IMasterCategoryPhysicalLocationService
    {
        IEnumerable<MasterCategoryPhysicalLocation> GetAllCategoryPhysicallocations();
        IEnumerable<MasterCategoryPhysicalLocation> FindCategoryPhysicallocationsById(SubmissionApplication submissionApplication);
        IEnumerable<SubmissionApplication> GetAllScreeningQuestions(SubmissionApplication submissionApplication);
        string InsertUpdatePhysicalLocation(PrimaryPhysicallocation primaryPhysicallocation);
        IEnumerable<MasterCategoryPhysicalLocation> FindCategoryPhysicallocationsById(string primaryCategoryId);
    }
}
