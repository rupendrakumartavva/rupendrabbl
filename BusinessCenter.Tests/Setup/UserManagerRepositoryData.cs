using BusinessCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Setup
{
   public class UserManagerRepositoryData
    {
         private readonly List<UserLoginHistory> _entities;
        public bool IsInitialized;

        public void AddUserLoginEntity(UserLoginHistory obj)
        {
            _entities.Add(obj);
        }

        public List<UserLoginHistory> UserLoginEntitiesList
        {
            get { return _entities; }
        }

        public UserManagerRepositoryData()
        {
            IsInitialized = true;
            _entities = new List<UserLoginHistory>();

            AddUserLoginEntity(new UserLoginHistory()
            {
                LoginHisId = 1,
                UserId = "2AC53E53-87D0-468F-9628-4AEBA1120613",
                LastLoginDate =System.DateTime.Now,
                Count = 4
            });
            AddUserLoginEntity(new UserLoginHistory()
            {
                LoginHisId = 2,
                UserId = "8EB70E26-725E-4E52-9109-CF9C37F3B980",
                LastLoginDate = System.DateTime.Now,
                Count = 3
            });
        }
    }
}
