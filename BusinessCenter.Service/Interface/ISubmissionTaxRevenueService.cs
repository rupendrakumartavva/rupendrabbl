using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Common;
using BusinessCenter.Data;
namespace BusinessCenter.Service.Interface
{
   public interface ISubmissionTaxRevenueService
    {
       IEnumerable<SubmissionTaxRevenue> FindByTaxRevenueNumber(SubmissionTaxRevenuEntity submissionTaxRevenu);
       bool SubmissionTaxRevenuInsertUpdate(SubmissionTaxRevenuEntity submissionTaxRevenu);
       bool DeleteSubmissionTaxandRevenue(SubmissionTaxRevenuEntity submissionTaxRevenu);
       bool DeleteTaxandRevenue(string masterId);
       IEnumerable<SubmissionTaxRevenue> FindByID(string masterId);
       bool TaxAndRevenueUpdate(string masterId);
       //  bool TaxServiceStatus(string Masterid);
    }
}
