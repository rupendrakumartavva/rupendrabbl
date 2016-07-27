using System.Collections.Generic;
using BusinessCenter.Data;

namespace BusinessCenter.Tests.Setup
{
    public class MasterTaxRevenueRepositoryData
    {
        private readonly List<MasterTaxRevenue> _entities;
        public bool IsInitialized;

        public void AddMasterTaxRevenueEntity(MasterTaxRevenue obj)
        {
            _entities.Add(obj);
        }

        public List<MasterTaxRevenue> MasterTaxRevenueEntitiesList
        {
            get { return _entities; }
        }


        public MasterTaxRevenueRepositoryData()
        {
            IsInitialized = true;
            _entities = new List<MasterTaxRevenue>();

            AddMasterTaxRevenueEntity(new MasterTaxRevenue()
            {
                TaxRevenueId = 1,
                TextFEINNumber = "11-1111111",
                Type = "FEIN",
                IsCleanHands =true
               

            });
            AddMasterTaxRevenueEntity(new MasterTaxRevenue()
            {
                TaxRevenueId = 2,
                TextFEINNumber = "56-0832111",
                Type = "FEIN",
               

            });

            AddMasterTaxRevenueEntity(new MasterTaxRevenue()
            {
                TaxRevenueId = 3,
                TextFEINNumber = "111-11-1234",
                Type = "SSN",
                IsCleanHands = true


            });

            AddMasterTaxRevenueEntity(new MasterTaxRevenue()
            {
                TaxRevenueId = 4,
                TextFEINNumber = "123-45-6789",
                Type = "SSN",
                IsCleanHands = false


            });
        }

    }
}