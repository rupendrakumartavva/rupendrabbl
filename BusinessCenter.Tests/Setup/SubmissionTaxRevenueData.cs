using System.Collections.Generic;
using BusinessCenter.Data;

namespace BusinessCenter.Tests.Setup
{
    public class SubmissionTaxRevenueData
    {
        private readonly List<SubmissionTaxRevenue> _entities;
        public bool IsInitialized;

        public void AddSubmissionTaxRevenueEntity(SubmissionTaxRevenue obj)
        {
            _entities.Add(obj);
        }

        public List<SubmissionTaxRevenue> SubmissionTaxRevenueEntitiesList
        {
            get { return _entities; }
        }

        public SubmissionTaxRevenueData()
        {
            IsInitialized = true;
            _entities = new List<SubmissionTaxRevenue>();

            AddSubmissionTaxRevenueEntity(new SubmissionTaxRevenue()
            {
               SubmissionTaxRevenueId=1,
               MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
               TaxRevenueNumber = "11-1111111",
               TaxRevenueType = "FEIN",
               FullName="Code IT INDIA",
               BusinessOwnerRoles = "Owner",
               CreatdedDate=System.DateTime.Now,
               UpdatedDate=System.DateTime.Now
            });
           
        }
    }
}