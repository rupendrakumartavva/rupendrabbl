using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data.Model;

namespace BusinessCenter.Data.Interface
{
    public interface IFeeCodeMapRepository
    {
        IEnumerable<FeeCodeMap> FindByID(string quantity);
        IEnumerable<OSub_Category_Fees> FindBycategoryID(string quantity, string description, int item);
        IEnumerable<OSub_Category_Fees> FindBycategoryName(string quantity, string description);
        bool Checkunits(string quantity);
        bool InsertUpdateFee(PrimaryPhysicallocation primaryPhysicallocation);
        bool UpdateFeecode(PrimaryPhysicallocation primaryPhysicallocation);
        //   bool UpdateFeecode(string oldUnits, string newUnits);
    }
}
