using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data.Model;

namespace BusinessCenter.Service.Interface
{
   public interface IOSubCategoryFeeService
   {
       bool UpdateSubFee(PrimaryPhysicallocation primaryPhysicallocation);
   }
}
