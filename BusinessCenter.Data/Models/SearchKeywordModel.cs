using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Models
{
   public class SearchKeywordModel
    {
       public string KeyId { get; set; }
       public string Keywords { get; set; }
       public string TypeID { get; set; }
       public string KeywordDid { get; set; }
       public int fullKeyCount { get; set; }
       public int KeyCount { get; set; }
       public string CreatedDate { get; set; }
    }
   public class SearchKeywordModel1
   {
       public string KeyId { get; set; }
       public string Keywords { get; set; }
       public string TypeID { get; set; }
       public string KeywordDid { get; set; }
       public int KeyCount { get; set; }
       public string CreatedDate { get; set; }
   }
    public class AutoFillKeyWord
    {
        public string SearchKeyWord { get; set; }
    }
    public class KewordsCount : IEnumerable
    {
        public string Businesscount { get; set; }
        public string Licensecount { get; set; }
        public string Firstnamecount { get; set; }
        public string Lastnamecount { get; set; }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    public class RegulatorCount
    {
        public string Regulator { get; set; }
        public int RegularCount { get; set; }
    }
}
