using BusinessCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Setup
{
  public  class SubmissionLicenseNumberCounterData
    {
      private readonly List<SubmissionLicenseNumberCounter> _entities;
        public bool IsInitialized;

        public void AddSubmissionLicenseCounterEntity(SubmissionLicenseNumberCounter obj)
        {
            _entities.Add(obj);
        }

        public List<SubmissionLicenseNumberCounter> SubmissionLicenseCounterEntitiesList
        {
            get { return _entities; }
        }


        public SubmissionLicenseNumberCounterData()
        {
            IsInitialized = true;
            _entities = new List<SubmissionLicenseNumberCounter>();

            AddSubmissionLicenseCounterEntity(new SubmissionLicenseNumberCounter()
            {
                CounterId = 1,
                Counter = 1,
                Sequence = 9,
                FiscalYear = 16,
                Type = "DAPP"
            });
            AddSubmissionLicenseCounterEntity(new SubmissionLicenseNumberCounter()
            {

                CounterId = 2,
                Counter = 1,
                Sequence = 9,
                FiscalYear = 15,
                Type = "DAPP"
            });
            AddSubmissionLicenseCounterEntity(new SubmissionLicenseNumberCounter()
            {

                CounterId = 3,
                Counter = 1,
                Sequence = 9,
                FiscalYear = 16,
                Type = "LAPP_IAPP"
            });
          


        }
    }
}
