using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data;

namespace BusinessCenter.Tests.Setup
{

    public class ClientsRepositoryData
    {
        private readonly List<Clients> _entities;

        public void AddClientEntity(Clients obj)
        {
            _entities.Add(obj);
        }

        public List<Clients> ClientEntitiesList
        {
            get { return _entities; }
        }

        public ClientsRepositoryData()
        {
            _entities = new List<Clients>();
            AddClientEntity(new Clients()
            {
                Id = "352549070fb44ce793a5343a5f846dcc",
                Secret = "5YV7M1r981yoGhELyB84aC+KiYksxZf1OY3++C1CtRM=",
                Name = "AuthKey",
                ApplicationType = 0,
                Active = true,
                RefreshTokenLifeTime = 30,
                AllowedOrigin = "http://dcbctest.codeitinc.com"
            });
        }
    
}
}
