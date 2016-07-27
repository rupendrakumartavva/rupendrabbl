using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
    public interface IRefreshTokensRepository
    {
        Task<bool> RemoveRefreshToken(string refreshTokenId);

        Task<IEnumerable<RefreshTokens>> FindRefreshToken(string refreshTokenId);

        IEnumerable<RefreshTokens> GetAllRefreshToken();

        Task<bool> RemoveRefreshToken(RefreshTokens refreshToken);

        Task<bool> AddRefreshToken(RefreshTokens token);

        Task<IEnumerable<RefreshTokens>> FindRefreshTokenPdf(string tokenGui);

        Task<bool> UpdateRefreshTokenTime(RefreshTokens token, string Id);
    }
}