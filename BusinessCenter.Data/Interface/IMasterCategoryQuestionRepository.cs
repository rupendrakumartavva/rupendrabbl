using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
    public interface IMasterCategoryQuestionRepository
    {
        IEnumerable<MasterCategoryQuestion> FindByID(string category, string unit);
        IEnumerable<MasterCategoryQuestion> FindBySecondaryName(string secondaryname);
   //     int InsertUpdateCategoryQuestions(CategoryQuestionModel categoryQuestionModel);
        bool InsertUpdateCategoryName(PrimaryPhysicallocation categoryQuestionModel);
    }
}
