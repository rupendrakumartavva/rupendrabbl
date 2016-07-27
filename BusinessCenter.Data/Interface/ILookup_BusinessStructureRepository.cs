using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
   public interface ILookup_BusinessStructureRepository
    {
       IEnumerable<Lookup_BusinessStructure> GetBusinessStructureAll();
       IEnumerable<Lookup_BusinessStructure> FindByStructure(string businessStructure);
       bool InsertUpdateBusienssStrucutureLookUp(BusinessStructureLookUp businessStructure);
    }
}
