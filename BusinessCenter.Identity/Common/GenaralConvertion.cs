using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Identity.Common
{
    public static class GenaralConvertion
    {
        public static bool CheckDateExpire(string fromDate, string toDate)
        {




            DateTime expireDate = DateTime.Parse(toDate);

            DateTime currentDate = DateTime.Parse(fromDate);

            TimeSpan remainingTimeSpan = expireDate - currentDate;




            if (remainingTimeSpan.TotalSeconds <= 86400 && remainingTimeSpan.TotalSeconds > 0)

                return true;

            return false;


        }
        public static bool CheckDateExpireForLockout(string fromDate, string toDate)
        {




            DateTime expireDate = DateTime.Parse(toDate);

            DateTime currentDate = DateTime.Parse(fromDate);

            TimeSpan remainingTimeSpan = expireDate - currentDate;




            if (remainingTimeSpan.TotalSeconds <= 300 && remainingTimeSpan.TotalSeconds > 0)

                return true;

            return false;


        }
        public static DateTime ValidateGivenTime(string inputTime)
        {
            DateTime? lockoutEndDateUtc = String.IsNullOrEmpty(inputTime.ToString())
                                ? Convert.ToDateTime("01/01/1700")
                                : (DateTime?)Convert.ToDateTime(inputTime.ToString());

            return Convert.ToDateTime(lockoutEndDateUtc);
        }
        public static bool CheckExpireDate(string fromDate, string toDate)
        {




            DateTime expireDate = DateTime.Parse(toDate);

            DateTime currentDate = DateTime.Parse(fromDate);

            TimeSpan remainingTimeSpan = expireDate - currentDate;

            if (remainingTimeSpan.Days != 0)
            { return true; }


            if (remainingTimeSpan.TotalSeconds <= 86400 && remainingTimeSpan.TotalSeconds > 0)

                return true;

            return false;


        }

    }
}
