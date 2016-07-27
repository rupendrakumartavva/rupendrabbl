using System.Collections.Generic;
using BusinessCenter.Data;

namespace BusinessCenter.Tests.Setup
{
    public class MasterCountryData
    {
        private readonly List<MasterCountry> _entities;
        public bool IsInitialized;

        public void AddMasterCountryEntity(MasterCountry obj)
        {
            _entities.Add(obj);
        }

        public List<MasterCountry> MasterCountryEntitiesList
        {
            get { return _entities; }
        }

        public MasterCountryData()
        {
            IsInitialized = true;
            _entities = new List<MasterCountry>();

            AddMasterCountryEntity(new MasterCountry()
            {
                CountryCode = "AD",
                CountryName = "Andorra",
                Status=true
            });
            AddMasterCountryEntity(new MasterCountry()
            {
                CountryCode = "IN",
                CountryName = "India",
                Status = true
            });
            AddMasterCountryEntity(new MasterCountry()
            {
                CountryCode = "US",
                CountryName = "United States",
                Status = true
            });
           
        }
    }
}