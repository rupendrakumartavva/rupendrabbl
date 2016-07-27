using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Service.Interface
{
   public interface IUserPasswordTrackingService
   {
       bool InsertUserPasswordTracking(string userId, string password);

       bool PasswordStatus(string userId, string password);
   }
}
