using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Implementation
{
    public class RefreshTokensRepository : GenericRepository<RefreshTokens>, IRefreshTokensRepository
    {
        public RefreshTokensRepository(IUnitOfWork context)
            : base(context)
        {
        }
        /// <summary>
        /// This method is delete refresh token based on refresh token id.
        /// </summary>
        /// <param name="refreshTokenId"></param>
        /// <returns>Return bool value</returns>
        public Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var getRefreshToken = FindBy(x => x.Id == refreshTokenId);
            if (getRefreshToken != null)
            {
                var firstOrDefault = getRefreshToken.FirstOrDefault();
                if (firstOrDefault != null)
                {
                    try
                    {
                        Delete(firstOrDefault);
                        Save();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }
        /// <summary>
        /// This method is used to insert/update the data to refesh token table based on user inputs
        /// </summary>
        /// <param name="token"></param>
        /// <returns>Return bool value</returns>
        public Task<bool> AddRefreshToken(RefreshTokens token)
        {
            var getAllRefreshToken =
                FindBy(x => x.Subject == token.Subject.Trim() && x.ClientId == token.ClientId && x.Id == token.Id);
            if (!getAllRefreshToken.Any())
            {
                Add(token);
                Save();
                return Task.FromResult(true);
            }
            else
            {
                Update(token, token.Id);
                Save();
                return Task.FromResult(true);
            }
        }
        /// <summary>
        /// This method is used to get the specific refresh token based on token id
        /// </summary>
        /// <param name="refreshTokenId"></param>
        /// <returns>Return refresh token data</returns>
        public Task<IEnumerable<RefreshTokens>> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = FindBy(x => x.Id == refreshTokenId);

            return Task.FromResult(refreshToken);
        }
        /// <summary>
        /// This method is used to get the specific refresh token based on token gui
        /// </summary>
        /// <param name="tokenGui"></param>
        /// <returns>Return refresh token</returns>
        public Task<IEnumerable<RefreshTokens>> FindRefreshTokenPdf(string tokenGui)
        {
            var refreshToken = FindBy(x => x.TokenGui == tokenGui);

            return Task.FromResult(refreshToken);
        }
        /// <summary>
        /// This method is used to get all refresh tokens
        /// </summary>
        /// <returns>Return refresh tokens data</returns>
        public IEnumerable<RefreshTokens> GetAllRefreshToken()
        {
            return GetAll();
        }
        /// <summary>
        /// This method is used to delete particular refresh token based on id.
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns>Return bool value</returns>
        public Task<bool> RemoveRefreshToken(RefreshTokens refreshToken)
        {
            if (refreshToken == null)
            {
                return Task.FromResult(false);
            }
            else
            {
                var getRefreshToken = FindBy(x => x.Id == refreshToken.Id);
                var firstOrDefault = getRefreshToken.FirstOrDefault();
                Delete(firstOrDefault);
                Save();
                return Task.FromResult(true);
            }
        }
        /// <summary>
        /// This method is used to update refresh token data based on user inputs
        /// </summary>
        /// <param name="refreshtoken"></param>
        /// <param name="id"></param>
        /// <returns>Return bool value</returns>
        public Task<bool> UpdateRefreshTokenTime(RefreshTokens refreshtoken, string id)
        {
            var result = Update(refreshtoken, id);
            Save();
            if (result == null)
                return Task.FromResult(false);
            return Task.FromResult(true);
        }
    }
}