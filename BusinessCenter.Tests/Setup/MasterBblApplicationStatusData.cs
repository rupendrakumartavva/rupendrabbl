using BusinessCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Setup
{
  public  class MasterBblApplicationStatusData
    {
        private readonly List<MasterBblApplicationStatus> _entities;
        public bool IsInitialized;

        public void AddMasterBblApplicationStatusEntity(MasterBblApplicationStatus obj)
        {
            _entities.Add(obj);
        }

        public List<MasterBblApplicationStatus>MasterBblApplicationStatusEntitiesList
        {
            get { return _entities; }
        }


        public MasterBblApplicationStatusData()
        {
            IsInitialized = true;
            _entities = new List<MasterBblApplicationStatus>();

            AddMasterBblApplicationStatusEntity(new MasterBblApplicationStatus()
            {
                BblApplicationStatusId = 1,
                Application_Cap_Status = "Active",
                Changed_Cap_Status = "Active"
            });
          


          

        }
    }
}
