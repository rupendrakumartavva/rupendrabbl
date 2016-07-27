using BusinessCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Setup
{
    public class SearchKeywordRepositoryData
    {
        private readonly List<KeywordMaster> _entities;
        public bool IsInitialized;

        public void AddKeyWordEntity(KeywordMaster obj)
        {
            _entities.Add(obj);
        }

        public List<KeywordMaster> KeyWordEntitiesList
        {
            get { return _entities; }
        }
        public SearchKeywordRepositoryData()
        {
            IsInitialized = true;
            _entities = new List<KeywordMaster>();

            AddKeyWordEntity(new KeywordMaster()
            {
                KeyId =1,
                Keywords = "ABC",
                TypeID = "1",
                SearchCriteria = "ALL",
                CreatedDate=System.DateTime.Now

            });
            AddKeyWordEntity(new KeywordMaster()
            {
                KeyId = 2,
                Keywords = "SYS",
                TypeID = "1",
                SearchCriteria = "ALL",
                CreatedDate = System.DateTime.Now
            });
            AddKeyWordEntity(new KeywordMaster()
            {
                KeyId = 3,
                Keywords = "ICE CREAM",
                TypeID = "1",
                SearchCriteria = "ALL",
                CreatedDate = System.DateTime.Now
            });
            AddKeyWordEntity(new KeywordMaster()
            {
                KeyId = 4,
                Keywords = "INC",
                TypeID = "1",
                SearchCriteria = "ALL",
                CreatedDate = System.DateTime.Now
            });

            AddKeyWordEntity(new KeywordMaster()
            {
                KeyId = 5,
                Keywords = "LIMBIC",
                TypeID = "1",
                SearchCriteria = "ALL",
                CreatedDate = System.DateTime.Now
            });

        

        }
    }
}
