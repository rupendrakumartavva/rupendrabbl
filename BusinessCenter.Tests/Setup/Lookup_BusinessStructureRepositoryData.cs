using BusinessCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Setup
{
   public class Lookup_BusinessStructureRepositoryData
    {
       private readonly List<Lookup_BusinessStructure> _entities;
       public void AddBusinessStructureEntity(Lookup_BusinessStructure obj)
        {
            _entities.Add(obj);
        }

       public List<Lookup_BusinessStructure> BusienssStructureEntitiesList
        {
            get { return _entities; }
        }
       public Lookup_BusinessStructureRepositoryData()
        {

            _entities = new List<Lookup_BusinessStructure>();


            AddBusinessStructureEntity(new Lookup_BusinessStructure()
            {
                LookUpBusinessStructureId = 1,
                BusinessStructure = "Corporation (For Profit)",
                IsManualAddress = false
            });
            AddBusinessStructureEntity(new Lookup_BusinessStructure()
            {
                LookUpBusinessStructureId = 2,
                BusinessStructure = "Corporation (Non-Profit)",
                IsManualAddress = true
            });
            AddBusinessStructureEntity(new Lookup_BusinessStructure()
            {
                LookUpBusinessStructureId = 3,
                BusinessStructure = "Limited Liability Company (LLC)",
                IsManualAddress = false
            });
        }
    }
}
