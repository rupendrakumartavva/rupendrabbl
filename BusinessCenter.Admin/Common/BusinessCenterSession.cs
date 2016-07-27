using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessCenter.Admin.Common
{
    public class BusinessCenterSession
    {
        private const string BusinessLoginUserSessionId = "UserLoginSessionId";
        private const string BusinessUserNameSessionId = "UserNameSessionId";
        public static string BusinessCenterLoginUserSessionId
        {
            get
            {
                try
                {
                    string returnbusinessCenterLoginUserSessionId;
                    if (HttpContext.Current.Session[BusinessCenterSession.BusinessLoginUserSessionId] == null)
                        returnbusinessCenterLoginUserSessionId = "";
                    else
                        returnbusinessCenterLoginUserSessionId = HttpContext.Current.Session[BusinessCenterSession.BusinessLoginUserSessionId].ToString();
                    return returnbusinessCenterLoginUserSessionId;
                }
                catch (FormatException)
                {
                    throw;
                }
            }

            set
            {
                HttpContext.Current.Session[BusinessCenterSession.BusinessLoginUserSessionId] = value;
            }
        }

        public static string BusinessCenterUserName
        {
            get
            {
                try
                {
                    string returnbusinessCenterUserNameSessionId;
                    if (HttpContext.Current.Session[BusinessCenterSession.BusinessUserNameSessionId] == null)
                        returnbusinessCenterUserNameSessionId = "";
                    else
                        returnbusinessCenterUserNameSessionId = HttpContext.Current.Session[BusinessCenterSession.BusinessUserNameSessionId].ToString();
                    return returnbusinessCenterUserNameSessionId;
                }
                catch (FormatException)
                {
                    throw;
                }
            }

            set
            {
                HttpContext.Current.Session[BusinessCenterSession.BusinessUserNameSessionId] = value;
            }
        }
    }
}