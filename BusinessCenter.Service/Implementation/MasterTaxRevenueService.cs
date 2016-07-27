using BusinessCenter.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Service.Implementation
{
    public class MasterTaxRevenueService : IMasterTaxRevenueService
    {
        protected IMasterTaxRevenueRepository _repository;

        public MasterTaxRevenueService(IMasterTaxRevenueRepository repo)
        {
            _repository = repo;
        }

         public string ValidateFEINNumber(SubmissionTaxRevenuEntity submissionTaxRevenuModel)
         {

             var commandata = _repository.ValidateFEINNumber(submissionTaxRevenuModel);
             return commandata;
         }
    }
}
