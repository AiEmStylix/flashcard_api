using flashcard_backend.Models;

namespace flashcard_backend.Interfaces;

public interface IRefreshTokenRepository
{
    Task<IEnumerable<RefreshToken>> GetAllRefreshToken();
    Task<RefreshToken?> GetRefreshToken(string token);
    Task<RefreshToken?> CreateRefreshToken(RefreshToken refreshToken);
    Task<bool> DeleteRefreshToken(RefreshToken refreshToken);
}