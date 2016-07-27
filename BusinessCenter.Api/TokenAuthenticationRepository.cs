using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessCenter.Identity.Interfaces;

namespace BusinessCenter.Api
{
    public class TokenAuthenticationRepository : ITokenAuthenticationRepository
    {
        private readonly IUserManager _userManager;

        public TokenAuthenticationRepository(IUserManager userManager)
        {
            _userManager = userManager;
        }

        


    }
}