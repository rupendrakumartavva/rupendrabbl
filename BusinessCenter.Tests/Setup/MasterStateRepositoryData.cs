using BusinessCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Setup
{
 public class MasterStateRepositoryData
    {
       private readonly List<MasterState> _entities;
        public bool IsInitialized;

        public void AddMasterStateEntity(MasterState obj)
        {
            _entities.Add(obj);
        }

        public List<MasterState> MasterStateEntitiesList
        {
            get { return _entities; }
        }


        public MasterStateRepositoryData()
        {
            IsInitialized = true;
            _entities = new List<MasterState>();

            AddMasterStateEntity(new MasterState()
            {
                StateCode = "AK",
                StateName = "Alaska",
                CountryCode = "US"
            });
            AddMasterStateEntity(new MasterState()
            {
                StateCode = "CA",
                StateName = "California",
                CountryCode = "US"
            });
            AddMasterStateEntity(new MasterState()
            {
                StateCode = "DC",
                StateName = "District of Columbia",
                CountryCode = "US"
            });
            AddMasterStateEntity(new MasterState()
            {
                StateCode = "FL",
                StateName = "Florida",
                CountryCode = "US"
            });

            AddMasterStateEntity(new MasterState()
            {
                StateCode = "WA",
                StateName = "Washington",
                CountryCode = "US"
            });

        }
    }
}
