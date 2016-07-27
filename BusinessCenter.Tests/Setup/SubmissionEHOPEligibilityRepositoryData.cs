using BusinessCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Setup
{
  public  class SubmissionEHOPEligibilityRepositoryData
    {
       private readonly List<SubmissionEHOPEligibility> _entities;
        public bool IsInitialized;

        public void AddSubmissionEHOPEligibilityEntity(SubmissionEHOPEligibility obj)
        {
            _entities.Add(obj);
        }

        public List<SubmissionEHOPEligibility> SubmissionEHOPEligibilityEntitiesList
        {
            get { return _entities; }
        }


        public SubmissionEHOPEligibilityRepositoryData()
        {
            IsInitialized = true;
            _entities = new List<SubmissionEHOPEligibility>();

            AddSubmissionEHOPEligibilityEntity(new SubmissionEHOPEligibility()
            {
                SubEhopId =1,
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                MasterEhopId = 1,
                TypeId=1,
                CreatedDate=System.DateTime.Now,
                UpdatedDate=System.DateTime.Now,
                CreatedBy = "2AC53E53-87D0-468F-9628-4AEBA1120613"
            });
            AddSubmissionEHOPEligibilityEntity(new SubmissionEHOPEligibility()
            {
                SubEhopId = 2,
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                MasterEhopId = 2,
                TypeId = 1,
                CreatedDate = System.DateTime.Now,
                UpdatedDate = System.DateTime.Now,
                CreatedBy = "2AC53E53-87D0-468F-9628-4AEBA1120613"
            });
            AddSubmissionEHOPEligibilityEntity(new SubmissionEHOPEligibility()
            {
                SubEhopId = 3,
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                MasterEhopId = 3,
                TypeId = 1,
                CreatedDate = System.DateTime.Now,
                UpdatedDate = System.DateTime.Now,
                CreatedBy = "2AC53E53-87D0-468F-9628-4AEBA1120613"
            });
            AddSubmissionEHOPEligibilityEntity(new SubmissionEHOPEligibility()
            {
                SubEhopId = 4,
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                MasterEhopId = 4,
                TypeId = 1,
                CreatedDate = System.DateTime.Now,
                UpdatedDate = System.DateTime.Now,
                CreatedBy = "2AC53E53-87D0-468F-9628-4AEBA1120613"
            });

           
          

        }

    }
}
