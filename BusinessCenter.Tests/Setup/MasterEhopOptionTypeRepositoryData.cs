using BusinessCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Setup
{
   public class MasterEhopOptionTypeRepositoryData
    {
        private readonly List<MasterEhopOptionType> _entities;
        public bool IsInitialized;

        public void AddMasterEhopOptionTypeEntity(MasterEhopOptionType obj)
        {
            _entities.Add(obj);
        }

        public List<MasterEhopOptionType> MasterEhopOptionTypeEntitiesList
        {
            get { return _entities; }
        }


        public MasterEhopOptionTypeRepositoryData()
        {
            IsInitialized = true;
            _entities = new List<MasterEhopOptionType>();

            AddMasterEhopOptionTypeEntity(new MasterEhopOptionType()
            {
                EhopOptionId =1,
                EhopOptionName = "Home office of a business person"
               
            });
            AddMasterEhopOptionTypeEntity(new MasterEhopOptionType()
            {
                EhopOptionId = 2,
                EhopOptionName = "Home office of a salesperson"
            });
            AddMasterEhopOptionTypeEntity(new MasterEhopOptionType()
            {
                EhopOptionId = 3,
                EhopOptionName = "Independent consultant, journalist, and writer"
            });
            AddMasterEhopOptionTypeEntity(new MasterEhopOptionType()
            {
                EhopOptionId = 4,
                EhopOptionName = "Typing, word processing or computing programming services"
            });

            AddMasterEhopOptionTypeEntity(new MasterEhopOptionType()
            {
                EhopOptionId = 5,
                EhopOptionName = "Telemarketing services"
            });


        }
    }
}
