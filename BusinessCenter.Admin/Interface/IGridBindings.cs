using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Admin.Models;

namespace BusinessCenter.Admin.Interface
{
   public  interface IGridBindings
   {
       Task<List<RegisterUserModel>> TypeBasedList(UserTypeModel usertypemodel, int roleId);
       Task<List<RegisterUserModel>> UserTypeBasedList(UserTypeModel usertypemodel);
   }
}
