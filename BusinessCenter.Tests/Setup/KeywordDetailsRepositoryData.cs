using BusinessCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Setup
{
    public class KeywordDetailsRepositoryData
    {
        private readonly List<KeywordDetails> _entities;
      

        public void AddKeyDetailsEntity(KeywordDetails obj)
        {
            _entities.Add(obj);
        }

        public List<KeywordDetails> KeyDetailsEntitiesList
        {
            get { return _entities; }
        }

        public KeywordDetailsRepositoryData()
        {

            _entities = new List<KeywordDetails>();
            AddKeyDetailsEntity(new KeywordDetails()
            {
                KeywordDid=1,
                KeyId=1,
                KeyCount=3,
                CreatedDate=System.DateTime.Now

            });
            AddKeyDetailsEntity(new KeywordDetails()
             {
                 KeywordDid = 2,
                 KeyId = 2,
                 KeyCount = 30,
                 CreatedDate = Convert.ToDateTime("2015-06-12 00:00:00.000")
             });
        }
    }
}
