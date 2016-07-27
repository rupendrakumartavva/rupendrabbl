using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data;

namespace BusinessCenter.Tests.Setup
{
    public class FeeCodeMapRepositoryData
    {
        private readonly List<FeeCodeMap> _entities;
        public void AddFeeCodeMapEntity(FeeCodeMap obj)
        {
            _entities.Add(obj);
        }

        public List<FeeCodeMap> FeeCodeMapEntitiesList
        {
            get { return _entities; }
        }
          public FeeCodeMapRepositoryData()
        {

            _entities = new List<FeeCodeMap>();

            AddFeeCodeMapEntity(new FeeCodeMap()
            {
                FeeCodeId=1,
                FeeCode = "T",
                Quantity="Rooms",
                IsTier=false,
                Status=null
            });
            AddFeeCodeMapEntity(new FeeCodeMap()
            {
                FeeCodeId = 2,
                FeeCode = "TA",
                Quantity = "Rooms",
                IsTier = true,
                Status = null
            });
            AddFeeCodeMapEntity(new FeeCodeMap()
            {
                FeeCodeId = 3,
                FeeCode = "T",
                Quantity = "Machines",
                IsTier = false,
                Status = null
            });
            AddFeeCodeMapEntity(new FeeCodeMap()
            {
                FeeCodeId = 4,
                FeeCode = "TA",
                Quantity = "Machines",
                IsTier = true,
                Status = null
            });
            AddFeeCodeMapEntity(new FeeCodeMap()
            {
                FeeCodeId = 5,
                FeeCode = "C",
                Quantity = "Seats",
                IsTier = true,
                Status = null
            });
            AddFeeCodeMapEntity(new FeeCodeMap()
            {
                FeeCodeId = 6,
                FeeCode = "C",
                Quantity = "Flats",
                IsTier = true,
                Status = null
            });
            AddFeeCodeMapEntity(new FeeCodeMap()
            {
                FeeCodeId = 7,
                FeeCode = "H",
                Quantity = "Kitchens",
                IsTier = false,
                Status = null
            });
            AddFeeCodeMapEntity(new FeeCodeMap()
            {
                FeeCodeId = 8,
                FeeCode = "HT",
                Quantity = "Kitchens",
                IsTier = true,
                Status = null
            });
        }
    }
}
