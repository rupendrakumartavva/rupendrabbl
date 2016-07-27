using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data;

namespace BusinessCenter.Tests.Setup
{
   public class CountryRepositoryData
    {
       private readonly List<MasterCountry> _entities;
       public void CountryClientEntity(MasterCountry obj)
       {
           _entities.Add(obj);
       }

       public List<MasterCountry> CountryEntitiesList
       {
           get { return _entities; }
       }
       public CountryRepositoryData()
        {
            _entities = new List<MasterCountry>();
            CountryClientEntity(new MasterCountry()
            {
                CountryCode="AD",
               CountryName="Andorra",
               Status=true
            });
            CountryClientEntity(new MasterCountry()
            {
                CountryCode = "AE",
                CountryName = "United Arab Emirates",
                Status = true
            });
        }
    }
}
