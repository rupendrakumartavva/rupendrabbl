using System;
using System.Collections.Generic;
using BusinessCenter.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Setup
{
    public class UserRoleRepositoryData
    {
        private readonly List<UserRole> _entities;
        public bool IsInitialized;

        public void AddUserRoleEntity(UserRole obj)
        {
            _entities.Add(obj);
        }

        public List<UserRole> UserRolesEntitiesList
        {
            get { return _entities; }
        }

        public UserRoleRepositoryData()
        {
            IsInitialized = true;
            _entities = new List<UserRole>();

            AddUserRoleEntity(new UserRole()
            {
               Id = 1,
               UserId = "2AC53E53-87D0-468F-9628-4AEBA1120613",
               RoleId = "1"
            });
            AddUserRoleEntity(new UserRole()
            {
              Id = 2,
              UserId = "8EB70E26-725E-4E52-9109-CF9C37F3B980",
              RoleId = "3"
            });
        }
    }
}
