using System;

namespace BusinessCenter.Admin.Common
{
    public static class GenaralConvertion
    {
        public static bool CheckDateExpire(string fromDate, string toDate)
        {
          var expireDate = DateTime.Parse(toDate);
            var currentDate = DateTime.Parse(fromDate);
            var remainingTimeSpan = expireDate - currentDate;
            return remainingTimeSpan.TotalSeconds <= 86400 && remainingTimeSpan.TotalSeconds>0;
        }


        public static DateTime ValidateGivenTime(string inputTime)
        {
            var lockoutEndDateUtc = String.IsNullOrEmpty(inputTime.ToString())
                ? Convert.ToDateTime("01/01/1700")
                : (DateTime?)Convert.ToDateTime(inputTime.ToString());

            return Convert.ToDateTime(lockoutEndDateUtc);
        }

        public static bool CheckExpireDate(string fromDate, string toDate)
        {

            var expireDate = DateTime.Parse(toDate);
            var currentDate = DateTime.Parse(fromDate);
            var remainingTimeSpan = expireDate - currentDate;
            if (remainingTimeSpan.Days != 0)
            { return true; }


            if (remainingTimeSpan.TotalSeconds <= 86400 && remainingTimeSpan.TotalSeconds > 0)

                return true;

            return false;


        }
        public static bool CheckDateExpireForLockout(string fromDate, string toDate)
        {
            var expireDate = DateTime.Parse(toDate);
            var currentDate = DateTime.Parse(fromDate);
            var remainingTimeSpan = expireDate - currentDate;

            string g = remainingTimeSpan.TotalSeconds.ToString();
            if (g.Contains("-"))
            {
                return false;
            }
            else
            {
                if (remainingTimeSpan.TotalSeconds <= 300 && remainingTimeSpan.TotalSeconds > 0)

                    return true;

                return false;
            }


        }
    }
}