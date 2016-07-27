using BusinessCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Setup
{
  public  class RoleRepositoryData
    {
       private readonly List<Role> _entities;
        public bool IsInitialized;

        public void AddRoleEntity(Role obj)
        {
            _entities.Add(obj);
        }

        public List<Role> RoleEntitiesList
        {
            get { return _entities; }
        }


        public RoleRepositoryData()
        {
            IsInitialized = true;
            _entities = new List<Role>();

            AddRoleEntity(new Role()
            {
                Id = "1",
                Name = "Super Admin"
                
            });
            AddRoleEntity(new Role()
            {
                Id = "2",
                Name = "Admin"
            });
            AddRoleEntity(new Role()
            {
                Id = "3",
                Name = "Employee"
            });
          

        }

    }
}
