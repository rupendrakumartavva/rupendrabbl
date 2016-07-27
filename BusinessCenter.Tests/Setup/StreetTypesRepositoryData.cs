using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data;

namespace BusinessCenter.Tests.Setup
{
    public class StreetTypesRepositoryData
    {
        private readonly List<StreetTypes> _entities;
        public bool IsInitialized;

        public void AddStreetType(StreetTypes obj)
        {
            _entities.Add(obj);
        }

        public List<StreetTypes> ListAll
        {
            get { return _entities; }
        }

        public StreetTypesRepositoryData()
        {
            IsInitialized = true;
            _entities = new List<StreetTypes>();

            AddStreetType(new StreetTypes()
            {
                StreetTypeId = 1,
                StreetType = "Alley",
                StreetCode = "AL"
            });

            AddStreetType(new StreetTypes()
            {
                StreetTypeId = 2,
                StreetType = "Road",
                StreetCode = "Road"
            });
           
        }
    }
}
