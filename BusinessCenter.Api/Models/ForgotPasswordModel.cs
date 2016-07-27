using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessCenter.Api.Models
{
    public class ForgotPasswordModel
    {
        public string FullName { get; set; }
        public string PasswordLink { get; set; }
    }
}