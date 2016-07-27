using BusinessCenter.Data;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessCenter.Admin.Common
{
    public class BusinessCenterCommon : IBusinessCenterCommon
    {
        private IUserRepository _userrepository;
        public BusinessCenterCommon(IUserRepository userrepository)
        { _userrepository = userrepository; }
        public bool ValidateSessionResult()
           {
               bool result = true;
               string userName = BusinessCenter.Admin.Common.BusinessCenterSession.BusinessCenterUserName.ToString();

               var userdetails = _userrepository.FindByLoginUsername(userName).ToList();
                string currentsession = BusinessCenter.Admin.Common.BusinessCenterSession.BusinessCenterLoginUserSessionId.ToString();
                if (userdetails.Any())
                {
                    var logeduserdetails = userdetails.FirstOrDefault();

                    string loggedsession = logeduserdetails.LoginSessionId.ToString();
                    if (currentsession.ToUpper().Trim() != loggedsession.ToUpper().Trim())
                    {
                        result = false;
                    }
                }
                return result;
        }
    }
}