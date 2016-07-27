using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data.Model;

namespace BusinessCenter.Service.Interface
{
   public interface IFeeCodeMapService
   {
       bool Checkunits(string quantity);
       bool InsertUpdateFee(PrimaryPhysicallocation primaryPhysicallocation);
       bool UpdateFeecode(PrimaryPhysicallocation primaryPhysicallocation);
   }
}
