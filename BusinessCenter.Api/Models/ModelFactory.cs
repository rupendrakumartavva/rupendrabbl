using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using BusinessCenter.Data;
using BusinessCenter.Identity.IdentityModels;
using BusinessCenter.Identity.Interfaces;

namespace BusinessCenter.Api.Models
{
    public class ModelFactory
    {
        private UrlHelper _UrlHelper;
        private IUserManager _userManager;

        public ModelFactory(HttpRequestMessage request, IUserManager userManager)
        {
            _UrlHelper = new UrlHelper(request);
            _userManager = userManager;
        }

        public RegisterUserModel Create(AppUser userDetails)
        {
            return new RegisterUserModel
            {
             //   Url = _UrlHelper.Link("GetUserById", new { id = appUser.Id }),
               
           
                FirstName = userDetails.FirstName,
                LastName = userDetails.LastName,
             
                UserName = userDetails.UserName,
                Email = userDetails.Email,
              
                SecurityQuestion1 = userDetails.SecurityQuestion1,
                SecurityQuestion2 = userDetails.SecurityQuestion2,
                SecurityQuestion3 = userDetails.SecurityQuestion3,
                SecurityAnswer1 = userDetails.SecurityAnswer1,
                SecurityAnswer2 = userDetails.SecurityAnswer2,
                SecurityAnswer3 = userDetails.SecurityAnswer3,
            
            };

        }

        public RoleReturnModel Create(Role appRole)
        {

            return new RoleReturnModel
            {
                Url = _UrlHelper.Link("GetRoleById", new { id = appRole.Id }),
                Id  = appRole.Id,
                Name = appRole.Name
            };

        }

        public class RoleReturnModel
        {
            public string Url { get; set; }
            public string Id { get; set; }
            public string Name { get; set; }
        }

        public class DeleteRefreshToken
        {
            public string RefreshTokenId { get; set; }
          
        }

        public class SubmissionEntityModel
        {
            public string MasterId { get; set; }

        }

        public class MasterStateModel
        {
            public string CountryCode { get; set; }

        }
    }
}