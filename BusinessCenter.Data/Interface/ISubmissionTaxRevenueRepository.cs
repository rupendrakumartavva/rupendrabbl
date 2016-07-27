using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Common;

namespace BusinessCenter.Data.Interface
{
    public interface ISubmissionTaxRevenueRepository
    {
        IEnumerable<SubmissionTaxRevenue> FindByTaxRevenueNumber(SubmissionTaxRevenuEntity submissionTaxRevenu);
        bool SubmissionTaxRevenuInsertUpdate(SubmissionTaxRevenuEntity submissionTaxRevenu);
        IEnumerable<SubmissionTaxRevenue> FindByID(string masterId);
        bool DeleteSubmissionTaxandRevenue(string masterId);
        bool DeleteTaxandRevenue(string masterId);
        bool UpdateTaxAndRevenue(string masterId);
    }
}
