using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BusinessCenter.Api.Models;
using BusinessCenter.Data;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Interface;

namespace BusinessCenter.Api.Controllers
{
     [RoutePrefix("api/RefreshToken")]
    public class RefreshTokenController : ApiController
    {
        private readonly IRefreshTokensRepository _refreshTokensRepository;
 
        public RefreshTokenController(IRefreshTokensRepository refreshTokensRepository)
        {
            _refreshTokensRepository = refreshTokensRepository;
        }

        
      
        [HttpPost]
        [Route("deleterefresh")]
        public async Task<IHttpActionResult> Delete(ModelFactory.DeleteRefreshToken tokenId)
        {
            var result = await _refreshTokensRepository.RemoveRefreshToken(Helper.GetHash(tokenId.RefreshTokenId));
            if (result)
            {
                return Ok();
            }
            return BadRequest("Token Id does not exist");

        }
    }
}
