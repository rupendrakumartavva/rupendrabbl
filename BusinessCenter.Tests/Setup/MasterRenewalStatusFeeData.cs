using BusinessCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Setup
{
   public class MasterRenewalStatusFeeData
    {
       private readonly List<MasterRenewalStatusFee> _entities;
        public bool IsInitialized;

        public void AddMasterRenewalStatusFeeEntity(MasterRenewalStatusFee obj)
        {
            _entities.Add(obj);
        }

        public List<MasterRenewalStatusFee>MasterRenewalStatusFeeEntitiesList
        {
            get { return _entities; }
        }


        public MasterRenewalStatusFeeData()
        {
            IsInitialized = true;
            _entities = new List<MasterRenewalStatusFee>();

            AddMasterRenewalStatusFeeEntity(new MasterRenewalStatusFee()
            {
                RenewalStatusFeesId = 1,
                StatusType = "Lapsed",
                FeeAmount = Convert.ToDecimal(250.00),
                StartRange=-1,
                EndRange = -30
            });
            AddMasterRenewalStatusFeeEntity(new MasterRenewalStatusFee()
            {
                RenewalStatusFeesId = 2,
                StatusType = "Expired",
                FeeAmount = Convert.ToDecimal(250.00),
                StartRange=-31,
                EndRange = -180
            });
            AddMasterRenewalStatusFeeEntity(new MasterRenewalStatusFee()
            {
                RenewalStatusFeesId = 3,
                StatusType = "Expiring Soon",
                FeeAmount = Convert.ToDecimal(350.00),
                StartRange = 1,
                EndRange = 90
            });
          

          
        }
    }
}
