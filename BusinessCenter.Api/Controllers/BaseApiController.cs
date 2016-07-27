using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessCenter.Api.Models;
using BusinessCenter.Identity;
using BusinessCenter.Identity.IdentityModels;
using BusinessCenter.Identity.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace BusinessCenter.Api.Controllers
{
    /// <summary>
    /// BaseApi for Every ApiController, It defines some common objects.
    /// </summary>
    public class BaseApiController : ApiController
    {
  //   private ModelFactory _modelFactory;

        /// <summary>
        /// This property is used to create the object of ApplicationUserManager
        /// </summary>
       
        protected IHttpActionResult GetErrorResult(ApplicationIdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
