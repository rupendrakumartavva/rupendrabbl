using System.Collections.Generic;
using BusinessCenter.Data;

namespace BusinessCenter.Tests.Setup
{
    public class FixFeeRepositoryData
    {
        private readonly List<FixFee> _entities;
        public bool IsInitialized;

        public void AddFixFeeEntity(FixFee obj)
        {
            _entities.Add(obj);
        }

        public List<FixFee> FixFeeEntitiesList
        {
            get { return _entities; }
        }


        public FixFeeRepositoryData()
        {
            IsInitialized = true;
            _entities = new List<FixFee>();

            AddFixFeeEntity(new FixFee()
            {
                ApplicationFee = (decimal?) 70.0000,
                EndorsementFee = (decimal?)25.0000,
                TradeRegFee = (decimal?)50.0000,
                RAOFee = (decimal?)43.0000,
                eHOPFee = (decimal?)66.0000,
                Id=1

            });
           
        }
    }
}