using BusinessCenter.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Service.Interface
{
    public interface IMasterTaxRevenueService
    {
        string ValidateFEINNumber(SubmissionTaxRevenuEntity submissionTaxRevenuModel);
    }
}
