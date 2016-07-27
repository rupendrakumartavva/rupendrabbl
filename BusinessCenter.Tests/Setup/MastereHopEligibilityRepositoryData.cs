using BusinessCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Setup
{
  public  class MastereHopEligibilityRepositoryData
    {
       private readonly List<MastereHOPEligibility> _entities;
        public bool IsInitialized;

        public void AddMastereHOPEligibilityEntity(MastereHOPEligibility obj)
        {
            _entities.Add(obj);
        }

        public List<MastereHOPEligibility> MastereHOPEligibilityEntitiesList
        {
            get { return _entities; }
        }


        public MastereHopEligibilityRepositoryData()
        {
            IsInitialized = true;
            _entities = new List<MastereHOPEligibility>();

            AddMastereHOPEligibilityEntity(new MastereHOPEligibility()
            {
                Id = 1,
                Name = "The Home Occupation is located in my primary residence."
            });
            AddMastereHOPEligibilityEntity(new MastereHOPEligibility()
            {
                Id = 2,
                Name = "No more than the larger of 25% of the floor area of the home or 250 sq. ft. will be used for the home occupation. 60% of floor area may be allowed for a home artist studio."
            });
            AddMastereHOPEligibilityEntity(new MastereHOPEligibility()
            {
                Id = 3,
                Name="The operation of my business does not produce a level of noise that exceeds the level that is normally associated with a residential area of the District's noise regulations."

            });
            AddMastereHOPEligibilityEntity(new MastereHOPEligibility()
            {
                Id = 4,
                Name="No more than one person who is not a resident of the home will be engaged or employed in my home."

            });

          


        }
    }
}
