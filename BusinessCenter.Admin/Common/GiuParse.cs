using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessCenter.Admin.Common
{
    public static  class GiuParse
    {
        public static  string GenarateGuiWithUserId(string id,int insertPart)
        {
            var guid = Guid.NewGuid().ToString();

            var h = guid.Insert(insertPart, id.ToString());
            return h;

        }

        public static string GetUserIdFromGui(string guid, int length,int insertPart)
        {
          var str = guid.Remove(0,insertPart);
            var h = str.Substring(0, length);
            return h;

        }
    }
}